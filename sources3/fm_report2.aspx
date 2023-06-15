<%@ Page Language="C#" AutoEventWireup="true" ClientTarget="Uplevel" CodeFile="fm_report2.aspx.cs" Inherits="fm_report2" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style>
         html {
                overflow: -moz-scrollbars-vertical;
        }
    </style>
</head>
<body style="overflow: -moz-scrollbars-vertical;">
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        </CR:CrystalReportSource>
        <CR:CrystalReportViewer ID="crviewer" runat="server" AutoDataBind="true" OnUnload="crviewer_Unload" HyperlinkTarget="_Blank" />
    </div>
    </form>
</body>
</html>
