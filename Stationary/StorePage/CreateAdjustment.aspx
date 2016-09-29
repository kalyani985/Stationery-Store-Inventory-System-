<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="CreateAdjustment.aspx.cs" Inherits="StorePage_CreateAdjustment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
          <style>
.div1 {
    width: 1100px;
    height: 90px;
    border: 1px solid black;
    box-sizing: border-box;
    padding:10px;
}
        .auto-style1 {
            width: 715px;
        }
 table {
    border-collapse: collapse;
                width: 1049px;
            }

td {
    padding-top: .2em;
    padding-bottom: .2em;
}
    </style>
    <div class="box">
        <h2 class="medium"><u>Create New Adjustment Voucher</u></h2>
        <asp:UpdatePanel ID="updReq" runat="server">
            <ContentTemplate>
                    <asp:Table ID="Table" runat="server" Width="547px" CellSpacing="10">
                        <asp:TableRow>
                            <asp:TableCell>Adjustment No  </asp:TableCell>
                            <asp:TableCell>                                
                                <asp:TextBox ID="adNo" Visible="true" runat="server" Width="130px"  Placeholder="eg. 111/1111/11" maxlength="12"></asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;Date</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="date" Visible="true" runat="server" Width="130px" maxlength="11" ></asp:TextBox>
                            </asp:TableCell>
                        </asp:TableRow></asp:Table>
                                        <br />
        Please select from Item List as below:
                <div class="div1">
                <asp:Table ID="Table1" runat="server" Width="1044px" CellSpacing="10">
                        <asp:TableRow>                       
                            <asp:TableCell>Category</asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="ddlCatType" runat="server" OnSelectedIndexChanged="ddlCatType_SelectedIndexChanged" AutoPostBack="true" Width="130px" ></asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;Item</asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="ddlCat" runat="server" Width="280px"></asp:DropDownList>
                            </asp:TableCell>
                        </asp:TableRow>
                        <asp:TableRow>
                            <asp:TableCell>Quantity</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="textQty" runat="server" Width="130px" maxlength="3"> </asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;Reason</asp:TableCell>
                            <asp:TableCell>
                                <asp:TextBox ID="textReason" runat="server" Width="130px" Placeholder="eg. Free Gift" maxlength="30"> </asp:TextBox>
                            </asp:TableCell>
                            <asp:TableCell>Adjustment</asp:TableCell>
                            <asp:TableCell>
                                <asp:DropDownList ID="addOrDeduct" runat="server" Width="130px">
                                    <asp:ListItem>Add</asp:ListItem>
                                    <asp:ListItem>Remove</asp:ListItem>
                                </asp:DropDownList>
                            </asp:TableCell>
                            <asp:TableCell><asp:Button ID="btnOK" runat="server" Text="Add" OnClick="btnOK_Click" CssClass="btn btn-sm btn-primary" Width="100px"/></asp:TableCell>
                        </asp:TableRow>
                    </asp:Table> 
                    </div>       
                                  <asp:RegularExpressionValidator id="RegularExpressionValidator1"
                                ControlToValidate="textQty"
                               ValidationExpression="\d+"
                               Display="Static"
                               EnableClientScript="true"
                               ErrorMessage="Please enter only number in Quantity field.."
                               runat="server" ForeColor="Red"/><br />            
                    <asp:GridView ID="gvReq" runat="server" AutoGenerateColumns="False" Width="784px" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:BoundField DataField="CategoryName" HeaderText="CategoryName" />
                            <asp:BoundField DataField="ItemNo" HeaderText="ItemNo" />
                            <asp:BoundField DataField="Description" HeaderText="Description" />
                            <asp:BoundField DataField="StockBalance" HeaderText="StockBalance" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                            <asp:BoundField DataField="Reason" HeaderText="Reason" />
                            <asp:BoundField DataField="Adjustment" HeaderText="Adjustment" />
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkedit" runat="server" OnClick="lnkedit_Click">Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkdelete" runat="server" OnClick="lnkdelete_Click">Delete</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
                    <table>
                        <tr>
                            <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn btn-sm btn-primary" Width="100px" style="float: right;"/></td>
                        </tr>
                    </table>
    </div> 
</asp:Content>

