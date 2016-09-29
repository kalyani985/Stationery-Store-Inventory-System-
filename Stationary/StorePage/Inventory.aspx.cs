using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using InventoryClass;

public partial class StorePage_Inventory : System.Web.UI.Page
{
    //Intantialize object
    Catelogue cat = new Catelogue();
    StockBalance sb = new StockBalance();
    StockCardController stockcontroller = new StockCardController();
    List<Catelogue> catlist = new List<Catelogue>();
    CatelogueController catc = new CatelogueController();
    StockCardController sccontoller = new StockCardController();
    CatalogueSpecify catspecify = new CatalogueSpecify();
    List<CatalogueSpecify> catspecifylist = new List<CatalogueSpecify>();

    //When the page loads
    protected void Page_Load(object sender, EventArgs e)
    {
        catlist = catc.GetAllCatelogue();
        InventorySummarylist(catlist);
        //When is not postback
        if (!IsPostBack)
        {
            DropDownType();
        }
    }

    //Get the Inventory list
    public void InventorySummarylist(List<Catelogue> catlist)
    {
        //Create new table
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("BinNumber");
        dt1.Columns.Add("ItemNumber");
        dt1.Columns.Add("Description");
        dt1.Columns.Add("UnitOfMeasure");
        dt1.Columns.Add("ReorderLevel");
        dt1.Columns.Add("ReorderQuantity");
        dt1.Columns.Add("Date");
        dt1.Columns.Add("BalanceAmount");

        foreach (Catelogue c in catlist)
        {
            sb.BalanceAmount = c.StockBalance.BalanceAmount;
            DateTime t = Convert.ToDateTime(c.StockBalance.Date);
            dt1.Rows.Add(c.BinNumber, c.ItemNumber, c.Description, c.UnitOfMeasure, c.ReorderLevel, c.ReorderQuantity, t.ToShortDateString(), sb.BalanceAmount);
        }
        //Put the table into Session
        Session["Inv_Table"] = dt1;
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }

    //Get the datasource of dropdown
    public void DropDownType()
    {
        catspecifylist = catc.GetAllCatelogueType();
        catDrpDwn.DataSource = catspecifylist.ToList();
        catDrpDwn.DataBind();
    }

    //Pagenation
    protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        catlist = catc.GetAllCatelogue();
        InventorySummarylist(catlist);
    }

    //When the user clicks "Search" button
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //Validation of the category and item description search
        if (catDrpDwn.SelectedValue == "ALL" && txtSearch.Text == "")
        {
            catlist = catc.GetAllCatelogue();
            InventorySummarylist(catlist);
        }
        else
        {
            DataTable dt = Session["Inv_Table"] as DataTable;
            DataView dv = new DataView(dt);
            dv.RowFilter = "Description LIKE '%" + txtSearch.Text + "%'";
            GridView1.DataSource = dv;
            GridView1.DataBind();
        }
    }

    //When the index of dropdown changes
    public void catDrpDwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        List<Catelogue> clist = new List<Catelogue>();
        clist = sccontoller.GetInventorySummary(txtSearch.Text, catDrpDwn.SelectedIndex);
        InventorySummarylist(clist);
        if (catDrpDwn.SelectedValue == "ALL")
        {
            catlist = catc.GetAllCatelogue();
            InventorySummarylist(catlist);
        }
    }

    //When the user clicks the button "Stock card"
    protected void GridView1_Redirect(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Redirect")
        {
            GridViewRow gvRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            if (gvRow != null)
            {
                String bin = gvRow.Cells[0].Text;
                string itemno = gvRow.Cells[1].Text;
                String des = gvRow.Cells[2].Text;
                String uom = gvRow.Cells[3].Text;
                Response.Redirect("Stock_Card.aspx?bin=" + bin + "&itemno=" + itemno + "&des=" + des + "&uom=" + uom);
            }
        }
    }
}