<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acc_ap_ho.aspx.cs" CodeBehind="fm_acc_ap_ho.aspx.cs" Inherits="fm_acc_ap_ho" Debug="true"%>


<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>

<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .main-content #mCSB_2_container{
            min-height: 520px;
        }
    </style>
    <script src="js/jquery.min.js"></script>
    <script type="text/javascript">


<%--        function GetUrlAjax() {
            var urlStr;
            $.ajax({
                type: "POST",
                url: "fm_acc_ap_ho.aspx/GetUrlData",
                data: "{}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (response) {
                    //////var data = $.parseJSON(response.d);
                    //$("#output").append("<li>success " + response.d + "<li>");
                    urlStr = response.d;
                    //return urlStr;
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    $("#output").append("<li>error " + response.d + "<li>");
                },
                async: false
            });
            return urlStr;
        };

        //function Show(strl, strp, strn, field1, field2, field3, par1, par2, par3) {
        function PrintApHo() {
            debugger;

            var l = GetUrlAjax();
            var p = '80';
            //var l = 'localhost';
            //var p = '9999';
            var n = 'fm_acc_ap_ho_print.aspx';
            var f1 = 'strSalespoint';
            var f2 = 'strDtfrom';
            var f3 = 'strDtto';
            var p1 = document.getElementById('<%=cbsalespoint.ClientID%>').value;
            var p2 = document.getElementById('<%=dtfrom.ClientID%>').value;
            var p3 = document.getElementById('<%=dtto.ClientID%>').value;
            var Geturl = 'http://' + l + ':' + p + '/' + n + '?' + f1 +'='+ p1 +'&'+ f2 +'='+ p2 +'&'+ f3 +'='+ p3;

            OpenWin = window.open(Geturl, "_blank");
        }--%>

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Account Payable To HO</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Print Account Payable To HO</div>
            </div>
            &nbsp
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-10 drop-down">
                   <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                   </asp:DropDownList>  
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Start Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dtfrom" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtfrom_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtfrom_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtfrom">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">End Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dtto" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dtto_CalendarExtender" CssClass="date" runat="server" BehaviorID="dtto_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtto">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
        </div>

    </div> 

    <div class="row navi">
        <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
            <asp:Button ID="printreport" runat="server" Text="Print" CssClass="btn-info btn btn-print" 
                OnClick="btprintapho_Click" />
                <%--OnClientClick="javascript:PrintApHo();" />--%>
            <%--<asp:Button ID="printreport" runat="server" Text="Print" CssClass="btn-info btn btn-print" />--%>
            <br/>
        </div>
        <br />
    </div>
    
    <br />

    <div id="output">
        <%--tes--%>
    </div>

</asp:Content>
