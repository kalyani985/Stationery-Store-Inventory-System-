<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" enableEventValidation="false" AutoEventWireup="true" CodeFile="Delegate.aspx.cs" Inherits="Department_HOD_Delegate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 <style type="text/css">
        .ajax__calendar_container TABLE {
            font-size: 10px;
            width: 100px;
        }

        .cal_Theme1 .ajax__calendar_container {
            background-color: #e2e2e2;
            border: solid 1px #cccccc;
        }

        .cal_Theme1 .ajax__calendar_header {
            background-color: #ffffff;
            margin-bottom: 4px;
        }

        .cal_Theme1 .ajax__calendar_title,
        .cal_Theme1 .ajax__calendar_next,
        .cal_Theme1 .ajax__calendar_prev {
            color: #004080;
            padding-top: 3px;
        }

        .cal_Theme1 .ajax__calendar_body {
            background-color: #e9e9e9;
            border: solid 1px #cccccc;
        }

        .cal_Theme1 .ajax__calendar_dayname {
            text-align: center;
            font-weight: bold;
            margin-bottom: 4px;
            margin-top: 2px;
        }

        .cal_Theme1 .ajax__calendar_day {
            text-align: center;
        }

        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_day,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_month,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_year,
        .cal_Theme1 .ajax__calendar_active {
            color: #004080;
            font-weight: bold;
            background-color: #ffffff;
        }

        .cal_Theme1 .ajax__calendar_today {
            font-weight: bold;
        }

        .cal_Theme1 .ajax__calendar_other,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_today,
        .cal_Theme1 .ajax__calendar_hover .ajax__calendar_title {
            color: #bbbbbb;
        }
    </style>
    <div class="container-fluid">
        <div class="box">
            <h2><u>Assign Delegate</u></h2>

            <table>
                <tr>
                    <td style="width: 133px"><b>Delegate Period</b></td>
                </tr>
            </table>
            Start Date<asp:TextBox ID="startDateTxt" runat="server" Width="130px"> </asp:TextBox>
            
            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="startDateTxt" ></cc1:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Star date is required." ControlToValidate="startDateTxt" CssClass="text-danger"></asp:RequiredFieldValidator>
            <br />
            <p></p>
            End Date 
            <asp:TextBox ID="endDateTxt" runat="server" Width="130px"></asp:TextBox>
            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="endDateTxt"></cc1:CalendarExtender>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="End date is required." ControlToValidate="endDateTxt" CssClass="text-danger"></asp:RequiredFieldValidator>
            <asp:CompareValidator ID="cmpVal1" ControlToCompare="startDateTxt" ControlToValidate="endDateTxt" Type="Date" Operator="GreaterThanEqual" ErrorMessage="End date should be after start date." runat="server" ForeColor="Red"></asp:CompareValidator>

            <br /> <br />
            <table>
                <tr>
                    <td>Please select one employee from the list below: </td>
                    <td></td>
                </tr>
            </table>
            <asp:GridView ID="representativeName" runat="server" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound"
                Width="254px" OnSelectedIndexChanged="representativeName_SelectedIndexChanged">
                <Columns>
                    <asp:TemplateField HeaderText="Employee Name">
                        <ItemTemplate>
                            <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Eval("Empid") %>' />
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            <br />
            <table>
                <tr>
                    <td></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="width: 266px">
                        <asp:Label ID="deleStatuslb" runat="server" Font-Bold="True"></asp:Label></td>
                    <td></td>
                </tr>
                <tr>
                    <td style="height: 33px; width: 266px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    <td style="height: 33px">
                        <asp:Button ID="SaveBtn" runat="server" Text="Submit" CssClass="btn btn-sm btn-primary" OnClick="SaveBtn_Click" Width="100px" /></td>
                </tr>
            </table>

        </div>
    </div>
</asp:Content>

