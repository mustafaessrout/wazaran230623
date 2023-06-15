<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_bankdepositlist.aspx.cs" Inherits="fm_bankdepositlist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function RefreshData() {
            $get('<%=btrefresh.ClientID%>').click();
            sweetAlert('Clearance Or Rejected has been completed', '', 'success'); return (false);
        }
    </script>
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--  <div class="divheader">
        Deposit List
    </div>
    <div class="h-divider"></div>--%>
    <div class="alert alert-info text-bold">Bank Deposit List</div>
    <div class="container margin-bottom">
        <div class="row">
            <div class="col-md-6 ">
                <label class="col-sm-2 control-label ">Salespoint</label>
                <div class="col-sm-4 drop-down">
                    <asp:DropDownList ID="cbsalespoint" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                </div>
                <label class="col-sm-2 control-label ">Deposit Status </label>
                <div class="drop-down col-sm-4">
                    <asp:DropDownList ID="cbstatus" runat="server" AutoPostBack="True" OnChange="javascript:ShowProgress();" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                </div>
                <label class="control-label col-sm-3" style="color: red; font-weight: bolder">Only Confirmed By HO can be cleareance!</label>
            </div>
        </div>
        <div class="row">
            <div class="divgrid">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" OnRowEditing="grd_RowEditing" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" CssClass="mGrid margin-top" OnRowUpdating="grd_RowUpdating" OnRowCancelingEdit="grd_RowCancelingEdit1" OnRowDataBound="grd_RowDataBound1">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Cheq/Bank Trf No.">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hddepositid" runat="server" Value='<%# Eval("deposit_id") %>' />
                                       <%-- <%if (cbstatus.SelectedValue.ToString() == "H")
                                            { %>
                                        <a href="javascript:popupwindow('bankcleareance.aspx?id=<%# Eval("deposit_id") %>');">
                                            <asp:Label ID="lbdepositno" runat="server" Text='<%# Eval("deposit_no") %>'></asp:Label></a>
                                        <%}
                                            else--%>
                                          <asp:Label ID="lbdepositonumber" runat="server" Text='<%# Eval("deposit_no") %> '></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sys No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbrefno" runat="server" Text='<%# Eval("ref_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate><%# Eval("deposit_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cust">
                                    <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salesman">
                                    <ItemTemplate><%#Eval("salesman") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Deposit No">
                                    <ItemTemplate><%#Eval("depositNo") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Account No.">
                                    <ItemTemplate><%# Eval("acc_no") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank">
                                    <ItemTemplate><%# Eval("bank_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Payment Type">
                                    <ItemTemplate>
                                        
                                        <%# Eval("deposit_typ_nm") %>
                                        <asp:HiddenField ID="hddeposittype" Value='<%#Eval("deposit_typ") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbamount" Font-Bold="true" Font-Size="Medium" ForeColor="Red" runat="server" Text='<%# Eval("amt","{0:#,##0.00}") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate><%# Eval("dep_sta_nm") %></ItemTemplate>
                                    <ItemTemplate>
                                        <%# Eval("dep_sta_nm") %>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <div class="drop-down form-control input-sm">
                                            <asp:DropDownList ID="cbstatus" runat="server"></asp:DropDownList>
                                        </div>

                                    </EditItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" VerticalAlign="Top" />
                                    <ItemStyle HorizontalAlign="Right" />
                                    <ItemTemplate>
                                        <%# Eval("dep_sta_nm") %>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Right" />
                                    <ItemStyle HorizontalAlign="Right" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bank Date">
                                    <ItemTemplate>
                                        <div class="drop-down-date">
                                            <asp:TextBox ID="dtbank" runat="server"></asp:TextBox>
                                            <asp:CalendarExtender ID="dtbank_Extender" TargetControlID="dtbank" Format="d/M/yyyy" runat="server"></asp:CalendarExtender>
                                        </div>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File ">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="urlfile" runat="server">View File</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </ContentTemplate>
                    <%-- <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cbstatus" EventName="SelectedIndexChanged" />
                    </Triggers>--%>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="row top-devider">
            <div class="navi">
                <asp:Button ID="btnew" runat="server" Text="New" CssClass="divhisd btn-success btn btn-new" OnClick="btnew_Click" />
                <asp:Button ID="btrefresh" runat="server" Text="Refresh" OnClick="btrefresh_Click" CssClass="divhisd btn btn-refresh btn-primary" />
            </div>
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

