<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="StationaryLogin.aspx.cs" Inherits="Account_StationaryLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <br /><br /><br /><br />
        <div class="col-lg-12 text-center">
            <img src ="../Images/banner.png" />

        <br />
      <div class="col-md-4"></div>
        <div class="col-md-4">
                <div class="panel panel-default">
                    <div class="panel-heading" style="font-size:larger; text-align:center"> 
                    <%--<span class="glyphicon glyphicon-lock"></span--%><b> Login</b></div>
                    <asp:PlaceHolder runat="server" ID="ErrorMessage" Visible="false">
                        <p class="text-danger">
                            <asp:Literal runat="server" ID="FailureText" />
                        </p>
                    </asp:PlaceHolder>
                    <div class="panel-body">
                    <div class="form-group">
                        <div >
                            <label for="empId" class="empIdLogin"> Employee ID </label>
                            <table>
                                <tr>
                                    <td style="width: 20px"><span aria-hidden="true" class="glyphicon glyphicon-user"/></td>
                                    <td><asp:TextBox runat="server" ID="empidTxt" CssClass="form-control" Placeholder="eg. 12345" CausesValidation="false" Width="231px" MaxLength="5"/></td>
                                </tr>
                            </table>                            
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="empidTxt"
                                CssClass="text-danger" ErrorMessage="Employee ID field is required " />
                            
                            <br /><asp:RegularExpressionValidator id="RegularExpressionValidator"
                                ControlToValidate="empidTxt"
                               ValidationExpression="\d+"
                               Display="Static"
                               EnableClientScript="true"
                               ErrorMessage="Invalid Employee ID."
                               runat="server" CssClass="text-danger"/>
                        
                        </div>
                        <div>
                            <label for="password" class="youpasswd"> Your password </label>
                            <table>
                                <tr>
                                    <td style="width: 20px"><span aria-hidden="true" class="glyphicon glyphicon-lock"/></td>
                                    <td><asp:TextBox runat="server" ID="pwTxt" TextMode="Password" CssClass="form-control" Placeholder="eg. X8df!90EO" Width="233px"/></td>
                                </tr>
                            </table>                                
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="pwTxt" CssClass="text-danger" ErrorMessage="The password field is required." />
                        </div>
                    </div>
                    </div>

                    <div class="form-group">
                        <div >
                            <asp:Button runat="server" OnClick="LogIn" Text="Login" CssClass="btn btn-sm btn-primary btn-block" Width="277px" Height="39px" Font-Size="Medium"/>
                        </div>
                    </div>
                    </div>
                </div>
    </div>
</asp:Content>

