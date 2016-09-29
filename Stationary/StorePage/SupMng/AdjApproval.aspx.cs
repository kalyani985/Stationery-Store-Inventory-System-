using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;
public partial class StorePage_SupMng_AdjApproval : System.Web.UI.Page
{//Intantialize object
        Employee emp = new Employee();
        Catelogue cat = new Catelogue();

        AdjustmentController ac = new AdjustmentController();
        CatelogueController c = new CatelogueController();

        List<AdjustmentApproval> avList = new List<AdjustmentApproval>();
        List<StockCardTransaction> sct = new List<StockCardTransaction>();
        List<AdjustmentVoucher> aList = new List<AdjustmentVoucher>();

        DataTable table = new DataTable();

        //When the page loads
        protected void Page_Load(object sender, EventArgs e)
        {
            //When is not postback
            if (!IsPostBack)
            {
                btn1.Style.Add("display", "none");
                btn2.Style.Add("display", "none");
                BindAdjustment();
            }
           
        }

        //Adjustment Voucher Approval List
        private void BindAdjustment()
        {   
            //Get the info of the employee who logs in 
            emp = (Employee)Session["empId"];
            
            //Get the list of Adjusment Voucher Approval needs to be decided by the employee (Supervisor/Manager)
            avList = ac.GetALLAdjustmentVoucherApproval(emp);

            //Create the Datatable
            table.Columns.Add("Date");
            table.Columns.Add("AdjustmentNumber");
            table.Columns.Add("Status");
            table.Columns.Add("Remarks");
            
            //Transfer the status from number into words
            for (int i = 0; i < avList.Count; i++)
            {
                string status = null;
                if (avList[i].StatusId == 0)//StatusId == 0 means "Pending"
                      {
                        status = "Pending";
                      }
                else if (avList[i].StatusId == 2)//StatusId == 2 means "Approved"
                     {

                         if (avList[i].UserLevelId == 7)//UserLevelId == 7 means Manager
                        {
                            status = "Approved By Manager";
                        }
                         else if (avList[i].UserLevelId == 6)//UserLevelId == 7 means Supervisor
                        {
                            status = "Approved By Supervisor";
                        }
                     }
                else if (avList[i].StatusId == 1)//StatusId == 1 means "Rejected"
                     {

                         if (avList[i].UserLevelId == 7)
                        {
                            status = "Rejected By Manager";//UserLevelId == 7 means Manager
                        }
                         else if (avList[i].UserLevelId == 6)
                        {
                            status = "Rejected By Supervisor";//UserLevelId == 7 means Supervisor
                        }
                     }

                table.Rows.Add(avList[i].ApprovalDate.ToShortDateString(), avList[i].AdjustmentNumber, status, avList[i].Remarks);
                
            }
            appGirdView.DataSource = table;
            appGirdView.DataBind();
            btnAssign(avList);
        }

        //For viewing the details of each Adjustment Voucher Approval
        protected void detBtn_Click(object sender, EventArgs e)
        {
            mpe1.Show();
            LinkButton detBtn = sender as LinkButton;
            if (detBtn != null)
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                adNo.Text = ((Label)(appGirdView.Rows[index].Cells[1].FindControl("Label2"))).Text;
                appDate.Text = ((Label)(appGirdView.Rows[index].Cells[1].FindControl("Label1"))).Text;

                //Get the list of Adjusment Voucher Approval needs to be decided by the employee (Supervisor/Manager)
                string adjustId = adNo.Text;
                sct = ac.GetAdjustmentVoucherDetailbyID(adjustId);

                table.Columns.Add("ItemNumber");
                table.Columns.Add("Description");
                table.Columns.Add("QuantityAdjusted");
                table.Columns.Add("Reason");

                foreach (StockCardTransaction item in sct)
                {
                    cat = c.GetCatelogue(item.ItemNumber);
                    table.Rows.Add(cat.ItemNumber, cat.Description, item.Quantity, item.Reason);
                }
                adDetail.DataSource = table;
                adDetail.DataBind();
            }
        }

        //When the user clicks "Approve" Button
        protected void appBtn_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            if (button != null)
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                AdjustmentController ac = new AdjustmentController();
                emp = (Employee)Session["empId"];
                string adjustmentNumber = ((Label)(appGirdView.Rows[index].Cells[1].FindControl("Label2"))).Text;
                string r = "";
                ac.ApprovedAdjustmentVoucher(adjustmentNumber, emp.UserLevelId, 2, r);
                BindAdjustment();
            }
        }

        //When the user clicks "Reject" Button
        protected void rejBtn_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            if (button != null)
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                AdjustmentController ac = new AdjustmentController();
                emp = (Employee)Session["empId"];
                string adjustmentNumber = ((Label)(appGirdView.Rows[index].Cells[1].FindControl("Label2"))).Text;
                TextBox reason = ((TextBox)(appGirdView.Rows[index].Cells[3].FindControl("txtReason")));
                string r = reason.Text;
                ac.ApprovedAdjustmentVoucher(adjustmentNumber, emp.UserLevelId,1,r);
                BindAdjustment();
            }
        }

        //Decide which button should be enabled or disabled
        private void btnAssign(List<AdjustmentApproval> aaList)
        {
            int CurrentRow = 0;
            foreach (GridViewRow gvr in appGirdView.Rows)
            {
                LinkButton appBtn = (LinkButton)gvr.FindControl("appBtn");
                LinkButton rejBtn = (LinkButton)gvr.FindControl("rejBtn");

                //When the adjustment voucher has already been approved or rejected, the user cant click the 
                //"Approve" or "Reject" button
                if (aaList[CurrentRow].StatusId == 2 || aaList[CurrentRow].StatusId == 1)
                {
                    appBtn.Enabled = false;
                    rejBtn.Enabled = false;
                }
                else
                {
                    appBtn.Enabled = true;
                    rejBtn.Enabled = true;
                }
                CurrentRow++;
            }
        }
    }
