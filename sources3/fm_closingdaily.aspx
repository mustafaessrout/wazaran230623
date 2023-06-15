<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_closingdaily.aspx.cs" Inherits="fm_closingdaily" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link rel="stylesheet" href="css/animate.css" />
    <link rel="stylesheet" href="css/font-awesome.min.css" />

    <script>
        function vEnableShow() {
            $get('showmessagex').className = "showmessage";
            //  alert('mpret');
        }

        function vDisableShow() {
            $get('showmessagex').className = "hidemessage";
        }
    </script>
    <style>
        .showmessage {
            position: fixed;
            top: 50%;
            left: 50%;
            margin-top: -60px;
            margin-left: -60px;
            border-radius: 10px;
            width: 125px;
            height: 125px;
            background: url(loader.gif) fixed center;
            display: normal;
        }

        .hidemessage {
            position: absolute;
            top: 50%;
            left: 50%;
            margin-top: 0px;
            margin-left: 0px;
            width: 150px;
            height: 150px;
            background: url(loader.gif) no-repeat center;
            display: none;
        }

        .modal-dialog.modal-size {
            width: 1200px;
            margin: auto;
        }
    </style>

    <script type="text/javascript">
        function openModal() {
            $('#myModal').modal('show');
        }
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="divheader">
        Closing Daily&nbsp; For Date :
        <asp:Label ID="lbdate" runat="server" Text="Label"></asp:Label>
    </div>
    <div class="h-divider"></div>
    <div class="clearfix">
        <div class="col-md-12">
            <table class="full">
                <tr>
                    <td class="col-sm-6 margin-bottom">
                        <table style="width: 100%">
                            <tr>
                                <td style="margin-left: 80px" class="auto-style1 checkbox no-margin">
                                    <asp:CheckBox ID="chprebatch" runat="server" Text="Pre-batch checking" Enabled="False" /></td>
                            </tr>
                            <tr>
                                <td class="auto-style1 checkbox no-margin" style="margin-left: 80px">
                                    <asp:CheckBox ID="chbackup" runat="server" Enabled="False" Text="Backup Database Before Process" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1 checkbox no-margin" style="margin-left: 80px">
                                    <asp:CheckBox ID="chCustomerPrice" runat="server" Enabled="False" Text="Process Customer Price Changed" />
                                </td>
                            </tr>
                            <tr>
        <td style="margin-left: 80px" class="auto-style1 checkbox no-margin">
            <asp:CheckBox ID="chcashier" runat="server" Text="Process Cashier " Enabled="False" /></td>
    </tr>
                            <tr>
                                <td style="margin-left: 80px" class="auto-style1 checkbox no-margin">
                                    <asp:CheckBox ID="chstock" runat="server" Text="Process Stock Movement" Enabled="False" /></td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="auto-style1 checkbox no-margin">
                                    <asp:CheckBox ID="chrtu" runat="server" Text="Process Return Payment Except KA" Enabled="False" /></td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chclaim" runat="server" Text="Claim Process which end period more than 10days" Enabled="False" /></td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chcdnd" runat="server" Enabled="False" Text="Process CN/DN if apprv by HO" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chdocnotapp" runat="server" Enabled="False" Text="Process doc not apply if period scheme started" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chstockin" runat="server" Enabled="False" Text="Process stock in after 2 days HO Apprv" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chstockmov" runat="server" Enabled="False" Text="Process All condition of daily stock mov &amp; AP" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="charcond" runat="server" Enabled="False" Text="Process All cond of AR, Cust Balance, AR" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chbadstock" runat="server" Enabled="False" Text="Proces s bad stock / Destroy in the  end of month" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chrequest" runat="server" Enabled="False" Text="Claim Process which Daily Confirmation when Invoice Received or Cashout / CNDN Approved" />
                                </td>
                            </tr>
                        </table>

                    </td>
                    <td class="col-sm-6">
                        <table>
                         
                            
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chSOA" runat="server" Enabled="False" Text="Statement Of Account " />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chcancelTO" runat="server" Text="Cancel Take Order if &gt; 7 days" Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chturnoffdiscount" runat="server" Text="Turn Off Discount" Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chcleanwrk" runat="server" Text="Clean Working Table" Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chandroid" runat="server" Text="Device Tab Task" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chnotifiedinv" runat="server" Text="Sent Email Invoice more 5 days pending" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chnotifiedbadstock" runat="server" Text="Sent Email For Badstock" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="cbCleanSmsOutbo" runat="server" Text="Clean Unused SMS Outbox" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chcollfreeitem" runat="server" Text="Collecting  Free Item" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chkdate" runat="server" Text="Advanced Wazaran Date" Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chweeklycashier" runat="server" Text="Weekly Cashier Report" Enabled="False" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">
                                    <asp:CheckBox ID="chbackupafter" runat="server" Enabled="False" Text="Backup Database After Process" />
                                </td>
                            </tr>
                            <tr>
                                <td style="margin-left: 80px" class="checkbox no-margin">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

          
        </div>
        <br />  
        <div class="col-md-12 margin-top">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdDepositConfirm" runat="server" AutoGenerateColumns="False" GridLines="None" CssClass="table table-hover mygrid margin-bottom full">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcode" runat="server" Text='<%# Eval("code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="WAZARAN SYS">
                                <ItemTemplate>
                                    <asp:Label ID="lbbranch" runat="server" Text='<%# Eval("branch") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="HO SYS">
                                <ItemTemplate>
                                    <asp:TextBox ID="txho" runat="server" CssClass="form-control"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transaction">
                                <ItemTemplate>
                                    <asp:Label ID="lbTransaction" runat="server" Text='<%# Eval("name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Transaction Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbDescriptionValue" runat="server" Text='<%# Eval("DescriptionValue") %>' style="text-align:left;"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F5F7FB" />
                        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                        <SortedDescendingCellStyle BackColor="#E9EBEF" />
                        <SortedDescendingHeaderStyle BackColor="#4870BE" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="navi margin-bottom">
        <div class="btn-group">
            <asp:Button ID="btnValClosingDaily" runat="server" Text="VALIDATE Closing Daily" OnClick="btnValClosingDaily_Click" CssClass="btn btn-primary btn-strat show-modal" />
            <asp:Button ID="btstart" runat="server" Text="START Closing Daily" OnClick="btstart_Click" OnClientClick="vEnableShow();" CssClass="btn btn-primary btn-strat" Enabled="false"  />
        </div>
    </div>

    <div class="divgrid">
    </div>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div id="showmessagex" class="hidemessage">
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="modal fade" id="myModal" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true" data-keyboard="false" data-backdrop="static">
        <div id="modal_dialog" runat="server" class="modal-dialog modal-size" style="height: 615px; overflow-y: auto; margin: auto; top: 38px;">
            <asp:UpdatePanel ID="upModal" runat="server" ChildrenAsTriggers="false" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="modal-content">
                        <div class="modal-header">
                            <h4 class="jajarangenjang" style="margin-left: 40px">Closing Daily Validation</h4>
                            <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;</button>
                            <span aria-hidden="true"></span>
                            </button>
                        </div>
                        <div id="modal_body" runat="server" class="modal-body" style="height: 399px; overflow-y: auto; margin: auto;">
                            <div class="container-fluid">
                                <div class="row">
                                    <div class="form-group">
                                        <div id="tbl" runat="server" class="table">
                                            <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False" CellPadding="2" Width="100%" OnDataBound="grd_DataBound">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Department">
                                                        <ItemTemplate>
                                                            <asp:Label ID="DepName" runat="server" Text='<%# Eval("DepartmentName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Closing Daily Items">
                                                        <ItemTemplate>
                                                            <%-- <asp:HiddenField ID="hdids" Value='<%# Eval("infoID") %>' runat="server" />--%>
                                                            <asp:Label ID="validTitle" runat="server" Text='<%# Eval("validationTitle") %>' ></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Status">
                                                        <ItemTemplate>
                                                            <asp:Label ID="validVal" runat="server" Text='<%# Eval("validationVal") %>' Width="870px"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div class="navi margin-bottom">
                                            <asp:Label ID="lballdone" runat="server" Style="align-items: center" ForeColor="Green" Enabled="false" Font-Bold="true" Font-Size="Medium" CssClass="StrongText"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <div class="navi margin-bottom">
                                <div class="btn-group">
                                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="btn btn-primary btn-strat show-modal" OnClick="btnRefresh_Click" />
                                    <asp:Button ID="btnCleared" runat="server" Text="START Closing Daily" OnClick="btnCleared_Click" CssClass="btn btn-primary btn-done" Enabled="false" data-dismiss="#myModal" aria-hidden="true" />
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

