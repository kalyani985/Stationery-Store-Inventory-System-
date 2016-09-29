using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections;
using InventoryClass;

public partial class StorePage_CreateAdjustment : System.Web.UI.Page
{
    CatelogueController catController = new CatelogueController();
    AdjustmentController adController = new AdjustmentController();
    List<CatalogueSpecify> cattypeList;
    List<Catelogue> catList;
    protected void Page_Load(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["empId"];
        if (emp == null)
        {
            Session.RemoveAll();
            Response.Redirect("Login.aspx");
        }
        if (!IsPostBack)
        {
            date.Text = DateTime.Today.ToShortDateString();
            string adjustNo = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
            adNo.Text = adjustNo;
            adNo.ReadOnly = true;
            BindCatType();
            int cattype = Convert.ToInt32(ddlCatType.SelectedValue);
            BindCat(cattype);
        }
        adNo.ReadOnly = true;
        ddlCatType.Enabled = true;
        ddlCat.Enabled = true;
        addOrDeduct.Enabled = true;
    }

    protected void ddlCatType_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cattype = Convert.ToInt32(ddlCatType.SelectedValue);
        BindCat(cattype);
    }

    private void BindCatType()
    {
        cattypeList = new List<CatalogueSpecify>();
        cattypeList = catController.GetAllCatelogueType();
        ddlCatType.DataTextField = "CatagoryDesc";
        ddlCatType.DataValueField = "CatagoryId";
        ddlCatType.DataSource = cattypeList;
        ddlCatType.DataBind();
        Session["CatType"] = cattypeList;
    }

    private void BindCat(int cattypeId)
    {
        catList = new List<Catelogue>();
        catList = catController.GetAllCatelogue().Where(x => x.CategoryId == cattypeId).Select(x => x).ToList();
        ddlCat.DataTextField = "Description";
        ddlCat.DataValueField = "ItemNumber";
        ddlCat.DataSource = catList;
        ddlCat.DataBind();
        Session["Cat"] = catList;
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        if (textQty.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Fill Data", "alert('Plz fill the quantity amount')", true);
            return;
        }
        Catelogue cat = catController.GetCatelogue(ddlCat.SelectedValue);
        DataTable dt = new DataTable();
        if (Session["MainTable"] == null)
        {
            dt = new DataTable();
            dt.Columns.Add("CategoryName");
            dt.Columns.Add("ItemNo");
            dt.Columns.Add("Description");
            dt.Columns.Add("StockBalance");
            dt.Columns.Add("Quantity");
            dt.Columns.Add("Reason");
            dt.Columns.Add("Adjustment");
            dt.Rows.Add(cat.CatalogueSpecify.CatagoryDesc, cat.ItemNumber, cat.Description, cat.StockBalance.BalanceAmount, textQty.Text, textReason.Text, addOrDeduct.Text);
            Session["MainTable"] = dt;
        }
        else
        {
            dt = (DataTable)Session["MainTable"];
            bool check = false;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString() == ddlCat.SelectedItem.Text)
                {
                    dt.Rows[i][3] = textQty.Text;
                    check = true;
                    break;
                }
            }
            if (check == false)
            {
                dt.Rows.Add(cat.CatalogueSpecify.CatagoryDesc, cat.ItemNumber, cat.Description, cat.StockBalance.BalanceAmount, textQty.Text, textReason.Text, addOrDeduct.Text);
            }

            Session["MainTable"] = dt;
        }
        textQty.Text = "";
        gvReq.DataSource = dt;
        gvReq.DataBind();
    }

    protected void lnkedit_Click(object sender, EventArgs e)
    {
        LinkButton lnkedit = sender as LinkButton;
        if (lnkedit != null)
        {
            addOrDeduct.Enabled = false;
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            List<CatalogueSpecify> cattypeList = (List<CatalogueSpecify>)Session["CatType"];
            for (int i = 0; i < cattypeList.Count; i++)
            {
                if (cattypeList[i].CatagoryDesc.ToString() == gvReq.Rows[index].Cells[0].Text)
                {
                    ddlCatType.SelectedIndex = i;
                }
            }
            ddlCatType.Enabled = false;
            BindCat(Convert.ToInt32(ddlCatType.SelectedValue.ToString()));
            List<Catelogue> listcat = (List<Catelogue>)Session["Cat"];
            for (int i = 0; i < listcat.Count; i++)
            {
                if (listcat[i].Description.ToString() == gvReq.Rows[index].Cells[2].Text)
                {
                    ddlCat.SelectedIndex = i;
                }
            }
            ddlCat.Enabled = false;
            textQty.Text = gvReq.Rows[index].Cells[4].Text;
        }



    }

    protected void lnkdelete_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt = (DataTable)Session["MainTable"];

        LinkButton lnkdelete = sender as LinkButton;
        if (lnkdelete != null)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][0].ToString() == gvReq.Rows[index].Cells[0].Text)
                {
                    dt.Rows[i].Delete();
                }
            }
            Session["MainTable"] = dt;
            gvReq.DataSource = dt;
            gvReq.DataBind();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Employee emp = (Employee)Session["empId"];
        DataTable dt = new DataTable();
        dt = (DataTable)Session["MainTable"];
        if (dt == null || dt.Rows.Count <= 0)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Unsucessful Transaction", "alert('Fill the adjustment!')", true);
            return;
        }
        string adjustNo = adController.CreateAdjustmentVoucher(adNo.Text, emp.EmpId, DateTime.Today.Date);

        int status = 0;
        if (adNo == null)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Unsucessful Transaction", "alert('Adjustment is not successful!')", true);
            return;
        }
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            StockTran sct = new StockTran();
            sct.ItemNumber = dt.Rows[i][1].ToString();
            sct.SCCode = adNo.Text;
            sct.Date = DateTime.Today.Date;
            sct.Quantity = Convert.ToInt32(dt.Rows[i][4].ToString());
            if (dt.Rows[i][6].ToString() == "Add")
            {
                sct.AddRemove = true;
            }
            else
            {
                sct.AddRemove = false;
            }
            sct.StatusId = 0;
            sct.Reason = dt.Rows[i][5].ToString();
            status += adController.CreateAdjustmentVoucherDetail(sct);
        }
        if (status == dt.Rows.Count)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Sucessful Transaction", "alert('Adjustment is successful!')", true);
            dt = null;
            Session["MainTable"] = null;
            gvReq.DataSource = dt;
            gvReq.DataBind();
        }


        Response.Redirect("Adjustment.aspx");
    }

}