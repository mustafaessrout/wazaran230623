<%@ Page Language="C#" AutoEventWireup="true" CodeFile="cashamountclosingadv.aspx.cs" Inherits="cashamountclosingadv" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Khula" rel="stylesheet" />
    <link href="https://fonts.googleapis.com/css?family=Montserrat" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
    <script src="js/index.js"></script>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_cashamountclosing.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }


        $("#form1").find('input, select, a').filter(':visible:enabled:first').focus();
    </script>
    <style>
        #form1 {
            display: flex;
            justify-content: center;
            align-content: center;
            position: absolute;
            top: 0;
            left: 0;
            width: 100%;
        }

        #mydiv {
            border: 1px solid #ccc;
            background-color: #f3f3f3;
            padding: 5px 20px 20px 20px;
            width: 70%;
            min-width: 500px;
        }

        .danger {
            margin-left: -21px;
            margin-right: -21px;
        }

            .danger > .row {
                padding-bottom: 15px;
                padding-top: 15px;
                margin-left: 0;
                margin-right: 0;
                color: #000;
                font-weight: 100;
            }

        #mydiv .control-label {
            position: relative;
            line-height: 15px;
            padding-top: 0;
            padding-right: 10px;
            font-size: 12px;
        }

        .control-label:after {
            position: absolute;
            content: ":";
            top: 0;
            right: 0;
        }

        input {
            color: #555;
        }

        .text-bold {
            font-weight: bold !important;
        }

        .margin-bottom-5 {
            margin-bottom: 5px !important;
        }

        .input-sm {
            height: auto;
            padding: 3px 5px;
            font-size: 12px;
            line-height: normal;
            border-radius: 3px;
        }
    </style>
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>

    <script>
        function v_sendmessage(phoneno, msg) {

            var DTO = { 'phone': phoneno, 'body': msg };
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: 'https://eu36.chat-api.com/instance103604/sendMessage?token=kkfkbdouwv4ecmgk',
                data: JSON.stringify(DTO),
                datatype: 'json',
                success: function (result) {
                    //do something
                    //window.alert('SUCCESS = ' + result.phone);
                    //console.log(result);
                },
                error: function (xmlhttprequest, textstatus, errorthrown) {
                    //sweetAlert(textstatus);
                    //console.log("error: " + errorthrown);
                }
            });//end of $.ajax()

        }

        function v_sendfile(phoneno, msg, filename) {

            var DTO = { 'phone': phoneno, 'body': msg, 'filename': filename };
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                url: 'https://api.chat-api.com/instance103604/sendFile?token=kkfkbdouwv4ecmgk',
                data: JSON.stringify(DTO),
                datatype: 'json',
                success: function (result) {
                    //do something
                    //    window.alert('SUCCESS = ' + result.phone);
                    //console.log(result);
                },
                error: function (xmlhttprequest, textstatus, errorthrown) {
                    //     sweetAlert(textstatus);
                    //console.log("error: " + errorthrown);
                }
            });//end of $.ajax()
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <div id="mydiv">
            <div class="divheader">CASHIER ADVANCE CLOSING</div>

            <div class="container-fluid danger">
                <div class="row">
                    <div>
                        <label class="control-label col-xs-2">Cash Closing No </label>
                        <div class="col-xs-6">
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                <ContentTemplate>
                                    <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbchclosingno" runat="server" Font-Bold="True"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>
                    <div class="row clearfix">
                        <div class="col-xs-10">
                            <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                                <ContentTemplate>

                                    <asp:GridView ID="grdcsh" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="margin-bottom margin-top table table-hover mygrid">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbcashier_cd" runat="server" Text='<%# Eval("cashier_id") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="System Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbcashier_dt" runat="server" Text='<%# Eval("cashier_dt","{0:d}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Cashier Info">
                                                <ItemTemplate><%# Eval("emp_nm") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="totamt">
                                                <ItemTemplate><%# Eval("totamt") %></ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Upload">
                                                <ItemTemplate>
                                                    <asp:FileUpload ID="fupl" CssClass="form-control" runat="server" /></ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <EditRowStyle CssClass="table-edit" />
                                        <FooterStyle CssClass="table-footer" />
                                        <HeaderStyle CssClass="table-header" />
                                        <PagerStyle CssClass="table-page" />
                                        <RowStyle />
                                        <SelectedRowStyle BackColor="#8aa3e8" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                                    </asp:GridView>

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>

            <div class="container-fluid">
                <div class="row navi">                    
                    <asp:LinkButton ID="btsave" OnClick="btsave_Click" runat="server" CssClass="btn btn-success btn-sm" OnClientClick="ShowProgress();"><span class="glyphicon glyphicon-save">Save</span></asp:LinkButton>
                    <asp:LinkButton ID="btclose" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" OnClick="btclose_Click" runat="server"><span class="glyphicon glyphicon-alert">Close</span></asp:LinkButton>
                </div>
            </div>
        </div>
    </form>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>
