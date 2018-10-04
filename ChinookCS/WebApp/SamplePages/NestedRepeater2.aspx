<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NestedRepeater2.aspx.cs" Inherits="WebApp.SamplePages.NestedRepeater2" %>

<%@ Register Src="~/UserControls/MessageUserControl.ascx" TagPrefix="uc1" TagName="MessageUserControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h1>Nested Repeater 2</h1>
    <uc1:MessageUserControl runat="server" ID="MessageUserControl" />
    <asp:Repeater ID="DTORepeater2" runat="server" DataSourceID="DTORepeater2ODS" ItemType="Chinook.Data.DTOs.SupportEmployee">
        <HeaderTemplate>
            <h3>Employee list</h3>
        </HeaderTemplate>
        <ItemTemplate>
            <h4><%# Item.Name %> (<%# Item.ClientCount %>)</h4> <br />
            <asp:Repeater ID="POCORepeater2" runat="server" DataSource="<%# Item.ClientList %>" ItemType="Chinook.Data.POCOs.PlaylistCustomer">
                <ItemTemplate>
                    <%# Item.lastname %> , <%# Item.firstname %> : <%# Item.phone %> <br />
                </ItemTemplate>
            </asp:Repeater>
        </ItemTemplate>

    </asp:Repeater>
    <asp:ObjectDataSource ID="DTORepeater2ODS" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Employee_GetPlaylistCustomer" 
        TypeName="ChinookSystem.BLL.EmployeeController"
        OnSelected="CheckForException"></asp:ObjectDataSource>
</asp:Content>
