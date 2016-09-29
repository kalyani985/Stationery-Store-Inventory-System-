<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ViewSupplierList.aspx.cs" Inherits="StorePage_SupMng_ViewSupplierList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <div class="box">
        <h2><u>View Supplier List</u></h2>
        <div>
                <asp:GridView ID="SupplierGridView" runat="server" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                    <asp:BoundField HeaderText="Supplier Code" DataField="SupplierCode" />
                    <asp:BoundField HeaderText="Supplier Name" DataField="SupplierName" />
                    <asp:BoundField HeaderText="Contact Name" DataField="ContactName" />
                    <asp:BoundField HeaderText="Phone No" DataField="PhoneNo" />
                    <asp:BoundField HeaderText="Fax No" DataField="FaxNo" />
                    <asp:BoundField HeaderText="Address" DataField="Address" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
              </asp:GridView> 
        </div>
    </div>
</asp:Content>

