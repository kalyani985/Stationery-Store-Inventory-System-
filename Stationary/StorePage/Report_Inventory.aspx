<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Report_Inventory.aspx.cs" Inherits="StorePage_Report_Inventory" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class ="box ">
        <h2><u>Inventory Report</u></h2>
        <table>
        <tr>
            <td class="modal-sm" style="width: 115px; height: 40px;">&nbsp;&nbsp;Month</td>
            <td class="modal-sm" style="width: 228px"><asp:DropDownList ID="ddlMonth" runat="server" Width="217px">
                    <asp:ListItem Value="1">Jan</asp:ListItem>
                    <asp:ListItem Value="2">Feb</asp:ListItem>
                    <asp:ListItem Value="3">Mar</asp:ListItem>
                    <asp:ListItem Value="4">Apr</asp:ListItem>
                    <asp:ListItem Value="5">May</asp:ListItem>
                    <asp:ListItem Value="6">Jun</asp:ListItem>
                    <asp:ListItem Value="7">Jul</asp:ListItem>
                    <asp:ListItem Value="8">Aug</asp:ListItem>
                    <asp:ListItem Value="9">Sep</asp:ListItem>
                    <asp:ListItem Value="10">Oct</asp:ListItem>
                    <asp:ListItem Value="11">Nov</asp:ListItem>
                    <asp:ListItem Value="12">Dec</asp:ListItem>
                                                      </asp:DropDownList></td>
            <td class="modal-sm" style="width: 115px; height: 40px;">&nbsp;&nbsp;Item Category</td>
            <td class="modal-sm" style="width: 228px"><asp:DropDownList ID="DropDownList1" runat="server" Width="217px">
                                                      </asp:DropDownList></td>
<%--            <td class="modal-sm" style="width: 130px; height: 40px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Month&nbsp;</td>
            <td style="height: 40px; width: 254px"><asp:DropDownList ID="DropDownList1" runat="server" Width="217px"></asp:DropDownList></td>--%>
            <td><asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-sm btn-primary" Width="100px" OnClick="btnGenerate_Click" /></td>
        </tr>
    </table>
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Height="21cm" Width="29cm" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt">
        <LocalReport ReportPath="StorePage\StoreReports\Report3.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StationeryInventoryConnectionString %>" SelectCommand="select c.Description,(sb.BalanceAmount) as InventoryLeft
from Catelogue c, CatalogueSpecify cs, StockBalance sb
where c.CategoryId= cs.CatagoryId and c.BalanceId=sb.BalanceId and MONTH(sb.Date)=@Month and cs.CatagoryDesc=@Item
">
            <SelectParameters>
                <asp:QueryStringParameter Name="Month" QueryStringField="Month" />
                <asp:QueryStringParameter Name="Item" QueryStringField="Item" />
            </SelectParameters>
        </asp:SqlDataSource>
    </div>
</asp:Content>

