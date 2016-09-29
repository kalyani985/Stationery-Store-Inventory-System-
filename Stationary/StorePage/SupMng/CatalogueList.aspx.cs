using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

public partial class StorePage_SupMng_CatalogueList : System.Web.UI.Page
{
    //Intantialize object
    CatelogueController cc = new CatelogueController();
    CatelogueController catc = new CatelogueController();
    List<Catelogue> clist = new List<Catelogue>();
    List<CatalogueSpecify> catspecifylist = new List<CatalogueSpecify>();
    DataTable table = new DataTable();

    //When the page loads
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCatalogue();
            DropDownType();
        }
    }

    //Show the list of the catelogue
    public void BindCatalogue()
    {
        clist = cc.GetAllCatelogue();

        table.Columns.Add("ItemNumber");
        table.Columns.Add("Category");
        table.Columns.Add("Description");
        table.Columns.Add("SupplierName");
        table.Columns.Add("ReorderLevel");
        table.Columns.Add("ReorderQuantity");
        table.Columns.Add("UnitOfMeasure");
        table.Columns.Add("UnitPrice");
        foreach (Catelogue item in clist)
        {
            table.Rows.Add(item.ItemNumber, item.CatalogueSpecify.CatagoryDesc, item.Supplier.SupplierName, item.ReorderLevel, item.ReorderQuantity, item.ReorderQuantity, item.UnitOfMeasure, item.Price);
        }
        Session["Pri_Table"] = table;
        CatalogueGridView.DataSource = table;
        CatalogueGridView.DataBind();
    }
    protected void CatalogueGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        CatalogueGridView.PageIndex = e.NewPageIndex;
        BindCatalogue();
    }

    //Bind the data of drop down list
    public void DropDownType()
    {
        catspecifylist = catc.GetAllCatelogueType();
        catDrpDwn.DataSource = catspecifylist.ToList();
        catDrpDwn.DataBind();
    }

    //When the index of the dropdown list changes
    public void catDrpDwn_SelectedIndexChanged(object sender, EventArgs e)
    {
        DataTable dt = Session["Pri_Table"] as DataTable;

        DataView dv = new DataView(dt);
        string search = catDrpDwn.Text.Trim();
        dv.RowFilter = "Category LIKE '%" + search + "%'";
        CatalogueGridView.DataSource = dv;
        CatalogueGridView.DataBind();

        if (catDrpDwn.SelectedValue == "ALL")
        {
            BindCatalogue();
        }
    }

    //When clicks the search button
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        if (catDrpDwn.SelectedValue == "ALL" && txtSearch.Text == "")
        {
            BindCatalogue();
        }
        else
        {
            DataTable dt = Session["Pri_Table"] as DataTable;
            DataView dv = new DataView(dt);
            dv.RowFilter = "Description LIKE '%" + txtSearch.Text + "%'";
            CatalogueGridView.DataSource = dv;
            CatalogueGridView.DataBind();
        }

    }
}