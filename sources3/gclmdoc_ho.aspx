﻿<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="gclmdoc_ho.aspx.cs" Inherits="gclmdoc_ho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
   
     <%--<asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
         <asp:ListItem Value="0">INVOICE</asp:ListItem>
         <asp:ListItem Value="1">SUMMARY</asp:ListItem>
         <asp:ListItem Value="2">SUMMARY CASHOUT</asp:ListItem>
         <asp:ListItem Value="3">SUMMARY CNDN</asp:ListItem>
         <asp:ListItem Value="4">PROPOSAL</asp:ListItem>
     </asp:RadioButtonList>
   
     <asp:RadioButtonList ID="rdyear" runat="server" RepeatDirection="Horizontal">
         <asp:ListItem>2016</asp:ListItem>
         <asp:ListItem>2017</asp:ListItem>
         <asp:ListItem>2018</asp:ListItem>
     </asp:RadioButtonList>
   
     <br />--%>
    <br />
    <asp:Label ID="lblClaim" runat="server" Text="Input Claim No (CLOxxxx)"></asp:Label>
    <asp:TextBox ID="txclaim" runat="server"></asp:TextBox>
    <asp:Button ID="btgn" runat="server" OnClick="btgn_Click" Text="Generate Document for CMHO" />
&nbsp;
<%--     <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Generate ALL" Enabled="false" />--%>
</asp:Content>

