<%@ Page Title="" Language="C#" MasterPageFile="~/eis2/eis2.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="eis2_Default" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <rsweb:ReportViewer ID="MyReportViewer" runat="server" Width="750" Height="100%" AsyncRendering="true" SizeToReportContent="true"></rsweb:ReportViewer>
</asp:Content>
    