<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_AdvancePettyCashRAWReport.aspx.cs" Inherits="fm_AdvancePettyCashRAWReport" %>

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

        }

    </script>
    <style>
        .col-sm-1half, .col-sm-10half {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
        }

        .col-sm-1half, .col-sm-8half {
            position: relative;
            min-height: 1px;
            padding-right: 15px;
            padding-left: 15px;
        }

        @media (min-width: 768px) {
            .col-sm-8half, .col-sm-10half {
                float: left;
            }

            .col-sm-8half {
                width: 69.5%;
            }


            .col-sm-10half {
                width: 87.7%;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        <h4 class="jajarangenjang">Advance Petty Cash RAW Report</h4>
    </div>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row ">
            <div class="clearfix  center">

                <div class="clearfix form-group col-sm-4 no-padding-right">
                    <label class="control-label col-sm-3">Date From :</label>
                    <div class="col-sm-9 drop-down-date">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtDateFrom" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeydown="return false;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="date" ID="dtDateFrom_CalendarExtender" runat="server" TargetControlID="dtDateFrom" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>


                <div class="clearfix form-group col-sm-4 no-padding-right">
                    <label class="control-label col-sm-3">Date To :</label>
                    <div class="col-sm-9 drop-down-date">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtDateTo" runat="server" CssClass="form-control" AutoCompleteType="Disabled" onkeydown="return false;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="date" ID="dtDateTo_CalendarExtender" runat="server" TargetControlID="dtDateTo" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                </ajaxToolkit:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>

            </div>


            <div class="clearfix center">

                <div class=" navi row margin-bottom space-between ">

                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-info print" Text="Emolyee Advance Petty Cash RAW" OnClick="btnShow_Click" />
                      <asp:Button ID="Button1" runat="server" CssClass="btn btn-info print" Text="Cash IN Cash OUT RAW" OnClick="Button1_Click" />
                </div>

            </div>
        </div>
    </div>
</asp:Content>

