using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

public partial class StorePage_PO : System.Web.UI.Page
{
    //Intantialize object
    PurchaseOrderController poController = new PurchaseOrderController();
    Employee emp = new Employee();
    Email email = new Email();
    decimal total;

    protected void Page_Load(object sender, EventArgs e)
    {
        //When the page loads
        if (!IsPostBack)
        {
            btn1.Style.Add("display", "none");
            btn2.Style.Add("display", "none");
            GetAllPO();
        }
    }

    //When the user clicks "Create New" button
    protected void createBtn_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateNewPO.aspx");
    }

    //When the user clicks "Delete" button
    protected void deleteBtn(object sender, EventArgs e)
    {
        int poNo = 0;
        int statusId = 8;
        LinkButton btnlink = sender as LinkButton;
        if (btnlink != null)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            poNo = Convert.ToInt32(dgvShowAllPO.Rows[index].Cells[0].Text);
            emp = (Employee)Session["empId"];
            int empId = emp.EmpId;
            poController.RemovePurchaseOrder(statusId, poNo, empId);
            GetAllPO();
        }

    }

    //When the user clicks "Send" button
    protected void sendBtn(object sender, EventArgs e)
    {
        //Email Supplier

        LinkButton lnksend = sender as LinkButton;
        if (lnksend != null)
        {
            List<PurchaseOrderDetail> podList = new List<PurchaseOrderDetail>();
            DataTable dtdetail = new DataTable();
            dtdetail.Columns.Add("ItemName");
            dtdetail.Columns.Add("Qty");

            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            int poNo = Convert.ToInt32(dgvShowAllPO.Rows[index].Cells[0].Text);
            emp = (Employee)Session["empId"];
            int status = poController.sendPurchaseOrder(poNo, emp.EmpId);

            if (status == 1)
            {
                podList = poController.GetPurchaseOrderbyID(poNo);

                for (int i = 0; i < podList.Count; i++)
                {
                    dtdetail.Rows.Add(podList[i].Catelogue.Description, podList[i].Quantity);
                }



                //for (int i = 0; i < dgvShowAllPO.Rows.Count; i++)
                //{
                //      int poNo = Convert.ToInt32(dgvShowAllPO.Rows[i].Cells[0].Text);
                //      podList = poController.GetPurchaseOrderbyID(poNo);
                //      dtdetail.Rows.Add(podList[i].Catelogue.Description, podList[i].Quantity);
                //}

                string append = "";
                for (int i = 0; i < dtdetail.Rows.Count; i++)
                {
                    append += dtdetail.Rows[i][0].ToString() + "," + dtdetail.Rows[i][1].ToString() + "<br/>";
                }

                List<String> toList1 = new List<string>();
                List<String> cclist1 = new List<string>();
                string subject1 = "";
                string body1 = "";
                toList1.Add("supplier.logicu@gmail.com");
                subject1 = "New Purchase Order";
                body1 = "Dear Supplier," + "<br />" + "<br />" + "Below is our purchase order for your kind attention." + "<br/>" + "<br />" + append + "<br />"
                    + "Thank you. " + "<br />" + "<br />" + "Regard, " + "<br/>" + emp.Name + "(Store Clerk)" + "<br/>" + "Contact No: 8885 2211";
                email.SendEmail(toList1, subject1, body1, cclist1);

                ClientScript.RegisterStartupScript(Page.GetType(),
                "Message", "alert('" + "A purchase order request has been sent to supplier. " + "');", true);
            }


        }
    }

    //When the user clicks "Cancel" button
    protected void cancelBtn(object sender, EventArgs e)
    {
        int poNo = 0;
        int statusId = 7;
        LinkButton btnlink = sender as LinkButton;
        if (btnlink != null)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;

            poNo = Convert.ToInt32(dgvShowAllPO.Rows[index].Cells[0].Text);
            emp = (Employee)Session["empId"];
            int empId = emp.EmpId;
            poController.RemovePurchaseOrder(statusId, poNo, empId);
            GetAllPO();
        }
    }

    //Get list of all the puchase order
    private void GetAllPO()
    {
        emp = (Employee)Session["empId"];
        List<POView> listPO = new List<POView>();
        listPO = poController.GetALLPurchaseOrder(emp.EmpId);

        //Create new table
        DataTable dtPO = new DataTable();
        dtPO.Columns.Add("poNo");
        dtPO.Columns.Add("date");
        dtPO.Columns.Add("supplierCode");
        dtPO.Columns.Add("totalAmount");
        dtPO.Columns.Add("status");

        for (int i = 0; i < listPO.Count; i++)
        {
            dtPO.Rows.Add(listPO[i].PONo, listPO[i].PODate, listPO[i].Suppliername, listPO[i].Totalamount, listPO[i].Status);
        }

        dgvShowAllPO.DataSource = dtPO;
        dgvShowAllPO.DataBind();
        btnAssign(listPO);
    }

    //Validate then the button is enabled or disabled
    private void btnAssign(List<POView> listPO)
    {
        int CurrentRow = 0;
        foreach (GridViewRow gvr in dgvShowAllPO.Rows)
        {
            LinkButton btnDelete = (LinkButton)gvr.FindControl("btnDelete");
            LinkButton btnCancel = (LinkButton)gvr.FindControl("btnCancel");
            LinkButton btnSent = (LinkButton)gvr.FindControl("btnSend");

            //When the status 
            if (listPO[CurrentRow].Status == "Pending...")
            {
                btnDelete.Enabled = true;
                btnCancel.Enabled = false;
                btnSent.Enabled = false;
            }
            else if (listPO[CurrentRow].Status == "Approved By Manager" || listPO[CurrentRow].Status == "Approved By Supervisor")
            {
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;
                btnSent.Enabled = true;
            }
            else if (listPO[CurrentRow].Status == "Rejected By Manager" || listPO[CurrentRow].Status == "Rejected By Manager")
            {
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;
                btnSent.Enabled = false;
            }
            else if (listPO[CurrentRow].Status == "Sent Supplier")
            {
                btnDelete.Enabled = false;
                btnCancel.Enabled = true;
                btnSent.Enabled = false;
            }
            else if (listPO[CurrentRow].Status == "Deleted Order")
            {
                btnDelete.Enabled = false;
                btnCancel.Enabled = false;
                btnSent.Enabled = false;
            }
            CurrentRow++;
        }
    }

    //When the user clicks "View" button
    protected void detBtn_Click(object sender, EventArgs e)
    {
        mpe1.Show();
        LinkButton detBtn = sender as LinkButton;
        if (detBtn != null)
        {
            GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
            int index = gvRow.RowIndex;
            supName.Text = dgvShowAllPO.Rows[index].Cells[2].Text;
            purNo.Text = dgvShowAllPO.Rows[index].Cells[0].Text;
            int poNo = Convert.ToInt32(purNo.Text);
            List<PurchaseOrderDetail> ListOrder = poController.GetPurchaseOrderbyID(poNo);

            //Create new table
            DataTable dt = new DataTable();
            dt.Columns.Add("ItemNos");
            dt.Columns.Add("Description");
            dt.Columns.Add("Qty");
            dt.Columns.Add("UnitPrice");
            dt.Columns.Add("Amount");

            foreach (var item in ListOrder)
            {
                dt.Rows.Add(item.ItemCode, item.Catelogue.Description, item.Quantity, item.Catelogue.Price, (item.Catelogue.Price * item.Quantity));
            }
            poDetail.DataSource = dt;
            poDetail.DataBind();

            //calculate total price
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                total += Convert.ToDecimal(dt.Rows[i][4].ToString());
            }
            totalLabel.Text = total.ToString();
        }

    }

}