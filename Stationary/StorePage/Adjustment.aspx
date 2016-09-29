<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Adjustment.aspx.cs" Inherits="StorePage_Adjustment" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="container">
    <div class="box">
        <h2><u>Adjustment List</u></h2>
        <div>
            <div>
                <div class="col-md-12 text-center">
                    <asp:Button ID="createBtn" runat="server" Text="Create New Adjustment" CssClass="btn btn-sm btn-primary" OnClick="createBtn_Click" /> </div>
                    <br />
                    <br />
                    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  AllowPaging="True" PageSize="18" OnPageIndexChanging="gridView_PageIndexChanging" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                        <AlternatingRowStyle />
                        <Columns>
                    <asp:BoundField DataField="AdjustmentNumber" HeaderText="Adjustment Number" SortExpression="AdjustmentNumber">
                    </asp:BoundField>
                    <asp:BoundField DataField="Date" HeaderText="Date" SortExpression="Date">
                    </asp:BoundField>

                    <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status">
                    </asp:BoundField>
                            <asp:TemplateField HeaderText="Detail">
                                <ItemTemplate>
                                    <asp:LinkButton ID="detBtn" runat="server" TabIndex="1" Text="View" OnClick="detBtn_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                        <PagerStyle />
                    </asp:GridView>

                    <asp:Button ID="btn1" runat="server" Text="Button" />
                    <asp:Button ID="btn2" runat="server" Text="Button" />
                    <ajax:ModalPopupExtender ID="mpe1" runat="server"
                        TargetControlID="btn1" PopupControlID="popup" CancelControlID="btnCancel1"
                        RepositionMode="RepositionOnWindowResizeAndScroll" DropShadow="true"
                        PopupDragHandleControlID="panelAddNewTitle"
                        BackgroundCssClass="modalBackground">
                    </ajax:ModalPopupExtender>
                    <asp:Panel ID="popup" runat="server" Style="display: none; background-color: white;"
                        ForeColor="Black"
                        Width="800" Height="500">
                        <asp:Panel ID="panelAddNewTitle" runat="server"
                            Style="cursor: move; font-family: Tahoma; padding: 2px;"
                            HorizontalAlign="Center" BackColor="Blue"
                            ForeColor="White" Height="25">
                            <b>Adjustment Voucher Details</b>
                        </asp:Panel>
                        <asp:UpdatePanel ID="UpdGrdCharge" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:Table ID="Table2" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell>Adjustment Number: </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="adNo" Visible="true" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                <asp:GridView ID="adjDetail" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                                <div class="modal-footer"> 
                                    <asp:Table ID="Table1" runat="server"></asp:Table>
                                    <asp:Button ID="btnCancel1" runat="server"
                                        Width="70" Text="Ok"
                                        CausesValidation="false"
                                        ValidationGroup="add" CssClass="btn btn-sm btn-primary" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </asp:Panel>
                </div>
            </div>
        </div>
        </div>    
</asp:Content>

