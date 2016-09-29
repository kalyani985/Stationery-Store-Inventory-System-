using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using InventoryClass;
using System.Drawing;
using System.Web.UI.WebControls;

//Author: Krishnasamy Kalyani 
//Modified by: Zhang Sha
public partial class Department_HOD_Collection_deptRept : System.Web.UI.Page
{
        //Initialize the object
        Employee emp1 = new Employee();
        EmployeeController empcontroller = new EmployeeController();
        List<Employee> emplist = new List<Employee>();
        Representative repre = new Representative();
        CollectionPointController cpc = new CollectionPointController();
        Email email = new Email();

        string DeptCode1;
        int id, collectionid;

        //When the page loads
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                emp1 = (Employee)Session["empId"];
                getAllEmployee();
                statuslb.Text = "";
                collectPoint.SelectedValue = GetSelectedValue(collectTxt.Text);

                foreach (GridViewRow row in repreName.Rows)
                {
                    string name = ((Label)(row.Cells[0].FindControl("allempName"))).Text;
                    if (name == currentTxt.Text)
                    {
                        row.BackColor = ColorTranslator.FromHtml("#00FFFF");
                        row.ToolTip = string.Empty;
                    } 
                }
            }
        }

        //Get all the employee
        protected void getAllEmployee()
        {
            emp1 = (Employee)Session["empId"];
            DeptCode1= emp1.DeptCode;
            repre = cpc.GetCurrentRepresentativeCollection(DeptCode1);
            if (repre.CollectionId == null && repre.EmpId == null)
            {
                collectTxt.Text = "No current Collection Point";
                currentTxt.Text = "No current Representative";
            }
            else if (repre.CollectionId == null)
            {
                collectTxt.Text = "No current Collection Point";
                currentTxt.Text = repre.Employee.Name;
            }
            else if ( repre.EmpId == null)
            {
                collectTxt.Text = repre.CollectionPoint.Description;
                currentTxt.Text = "No current Representative";
            }
            else
            {
                collectTxt.Text = repre.CollectionPoint.Description;
                currentTxt.Text = repre.Employee.Name;
            }
            
            /////////////////////NEED TO REPAIR////////////////////////////
            emplist = null;
            emplist = empcontroller.GetAllEmployee(DeptCode1);
            Employee currentEmployee = emplist.Where(x => x.UserLevelId == 2).Select(x => x).FirstOrDefault();
            //Session["id"] = currentEmployee.EmpId;
            emplist = emplist.Where(emp => emp.UserLevelId == 1 || emp.UserLevelId == 2||emp.UserLevelId == 3).ToList();

            repreName.DataSource = null;
            repreName.DataSource = emplist.ToList();
            repreName.DataBind();
        }

        //Transfer the name of the collection point to value
        protected string GetSelectedValue(string collectTxt)
        {
            switch (collectTxt)
            {
                case "Admin Building (9:30am)":
                    return "1";
                case "Management School (11:00am)":
                    return "2";
                case "Medical School (9:30am)":
                    return "3";
                case "Engineering School (11:00am)":
                    return "4";
                case "Science School (9:30am)":
                    return "5";
                case "University Hall (11:00am)":
                    return "6";
            }
            return null;
        }
        
        //When the user clicks "Select"
        protected void OnRowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(repreName, "Select$" + e.Row.RowIndex);
                e.Row.ToolTip = "Click to select this row.";
            }
        }       
        
        //When the uer clicks "Save" button
        protected void SaveBtn_Click(object sender, EventArgs e)
        {
            //When the user doesnt change either the collection point or the representative
            if (repreName.SelectedRow == null && collectPoint.SelectedItem.Text == collectTxt.Text)
            {
                statuslb.Text = "No Collection Point & Representative Updated";
            }
            //When the user doesnt change the representative but change the collection point
            else if (repreName.SelectedRow == null && collectPoint.SelectedItem.Text != collectTxt.Text)
            {
                    emp1 = (Employee)Session["empId"];
                    DeptCode1 = emp1.DeptCode;
                    collectionid = Convert.ToInt32(collectPoint.SelectedValue);
                    id = Convert.ToInt32(Session["id"]);
                    Session["collectPoint"] = collectPoint.SelectedItem.Text;
                    statuslb.Text = "New Collection Point Updated";
                    int status = cpc.ChangeRepresentativeCollection(id, collectionid, DeptCode1);

                    getAllEmployee();
                    foreach (GridViewRow row in repreName.Rows)
                    {
                        string name = ((Label)(row.Cells[0].FindControl("allempName"))).Text;
                        if (name == currentTxt.Text)
                        {
                            row.BackColor = ColorTranslator.FromHtml("#00FFFF");
                            row.ToolTip = string.Empty;
                        }
                    }
                    Session.Remove("assignRep");
                    Session.Remove("collectPoint");

                    Employee emp2 = new Employee();
                    emp2 = empcontroller.getEmployee(id);

                    //send email
                    List<String> toList = new List<string>();
                    List<String> cclist = new List<string>();
                    string subject = "";
                    string body = "";
                    toList.Add(emp2.Email);
                    subject = "Notification: New Collection Point Updated";
                    body = "Dear " + emp2.Name + "," + "<br/>" + "<br />"
                        + "Please note that the collection point has changed. " + "<br />" + "<br />" + "The new collection point is " + collectPoint.SelectedItem.Text + "." +
                        "<br />" + "<br />" + "Regard, " + "<br/>" + emp1.Name + "(Department Head)";
                    email.SendEmail(toList, subject, body, cclist);

                    ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "alert('" + "Collection point has been changed successfully. An email notification has sent to new department representative: " + emp2.Name + "');", true);
            }
            //When the user changes the representative but doesnt change the collection point
            else if (repreName.SelectedRow != null && collectPoint.SelectedItem.Text == collectTxt.Text)
            {
                string reName = ((Label)repreName.SelectedRow.Cells[0].FindControl("allempName")).Text;
                    emp1 = (Employee)Session["empId"];
                    DeptCode1 = emp1.DeptCode;
                    collectionid = Convert.ToInt32(GetSelectedValue(collectTxt.Text));
                    EmployeeController ec = new EmployeeController();
                    List<Employee> eList = ec.GetAllEmployee(DeptCode1);
                    Employee empRep = eList.Where(x=>x.Name ==reName).Select(x=>x).FirstOrDefault();
                    statuslb.Text = "New Representative Updated";
                    int status = cpc.ChangeRepresentativeCollection(empRep.EmpId, collectionid, DeptCode1);

                    getAllEmployee();

                    //send email
                    List<String> toList = new List<string>();
                    List<String> cclist = new List<string>();
                    string subject = "";
                    string body = "";
                    toList.Add(empRep.Email);
                    subject = "Notification: Assign as Department Representative";
                    body = "Dear " + empRep.Name + "," + "<br />" + "<br />" + "You have been assigned as our Department Representative." + "<br/>" + "<br />"
                        + "The collection point is " + collectPoint.SelectedItem.Text + "." +
                        "<br />" + "<br />" + "Regard, " + "<br/>" + emp1.Name + "(Department Head)";
                    email.SendEmail(toList, subject, body, cclist);

                    ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "alert('" + "New Representative has been changed successfully. An email notification has sent to new department representative: " + empRep.Name + "');", true);

                    repreName.SelectedRow.BackColor = ColorTranslator.FromHtml("#00FFFF");
                }
                //When the user changes both the representative and the collection point
            else if (repreName.SelectedRow != null && collectPoint.SelectedItem.Text != collectTxt.Text)
                {
                    string reName = ((Label)repreName.SelectedRow.Cells[0].FindControl("allempName")).Text;
                    emp1 = (Employee)Session["empId"];
                    DeptCode1 = emp1.DeptCode;
                    collectionid = Convert.ToInt32(collectPoint.SelectedValue);
                    EmployeeController ec = new EmployeeController();
                    List<Employee> eList = ec.GetAllEmployee(DeptCode1);
                    Employee empRep = eList.Where(x => x.Name == reName).Select(x => x).FirstOrDefault();
                    statuslb.Text = "New Collection Point & Representative Updated";
                    int status = cpc.ChangeRepresentativeCollection(empRep.EmpId, collectionid, DeptCode1);

                    getAllEmployee();
                    
                    //send email
                    List<String> toList = new List<string>();
                    List<String> cclist = new List<string>();
                    string subject = "";
                    string body = "";
                    toList.Add(empRep.Email);
                    subject = "Notification: Assign as Department Representative";
                    body = "Dear " + empRep.Name + "," + "<br />" + "<br />" + "You have been assigned as our Department Representative." + "<br/>" + "<br />"
                        + "Please note that the collection point has changed. " + "<br />" + "<br />" + "The new collection point is " + collectPoint.SelectedItem.Text + "." +
                        "<br />" + "<br />" + "Regard, " + "<br/>" + emp1.Name + "(Department Head)";
                    email.SendEmail(toList, subject, body, cclist);

                    ClientScript.RegisterStartupScript(Page.GetType(),
                        "Message", "alert('" + "Representative and Collection point has been changed successfully. An email notification has sent to new department representative: " + empRep.Name + "');", true);

                    repreName.SelectedRow.BackColor = ColorTranslator.FromHtml("#00FFFF");
                }
            }
        //When the index of the gridview changes
        protected void repreName_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (GridViewRow row in repreName.Rows)
            {
                if (row.RowIndex == repreName.SelectedIndex)
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

            string Empid = ((HiddenField)repreName.SelectedRow.FindControl("HiddenField1")).Value;
                id = Convert.ToInt32(Empid);

                statuslb.Text = "Selected Employee: " + ((Label)repreName.SelectedRow.FindControl("allempName")).Text;
                Session["id"] = id;
                Session["assignRep"] = ((Label)repreName.SelectedRow.FindControl("allempName")).Text;
        }
    }
