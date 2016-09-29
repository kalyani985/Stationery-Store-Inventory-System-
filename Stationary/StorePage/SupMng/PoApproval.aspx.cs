using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

public partial class StorePage_SupMng_PoApproval : System.Web.UI.Page
{
    //Intantialize object
    Employee emp = new Employee();
    Catelogue cat = new Catelogue();

    PurchaseOrderController poc = new PurchaseOrderController();
    CatelogueController c = new CatelogueController();

    List<PurchaseOrderApproval> poalist = new List<PurchaseOrderApproval>();

    DataTable table = new DataTable();

    //When the page loads
    protected void Page_Load(object sender, EventArgs e)
    {
        //When is not postback
        if (!IsPostBack)
        {
            btn1.Style.Add("display", "none");
            btn2.Style.Add("display", "none");
            BindPurchaseOrder();
        }
    }

    //Purchase Order Approval List
    private void BindPurchaseOrder()
    {
        //Get the info of the employee who logs in 
        emp = (Employee)Session["empId"];

        //Get the list of Purchase Order Approval needs to be decided by the employee (Supervisor/Manager)
        poalist = poc.GetALLPurchaseOrderApproval(emp.EmpId);

        table.Columns.Add("Date");
        table.Columns.Add("PONo");
        table.Columns.Add("Status");
        table.Columns.Add("Remarks");

        //Transfer the status from number into words
        for (int i = 0; i < poalist.Count; i++)
        {
            string status = null;
            if (poalist[i].StatusId == 0)
            {
                status = "Pending";
            }
            else if (poalist[i].StatusId == 2)//StatusId == 2 means "Approved"
            {

                if (poalist[i].UserLevelId == 7)//UserLevelId == 7 means Manager
                {
                    status = "Approved By Manager";
                }
                else if (poalist[i].UserLevelId == 6)//UserLevelId == 7 means Supervisor
                {
                    status = "Approved By Supervisor";
                }
            }
            else if (poalist[i].StatusId == 1)//StatusId == 1 means "Rejected"
            {

                if (poalist[i].UserLevelId == 7)//UserLevelId == 7 means Manager
                {
                    status = "Rejected By Manager";
                }
                else if (poalist[i].UserLevelId == 6)
                {
                    status = "Rejected By Supervisor";
                }
            }
            table.Rows.Add(poalist[i].Date.ToShortDateString(), poalist[i].PONo, status, poalist[i].Remarks);
        }
        poappGirdView.DataSource = table;
        poappGirdView.DataBind();
        btnAssign(poalist);
    }

    //When the user clicks "View" Button
    protected void detBtn_Click(object sender, EventArgs e)
    {
        mpe1.Show();
        LinkButton detBtn = sender as LinkButton;
        if (detBtn != null)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            poNo1.Text = ((Label)(poappGirdView.Rows[index].Cells[1].FindControl("Label2"))).Text;
            int poNo = Convert.ToInt32(poNo1.Text);
            PurchaseOrderController poc = new PurchaseOrderController();
            List<PurchaseOrderDetail> pod = new List<PurchaseOrderDetail>();
            pod = poc.GetPurchaseOrderbyID(poNo);
            string name = null;
            foreach (PurchaseOrderDetail item in pod)
            {
                name = item.Catelogue.Supplier.SupplierName;
            }
            suppName.Text = name;

            DataTable table = new DataTable();
            table.Columns.Add("ItemNumber");
            table.Columns.Add("Description");
            table.Columns.Add("Quantity");
            table.Columns.Add("UnitPrice");
            table.Columns.Add("Amount");

            foreach (PurchaseOrderDetail item in pod)
            {
                cat = c.GetCatelogue(item.ItemCode);
                table.Rows.Add(cat.ItemNumber, cat.Description, item.Quantity, cat.Price, item.Quantity * cat.Price);
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
            PurchaseOrderController pc = new PurchaseOrderController();
            emp = (Employee)Session["empId"];
            int poNo = Convert.ToInt32(((Label)(poappGirdView.Rows[index].Cells[1].FindControl("Label2"))).Text);
            pc.ApprovedPurchaseOrder(poNo, emp.EmpId, 2, "");
            BindPurchaseOrder();
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
            PurchaseOrderController pc = new PurchaseOrderController();
            emp = (Employee)Session["empId"];
            int poNo = Convert.ToInt32(((Label)(poappGirdView.Rows[index].Cells[1].FindControl("Label2"))).Text);
            TextBox reason = ((TextBox)(poappGirdView.Rows[index].Cells[3].FindControl("txtReason")));
            string r = reason.Text;
            pc.ApprovedPurchaseOrder(poNo, emp.EmpId, 1, r);
            BindPurchaseOrder();
        }
    }

    //Decide which button should be enabled or disabled
    private void btnAssign(List<PurchaseOrderApproval> poaList)
    {
        int CurrentRow = 0;
        foreach (GridViewRow gvr in poappGirdView.Rows)
        {
            LinkButton appBtn = (LinkButton)gvr.FindControl("appBtn");
            LinkButton rejBtn = (LinkButton)gvr.FindControl("rejBtn");

            //When the adjustment voucher has already been approved or rejected, the user cant click the 
            //"Approve" or "Reject" button
            if (poaList[CurrentRow].StatusId == 2 || poaList[CurrentRow].StatusId == 1)
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