<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_DriverOtherDeliveryReport.aspx.cs" Inherits="fm_DriverOtherDeliveryReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="css/anekabutton.css" rel="stylesheet" />

    <script src="admin/js/bootstrap.min.js"></script>
    <script type = "text/javascript" >

      
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
    <style type="text/css">
        .auto-style1 {
            width: 271px;
        }

        .auto-style2 {
            width: 268px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
      <div class="divheader"><h4 class="jajarangenjang">Driver Other Delivery Report</h4></div>
  <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">


              
                    <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Date From :</label>
                    <div class="col-sm-9 drop-down-date">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtDateFrom" runat="server" CssClass="form-control" autocompletetype="Disabled" onkeydown="return false;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="date" ID="dtDateFrom_CalendarExtender" runat="server" TargetControlID="dtDateFrom" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                </ajaxToolkit:CalendarExtender>
                           </ContentTemplate>
                        </asp:UpdatePanel>  
                     
                    </div>
                </div>
                
                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Date To :</label>
                    <div class="col-sm-9 drop-down-date">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtDateTo" runat="server" CssClass="form-control" autocompletetype="Disabled" onkeydown="return false;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="date" ID="dtDateTo_CalendarExtender" runat="server" TargetControlID="dtDateTo" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy" >
                                </ajaxToolkit:CalendarExtender>
                           </ContentTemplate>
                        </asp:UpdatePanel>  
                     
                    </div>
                </div>  
                
                 <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Driver :</label>
                    <div class="col-sm-9 drop-down ">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlDriver" runat="server" AutoPostBack="False" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                 </ContentTemplate> 
                                
                       </asp:UpdatePanel>
                    </div>
                      </div>
                   
                 <div>  
                  <div class="clearfix text-right col-sm-6 no-padding-right">
              <%-- <div class="col-md-12"style="text-align: center">--%>
                    <asp:Button ID="btnShow" runat="server" CssClass="btn btn-info print" Text="SHOW REPORT" OnClick="btnShow_Click" />
                     <asp:Button ID="btnShowAll" runat="server" CssClass="btn btn-info print" Text="SHOW ALL" OnClick="btnShowAll_Click" />
                  
                <%--</div>--%>
            </div>               
              </div>
         </div>
    </div>
   </div> 
</asp:Content>

