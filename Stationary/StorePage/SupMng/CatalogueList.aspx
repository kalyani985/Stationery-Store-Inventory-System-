<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="CatalogueList.aspx.cs" Inherits="StorePage_SupMng_CatalogueList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="box">
       <h2><u>Catalogues List</u></h2>
       <div>
           <div class="col-md-5">Category: <asp:DropDownList ID="catDrpDwn" runat="server" Width="200px" DataTextField="CatagoryDesc" DataValueField="CatagoryDesc"
                     OnSelectedIndexChanged="catDrpDwn_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                    <asp:ListItem>ALL</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="inventStatuslb" runat="server"></asp:Label>
                </div>
                <div class="col-md-5"> <asp:TextBox ID="txtSearch" runat="server" Width="300px" Placeholder="Item Description" maxlength="20"></asp:TextBox></div>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-sm btn-primary" OnClick="btnSearch_Click" width="100px"/> 
         <asp:GridView ID="CatalogueGridView" runat="server" AutoGenerateColumns="False"
             CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging ="true" OnPageIndexChanging="CatalogueGridView_PageIndexChanging">
<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
             <Columns>
                 <asp:BoundField DataField="ItemNumber" HeaderText="ItemNumber" />
                 <asp:BoundField DataField="Category" HeaderText="Category" />
                 <asp:BoundField DataField="Description" HeaderText="Description" />
                 <asp:BoundField DataField="SupplierName" HeaderText="SupplierName" />
                 <asp:BoundField DataField="ReorderLevel" HeaderText="ReorderLevel" />
                 <asp:BoundField DataField="ReorderQuantity" HeaderText="ReorderQuantity" />
                 <asp:BoundField DataField="UnitOfMeasure" HeaderText="UnitOfMeasure" />
                 <asp:BoundField DataField="UnitPrice" HeaderText="UnitPrice" />
             </Columns>

<PagerStyle CssClass="pgr"></PagerStyle>
              </asp:GridView> 
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StationeryInventoryConnectionString %>" SelectCommand="SELECT [SupplierCode], [SupplierName], [ContactNmae], [PhoneNo], [FaxNo], [Address] FROM [Supplier]"></asp:SqlDataSource>
            </div>
        </div>
</asp:Content>

