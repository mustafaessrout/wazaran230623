<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmprnSalesbymonthly.aspx.cs" Inherits="frmprnSalesbymonthly" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Sales by monthly</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Period</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbYearCD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbYearCD_SelectedIndexChanged" CssClass="form-control  ">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Salespoint</label>
                <div class="col-sm-10">
                   <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="makeitreadonly ro form-control  " Enabled="False">
                    </asp:DropDownList>
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <div class="col-md-offset-2 col-md-4 col-sm-5">
                      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txfrom" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender ID="txfrom_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="txfrom" TodaysDateFormat="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                </div>
                <p class="text-center col-sm-2 col-md-1" style="margin-top:8px; margin-bottom:0;">To</p>
                <div class="col-sm-5">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txto" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender ID="txto_CalendarExtender" CssClass="date" runat="server" TargetControlID="txto" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Salesman</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbsalesman" runat="server" AutoPostBack="True" CssClass="form-control  " >
                    </asp:DropDownList>
                     
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Outlet Type</label>
                <div class="col-sm-10 drop-down">
                   <asp:DropDownList ID="cbotlcdFr" runat="server" CssClass="form-control  ">
                    </asp:DropDownList>
                     
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Outlet Type</label>
                <div class="col-sm-10 drop-down">
                   <asp:DropDownList ID="cbotlcdTo" runat="server" CssClass="form-control  ">
                    </asp:DropDownList>
                     
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Item</label>
                <div class="col-sm-10 drop-down">
                   <asp:DropDownList ID="cbitem_cdFr" runat="server" CssClass="chosen-select form-control  "  AppendDataBoundItems="True" AutoPostBack="True"   data-placeholder="Choose a Item">
                    </asp:DropDownList>
                     
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Item</label>
                <div class="col-sm-10 drop-down">
                   <asp:DropDownList ID="cbitem_cdTo" runat="server" CssClass="form-control  ">
                    </asp:DropDownList>
                     
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Prod Group</label>
                <div class="col-sm-10 drop-down">
                   <asp:DropDownList ID="cbProd_cdFr" runat="server" CssClass="form-control  ">
                    </asp:DropDownList>
                     
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Prod Group</label>
                <div class="col-sm-10 drop-down">
                   <asp:DropDownList ID="cbProd_cdTo" runat="server" CssClass="form-control  ">
                    </asp:DropDownList>
                     
                </div>
            </div>

            <div class="clearfix form-group col-sm-6">
                <label class="col-sm-2 control-label">Report Name</label>
                <div class="col-sm-10 drop-down">
                   <asp:DropDownList ID="cbreport" runat="server" CssClass="form-control  "  AutoPostBack="True" OnSelectedIndexChanged="cbreport_SelectedIndexChanged">
                    </asp:DropDownList>
                     
                </div>
            </div>

        </div>

         <div class="navi row padding-bottom padding-top">
            <asp:Button ID="btprint" runat="server" CssClass="btn-info btn btn-print" Text="Print" OnClick="btprint_Click" />
        </div>
    </div>

   
</asp:Content>

