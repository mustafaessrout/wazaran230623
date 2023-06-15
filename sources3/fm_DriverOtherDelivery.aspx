<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_DriverOtherDelivery.aspx.cs" Inherits="fm_DriverOtherDelivery" %>


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
               <%-- $('#<%=lbmsg.ClientID %>').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');--%>

        }
    </script>
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
      <div class="divheader"><h4 class="jajarangenjang">Driver Other Delivery</h4></div>
  <div class="h-divider"></div>
    

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">

               <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Driver :</label>
                    <div class="col-sm-9 drop-down ">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdids" runat="server" />
                                <asp:DropDownList ID="ddlDriver" runat="server" AutoPostBack="True" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                                 </ContentTemplate> 
                                
                       </asp:UpdatePanel>
                    </div>
                    </div>
            

                 <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">From :</label>
                    <div class="col-sm-9 drop-down">
                         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddllocFrom" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddllocFrom_SelectedIndexChanged" AppendDataBoundItems="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">To :</label>
                    <div class="col-sm-9 drop-down">
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddllocTo" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged ="ddllocTo_SelectedIndexChanged" AppendDataBoundItems="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                 <div class="clearfix">
                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Reason :</label>
                    <div class="col-sm-9 drop-down">
                         <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlReason" runat="server" AutoPostBack="True" CssClass="form-control" AppendDataBoundItems="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                     <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Period :</label>
                    <div class="col-sm-9 drop-down">
                         <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlPeriod" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged ="ddlPeriod_SelectedIndexChanged" AppendDataBoundItems="true">
                                    <asp:ListItem>Select</asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="clearfix form-group col-sm-3 no-padding-right">
                    <label class="control-label col-sm-3">Date :</label>
                    <div class="col-sm-9 drop-down-date">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtDrIOtherDelDate" runat="server" CssClass="form-control" autocompletetype="Disabled" onkeydown="return false;"></asp:TextBox>
                                <ajaxToolkit:CalendarExtender CssClass="date" ID="dtDrIOtherDelDate_CalendarExtender" runat="server" TargetControlID="dtDrIOtherDelDate" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                </ajaxToolkit:CalendarExtender>
                           </ContentTemplate>
                        </asp:UpdatePanel>  
                     
                    </div>
                </div>

                    
            <div class="clearfix form-group col-sm-6 no-padding-right">
                <label class="control-label col-sm-2  ">Remark :</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                        <asp:TextBox ID="txRemark" runat="server" CssClass="form-control" Height="100%"></asp:TextBox>
                      </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>


           
                    
                </div>
            
              </div>
         </div>
         <div class="clearfix text-right col-sm-12 no-padding-right">
                <div class="col-md-12"style="text-align: center">
                     <asp:Button ID="btnNew" runat="server" CssClass="btn btn-warning submit" Text="NEW" OnClick="btnNew_Click" Visible="false"  />
                          <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-warning submit" Text="SUBMIT" OnClick="btnSubmit_Click" />
                    <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-info print" Text="PRINT" OnClick="btnPrint_Click" Visible="false" />
                     <asp:Button ID="btnReport" runat="server" CssClass="btn btn-info print" Text="REPORT" OnClick="btnReport_Click" />
                    <%-- <asp:Label ID="lbmsg" runat="server" style="font-weight: 700" Text="Label"></asp:Label>--%>
                </div>
            </div>
    </div>
       
   
</asp:Content>

