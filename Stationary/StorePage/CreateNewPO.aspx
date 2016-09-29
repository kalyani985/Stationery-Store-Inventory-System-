<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="CreateNewPO.aspx.cs" Inherits="StorePage_CreateNewPO" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class ="box">
        <h2><u>New Purchase Order</u></h2>
        <table style="width: 100%;">
            <tr>
                <td style="width: 165px">
                    Supplier Name:
                </td>
                  <td>
                    <asp:DropDownList ID="SupplierDropDownList" runat="server" Width="150px" DataTextField="SupplierName" DataValueField="SupplierCode">
                       <%-- AppendDataBoundItems="true" AutoPostBack="true"--%>
               </asp:DropDownList>
                </td>
            </tr>
            <tr><td style="height: 5px"></td></tr>
            <tr>
                <td style="width: 165px">
                    Order Date:
                </td>
                <td>
                  <asp:TextBox ID="suppDate" runat="server" Width="150px" maxlength="11" ></asp:TextBox> 
                <%--<ajaxToolkit:CalendarExtender ID="CalendarExtender" 
                    runat="server" TargetControlID="suppDate">
                </ajaxToolkit:CalendarExtender>--%>
                </td>
            </tr>
        </table>
        <div class ="gridview">
         <asp:UpdatePanel ID="updGrid" runat="server">
             <ContentTemplate>
                  <asp:GridView ID="newPOGridView" runat="server" AutoGenerateColumns="False" 
                      EmptyDataText = "No Records Selected" OnSelectedIndexChanged="newPOGridView_SelectedIndexChanged"
                      CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
            <Columns>
                <asp:BoundField HeaderText="Item No" DataField="ItemNos" ItemStyle-Width="15%" SortExpression="ItemNos">
                    <ItemStyle Width="15%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Description" DataField="Desc" ItemStyle-Width="25%" SortExpression="Desc">
                    <ItemStyle Width="25%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Current Stock" DataField="CurrStock" ItemStyle-Width="10%" SortExpression="CurrStock">
                    <ItemStyle Width="10%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Quantity" DataField="Qty" ItemStyle-Width="10%" SortExpression="Qty">
                    <ItemStyle Width="10%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Unit Price" DataField="UnitP" ItemStyle-Width="10%" SortExpression="UnitP">
                    <ItemStyle Width="10%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Sub-Total" DataField="SubTot" ItemStyle-Width="10%" SortExpression="SubTot">
                    <ItemStyle Width="10%"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="New Order Qty" DataField="NewQty" ItemStyle-Width="10%" SortExpression="NewQty">
                    <ItemStyle Width="1%"></ItemStyle>
                </asp:BoundField>

                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton ID="addItem" runat="server" Text="Add Qty" CssClass="btn btn-sm btn-primary" OnClick="addItem_Click" ></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
             </ContentTemplate>
         </asp:UpdatePanel>
            </div>
        <div>

            <asp:Button ID="btn1" runat="server" Text="Button" />
            <!--Item-->
         <ajax:ModalPopupExtender ID="mpe1" runat="server"
                TargetControlID="btn1" PopupControlID="popup" CancelControlID="btnCancel1"
                RepositionMode="RepositionOnWindowResizeAndScroll" DropShadow="true"
                PopupDragHandleControlID="panelAddNewTitle"
                BackgroundCssClass="modalBackground">
        </ajax:ModalPopupExtender>
        <asp:Panel ID="popup" runat="server" Style="display: none; background-color: white;"
                ForeColor="Black"
                Width="500" Height="210">
            <asp:Panel ID="panelAddNewTitle" runat="server"
                    Style="cursor: move; font-family: Tahoma; padding: 2px;"
                    HorizontalAlign="Center" BackColor="Blue"
                    ForeColor="White" Height="25">
                    <b>Set Item Order Quantity</b>
                </asp:Panel>
                  <asp:UpdatePanel ID="UpdGrdCharge" runat="server" UpdateMode="Always" ChildrenAsTriggers="true">
                      <ContentTemplate>
                          <div>
                              <asp:Table ID="Table1" runat="server">
                                  <asp:TableRow>
                                      <asp:TableCell>Item Number: </asp:TableCell>
                                      <asp:TableCell> <asp:Label ID="ItmNos" Visible="true" runat="server"></asp:Label></asp:TableCell>
                                  </asp:TableRow>

                                  <asp:TableRow>
                                      <asp:TableCell>Set Order Quantity: </asp:TableCell>
                                      <asp:TableCell>
                                          <asp:TextBox ID="setItmQty" runat="server" maxlength="4"></asp:TextBox></asp:TableCell>
                                  </asp:TableRow>
                              </asp:Table>
                     <asp:RegularExpressionValidator id="RegularExpressionValidator1"
                                ControlToValidate="setItmQty"
                               ValidationExpression="\d+"
                               Display="Static"
                               EnableClientScript="true"
                               ErrorMessage="Please enter only number in Set Order Quantity field."
                               runat="server" ForeColor="Red"/>
                          </div>
                          <div class="modal-footer">
                              <asp:Button ID="btnCancel1" runat="server" Width="70" Text="Cancel" OnClick="btncancel_Click" CausesValidation="false" ValidationGroup="add" CssClass="btn btn-sm btn-primary" Align="right" />
                              <asp:Button ID="btnok" runat="server" Text="Add" OnClick="btnok_Click" Width="70" CausesValidation="false" ValidationGroup="add" CssClass="btn btn-sm btn-primary" />
                          </div>
                      </ContentTemplate>
                  </asp:UpdatePanel>
            </asp:Panel>
        </div>
        <div >
                    <asp:UpdatePanel ID ="updTotal" runat="server">
                        <ContentTemplate>
                              <td><b>Total:  <asp:Label ID="totalLbl" runat="server" Text=""></asp:Label></b></td>
                        </ContentTemplate>
                    </asp:UpdatePanel>
        </div>
        <br />
        <div>
            <asp:Button ID="createBtn" runat="server" Text="Create" CssClass="btn btn-sm btn-primary"
                 OnClick="createBtn_Click" Width="100px"  style="float: right;"/>
        </div>
    </div>
</asp:Content>

