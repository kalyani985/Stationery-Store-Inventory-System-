<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Stock_Card.aspx.cs" Inherits="StorePage_Stock_Card" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class ="box">       
     <h2><u>Stock Card</u></h2> 
        <table style="width: 100%;">
            <tr>
                <td style="width: 186px">
                    <asp:Label ID="itemCodeLbl" runat="server" Text="Item Code :"></asp:Label></td>
                <td style="width: 389px">
                    <asp:Label ID="itemCode" runat="server" Text='<%#Eval("itemCode") %>'></asp:Label></td>
                <td>1st Supplier : BANES</td>
            </tr>
            <tr>
                <td style="width: 186px">
                    <asp:Label ID="itemDespLbl" runat="server" Text="Item Description :"></asp:Label></td>
                <td style="width: 389px">
                    <asp:Label ID="itemDesp" runat="server" Text='<%#Bind("itemDesp") %>'></asp:Label></td>
                <td>2nd Supplier : ALPHA</td>
            </tr>
            <tr>
                <td style="width: 186px">
                    <asp:Label ID="binLbl" runat="server" Text="Bin# :"></asp:Label></td>
                <td style="width: 389px">
                    <asp:Label ID="bin" runat="server" Text='<%#Bind("Bin") %>'></asp:Label></td>
                <td>3rd Supplier : CHEP</td>
            </tr>
            <tr>
                <td style="width: 186px">
                    <asp:Label ID="uomLbl" runat="server" Text="UOM :"></asp:Label></td>
                <td style="width: 389px">
                    <asp:Label ID="uom" runat="server" Text='<%#Bind("UOM") %>'></asp:Label>
                </td>
            </tr>
        </table>
    
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
        <Columns>
            <asp:BoundField DataField="Date" HeaderText="Date" />
            <asp:BoundField DataField="Description" HeaderText="Dept / Supplier" />
            <asp:BoundField DataField="OutstandingQuantity" HeaderText="Quantity" />
            <asp:BoundField DataField="StockBalance" HeaderText="Balance" />
        </Columns>
    </asp:GridView>
        <asp:Button ID="backBtn" runat="server" Text="Back" CssClass="btn btn-sm btn-primary" OnClick="backBtn_Click" width="100px" style="float: right;"/>

        </div>
</asp:Content>

