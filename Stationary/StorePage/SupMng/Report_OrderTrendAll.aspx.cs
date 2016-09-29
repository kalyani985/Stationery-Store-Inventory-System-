using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class StorePage_SupMng_Report_OrderTrendAll : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/StorePage/SupMng/Report_OrderTrendAll.aspx?Month=" + ddlDept.SelectedValue);
    }
}