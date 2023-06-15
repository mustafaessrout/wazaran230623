<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_MerchandiserUpload.aspx.cs" Inherits="fm_MerchandiserUpload" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <script src="css/jquery-1.12.4"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        //$(document).ready(function () {
        //    $('#pnlmsg').hide();
        //});

    </script>
    <style>
        body {
            overflow-y: auto !important;
        }

        .form-horizontal.required .form-control {
            content: "*";
            color: red;
        }

        .mygrid thead th, .mygrid tbody th {
            background-color: #5D7B9D !important;
        }
        .container {
            width: 1073px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lblMsg" runat="server" Width="100%" Visible="false"></asp:Label>
                </ContentTemplate>
            </asp:UpdatePanel>
            <h4 class="jajarangenjang">Upload Merchandiser Documents</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-2">Branch</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="True"
                            OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-sm-2">Period</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlPeriod_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>


                </div>
                <div class="row">
                    <label class="control-label col-md-2">Level</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cblevel" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cblevel_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-2">Job Title</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbjobtitle" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbjobtitle_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">

                    <label class="control-label col-sm-2">Display Product Type</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbKPI" runat="server" CssClass="form-control" AutoPostBack="true"
                            OnSelectedIndexChanged="cbKPI_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-sm-2">Employee</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbEmployee" runat="server" CssClass="form-control" AutoPostBack="True"
                            OnSelectedIndexChanged="cbEmployee_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <label class="control-label col-sm-2">KPI Weight</label>
                    <div class="col-sm-2">
                        <asp:Label ID="lblKPIWeight" runat="server"></asp:Label>
                    </div>
                    <label class="control-label col-sm-2">KPI Target</label>
                    <div class="col-sm-2">
                        <asp:Label ID="lblTarget" runat="server"></asp:Label>
                    </div>
                    <label class="control-label col-sm-2">Achievement</label>
                    <div class="col-sm-2">
                        <asp:Label ID="lblAchievement" runat="server"></asp:Label>
                    </div>
                </div>
                <div class="row">
                    <label class="control-label col-sm-2">Is Incentive Approved</label>
                    <div class="col-sm-2">
                        <asp:Label ID="lblIsIncentive" runat="server" ForeColor="Red" Font-Size="X-Large" Text="No"></asp:Label>
                    </div>
                    <label class="control-label col-md-2">Upload Documents</label>
                    <div class="col-md-2 ">
                        <asp:FileUpload ID="upUploadDoc" runat="server" />
                        <asp:UpdatePanel ID="UpdatePanel50" runat="server">
                            <ContentTemplate>
                                <asp:HiddenField ID="hdfUploadDoc" runat="server"></asp:HiddenField>
                                <%--<input type="submit" value="Upload Files" id="btnUploadFiles" onclick="uploadFiles()" title="UploadFiles" />
                        <asp:Label ID="lbfileloc" runat="server" Text=''></asp:Label>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
            <div class="row navi padding-bottom padding-top margin-bottom" style="text-align: center;">

                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:LinkButton ID="btsave" CssClass="btn-warning btn btn-warning" runat="server" OnClick="btsave_Click">Update</asp:LinkButton>
                    </ContentTemplate>
                    <Triggers>
                        <asp:PostBackTrigger ControlID="btsave" />
                    </Triggers>
                </asp:UpdatePanel>
                <span style="padding-left: 378px;"><span style="color: red">*</span> Mandotry field</span>
            </div>
            <h4 class="jajarangenjang">Show Upload Documents</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdUploadDocuments"
                                runat="server" CssClass="table table-striped table-page-fix table-hover table-fix mygrid" data-table-page="#ss"
                                AutoGenerateColumns="False" OnRowDeleting="grdUploadDocuments_RowDeleting"
                                CellPadding="0" GridLines="None" EmptyDataText="No records Found">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Documents">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdmerchUpload_cd" runat="server" Value='<%# Eval("merchUpload_cd") %>'></asp:HiddenField>
                                            <a id='lnkUpload' href='<%# Eval("filepath") %>' target='_blank' runat='server' title='Download'>Download</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowDeleteButton="True" />
                                </Columns>
                            </asp:GridView>
                            <div class="table-copy-page-fix" id="ss"></div>
                        </ContentTemplate>
                        <Triggers>
                            <%--<asp:AsyncPostBackTrigger ControlID="cbsalespoint" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbEmp_type" EventName="SelectedIndexChanged" />--%>
                        </Triggers>
                    </asp:UpdatePanel>

                </div>
            </div>


        </div>
    </div>
</asp:Content>
