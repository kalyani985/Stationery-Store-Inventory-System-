using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

public partial class StorePage_CreateNewPO : System.Web.UI.Page
{
    decimal total;
    PurchaseOrderController poController = new PurchaseOrderController();
    SupplierController sController = new SupplierController();
    EmployeeController econtroller = new EmployeeController();
    Email email = new Email();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            btn1.Style.Add("display", "none");
            bindNewPO();
            SupplierDropDown();
        }
    }

    public void SupplierDropDown()
    {

        List<Supplier> sList = sController.GetALLSupplier();
        SupplierDropDownList.DataSource = sList.ToList();
        SupplierDropDownList.DataBind();
    }

    private void bindNewPO()
    {
        List<Catelogue> List_shortItems = poController.ListNewPurchaseOrder();
        suppDate.Text = DateTime.Today.Date.ToShortDateString();
        suppDate.ReadOnly = true;
        DataTable dt = new DataTable();
        dt.Columns.Add("ItemNos");
        dt.Columns.Add("Desc");
        dt.Columns.Add("CurrStock");
        dt.Columns.Add("Qty");
        dt.Columns.Add("UnitP");
        dt.Columns.Add("SubTot");
        dt.Columns.Add("NewQty");

        foreach (var item in List_shortItems)
        {
            dt.Rows.Add(item.ItemNumber, item.Description, item.StockBalance.BalanceAmount, item.ReorderQuantity, item.Price, item.Price * item.ReorderQuantity, null);
        }

        newPOGridView.Columns[6].Visible = true;
        Session.Add("GridTable", dt);
        newPOGridView.DataSource = dt;
        newPOGridView.DataBind();

        totalling(dt);
        newPOGridView.Columns[6].Visible = false;

    }//bindNewPO

    protected void totalling(DataTable dt)
    {
        //calculate total price
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            total += Convert.ToDecimal(dt.Rows[i][5].ToString());
        }
        totalLbl.Text = total.ToString();
        // return total;
    }
    protected void createBtn_Click(object sender, EventArgs e)
    {

        PurchaseOrder newPO = new PurchaseOrder();
        Employee emp = (Employee)Session["empId"];

        newPO.Date = Convert.ToDateTime(suppDate.Text);
        newPO.SupplierCode = SupplierDropDownList.SelectedValue;
        newPO.EmpId = emp.EmpId;
        newPO.Status = 0; //not processed

        DataTable dtdetail = new DataTable();
        dtdetail.Columns.Add("ItemName");
        dtdetail.Columns.Add("Qty");

        int poNum = poController.CreatePurchaseOrder(newPO);

        for (int i = 0; i < newPOGridView.Rows.Count; i++)
        {
            int qty = 0;
            PurchaseOrderDetail newPODetails = new PurchaseOrderDetail();
            Label item = (Label)newPOGridView.Rows[i].Cells[0].FindControl("ItemNos");

            newPODetails.ItemCode = newPOGridView.Rows[i].Cells[0].Text;
            newPODetails.PONo = poNum;
            if (((TextBox)newPOGridView.Rows[i].Cells[6].FindControl("NewQty")) == null)
            {
                newPODetails.Quantity = Convert.ToInt32((newPOGridView.Rows[i].Cells[3].Text));
            }
            else
            {
                newPODetails.Quantity = Convert.ToInt32(newPOGridView.Rows[i].Cells[6].Text);
            }
            poController.CreatePurchaseOrderDetails(newPODetails);

            string desc = newPOGridView.Rows[i].Cells[1].Text;
            qty = Convert.ToInt32(newPOGridView.Rows[i].Cells[3].Text);
            dtdetail.Rows.Add(desc, qty);

        }

        string append = "";
        for (int i = 0; i < dtdetail.Rows.Count; i++)
        {
            append += dtdetail.Rows[i][0].ToString() + "," + dtdetail.Rows[i][1].ToString() + "<br/>";
        }

        //Email Supervisor 
        if (Convert.ToDecimal(totalLbl.Text) <= 250)
        {
            Employee emp2 = new Employee();
            List<Employee> eList = new List<Employee>();
            eList = econtroller.GetAllEmployee(emp.DeptCode);
            emp2 = eList.Where(x => x.UserLevelId == 6).Select(x => x).FirstOrDefault();
            List<String> toList = new List<string>();
            List<String> cclist = new List<string>();
            string subject = "";
            string body = "";
            toList.Add(emp2.Email);
            subject = "Approval for new purchase order (Total amount above $100.00)";
            body = "Dear" + emp2.Name + "," + "<br />" + "<br />" + "Below is the purchase order for your approval. " + "<br/>" + "<br />" + append + "<br />"
                + "Thank you. " + "<br />" + "<br />" + "Regard, " + "<br/>" + emp.Name + "(Store Clerk)" + "<br/>" + "Contact No: 8885 2211";
            email.SendEmail(toList, subject, body, cclist);

            ClientScript.RegisterStartupScript(Page.GetType(),
            "Message", "alert('" + "An email notification has been sent to Store Supervisor: " + emp2.Name + "');", true);
        }
        //Email Manager
        else
        {
            Employee emp2 = new Employee();
            List<Employee> eList = new List<Employee>();
            eList = econtroller.GetAllEmployee(emp.DeptCode);
            emp2 = eList.Where(x => x.UserLevelId == 7).Select(x => x).FirstOrDefault();
            List<String> toList = new List<string>();
            List<String> cclist = new List<string>();
            string subject = "";
            string body = "";
            toList.Add(emp2.Email);
            subject = "Approval for new purchase order (Total amount above $250.00)";
            body = "Dear " + emp2.Name + "," + "<br />" + "<br />" + "Below is the purchase order for your approval. " + "<br/>" + "<br />" + append + "<br />"
                + "Thank you. " + "<br />" + "<br />" + "Regard, " + "<br/>" + emp.Name + "(Store Clerk)" + "<br/>" + "Contact No: 8885 2211, EXT 123";
            email.SendEmail(toList, subject, body, cclist);

            ClientScript.RegisterStartupScript(Page.GetType(),
            "Message", "alert('" + "An email notification has been sent to Store Manager: " + emp2.Name + "');", true);
        }
        Response.Redirect("PO.aspx");
    }

    protected void addItem_Click(object sender, EventArgs e)
    {
        mpe1.Show();
        LinkButton detBtn = sender as LinkButton;
        if (detBtn != null)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            //Set label to Item Number
            ItmNos.Text = newPOGridView.Rows[index].Cells[0].Text;

            Session.Add("IndexGridPO", index);
        }
    }

    protected void newPOGridView_SelectedIndexChanged(object sender, EventArgs e)
    {
        string testID = newPOGridView.SelectedRow.Cells[0].Text;

        string url = "/StorePage/PO_PopUp.aspx?ItemNos=" + Server.UrlEncode(testID); //use this  '" + url + "',

        Response.Write("<script language=\"javascript\">var popUp = window.open('" + url + "', 'SportZip', 'height = 300, width = 400, status = no, toolbar = no, menubar = no, location = yes, scrollbars= yes');popUp.focus();</script>");
    }



    protected void btnok_Click(object sender, EventArgs e)
    {
        String newqty = "";
        newqty = setItmQty.Text;
        int index = Convert.ToInt32(Session["IndexGridPO"]);

        DataTable dt = new DataTable();
        dt = (DataTable)Session["GridTable"];

        string currentqty = dt.Rows[index][3].ToString();

        decimal newqty1;
        bool checkfirst = decimal.TryParse(newqty, out newqty1);
        decimal newqty2;
        bool checkSecond = decimal.TryParse(currentqty, out newqty2);
        if (checkfirst == true && checkSecond == true)
        {
            Decimal total = Convert.ToDecimal(newqty) + Convert.ToDecimal(currentqty);
            dt.Rows[index][3] = total.ToString();
            dt.Rows[index][5] = Convert.ToDecimal(dt.Rows[index][3]) * Convert.ToDecimal(dt.Rows[index][4]);
            totalling(dt);
            newPOGridView.DataSource = dt;
            newPOGridView.DataBind();

            //totalLbl.Text = totalling(dt).ToString();
            mpe1.Hide();
        }
        else
        {
            return;
        }



    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        mpe1.Hide();
    }


}