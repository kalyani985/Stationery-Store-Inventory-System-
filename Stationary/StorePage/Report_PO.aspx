<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Report_PO.aspx.cs" Inherits="StorePage_Report_PO" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="box">
        <h2><u>Reorder Report</u></h2>
        <table>
        <tr>
            <td class="modal-sm" style="width: 109px; height: 40px;">Month</td>
            <td class="modal-sm" style="width: 228px"><asp:DropDownList ID="ddlMonth" AutoPostBack="true" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" runat="server" Width="217px">
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
            <td class="modal-sm" style="width: 228px"><asp:DropDownList ID="ddlPO" runat="server" Width="217px"></asp:DropDownList>

            <td class="modal-sm" style="width: 155px; height: 40px;"></td>
            <td>&nbsp;</td>
            <td><asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-sm btn-primary" Width="100px" OnClick="btnGenerate_Click" /></td>
        </tr>
    </table>
       
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="513px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1144px">
        <LocalReport ReportPath="StorePage\StoreReports\Report5.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource3" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StationeryInventoryConnectionString %>" SelectCommand="select pd.ItemCode,c.Description,sb.BalanceAmount,c.ReorderLevel,c.ReorderQuantity,pd.PONo
from Catelogue c,PurchaseOrderDetail pd,StockBalance sb
where
pd.ItemCode=c.ItemNumber and c.BalanceId=sb.BalanceId and pd.PONo=@PONO">
        <SelectParameters>
            <asp:QueryStringParameter Name="PONO" QueryStringField="PONO" />
        </SelectParameters>
    </asp:SqlDataSource>
 </div>
</asp:Content>

