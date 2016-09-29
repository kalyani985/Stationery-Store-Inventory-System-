<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ProcessDO.aspx.cs" Inherits="StorePage_ProcessDO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="box">
        <h2><u>Delivery Order</u></h2>
        <asp:UpdatePanel ID="UpdatePanelNew" runat="server">
            <ContentTemplate>
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                    <AlternatingRowStyle />
                    <Columns>

                        <asp:BoundField DataField="PONo" HeaderText="PO No" HtmlEncode="true" ReadOnly="True" SortExpression="PONo" />
                        <asp:BoundField DataField="Date" HeaderText="Date" HtmlEncode="true" SortExpression="Date" />
                        <asp:BoundField DataField="SupplierName" HeaderText="Supplier Name" HtmlEncode="true" SortExpression="SupplierName" />
                        <asp:BoundField DataField="DoNo" HeaderText="DO No" SortExpression="DoNo" />

                        <asp:TemplateField HeaderText="Status" SortExpression="Status">
                            <EditItemTemplate>
                                <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField ItemStyle-Width="30px" HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="lnkEdit" runat="server" Text="Edit" OnClick="Edit"></asp:LinkButton>
                            </ItemTemplate>
                            <ItemStyle Width="30px" />
                        </asp:TemplateField>
                    </Columns>

                </asp:GridView>



                <ajax:ModalPopupExtender ID="popup" runat="server" DropShadow="false"
                    PopupControlID="Panel1" TargetControlID="lnkFake"
                    BackgroundCssClass="modalBackground">
                </ajax:ModalPopupExtender>

                <asp:Panel ID="Panel1" runat="server" Style="display: none; background-color: white;" CssClass="modalPopup" ForeColor="Black"
                    Width="550" Height="250">
                    <asp:Panel ID="panelAddNewTitle" runat="server"
                        Style="cursor: move; font-family: Tahoma; padding: 2px;"
                        HorizontalAlign="Center" BackColor="Blue"
                        ForeColor="White" Height="25">
                        <b>Update Delivery Order</b>
                    </asp:Panel>



                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="PO NO"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="PONumber" Width="40px" MaxLength="5" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label2" runat="server" Text="Date "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="DO No. "></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtDoNumber" runat="server"></asp:TextBox>
                            </td>
                        </tr>
                        </table>
                    <div class="modal-footer">
                                <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="Save" fore-color="blue" Font-Bold="true" Font-Size="Medium" />
           
                                <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="return Hidepopup()"
                                    fore-color="blue" Font-Bold="true" Font-Size="Medium" />
      </div>

                </asp:Panel>


                <asp:LinkButton ID="lnkFake" runat="server"></asp:LinkButton>


            </ContentTemplate>

            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="GridView1" />
                <asp:AsyncPostBackTrigger ControlID="btnSave" />
            </Triggers>

        </asp:UpdatePanel>
    </div>
</asp:Content>

