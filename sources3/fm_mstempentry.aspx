<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstempentry.aspx.cs" Inherits="fm_mstempentry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#<%=dtjoin.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
        });
    </script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style>
        .date.posTop {
            top: auto !important;
            bottom: 35px !important;
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Employee Entry</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <div class="clearfix col-md-offset-1 col-sm-5  text-center">
                <asp:Image ID="img" runat="server" ImageUrl="~/noimage.jpg" CssClass="img" />
            </div>
            <div class="clearfix col-md-5 col-sm-7">
                <div class="clearfix form-group">
                    <label class="control-label col-sm-2">Salespoint</label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <asp:Label ID="lbname" runat="server" Text="English Name" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txname" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <asp:Label ID="lbname_ar" runat="server" Text="Arabic Name" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txname_ar" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <asp:Label ID="lbposition" runat="server" Text="Position" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbpos" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <asp:Label ID="lbempno" runat="server" Text="Employee Number" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10">
                        <asp:TextBox ID="txempcd" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <asp:Label ID="lbdept" runat="server" Text="Department" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbdepartment" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <asp:Label ID="Label3" runat="server" Text="Level" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cblevel" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <asp:Label ID="lbnational" runat="server" Text="Nationality" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10 drop-down">
                        <asp:DropDownList ID="cbnationality" runat="server">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <label class="control-label col-sm-2">Joint Date</label>
                    <div class="col-sm-10 drop-down-date">
                        <asp:TextBox ID="dtjoin" runat="server" CssClass="form-control"></asp:TextBox>
                        <asp:CalendarExtender ID="dtjoin_CalendarExtender" CssClass="date posTop" PopupPosition="TopLeft" runat="server" Format="d/M/yyyy" TargetControlID="dtjoin">
                        </asp:CalendarExtender>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <label class="col-sm-2 control-label">Picture</label>
                    <div class="col-sm-10">
                        <asp:FileUpload ID="upl" runat="server" />
                    </div>
                </div>
            </div>

        </div>
        <div class="h-divider"></div>
        <div class="row">
            <div class="clearfix col-md-12 col-sm-12">
                <div class="clearfix form-group">
                    <div class="col-sm-12">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="chaccess" runat="server" AutoPostBack="true" OnCheckedChanged="chaccess_CheckedChanged" Text="Tick this, if this Employee access Application." />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div runat="server" id="inputAccess">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="clearfix form-group">
                                <asp:Label ID="lbusr_nm" runat="server" Text="User ID" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txusr_nm" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="clearfix form-group">
                                <asp:Label ID="lbpass" runat="server" Text="Password" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txpassword" runat="server" CssClass="form-control" TextMode="Password"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="clearfix form-group">
                                <asp:Label ID="lbmobile" runat="server" Text="Mobile No" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txmobile" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="clearfix form-group">
                                <asp:Label ID="lbemail" runat="server" Text="Email" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txemail" runat="server" CssClass="form-control" TextMode="Email"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="clearfix form-group">
                                <asp:Label ID="lbrole" runat="server" Text="User Role" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:DropDownList ID="cbrole" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="clearfix form-group">
                                <asp:Label ID="Label1" runat="server" Text="Branch Access" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-3">
                                    <asp:DropDownList ID="cbsalespointacc" runat="server" CssClass="form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="col-sm-2">
                                    <asp:Button ID="btaddsalespointacc" runat="server" Text="Add" CssClass="btn-success btn btn-add" OnClick="btaddsalespointacc_Click" />
                                </div>
                                <div class="col-sm-5">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:GridView ID="grdsalespoint" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grdsalespoint_PageIndexChanging" ShowFooter="False" CellPadding="0" OnRowDeleting="grdsalespoint_RowDeleting" AllowPaging="True" PageSize="5">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Salespoint">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label>
                                                            -
                                                            <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                                            <asp:HiddenField ID="hdsalespointcd" runat="server" Value='<%#Eval("salespointcd") %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" />
                                                </Columns>
                                            </asp:GridView>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chaccess" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <div class="h-divider"></div>
        <div class="row">
            <div class="clearfix col-md-12 col-sm-12">
                <div class="clearfix form-group">
                    <div class="col-sm-12">
                        <asp:CheckBox ID="chtablet" runat="server" AutoPostBack="true" OnCheckedChanged="chtablet_CheckedChanged" Text="Tick this, if this Employee Have Tablet for Transaction." />
                    </div>
                </div>
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div runat="server" id="inputTablet">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="clearfix form-group">
                                <asp:Label ID="lbimei" runat="server" Text="Imei" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="tximei" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="clearfix form-group">
                                <asp:Label ID="lbsim" runat="server" Text="Mobile No" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txsim" runat="server" CssClass="form-control" TextMode="Phone"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="clearfix form-group">
                                <asp:Label ID="lbgmail" runat="server" Text="Gmail" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txgmail" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="clearfix form-group">
                                <asp:Label ID="lbpwdgmail" runat="server" Text="Gmail Password" CssClass="control-label col-sm-2"></asp:Label>
                                <div class="col-sm-10">
                                    <asp:TextBox ID="txgmailpass" runat="server" CssClass="form-control"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chtablet" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <div class="row navi margin-bottom padding-bottom padding-top">
            <asp:Button ID="btadd" runat="server" Text="New" OnClientClick="ShowProgress();" CssClass="btn-success btn btn-add" OnClick="btadd_Click" />
            <asp:Button ID="btedit" OnClientClick="ShowProgress();" runat="server" Text="Edit" CssClass="btn-primary btn btn-edit" />
            <asp:Button ID="btsave" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click" Text="SAVE" CssClass="btn-warning btn btn-save" />
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

