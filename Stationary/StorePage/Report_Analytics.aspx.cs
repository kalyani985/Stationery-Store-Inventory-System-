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

public partial class StorePage_Report_Analytics : System.Web.UI.Page
{
    bool quantity, cost;
    string first, second, third;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {

            CatelogueController c = new CatelogueController();
            List<CatalogueSpecify> l1 = c.GetAllCatelogueType();
            ddlItem.DataTextField = "CatagoryDesc";
            ddlItem.DataValueField = "CatagoryId";
            ddlItem.DataSource = l1;
            ddlItem.DataBind();
        }
        //ListBox1.Visible = false;
        Label1.Text = "";
        //removeBtn.Visible = false;

    }

    protected void btnGenerate_Click(object sender, EventArgs e)
    {
        if (RadioButtonList1.SelectedValue.Equals("Quantity") && ListBox1.Items.Count != 0)
        {
            quantity = true;
            cost = false;


        }
        else if (RadioButtonList1.SelectedValue.Equals("Cost") && ListBox1.Items.Count != 0)
        {
            quantity = false;
            cost = true;

        }


        //Response.Redirect("~/StorePage/Report_Analytics.aspx");

        if (ListBox1.Items.Count != 0)
        {
            if (ListBox1.Items.Count == 1)
            {
                first = ListBox1.Items[0].Value + "-" + "01";
                second = "";
                third = "";
                System.Diagnostics.Debug.WriteLine(first);
            }
            else if (ListBox1.Items.Count == 2)
            {
                first = ListBox1.Items[0].Value + "-" + "01";
                second = ListBox1.Items[1].Value + "-" + "01";
                third = "";
            }
            else if (ListBox1.Items.Count == 3)
            {
                first = ListBox1.Items[0].Value + "-" + "01";
                second = ListBox1.Items[1].Value + "-" + "01";
                third = ListBox1.Items[2].Value + "-" + "01";
            }
        }
        else
        {
            Label1.Text = "Atleast one value should be selected";
            return;
        }

        DataTable dt = GetSPResult();
        ReportViewer1.Visible = true;
        if (quantity)
        {
            ReportViewer1.LocalReport.ReportPath = "StorePage/StoreReports/Report6.rdlc";
        }
        else
        {
            ReportViewer1.LocalReport.ReportPath = "StorePage/StoreReports/Report6a.rdlc";
        }
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(new Microsoft.Reporting.WebForms.ReportDataSource("DataSet1", dt));


    }

    private DataTable GetSPResult()
    {
        DataTable ResultsTable = new DataTable();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["StationeryInventoryConnectionString"].ConnectionString);

        try
        {
            SqlCommand cmd = new SqlCommand("Rpt_TrendReport", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@DeptCode", ddlDept.SelectedValue);
            cmd.Parameters.AddWithValue("@CatId", ddlItem.SelectedValue);
            cmd.Parameters.AddWithValue("@FirstMonth", first);
            cmd.Parameters.AddWithValue("@SecondMonth", second);
            cmd.Parameters.AddWithValue("@ThirdMonth", third);
            cmd.Parameters.AddWithValue("@ByQty", quantity);
            cmd.Parameters.AddWithValue("@ByPrice", cost);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            adapter.Fill(ResultsTable);
        }
        catch (Exception ex)
        {
            Response.Write(ex.ToString());
        }
        finally
        {
            if (conn != null)
            {
                conn.Close();
            }
        }
        return ResultsTable;
    }

    protected void addBtn_Click(object sender, EventArgs e)
    {

        //String month = ddlMonth.SelectedValue;
        //int year = Convert.ToInt32(DropDownList1.SelectedItem.Text);
        ListBox1.Visible = true;

        if (ListBox1.Items.Count <= 2)
        {
            ListBox1.Items.Add(DropDownList1.SelectedItem.Text + "-" + ddlMonth.SelectedValue);
        }
        else
        {
            Label1.Text = "Max of 3 months can be selected";

        }

    }

    protected void removeBtn_Click(object sender, EventArgs e)
    {
        while (ListBox1.GetSelectedIndices().Length > 0)
        {
            ListBox1.Items.Remove(ListBox1.SelectedItem);
        }

    }
}