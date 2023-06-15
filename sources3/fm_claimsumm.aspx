<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" CodeFile="fm_claimsumm.aspx.cs" Inherits="fm_claimsumm" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="v4-alpha/bootstrap.min.css" rel="stylesheet" />
    <link href="v4-alpha/docs.min.css" rel="stylesheet" />

    <script src="v4-alpha/bootstrap.min.js"></script>
    <script src="v4-alpha/docs.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Claim Summary</div>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row">
            <div class="col-md-offset-2 col-md-8 margin-bottom">
                <label for="startDate" class="col-sm-2 col-form-label">Report Type</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbreport" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" CssClass="form-control" AutoPostBack="True">
                        <asp:ListItem Value="0">SALES VS CLAIM SUMMARY</asp:ListItem>
                        <asp:ListItem Value="1">SALES VS CLAIM BY PERIOD</asp:ListItem>
                        <asp:ListItem Value="2">SALES VS CLAIM DETAIL</asp:ListItem>
                    </asp:DropDownList>

                </div>
            </div>
            <div class="col-md-offset-2 col-md-8 margin-bottom">
                <label for="startDate" class="col-sm-2 col-form-label">Start Date</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtstart" runat="server" CssClass="form-control input-group-lg" Enabled="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="dtstart_CalendarExtender" runat="server" BehaviorID="dtstart_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtstart">
                            </ajaxToolkit:CalendarExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbreport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="col-md-offset-2 col-md-8 margin-bottom">
                <label for="startDate" class="col-sm-2 col-form-label">End Date</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-group-lg" Enabled="False"></asp:TextBox>
                            <ajaxToolkit:CalendarExtender ID="dtend_CalendarExtender" runat="server" BehaviorID="dtend_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtend">
                            </ajaxToolkit:CalendarExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbreport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="col-md-offset-2 col-md-8 margin-bottom">
                <label for="startDate" class="col-sm-2 col-form-label">Group from</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                    <asp:DropDownList ID="cbProd_cdFr" CssClass="form-control input-group-lg" runat="server">
                    </asp:DropDownList>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbreport" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>
        <div class="row navi text-center margin-bottom padding-bottom">
            <asp:Button ID="btprint" runat="server" Text="Print Report" CssClass="btn btn-success" OnClick="btprint_Click" />
        </div>
    </div>
</asp:Content>

