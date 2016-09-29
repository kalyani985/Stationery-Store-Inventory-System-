using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;

public partial class StorePage_Report_PO : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StorePage/Report_PO.aspx?PONO=" + ddlPO.SelectedValue);
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        PurchaseOrderController p = new PurchaseOrderController();
        List<int> a = p.GetPurchaseOrderByMonth(Convert.ToInt32(ddlMonth.SelectedValue));

        //ddlPO.DataTextField = "PO Num";
        ddlPO.DataSource = a;
        ddlPO.DataBind();
    }
}