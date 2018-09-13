<%@ Page Title="ODS Query" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ODSQuery.aspx.cs" Inherits="WebApp.SamplePages.ODSQuery" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server"> 
    <h1>ODS Query</h1>
    <asp:Label ID="Label1" runat="server" Text="Select an artist"></asp:Label>
    <asp:DropDownList ID="ArtistList" runat="server"></asp:DropDownList>
    <br />
    <asp:LinkButton ID="FetchArtist" runat="server">Fetch</asp:LinkButton>

    <asp:ObjectDataSource ID="ArtistListODS" runat="server"></asp:ObjectDataSource>

</asp:Content>
