<%@ Page Title="New Tickets" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewTickets.aspx.cs" Inherits="OMSTWebApp.Assessments.NewTickets" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
        <h1 class="page-header">These are tickets added by running your code during this assessment.</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-md-8">
        <h3>New Tickets Added</h3>
            <asp:ListView ID="NewTicketList" runat="server" DataSourceID="NewTicketListODS">
                <EmptyDataTemplate>
                    <table runat="server" class="table">
                        <tr>
                            <td>No data was returned. No new tickets have been added by your assessment.</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>

                <ItemTemplate>
                    <tr>
                        <td>
                            <asp:Label Text='<%# Eval("TicketID") %>' runat="server" ID="TicketIDLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("ShowTimeID") %>' runat="server" ID="ShowTimeIDLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("TicketCategoryID") %>' runat="server" ID="TicketCategoryIDLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("TicketPrice") %>' runat="server" ID="TicketPriceLabel" /></td>
                        <td>
                            <asp:Label Text='<%# Eval("TicketPremium") %>' runat="server" ID="TicketPremiumLabel" /></td>

                    </tr>
                </ItemTemplate>
                <LayoutTemplate>
                    <table runat="server">
                        <tr runat="server">
                            <td runat="server">
                                <table runat="server" id="itemPlaceholderContainer" class="table table-hover">
                                    <tr runat="server">
                                        <th runat="server">TicketID</th>
                                        <th runat="server">ShowTimeID</th>
                                        <th runat="server">TicketCategoryID</th>
                                        <th runat="server">TicketPrice</th>
                                        <th runat="server">TicketPremium</th>
                                    </tr>
                                    <tr runat="server" id="itemPlaceholder"></tr>
                                </table>
                            </td>
                        </tr>
                        <tr runat="server">
                            <td runat="server" style="text-align: center; background-color: #CCCCCC; font-family: Verdana, Arial, Helvetica, sans-serif; color: #000000;">
                                <asp:DataPager runat="server" ID="DataPager1">
                                    <Fields>
                                        <asp:NextPreviousPagerField ButtonType="Button" ShowFirstPageButton="True" ShowLastPageButton="True"></asp:NextPreviousPagerField>
                                    </Fields>
                                </asp:DataPager>
                            </td>
                        </tr>
                    </table>
                </LayoutTemplate>

            </asp:ListView>
        </div>
        <div class="col-md-4">
            <h3>Ticket Categories</h3>
            <asp:GridView ID="TicketCategoryList" runat="server"
                AutoGenerateColumns="False"
                DataSourceID="TicketCategoryListODS"
                GridLines="Horizontal" BorderStyle="None" CssClass="table">
                <Columns>
                    <asp:BoundField DataField="Description"
                        HeaderText="Category"></asp:BoundField>
                    <asp:BoundField DataField="TicketPrice"
                        HeaderText="Price"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <asp:ObjectDataSource ID="NewTicketListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="Tickets_NewlyAddedTickets"
        TypeName="OMSTSystem.BLL.TicketsController"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="TicketCategoryListODS" runat="server"
        OldValuesParameterFormatString="original_{0}"
        SelectMethod="TicketCategory_List"
        TypeName="OMSTSystem.BLL.TicketsController"></asp:ObjectDataSource>
</asp:Content>
