using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class StorePage_ProcessDO : System.Web.UI.Page
{
    Employee emp = new Employee();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.BindData();
        }
    }

    public void BindData()
    {

        DeliveryOrderController doc = new DeliveryOrderController();
        List<PurchaseOrder> poList = new List<PurchaseOrder>();

        emp = (Employee)Session["empId"];
        poList = doc.getAllDeliveryOrder(emp.EmpId);
        DataTable dt = new DataTable();
        string statusdesc = "";
        string dono = null;
        dt.Columns.Add("PONo");
        dt.Columns.Add("Date");
        dt.Columns.Add("SupplierName");
        dt.Columns.Add("DoNo");
        dt.Columns.Add("Status");
        for (int i = 0; i < poList.Count; i++)
        {
            if (poList[i].Status == 9)
            {
                statusdesc = "Complete";

            }
            else
            {
                statusdesc = "Incomplete";
            }
            dono = poList[i].DeliveryOrders.Select(x => x.DOCode).FirstOrDefault();
            dt.Rows.Add(poList[i].PONo, poList[i].Date.ToShortDateString(), poList[i].Supplier.SupplierName, dono, statusdesc);
        }
        dt.AcceptChanges();
        GridView1.DataSource = dt;
        GridView1.DataBind();
        btnAssign(poList);
    }

    private void btnAssign(List<PurchaseOrder> poList)
    {
        int CurrentRow = 0;
        foreach (GridViewRow gvr in GridView1.Rows)
        {
            LinkButton btnEdit = (LinkButton)gvr.FindControl("lnkEdit");
            if (poList[CurrentRow].Status == 9)
            {
                btnEdit.Enabled = false;
            }
            else
            {
                btnEdit.Enabled = true;
            }
            CurrentRow++;
        }
    }

    protected void Edit(object sender, EventArgs e)
    {
        using (GridViewRow row = (GridViewRow)((LinkButton)sender).Parent.Parent)
        {
            PONumber.ReadOnly = true;
            PONumber.Text = row.Cells[0].Text;
            txtDate.Text = row.Cells[1].Text;

            if (row.Cells[3].Text != "&nbsp;")
            {
                txtDoNumber.Text = row.Cells[3].Text;
            }

            popup.Show();
        }
    }

    protected void Save(object sender, EventArgs e)
    {
        DeliveryOrderController doc = new DeliveryOrderController();
        doc.CreateDeliveryOrder(txtDoNumber.Text, Convert.ToDateTime(txtDate.Text), Convert.ToInt32(PONumber.Text));
        BindData();
    }

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {

    }

    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton likBtn = (LinkButton)e.Row.FindControl("lnkEdit");
        string Status = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "Status"));
        System.Diagnostics.Debug.Write(Status);
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (Status == "Complete")
            {
                likBtn.Enabled = false;
            }
            else
            {
                likBtn.Enabled = true;
            }
        }
    }
}