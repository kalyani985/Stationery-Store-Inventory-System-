using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

public partial class StorePage_Adjustment : System.Web.UI.Page
{
    //Intantialize object
    AdjustmentVoucher av = new AdjustmentVoucher();
    List<AdjustmentVoucher> listAv = new List<AdjustmentVoucher>();
    List<AdjustmentApproval> aaList = new List<AdjustmentApproval>();
    List<StockCardTransaction> listSt = new List<StockCardTransaction>();
    AdjustmentController ac = new AdjustmentController();
    StockCardController sc = new StockCardController();
    EmployeeController ec = new EmployeeController();

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

    //Adjustment Voucher List
    public void BindAdjustment()
    {

        Employee emp = (Employee)Session["empId"];

        //Get all adjustment trasaction for employee
        listAv = ac.GetAllAdjustmentVoucher(emp);

        //Create the Datatable
        DataTable table = new DataTable();
        table.Columns.Add("AdjustmentNumber");
        table.Columns.Add("Date");
        table.Columns.Add("Status");
        table.Columns.Add("Remarks");

        // Add rows.
        for (int i = 0; i < listAv.Count; i++)
        {
            //Transfer the status from number into words
            string status = null;
            aaList = ac.GetAdjustmentApproval(listAv[i].AdjustmentNumber);
            //string remarks = null;
            if (listAv[i].StatusId == 0)//StatusId == 0 means "Pending"
            {
                status = "Pending";

            }
            else if (listAv[i].StatusId == 2)//StatusId == 2 means "Approved"
            {

                if (aaList.Count() < 2)//When the count of the adjustment approval is less than two, it means "Approved By Supervisor"
                {
                    status = "Approved By Supervisor";
                }
                else//When the count of the adjustment approval is more than two, it means "Approved By Manager"
                {
                    status = "Approved By Manager";
                }
            }
            else if (listAv[i].StatusId == 1)///StatusId == 1 means "Rejected"
            {
                if (aaList.Count() < 2)//When the count of the adjustment approval is less than two, it means "Rejected By Supervisor"
                {
                    status = "Rejected By Supervisor";
                }
                else//When the count of the adjustment approval is more than two, it means "Rejected By Manager"
                {
                    status = "Rejected By Manager";
                }
            }
            table.Rows.Add(listAv[i].AdjustmentNumber, listAv[i].Date.ToShortDateString(), status);
        }
        GridView1.DataSource = table;
        GridView1.DataBind();
    }

    //When the user clicks "View Detail"
    protected void detBtn_Click(object sender, EventArgs e)
    {
        //The pop up will show
        mpe1.Show();
        LinkButton detBtn = sender as LinkButton;
        if (detBtn != null)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            adNo.Text = GridView1.Rows[index].Cells[0].Text;
            string adjId = adNo.Text;
            listSt = ac.GetAdjustmentVoucherDetailbyID(adjId);

            //Create new table
            DataTable table = new DataTable();
            table.Columns.Add("ItemNo");
            table.Columns.Add("Description");
            table.Columns.Add("Qty");
            table.Columns.Add("AddRemove");
            table.Columns.Add("Reason");

            // Add rows.
            foreach (var array in listSt)
            {
                table.Rows.Add(array.ItemNumber, array.Catelogue.Description, array.Quantity, getStatus(array.AddRemove), array.Reason);
            }

            adjDetail.DataSource = table;
            adjDetail.DataBind();
        }
    }

    //Get the status of the quantity
    public String getStatus(Boolean id)
    {
        //"true" means add the quantity, "false" means deduct the quantity
        switch (id)
        {
            case true:
                return "Add";
            case false:
                return "Deduct";
        }
        return "unknown";
    }

    //Pagenation
    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        BindAdjustment();
    }

    //When the user clicks the "Create New Requisition" button
    protected void createBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAdjustment.aspx");
    }
    

}