<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimList.aspx.cs" Inherits="fm_claimList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function OpenDetails(claim_no) {
            window.open("fm_invByProposal.aspx?number=" + claim_no);
        }
    </script>
    <style type="text/css">
        .auto-style1 {
            width: 271px;
        }

        .auto-style2 {
            width: 268px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Claim List</div>
    <div class="h-divider"></div>
    <div class="container">

        <div class="clearfix margin-bottom">
            <div class="clearfix margin-bottom">
                <div class="col-md-6 no-padding ">
                    <label class="col-md-2 col-sm-4 control-label titik-dua">Type</label>
                    <div class="col-md-10 col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddtype_SelectedIndexChanged" CssClass="form-control">
                                    <asp:ListItem Value="ALL">SELECT ALL</asp:ListItem>
                                    <asp:ListItem Value="IV">INVOICES</asp:ListItem>
                                    <asp:ListItem Value="CSH">CASH OUT</asp:ListItem>
                                    <asp:ListItem Value="CNDN">CN / DN</asp:ListItem>
                                    <asp:ListItem Value="BA">BUS. AG.</asp:ListItem>
                                </asp:DropDownList>
                                <%--<asp:RadioButtonList ID="rdbtrxn" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdbtrxn_SelectedIndexChanged" AutoPostBack="True">
                                    <asp:ListItem Value="IV">INVOICES</asp:ListItem>
                                    <asp:ListItem Value="CSH">CASH OUT</asp:ListItem>
                                    <asp:ListItem Value="CNDN">CN / DN</asp:ListItem>
                                    <asp:ListItem Value="BA">BUS. AG.</asp:ListItem>
                                </asp:RadioButtonList>--%>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddtype" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-6 no-padding margin-bottom">
                <label class="control-label col-md-2 col-sm-4 titik-dua">Product Group</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbProd_cd" runat="server" CssClass="form-control  ">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>
            </div>
            <div class="clearfix">
                <div class="col-md-6 no-padding margin-bottom">
                    <label class="control-label col-md-2 col-sm-4 titik-dua">Branch</label>
                    <div class="col-md-10 col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddBranch" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddBranch_SelectedIndexChanged"></asp:DropDownList></td>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddBranch" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-6 no-padding margin-bottom">
                    <label class="control-label col-md-2 col-sm-4 titik-dua">Month</label>
                    <div class="col-md-10 col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddMonth" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddMonth_SelectedIndexChanged">
                                    <%-- <asp:ListItem Text="=All=" Value="0"></asp:ListItem>--%>
                                    <asp:ListItem Text="January" Value="01"></asp:ListItem>
                                    <asp:ListItem Text="February" Value="02"></asp:ListItem>
                                    <asp:ListItem Text="March" Value="03"></asp:ListItem>
                                    <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                    <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                    <asp:ListItem Text="June" Value="06"></asp:ListItem>
                                    <asp:ListItem Text="July" Value="07"></asp:ListItem>
                                    <asp:ListItem Text="August" Value="08"></asp:ListItem>
                                    <asp:ListItem Text="September" Value="09"></asp:ListItem>
                                    <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                    <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                    <asp:ListItem Text="December" Value="12"></asp:ListItem>
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddMonth" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-md-6 no-padding margin-bottom">
                    <label class="control-label col-md-2 col-sm-4 titik-dua">Status</label>
                    <div class="col-md-10 col-sm-8 drop-down">
                       <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddStatus" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddStatus_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddStatus" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                 <div class="col-md-6 no-padding">
                    <label class="control-label col-md-2 col-sm-4 titik-dua">Year</label>
                    <div class="col-md-10 col-sm-8 drop-down">
                       <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddYear" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddYear_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddYear" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clearfix ">
                <div class="col-md-12 no-padding ">
                    <label class="col-md-1 col-sm-2 control-label titik-dua">Search</label>
                    <div class="col-md-11 col-sm-10 ">
                        <div class="input-group">
                            <asp:TextBox ID="txsearhc" runat="server" CssClass="form-control"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btsearch" runat="server" CssClass="btn btn-primary search" OnClick="btsearch_Click" Text="Search" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <br />
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <div class="overflow-y" style="max-height:250px; width:100%;">
                    <asp:GridView ID="gridClaim" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="4" GridLines="None" PageSize="15" CssClass="table table-striped table-hover table-fix mygrid">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Approve">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="approve" runat="server" value='<%# Eval("claim_no") %>'></asp:CheckBox></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim No.">
                                <ItemTemplate>
                                    <a href="javascript:OpenDetails('<%# Eval("claim_no") %>');">
                                        <asp:Label ID="claimNo" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim Date">
                                <ItemTemplate>
                                    <asp:Label ID="claimDate" runat="server" Text='<%# Eval("claim_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CCNR No.">
                                <ItemTemplate>
                                    <asp:Label ID="ccnrNo" runat="server" Text='<%# Eval("ccnr_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proposal Number">
                                <ItemTemplate>
                                    <asp:Label ID="prop_no" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Budget">
                                <ItemTemplate>
                                    <asp:Label ID="budget" runat="server" Text='<%# Eval("budget_prop") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotsubtotalSO" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Free Cash">
                                <ItemTemplate>
                                    <asp:Label ID="freevalue" runat="server" Text='<%# Eval("freevalue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Free Qty">
                                <ItemTemplate>
                                    <asp:Label ID="freeqty" runat="server" Text='<%# Eval("freeqty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="status" runat="server" Text='<%# Eval("statusd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit"/>
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
            <ContentTemplate>
                <div class="overflow-y" style="max-height:250px; width:100%;">

                    <asp:GridView ID="grdclaimcashout" runat="server" AutoGenerateColumns="False" ShowFooter="True"  GridLines="None" CssClass="table table-striped table-hover table-fix mygrid" PageSize="15">
                        <AlternatingRowStyle  />
                        <Columns>
                            <asp:TemplateField HeaderText="Approve">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="approve" runat="server" value='<%# Eval("claim_no") %>'></asp:CheckBox></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim No.">
                                <ItemTemplate>
                                    <a href="javascript:OpenDetails('<%# Eval("claim_no") %>');">
                                        <asp:Label ID="claimNo" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim Date">
                                <ItemTemplate>
                                    <asp:Label ID="claimDate" runat="server" Text='<%# Eval("claim_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CCNR No.">
                                <ItemTemplate>
                                    <asp:Label ID="ccnrNo" runat="server" Text='<%# Eval("ccnr_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proposal Number">
                                <ItemTemplate>
                                    <asp:Label ID="prop_no" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Budget">
                                <ItemTemplate>
                                    <asp:Label ID="budget" runat="server" Text='<%# Eval("budget_prop") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotsubtotalSO" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Free Cash">
                                <ItemTemplate>
                                    <asp:Label ID="freevalue" runat="server" Text='<%# Eval("ordervalue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="status" runat="server" Text='<%# Eval("statusd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit"/>
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
            <ContentTemplate>
                <div class="overflow-y" style="max-height:250px; width:100%;">

                    <asp:GridView ID="grdclaimcndn" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover table-fix mygrid" GridLines="None"  PageSize="15">
                        <AlternatingRowStyle/>
                        <Columns>
                            <asp:TemplateField HeaderText="Approve">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="approve" runat="server" value='<%# Eval("claim_no") %>'></asp:CheckBox></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim No.">
                                <ItemTemplate>
                                    <a href="javascript:OpenDetails('<%# Eval("claim_no") %>');">
                                        <asp:Label ID="claimNo" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim Date">
                                <ItemTemplate>
                                    <asp:Label ID="claimDate" runat="server" Text='<%# Eval("claim_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CCNR No.">
                                <ItemTemplate>
                                    <asp:Label ID="ccnrNo" runat="server" Text='<%# Eval("ccnr_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proposal Number">
                                <ItemTemplate>
                                    <asp:Label ID="prop_no" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Budget">
                                <ItemTemplate>
                                    <asp:Label ID="budget" runat="server" Text='<%# Eval("budget_prop") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotsubtotalSO" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Amount">
                                <ItemTemplate>
                                    <asp:Label ID="freevalue" runat="server" Text='<%# Eval("ordervalue") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="status" runat="server" Text='<%# Eval("statusd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit"/>
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
            <ContentTemplate>
                <div class="overflow-y" style="max-height:250px; width:100%;">

                    <asp:GridView ID="grdclaimcontract" runat="server" AutoGenerateColumns="False" ShowFooter="True" CssClass="table table-striped table-hover table-fix mygrid" GridLines="None" PageSize="15">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Approve">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="approve" runat="server" value='<%# Eval("claim_no") %>'></asp:CheckBox></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim No.">
                                <ItemTemplate>
                                    <a href="javascript:OpenDetails('<%# Eval("claim_no") %>');">
                                        <asp:Label ID="claimNo" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim Date">
                                <ItemTemplate>
                                    <asp:Label ID="claimDate" runat="server" Text='<%# Eval("claim_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CCNR No.">
                                <ItemTemplate>
                                    <asp:Label ID="ccnrNo" runat="server" Text='<%# Eval("ccnr_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proposal Number">
                                <ItemTemplate>
                                    <asp:Label ID="prop_no" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Budget">
                                <ItemTemplate>
                                    <asp:Label ID="budget" runat="server" Text='<%# Eval("budget_prop") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotsubtotalSO" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Free Qty">
                                <ItemTemplate>
                                    <asp:Label ID="freeqty" runat="server" Text='<%# Eval("freeqty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="status" runat="server" Text='<%# Eval("statusd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit"/>
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="navi">
            <asp:Button ID="btnPrint" runat="server" CssClass="btn btn-info print" Text="PRINT" OnClick="btnPrint_Click" Visible="False" />
            <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-warning save" Text="APPROVE" OnClick="btnApprove_Click" Visible="False" />
        </div>
    </div>

</asp:Content>
