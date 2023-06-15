<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salestargetho2Achievement.aspx.cs" Inherits="fm_salestargetho2Achievement" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="css/jquery-1.12.4"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        View Sales Target Achievement 
    </div>
    <img src="div2.png" class="divid" />

    <div class="container">
        <div class="form-horizontal">

            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Report Types</label>
                     <div class="col-md-2 drop-down">

                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlReportTypes" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlReportTypes_SelectedIndexChanged">
                                    <asp:ListItem Selected="True" Text="Daliy Closing" Value="D"></asp:ListItem>
                                    <asp:ListItem Text="Month Closing" Value="M"></asp:ListItem>
                                </asp:DropDownList>
                                <%--                                <asp:ListBox ID="cbsalespoint" Visible="false" runat="server" CssClass="form-control"
                                    SelectionMode="Multiple" Rows="6" Width="300px"></asp:ListBox>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-4">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="chkAllSalespoint" Visible="false" runat="server" Text="All Branch" Checked="false" AutoPostBack="true" OnCheckedChanged="chkAllSalespoint_CheckedChanged" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>

            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Period</label>
                    < <div class="col-md-2">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:ListBox ID="lbPeriod" runat="server" SelectionMode="Multiple" Rows="6" Width="300px"></asp:ListBox>
                                <%--<asp:ListBox ID="cbperiod" SelectMethod="Multiple" runat="server" CssClass="form-control" autogeneratecolumns="False" 
                                    ></asp:ListBox>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-4">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:CheckBox ID="chkAllPeriod" Visible="false" runat="server" Text="All Period" Checked="false" AutoPostBack="true" OnCheckedChanged="chkAllPeriod_CheckedChanged" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Record Date</label>
                    <div class="col-md-2 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlDate" runat="server"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class="navi">
        <asp:Button ID="btnAchievement" runat="server" Text="Raw Data Achievement" CssClass="button2 print" OnClick="btnAchievement_Click" />
    </div>
</asp:Content>

