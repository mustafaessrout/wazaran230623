<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mst_employeeVehicleBranch.aspx.cs" Inherits="fm_mst_employeeVehicleBranch" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <asp:HiddenField ID="hdAssecode" runat="server"></asp:HiddenField>

        <div class="form-horizontal" style="font-family: Calibri; font-size: small">
            <h4 class="jajarangenjang">Master Employee</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div class="table-page-fixer">
                                <div class="overflow-y relative" >
                                    <asp:GridView ID="grd"
                                        runat="server" CssClass="table table-striped table-page-fix table-hover table-fix mygrid" data-table-page="#ss"
                                        AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting"
                                        CellPadding="0" GridLines="None">
                                        <AlternatingRowStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Employee">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvhc_nm" runat="server" Text='<%# Eval("vhc_nm") %>'></asp:Label>
                                                    <asp:HiddenField ID="hdfemp_cd" runat="server" Value='<%# Eval("emp_cd") %>'></asp:HiddenField>
                                                    <asp:HiddenField ID="hdfvhc_cd" runat="server" Value='<%# Eval("vhc_cd") %>'></asp:HiddenField>
                                                    
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Vehicle No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblvehicle" runat="server" Text='<%# Eval("vhc_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField ShowDeleteButton="True" />
                                        </Columns>
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
    </asp:Content>
