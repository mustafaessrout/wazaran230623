<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockopschedule.aspx.cs" Inherits="fm_stockopschedule" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style>
        .date-title{
            font-size:14px;
        }
        .label1{
            background-color: transparent !important;
            border: none;
            text-align: center;
            width: 70px;
        }
        .text-small{
            font-size:13px !important;
        }
        .main-content #mCSB_2_container{
            min-height: 520px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader no-margin-bottom">
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <div >
                    <span class="inline-block">Stock Opname Schedule for period</span> 
                    <span class="inline-block drop-down pull-right">
                        <asp:DropDownList ID="cbperiod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged" CssClass="form-control" Width="170px">
                        </asp:DropDownList>
                        <i class="fa fa-calendar-o" aria-hidden="true" style="right: 13px;font-size: 14px;"></i>
                    </span>
                </div>
                <div class="date-title">
                    <asp:TextBox ID="txfrom" runat="server" CssClass="ro label1 text-primary text-left" Enabled="False"></asp:TextBox>
                    <asp:CalendarExtender ID="txfrom_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="txfrom" TodaysDateFormat="d/M/yyyy">
                    </asp:CalendarExtender>

                    <i class="fa fa-minus text-primary" style="margin-left:5px; margin-right:5px;font-size: 10px;" aria-hidden="true"></i>

                    <asp:TextBox ID="txto" runat="server" CssClass="ro label1 text-primary text-right" Enabled="False"></asp:TextBox>
                    <asp:CalendarExtender ID="txto_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="txto" TodaysDateFormat="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
                
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    <div class="h-divider"></div>
    
    <div class="container-fluid">
        <div class="row">
            <div class="clearfix form-group col-md-11 col-sm-10 no-padding-left no-padding-right padding-bottom">
                <label class="col-sm-2 control-label">Stock Opname For</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbstocktype" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbstocktype_SelectedIndexChanged">
                    </asp:DropDownList>
                     
                </div>
            </div>

            <div class="clearfix form-group margin-top padding-top">
                <div class="col-sm-7 no-padding-left">
                    <label for="cbwhs" class="">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbdepo" runat="server"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbstocktype" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>    
                    </label>
                    <div class="drop-down">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                 <asp:DropDownList ID="cbwhs" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                         
                    </div>
                </div>
                <div class="col-md-4 col-sm-3 no-padding-left">
                    <label for="dtschedule" class="text-small">Schedule Date (d/m/yyyy)</label>
                    <div class="drop-down-date">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtschedule" runat="server" OnTextChanged="dtschedule_TextChanged" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="dtschedule_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtschedule">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-1 col-sm-2 no-padding">
                    <label>&nbsp;</label>
                    <div>
                        <asp:Button ID="btAdd" runat="server" CssClass="btn-success btn btn-add" Text="Add" OnClick="btAdd_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" OnRowDeleting="grd_RowDeleting" CssClass="table table-hover table-striped mygrid" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Salesman/Warehouse">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdids" runat="server" Value='<%# Eval("ids") %>' />
                                    <asp:HiddenField ID="hdwhs_cd" runat="server" Value='<%# Eval("whs_cd") %>' />
                                   <%# Eval("whs_name") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date Schedule">
                                <ItemTemplate>
                                    <asp:Label ID="dtschedule" runat="server" Text='<%# Eval("schedule_dt","{0:d/M/yyyy}") %>'></asp:Label> 
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txschedule_dt" runat="server" CssClass="form-control input-sm" Text='<%# Eval("schedule_dt","{0:d/M/yyyy}") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" SelectText="Print" ShowSelectButton="True" />
                        </Columns>
                        <EditRowStyle BackColor="#BDC3C7" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle/>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbperiod" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="row navi margin-bottom padding-bottom">
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
    </div>

    
</asp:Content>

