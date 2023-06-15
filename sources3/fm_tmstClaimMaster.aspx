<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_tmstClaimMaster.aspx.cs" MasterPageFile="~/site.master" Inherits="fm_tmstClaimMaster" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
   
     <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery-1.9.1.min.js"></script>
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>
<%--     <link href="v4-alpha/bootstrap.min.css" rel="stylesheet" />
    <link href="v4-alpha/docs.min.css" rel="stylesheet" />
    <script src="v4-alpha/bootstrap.min.js"></script>
    <script src="v4-alpha/docs.min.js"></script>--%>

    <script type="text/javascript">
        function OpenPop(url, newname, settings, inv_no, received_dt, disc_cd) {
            window.open(url + "?inv=" + inv_no + "&rcv=" + received_dt + "&disc=" + disc_cd, newname, settings);
        }
    </script>
    <style>
        .ajax__tab_xp .ajax__tab_header .ajax__tab_tab {
            height: auto !important;
            color: #000 !important;
        }
    </style>
    <script>
        function EmpSelected(sender, e) {
            $get('<%=hdemp.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div>
        <ajaxToolkit:TabContainer ID="tbPromotionGroup" runat="server" Height="700px" ActiveTabIndex="0">
            <ajaxToolkit:TabPanel ID="TabPanel1" runat="server" HeaderText="PROMOTION GROUP">
                <ContentTemplate>
                    <div class="form-group">
                        <label class="control-label col-md-1" style="font-size: smaller">Kinds of Promotion</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbpromokind" runat="server" Width="15em" AutoPostBack="True" OnSelectedIndexChanged="cbpromokind_SelectedIndexChanged">
                                        <asp:ListItem Value="ATL">ATL</asp:ListItem>
                                        <asp:ListItem Value="BTL">BTL</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-1" style="font-size: smaller">Promotion Code</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPromo_cd" runat="server" MaxLength="2"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-1" style="font-size: smaller">Promotion Group</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPromo_nm" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdPromotionGroup" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" CssClass="mygrid"
                                    OnRowCancelingEdit="grdPromotionGroup_RowCancelingEdit" OnRowDeleting="grdPromotionGroup_RowDeleting">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Promo Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPromo_cd" runat="server" Text='<%# Eval("promo_cd") %>'></asp:Label>
                                                <asp:HiddenField ID="hdPromokind" runat="server" Value='<%# Eval("promokind") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Promo Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPromo_nm" runat="server" Text='<%# Eval("promo_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField HeaderText="Action" ShowEditButton="false" ShowHeader="true" ShowDeleteButton="true" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:Button ID="btSavePromotionGroup" runat="server" Text="Add Promotion Group" CssClass="btn btn-success" OnClick="btSavePromotionGroup_Click" />
                </ContentTemplate>

            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel2" runat="server" HeaderText="PROMOTION TYPE">
                <ContentTemplate>
                    <div class="form-group">
                        <label class="control-label col-md-1" style="font-size: smaller">Promotion Group</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlPromotionGroup" runat="server" Width="15em" AutoPostBack="True" OnSelectedIndexChanged="ddlPromotionGroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-1" style="font-size: smaller">Promotion Type Code</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPromotionType" runat="server" MaxLength="2"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-1" style="font-size: smaller">Promotion Type Name</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtPromotionTypeName" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvPromotionType" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" CssClass="mygrid"
                                    OnRowCancelingEdit="gvPromotionType_RowCancelingEdit" OnRowDeleting="gvPromotionType_RowDeleting">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Promo Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPromo_cd" runat="server" Text='<%# Eval("promoGrpName") %>'></asp:Label>
                                                <asp:HiddenField ID="hdPromokind" runat="server" Value='<%# Eval("promo_cd") %>' />
                                                <asp:HiddenField ID="hdfPromoType" runat="server" Value='<%# Eval("promo_typ") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Promo Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPromo_nm" runat="server" Text='<%# Eval("promotypName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField HeaderText="Action" ShowEditButton="false" ShowHeader="true" ShowDeleteButton="true" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:Button ID="btnPromotionType" runat="server" Text="Add Promotion Type" CssClass="btn btn-success" OnClick="btnPromotionType_Click" />

                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel3" runat="server" HeaderText="PROPOSAL SIGN BY PRINCIPAL">
                <ContentTemplate>
                    <div class="form-group clearfix">
                        <label class="control-label col-md-2" style="font-size: smaller">Principal (Vendor)</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbvendor" runat="server" Width="15em" AutoPostBack="True" OnSelectedIndexChanged="cbvendor_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <div class="form-group clearfix">
                        <label class="control-label col-md-2" style="font-size: smaller">PROPOSAL SIGN BY PRINCIPAL</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePane19" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="rdProposalSignByPrincipal" runat="server" AutoPostBack="true" Width="600px" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdProposalSignByPrincipal_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="MARKETMGR" Text="Marketing Mgr"></asp:ListItem>
                                        <asp:ListItem Value="FINDEP" Text="Finance Dept."></asp:ListItem>
                                        <asp:ListItem Value="NSPM" Text="NSPM"></asp:ListItem>
                                        <asp:ListItem Value="MARKETDIR" Text="Marketing Director"></asp:ListItem>
                                        <asp:ListItem Value="GMDIR" Text="GM/Fin Director"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <br />
                    <div class="form-group clearfix">
                        <label class="control-label col-md-2" style="font-size: smaller">Proposal Principal Name According Vender</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtProposalPrincipalName" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvProposalPrincipal" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" CssClass="mygrid"
                                    OnRowCancelingEdit="gvProposalPrincipal_RowCancelingEdit" OnRowDeleting="gvProposalPrincipal_RowDeleting">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Position">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("position") %>'></asp:Label>
                                                <asp:HiddenField ID="hdfPosition" runat="server" Value='<%# Eval("position") %>' />
                                                <asp:HiddenField ID="hdfidv" runat="server" Value='<%# Eval("idv") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Full Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblFull_nm" runat="server" Text='<%# Eval("fullname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField HeaderText="Action" ShowEditButton="false" ShowHeader="true" ShowDeleteButton="true" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:Button ID="Button1" runat="server" Text="Add Principal" CssClass="btn btn-success" OnClick="btnPrincipal_Click" />

                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="PROPOSAL SIGN BY SBTC">
                <ContentTemplate>

                    <div class="form-group clearfix">
                        <label class="control-label col-md-2" style="font-size: smaller">PROPOSAL SIGN BY SBTC</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:RadioButtonList ID="rdProposalSignBySBTC" runat="server" AutoPostBack="true" Width="900px" RepeatDirection="Horizontal"
                                        OnSelectedIndexChanged="rdProposalSignBySBTC_SelectedIndexChanged">
                                        <asp:ListItem Selected="True" Value="APC" Text="A & P Coordinator"></asp:ListItem>
                                        <asp:ListItem Value="CAP" Text="Claim (A & P) Dept Head"></asp:ListItem>
                                        <asp:ListItem Value="PRODMGR" Text="Product Manager"></asp:ListItem>
                                        <asp:ListItem Value="KAMGR " Text="Key Account Mgr"></asp:ListItem>
                                        <asp:ListItem Value="GMSBTC" Text="GM/Deputy GM SBTC"></asp:ListItem>
                                        <asp:ListItem Value="MARKETMGR" Text="Marketing Manager"></asp:ListItem>
                                    </asp:RadioButtonList>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                    <br />
                    <div class="form-group clearfix">
                        <label class="control-label col-md-2" style="font-size: smaller">Proposal Employee</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                  

                                     <asp:TextBox ID="txname" runat="server" CssClass="form-control input-sm" Height="100%"  Width="500px"></asp:TextBox>
                    <ajaxToolkit:AutoCompleteExtender ID="txname_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" 
                        TargetControlID="txname" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="10" CompletionInterval="1" 
                        MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" 
                        OnClientItemSelected="EmpSelected">
                    </ajaxToolkit:AutoCompleteExtender>
                    <asp:HiddenField ID="hdemp" runat="server" />
                                   <%-- <span class="input-group-btn">Status:
                                    <button type="submit" class="btn btn-primary" runat="server" id="btadd" onserverclick="btadd_Click">
                                        <i class="glyphicon glyphicon-search" aria-hidden="true"></i>
                                    </button>
                                    </span>--%>


                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvProposalSBTC" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" CssClass="mygrid"
                                    OnRowCancelingEdit="gvProposalSBTC_RowCancelingEdit" OnRowDeleting="gvProposalSBTC_RowDeleting">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Position">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPosition" runat="server" Text='<%# Eval("job_cdValue") %>'></asp:Label>
                                                <asp:HiddenField ID="hdfJobCD" runat="server" Value='<%# Eval("job_cd") %>' />
                                                <asp:HiddenField ID="hdfids" runat="server" Value='<%# Eval("ids") %>' />
                                                <asp:HiddenField ID="hdfEMPCD" runat="server" Value='<%# Eval("emp_cd") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblEmpName" runat="server" Text='<%# Eval("empname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField HeaderText="Action" ShowEditButton="false" ShowHeader="true" ShowDeleteButton="true" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                     <%-- <asp:DropDownList ID="cbProposalEmployee" runat="server" Width="20em">
                                    </asp:DropDownList>--%>
                    <%--<div id="divw" style="font-family:Calibri;font-size:small"></div>--%>
                    <asp:Button ID="Button2" runat="server" Text="Add SBTC Employee" CssClass="btn btn-success" OnClick="btnSBTCEmployee_Click" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="PROPOSAL STATUS">
                <ContentTemplate>

                    <div class="form-group">
                        <label class="control-label col-md-1" style="font-size: smaller">Proposal Status</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtProposalStatus" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvProposalStatus" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" CssClass="mygrid"
                                    OnRowCancelingEdit="gvProposalStatus_RowCancelingEdit" OnRowDeleting="gvProposalStatus_RowDeleting">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Promo Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPromo_cd" runat="server" Text='<%# Eval("status_no") %>'></asp:Label>
                                                <asp:HiddenField ID="hdStatusNo" runat="server" Value='<%# Eval("status_no") %>' />
                                                <asp:HiddenField ID="hdfStatusName" runat="server" Value='<%# Eval("status_nm") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPromo_nm" runat="server" Text='<%# Eval("status_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField HeaderText="Action" ShowEditButton="false" ShowHeader="true" ShowDeleteButton="true" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:Button ID="Button3" runat="server" Text="Add Proposal Status" CssClass="btn btn-success" OnClick="btProposalStatus_Click" />
                </ContentTemplate>
            </ajaxToolkit:TabPanel>
            <ajaxToolkit:TabPanel ID="TabPanel6" runat="server" HeaderText="CLAIM ISSUE">
                <ContentTemplate>
                    <div class="form-group">
                        <label class="control-label col-md-1" style="font-size: smaller">Group</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbgroup" runat="server" Width="15em" AutoPostBack="True" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-1" style="font-size: smaller">Issue Code</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtIssueCode" runat="server" MaxLength="4"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="form-group">
                        <label class="control-label col-md-1" style="font-size: smaller">Issue</label>
                        <div class="col-md-2">
                            <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txtSubGroup" runat="server"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="gvIssue" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" EmptyDataText="NO DATA" CssClass="mygrid"
                                    OnRowCancelingEdit="gvIssue_RowCancelingEdit" OnRowDeleting="gvIssue_RowDeleting">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Promo Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lblissue_No" runat="server" Text='<%# Eval("issue_No") %>'></asp:Label>
                                                <asp:HiddenField ID="hdfIssue_No" runat="server" Value='<%# Eval("issue_no") %>' />
                                                <asp:HiddenField ID="hdfissue_nm" runat="server" Value='<%# Eval("issue_nm") %>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Issue Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lblissue_nm" runat="server" Text='<%# Eval("issue_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField HeaderText="Action" ShowEditButton="false" ShowHeader="true" ShowDeleteButton="true" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:Button ID="Button4" runat="server" Text="Add Issue" CssClass="btn btn-success" OnClick="btnIssue_Click" />

                </ContentTemplate>
            </ajaxToolkit:TabPanel>
        </ajaxToolkit:TabContainer>
    </div>
</asp:Content>

