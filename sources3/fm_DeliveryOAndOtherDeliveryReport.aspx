<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_DeliveryOAndOtherDeliveryReport.aspx.cs" Inherits="fm_DeliveryOAndOtherDeliveryReport" %>

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
 
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        <h4 class="jajarangenjang">Delivery Order and Other Delivery Report</h4>
    </div>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row ">
            <div class="clearfix" >
                <div style="display:flex;justify-content:center">
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

            <div>
                 <div class="row">
                <div class="clearfix text-center col-sm-12 no-padding-right">
                    <%-- <div class="col-md-12"style="text-align: center">--%>
                    <asp:Button ID="btnDOI" runat="server" CssClass="btn btn-info print" Text=" Loading Report" OnClick="btnDOI_Click" />
                    <asp:Button ID="btnDODI" runat="server" CssClass="btn btn-info print" Text=" Receipt Report" OnClick="btnDODI_Click" />
                    <asp:Button ID="btnOD" runat="server" CssClass="btn btn-info print" Text=" Driver Delivery Other  Report" OnClick="btnOD_Click" />
                    <asp:Button ID="btnDLCR" runat="server" CssClass="btn btn-info print" Text=" Driver Loading and Customer Receive Report" OnClick="btnDLCR_Click" />
                    </div>
                    <%--</div>--%>
                </div>
            </div>

        </div>
    </div>
    </div>
</asp:Content>

