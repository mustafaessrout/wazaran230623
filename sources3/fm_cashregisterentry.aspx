<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashregisterentry.aspx.cs" Inherits="fm_cashregisterentry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    
    <script>
        function RefreshData()
        {
            $get('<%=btrefresh.ClientID%>').click();
            sweetAlert('Payment closed', '', 'success');
            return (false);
        }
        function ItemSelectedsalesman_cd(sender, e) {

            $get('<%=hdsalesman_cd.ClientID%>').value = e.get_value();
        }
    </script>
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want <%if (cbinout.SelectedValue.ToString()=="O"){%>to paid<%}else{%>to received <%}%> ?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <style>
        .auto-complate-list{
            min-height:200px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdsalesman_cd" runat="server" />
   
    
        <div class="divheader">Cash Register</div>
        <div class="h-divider"></div>
        
       
        <div class="container">
            <div class="row margin-bottom">
                    <div class="col-md-3 clearfix form-group no-padding">
                    <label class="control-label col-sm-4 titik-dua">Type</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbinout" CssClass="form-control form-control-static "  runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbinout_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 clearfix form-group no-padding">
                    <label class="control-label col-sm-4 titik-dua ">Source</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbsource" CssClass="form-control form-control-static " runat="server"  AutoPostBack="True" OnSelectedIndexChanged="cbsource_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 clearfix form-group no-padding">
                    <label class="control-label col-sm-4 titik-dua">Payment</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbpaymenttype" runat="server" CssClass="form-control " AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
                <div class="col-md-3 clearfix form-group no-padding">
                    <label class="control-label col-sm-4 titik-dua">Salesman</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txsalesman_cd" runat="server" Width="100%" Height="100%" CssClass="form-control " AutoPostBack="True" OnTextChanged="txsalesman_cd_TextChanged"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txsalesman_cd_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" TargetControlID="txsalesman_cd" ServiceMethod="GetCompletionListsalesman_cd" UseContextKey="True" MinimumPrefixLength="1" CompletionSetCount="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="ItemSelectedsalesman_cd">
                            </asp:AutoCompleteExtender>
                      
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="row margin-top">
                <div class="">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                       
                    <table class="table row-no-padding mygrid" style=" overflow:auto">
                        <%--  <tr style="background-color:silver">
                            <td colspan="2" style="text-align: right">All Cashregister</td>
                            <td colspan="2">
                                <asp:Button ID="btall" runat="server" OnClick="btall_Click" Text="All Data Cashout" CssClass="divhid" />
                            </td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>--%>
                        <tr>
                            <th>Ref</th>
                            <th>Type</th>
                            <th>Item</th>
                            <th>Amount</th>
                            <th>PIC</th>
                            <th>Approval</th>
                            <th>Routine/Non Routine</th>
                            <th>Asset</th>
                            <th>Claim To HO</th>
                            <th>Remark</th>
                            <th>Add</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txref" CssClass="form-control input-sm" runat="server" Width="50px" Height="100%"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control input-sm" OnSelectedIndexChanged="cbtype_SelectedIndexChanged" AutoPostBack="True" style="min-width:110px;"></asp:DropDownList></td>
                            <td>
                                <div >
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="cbitem" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbitem_SelectedIndexChanged" style="min-width:110px;">
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="cbtype" EventName="SelectedIndexChanged"  />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            
                            </td>
                            <td>
                                <asp:TextBox ID="txamount" runat="server" Height="100%" CssClass="form-control input-sm ro" style="min-width:90px;" ></asp:TextBox>
                            </td>
                            <td style="position:relative">
                                <asp:TextBox ID="txpic" runat="server"  CssClass="form-control input-sm ro" style="min-width:120px;"  ></asp:TextBox>
                       
                                <asp:AutoCompleteExtender ID="txpic_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txpic" UseContextKey="True" 
                                        CompletionListElementID="divwidth" MinimumPrefixLength="1" EnableCaching="false"  FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="cbapproval" runat="server" CssClass="form-control input-sm ro"></asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div>
                                    <asp:DropDownList ID="cbroutine" runat="server" CssClass="form-control input-sm ro" Enabled="false">
                                        <asp:ListItem Value="R">Routine</asp:ListItem>
                                        <asp:ListItem Value="N">Non Routine</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                            </td>
                            <td>
                                <div class="full center">
                                        <asp:CheckBox ID="chasset" runat="server" CssClass="ro" />
                                </div>
                            </td>
                            <td>
                                <div class="full center">
                                    <asp:CheckBox ID="chclaim" runat="server" CssClass="ro" />
                                </div>
                            </td>
                            <td>
                                <asp:TextBox ID="txremark" runat="server" CssClass="ro" style="min-width:90px;"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-sm btn btn-success btn-add" OnClick="btadd_Click" /></td>
                        </tr>
                    </table>
                             </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="row">
                <div class="form-group clearfix margin-top padding-top">
                    <div class="col-md-3 clearfix no-padding">
                        <p class="col-sm-6 text-left titik-dua">OPEN BALANCE </p>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="col-sm-6 text-right">
                            <ContentTemplate>
                                <asp:Label ID="lbopenbalance" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-3 clearfix no-padding">
                        <p class="col-sm-6 text-left titik-dua">CASH OUT </p>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" class="col-sm-6 text-right">
                            <ContentTemplate>
                                <asp:Label ID="lbcashout" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label></td>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-3 clearfix no-padding">
                        <p class="col-sm-6 text-left titik-dua">CASH IN </p>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server" class="col-sm-6 text-right">
                            <ContentTemplate>
                                <asp:Label ID="lbcashin" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-3 clearfix no-padding">
                        <p class="col-sm-6 text-left titik-dua">CLOSING CASH </p>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" class="col-sm-6 text-right">
                            <ContentTemplate>
                                <asp:Label ID="lbcashClosing" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                  
                </div>
            </div>
            <div class="row">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0"  GridLines="None"  OnRowDataBound="grd_RowDataBound" ShowFooter="True" CssClass="table table-hover mygrid">
                            <AlternatingRowStyle />
                                <Columns>
                                    <%--<asp:TemplateField HeaderText="Ref No.">
                                        <ItemTemplate>
                       
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            <asp:Label ID="lblcash_id" runat="server" Text='<%# Eval("cash_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Manual No.">
                                        <ItemTemplate>
                                            <asp:Label ID="lbmanualno" runat="server" Text='<%# Eval("manual_no") %>'></asp:Label></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcust" runat="server"></asp:Label>
                                            </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate><%# Eval("cash_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cashout Type">
                                        <ItemTemplate><%# Eval("cashout_typ") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Item">
                                        <ItemTemplate>
                                            <asp:HiddenField ID="hditemco" runat="server" Value='<%# Eval("itemco_cd") %>' />
                                            <%# Eval("itemco_nm") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt","{0:#,##0.00}") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotamt" runat="server"></asp:Label>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approval">
                                        <ItemTemplate>
                                            <%# Eval("app_nm") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salesman_cd">
                                        <ItemTemplate>
                                            <asp:Label ID="lbSalesman_cd" runat="server" Text='<%# Eval("salesman_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate><%# Eval("cash_sta_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Doc No">
                                        <ItemTemplate>
                                            <asp:Label ID="lbdocno" runat="server" Text='<%# Eval("ref_no")%>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Remark">
                                        <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowEditButton="True" />
                                </Columns>
                                <EditRowStyle CssClass="table-edit"/>
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle  />
                                <SelectedRowStyle CssClass="table-edit"/>
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbitem" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="row">
                <div class="navi margin-bottom">
                    <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" style="display:none" />
                    <asp:LinkButton ID="btpaid" CssClass="btn btn-primary" runat="server" OnClientClick="Confirm()" OnClick="OnConfirm"> <span class="glyphicon glyphicon-play"></span></asp:LinkButton> 
                    <%-- <asp:Button ID="btpaid" runat="server" Text="Paid All" CssClass="button2 btn add" OnClick = "OnConfirm"  OnClientClick = "Confirm()" />--%>
                </div>
            </div>
        </div>
    
   
</asp:Content>

