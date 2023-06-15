<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_acc_ap_ho_print.aspx.cs" Inherits="fm_acc_ap_ho_print" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
 <head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server"> 



    <%--    <div class="divheader">Account Payable To HO</div>
        <div class="h-divider"></div>--%>

        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>

        <div class="row navi">
        <div class="clearfix margin-bottom col-md-offset-1 col-md-16">
        <div id="_oReportDiv">
            <%--<rsweb:ReportViewer ID="ReportViewer1" runat="server" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt"  Width="100%" Height="100%" ZoomMode="PageWidth" SizeToReportContent="True">
    <%--            <LocalReport ReportPath="rp_acc_ap_ho.rdlc">
                    <DataSources>
                        <rsweb:ReportDataSource DataSourceId="ObjectDataSource2" Name="DataSet_acc_ap_ho2" />
                    </DataSources>
                </LocalReport>--%>
            <%--</rsweb:ReportViewer>--%>
    <%--        <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData" TypeName="DataSet_acc_ap_ho2TableAdapters.sp_ap_ho_getTableAdapter" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:QueryStringParameter Name="@salespoint_cd" QueryStringField="salespointcd" Type="String" />
                    <asp:QueryStringParameter Name="@start_dt" QueryStringField="start_dt" Type="DateTime" />
                    <asp:QueryStringParameter Name="@end_dt" QueryStringField="end_dt" Type="DateTime" />
                    <asp:QueryStringParameter Name="@user" QueryStringField="user_print" Type="String" />
                    <asp:QueryStringParameter Name="@salesPointName" QueryStringField="salespoint_nm" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>--%>
            <%--<asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetData" TypeName="DataSet_rp_acc_ap_ho2TableAdapters.sp_ap_ho_getTableAdapter"></asp:ObjectDataSource>--%>
            <%--<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData" TypeName="DataSet_rp_acc_ap_hoTableAdapters.sp_ap_ho_getTableAdapter"></asp:ObjectDataSource>--%>
        --%></div>
        </div>
        </div>
    </form> 
</body>
</html>
