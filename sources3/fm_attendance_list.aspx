<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_attendance_list.aspx.cs" Inherits="fm_attendance_list" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div id="container">
        <h4 class="jajarangenjang"><asp:Label runat="server" ID="lbTitle"></asp:Label></h4>
        <div class="h-divider"></div>

        <div id="summaryclosing" runat="server">
            <%--<h5 class="jajarangenjang">Daily Closing Summary</h5>--%>
            <div class="row">
                <div class="col-sm-6 ">
                    <div class="clearfix form-group">
                        <label class="control-label col-sm-2">Period</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged" CssClass="form-control  ">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="clearfix form-group">
                        <div class="col-sm-offset-2 col-sm-10">
                        <div class="no-padding col-xs-5">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txfrom" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txfrom_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="txfrom_CalendarExtender" runat="server" TargetControlID="txfrom" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                    </asp:CalendarExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>

                        <p class="col-xs-2 text-center text-bold" style="margin-top: 8px;">To</p>

                        <div class="col-xs-5 no-padding">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txto" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txto_TextChanged"></asp:TextBox>
                                    <asp:CalendarExtender ID="txto_CalendarExtender" runat="server" TargetControlID="txto" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                    </asp:CalendarExtender>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    </div>
                </div>

                <div class="col-sm-6">
                    <div class="clearfix form-group" runat="server" id="listSalespoint">
                        <label class="control-label col-sm-2">Salespoint</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbSalesPointCD" runat="server" CssClass="form-control  ">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>                         
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="navi row padding-bottom padding-top margin-top">
             <asp:Button ID="btprint" runat="server" CssClass="btn-info btn btn-print" Text="Print" OnClick="btprint_Click" />
        </div>

    </div>
</asp:Content>

