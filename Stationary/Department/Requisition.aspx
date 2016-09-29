<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Requisition.aspx.cs" Inherits="Department_Requisition" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
 <div class="container">
            <div class="box">
                <div>
                    <h2 class="medium"><u>Requisition History</u></h2>
                     <div class="col-md-12 text-center">
                    <asp:Button ID="createBtn" runat="server" Text="Create New Requisition" CssClass="btn btn-sm btn-primary" OnClick="createBtn_Click" />
                </div>
                    <br /><br />
                    <asp:GridView ID="requisitionGridView" runat="server" AllowPaging="True" PageSize="18" OnPageIndexChanging="gridView_PageIndexChanging" AutoGenerateColumns="False"
                        CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">

                        <AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>
                        <EmptyDataRowStyle ForeColor="Red" />
                        <EmptyDataTemplate>
                            No records found.  

                        </EmptyDataTemplate>
                        <Columns>
                            <asp:BoundField DataField="Date" HeaderText="Date" ItemStyle-Width="15%" SortExpression="Date">
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="RequisitionNumber" HeaderText="Req. Nos." ItemStyle-Width="15%" SortExpression="RequisitionNumber">
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="15%" SortExpression="Status">
                                <ItemStyle Width="15%"></ItemStyle>
                            </asp:BoundField>
                            <asp:BoundField DataField="Reason" HeaderText="Reason" ItemStyle-Width="50%" SortExpression="Reason">

                                <ItemStyle Width="30%"></ItemStyle>

                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Detail">
                                <ItemTemplate>
                                    <asp:LinkButton ID="detBtn" runat="server" TabIndex="1" Text="View" OnClick="detBtn_Click"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>

                        <PagerStyle CssClass="pgr"></PagerStyle>
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
                        Width="600" Height="700">
                        <asp:Panel ID="panelAddNewTitle" runat="server"
                            Style="cursor: move; font-family: Tahoma; padding: 2px;"
                            HorizontalAlign="Center" BackColor="Blue"
                            ForeColor="White" Height="25">
                            <b>Requisition Details</b>
                        </asp:Panel>
                        <asp:UpdatePanel ID="UpdGrdCharge" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                            <ContentTemplate>
                                <asp:Table ID="Table2" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell>Requisiton Number: </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="reqNo" Visible="true" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                                <asp:GridView ID="reDetail" runat="server" style="align-content:center" Width="490px" Height="180px" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                                    <Columns>
                                    </Columns>
                                </asp:GridView>      
                                    <div class="modal-footer" >
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
</asp:Content>
