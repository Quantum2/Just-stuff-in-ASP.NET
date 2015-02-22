<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Xbox_Live_Stats._Default" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div class="jumbotron">
        <h1>Xbox Live gamertag stats</h1>
        <p class="lead">Just a simple stats and gamertag signature generator I made on my free time...</p>
        <p><a href="stats1" class="btn btn-primary btn-lg">Take me there &raquo;</a></p>
    </div>

    <div class="row">
        <div class="col-md-4">
            <h2>Steam gamercard generator</h2>
            <p>
                Works similar to the Xbox Live stats, but this one is for the master race :P</p>
            <p>
                <a class="btn btn-default" href="stats2">Lets go &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Moar stuff</h2>
            <p>
                TO DO</p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301949">Learn more &raquo;</a>
            </p>
        </div>
        <div class="col-md-4">
            <h2>Even more stuff</h2>
            <p>
                TO DO</p>
            <p>
                <a class="btn btn-default" href="http://go.microsoft.com/fwlink/?LinkId=301950">Learn more &raquo;</a>
            </p>
        </div>
    </div>

</asp:Content>
