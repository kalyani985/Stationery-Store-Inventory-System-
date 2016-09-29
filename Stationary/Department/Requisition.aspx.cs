using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;
public partial class Department_Requisition : System.Web.UI.Page
 {
        string querystringValue = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btn1.Style.Add("display", "none");
                btn2.Style.Add("display", "none");
                querystringValue = Request.QueryString["Id"];
                BindRequisitions();
            }           
        }

        private void BindRequisitions()
        {            
            //Requisition entity controller
            RequisitionController rq = new RequisitionController();

            Employee emp = (Employee)Session["empId"];
            int empId = emp.EmpId;

            //RequisitionTransaction and list of 
            RequisitionTransaction rt = new RequisitionTransaction();
            List<RequisitionTransaction> listRq = new List<RequisitionTransaction>();
           
            //getAllRequisition for employee ID
            listRq = rq.getALLRequisition(empId);
            DataTable table = new DataTable();

            //int count = listemp[0].
            // Add columns.
            table.Columns.Add("Date");
            table.Columns.Add("RequisitionNumber");
            table.Columns.Add("Status");
            table.Columns.Add("Reason");
          

            // Add rows.
            foreach (var array in listRq)
            {
                table.Rows.Add(array.Date.ToShortDateString(), array.RequisitionId, array.Status.StatusDescription, array.Remarks);
            }
            requisitionGridView.DataSource = table;
            requisitionGridView.DataBind();
        }

        protected void detBtn_Click(object sender, EventArgs e)
        {
            mpe1.Show();
            LinkButton detBtn = sender as LinkButton;
            if (detBtn != null)
            {
                GridViewRow gvRow = (GridViewRow)(sender as Control).Parent.Parent;
                int index = gvRow.RowIndex;
                RequisitionController rq = new RequisitionController();

                //RequisitionTransactionDetail and list of 
                RequisitionTransactionDetail rtd = new RequisitionTransactionDetail();
                List<RequisitionTransactionDetail> listRtd = new List<RequisitionTransactionDetail>();
                CatelogueController catController = new CatelogueController();

                int reqId = Convert.ToInt32(requisitionGridView.Rows[index].Cells[1].Text);
                //getAllRequisition for requisition ID
                reqNo.Text = Convert.ToString(reqId);
                listRtd = rq.getRequisitionDetailbyID(reqId);
                DataTable table = new DataTable();

                //int count = listemp[0].
                // Add columns.
                table.Columns.Add("Description");
                table.Columns.Add("Qty");
                table.Columns.Add("UOM");
                //table.Columns.Add("Reason");

                Catelogue cat = new Catelogue();

                // Add rows.
                foreach (var array in listRtd)
                {
                    cat = catController.GetCatelogue(array.ItemNumber);
                    table.Rows.Add(cat.Description, array.Quantity, cat.UnitOfMeasure);
                }

                reDetail.DataSource = table;
                reDetail.DataBind();
            }
        }

        protected void createBtn_Click(object sender, EventArgs e)
        {
            Response.Redirect("CreateRequisition.aspx");
        }

        protected void gridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            requisitionGridView.PageIndex = e.NewPageIndex;
            BindRequisitions();
        }
    }