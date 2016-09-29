<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Report_Dis.aspx.cs" Inherits="StorePage_Report_Dis" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="box">
        <h2><u>Disburmenet List for each Department</u></h2>
        <table>
        <tr>
            <td class="modal-sm" style="width: 109px; height: 40px;">Department</td>
            <td class="modal-sm" style="width: 228px"><asp:DropDownList ID="ddlDept" runat="server" Width="217px">
                 <asp:ListItem Value="ARTS">Arts and Science Dept</asp:ListItem>
                    <asp:ListItem Value="BUSS">Business School Dept</asp:ListItem>
                    <asp:ListItem Value="COMM">Commerce Dept</asp:ListItem>
                    <asp:ListItem Value="CPSC">Computer Science</asp:ListItem>
                    <asp:ListItem Value="ENGG">Engineering Dept</asp:ListItem>
                    <asp:ListItem Value="ENGL">English Dept</asp:ListItem>
                    <asp:ListItem Value="MEDI">Medical Dept</asp:ListItem>
                    <asp:ListItem Value="REGR">Registrar Dept</asp:ListItem>
                    <asp:ListItem Value="SCIE">Science Dept</asp:ListItem>
                    <asp:ListItem Value="ZOOL">Zoology Dept</asp:ListItem> 
                      </asp:DropDownList>

            <td class="modal-sm" style="width: 155px; height: 40px;"></td>
            <td>&nbsp;</td>
            <td><asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-sm btn-primary" Width="100px" OnClick="btnGenerate_Click" /></td>
        </tr>
    </table>
        
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="513px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1144px">
        <LocalReport ReportPath="StorePage\StoreReports\Report4.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource2" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:StationeryInventoryConnectionString %>" SelectCommand="select c.Description, p.Quantity, cp.Description as CollectionPoint,dp.Name,emp.Name as Representative
from Catelogue c, Process p, DisbursementForm df, Representative r, CollectionPoint cp,Department dp,Employee emp
where df.StatusId=4 and p.ItemNumber=c.ItemNumber and df.DFNo=p.DFNo and df.DeptCode=@DeptCode
and df.DeptCode = r.Deptcode and r.CollectionId = cp.CollectionId and df.DeptCode=dp.DeptCode and r.EmpId=emp.EmpId
">
        <SelectParameters>
            <asp:QueryStringParameter Name="DeptCode" QueryStringField="DeptCode" />
        </SelectParameters>
    </asp:SqlDataSource>
        </div>
</asp:Content>

