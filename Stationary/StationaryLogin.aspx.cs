using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;


public partial class Account_StationaryLogin : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {              
            // Enable this once you have account confirmation enabled for password reset functionality
            //ForgotPasswordHyperLink.NavigateUrl = "Forgot
            //var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
        }

        protected void LogIn(object sender, EventArgs e)
        {
            if(empidTxt.Text.Length>5)
            {
                RegularExpressionValidator.Visible = true;
            }
            LoginController loginCtr = new LoginController();
            Employee emp = new Employee();


            int empid = Convert.ToInt32(empidTxt.Text);
            string pw = pwTxt.Text;
            emp = loginCtr.SecurityCheck(empid, pw);
            Session["empId"] = emp;
 
            if (emp!= null && Session["empId"]!=null)
            {
                //employee
                if (emp.UserLevelId == 1)
                {
                    Response.Redirect("Department/Requisition.aspx");
                }
                //Representative
                else if (emp.UserLevelId == 2)
                {
                    Response.Redirect("Department/Requisition.aspx");
                }
                //Delegate
                else if (emp.UserLevelId == 3)
                {
                    Response.Redirect("Department/HOD/Approval.aspx");
                }
                //HOD
                else if (emp.UserLevelId == 4)
                {
                    Response.Redirect("Department/HOD/Approval.aspx");
                }
                //Clerk
                else if (emp.UserLevelId == 5)
                {
                    Response.Redirect("StorePage/Inventory.aspx");
                }
                //Supervisor
                else if (emp.UserLevelId == 6)
                {
                    Response.Redirect("StorePage/SupMng/PoApproval.aspx");
                }
                //Manager
                else if (emp.UserLevelId == 7)
                {
                    Response.Redirect("StorePage/SupMng/PoApproval.aspx");
                }
            }

            else
            {
                FailureText.Text = "Invalid employee ID or password.";
                ErrorMessage.Visible = true;
            }            
        }
    }
  
