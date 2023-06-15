﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_report4.aspx.cs" Inherits="fm_report4" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <CR:CrystalReportSource ID="crsource" runat="server"></CR:CrystalReportSource>
            <CR:CrystalReportViewer ID="crv" runat="server" AutoDataBind="true" />
        </div>
    </form>
</body>
</html>
