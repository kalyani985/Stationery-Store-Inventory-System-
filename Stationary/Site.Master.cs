using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;


    public partial class SiteMaster : MasterPage
    {
        //private const string AntiXsrfTokenKey = "__AntiXsrfToken";
        //private const string AntiXsrfUserNameKey = "__AntiXsrfUserName";
        //private string _antiXsrfTokenValue;

        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    // The code below helps to protect against XSRF attacks
        //    var requestCookie = Request.Cookies[AntiXsrfTokenKey];
        //    Guid requestCookieGuidValue;
        //    if (requestCookie != null && Guid.TryParse(requestCookie.Value, out requestCookieGuidValue))
        //    {
        //        // Use the Anti-XSRF token from the cookie
        //        _antiXsrfTokenValue = requestCookie.Value;
        //        Page.ViewStateUserKey = _antiXsrfTokenValue;
        //    }
        //    else
        //    {
        //        // Generate a new Anti-XSRF token and save to the cookie
        //        _antiXsrfTokenValue = Guid.NewGuid().ToString("N");
        //        Page.ViewStateUserKey = _antiXsrfTokenValue;

        //        var responseCookie = new HttpCookie(AntiXsrfTokenKey)
        //        {
        //            HttpOnly = true,
        //            Value = _antiXsrfTokenValue
        //        };
        //        if (FormsAuthentication.RequireSSL && Request.IsSecureConnection)
        //        {
        //            responseCookie.Secure = true;
        //        }
        //        Response.Cookies.Set(responseCookie);
        //    }

        //    Page.PreLoad += master_Page_PreLoad;
        //}

        //protected void master_Page_PreLoad(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        // Set Anti-XSRF token
        //        ViewState[AntiXsrfTokenKey] = Page.ViewStateUserKey;
        //        ViewState[AntiXsrfUserNameKey] = Context.User.Identity.Name ?? String.Empty;
        //    }
        //    else
        //    {
        //        // Validate the Anti-XSRF token
        //        if ((string)ViewState[AntiXsrfTokenKey] != _antiXsrfTokenValue
        //            || (string)ViewState[AntiXsrfUserNameKey] != (Context.User.Identity.Name ?? String.Empty))
        //        {
        //            throw new InvalidOperationException("Validation of Anti-XSRF token failed.");
        //        }
        //    }
        //}

        protected void Page_Load(object sender, EventArgs e)
        {
            Employee emp = (Employee)Session["empId"];
            if (Session["empId"] != null)
            {
                Login.Visible = true;
                lblWelcome.Text = "Welcome " + emp.Name.ToString() + "!";
                Login.Text = "Log Out";

                
            }
            //employee menu
            if (Session["empId"]!=null && emp.UserLevelId == 1)
            {
                req.Visible = true;
            }
            //department representative menu
            else if (Session["empId"]!=null && emp.UserLevelId == 2)
            {
                req.Visible = true;
                coll.Visible = true;
                view.Visible = true;
            }
            // department delegated emp
            else if (Session["empId"] != null && emp.UserLevelId == 3)
            {
                app_dept.Visible = true;
                coll_dept.Visible = true;
            }
            //HOD
            else if (Session["empId"] != null && emp.UserLevelId == 4)
            {
                app_dept.Visible = true;
                coll_dept.Visible = true;
                del.Visible = true;
            }
            //store clerk
            else if (Session["empId"] != null && emp.UserLevelId == 5)
            {
                inv.Visible = true;
                pur.Visible = true;
                rep_clerk.Visible = true;
            }
            //store supervisor
            else if (Session["empId"] != null && emp.UserLevelId == 6)
            {
                app_store.Visible = true;
                rep_sup.Visible = true;
            }
            //store manager
            else if (Session["empId"] != null && emp.UserLevelId == 7)
            {
                app_store.Visible = true;
                cat.Visible = true;
            }
        }

        protected void Login_Click(object sender, EventArgs e)
        {
            if (Login.Text == "Log Out")
            {
                Session.RemoveAll();
                Response.Redirect("~/StationaryLogin.aspx");
            }
        }
    }
