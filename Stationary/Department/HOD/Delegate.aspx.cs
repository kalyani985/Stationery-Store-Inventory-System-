using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;
using System.Drawing;
using System.Data.SqlClient; 
//Author: Krishnasamy Kalyani 
public partial class Department_HOD_Delegate : System.Web.UI.Page
{
    //Initialize the object
    DelegateController delecon = new DelegateController();
    EmployeeController empcon = new EmployeeController();
    Employee emp = new Employee();
    DelegateEmp dele = new DelegateEmp();
    Email email = new Email();
    int id;

    //When the page loads
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindEmployee();
            CalendarExtender1.StartDate = DateTime.Now;
            CalendarExtender2.StartDate = DateTime.Now;
            startDateTxt.Text = DateTime.Now.ToShortDateString();
            endDateTxt.Text = DateTime.Now.ToShortDateString();
        }
    }

    //List the employees' name in gridview
    private void BindEmployee()
    {
        List<Employee> listemp = empcon.GetAllEmployee(SendDepCode());
        listemp = listemp.Where(x => x.UserLevelId == 1).ToList();
        representativeName.DataSource = listemp;
        representativeName.DataBind();
    }

    //Get the employee Id of the login employee
    public string SendDepCode()
    {
        emp = (Employee)Session["empId"];
        string ss = emp.DeptCode;
        return ss;
    }

    //When the user clicks "Save" button
    protected void SaveBtn_Click(object sender, EventArgs e)
    {
        //When the user doesnt enter the start date and end date
        if (startDateTxt.Text != "" && endDateTxt.Text != "")
        {
            if (representativeName.SelectedIndex != -1)
            {
                id = Convert.ToInt32(Session["id"]);
                dele.EmpId = id;
                dele.DeptCode = SendDepCode();
                string startdate = Convert.ToDateTime(startDateTxt.Text).ToString("yyyy-MM-dd");
                dele.StartDate = Convert.ToDateTime(startdate);
                string enddate = Convert.ToDateTime(endDateTxt.Text).ToString("yyyy-MM-dd");
                dele.EndDate = Convert.ToDateTime(enddate);

                delecon.NewDelegate(dele);

                Employee emp1 = new Employee();
                emp1 = empcon.getEmployee(id);

                //Send email
                deleStatuslb.Text = "Successfully delegated";
                BindEmployee();
                List<String> toList = new List<string>();
                List<String> cclist = new List<string>();
                string subject = "";
                string body = "";
                toList.Add(emp1.Email);
                subject = "Notification: Temporary Authorised Person.";
                body = "Dear " + emp1.Name + "," + "<br />" + "<br />" + "Please note that you have been delegated as authorised person from the period "
                    + startDateTxt.Text + " to " + endDateTxt.Text + "." + "<br />" + "<br />" + "Regard, " + "<br/>" + emp.Name + "(Department Head)";
                email.SendEmail(toList, subject, body, cclist);
                ClientScript.RegisterStartupScript(Page.GetType(),
                "Message", "alert('" + " An email notification has sent to delegated person:  " + emp1.Name + "');", true);
            }
            else
            {
                deleStatuslb.Text = "No Employee Selected";
            }
        }
        else
        {
            deleStatuslb.Text = "Start and End Date field required.";
        }

    }

    //When the user changes the representative
    protected void representativeName_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in representativeName.Rows)
        {


            if (row.RowIndex == representativeName.SelectedIndex)
            {
                row.BackColor = ColorTranslator.FromHtml("#00FFFF");
                row.ToolTip = string.Empty;
            }
            else
            {
                row.BackColor = ColorTranslator.FromHtml("#FFFFFF");
                row.ToolTip = "Click to select this row.";
            }
        }

        string Empid = ((HiddenField)representativeName.SelectedRow.FindControl("HiddenField1")).Value;
        id = Convert.ToInt32(Empid);

        deleStatuslb.Text = "Selected Employee: " + ((Label)representativeName.SelectedRow.FindControl("Label1")).Text;
        Session["id"] = id;
        Session["assignRep"] = ((Label)representativeName.SelectedRow.FindControl("Label1")).Text;
    }

    protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(representativeName, "Select$" + e.Row.RowIndex);
            e.Row.ToolTip = "Click to select this row.";
        }

    }


}