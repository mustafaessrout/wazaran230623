<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_poreport.aspx.cs" Inherits="fm_poreport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>Purchasing REPORT -
        <asp:Label ID="lbsp" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red"></asp:Label>
    </h3>

    <div class="container">
        <div class="form-horizontal">
            <div class="form-group">
                <label class="control-label col-md-2">Period</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbperiod" runat="server" CssClass="form-control-static" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbperiod" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-2">Start Date:</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtstart" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                            <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-2">End Date:</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtend" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                            <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtend">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-2">Trabsaction Type</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbtyp" CssClass="form-control-static" runat="server" Width="100%" ></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbtyp" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>

                </div>

            </div>
            <img src="div2.png" class="divid" />
            <div class="navi">
                <asp:Button ID="btprint" runat="server" Text="Print"  CssClass="btn btn-default btn-lg" OnClick="btprint_Click" />

            </div>
        </div>
    </div>
</asp:Content>

