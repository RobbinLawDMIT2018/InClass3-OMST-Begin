<%@ Page Title="Ticket Sales - OMST" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PurchaseTickets.aspx.cs" Inherits="OMSTWebsite.Assessments.PurchaseTickets" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <br />
    <div class="row" style="background-color: gold">
        <div class="col-md-12">
            <h1>OMST Ticket Sales</h1>
        </div>
    </div>
    <div class="row">
        <div class="col-md-12">
            <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
        </div>
    </div>
    <div class="row">
        <div class="col-md-3 bg-light border"><h5>1) Select Location</h5> (reveals movies)</div>
        <div class="col-md-3 bg-light border"><h5>2) Select Movie</h5> (reveals show times and ticket selection)</div>
        <div class="col-md-3 bg-light border"><h5>3) Select Show Time</h5> (enables buy button)</div>
        <div class="col-md-3 bg-light border"><h5>4) Select Tickets</h5> Press Buy to purchase tickets</div>
    </div>
    <div class="row">
        <asp:Panel ID="LocationPanel" runat="server" CssClass="col-md-3">
            <h4>Locations</h4>
            <asp:RadioButtonList ID="LocationList" runat="server"
                DataSourceID="LocationListODS"
                DataTextField="DisplayText"
                DataValueField="DisplayValue"
                AutoPostBack="true" OnSelectedIndexChanged="LocationList_SelectedIndexChanged">
            </asp:RadioButtonList>
            <asp:Button ID="Clear" runat="server" Text="Clear"
                class="btn btn-primary" OnClick="Clear_Click" />

            <asp:ObjectDataSource ID="LocationListODS" runat="server"
                OldValuesParameterFormatString="original_{0}"
                SelectMethod="Locations_List"
                TypeName="OMSTSystem.BLL.TicketsController"
                OnSelected="CheckForException"></asp:ObjectDataSource>
        </asp:Panel>
        <asp:Panel ID="MoviePanel" runat="server" Visible="false" CssClass="col-md-3">
            <h4>Movies</h4>
            <asp:RadioButtonList ID="MovieList" runat="server" OnSelectedIndexChanged="MovieList_SelectedIndexChanged"
                AutoPostBack="true">
            </asp:RadioButtonList>
        </asp:Panel>
        <asp:Panel ID="ShowTimesTicketsPanel" runat="server" Visible="false" CssClass="col-md-6">
            <div class="row">
                <div class="col-sm-6">
                    <h4>ShowTimes&nbsp;&nbsp;</h4>

                    <asp:RadioButtonList ID="ShowTimeList" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ShowTimeList_SelectedIndexChanged">
                    </asp:RadioButtonList>
                </div>
                <div class="col-sm-6">
                    <h4>Tickets</h4>

                    <asp:Label ID="Infant" runat="server" Text="Infant" Width="100px" />&nbsp;&nbsp;
                    <asp:DropDownList ID="InfantTickets" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangeTotal" />&nbsp;&nbsp;
                    <asp:Label ID="InfantPrice" runat="server" />
                    <hr />

                    <asp:Label ID="Child" runat="server" Text="Child" Width="100px" />&nbsp;&nbsp;
                    <asp:DropDownList ID="ChildTickets" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangeTotal" />&nbsp;&nbsp;
                    <asp:Label ID="ChildPrice" runat="server" />
                    <hr />

                    <asp:Label ID="Teen" runat="server" Text="Teen" Width="100px" />&nbsp;&nbsp;
                    <asp:DropDownList ID="TeenTickets" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangeTotal" />&nbsp;&nbsp;
                    <asp:Label ID="TeenPrice" runat="server" />
                    <hr />

                    <asp:Label ID="Adult" runat="server" Text="Adult" Width="100px" />&nbsp;&nbsp;
                    <asp:DropDownList ID="AdultTickets" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangeTotal" />&nbsp;&nbsp;
                    <asp:Label ID="AdultPrice" runat="server" />
                    <hr />

                    <asp:Label ID="Senior" runat="server" Text="Senior" Width="100px" />&nbsp;&nbsp;
                    <asp:DropDownList ID="SeniorTickets" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ChangeTotal" />&nbsp;&nbsp;
                    <asp:Label ID="SeniorPrice" runat="server" />
                    <hr />

                    <asp:Label ID="Label1" runat="server" Text="Premium:" />&nbsp;&nbsp;
                    <asp:Label ID="TicketPremium" runat="server" />

                    <asp:Label ID="Total" runat="server" Text="Total:"></asp:Label>&nbsp;&nbsp;
                    <asp:Label ID="TotalPrice" runat="server" />
                    <hr />

                    <asp:Button ID="Buy" runat="server" Text="Buy" class="btn btn-success" Enabled="false" OnClick="Buy_Click" />
                </div>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
