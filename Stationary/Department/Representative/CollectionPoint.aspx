<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="CollectionPoint.aspx.cs" Inherits="Department_Representative_CollectionPoint" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
        <style type="text/css">
                           body
        {
            font-family: Arial;
            font-size: 10pt;
        }
        td
        {
            cursor: pointer;
        }
        .hover_row
        {
            background-color: #FFFFBF;
        }
        .hidden
     {
         display:none;
     }
                    .auto-style2 {
                        height: 5px;
                    width: 214px;
                }
                .auto-style3 {
                    width: 346px;
                }
                .auto-style4 {
                    width: 214px;
                }
                </style>
        <div class="container-fluid">
        <div class="row">
            <div class="box">     
             <h2> <u>Change Collection Point</u></h2>
                <table>
                    <tr>
                        <td class="auto-style4">Representative Name</td>
                        <td><asp:TextBox ID="currentTxt" runat="server"  Width="240px" Enabled="False"></asp:TextBox></td>
                    </tr>
                    <tr><td class="auto-style2"></td></tr>
                    <tr>
                        <td class="auto-style4">Current Collection Point</td>
                        <td><asp:TextBox ID="collectTxt" runat="server" Width="240px" Enabled="False" ></asp:TextBox> </td>
                    </tr>
                </table>
                <br />
                <asp:RadioButtonList ID="collectPoint" runat="server" OnSelectedIndexChanged="collectPoint_SelectedIndexChanged" >
                    <asp:ListItem Value="1">Administration Building (9:30am)</asp:ListItem>
                    <asp:ListItem Value="2">Medical School (9:30am)</asp:ListItem>
                    <asp:ListItem Value="3">Science School (9:30am)</asp:ListItem>
                    <asp:ListItem Value="4">Engineering School (11:00am)</asp:ListItem>
                    <asp:ListItem Value="5">Management School (11:00am)</asp:ListItem>
                    <asp:ListItem Value="6">University Hospital (11:00am)</asp:ListItem>
                </asp:RadioButtonList>
                     <br />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Collection Point"
                          ControlToValidate="collectPoint" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                         
                     <br />
     
                <div>
                    <table>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="SaveBtn" runat="server" Text="Submit" CssClass="btn btn-sm btn-primary" OnClick="SaveBtn_Click" Width="100px"/>
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style3"><asp:Label ID="collectionStatuslb" runat="server"></asp:Label></td>
                        </tr>
                    </table>
                    </div>
            </div>
            </div>
            </div>
</asp:Content>