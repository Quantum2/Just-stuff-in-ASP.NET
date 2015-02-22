<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="stats1.aspx.cs" Inherits="Xbox_Live_Stats.stats1" Async="true" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <p>
    <br />
</p>
<p style="font-weight: bold; font-size: large;">
    Just insert your gamertag and press the button to fetch stats</p>
<p style="font-weight: bold; font-size: large;">
    &nbsp;</p>
<p>
    <asp:Label ID="Label1" runat="server" Text="Gamertag: "></asp:Label>
    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Go !" />
</p>
<p>
    <asp:Label ID="Label2" runat="server" Font-Italic="True"></asp:Label>
</p>
<p>
    &nbsp;</p>
<p>
    <asp:Label ID="Label7" runat="server" Font-Bold="True" Text="Profile Picture: " Visible="False"></asp:Label>
    <asp:Image ID="Image1" runat="server" Height="64px" Visible="False" Width="64px" />
</p>
<p>
    <asp:Label ID="Label3" runat="server" Font-Bold="True" Visible="False"></asp:Label>
</p>
<p>
    <asp:Label ID="Label4" runat="server" Font-Bold="True" Visible="False"></asp:Label>
</p>
<p>
    <asp:Label ID="Label5" runat="server" Font-Bold="True" Visible="False"></asp:Label>
</p>
<p>
    <asp:Label ID="Label6" runat="server" Font-Bold="True" Visible="False"></asp:Label>
</p>
</asp:Content>
