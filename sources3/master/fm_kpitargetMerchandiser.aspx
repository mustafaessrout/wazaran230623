<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_kpitargetMerchandiser.aspx.cs" Inherits="fm_kpitargetMerchandiser" %>

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

            <h4 class="jajarangenjang">KPI Target</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Branch</label>
                    <div class="col-sm-4 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                </div>

                <div class="row">
                    <label class="control-label col-sm-1">Period</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbperiod" onchange="ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Level</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cblevel" onchange="ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cblevel_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <%--<label class="control-label col-sm-2">Is Team Leader</label>
                    <div class="col-sm-4 drop-down">
                        <asp:CheckBox ID="ckTL" runat="server" AutoPostBack="true" OnCheckedChanged="cbTL_CheckedChanged" />
                    </div>--%>
                    <label class="control-label col-md-1">Job Title</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbjobtitle" onchange="ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbjobtitle_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Employee</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbemployee" onchange="ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbemployee_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDataBound="grd_RowDataBound" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" CellPadding="0" OnSelectedIndexChanged="grd_SelectedIndexChanged" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Key Responsible">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdids" Value='<%#Eval("IDS") %>' runat="server" />
                                        <asp:Label ID="lbkeyresp" runat="server" Text='<%#Eval("keyresp_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section ">
                                    <ItemTemplate><%#Eval("section_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Objective">
                                    <ItemTemplate><%#Eval("objective") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="KPI">
                                    <ItemTemplate>
                                        <%#Eval("kpi")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtarget" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtarget" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Action" ShowSelectButton="True" />
                            </Columns>
                            <SelectedRowStyle BackColor="Yellow" />
                        </asp:GridView>
                    </div>
                </div>

            </div>
            <div class="col-md-12" style="text-align: center">
                <%--<asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click"><i class="fa fa-plus">&nbsp;New</i></asp:LinkButton>
                <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-success" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;Save</i></asp:LinkButton>
                <asp:LinkButton ID="bprint" CssClass="btn btn-danger" runat="server"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>--%>
            </div>
        </div>
    </div>


</asp:Content>

