<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="PO.aspx.cs" Inherits="StorePage_PO" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="box">
        <h2><u>Purchase Order History</u></h2>
       <div class="col-md-12 text-center">
         <asp:Button ID="createBtn" runat="server" Text="Create New Purchase Order" CssClass="btn btn-sm btn-primary" OnClick="createBtn_Click"/> <br /> <br />

        </div>
         <asp:GridView ID="dgvShowAllPO" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">

<AlternatingRowStyle CssClass="alt"></AlternatingRowStyle>

             <Columns>
                 <asp:BoundField DataField="poNo" HeaderText="PO No" ItemStyle-Width="10%" SortExpression="poNo">
                    <ItemStyle Width="10%"></ItemStyle>
                </asp:BoundField>
                 <asp:BoundField DataField="date" HeaderText="Order Date" ItemStyle-Width="15%" SortExpression="date">
                    <ItemStyle Width="15%"></ItemStyle>
                </asp:BoundField>
                 <asp:BoundField DataField="supplierCode" HeaderText="Supplier Code" ItemStyle-Width="10%" SortExpression="supplierCode">
                    <ItemStyle Width="10%"></ItemStyle>
                </asp:BoundField>
                 <asp:BoundField DataField="totalAmount" HeaderText="Total Amount" ItemStyle-Width="10%" SortExpression="totalAmount">
                    <ItemStyle Width="10%"></ItemStyle>
                </asp:BoundField>

                 <asp:BoundField DataField="status" HeaderText="Status" ItemStyle-Width="20%" SortExpression="status">
                    <ItemStyle Width="20%"></ItemStyle>
                </asp:BoundField>
                 <asp:TemplateField HeaderText="Detail">
                     <ItemTemplate>
                         <asp:LinkButton ID="detBtn" runat="server" TabIndex="1" Text="View" OnClick="detBtn_Click"></asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton ID="btnDelete" runat="server"  onClick ="deleteBtn">Delete</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton ID="btnCancel"  onClick ="cancelBtn" runat="server"  >Cancel</asp:LinkButton>
                     </ItemTemplate>
                 </asp:TemplateField>
                 <asp:TemplateField>
                     <ItemTemplate>
                         <asp:LinkButton ID="btnSend" onClick ="sendBtn" runat="server"  >Send</asp:LinkButton>
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
                 Width="500" Height="500">
                 <asp:Panel ID="panelAddNewTitle" runat="server"
                     Style="cursor: move; font-family: Tahoma; padding: 2px;"
                     HorizontalAlign="Center" BackColor="Blue"
                     ForeColor="White" Height="25">
                     <b>Puchase Order Details</b>
                 </asp:Panel>
                 <asp:UpdatePanel ID="UpdGrdCharge" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                     <ContentTemplate>
                         <asp:Table ID="Table2" runat="server">
                             <asp:TableRow>
                                 <asp:TableCell>Supplier Name:  </asp:TableCell>
                                 <asp:TableCell>
                                     <asp:Label ID="supName" Visible="true" runat="server"></asp:Label>
                                 </asp:TableCell>
                             </asp:TableRow>
                             <asp:TableRow>
                                 <asp:TableCell>PO No: </asp:TableCell>
                                 <asp:TableCell>
                                     <asp:Label ID="purNo" Visible="true" runat="server"></asp:Label>
                                 </asp:TableCell>
                             </asp:TableRow>
                         </asp:Table>
                         <asp:GridView ID="poDetail" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                             <Columns>
                             </Columns>
                         </asp:GridView>
                         </div>
                         <br />
                         <div>
                             <table style="width: 678px">
                                 <tr>
                                     <td align="right" class="auto-style7"><b>&nbsp;Total Price: 
                                     </b></td>
                                     <td><b>$
                                     <asp:Label ID="totalLabel" runat="server"></asp:Label></b></td>
                                 </tr>
                             </table>
                         </div>
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
</asp:Content>

