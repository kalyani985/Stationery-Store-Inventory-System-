using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using InventoryClass;

public partial class StorePage_Outstanding_Requisition : System.Web.UI.Page
{
    //Intantialize object
    Catelogue cat = new Catelogue();
    OutStandingRequisition oureq = new OutStandingRequisition();
    RequisitionTransactionDetail rtd = new RequisitionTransactionDetail();
    StockBalance sb = new StockBalance();
    List<OutStandingRequisition> oureqlist = new List<OutStandingRequisition>();
    OutStandingController ourepcontroller = new OutStandingController();
    RequisitionController rc = new RequisitionController();
    Email email = new Email();
    EmployeeController econtroller = new EmployeeController();
    Employee emp1 = new Employee();

    //When the page loads
    protected void Page_Load(object sender, EventArgs e)
    {
        //When is not postback
        if (!IsPostBack)
        {
            OutstandingList();
        }
    }

    //Get the list of outstanding
    protected void OutstandingList()
    {
        oureqlist = ourepcontroller.GetAllOutstandingRequisition();
        DataTable dt1 = new DataTable();
        dt1.Columns.Add("DeptName");
        dt1.Columns.Add("ItemCode");
        dt1.Columns.Add("Description");
        dt1.Columns.Add("StockBalance");
        dt1.Columns.Add("OutstandingQuantity");

        for (int i = 0; i < oureqlist.Count; i++)
        {
            oureq = oureqlist[i];
            cat.Description = oureq.Catelogue.Description;
            sb.BalanceAmount = oureq.Catelogue.StockBalance.BalanceAmount;
            dt1.Rows.Add(oureq.DeptCode, oureq.ItemNumber, cat.Description, sb.BalanceAmount, oureq.Quantity);
        }
        GridView1.DataSource = dt1;
        GridView1.DataBind();
    }

    //Validate the "Generate" button
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (GridView1.Rows.Count != 0)
        {
            LinkButton generate = (LinkButton)e.Row.Cells[5].FindControl("generateBtn");
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                foreach (GridViewRow row in GridView1.Rows)
                {
                    int i = Convert.ToInt32(e.Row.Cells[3].Text);
                    int j = Convert.ToInt32(e.Row.Cells[4].Text);

                    //outstanding quantity is more than stock quantity
                    //Requisition can be generated
                    if (i > j)
                    {
                        generate.Enabled = false;
                    }
                    else
                    {
                        generate.Visible = true;
                    }
                }
            }
        }
    }

    //When the user clicks "Generate" button
    protected void GridView1_RowCommand1(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Generate")
        {
            GridViewRow gRow = (GridViewRow)((LinkButton)e.CommandSource).NamingContainer;
            LinkButton generate = (LinkButton)gRow.Cells[5].FindControl("generateBtn");
            if (gRow != null)
            {
                emp1 = (Employee)Session["empId"];
                string deptcode = gRow.Cells[0].Text;
                string itemcode = gRow.Cells[1].Text;
                int quantity = Convert.ToInt32(gRow.Cells[3].Text);
                ourepcontroller.ProcessOutstandingRequisition(itemcode, deptcode);

                //Email
                Employee emp2 = new Employee();
                List<Employee> eList = new List<Employee>();
                eList = econtroller.GetAllEmployee(deptcode);
                emp2 = eList.Where(x => x.UserLevelId == 2).Select(x => x).FirstOrDefault();
                List<String> toList = new List<string>();
                List<String> cclist = new List<string>();
                string subject = "";
                string body = "";
                toList.Add(emp2.Email);
                subject = "Notification: Collection of outstanding item";
                body = "Dear " + emp2.Name + "," + "<br />" + "<br />" + "Your item is already in stock. Please contact us before you come to collect." + "<br/>" + "<br />"
                    + "Thank you. " + "<br />" + "<br />" + "Regard, " + "<br/>" + emp1.Name + "(Store Clerk)" + "<br/>" + "Contact No: 8885 2211";
                email.SendEmail(toList, subject, body, cclist);

                ClientScript.RegisterStartupScript(Page.GetType(),
                "Message", "alert('" + "An email notification has sent to department representative: " + emp2.Name + "');", true);
            }
            OutstandingList();
        }
    }
}