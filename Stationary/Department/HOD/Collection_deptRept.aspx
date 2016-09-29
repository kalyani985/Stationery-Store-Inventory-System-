<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Collection_deptRept.aspx.cs" Inherits="Department_HOD_Collection_deptRept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 <style type="text/css">
        body {
            font-family: Arial;
            font-size: 10pt;
        }

        td {
            cursor: pointer;
        }

        .hover_row {
            background-color: #FFFFBF;
        }

        .hidden {
            display: none;
        }

        .auto-style1 {
            width: 163px;
        }

        .auto-style2 {
            height: 5px;
        }

        .auto-style3 {
            width: 445px;
        }
    </style>
    <div class="container-fluid">
        <div class="row">
            <div class="box">
                <h2><u>Change of Collection Point and Representative</u></h2>
                <table>
                    <tr>
                        <td class="auto-style1">Current Representative</td>
                        <td>
                            <asp:TextBox ID="currentTxt" runat="server" Width="180px" ReadOnly="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td class="auto-style2"></td>
                    </tr>
                    <tr>
                        <td>Current Collection Point</td>
                        <td>
                            <asp:TextBox ID="collectTxt" runat="server" Width="242px" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                </table>

                <div class=" col-sm col-sm-8 radio spaceforDiv">

                            <asp:RadioButtonList ID="collectPoint" runat="server">
                                <asp:ListItem Value="1">Admin Building (9:30am)</asp:ListItem>
                                <asp:ListItem Value="2">Management School (11:00am)</asp:ListItem>
                                <asp:ListItem Value="3">Medical School (9:30am)</asp:ListItem>
                                <asp:ListItem Value="4">Engineering School (11:00am)</asp:ListItem>
                                <asp:ListItem Value="5">Science School (9:30am)</asp:ListItem>
                                <asp:ListItem Value="6">University Hall (11:00am)</asp:ListItem>
                            </asp:RadioButtonList>
                            <br />
                            &nbsp;
                     </div>
                <div class=" col-sm col-sm-8">
                    Department Employees:
                    <br />
                </div>
                            <div class="col-sm-8">
                                <asp:UpdatePanel ID="updGrid" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="repreName" runat="server" AutoGenerateColumns="False" OnRowDataBound="OnRowDataBound" DataValueField="Name"
                                    Width="254px" OnSelectedIndexChanged="repreName_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Employee Name">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="HiddenField1" runat="server" Value='<%# Bind("Empid") %>' />
                                                <asp:Label ID="allempName" runat="server" Text='<%# Bind("Name") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                </div>
                <br />
                <br />
                <table style="margin:3px auto; width:100%">
                    <tr>
                        <td>
                            <asp:Label ID="statuslb" runat="server" Font-Bold="True"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style3"></td>
                        <td>
                            <asp:Button ID="SaveBtn" runat="server" Text="Submit" CssClass="btn btn-sm btn-primary" OnClick="SaveBtn_Click" Width="100px" /></td>
                    </tr>
                </table>
                <br />
            </div>
        </div>
    </div>
</asp:Content>
