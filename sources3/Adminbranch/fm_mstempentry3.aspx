<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_mstempentry3.aspx.cs" Inherits="fm_mstempentry3" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 <%--   <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />--%>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function refreshdata(s)
        {
            $get('<%=hdemp.ClientID%>').value = s;
            $get('<%=btrefresh.ClientID%>').click();
        }
    </script>
    <style type="text/css">
        .messagealert {
            width: 100%;
            position: fixed;
             top:0px;
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
            $('#<%=lbmsg.ClientID %>').append('<div id="alert_div" style="margin: 0 0.5%; -webkit-box-shadow: 3px 4px 6px #999;" class="alert fade in ' + cssclass + '"><a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a><strong>' + messagetype + '!</strong> <span>' + message + '</span></div>');

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdemp" runat="server" />
    <%--<div class="container">--%>
        <div class="form-horizontal">
            <h3>Personal Data</h3>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1" style="font-size:x-small">Emp Code</label>
                <div class="col-md-3">
                    <div class="input-group">
                    <asp:TextBox ID="txempcode" runat="server" CssClass="form-control col-md-2" Height="100%"></asp:TextBox>
                      
                    <span class="input-group-btn">
                            <button type="submit" class="btn btn-primary" runat="server" id="btsearch" onserverclick="btsearch_ServerClick">
                               <i class="glyphicon glyphicon-search" aria-hidden="true"></i>
                            </button>
                        </span>
                    </div>
                </div>
                 <label class="control-label col-md-1" style="font-size:x-small">Emp Name</label>
                <div class="col-md-3" style="font-size:x-small">
                    <asp:TextBox ID="txempname" runat="server" CssClass="form-control col-md-2" Height="100%"></asp:TextBox>
                </div>
                <label class="control-label col-md-1" style="font-size:x-small">Job Title:</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbjobtitle" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
                </div>
            </div>
             <div class="form-group">
                <label class="control-label col-md-1" style="font-size:x-small">Level</label>
                <div class="col-md-3">
                   <asp:DropDownList ID="cblevel" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1" style="font-size:x-small">Nationality</label>
                <div class="col-md-3" style="font-size:x-small">
                     <asp:DropDownList ID="cbnationality" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1" style="font-size:x-small">Salespoint:</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbsp" runat="server" CssClass="form-control-static input-sm"></asp:DropDownList>
                </div>
            </div>
             <div class="form-group">
                <label class="control-label col-md-1" style="font-size:x-small">Join Date</label>
                <div class="col-md-3">
                    <asp:TextBox ID="dtjoin" runat="server" CssClass="form-control input-sm" Width="100%" Height="100%"></asp:TextBox>
                    <asp:CalendarExtender ID="dtjoin_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtjoin">
                    </asp:CalendarExtender>
                </div>
                 <label class="control-label col-md-1" style="font-size:x-small">Shorname</label>
                <div class="col-md-3" style="font-size:x-small">
                      <asp:TextBox ID="txshortname" runat="server" CssClass="form-control input-sm" Height="100%" Width="100%"></asp:TextBox>
                </div>
                <label class="control-label col-md-1" style="font-size:x-small">Married Status:</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbmarried" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
                </div>
            </div>
            <div class="h-divider"></div>
            <div class="form-group">
                <h4>Wazaran Profile</h4>
                <label class="control-label col-md-1" style="font-size:x-small">User ID</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txuserid" runat="server" CssClass="form-control input-sm" Width="100%" Height="100%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtjoin">
                    </asp:CalendarExtender>
                </div>
                 <label class="control-label col-md-1" style="font-size:x-small">Password</label>
                <div class="col-md-3" style="font-size:x-small">
                      <asp:TextBox ID="txpassword" runat="server" CssClass="form-control input-sm" Height="100%" Width="100%" TextMode="Password"></asp:TextBox>
                </div>
                <label class="control-label col-md-1" style="font-size:x-small">Re-Type:</label>
                <div class="col-md-2">
                     <asp:TextBox ID="txpassretype" runat="server"  CssClass="form-control input-sm" Height="100%" Width="100%" TextMode="Password"></asp:TextBox>
                </div>
            </div>
               <div class="form-group">
                <label class="control-label col-md-1" style="font-size:x-small">Mobile No:</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txmobile" runat="server" CssClass="form-control input-sm" Width="100%" Height="100%"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="d/M/yyyy" TargetControlID="dtjoin">
                    </asp:CalendarExtender>
                </div>
                 <label class="control-label col-md-1" style="font-size:x-small">Email</label>
                <div class="col-md-3" style="font-size:x-small">
                      <asp:TextBox ID="txemail" runat="server" CssClass="form-control input-sm" Height="100%" Width="100%"></asp:TextBox>
                </div>
                <label class="control-label col-md-1" style="font-size:x-small">process:</label>
                <div class="col-md-2">
                     <asp:TextBox ID="TextBox3" runat="server"  CssClass="form-control input-sm" Height="100%" Width="100%"></asp:TextBox>
                </div>
            </div>
             <div class="form-group">
                 <div class="navi">
                     <asp:LinkButton ID="btnew" CssClass="btn btn-default btn-lg" runat="server" ToolTip="New" OnClick="btnew_Click"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                      <asp:LinkButton ID="btedit" CssClass="btn btn-default btn-lg" runat="server" ToolTip="Edit" OnClick="btedit_Click"><span class="glyphicon glyphicon-check"></span></asp:LinkButton>
                      <asp:LinkButton ID="btsave" CssClass="btn btn-default btn-lg" runat="server" ToolTip="Save" OnClick="btsave_Click"><span class="glyphicon glyphicon-floppy-disk"></span></asp:LinkButton>
                     <%-- <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-default glyphicon-save" OnClick="btsave_Click" />--%>
                     <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" Text="Button" />
                     
                     <asp:Label ID="lbmsg" runat="server" style="font-weight: 700" Text="Label"></asp:Label>
                     
                 </div>
                 </div>
        </div>
<%--    </div>--%>
    

    <div class="alert alert-danger fade in" id="msg" runat="server">
        <a href="#" class="close" data-dismiss="alert">&times;</a>
    </div>
      <script>
          $(document).ready(function () {
              $("#<%=btsearch.ClientID%>").click(function () {
            PopupCenter('lookup_emp.aspx', 'xtf', '900', '500');
        });
    });


    </script>  

</asp:Content>

