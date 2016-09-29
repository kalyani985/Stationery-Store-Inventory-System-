using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

public partial class StorePage_SupMng_ViewSupplierList : System.Web.UI.Page
{
    //Author: Zhang Sha
        //Intantialize object
        SupplierController sc = new SupplierController();

        List<Supplier> sList = new List<Supplier>();

        DataTable table = new DataTable();

        //When the page loads, list all the suppliers
        protected void Page_Load(object sender, EventArgs e)
        {
            sList = sc.GetALLSupplier();

            table.Columns.Add("SupplierCode");
            table.Columns.Add("SupplierName");
            table.Columns.Add("ContactName");
            table.Columns.Add("PhoneNo");
            table.Columns.Add("FaxNo");
            table.Columns.Add("Address");
            foreach (Supplier item in sList)
            {
                table.Rows.Add(item.SupplierCode, item.SupplierName, item.ContactNmae, item.PhoneNo, item.FaxNo, item.Address);
            }
            SupplierGridView.DataSource = table;
            SupplierGridView.DataBind();
    }
}