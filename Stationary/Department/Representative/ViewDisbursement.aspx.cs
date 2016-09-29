using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

public partial class Department_Representative_ViewDisbursement : System.Web.UI.Page
{
    Employee emp = new Employee();
        protected void Page_Load(object sender, EventArgs e)
        {
            btn1.Style.Add("display", "none");
            btn2.Style.Add("display", "none");
            BindDisbursement();
        }

        private void BindDisbursement()
        {
            emp = (Employee)Session["empId"];
            //Disbursement entity controller
            DisbursementController disCont = new DisbursementController();

            //Disbursement and list of 
            DisbursementForm dis = new DisbursementForm();
            List<DisbursementForm> listDs = new List<DisbursementForm>();

            //getAllDisbursement with null deptCode
            listDs = disCont.DisbursementList(emp.DeptCode);

            DataTable table = new DataTable();

            //int count = listemp[0].
            // Add columns.
            table.Columns.Add("DFNos");
            table.Columns.Add("Date");
            table.Columns.Add("Status");


            // Add rows.
            foreach (var array in listDs)
            {
                table.Rows.Add(array.DFNo, array.Date.ToShortDateString(), getStatus(array.StatusId));
            }

            //Session["Pri_Table"] = table;
            DisburseGridView.DataSource = table;
            DisburseGridView.DataBind();
           
        }//BindDisbursement

        protected void DisburseGridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DisburseGridView.PageIndex = e.NewPageIndex;
            BindDisbursement();
        }

        protected void detBtn_Click(object sender, EventArgs e)
        {
            emp = (Employee)Session["empId"];
            mpe1.Show();
            LinkButton detBtn = sender as LinkButton;
            if (detBtn != null)
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                DisbursementController dbCtrl = new DisbursementController();

                DisbursementForm dF = new DisbursementForm();
                List<Process> list_dF = new List<Process>();
                Representative Rep = new Representative();
                CollectionPointController colPtCtrl = new CollectionPointController();
                Rep = colPtCtrl.GetCurrentRepresentativeCollection(emp.DeptCode);
                String testColPt = getCollectionPt(Rep.CollectionPoint.CollectionId);
                disList.Text = DisburseGridView.Rows[index].Cells[0].Text;
                date.Text = DisburseGridView.Rows[index].Cells[1].Text;
                repName.Text = emp.Name;
                collPoint.Text = testColPt;

                int dFNum = Convert.ToInt32(disList.Text);

                String deptC = emp.DeptCode;
                list_dF = dbCtrl.ProcessListByDisID(deptC, dFNum);
                DataTable table = new DataTable();

                table.Columns.Add("ItemNo");
                table.Columns.Add("DFNum");
                table.Columns.Add("Description");
                table.Columns.Add("Qty");

                foreach (var array in list_dF)
                {
                    table.Rows.Add(array.ItemNumber, array.DFNo, array.Catelogue.Description, array.Quantity);
                }

                disDetail.DataSource = table;
                disDetail.DataBind();
     
            }
        }


        protected String getStatus(int statusId)
        {
            switch (statusId)
            {
                case 3:
                    return "Processed";
                case 4:
                    return "Not Processed";
                default:
                    return "Unknown";
            }
        }//getStatus

        private string getCollectionPt(int p)
        {
            switch (p)
            {
                case 1:
                    return "Admin Building (9:30am)";
                case 2:
                    return "Management School (11:00am)";
                case 3:
                    return "Medical School (9:30am)";
                case 4:
                    return "Engineering School (11:00am)";
                case 5:
                    return "Science School (9:30am)";
                case 6:
                    return "University Hall (11:00am)";
                default:
                    return "Unknown";
            }
        }//getCollectionPt

        protected void disDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            disDetail.PageIndex = e.NewPageIndex;

            emp = (Employee)Session["empId"];
            LinkButton detBtn = sender as LinkButton;
            if (detBtn != null)
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                DisbursementController dbCtrl = new DisbursementController();

                DisbursementForm dF = new DisbursementForm();
                List<Process> list_dF = new List<Process>();
                Representative Rep = new Representative();
                CollectionPointController colPtCtrl = new CollectionPointController();
                Rep = colPtCtrl.GetCurrentRepresentativeCollection(emp.DeptCode);
                String testColPt = getCollectionPt(Rep.CollectionPoint.CollectionId);
                disList.Text = DisburseGridView.Rows[index].Cells[0].Text;
                date.Text = DisburseGridView.Rows[index].Cells[1].Text;
                repName.Text = emp.Name;
                collPoint.Text = testColPt;

                int dFNum = Convert.ToInt32(disList.Text);

                String deptC = emp.DeptCode;
                list_dF = dbCtrl.ProcessListByDisID(deptC, dFNum);
                DataTable table = new DataTable();

                table.Columns.Add("ItemNo");
                table.Columns.Add("DFNum");
                table.Columns.Add("Description");
                table.Columns.Add("Qty");

                foreach (var array in list_dF)
                {
                    table.Rows.Add(array.ItemNumber, array.DFNo, array.Catelogue.Description, array.Quantity);
                }

                disDetail.DataSource = table;
                disDetail.DataBind();

            }
     

        }
}
