<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="ViewDisbursement.aspx.cs" Inherits="Department_Representative_ViewDisbursement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 <div class="box">
        <h2><u>Disbursement List</u></h2>
        <div class="spaceforDiv">
            <asp:GridView ID="DisburseGridView" runat="server" AutoGenerateColumns="False" Width="563px" OnPageIndexChanging="DisburseGridView_PageIndexChanging" 
               CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">

                <AlternatingRowStyle />
                <Columns>
                    <asp:BoundField DataField="DFNos" HeaderText="Disbursement No" ItemStyle-Width="25%" SortExpression="DFNos">
                        <ItemStyle Width="25%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="25%" SortExpression="Date">
                        <ItemStyle Width="25%"></ItemStyle>
                    </asp:BoundField>

                    <asp:BoundField HeaderText="Status" DataField="Status" ItemStyle-Width="25%" SortExpression="Status">
                        <ItemStyle Width="25%"></ItemStyle>
                    </asp:BoundField>
                     <asp:TemplateField HeaderText="View Details" >
                   <ItemTemplate>
                      <asp:LinkButton ID="detBtn" runat="server" TabIndex="1" Text="View" OnClick="detBtn_Click"></asp:LinkButton>
                   </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
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
                Width="600" Height="500">
                <asp:Panel ID="panelAddNewTitle" runat="server"
                    Style="cursor: move; font-family: Tahoma; padding: 2px;"
                    HorizontalAlign="Center" BackColor="Blue"
                    ForeColor="White" Height="25">
                    <b>Disbursement Details</b>
                </asp:Panel>
                <asp:UpdatePanel ID="UpdGrdCharge" runat="server">
                    <ContentTemplate>
                        <asp:Table ID="Table2" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>Date: </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="date" Visible="true" runat="server"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>&nbsp;&nbsp;&nbsp;</asp:TableCell>
                                <asp:TableCell>Disbursement List: </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="disList" Visible="true" runat="server"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                            <asp:TableRow>
                                <asp:TableCell>Representative Name: </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="repName" Visible="true" runat="server"></asp:Label>
                                </asp:TableCell>
                                <asp:TableCell>&nbsp;&nbsp;&nbsp;</asp:TableCell>
                                <asp:TableCell>Collection Point: </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="collPoint" Visible="true" runat="server"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>

                        <asp:GridView ID="disDetail" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true" OnPageIndexChanging="disDetail_PageIndexChanging">
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
</asp:Content>
