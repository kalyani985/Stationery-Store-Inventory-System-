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

public partial class Department_CreateRequisition : System.Web.UI.Page
{
        CatelogueController catController = new CatelogueController();
        RequisitionController reqController = new RequisitionController();
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
                BindCatType();
                int cattype = Convert.ToInt32(ddlCatType.SelectedValue);
                BindCat(cattype);
            }
            ddlCatType.Enabled = true;
            ddlCat.Enabled = true;
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Fill Data", "alert('Please fill the quantity amount.')", true);
                return;
            }
            Catelogue cat = catController.GetCatelogue(ddlCat.SelectedValue);
            DataTable dt = new DataTable();
            if (Session["MainTable"] == null)
            {
                dt = new DataTable();
                dt.Columns.Add("CategoryName");
                dt.Columns.Add("ItemName");
                dt.Columns.Add("Quantity");
                dt.Columns.Add("UOM");
                dt.Columns.Add("ItemNumber");
                dt.Rows.Add(cat.CatalogueSpecify.CatagoryDesc, cat.Description, textQty.Text, cat.UnitOfMeasure, cat.ItemNumber);
                Session["MainTable"] = dt;
            }
            else
            {
                dt = (DataTable)Session["MainTable"];
                bool check = false;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i][1].ToString() == ddlCat.SelectedItem.Text)
                    {
                        dt.Rows[i][2] = textQty.Text;
                        check = true;
                        break;
                    }
                }
                if (check == false)
                {
                    dt.Rows.Add(cat.CatalogueSpecify.CatagoryDesc, cat.Description, textQty.Text, cat.UnitOfMeasure, cat.ItemNumber);
                }



                Session["MainTable"] = dt;
            }

            //dt.Rows.Add(cat.ItemNumber, cat.Description, textQty.Text, cat.UnitOfMeasure);
            textQty.Text = "";
            //dt.Columns.Remove("ItemNumber");
            gvReq.DataSource = dt;
            gvReq.DataBind();
        }

        protected void lnkedit_Click(object sender, EventArgs e)
        {
            LinkButton lnkedit = sender as LinkButton;
            if (lnkedit != null)
            {
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
                    if (listcat[i].Description.ToString() == gvReq.Rows[index].Cells[1].Text)
                    {
                        ddlCat.SelectedIndex = i;
                    }
                }
                ddlCat.Enabled = false;
                textQty.Text = gvReq.Rows[index].Cells[2].Text;
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
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Unsucessful Transaction", "alert('Fill the requestions!')", true);
                return;
            }
            int reqId = reqController.CreateRequisition(emp.EmpId, DateTime.Today.Date);
            int status = 0;
            if (reqId == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Unsucessful Transaction", "alert('Requisition is not successful!')", true);
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                status += reqController.CreateRequisitionDetails(reqId, dt.Rows[i][4].ToString(), Convert.ToInt32(dt.Rows[i][2].ToString()), emp.DeptCode);
            }
            if (status == dt.Rows.Count)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "Sucessful Transaction", "alert('Requisition is successful!')", true);
                dt = null;
                Session["MainTable"] = null;
                gvReq.DataSource = dt;
                gvReq.DataBind();
            }

            Response.Redirect("Requisition.aspx");
        }
    }
   