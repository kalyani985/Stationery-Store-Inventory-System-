using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using InventoryClass;
using System.Data;

public partial class StorePage_ViewSupplierList2 : System.Web.UI.Page
{
    //Intantialize object
    SupplierController sc = new SupplierController();
    List<Supplier> slist = new List<Supplier>();
    DataTable table = new DataTable();

    //List All the Supplier
    protected void Page_Load1(object sender, EventArgs e)
    {
        slist = sc.GetALLSupplier();

        table.Columns.Add("Supplier Code");
        table.Columns.Add("Supplier Name");
        table.Columns.Add("Contact Name");
        table.Columns.Add("Phone No");
        table.Columns.Add("Fax No");
        table.Columns.Add("Address No");
        foreach (Supplier item in slist)
        {
            table.Rows.Add(item.SupplierCode, item.SupplierName, item.ContactNmae, item.PhoneNo, item.FaxNo, item.Address);
        }
        SupplierGridView1.DataSource = table;
        SupplierGridView1.DataBind();
    }
}