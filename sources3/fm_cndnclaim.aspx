<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cndnclaim.aspx.cs" Inherits="fm_cndnclaim" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function PropSelected(sender, e)
        {
            $get('<%=hdprop.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function RefreshData(sval)
        {
            $get('<%=txcust.ClientID%>').value = sval;
            $get('<%=hdcust.ClientID%>').value = sval;
            $get('<%=btsearchinv.ClientID%>').click();
        }

        function SelectClaim(sClaim)
        {
            $get('<%=hdclaim.ClientID%>').value = sClaim;
            $get('<%=btclaim.ClientID%>').click();
        }

        function SelectProposal(sProp)
        {
            $get('<%=hdprop.ClientID%>').value = sProp;
            $get('<%=btsearch.ClientID%>').click();
        }

        function ContractSelected(scontract) {
            $get('<%=hdcontract.ClientID%>').value = scontract;
            $get('<%=txcontract.ClientID%>').value = scontract;
            $get('<%=btsearch3.ClientID%>').click();
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdprop" runat="server" />
    <asp:HiddenField ID="hdrdcust" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hdclaim" runat="server" />
    <asp:HiddenField ID="hdcontract" runat="server" />

    <div class="divheader">Credit Note Claim Request</div>

    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="col-xs-12 form-horizontal">

            <div class="form-group">
                 <label class="control-label col-md-1">CN No.</label>
                <div class="col-md-5">
                    <div class="input-group col-md-12">
                        <asp:TextBox ID="txcnno" runat="server" CssClass="form-control ro" Enabled ="false"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearchcn" runat="server" CssClass="btn btn-primary" OnClick="btsearchcn_Click"><span class="fa fa-search"></span></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <div id="vContract" runat="server">
                    <label class="col-md-1 control-label">Contract</label>
                    <div class="col-md-5">                 
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="input-group">
                            <ContentTemplate>
                                <asp:TextBox ID="txcontract" runat="server" CssClass="form-control input-sm"  OnTextChanged="txcontract_TextChanged"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btsearchcontract" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchcontract_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>                            
                    </div>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Proposal</label>
                <div class="col-md-5">
                    <div class="input-group col-md-12">
                    <asp:TextBox ID="txproposal" runat="server" CssClass="form-control input-group-sm"  ></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txproposal_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txproposal" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="PropSelected" CompletionListCssClass="divcust">
                    </asp:AutoCompleteExtender>
                         <div class="input-group-btn">
                            <asp:LinkButton ID="btse" runat="server" CssClass="btn btn-primary" OnClick="btse_Click"><span class="fa fa-search"></span></asp:LinkButton>
                         </div>
                    </div>
                </div>
                <label class="control-label col-md-1">Remark</label>
                <div class="col-md-5">
                    <asp:TextBox ID="txremark" runat="server"   CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Prop Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtprop" runat="server" CssClass="form-control"  ></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtprop_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtprop">
                    </asp:CalendarExtender>
                </div>
                <label class="control-label col-md-1">Start Date</label>
               <div class="col-md-2">
                    <asp:TextBox ID="dtstart" runat="server" CssClass="form-control"  ></asp:TextBox>
                   <asp:CalendarExtender CssClass="date" ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                    </asp:CalendarExtender>
                </div>
                  <label class="control-label col-md-1">End Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtend" runat="server" CssClass="form-control"  ></asp:TextBox>
                    <asp:CalendarExtender CssClass="date" ID="dtend_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtend">
                    </asp:CalendarExtender>
                </div>
                  <label class="control-label col-md-1">Dlvry Date</label>
               <div class="col-md-2">
                    <asp:TextBox ID="dtdelivery" runat="server" CssClass="form-control"  ></asp:TextBox>
                   <asp:CalendarExtender CssClass="date" ID="dtdelivery_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtdelivery">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Dest Prop</label>
                <div class="col-md-2">
                    <asp:Label ID="lbcust" runat="server" CssClass="form-control ro"></asp:Label>
                </div>
                <label class="control-label col-md-1">Participant</label>
                <div class="col-md-2 drop-down">
                   <asp:DropDownList ID="cbcode" runat="server" CssClass="form-control "></asp:DropDownList>
                     
                </div>
                 <label class="control-label col-md-1">Customer</label>
                 <div class="col-md-2">
                      <div class="input-group">
                 
                        <asp:TextBox ID="txcust" runat="server" CssClass="form-control"  ></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txcust" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                     
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearchcust" runat="server" CssClass="btn btn-primary" OnClick="btsearchcust_Click"><span class="fa fa-search"></span></asp:LinkButton>
                        </div>
                    </div>
                 </div>
                 <label class="control-label col-md-1">Budget</label>
                <div class="col-md-2">
                    <asp:Label ID="lbbudget" runat="server" CssClass="form-control ro"></asp:Label>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Vat</label>
                <div class="col-md-2">
                    <asp:RadioButtonList ID="rdVat" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal">
                        <asp:ListItem Value="1">On</asp:ListItem>
                        <asp:ListItem Value="0">Off</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
                <label class="control-label col-md-1">Payment Type</label>
                <div class="col-md-2 drop-down">
                   <asp:DropDownList ID="cbpaymentType" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbpaymentType_SelectedIndexChanged" AutoPostBack="true">
                       <asp:ListItem Value="F">Full</asp:ListItem>
                       <asp:ListItem Value="P">Partial</asp:ListItem>
                   </asp:DropDownList>                     
                </div>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                <div id="totAmount" runat="server">
                    <label class="control-label col-md-1">Total Amount</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txTotalAmount" runat="server" CssClass="form-control"  ></asp:TextBox>
                    </div>
                    <div class="col-md-2">
                        <asp:Button ID="btnapplyamount" runat="server" OnClick="btnapplyamount_Click" Text="Apply Amount" CssClass="divhid" />
                    </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
                <label class="control-label col-md-1">Remain Budget</label>
                <div class="col-md-2">
                    <asp:Label ID="lbremain" runat="server" CssClass="form-control ro"></asp:Label>
                </div>
            </div>

            <div class="form-group">
                <asp:UpdatePanel runat="server" ID="uploadDoc">
                <ContentTemplate>
                <div class="h-divider"></div>
                <div class="clearfix margin-bottom">             
                    <label class="col-md-1 col-sm-4 control-label">Upload Document Sales</label>
                    <div class="col-md-11 col-sm-8">
                        <asp:FileUpload ID="upl" runat="server"></asp:FileUpload>
                        <asp:HyperLink ID="hpfile_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                        <asp:Label ID="lblocfile" runat="server" Text='Sales Document'></asp:Label></asp:HyperLink>
                    </div>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                              <asp:GridView ID="grdinv" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="mygrid table table-hover" EmptyDataText="No Invoice Found" OnRowEditing="grdinv_RowEditing" OnRowUpdating="grdinv_RowUpdating" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Inv No">
                            <ItemTemplate>
                                    <asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Man No">
                                <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate><%# Eval("inv_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate><%# Eval("due_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust">
                                <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salesman">
                                <ItemTemplate><%# Eval("salesman_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot Amt">
                                <ItemTemplate><%# Eval("totamt") %></ItemTemplate>
                            </asp:TemplateField>
                           <asp:TemplateField HeaderText="Temp Balance">
                                <ItemTemplate>
                                    <asp:Label ID="lbremain" runat="server" Text='<%# Eval("tempbalance") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remain">
                                <ItemTemplate>
                                    <asp:Label ID="lbbalance" runat="server" Text='<%# Eval("balance") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vat">
                                <ItemTemplate>
                                    <asp:Label ID="lbat" runat="server" Text='<%# Eval("vat") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To Be Paid">
                                <ItemTemplate>
                                    <asp:Label ID="lbpaid" runat="server" Text='<%# Eval("amt") %>'></asp:Label></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txpaid" runat="server" Width="5em" CssClass="form-control input-sm"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotpaid" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            
                            <asp:CommandField ShowEditButton="True" />
                        </Columns>
                         <EditRowStyle CssClass="table-edit" />
                       <FooterStyle CssClass="table-footer" />
                       <HeaderStyle CssClass="table-header"/>
                       <PagerStyle CssClass="table-page"/>
                       <RowStyle  />
                       <SelectedRowStyle CssClass="table-edit"/>
                       <SortedAscendingCellStyle BackColor="#E9E7E2" />
                       <SortedAscendingHeaderStyle BackColor="#506C8C" />
                       <SortedDescendingCellStyle BackColor="#FFFDF8" />
                       <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                        <asp:GridView ID="grdviewinv" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="mygrid table table-hover" EmptyDataText="No Invoice Found" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Inv No">
                            <ItemTemplate>
                                    <asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Man No">
                                <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate><%# Eval("inv_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate><%# Eval("due_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust">
                                <ItemTemplate><%# Eval("cust_desc") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salesman">
                                <ItemTemplate><%# Eval("salesman_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amt">
                                <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Vat">
                                <ItemTemplate>
                                    <asp:Label ID="lbat" runat="server" Text='<%# Eval("vat") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot Amt + Vat">
                                <ItemTemplate>
                                    <asp:Label ID="lbat" runat="server" Text='<%# Eval("totamt") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                            <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header"/>
                        <PagerStyle CssClass="table-page"/>
                        <RowStyle  />
                        <SelectedRowStyle CssClass="table-edit"/>
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  
                </div>
            </div>
            <div class="h-divider"></div>
           
        </div>
    </div>
    <div class="navi margin-top margin-bottom">
         <asp:LinkButton ID="btnew" runat="server" OnClick="btnew_Click" CssClass="btn btn-success ">New</asp:LinkButton>
         <asp:LinkButton ID="btsave" CssClass="btn btn-primary" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
        <asp:LinkButton ID="btapprove" CssClass="btn btn-primary" runat="server" OnClick="btapprove_Click">Approve</asp:LinkButton>
        <asp:LinkButton ID="btreject" CssClass="btn btn-danger" runat="server" OnClick="btreject_Click">Reject</asp:LinkButton>
        <asp:LinkButton ID="btprint" CssClass="btn btn-info" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
         <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" CssClass="divhid" />
        <asp:Button ID="btsearchinv" runat="server" OnClick="btsearchinv_Click" Text="Button" CssClass="divhid" />
         <asp:Button ID="btclaim" runat="server" OnClick="btclaim_Click" Text="Button" CssClass="divhid" />
        <asp:Button ID="btsearch3" runat="server" Text="Button" OnClick="btsearch3_Click" CssClass="divhid" />
    </div>
    <div id="divcust" style="font-size:smaller;font-family:Calibri"></div>
</asp:Content>

