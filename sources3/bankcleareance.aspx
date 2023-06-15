<%@ Page Language="C#" AutoEventWireup="true" CodeFile="bankcleareance.aspx.cs" Inherits="bankcleareance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <%--     <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
     
    
    
    
   

    <script src="js/index.js"></script>--%>

    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert.min.js"></script>
    <link href="css/font-face/khula.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <style>
        /*#form1 {
            height: 100%;
            width: 100%;
            display: flex;
            justify-content: center;
            align-content: center;
        }*/

        #mydiv {
            min-width: 600px;
            max-width: 1000px;
            border: 1px solid #ccc;
            background-color: #f3f3f3;
            padding: 20px 20px 20px 20px;
        }

        /*.sweet-alert {
            position: fixed;
            z-index: 99999999;
            margin: 0 !important;
            background: #fff;
            top: 25%;
            left: 20%;
            padding: 10px 20px;
            box-shadow: 1px 3px 5px rgba(0,0,0,.3);
            width: 770px;
            border-radius: 5px;
        }

           .sweet-alert fieldset input {
                width: 100%;
                border-radius: 5px;
                box-shadow: none;
                border: 1px solid #a09696;
                padding: 3px 5px 4px;
            }

            .sweet-alert .sa-confirm-button-container {
                text-align: center;
            }

                .sweet-alert .sa-confirm-button-container .confirm {
                    display: inline-block;
                    background-color: rgb(51, 122, 183) !important;
                    border: none;
                    box-shadow: none !important;
                    padding: 5px;
                    color: #fff;
                    border-radius: 3px;
                    width: 100px;
                }

            .sweet-alert .sa-error-container {
                display: flex;
                margin-top: 10px;
            }

                .sweet-alert .sa-error-container > * {
                    color: #d9534f;
                    font-weight: bold;
                }

                .sweet-alert .sa-error-container .icon {
                    padding-right: 7px;
                }*/
    </style>

    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="mydiv">
            <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server"></asp:ToolkitScriptManager>
            <div class="divheader">Bank Transfer / Cheque Clearance</div>

            <div class="divheader subheader subheader-bg text-bold" style="font-size: 14px; margin-bottom: 10px !important;">Bank Transfer / Cheque Detail</div>
            <div class="margin-bottom">
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua">Customer</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lbcust" runat="server" CssClass="padding-top-4 block"></asp:Label>
                    </div>
                </div>
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua text-red">Transfer/Cheque No.</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lbdepositno" runat="server" CssClass="padding-top-4 block text-bold text-red"></asp:Label>
                    </div>
                </div>
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua">Account No.</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lbbankaccountno" runat="server" CssClass="padding-top-4 block text-bold text-red"></asp:Label>
                    </div>
                </div>
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua">Bank Name</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lbbankname" runat="server" CssClass="padding-top-4 block text-bold text-red"></asp:Label>
                    </div>
                </div>
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua">Type</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lbcashouttype" runat="server" CssClass="padding-top-4 block text-bold text-red"></asp:Label>
                    </div>
                </div>
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua">Vendor</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lbcashoutvendor" runat="server" CssClass="padding-top-4 block text-bold text-red"></asp:Label>
                    </div>
                </div>
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua">Amount</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lbamount" runat="server" CssClass="padding-top-4 block text-bold text-red"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="divheader subheader subheader-bg text-bold" style="font-size: 14px; margin-bottom: 10px !important;">Payment Details</div>
            <div class="margin-bottom">
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua padding-top-4">Payment No</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lblPaymentNo" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="margin-bottom">
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua padding-top-4">Payment Mode</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lblPaymentMode" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="margin-bottom">
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua padding-top-4">Customer</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lblCust" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="margin-bottom">
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua padding-top-4">Salesman</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lblEmp" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </div>
            <div class="margin-bottom">
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua padding-top-4">Download</label>
                    <div class="col-sm-8">

                        <a href="N/A" title="Download" target="_blank" runat="server" id="aDownload">
                            <asp:Label ID="lblDownload" runat="server" Text=""></asp:Label>
                        </a>
                    </div>
                </div>
            </div>
            <div class="margin-bottom">
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua padding-top-4">Payment Status (BT/CQ)</label>
                    <div class="col-sm-8 drop-down">

                        <asp:DropDownList ID="ddlPaymentAttribute" CssClass="form-control" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            
            <div class="divheader subheader subheader-bg text-bold" style="font-size: 14px; margin-bottom: 10px !important;">Deposit In</div>
            <div class="margin-bottom">
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua padding-top-4">Bank Account</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbbankto" runat="server" OnSelectedIndexChanged="cbbankto_SelectedIndexChanged" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>

            <div class="divheader subheader subheader-bg text-bold" style="font-size: 14px; margin-bottom: 10px !important;">&nbsp;</div>
            <div class="margin-bottom">
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua">Cleareance Date</label>
                    <div class="col-sm-8 drop-down-date drop-down-date-sm">
                        <asp:TextBox ID="dtclear" runat="server" CssClass="ro form-control input-sm"></asp:TextBox>
                        <asp:CalendarExtender ID="dtclear_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtclear" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </div>
                </div>
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua padding-top-4">Clearance By</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lbcreated" runat="server" CssClass="padding-top-4 block text-primary text-bold"></asp:Label>
                    </div>
                </div>
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua padding-top-4">Remark</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txremark" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="margin-bottom">
                <div class="clearfix">
                    <label class="col-sm-4 titik-dua padding-top-4">Upload</label>
                    <div class="col-sm-8">
                        <asp:FileUpload ID="upl" runat="server" />
                    </div>
                </div>
            </div>

            <div class="clearfix">
                <div class="col-xs-12">Do <span class="auto-style1"><strong>Clearance</strong></span> this Deposit Bank </div>
            </div>
            <div class="navi margin-top">
                <asp:Button ID="btcancel" Style="display: none" runat="server" Text="Reject" CssClass="btn btn-danger btn-delete" OnClick="btcancel_Click" />
                <asp:Button ID="btyes" runat="server" Text="CLEAREANCE NOW" CssClass="btn-success btn btn-add" OnClick="btyes_Click" />
            </div>
        </div>

    </form>
</body>
</html>
