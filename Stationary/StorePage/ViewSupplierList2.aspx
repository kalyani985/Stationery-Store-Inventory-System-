<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ViewSupplierList2.aspx.cs" Inherits="StorePage_ViewSupplierList2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="box">
       <h2><u>View Supplier List</u></h2>
       <div>
         <asp:GridView ID="SupplierGridView1" runat="server" AutoGenerateColumns="False" 
             DataKeyNames="SupplierCode" DataSourceID="SqlDataSource1" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                    <asp:BoundField HeaderText="SupplierCode" DataField="SupplierCode" ReadOnly="True" SortExpression="SupplierCode" />
                    <asp:BoundField HeaderText="SupplierName" DataField="SupplierName" SortExpression="SupplierName" />
                    <asp:BoundField HeaderText="ContactNmae" DataField="ContactNmae" SortExpression="ContactNmae" />
                    <asp:BoundField HeaderText="PhoneNo" DataField="PhoneNo" SortExpression="PhoneNo" />
                    <asp:BoundField HeaderText="FaxNo" DataField="FaxNo" SortExpression="FaxNo" />
                <asp:BoundField HeaderText="Address" DataField="Address" SortExpression="Address" />
                </Columns>
              </asp:GridView> 
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StationeryInventoryConnectionString %>" SelectCommand="SELECT [SupplierCode], [SupplierName], [ContactNmae], [PhoneNo], [FaxNo], [Address] FROM [Supplier]"></asp:SqlDataSource>
            </div>
        </div>
</asp:Content>

