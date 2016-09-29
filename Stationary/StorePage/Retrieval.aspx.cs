using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;
public partial class StorePage_Retrieval : System.Web.UI.Page
{
    Boolean Consolidated = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        dateTxt.Text = DateTime.Today.ToString("dd/MM/yyyy");
        if (!IsPostBack)
        {
            BindRetrieval();
        }

    }

    private void BindRetrieval()
    {
        //Disbursement entity controller
        DisbursementController dsC = new DisbursementController();

        //Retrieval Model and list of 
        RetrieveModel rm = new RetrieveModel();
        List<RetrieveModel> listRm = new List<RetrieveModel>();

        //getAllRetrieval 
        listRm = dsC.GetRetrieveALL();
        DataTable table = new DataTable();

        // Add columns.
        table.Columns.Add("ItemNos");
        table.Columns.Add("Bin");
        table.Columns.Add("ItemName");
        table.Columns.Add("QtyNeed");
        table.Columns.Add("ActualQty");
        table.Columns.Add("DeptCode");
        table.Columns.Add("DeptNeed");
        table.Columns.Add("Show");

        DataTable dt = new DataTable();
        dt = (DataTable)Session["CurrentBalance"];
        if (dt == null)
        {
            dt = new DataTable();
            dt.Columns.Add("ItemNo");
            dt.Columns.Add("CurrentQty");
        }
        Session.Add("CurrentBalance", dt);

        // Add rows.
        foreach (var array in listRm)
        {
            if (array.Needitem < array.Actualitem)
            {
                if (array.Consolidateitem.Count > 1)
                {
                    table.Rows.Add(array.Consolidateitem[0].Itemno, array.Bin, array.Itemname, array.Needitem, array.Actualitem, null, null, false);

                    //Testing
                    //System.Diagnostics.Debug.WriteLine(">>>>>>>" + array.Consolidateitem.GetType());

                    foreach (var subarray in array.Consolidateitem)
                    {
                        table.Rows.Add(subarray.Itemno, null, null, null, null, subarray.Departmentcode, subarray.Qty, false);
                    }

                    //Saving current stock for each item
                    DataTable dttemp = (DataTable)Session["CurrentBalance"];
                    if (dttemp.Rows.Count < 1)
                    {
                        dttemp.Rows.Add(array.Consolidateitem[0].Itemno, array.Actualitem);
                    }
                    else
                    {
                        for (int i = 0; i < dttemp.Rows.Count; i++)
                        {
                            if (dttemp.Rows[i][0].ToString() != array.ItemNo)
                            {
                                dttemp.Rows.Add(array.Consolidateitem[0].Itemno, array.Actualitem);
                                break;
                            }
                        }
                    }



                    Session.Add("CurrentBalance", dttemp);

                }
                else
                {

                    table.Rows.Add(array.Consolidateitem[0].Itemno, array.Bin, array.Itemname, array.Needitem, array.Actualitem, array.Consolidateitem[0].Departmentcode, array.Consolidateitem[0].Qty, false);

                    //Saving current stock for each item
                    DataTable dttemp = (DataTable)Session["CurrentBalance"];
                    if (dttemp.Rows.Count < 1)
                    {
                        dttemp.Rows.Add(array.Consolidateitem[0].Itemno, array.Actualitem);
                    }
                    else
                    {
                        if (dttemp.Rows.Count < 1)
                        {
                            dttemp.Rows.Add(array.Consolidateitem[0].Itemno, array.Actualitem);
                        }
                        else
                        {
                            for (int i = 0; i < dttemp.Rows.Count; i++)
                            {
                                if (dttemp.Rows[i][0].ToString() != array.ItemNo)
                                {
                                    dttemp.Rows.Add(array.Consolidateitem[0].Itemno, array.Actualitem);
                                    break;
                                }
                            }
                        }
                    }
                    Session.Add("CurrentBalance", dttemp);
                }
            }
            else
            {

                if (array.Consolidateitem.Count > 1)
                {
                    table.Rows.Add(array.Consolidateitem[0].Itemno, array.Bin, array.Itemname, array.Needitem, array.Actualitem, null, null, false);

                    foreach (var subarray in array.Consolidateitem)
                    {
                        table.Rows.Add(subarray.Itemno, null, null, null, null, subarray.Departmentcode, subarray.Qty, true);
                    }

                    //Saving current stock for each item
                    DataTable dttemp = (DataTable)Session["CurrentBalance"];

                    if (dttemp.Rows.Count < 1)
                    {
                        dttemp.Rows.Add(array.Consolidateitem[0].Itemno, array.Actualitem);
                    }
                    else
                    {
                        for (int i = 0; i < dttemp.Rows.Count; i++)
                        {
                            if (dttemp.Rows[i][0].ToString() != array.ItemNo)
                            {
                                dttemp.Rows.Add(array.Consolidateitem[0].Itemno, array.Actualitem);
                                break;
                            }
                        }
                    }

                    Session.Add("CurrentBalance", dttemp);
                }
                else
                {
                    table.Rows.Add(array.Consolidateitem[0].Itemno, array.Bin, array.Itemname, array.Needitem, array.Actualitem, array.Consolidateitem[0].Departmentcode, array.Consolidateitem[0].Qty, true);
                    DataTable dttemp = (DataTable)Session["CurrentBalance"];

                    //Saving current stock for each item
                    if (dttemp.Rows.Count < 1)
                    {
                        dttemp.Rows.Add(array.Consolidateitem[0].Itemno, array.Actualitem);
                    }
                    else
                    {
                        for (int i = 0; i < dttemp.Rows.Count; i++)
                        {
                            if (dttemp.Rows[i][0].ToString() != array.ItemNo)
                            {
                                dttemp.Rows.Add(array.Consolidateitem[0].Itemno, array.Actualitem);
                                break;
                            }
                        }
                    }

                    Session.Add("CurrentBalance", dttemp);
                }

            }
        }
        createSession(listRm);
        GridView1.Columns[0].Visible = true;
        GridView1.Columns[7].Visible = true;

        GridView1.DataSource = table;
        GridView1.DataBind();
        GridView1_Show(table);

        GridView1.Columns[0].Visible = false;
        GridView1.Columns[7].Visible = false;
    }

    private void createSession(List<RetrieveModel> listRm)
    {
        RetrieveModel rm = new RetrieveModel();

        ConsolidateItem rmCI;
        List<ConsolidateItem> List_rmCI = new List<ConsolidateItem>();

        foreach (var array in listRm)
        {
            if (array.Consolidateitem.Count >= 1)
            {
                foreach (var subarray in array.Consolidateitem)
                {
                    rmCI = new ConsolidateItem();
                    rmCI.Departmentcode = subarray.Departmentcode;
                    rmCI.Itemno = subarray.Itemno;
                    rmCI.Qty = subarray.Qty;
                    List_rmCI.Add(rmCI);
                }
            }
        }

        Session["RetrConsItms"] = List_rmCI;
    }

    protected void GridView1_Show(DataTable dt)
    {
        List<int> RowNos = new List<int>();

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            GridView1.Rows[i].Cells[9].Visible = true;
            if (dt.Rows[i][7].Equals("False"))
            {
                for (int j = 0; j < GridView1.Rows[i].Cells.Count; j++)
                {
                    TextBox tBox = (TextBox)GridView1.Rows[i].Cells[j].FindControl("SetQty_Retr");
                    if (tBox != null)
                    {
                        tBox.Visible = false;
                    }
                }
                GridView1.Rows[i].Cells[9].Visible = false;
            }
            if (dt.Rows[i][0].Equals(""))
            {
                for (int j = 0; j < GridView1.Rows[i].Cells.Count; j++)
                {
                    TextBox tBox = (TextBox)GridView1.Rows[i].Cells[j].FindControl("SetQty_Retr");
                    if (tBox != null)
                    {
                        tBox.Visible = false;

                    }
                }
                GridView1.Rows[i].Cells[9].Visible = false;
            }
        }

    }//GridView1_Show


    private void updateSession(string ItemNos, string DeptCode, int Qty, object sender)
    {
        ConsolidateItem newRmCI = new ConsolidateItem();
        List<ConsolidateItem> List_rmCI = new List<ConsolidateItem>();

        newRmCI.Departmentcode = DeptCode;
        newRmCI.Itemno = ItemNos;
        newRmCI.Qty = Qty;

        int tstQty = 0;//To hold the current stock level for item\\


        ImageButton btn = (ImageButton)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int RowIndex1 = gvr.RowIndex;


        DataTable dtchkQty = new DataTable();
        dtchkQty = (DataTable)Session["CumulativeDisburseLevel"];
        if (dtchkQty == null)
        {
            dtchkQty = new DataTable();
            dtchkQty.Columns.Add("ItemNumber");
            dtchkQty.Columns.Add("Qty");
            dtchkQty.Columns.Add("RowIndex");
            dtchkQty.Rows.Add(ItemNos, Qty, RowIndex1);
        }
        else
        {
            int check = 0;
            for (int i = 0; i < dtchkQty.Rows.Count; i++)
            {
                if (dtchkQty.Rows[i][0].ToString() == ItemNos)
                {
                    //ImageButton btn1 = (ImageButton)sender;

                    ////Get the row that contains this button
                    //GridViewRow gvr1 = (GridViewRow)btn.NamingContainer;
                    //int RowIndex1 = gvr.RowIndex;

                    int total = 0;

                    if (Convert.ToInt32(dtchkQty.Rows[i][2].ToString()) == RowIndex1)
                    {
                        dtchkQty.Rows[i][1] = Qty;
                        break;
                    }
                    else
                    {

                        total = Convert.ToInt32(dtchkQty.Rows[i][1].ToString()) + Qty;
                        dtchkQty.Rows[i][1] = total;
                        check = 1;
                        break;
                    }
                }
            }
            if (check == 0)
            {
                dtchkQty.Rows.Add(ItemNos, Qty, RowIndex1);
            }
        }
        Session["CumulativeDisburseLevel"] = dtchkQty;


        List_rmCI = (List<ConsolidateItem>)Session["RetrConsItms"];

        DataTable dt = (DataTable)Session["CurrentBalance"];

        int rowcount = 0;
        //Look for item in Session
        foreach (var item in List_rmCI)
        {
            if (item.Itemno == newRmCI.Itemno)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (item.Itemno == dt.Rows[i][0].ToString())
                    {
                        tstQty = Convert.ToInt32(dt.Rows[i][1]);
                        break;
                    }
                }

                for (int i = 0; i < dtchkQty.Rows.Count; i++)
                {
                    if (dtchkQty.Rows[i][0].ToString() == ItemNos)
                    {
                        if (Convert.ToInt32(dtchkQty.Rows[i][1].ToString()) > tstQty)
                        {
                            DataTable dtbackQty = new DataTable();
                            dtbackQty = (DataTable)Session["CumulativeDisburseLevel"];
                            for (int j = 0; j < dtbackQty.Rows.Count; j++)
                            {
                                if (dtbackQty.Rows[i][0].ToString() == ItemNos)
                                {
                                    int backqty = 0;
                                    backqty = Convert.ToInt32(dtbackQty.Rows[i][1].ToString()) - Qty;
                                    dtbackQty.Rows[i][1] = backqty;
                                    break;
                                }
                            }
                            Session["CumulativeDisburseLevel"] = dtbackQty;

                            //ImageButton btn = (ImageButton)sender;

                            ////Get the row that contains this button
                            //GridViewRow gvr = (GridViewRow)btn.NamingContainer;
                            //int RowIndex = gvr.RowIndex;

                            TextBox txtQty = (TextBox)gvr.FindControl("SetQty_Retr") as TextBox;
                            txtQty.Text = "";
                            txtQty.Focus();

                            ClientScript.RegisterStartupScript(Page.GetType(), "Message", "alert('" + "Amount entered exceeds current inventory! Please re-enter correct quantity." + "');", true);
                            return;
                        }
                        else
                        {
                            if (item.Departmentcode == newRmCI.Departmentcode)
                            {
                                //item.Qty = newRmCI.Qty;
                                List_rmCI[rowcount].Qty = newRmCI.Qty;

                                Session["RetrConsItms"] = List_rmCI;
                                break;
                            }
                        }
                    }
                }


                //Check if exceed current inventory



            }
            rowcount++;
        }
    }//updateSession

    protected void OnPaging(object sender, GridViewPageEventArgs e)
    {

        GridView1.PageIndex = e.NewPageIndex;
        BindRetrieval();
    }


    protected void btnSetQty_Retr_Click(object sender, ImageClickEventArgs e)
    {
        //Get the button that raised the event
        ImageButton btn = (ImageButton)sender;

        //Get the row that contains this button
        GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        int RowIndex = gvr.RowIndex;

        var testItemNos = GridView1.Rows[RowIndex].Cells[0].Text;
        var testDeptCode = GridView1.Rows[RowIndex].Cells[5].Text;
        TextBox txtQty = (TextBox)gvr.FindControl("SetQty_Retr") as TextBox;
        int testQty = 0;

        if (txtQty != null)
        {
            if (txtQty.Text.Equals(""))
                testQty = 0;
            else
                testQty = Convert.ToInt16(txtQty.Text);

            updateSession(testItemNos, testDeptCode, testQty, sender);
        }

    }//btnSetQty_Retr_Click



    protected void processBtn_Click(object sender, EventArgs e)
    {
        //Get the button that raised the event
        //Button btn = (Button)sender;

        //Disbursement entity controller
        DisbursementController dsC = new DisbursementController();

        //Get List of consolidated items from Sesssion
        List<ConsolidateItem> testL = (List<ConsolidateItem>)Session["RetrConsItms"];

        //Check all text box not blank
        //GridViewRow gvr = (GridViewRow)btn.NamingContainer;
        //int RowIndex = gvr.RowIndex;

        foreach (GridViewRow row in GridView1.Rows)
        {
            if (!((TextBox)row.FindControl("SetQty_Retr")).Equals(""))
            {
                //Process when all textbox is filled   
                dsC.ProcessRequisition(testL);
                Session.Remove("RetrConsItms");
                Session.Remove("CumulativeDisburseLevel");
                Session.Remove("CurrentBalance");
                GridView1.DataBind();
            }
        }



    }

    //void GridView1_RowCreated(Object sender, GridViewRowEventArgs e)
    //{

    //    // The GridViewCommandEventArgs class does not contain a 
    //    // property that indicates which row's command button was
    //    // clicked. To identify which row's button was clicked, use 
    //    // the button's CommandArgument property by setting it to the 
    //    // row's index.
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //         if (e.Row.Cells[7]==null) 
    //        {
    //          e.Row.Cells[8].Visible = false;
    //          e.Row.Cells[9].Visible = false;

    //        }
    //    }

    //}
    //protected void GridView1_RowDataBound(Object sender, GridViewRowEventArgs e)
    //  {
    //    if(e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        GridViewRow gvr = e.Row;
    //      // Hide the edit button when some condition is true
    //      // for example, the row contains a certain property
    //      if (gvr.Cells[7]==null) 
    //      {
    //          gvr.Cells[8].Visible = false;
    //          gvr.Cells[9].Visible = false;

    //          //ImageButton btnEdit = (ImageButton)e.Row.Cells[9].Controls[0]; //FindControl("btnSetQty_Retr");
    //          //btnEdit.Visible = false;
    //      }
    //    }   
    //  }
    //Pagenation
    //protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    BindRetrieval();
    //}
}