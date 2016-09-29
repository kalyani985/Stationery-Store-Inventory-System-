<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Report_Analytics.aspx.cs" Inherits="StorePage_Report_Analytics" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="box">
        <h2><u>Analytics Report</u></h2>
        <table>
        <tr>            
           <td class="modal-sm" style="width: 115px; height: 40px; text-align: left;">&nbsp;&nbsp;Item Category</td>
            <td style="width: 180px"><asp:DropDownList ID="ddlItem" runat="server" Width="130px"></asp:DropDownList></td>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Department</td>
            <td><asp:DropDownList ID="ddlDept" runat="server" Width="217px">
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
            <td class="text-center">&nbsp;</td>

        </tr>
            <tr> 
            <td class="modal-sm" style="width: 115px; height: 40px; text-align: left;">&nbsp;&nbsp;Month</td>
            <td>
                <asp:DropDownList ID="ddlMonth" runat="server" Width="130px">
                    <asp:ListItem Value="01">Jan</asp:ListItem>
                    <asp:ListItem Value="02">Feb</asp:ListItem>
                    <asp:ListItem Value="03">Mar</asp:ListItem>
                    <asp:ListItem Value="04">Apr</asp:ListItem>
                    <asp:ListItem Value="05">May</asp:ListItem>
                    <asp:ListItem Value="06">Jun</asp:ListItem>
                    <asp:ListItem Value="07">Jul</asp:ListItem>
                    <asp:ListItem Value="08">Aug</asp:ListItem>
                    <asp:ListItem Value="09">Sep</asp:ListItem>
                    <asp:ListItem Value="10">Oct</asp:ListItem>
                    <asp:ListItem Value="11">Nov</asp:ListItem>
                    <asp:ListItem Value="12">Dec</asp:ListItem>
                </asp:DropDownList></td>
                <td class="modal-sm" style="width: 117px; height: 40px; text-align: left;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Year</td>
                  <td>
                      <asp:DropDownList ID="DropDownList1" runat="server" Width="217px" >
                    <asp:ListItem >2015</asp:ListItem>
                    <asp:ListItem >2016</asp:ListItem>
                    <asp:ListItem >2017</asp:ListItem>
                </asp:DropDownList></td>
                <td style="width: 125px"><asp:Button ID="addBtn" runat="server" Text="Add" CssClass="btn btn-sm btn-info" Width="80px" OnClick="addBtn_Click" /></td>
            </tr>
            <tr>
                <td></td>
                <td style="width: 180px">
                    &nbsp;<asp:ListBox ID="ListBox1" runat="server" Width="204px"></asp:ListBox>
                    &nbsp;<asp:Label ID="Label1" runat="server"></asp:Label>
                </td>
                <td><asp:Button ID="removeBtn" runat="server" Text="Remove" CssClass="btn btn-sm btn-info" Width="80px" OnClick="removeBtn_Click"/></td>
            </tr>
            <tr>
                <td class="modal-sm" style="width: 115px; height: 40px; text-align: left;">&nbsp;&nbsp;Y-Axis: </td>
                <td>  <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="191px">
                   <asp:ListItem Selected="true">Quantity</asp:ListItem>   
                      <asp:ListItem >Cost</asp:ListItem>
                      </asp:RadioButtonList>
                </td>
                <td style="width: 117px">&nbsp;</td>               
            </tr>
            <tr>
                <td></td>
                <td style="width: 180px"></td>
            <td style="width: 117px"><asp:Button ID="btnGenerate" runat="server" Text="Generate" CssClass="btn btn-sm btn-primary" Width="100px" OnClick="btnGenerate_Click" /></td>
            </tr>
    </table>
        <br />
    <rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" Height="513px" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="1144px">
        <LocalReport ReportPath="StorePage\StoreReports\Report6.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSource1" Name="DataSet1" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="AD.StorePage.NASTableAdapters.Rpt_TrendReportTableAdapter">
            <SelectParameters>
                <asp:Parameter Name="DeptCode" Type="String" />
                <asp:Parameter Name="CatId" Type="String" />
                <asp:Parameter Name="FirstMonth" Type="DateTime" />
                <asp:Parameter Name="SecondMonth" Type="DateTime" />
                <asp:Parameter Name="ThirdMonth" Type="DateTime" />
                <asp:Parameter Name="ByQty" Type="Boolean" />
                <asp:Parameter Name="ByPrice" Type="Boolean" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </div>
</asp:Content>

