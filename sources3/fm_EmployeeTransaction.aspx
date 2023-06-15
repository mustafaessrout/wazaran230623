<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_EmployeeTransaction.aspx.cs" Inherits="fm_EmployeeTransaction" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <script src="admin/js/bootstrap.min.js"></script>
    <script type="text/javascript">

      
    </script>
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
          <%--  $(document).ready(function () {
                $("#<%=dtTrxnDate.ClientID%>").value = new Date;
            function updateHiddenField() {
                var dat = $("#<%=dtTrxnDate.ClientID%>").value;
                $("hddtTrxnDate").Val(dat);
}--%>

        }

    </script>
    <style>
        .col-sm-1half, .col-sm-10half {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
        }

        @media (min-width: 768px) {
            .col-sm-1half, .col-sm-10half {
                float: left;
            }

            .col-sm-1half {
                width: 12.2733%;
            }

            .col-sm-10half {
                width: 87.7%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        <h4 class="jajarangenjang">Employee Transaction</h4>
    </div>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">

                <div style="height: 70px">
                    <div class="clearfix form-group col-sm-4 no-padding-right">
                        <label class="control-label col-sm-3">Employee :</label>
                        <div class="col-sm-9 drop-down ">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:HiddenField ID="hdids" runat="server" />
                                    <asp:DropDownList ID="ddlEmp" runat="server" AutoPostBack="True" CssClass="form-control" AppendDataBoundItems="true" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>

                            </asp:UpdatePanel>
                        </div>
                    </div>



                    <div class="clearfix form-group col-sm-4 no-padding-right">
                        <label class="control-label col-sm-3">Transaction Date :</label>
                        <div class="col-sm-9 drop-down-date">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server"  >
                                <ContentTemplate>
                                    <asp:TextBox ID="dtTrxnDate" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeydown="return false;"></asp:TextBox>
                                    <ajaxToolkit:CalendarExtender CssClass="date" ID="dtTrxnDate_CalendarExtender" runat="server" TargetControlID="dtTrxnDate" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                    </ajaxToolkit:CalendarExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>


                    <div class="clearfix form-group col-sm-4 no-padding-right">
                        <label class="control-label col-sm-3">Transaction Type :</label>
                        <div class="col-sm-9 drop-down">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlEmpTrxnType" runat="server" CssClass="form-control" AppendDataBoundItems="true" >
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>

                <div style="height: 70px">

                    <div class="clearfix form-group col-sm-4 no-padding-right">
                        <label class="control-label col-sm-3">Reference No :</label>
                        <div class="col-sm-9">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txRefNo" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

 


                    <div class="clearfix form-group col-sm-4 no-padding-right">
                        <label class="control-label col-sm-3  ">Amount :</label>
                        <div class="col-sm-9">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server" >
                                <ContentTemplate>
                                    <asp:TextBox ID="txAmt" runat="server" CssClass="form-control" Height="100%" AutoPostBack ="true"  CausesValidation="true"   OnTextChanged="ddlVat_SelectedIndexChanged"></asp:TextBox>
                                    <ajaxToolkit:FilteredTextBoxExtender ID="txAmt_FilteredTextBoxExtender" runat="server"
                                        TargetControlID="txAmt"
                                        FilterType="Custom, Numbers"
                                        ValidChars="." />
                                    <asp:RegularExpressionValidator ID="txAmt_Regex" runat="server"  ValidationExpression="(((\d+\.\d{2})?))$"
                                        ErrorMessage="Please Enter a Valid Amount as 1.00 or 1.25!" ControlToValidate="txAmt"  ForeColor="Red" Style="padding-top: 5px; display: block;" />
                                    <triggers>
                                    <asp:AsyncPostBackTrigger ControlID ="txAmt"  CausesValidation="true"  EventName="OnTextChanged"/>
                                    </triggers>
                                   

                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>



                    <div class="clearfix form-group col-sm-4 no-padding-right">
                        <label class="control-label col-sm-3">Vat Included :</label>
                        <div class="col-sm-9 drop-down">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                     <asp:HiddenField ID="hdvat" runat="server" />
                                    <asp:DropDownList ID="ddlVat" runat="server" CssClass="form-control" AppendDataBoundItems="true" AutoPostBack="true" CausesValidation="true" OnSelectedIndexChanged="ddlVat_SelectedIndexChanged">
                                        <asp:ListItem>Yes</asp:ListItem>
                                        <asp:ListItem>No</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:Label ID="lbvat" runat="server" Style="font-weight: 700; padding-top:5px; padding-left:17px; display:block;" Text="Vat = 0.00" ForeColor="Blue" CausesValidation="true" Visible="false"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                    </div>

                </div>


                <div style="height: 70px">
                    <div class="clearfix form-group col-sm-4 no-padding-right">
                        <label class="control-label col-sm-3">Reason :</label>
                        <div class="col-sm-9 drop-down">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlReason" runat="server" CssClass="form-control" AppendDataBoundItems="true">
                                        <asp:ListItem>Select</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>


                    <div class="clearfix form-group col-sm-8 no-padding-right ">
                        <label class="control-label col-sm-1half">Remark :</label>
                        <div class="col-sm-10half">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txRemark" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>

                <div class="clearfix text-right col-sm-12 no-padding-right">
                    <div class="col-md-12" style="text-align: center">
                        <asp:Button ID="btnNew" runat="server" CssClass="btn btn-warning submit" Text="NEW" OnClick="btnNew_Click" Visible="false" />
                        <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-warning submit" Text="SUBMIT" OnClick="btnSubmit_Click" />
                        <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-info print" Text="PRINT" OnClick="btnPrint_Click" Visible="false" />
                        <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info print" Text="REPORT" OnClick="btnReport_Click" />

                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
