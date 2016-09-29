<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Retrieval.aspx.cs" Inherits="StorePage_Retrieval" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
      <div class="box">
        <div class="col-md-12 text-center"></div>
        <h2><u>Retrieval Form</u></h2>
            <div>Date :
            <asp:TextBox ID="dateTxt" runat="server"  Width="130px" ReadOnly="true" Text=""></asp:TextBox>
                <br />
            </div>
        <div>

            <asp:GridView ID="GridView1" runat="server" PageSize="20" AutoGenerateColumns="False" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" >
                <Columns>

                    <asp:BoundField DataField="ItemNos" HeaderText="" ItemStyle-Width="1%" SortExpression="ItemNos">
                        <ItemStyle Width="1%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Bin" HeaderText="Bin#" ItemStyle-Width="10%" SortExpression="Bin">
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ItemName" HeaderText="Item Name" ItemStyle-Width="15%" SortExpression="ItemName">
                        <ItemStyle Width="20%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="QtyNeed" HeaderText="Req'd Qty" ItemStyle-Width="10%" SortExpression="QtyNeed">
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="ActualQty" HeaderText="Actual Qty" ItemStyle-Width="10%" SortExpression="ActualQty">
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DeptCode" HeaderText="Department" ItemStyle-Width="15%" SortExpression="DeptCode">
                        <ItemStyle Width="15%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="DeptNeed" HeaderText="Dept Need" ItemStyle-Width="15%" SortExpression="DeptNeed">
                        <ItemStyle Width="15%"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Show" HeaderText="" ItemStyle-Width="1%">
                        <ItemStyle Width="1%"></ItemStyle>
                    </asp:BoundField>

                    <asp:TemplateField HeaderText="Qty Disb" ItemStyle-Width="10%">
                        <ItemTemplate>

                            <asp:TextBox ID="SetQty_Retr" runat="server" Width="30px"></asp:TextBox>

                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>

                    <asp:TemplateField ShowHeader="False" ItemStyle-Width="8%">
                        <ItemTemplate>

                            <asp:ImageButton ID="btnSetQty_Retr" runat="server" OnClick="btnSetQty_Retr_Click" ImageAlign="Middle" ImageUrl="~/Images/save.png"></asp:ImageButton>

                        </ItemTemplate>
                        <ItemStyle Width="10%"></ItemStyle>
                    </asp:TemplateField>

                </Columns>
            </asp:GridView>
        </div>
                <div style="display:block;float:right;">
                    <asp:Button ID="processBtn" runat="server" OnClick="processBtn_Click" Text="Process" Width="100px"  CssClass="btn btn-sm btn-primary" />
                </div>
        </div>
</asp:Content>

