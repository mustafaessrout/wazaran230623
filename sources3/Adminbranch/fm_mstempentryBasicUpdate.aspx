<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_mstempentryBasicUpdate.aspx.cs" Inherits="fm_mstempentryBasicUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <%--   <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />--%>
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function refreshdata(s) {
            $get('<%=hdemp.ClientID%>').value = s;
            $get('<%=btrefresh.ClientID%>').click();
        }
    </script>
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
    <style>
        input[type=checkbox][disabled] {
            outline: 1px solid red;
        }

        .circleBase {
            border-radius: 50%;
            behavior: url(PIE.htc); /* remove if you don't care about IE8 */
        }

        .type1 {
            width: 20px;
            height: 20px;
            display: inline-block;
            background: yellow;
        }

            .type1.green {
            }

            .type1.red {
            }

        .lblHOStat {
            vertical-align: top;
            line-height: 20px;
            padding-left: 10px;
            padding-right: 10px;
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


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <asp:HiddenField ID="hdemp" runat="server" />
    <%--<div class="container">--%>
    <div class="form-horizontal">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Width="100%" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <h3>Update Personal Data</h3>
        <div class="h-divider"></div>
        <div class="form-group">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">

                <ContentTemplate>
                    <asp:HiddenField ID="hdfHOConnected" runat="server" />
                    <asp:HiddenField ID="hdfBaranchID" runat="server" />
                    <asp:HiddenField ID="hdfUserID" runat="server" />
                    <asp:HiddenField ID="hdfSyncID" runat="server" />

                    <div class="clearfix margin-bottom">
                        <div class="col-md-6 col-sm-12">
                            <asp:Label ID="lblBranch" runat="server" Text="Branch Name" CssClass="titik-dua col-md-3 col-sm-4 text-bold"></asp:Label>
                            <asp:Label ID="lblBranchName" runat="server" Text="" CssClass="padding-top-4 col-md-9 col-sm-8 text-bold block"></asp:Label>
                        </div>
                    </div>
                    <div class="clearfix margin-bottom">
                        <div class="col-md-6 col-sm-12">
                            <asp:Label ID="blbHOStatus" runat="server" Text="HO Status" CssClass="titik-dua col-md-3 col-sm-4 text-bold"></asp:Label>
                            <div class="col-md-9 col-sm-8">
                                <div id="dvHOStatusValue" class="circleBase type1" runat="server"></div>
                                <asp:Label ID="lblHOStat" Text="Connected" CssClass="lblHOStat text-bold" runat="server" />
                                <asp:Button ID="btnRefesh" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btSearchHO_Click" Text="Refresh" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1" style="font-size: x-small">Emp Code</label>
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
            <label class="control-label col-md-1" style="font-size: x-small">Emp Name</label>
            <div class="col-md-3" style="font-size: x-small">
                <asp:TextBox ID="txempname" runat="server" CssClass="form-control col-md-2" Height="100%"></asp:TextBox>
            </div>
            <label class="control-label col-md-1" style="font-size: x-small">Job Title:</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbjobtitle" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1" style="font-size: x-small">Level</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cblevel" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1" style="font-size: x-small">Nationality</label>
            <div class="col-md-3" style="font-size: x-small">
                <asp:DropDownList ID="cbnationality" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1" style="font-size: x-small">Salespoint:</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbsp" runat="server" CssClass="form-control-static input-sm"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1" style="font-size: x-small">Join Date</label>
            <div class="col-md-3">
                <asp:TextBox ID="dtjoin" runat="server" CssClass="form-control input-sm" Width="100%" Height="100%"></asp:TextBox>
                <asp:CalendarExtender ID="dtjoin_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtjoin">
                </asp:CalendarExtender>
            </div>
            <label class="control-label col-md-1" style="font-size: x-small">Sort Name</label>
            <div class="col-md-3" style="font-size: x-small">
                <asp:TextBox ID="txshortname" runat="server" CssClass="form-control input-sm" Height="100%" Width="100%"></asp:TextBox>
            </div>
            <label class="control-label col-md-1" style="font-size: x-small">Married Status:</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbmarried" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
            </div>

        </div>
        <div class="form-group">
            <label class="control-label col-md-1" style="font-size: x-small">Department Status:</label>
            <div class="col-md-2">
                <asp:DropDownList ID="ddlDepartment" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="navi">
                <asp:LinkButton ID="btnew" CssClass="btn btn-default btn-lg" runat="server" ToolTip="New" OnClick="btnew_Click"><span class="glyphicon glyphicon-ok"></span></asp:LinkButton>
                <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-default glyphicon-save" OnClick="btsave_Click" />

            </div>
        </div>
        <h3>Login Data Wazaran</h3>
        <div class="h-divider"></div>

        <div class="form-group">
            <label class="control-label col-md-1" style="font-size: x-small">Email ID</label>
            <div class="col-md-3" style="font-size: x-small">
                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control input-sm" Height="100%" Width="100%"></asp:TextBox>
            </div>
            <label class="control-label col-md-1" style="font-size: x-small">Mobile No</label>
            <div class="col-md-3" style="font-size: x-small">
                <asp:TextBox ID="txtmobile" runat="server" CssClass="form-control input-sm" Height="100%" Width="100%"></asp:TextBox>
            </div>
            <label class="control-label col-md-1" style="font-size: x-small">Role</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbrole" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
        </div>
        <div class="h-divider"></div>

        <div class="form-group">
            <div class="navi">
                
                <%--<asp:LinkButton ID="btnHO" CssClass="btn btn-default btn-lg" runat="server" ToolTip="New" OnClick="btnHO_Click">Update HO</asp:LinkButton>--%>

                <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" Text="Button" Style="display: none;" />
                <asp:Button ID="btnWazaranLogin" runat="server" Text="Wazaran Login" CssClass="btn btn-default glyphicon-save" OnClick="btnWazaranLogin_Click" />
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
                PopupCenter('lookup_emp.aspx', 'xtf', '900', '600');
            });
        });


    </script>

</asp:Content>

