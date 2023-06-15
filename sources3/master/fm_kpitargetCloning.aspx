<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_kpitargetCloning.aspx.cs" Inherits="fm_kpitargetCloning" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Adminbranch/Content/beatifullcontrol.css" rel="stylesheet" />
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="container">
        <div class="form-horizontal">

            <h4 class="jajarangenjang">KPI Target Cloning</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Branch</label>
                    <div class="col-sm-4 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <label class="control-label col-sm-1">Period From</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbperiod" onchange="ShowProgress();" CssClass="form-control" runat="server" ></asp:DropDownList>
                    </div>
                    <label class="control-label col-sm-1">Period To</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbperiodTo" onchange="ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbperiodTo_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-12" style="text-align: center">
                    <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click"><i class="fa fa-plus">&nbsp;Cloning</i></asp:LinkButton>

                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" OnSelectedIndexChanged="grd_SelectedIndexChanged" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Emp Name">
                                    <ItemTemplate><%#Eval("empName") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Key Responsible">
                                    <ItemTemplate><%#Eval("KeyResponsible") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section ">
                                    <ItemTemplate><%#Eval("sectiocValue") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Objective">
                                    <ItemTemplate><%#Eval("objective") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target">
                                    <ItemTemplate><%#Eval("qty") %></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="Yellow" />
                        </asp:GridView>
                    </div>
                </div>

            </div>

        </div>
    </div>


</asp:Content>

