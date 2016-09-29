using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;

public partial class StorePage_Report_Inventory : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            CatelogueController c = new CatelogueController();
            List<CatalogueSpecify> l1 = c.GetAllCatelogueType();


            DropDownList1.DataTextField = "CatagoryDesc";
            DropDownList1.DataSource = l1;
            DropDownList1.DataBind();
        }
    }

    public void btnGenerate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StorePage/Report_Inventory.aspx?Month=" + ddlMonth.SelectedValue + "&Item=" + DropDownList1.SelectedValue);
    }
}