using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

//Author: Azize Abuduaini 
//Modified: Krishnasamy Kalyani 
public partial class Department_HOD_Approval : System.Web.UI.Page
{
    //When page loads
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btn1.Style.Add("display", "none");
                btn2.Style.Add("display", "none");
                dataBind();
            }
            
        }

        //Bind the Requisition Approval
        public void dataBind()
        {
            RequisitionController rc = new RequisitionController();
            //Get login employee
            Employee emp = new Employee();
            emp = (Employee)Session["empId"];
            EmployeeController empCtrl = new EmployeeController();

            //Get all requisition requisition
            RequisitionApprovalTransaction rt = new RequisitionApprovalTransaction();
            List<RequisitionApprovalTransaction> listRq = new List<RequisitionApprovalTransaction>();
            listRq = rc.GetApprovalRequistion(emp.DeptCode);

            //Create new table
            DataTable table = new DataTable();
            table.Columns.Add("Employee");
            table.Columns.Add("Date");
            table.Columns.Add("RequisitionNumber");
            table.Columns.Add("Status");
            table.Columns.Add("Reason");

            //Add row
            foreach (var array in listRq)
            {
                table.Rows.Add(empCtrl.getEmployee(array.EmpId).Name, array.ApprovalDate.ToShortDateString(), array.RequisitionId, getStatus(array.StatusId));
            }
            requiAppGridView.DataSource = table;
            requiAppGridView.DataBind();
        }

        //When the GridView page changes
        protected void requiAppGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            requiAppGridView.PageIndex = e.NewPageIndex;
            dataBind();
        }

        //Transfer the status from number into words
        public String getStatus(int id)
        {
            switch (id)
            {
                case 0:
                    return "Pending";
                case 1:
                    return "Rejected";
                case 2:
                    return "Approved";
            }
            return "unknown";
        }

        //Get the employee id of the login employee
        public string sendDepCode()
        {
            Employee emp = new Employee();
            emp = (Employee)Session["empId"];
            string s = emp.DeptCode;
            return s;
        }

        //When the user clicks "View" button
        protected void detBtn_Click(object sender, EventArgs e)
        {
            mpe1.Show();
            LinkButton detBtn = sender as LinkButton;
            if (detBtn != null)
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;

                int rec_id = Convert.ToInt32(((Label)(requiAppGridView.Rows[index].Cells[2].FindControl("requiNum"))).Text);

                //Get the list of Requisition transaction details
                RequisitionController rq = new RequisitionController();
                RequisitionTransactionDetail rtd = new RequisitionTransactionDetail();
                List<RequisitionTransactionDetail> listRtd = new List<RequisitionTransactionDetail>();
                CatelogueController catController = new CatelogueController();

                //Get requistion by requisition id
                listRtd = rq.getRequisitionDetailbyID(rec_id);
                empName.Text = ((Label)(requiAppGridView.Rows[index].Cells[0].FindControl("Label1"))).Text;

                //Create new table
                DataTable table = new DataTable();
                table.Columns.Add("Description");
                table.Columns.Add("Qty");
                table.Columns.Add("UOM");
          
                Catelogue cat = new Catelogue();

                // Add rows.
                foreach (var array in listRtd)
                {
                    cat = catController.GetCatelogue(array.ItemNumber);
                    table.Rows.Add(cat.Description, array.Quantity, cat.UnitOfMeasure);
                }
                reqDetails.DataSource = table;
                reqDetails.DataBind();
            }
        }

        //When the user clicks "Approve"
        protected void appBtn_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            if (button != null)
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;

                //Get approval requistion 
                RequisitionController re = new RequisitionController();
                RequisitionApprovalTransaction rat = new RequisitionApprovalTransaction();
                List<RequisitionApprovalTransaction> List = re.GetApprovalRequistion(sendDepCode());

                //And the information of the requisition approval
                rat.EmpId = List[index].EmpId;
                rat.DeptCode = requiAppGridView.Rows[index].Cells[6].Text;
                rat.RequisitionId = Convert.ToInt32(((Label)(requiAppGridView.Rows[index].Cells[2].FindControl("requiNum"))).Text);
                rat.Remarks = "";
                rat.StatusId = 2;
                rat.ApprovalDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                rat.DeptCode = sendDepCode();
                re.ApprovedRequistion(rat);
                dataBind();
            }
        }

        //When the user clicks "Reject"
        protected void rejBtn_Click(object sender, EventArgs e)
        {
            LinkButton button = sender as LinkButton;
            if (button != null)
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;

                //Get the approval requisition
                RequisitionController re = new RequisitionController();
                RequisitionApprovalTransaction rat = new RequisitionApprovalTransaction();
                List<RequisitionApprovalTransaction> List = re.GetApprovalRequistion(sendDepCode());

                //Add information to requisition approval transaction
                TextBox reason = ((TextBox)(requiAppGridView.Rows[index].Cells[4].FindControl("txtReason")));
                string r = reason.Text;
                rat.EmpId = List[index].EmpId;
                rat.DeptCode = requiAppGridView.Rows[index].Cells[6].Text;
                rat.RequisitionId = Convert.ToInt32(((Label)(requiAppGridView.Rows[index].Cells[2].FindControl("requiNum"))).Text);
                rat.Remarks = r;
                rat.StatusId = 1;
                rat.ApprovalDate = Convert.ToDateTime(DateTime.Now.Date.ToString("yyyy-MM-dd"));
                rat.DeptCode = sendDepCode();
                re.ApprovedRequistion(rat);
                dataBind();
            }
        }
    }
