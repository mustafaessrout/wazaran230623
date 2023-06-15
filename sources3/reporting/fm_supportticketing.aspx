<%@ Page Title="" Language="C#" MasterPageFile="~/reporting/reporting.master" AutoEventWireup="true" CodeFile="fm_supportticketing.aspx.cs" Inherits="reporting_fm_supportticketing" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <link rel="stylesheet" href="css/anekabutton.css" />
    <link rel="Stylesheet" href="css/chosen.css" />
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.6.4/jquery.min.js" type="text/javascript"></script>
    <script src="css/chosen.jquery.js" type="text/javascript"></script>
    <script>
        function openwindow(url) {
            mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=400,height=500");
            mywindow.moveTo(400, 200);
        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">

    <div class="divheader">SUPPORT TICKETING</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="form-horizontal">
            <div class="clearfix margin-bottom form-group">
                <div class="col-md-2 titik-dua control-label">
                    Status
                </div>
                <div class="col-md-4 drop-down">
                    <asp:DropDownList ID="cbstatus" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged">
                        <asp:ListItem Value="N">New</asp:ListItem>
                        <asp:ListItem Value="C">Completed</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <div class="clearfix margin-bottom form-group">
                <div class="col-md-2 titik-dua control-label">
                    Branch
                </div>
                <div class="col-md-4 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </div>
        </div>

        <div class="h-divider"></div>

        <div id="showTicket" runat="server">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdticket" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="table table-striped mygrid" ShowHeaderWhenEmpty="true" OnRowCommand="grdticket_RowCommand" AllowPaging="true" OnPageIndexChanging="grdticket_PageIndexChanging" PageSize="50" GridLines="None">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Ticket No">
                                <ItemTemplate>
                                    <asp:Label ID="lbno" runat="server" Text='<%# Eval("ps_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbdate" runat="server" Text='<%# Eval("created_dt","{0:yyyy-MM-dd HH:mm:ss}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <%# Eval("branch") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Created By">
                                <ItemTemplate>
                                    <%# Eval("created_by") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Title">
                                <ItemTemplate>
                                    <asp:Label ID="lbtitle" runat="server" Text='<%# Eval("problem_title") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lbremark" runat="server" Text='<%# Eval("remark") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <%# Eval("status") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:ButtonField ButtonType="Link"  Text="<i aria-hidden='true' class='fa fa-support'></i> View" ControlStyle-CssClass="btn default btn-xs purple" CommandName="view">
                                <ControlStyle CssClass="btn default btn-xs purple" />
                            </asp:ButtonField>  
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>

            </asp:UpdatePanel>
        </div>

    </div>

</asp:Content>

