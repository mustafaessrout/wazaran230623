<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookupinvreturn.aspx.cs" Inherits="lookupinvreturn" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=1, user-scalable=no" />

    <title>Wazaran System</title>
    <link href="css/lightbox.css" rel="stylesheet" />
    <script src="css/lightbox-plus-jquery.min.js"></script>
    <link rel="stylesheet" href="css/anekabutton.css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <link href="css/styles.css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css" href="css/default.css" />
    <link rel="stylesheet" type="text/css" href="css/component.css" />
    <link rel="stylesheet" href="css/jquery.scrollbar.css" />
    <script src="js/jquery.scrollbar.js"></script>
    <script src="js/modernizr.custom.js"></script>

    <!--custom css-->
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/animate.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/responsive.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet" />
    <link href="css/jquery.mCustomScrollbar.min.css" rel="stylesheet" />



    <!--custom js-->
    <script src="js/index.js"></script>

    <script src="js/sweetalert.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>

    <script src="js/modernizr.js"></script>
    <script src="js/jquery.floatThead.js"></script>
    <script src="js/jquery.mCustomScrollbar.concat.min.js"></script>



    <style type="text/css">
        .messagealert {
            width: 100%;
            position: fixed;
            top: 0px;
            z-index: 100000;
            padding: 0;
            font-size: 15px;
        }
    </style>
    <script type="text/javascript">
        function ShowMessage(message, messagetype) {
            var cssclass;
            switch (messagetype) {
                case 'Success':
                    cssclass = 'alert-success'
                    break;
                case 'Error':
                    cssclass = 'alert-danger'
                    break;
                case 'Warning':
                    cssclass = 'alert-warning'
                    break;
                default:
                    cssclass = 'alert-info'
            }
            <%--$('#<%=Page.ClientID %>').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');--%>

        }
        function PopupCenter(url, title, w, h) {
            // Fixes dual-screen position                         Most browsers      Firefox
            var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
            var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

            var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
            var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

            var left = ((width / 2) - (w / 2)) + dualScreenLeft;
            var top = ((height / 2) - (h / 2)) + dualScreenTop;
            var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

            // Puts focus on the newWindow
            if (window.focus) {
                newWindow.focus();
            }
        }
        function MsgWarning(sCap, sMsg) {
            sweetAlert(sCap, sMsg, 'warning');
        }

        function MsgSuccess(sCap, sMsg) {
            sweetAlert(sCap, sMsg, 'success');
        }
    </script>

    <style>
        .marquee {
            background: #fff;
            box-shadow: 0 0 1px #777;
            width: 800px;
            margin: 2px auto;
            white-space: nowrap;
            overflow: hidden;
            box-sizing: border-box;
            border: 1px green none;
        }

            .marquee span {
                display: inline-block;
                padding-left: 100%;
                text-indent: 0;
                border: 1px red none;
                animation: marquee 15s linear infinite;
            }

                .marquee span:hover {
                    animation-play-state: paused
                }

        /* Make it move */
        @keyframes marquee {
            0% {
                transform: translate(0, 0);
            }

            100% {
                transform: translate(-100%, 0);
            }
        }

        /* Make it pretty */
        .microsoft {
            padding-left: 1.5em;
            position: relative;
            font: 16px Arial,'Segoe UI', Tahoma, Helvetica, Sans-Serif;
        }

            /* ::before was :before before ::before was ::before - kthx */
            .microsoft:before, .microsoft::before {
                z-index: 2;
                content: '';
                position: absolute;
                top: -1em;
                left: -1em;
                width: .5em;
                height: .5em;
                /*box-shadow: 1.0em 1.25em 0 #F65314,
        		1.6em 1.25em 0 #7CBB00,
        		1.0em 1.85em 0 #00A1F1,
        		1.*/
                box-shadow: 1.85em 0 #FFBB00;
            }

            .microsoft:after, .microsoft::after {
                z-index: 1;
                content: '';
                position: absolute;
                top: 0;
                left: 0;
                width: 2em;
                height: 2em;
                /* background-image: linear-gradient(90deg, white 70%, rgba(255,255,255,0));*/
                background-image: none;
            }

        /* Style the links */
        .vanity {
            color: #333;
            text-align: center;
            font: .75em 'Segoe UI', Tahoma, Helvetica, Sans-Serif;
        }

            .vanity a, .microsoft a {
                color: #1570A6;
                transition: color .5s;
                text-decoration: none;
            }

                .vanity a:hover, .microsoft a:hover {
                    color: #F65314;
                }

        /* Style toggle button */
        .toggle {
            display: block;
            margin: 2em auto;
        }
    </style>
    <style>
        #showmessage {
            position: fixed;
            top: 50%;
            left: 50%;
            width: 500px;
            height: 500px;
            margin-top: -250px; /* Half the height */
            margin-left: -250px;
        }
    </style>
    <style type="text/css">
        a:visited {
            color: white;
            font-family: Tahoma,Verdana;
            font-weight: normal;
            text-decoration: none;
        }

        a:active {
            text-decoration: none;
        }

        .jajarangenjang {
            width: 50%;
            /*height: 75px;*/
            background-color: #07466c;
            color: white;
            font-weight: bolder;
            font-family: Calibri;
            -webkit-transform: skew(170deg);
            -moz-transform: skew(170deg);
            -o-transform: skew(170deg);
            transform: skew(170deg);
            padding-bottom: 5px;
            margin-bottom: 0px;
            border-radius: 5px;
            /*padding-bottom:0.5em;*/
            padding-top: 0.2em;
            margin-left: 0px;
            padding-left: 5px;
            /*outline-offset:1px;*/
        }
    </style>


    <script>
        (function ($) {

            //cache nav
            var nav = $("#topNav");

            //add indicator and hovers to submenu parents
            nav.find("li").each(function () {
                if ($(this).find("ul").length > 0) {
                    $("<span>").text("^").appendTo($(this).children(":first"));

                    //show subnav on hover
                    $(this).mouseenter(function () {
                        $(this).find("ul").stop(true, true).slideDown();
                    });

                    //hide submenus on exit
                    $(this).mouseleave(function () {
                        $(this).find("ul").stop(true, true).slideUp();
                    });
                }
            });
        })(jQuery);
    </script>
    <script>
        function openreport(url) {
            window.open(url, "myrep");
        }

        function openreport2(url) {
            window.open(url, "myrep");
        }

        function popupwindow(url) {
            mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=900,height=600");
            mywindow.moveTo(400, 200);
        }

    </script>
    <script>
        function openreport1(url1, url2) {
            window.open(url1, "Report View");

            window.open(url2, "Report Print");
        }


    </script>
    <script type="text/javascript">
            var lab = 'cd';
            function start() {
                //Enter time in sec
                var amount = getCookie("cdclosing");
                displayCountdown(setCountdown(0, 0, amount), lab);
            }
            loaded(lab, start);
            var pageLoaded = 0;
            window.onload = function () { pageLoaded = 1; }
            function loaded(i, f) {
                if (document.getElementById && document.getElementById(i) != null)
                    f();
                else if (!pageLoaded)
                    setTimeout('loaded(\'' + i + '\',' + f + ')', 100);
            }
            function setCountdown(hour, min, second) {
                if (hour > 0)
                    min = min * hour * 60;
                c = setC(min, second);
                return c;
            }
            function setC(min, second) {
                if (min > 0)
                    second = min * 60 * second;
                return Math.floor(second);
            }
            function displayCountdown(countdn, cd) {
                if (countdn < 0) {
                    //window.location = "fm_closingdaily.aspx";
                }
                else {
                    var secs = countdn % 60;
                    if (secs < 10)
                        secs = '0' + secs;
                    var countdn1 = (countdn - secs) / 60;
                    var mins = countdn1 % 60;
                    if (mins < 10)
                        mins = '0' + mins;
                    countdn1 = (countdn1 - mins) / 60;
                    var hours = countdn1 % 24;
                    document.getElementById(cd).innerHTML = hours + ' : ' + mins + ' : ' + secs;
                    setTimeout('displayCountdown(' + (countdn - 1) + ',\'' + cd + '\');', 999);
                }
            }
            function getCookie(cname) {
                var name = cname + "=",
                    ca = document.cookie.split(';'),
                    i,
                    c,
                    ca_length = ca.length;
                for (i = 0; i < ca_length; i += 1) {
                    c = ca[i];
                    while (c.charAt(0) === ' ') {
                        c = c.substring(1);
                    }
                    if (c.indexOf(name) !== -1) {
                        return c.substring(name.length, c.length);
                    }
                }
                return "";
            }

            function setCookie(variable, value, expires_seconds) {
                var d = new Date();
                d = new Date(d.getTime() + 1000 * expires_seconds);
                document.cookie = variable + '=' + value + '; expires=' + d.toGMTString() + ';';
            }
            function toTitleCase(str) {
                return str.replace(/\w\S*/g, function (txt) { return txt.charAt(0).toUpperCase() + txt.substr(1).toLowerCase(); });
            }
    </script>
    <script type="text/javascript">
            function CustSelected(sender, e) {
                $get('<%=hdcust.ClientID%>').value = e.get_value();

            }
            function InvoiceSelected(sender, e) {

                $get('<%=hdfInv.ClientID%>').value = e.get_value();

            }
          
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <%--<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>--%>

        <asp:ToolkitScriptManager ID="ScriptManagerTK" runat="server" EnablePageMethods="true">
            <Scripts>
                <asp:ScriptReference Path="js/AutoLostFocus.js" />
            </Scripts>
        </asp:ToolkitScriptManager>

        <div class="containers bg-white">
            <div class="divheader">List Of Invoice Can be Full Returned</div>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <%--<div class="col-md-3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:RadioButton ID="rbInvoiceDate" Checked="true" GroupName="search" runat="server" Text="Search By Inoice Date From" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2">

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtInvoiceFromDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtInvoiceFromDate">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">To</label>
                    <div class="col-md-2">

                        
                                <asp:TextBox ID="dtInvoiceToDate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="CalendarExtender2" runat="server" Format="d/M/yyyy" TargetControlID="dtInvoiceToDate">
                                </asp:CalendarExtender>
                            
                    </div>--%>
                </div>
            </div>
            <%--            <div class="form-group">
                <div class="row">
                    <div class="col-md-3">

                        <asp:RadioButton ID="rbInvoiceType" Checked="true"  GroupName="search" runat="server" Text="Search By Invoice Canceled Status" />

                    </div>
                    <div class="col-md-6  drop-down">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbInvoiceType" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbInvoiceType_SelectedIndexChanged"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
            </div>--%>

            <div class="form-group">
                <div class="row">
                    <div class="col-md-3">

                        <asp:RadioButton ID="rbInvoice" GroupName="search"  runat="server" Text="Search By Invoice" />

                    </div>

                    <div class="col-md-6">

                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>

                                <div class="input-group full">
                                    <div>
                                        <asp:HiddenField ID="hdfInv" runat="server" />
                                        <asp:TextBox ID="txtInovie" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>

                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" CompletionListElementID="divw" runat="server"
                                            TargetControlID="txtInovie" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                            MinimumPrefixLength="1" OnClientItemSelected="InvoiceSelected" ServiceMethod="GetCompletionInvoiceFullReturnList" UseContextKey="True"
                                            ContextKey="true">
                                        </asp:AutoCompleteExtender>
                                    </div>

                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-3">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:RadioButton ID="rbCustomerSearch" GroupName="search" runat="server" Text="Search By Customer" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                    <div class="col-md-6">

                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdcust" runat="server" />
                                <div class="input-group full">
                                    <div>
                                        <asp:TextBox ID="txcust" runat="server" CssClass="form-control" Height="100%" Width="100%"></asp:TextBox>

                                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txcust_AutoCompleteExtender" CompletionListElementID="divw" runat="server"
                                            TargetControlID="txcust" EnableCaching="false" FirstRowSelected="false" CompletionInterval="0" CompletionSetCount="1"
                                            MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" UseContextKey="True"
                                            ContextKey="true">
                                        </asp:AutoCompleteExtender>
                                    </div>

                                </div>

                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>

                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-3">

                        <asp:RadioButton ID="rbApprovedInv" GroupName="search" runat="server" Text="Show Approved Inv" />

                    </div>
                     
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <asp:LinkButton ID="btShowData" runat="server" CssClass="btn btn-primary" OnClick="btShowData_Click">Show Data</asp:LinkButton>
                    <asp:LinkButton ID="btnAllData" runat="server" CssClass="btn btn-primary" OnClick="btnAllData_Click">All Data</asp:LinkButton>
                </div>

            </div>

            <div class="h-divider"></div>
            <div class="row ">

                <div class="margin-bottom clearfix">
                    <div class="col-md-12">
                        <div class="overflow-y" style="max-height: 400px">
                            <asp:GridView ID="grd" EmptyDataText="New Records Found" CssClass="table table-striped table-fix mygrid" PageSize="10" AllowPaging="true" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging" CellPadding="2" OnPageIndexChanging="grd_PageIndexChanging" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Inv No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("inv_no")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Man No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblmanual_no" runat="server" Text='<%# Eval("manual_no")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblinv_dt" runat="server" Text='<%# Eval("inv_dt")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcust_desc" runat="server" Text='<%# Eval("cust_desc")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salesman">
                                        <ItemTemplate>
                                            <asp:Label ID="lblemp_desc" runat="server" Text='<%# Eval("emp_desc")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amt">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbalance" runat="server" Text='<%# Eval("balance")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Order No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblso_cd" runat="server" Text='<%# Eval("so_cd")%>'></asp:Label>
                                        </ItemTemplate>

                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" />
                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div class="clearfix well well-sm danger text-white">
                    <div class="col-md-12">
                        1. Invoice must be un paid.<br />
                        2. Invoice not yet received back by Back Office.		
                    </div>

                </div>

            </div>

        </div>
    </form>
</body>
</html>
