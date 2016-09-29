<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="CreateRequisition.aspx.cs" Inherits="Department_CreateRequisition" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
  <style>
.div1 {
    width: 980px;
    height: 50px;
    border: 1px solid black;
    box-sizing: border-box;
    padding:10px;
}
        .auto-style1 {
            width: 748px;
        }
    </style>
    <div class="box">
        <h2 class="medium"><u>Create New Requisition</u></h2>
        <br />

        Please select from Item List as below:
        <asp:UpdatePanel ID="updReq" runat="server">
            <ContentTemplate>
                <div class="div1">
                <asp:Table ID="Table" runat="server" Width="685px" cellspacing="10">
                    <asp:TableRow>
                        <asp:TableCell>Category</asp:TableCell>

                        <asp:TableCell>
                            <asp:DropDownList ID="ddlCatType" runat="server" OnSelectedIndexChanged="ddlCatType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:TableCell>
                          <asp:TableCell>Item</asp:TableCell>
                        <asp:TableCell>
                            <asp:DropDownList ID="ddlCat" runat="server"></asp:DropDownList>
                        </asp:TableCell>
                        <asp:TableCell>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</asp:TableCell>
                        <asp:TableCell>Quantity</asp:TableCell>
                        <asp:TableCell>
                            <asp:TextBox ID="textQty" runat="server" MaxLength="4"></asp:TextBox>
                        </asp:TableCell>
                        <asp:TableCell>
                            <asp:Button ID="btnOK" runat="server" Text="Add" OnClick="btnOK_Click" CssClass="btn btn-sm btn-primary" Width="100px"/>
                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </div>
                        <asp:RegularExpressionValidator runat="server" ControlToValidate="textQty"
                   ValidationExpression="\d+"                 
                   Display="Static"
                   EnableClientScript="true"
                   ErrorMessage ="Please enter only number in Quantity field." ForeColor="Red"/><br />
                <div>

                    <asp:GridView ID="gvReq" runat="server" AutoGenerateColumns="False" Width="784px"  ShowHeaderWhenEmpty="true" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <Columns>
                            <asp:BoundField DataField="CategoryName" HeaderText="Category Name" />
                            <asp:BoundField DataField="ItemName" HeaderText="Item Name" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                            <asp:BoundField DataField="UOM" HeaderText="UOM" />
                            <asp:BoundField DataField="ItemNumber" HeaderText="ItemNumber" Visible="False" />
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
                </div>

            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
                            <table>
                        <tr>
                            <td class="auto-style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                            <td><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" Width="100px" CssClass="btn btn-sm btn-primary"/></td>
                        </tr>
                    </table>

    </div>


</asp:Content>

