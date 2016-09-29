<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Outstanding_Requisition.aspx.cs" Inherits="StorePage_Outstanding_Requisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class ="box">
        <h2><u>Outstanding Requisition List</u></h2>
        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" OnRowCommand="GridView1_RowCommand1" OnRowDataBound="GridView1_RowDataBound"
            CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" >
             <emptydatarowstyle forecolor="Red"/>
            <emptydatatemplate>
             No records found.  

             </emptydatatemplate> 
            <Columns>
                <asp:BoundField DataField="DeptName" HeaderText="Dept Code" />
                <asp:BoundField DataField="ItemCode" HeaderText="Item Code" />
                <asp:BoundField DataField="Description" HeaderText="Item Description" />
                <asp:BoundField DataField="OutstandingQuantity" HeaderText="Outstanding Quantity" />
                <asp:BoundField DataField="StockBalance" HeaderText="Stock Balance" />
                <asp:TemplateField>
                             <ItemTemplate>
                               <asp:LinkButton ID ="generateBtn" runat="server" TabIndex="1" Text="Generate" CommandName="Generate"></asp:LinkButton>
                             </ItemTemplate>
                        </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>

