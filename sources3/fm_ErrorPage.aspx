<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_ErrorPage.aspx.cs" Inherits="fm_ErrorPage" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <div id="notfound">
			<div class="notfound">
				<div class="notfound-404">
					<h1>Oops!</h1>
					<h2><asp:Label ID="lblError" Text="" Style="color: red;" runat="server"></asp:Label></h2>
				</div>
				<asp:HyperLink runat="server" NavigateUrl="~/Default_2.aspx">
					<p>Go TO Homepage </p>
				</asp:HyperLink>
			</div>
		</div>
    </div>
</asp:Content>

