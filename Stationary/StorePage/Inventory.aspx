<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeFile="Inventory.aspx.cs" Inherits="StorePage_Inventory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" Runat="Server">
     <div class="box">
        <h2><u>Inventory Summary</u></h2>
        <div>
            <div>
                <div class="col-md-5">
                    Category:
                    <asp:DropDownList ID="catDrpDwn" runat="server" Width="200px" DataTextField="CatagoryDesc" DataValueField="CatagoryDesc"
                        OnSelectedIndexChanged="catDrpDwn_SelectedIndexChanged" AppendDataBoundItems="true" AutoPostBack="true">
                        <asp:ListItem>ALL</asp:ListItem>
                    </asp:DropDownList>
                    <asp:Label ID="inventStatuslb" runat="server"></asp:Label>
                </div>
                <div class="col-md-5">
                    <asp:TextBox ID="txtSearch" runat="server" Width="300px" Placeholder="Item Description" maxlength="20"></asp:TextBox></div>
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-sm btn-primary" OnClick="btnSearch_Click" Width="100px" />
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" OnRowCommand="GridView1_Redirect" CssClass="mGrid" PagerStyle-CssClass="pgr" AlternatingRowStyle-CssClass="alt" AllowPaging="true" OnPageIndexChanging="gridView_PageIndexChanging">
                    <Columns>
                        <asp:BoundField DataField="BinNumber" HeaderText="Bin#" />
                        <asp:BoundField DataField="ItemNumber" HeaderText="Item Code" />
                        <asp:BoundField DataField="Description" HeaderText="Item Description" />
                        <asp:BoundField DataField="UnitOfMeasure" HeaderText="Unit of Measure" />
                        <asp:BoundField DataField="ReorderLevel" HeaderText="Reorder Level" />
                        <asp:BoundField DataField="ReorderQuantity" HeaderText="Reorder Quantity" />
                        <asp:BoundField DataField="Date" HeaderText="Lastest Update" />
                        <asp:BoundField DataField="BalanceAmount" HeaderText="Balance" />

                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="stockBtn" runat="server" TabIndex="1" Text="Stock Card" CommandName="Redirect"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

