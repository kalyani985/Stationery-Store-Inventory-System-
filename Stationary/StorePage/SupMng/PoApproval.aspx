<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="PoApproval.aspx.cs" Inherits="StorePage_SupMng_PoApproval" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
    <div class="box">
        <h2><u>Purchase order Approval</u></h2>
     <asp:GridView ID="poappGirdView" runat="server" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
                <Columns>
                <asp:TemplateField HeaderText="Date" SortExpression="Date">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("Date") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Bind("Date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PONo" SortExpression="PONo">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("PONo") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label2" runat="server" Text='<%# Bind("PONo") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" SortExpression="Status">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("Status") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label3" runat="server" Text='<%# Bind("Status") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                     <asp:TemplateField HeaderText="Remarks" SortExpression="Remarks">
                    <EditItemTemplate>
                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("Remarks") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label4" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Reject Reason (Optional)">
                <ItemTemplate>
                    <asp:TextBox ID="txtReason" runat="server" Height="22px" Width="165px"></asp:TextBox>
                </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Approve/Reject">
                   <ItemTemplate>
                      <asp:LinkButton ID="appBtn" runat="server" TabIndex="1" Text="Approve" OnClick="appBtn_Click"></asp:LinkButton> /
                      <asp:LinkButton ID="rejBtn" runat="server" TabIndex="1" Text="Reject" OnClick="rejBtn_Click"></asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="View Details" >
                   <ItemTemplate>
                      <asp:LinkButton ID="detBtn" runat="server" TabIndex="1" Text="View" OnClick="detBtn_Click"></asp:LinkButton>
                   </ItemTemplate>
                </asp:TemplateField>
                </Columns>
            </asp:GridView>  
           <asp:Button ID="btn1" runat="server" Text="Button" />
           <asp:Button ID="btn2" runat="server" Text="Button" />          
        <!--Details-->
         <ajax:ModalPopupExtender ID="mpe1" runat="server"
                TargetControlID="btn1" PopupControlID="popup" CancelControlID="btnCancel1"
                RepositionMode="RepositionOnWindowResizeAndScroll" DropShadow="true"
                PopupDragHandleControlID="panelAddNewTitle"
                BackgroundCssClass="modalBackground">
        </ajax:ModalPopupExtender>
        <asp:Panel ID="popup" runat="server" Style="display: none; background-color: white;"
                ForeColor="Black"
                Width="600" Height="550">
            <asp:Panel ID="panelAddNewTitle" runat="server"
                    Style="cursor: move; font-family: Tahoma; padding: 2px;"
                    HorizontalAlign="Center" BackColor="Blue"
                    ForeColor="White" Height="25">
                    <b>Purchase Order Details</b>
                </asp:Panel>
                  <asp:UpdatePanel ID="UpdGrdCharge" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                    <ContentTemplate>
                         <asp:Table ID="Table2" runat="server">
                                    <asp:TableRow>
                                        <asp:TableCell>PO No: </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="poNo1" Visible="true" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                             <asp:TableRow>
                                        <asp:TableCell>Supplier Name: </asp:TableCell>
                                        <asp:TableCell>
                                            <asp:Label ID="suppName" Visible="true" runat="server"></asp:Label>
                                        </asp:TableCell>
                                    </asp:TableRow>
                                </asp:Table>
                        <asp:GridView ID="adDetail" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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
</asp:Content>

