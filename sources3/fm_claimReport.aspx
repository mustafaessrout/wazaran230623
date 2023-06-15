<%@ Page Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimReport.aspx.cs" Inherits="fm_claimReport" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function OpenDetails(claim_no, type) {
            window.open("fm_invByProposal.aspx?number=" + claim_no + "&type=" + type);
        }

  <%--      function RefreshData(proposal_no, benefit, disc_cd) {
            $get('<%=txtProposal.ClientID%>').value = proposal_no;
            $get('<%=disc_cd.ClientID%>').value = disc_cd;
            
        }--%>
        function RefreshData(claim_no) {
            $get('<%=txclaimno.ClientID%>').value = claim_no;
            $get('<%=btsl.ClientID%>').click();
        }
        function RefreshProduct(product_cd, product_nm) {
            $get('<%=txitemsearch.ClientID%>').value = product_nm;
            $get('<%=hditem.ClientID%>').value = product_cd;

        }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">Claim Report</div>

    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Branch</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddBranch" runat="server" AutoPostBack="true" CssClass="form-control"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddBranch" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Month</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddMonth" runat="server" AutoPostBack="true" CssClass="form-control">
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
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Status</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddStatus" runat="server" AutoPostBack="true" CssClass="form-control">
                                <%--<asp:ListItem Text="PENDING" Value="P"></asp:ListItem>--%>
                                <asp:ListItem Text="APPROVED" Value="A" Selected="True"></asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddStatus" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Year</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddYear" runat="server" AutoPostBack="true" CssClass="form-control">
                                
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddYear" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Claim Type</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbtype" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="cbtype_SelectedIndexChanged">                                
                                <asp:ListItem Text="INVOICE CLAIM" Value="0"></asp:ListItem>
                                <asp:ListItem Text="BUSINESS AGREEMENT CLAIM" Value="1"></asp:ListItem>
                                <asp:ListItem Text="CREDIT NOTE CLAIM" Value="2"></asp:ListItem>
                                <asp:ListItem Text="CASHOUT CLAIM" Value="3"></asp:ListItem>
                                <asp:ListItem Text="MONTHLY CLAIM" Value="4"></asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbtype" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Report</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddReportType" runat="server" AutoPostBack="true" CssClass="form-control" OnSelectedIndexChanged="ddReportType_SelectedIndexChanged">
                                <%--                        <asp:ListItem Text="0 - Monthly Claim" Value="Monthly"></asp:ListItem>
                        <asp:ListItem Text="1 - Free Goods" Value="FG"></asp:ListItem>
                        <asp:ListItem Text="2 - Discount" Value="DC"></asp:ListItem>
                        <asp:ListItem Text="3 - Tactical Bonus" Value="TB"></asp:ListItem>
                        <asp:ListItem Text="4 - Gondola Display" Value="GD"></asp:ListItem>
                        <asp:ListItem Text="5 - Sample Promotion" Value="SP"></asp:ListItem>
                        <asp:ListItem Text="6 - Leaflet Promotion" Value="LP"></asp:ListItem>--%>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddReportType" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:TextBox ID="so_cd" runat="server" Style="display: none"></asp:TextBox>
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Principle</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbprincible" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <label class="control-label col-sm-2">Report Department</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbdep" runat="server" Enabled="False" CssClass="form-control">
                                <asp:ListItem Value="0">Claim Status</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-sm-10 col-sm-offset-2">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:CheckBox ID="chrep" runat="server" AutoPostBack="True" OnCheckedChanged="chrep_CheckedChanged" CssClass="checkbox" Text="Activate" />
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="chrep" EventName="CheckedChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-6 clearfix form-group">
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbldisc" runat="server" Text="Claim NO." Visible="False" CssClass="control-label col-sm-2"></asp:Label>
                        <div class="col-sm-10">
                            <div class="input-group">
                                <asp:TextBox ID="txclaimno" runat="server" CssClass="makeitreadonly form-control" Visible="False" AutoPostBack="True"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:Button ID="btsearchso" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearchso_Click" Visible="False" />
                                </div>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="row padding-top padding-bottom">
            <div class="col-sm-6 clearfix form-group">
                <asp:TextBox ID="txitemsearch" runat="server" CssClass="makeitreadonly ro form-control margin-bottom" Rows="3" TextMode="MultiLine" ReadOnly="True" Visible="false"></asp:TextBox>
                <ajaxToolkit:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txitemsearch" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="divwidthi">
                </ajaxToolkit:AutoCompleteExtender>
                <asp:Button ID="searchID" runat="server" CssClass="btn-primary btn btn-search pull-right" Text="Search" OnClick="btsearchProduct_Click" Visible="false" />
                <asp:HiddenField ID="hditem" runat="server" />
            </div>
            <div class="col-sm-6 clearfix form-group">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" EmptyDataText="No files available">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:CheckBox ID="chkSelect" runat="server" />
                                <asp:Label ID="lblFilePath" runat="server" Text='<%# Eval("fileloc") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="fileloc" HeaderText="File Name" />
                        <asp:TemplateField HeaderText="Document Name">
                            <ItemTemplate>
                                <asp:Label ID="lbdoc_nm" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
                <br />
                <asp:Button ID="btnDownload" runat="server" Text="Download" OnClick="DownloadFiles" Visible="false" CssClass="btn btn-primary btn-download" />
                <asp:Button ID="btsl" runat="server" OnClick="btsl_Click" Style="display: none;" />
            </div>
        </div>

        <div class="row navi">
            <asp:Button ID="btnPrint" runat="server" CssClass="btn-info btn btn-print" Text="Print Report" OnClick="btnPrint_Click" />
        </div>
    </div>




    <br />


    <br />

</asp:Content>
