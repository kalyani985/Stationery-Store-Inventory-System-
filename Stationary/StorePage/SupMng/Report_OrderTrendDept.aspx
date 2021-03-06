﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Report_OrderTrendDept.aspx.cs" Inherits="StorePage_SupMng_Report_OrderTrendDept" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class ="box ">
        <h2><u>Order Trend Analysis by Department</u></h2>
        <table>
        <tr>
            <td class="modal-sm" style="width: 115px; height: 40px;">&nbsp;&nbsp;Department</td>
            <td class="modal-sm" style="width: 228px"><asp:DropDownList ID="ddlDept" runat="server" Width="217px">
                 <asp:ListItem>Arts and Science Dept</asp:ListItem>
                    <asp:ListItem>Business School Dept</asp:ListItem>
                    <asp:ListItem>Commerce Dept</asp:ListItem>
                    <asp:ListItem>Computer Science</asp:ListItem>
                    <asp:ListItem>Engineering Dept</asp:ListItem>
                    <asp:ListItem>English Dept</asp:ListItem>
                    <asp:ListItem>Medical Dept</asp:ListItem>
                    <asp:ListItem>Registrar Dept</asp:ListItem>
                    <asp:ListItem>Science Dept</asp:ListItem>
                    <asp:ListItem>Zoology Dept</asp:ListItem>
                 
                      </asp:DropDownList>
            </td>
            <td class="modal-sm" style="width: 130px; height: 40px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Month&nbsp;</td>
            <td style="height: 40px; width: 254px"><asp:DropDownList ID="DropDownList1" runat="server" Width="217px">
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
            <td><asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-sm btn-primary" Width="100px" OnClick="btnGenerate_Click" /></td>
        </tr>
    </table>

        <!--Add crystal report viewer-->
    
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="477px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1140px">
        <LocalReport ReportPath="StorePage\StoreReports\Report2.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="SqlDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
        </rsweb:ReportViewer>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:StationeryInventoryConnectionString %>" SelectCommand="select c.Description,rd.ItemNumber,sum(rd.Quantity) as Quantity from RequisitionTransaction r, RequisitionTransactionDetail rd,Department d,Catelogue c
where r.RequisitionId = rd.RequisitionID and rd.DeptCode = d.DeptCode and c.ItemNumber=rd.ItemNumber and d.Name=@DeptName and MONTH(r.Date)=@Month group by rd.ItemNumber,c.Description">
            <SelectParameters>
                <asp:QueryStringParameter Name="DeptName" QueryStringField="DeptName" />
                <asp:QueryStringParameter Name="Month" QueryStringField="Month" />
            </SelectParameters>
        </asp:SqlDataSource>
            </div>
</asp:Content>

