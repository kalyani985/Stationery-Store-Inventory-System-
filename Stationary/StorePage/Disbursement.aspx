<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Disbursement.aspx.cs" Inherits="StorePage_Disbursement" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="box">
       <h2><u>Disbursement List</u></h2>
         <div class="col-lg-1 spaceforDiv" style="width: 100%;">
             Department:
             <asp:DropDownList ID="deptDropDown" runat="server" Height="30px" Width="16%"  DataTextField="Name" DataValueField="DeptCode"
                     OnSelectedIndexChanged="deptDropDown_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                 <asp:ListItem Value="">ALL</asp:ListItem>
                 <asp:ListItem Value="ARTS">Arts and Science Dept</asp:ListItem>
                 <asp:ListItem Value="BUSS">Business School Dept</asp:ListItem>
                 <asp:ListItem Value="COMM">Commerce Dept</asp:ListItem>
                 <asp:ListItem Value="CPSC">Computer Science</asp:ListItem>
                 <asp:ListItem Value="ENGG">Engineering Dept</asp:ListItem>
                 <asp:ListItem Value="ENGL">English Dept</asp:ListItem>
                 <asp:ListItem Value="MEDI">Medical Dept</asp:ListItem>
                 <asp:ListItem Value="REGR">Registrar Dept</asp:ListItem>
                 <asp:ListItem Value="SCIE">Science Dept</asp:ListItem>
                 <asp:ListItem Value="STORE">Warehouse Store</asp:ListItem>
                 <asp:ListItem Value="ZOOL">Zoology Dept</asp:ListItem>
             </asp:DropDownList>
         </div>

         <div class="spaceforDiv">
             <asp:GridView ID="DisburseGridView" runat="server" AutoGenerateColumns="False"
                 OnPageIndexChanging="DisburseGridView_PageIndexChanging"
                 CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">

                 <Columns>
                     <asp:BoundField DataField="DFNos" HeaderText="Disbursement No" SortExpression="DFNos">
                     </asp:BoundField>
                     <asp:BoundField DataField="deptC" HeaderText="Department Code"  SortExpression="deptC">
                     </asp:BoundField>

                     <asp:BoundField DataField="deptName" HeaderText="Deparment Name"  SortExpression="deptName">
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
                 Width="550" Height="500">
                 <asp:Panel ID="panelAddNewTitle" runat="server"
                     Style="cursor: move; font-family: Tahoma; padding: 2px;"
                     HorizontalAlign="Center" BackColor="Blue"
                     ForeColor="White" Height="25">
                     <b>Disbursement Details</b>
                 </asp:Panel>
                 <asp:UpdatePanel ID="UpdGrdCharge" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                     <ContentTemplate>
                          <asp:Table ID="Table2" runat="server">
                            <asp:TableRow>
                                <asp:TableCell>Date: </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="date" Visible="true" runat="server"></asp:Label>
                                </asp:TableCell>
                                </asp:TableRow>
                               <asp:TableRow>
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
                            </asp:TableRow>
                               <asp:TableRow>
                                <asp:TableCell>Collection Point: </asp:TableCell>
                                <asp:TableCell>
                                    <asp:Label ID="collPoint" Visible="true" runat="server"></asp:Label>
                                </asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                         <asp:GridView ID="disDetail" runat="server" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt">
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

