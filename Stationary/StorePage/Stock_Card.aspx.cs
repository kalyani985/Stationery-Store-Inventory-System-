using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

public partial class StorePage_Stock_Card : System.Web.UI.Page
{

    StockCardTransaction sct = new StockCardTransaction();
    StockCard sc = new StockCard();
    List<StockCard> sctlist = new List<StockCard>();
    StockCardController stockcc = new StockCardController();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            bin.Text = Request.QueryString["bin"];
            itemCode.Text = Request.QueryString["itemno"];
            itemDesp.Text = Request.QueryString["des"];
            uom.Text = Request.QueryString["uom"];//bin&itemno&des&uom
            System.Diagnostics.Debug.Write("ITEM CODE:\t", itemCode.Text);
            StockcardList();
        }
    }
    public void StockcardList()
    {
        string itemno = Request.QueryString["itemno"];

        sctlist = stockcc.GetStockCardTransaction(itemno);
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("Date");
        dt1.Columns.Add("Description");
        dt1.Columns.Add("OutstandingQuantity");
        dt1.Columns.Add("StockBalance");
        for (int i = 0; i < sctlist.Count; i++)
        {
            sc = sctlist[i];
            dt1.Rows.Add(sc.StockDate, sc.Desc, sc.Qty, sc.Historybalance);

        }
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }
    protected void backBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("Inventory.aspx");
    }
}