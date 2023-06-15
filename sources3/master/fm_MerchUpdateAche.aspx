<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_MerchUpdateAche.aspx.cs" Inherits="fm_MerchUpdateAche" %>

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
            <h4 class="jajarangenjang">Update Merchandiser KPI</h4>
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

                    <label class="control-label col-sm-2">Employee</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbEmployee" runat="server" CssClass="form-control" AutoPostBack="True"
                            OnSelectedIndexChanged="cbEmployee_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                    <label class="control-label col-sm-2">Is Incentive Approved</label>
                    <div class="col-sm-2">
                        <asp:Label ID="lblIsIncentive" runat="server" ForeColor="Red" Font-Size="X-Large" Text="No"></asp:Label>
                    </div>
                </div>
            </div>

            <h4 class="jajarangenjang">Show Achievement</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-11">
                    <asp:UpdatePanel ID="UpdatePanel45" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdAchievement"
                                runat="server" CssClass="mGrid" data-table-page="#ss"
                                AutoGenerateColumns="False" OnRowCancelingEdit="grdAchievement_RowCancelingEdit"
                                OnRowEditing="grdAchievement_RowEditing"
                                OnRowUpdating="grdAchievement_RowUpdating" OnRowDataBound="grdAchievement_RowDataBound"
                                CellPadding="0" GridLines="None" EmptyDataText="No records Found">
                                <AlternatingRowStyle />
                                <Columns>
                                    <asp:TemplateField HeaderText="Section" ControlStyle-Width="80">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hdmerchUpload_cd" runat="server" Value='<%# Eval("ids") %>'></asp:HiddenField>
                                            <asp:HiddenField ID="hdftarget" runat="server" Value='<%# Eval("target") %>'></asp:HiddenField>
                                            <asp:Label ID="lblsection_nm" runat="server" Text='<%#Eval("section_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Key Response" ControlStyle-Width="200">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkeyresp_nm" runat="server" Text='<%#Eval("keyresp_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                    
                                    <asp:TemplateField HeaderText="Objective" ControlStyle-Width="250">
                                        <ItemTemplate>
                                            <asp:Label ID="lblObjective" runat="server" Text='<%#Eval("Objective") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                     
                                    <asp:TemplateField HeaderText="kpi" ControlStyle-Width="250">
                                        <ItemTemplate>
                                            <asp:Label ID="lblkpi" runat="server" Text='<%#Eval("kpi") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>   
                                    <asp:TemplateField HeaderText="Weight KPI" ControlStyle-Width="30">
                                        <ItemTemplate>
                                            <asp:Label ID="lblweight_kpi" runat="server" Text='<%#Eval("weight_kpi") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                     
                                    <asp:TemplateField HeaderText="Target" ControlStyle-Width="30">
                                        <ItemTemplate>
                                            <asp:Label ID="lbltarget" runat="server" Text='<%#Eval("target") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>                                        
                                    
                                    <asp:TemplateField HeaderText="Achievement" ControlStyle-Width="30">
                                        <ItemTemplate>
                                            <asp:Label ID="lbachievement" runat="server" Text='<%#Eval("achievementValue")%>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txachievement" Text='<%#Eval("achievementValue")%>' runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="true" />
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
