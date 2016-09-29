using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

public partial class StorePage_Disbursement : System.Web.UI.Page
{
    //Intantialize object
    Representative Rep = new Representative();
    DisbursementForm dForm = new DisbursementForm();
    CollectionPointController colPtCtrl = new CollectionPointController();
    DisbursementController dbController = new DisbursementController();
    DisbursementController disCont = new DisbursementController();
    List<Process> processList = new List<Process>();

    //When the page loads
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //When is not postback
            btn1.Style.Add("display", "none");
            btn2.Style.Add("display", "none");
            BindDisbursement();
        }
    }

    //Show the Disbursement List
    private void BindDisbursement()
    {
        //Disbursement entity controller

        //Disbursement and list of 
        DisbursementForm dis = new DisbursementForm();
        List<DisbursementForm> listDs = new List<DisbursementForm>();

        //getAllDisbursement with null deptCode
        string deptCode = "";
        listDs = disCont.DisbursementList(deptCode);

        //Create new table
        DataTable table = new DataTable();
        table.Columns.Add("DFNos");
        table.Columns.Add("deptC");
        table.Columns.Add("deptName");
        table.Columns.Add("Date");
        table.Columns.Add("Status");

        // Add rows.
        foreach (var array in listDs)
        {
            table.Rows.Add(array.DFNo, array.DeptCode, array.Department.Name, array.Date.ToShortDateString(), getStatus(array.StatusId));
        }
        //Put the table into Session
        Session["Pri_Table"] = table;
        DisburseGridView.DataSource = table;
        DisburseGridView.DataBind();
    }

    //When the user clicks "View" Button
    protected void detBtn_Click(object sender, EventArgs e)
    {
        //The pop up will show
        mpe1.Show();
        LinkButton detBtn = sender as LinkButton;
        if (detBtn != null)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            string depCode = DisburseGridView.Rows[index].Cells[1].Text;//Get department name
            Rep = colPtCtrl.GetCurrentRepresentativeCollection(DisburseGridView.Rows[index].Cells[1].Text);//Get current representative object
            string colPoint = getCollectionPt(Rep.CollectionPoint.CollectionId);//Get current collection point
            string repreName = Rep.Employee.Name; //Get representative name

            //The label in pop up 
            date.Text = DisburseGridView.Rows[index].Cells[3].Text;
            disList.Text = DisburseGridView.Rows[index].Cells[0].Text;
            repName.Text = repreName;
            collPoint.Text = colPoint;

            int disNo = Convert.ToInt16(disList.Text);

            //Get the process list
            processList = dbController.ProcessListByDisID(depCode, disNo);

            DataTable table = new DataTable();
            table.Columns.Add("ItemNo");
            table.Columns.Add("DFNum");
            table.Columns.Add("Description");
            table.Columns.Add("Qty");

            foreach (var array in processList)
            {
                table.Rows.Add(array.ItemNumber, array.DFNo, array.Catelogue.Description, array.Quantity);
            }
            disDetail.DataSource = table;
            disDetail.DataBind();
        }
    }

    //When the index of the dropdown list changes
    public void deptDropDown_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = Session["Pri_Table"] as DataTable;

        DataView dv = new DataView(dt);
        dv.RowFilter = "deptC LIKE '%" + deptDropDown.SelectedValue + "%'";
        DisburseGridView.DataSource = dv;
        DisburseGridView.DataBind();
    }

    //Pagenation
    protected void DisburseGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        DisburseGridView.PageIndex = e.NewPageIndex;
        BindDisbursement();
    }

    //Transfer the status from number into words
    protected String getStatus(int statusId)
    {
        switch (statusId)
        {
            case 3:
                return "Processed";
            case 4:
                return "Not Processed";
            default:
                return "Unknown";
        }
    }

    //Transfer the collection point id into collection point names
    private string getCollectionPt(int p)
    {
        switch (p)
        {
            case 1:
                return "Admin Building (9:30am)";
            case 2:
                return "Management School (11:00am)";
            case 3:
                return "Medical School (9:30am)";
            case 4:
                return "Engineering School (11:00am)";
            case 5:
                return "Science School (9:30am)";
            case 6:
                return "University Hall (11:00am)";
            default:
                return "Unknown";
        }
    }
}