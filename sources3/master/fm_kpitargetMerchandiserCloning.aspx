<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_kpitargetMerchandiserCloning.aspx.cs" Inherits="master_fm_kpitargetMerchandiserCloning" %>


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

            <h4 class="jajarangenjang">Target Cloning</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Branch</label>
                    <div class="col-sm-4 drop-down">
                        <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" ></asp:DropDownList>
                    </div>

                </div>

                <div class="row">
                    <label class="control-label col-sm-1">Period From</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbperiod" onchange="ShowProgress();" CssClass="form-control" runat="server"></asp:DropDownList>
                    </div>
                    <label class="control-label col-sm-1">Period To</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbperiodTo" onchange="ShowProgress();" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbperiodTo_SelectedIndexChanged" runat="server"></asp:DropDownList>
                    </div>
                </div>

            </div>
            <div class="col-md-12" style="text-align: center">
                <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-success" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;Cloning</i></asp:LinkButton>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:GridView ID="grd" CssClass="mGrid" runat="server"  AutoGenerateColumns="False"  CellPadding="0">
                        <Columns>
                            <asp:BoundField DataField="empName" HeaderText="ID" />
                            <asp:BoundField DataField="empName" HeaderText="Name" />
                            <asp:BoundField DataField="KeyResponsible" HeaderText="Key Responsible" />
                            <asp:BoundField DataField="sectiocValue" HeaderText="Section Value" />
                            <asp:BoundField DataField="objective" HeaderText="Objective" />
                            <asp:BoundField DataField="kpi" HeaderText="KPI" />
                            <asp:BoundField DataField="qty" HeaderText="Target" />
                        </Columns>
                        <SelectedRowStyle BackColor="Yellow" />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

