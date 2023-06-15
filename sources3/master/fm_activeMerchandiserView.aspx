<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_activeMerchandiserView.aspx.cs" Inherits="fm_activeMerchandiserView" %>

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
            <h4 class="jajarangenjang">Active Merchandiser</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Period</label>
                    <div class="col-sm-3 drop-down">
                        <asp:DropDownList ID="ddlPeriod" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-sm-1">Branch</label>
                    <div class="col-sm-3 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="True"
                            OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="clearfix">
                    <div class="col-md-8">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <div class="table-page-fixer ">
                                    <div class="overflow-y relative" >
                                        <asp:GridView ID="grd"
                                            runat="server" CssClass="table table-striped table-page-fix table-hover table-fix mygrid" data-table-page="#ss"
                                            AutoGenerateColumns="False" AllowPaging="True" 
                                             CellPadding="0" GridLines="None" PageSize="100">
                                            <AlternatingRowStyle />
                                            <Columns>
                                                <asp:TemplateField HeaderText="Emp Number">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpno" runat="server" Text='<%#Eval("emp_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="Emp Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblEmpName" runat="server" Text='<%#Eval("emp_desc") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Is Team Leader">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblTL" runat="server" Text='<%#Eval("isTL") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                               
                                                <%--<asp:CommandField ShowSelectButton="True" />--%>
                                            </Columns>
                                            <EditRowStyle CssClass="table-edit" />
                                            <FooterStyle CssClass="table-footer" />
                                            <HeaderStyle CssClass="table-header" />
                                            <PagerStyle CssClass="table-page" />
                                            <RowStyle />
                                            <SelectedRowStyle CssClass="table-edit" />
                                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                        </asp:GridView>
                                    </div>
                                </div>
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
    </div>
</asp:Content>

