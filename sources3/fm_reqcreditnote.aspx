<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reqcreditnote.aspx.cs" Inherits="fm_reqcreditnote" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_reqcreditnote.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }

        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btinv.ClientID%>').click();
        }
    </script>
 
    <style>
        .auto-complate-list{
            min-width:250px !important;
        }
        .table-edit input[type="text"]{
                display: block;
                width: 100%;
                height: 34px;
                padding: 6px 12px;
                font-size: 14px;
                line-height: 1.42857143;
                color: #555;
                background-color: #fff;
                background-image: none;
                border: 1px solid #ccc;
                border-radius: 4px;
                -webkit-box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
                box-shadow: inset 0 1px 1px rgba(0,0,0,.075);
                -webkit-transition: border-color ease-in-out .15s,-webkit-box-shadow ease-in-out .15s;
                -o-transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
                transition: border-color ease-in-out .15s,box-shadow ease-in-out .15s;
                height: 30px;
                padding: 5px 10px;
                font-size: 12px;
                line-height: 1.5;
                border-radius: 3px;
                border-color: #a09696;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Credit Debit Note Request</div>
    <div class="h-divider"></div>

    <div class="container">
        <div class="row clearfix margin-bottom" >
            <div class="clearfix col-md-4 col-sm-6 margin-bottom">
                <label class="control-label col-sm-4 titik-dua">Status</label>
                <div class="col-sm-8">
                      <asp:Label ID="lbstatus" runat="server" CssClass="form-control text-red"></asp:Label>
                </div>
            </div>
           <div class="clearfix col-md-4 col-sm-6 margin-bottom">
               <asp:UpdatePanel runat="server">
                   <ContentTemplate>
                        <label class="control-label col-sm-4 titik-dua">Request No</label>
                            <div class="col-sm-8">
                                <div class="input-group">
                                     <asp:TextBox ID="txreqno"  runat="server" CssClass="form-control input-group-sm">NEW</asp:TextBox>
                                     <div class="input-group-btn">
                                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" OnClientClick="openwindow();return(false);" runat="server"><i class="fa fa-search"></i></asp:LinkButton>
                                     </div>
                                </div>
                            </div>
                   </ContentTemplate>
               </asp:UpdatePanel>
            </div>
           
            <div class="clearfix col-md-4 col-sm-6 margin-bottom">
                <label class="control-label col-sm-4 titik-dua">Date</label>
                <div class="col-sm-8">
                    <asp:Label ID="lbdate" runat="server" CssClass="form-control"></asp:Label>
                </div>
            </div>
             <div class="clearfix col-sm-4  margin-bottom">
                    <label class="col-sm-4 titik-dua control-label">Invoice Type</label>
                    <div class="col-sm-8 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="invtyp" runat="server" CssClass="form-control" OnSelectedIndexChanged="invtyp_selected" AutoPostBack="True" >
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            <div class="clearfix col-md-4 col-sm-6 margin-bottom">
                 <label class="control-label col-sm-4 titik-dua">Customer</label>
                <div class="col-sm-8">
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                         <ContentTemplate>
                             <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox>
                             <asp:AutoCompleteExtender ID="txsearch_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" TargetControlID="txsearch" UseContextKey="True">
                             </asp:AutoCompleteExtender>
                             <asp:HiddenField ID="hdcust" runat="server" />
                         </ContentTemplate>
                     </asp:UpdatePanel>
                </div>
            </div>
        </div>
  
        <div class="row">
            <div class="divheader subheader subheader-bg">
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        Credit / Debit Note Request
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="well well-sm  primary">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        Request No.
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
              <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
            </div>

            <div class="clearfix">
                <div class="clearfix col-sm-4 no-padding margin-bottom">
                    <label class="col-sm-4 titik-dua control-label">Date</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                             <ContentTemplate>
                                 <asp:Panel runat="server" ID="dtcnPnl">
                                     <asp:TextBox ID="dtcn" runat="server" CssClass="form-control"></asp:TextBox>
                                     <asp:CalendarExtender ID="dtcn_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="dtcn" TodaysDateFormat="d/M/yyyy">
                                     </asp:CalendarExtender>
                                </asp:Panel>
                             </ContentTemplate>
                         </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix col-sm-8 no-padding margin-bottom">
                    <label class="col-sm-2 titik-dua control-label">Customer</label>
                    <div class="col-sm-10" style="min-height:34px;">
                        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbcustname" runat="server" CssClass="form-control"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix col-sm-4 no-padding margin-bottom">
                    <label class="col-sm-4 titik-dua control-label">Salesman</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txsalesman" runat="server" CssClass="makeitreadonly form-control ro" Enabled="false"></asp:TextBox>
                                <asp:HiddenField ID="hdsalesman_cd" runat="server" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix col-sm-8 no-padding margin-bottom">
                    <label class="col-sm-2 titik-dua control-label">Adress</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txadress" runat="server" CssClass="makeitreadonly ro form-control" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix col-sm-4 no-padding margin-bottom">
                    <label class="col-sm-4 titik-dua control-label">Remark</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txarcn_note" runat="server" CssClass="form-control"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix col-sm-8 no-padding margin-bottom">
                    <label class="col-sm-2 titik-dua control-label">Type CN/DN</label>
                    <div class="col-sm-10 drop-down">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbcn" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix col-sm-4 no-padding margin-bottom">
                    <label class="col-sm-4 titik-dua control-label">Attachment</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="uplPnl">
                                    <asp:FileUpload ID="upl" runat="server" ClientIDMode="Static" CssClass="form-control" />
                                </asp:Panel>
                                <asp:HyperLink ID="hlpic" runat="server" class="example-image-link" data-lightbox="example-1" Text="Picture"> 
                                <asp:Label ID="lblemailid" runat="server" Font-Size="Small" ></asp:Label></asp:HyperLink>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:HiddenField ID="hdfile_nm" runat="server" />
                    </div>
                </div>
                <div class="clearfix col-sm-8 no-padding margin-bottom">
                    <label class="col-sm-2 titik-dua control-label">Ref No.</label>
                    <div class="col-sm-10" style="margin-bottom: 20px;">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txcndnrefno" runat="server" CssClass="form-control"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                
            </div>

        </div>
    
        <div class="clearfix" id="grdcdPnl" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                         <ContentTemplate>
                             <asp:GridView ID="grdcn" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" CellPadding="0" GridLines="None" OnRowCancelingEdit="grdcn_RowCancelingEdit" OnRowDataBound="grdcn_RowDataBound" OnRowEditing="grdcn_RowEditing" OnRowUpdating="grdcn_RowUpdating" OnSelectedIndexChanging="grdcn_SelectedIndexChanging" ShowFooter="True">
                                 <AlternatingRowStyle/>
                                 <Columns>
                                     <asp:TemplateField HeaderText="Invoice No.">
                                         <ItemTemplate>
                                             <asp:Label ID="lbinv_no" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                                         </ItemTemplate>
                                     </asp:TemplateField>
								     <asp:BoundField DataField="inv_dt" HeaderText="Invoice Date" />
                                     <asp:BoundField DataField="manual_no" HeaderText="Manual No" />
                                     <asp:TemplateField HeaderText="Inv Amount">
                                         <ItemTemplate>
                                             <asp:Label ID="lbtotinv" runat="server" Text='<%# Eval("totinv","{0:n2}") %>'></asp:Label>
                                         </ItemTemplate>
                                      <%--    <EditItemTemplate>
                                             <asp:TextBox ID="txtotinv" runat="server" HorizontalAlignment="right" Text='<%# Eval("totinv","{0:n2}") %>'></asp:TextBox>
                                         </EditItemTemplate>--%>
                                         <FooterTemplate>
                                             <div >
                                                 <asp:Label ID="lbltotinv" runat="server" />
                                             </div>
                                         </FooterTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Temp Balance">
                                         <ItemTemplate>
                                             <asp:Label ID="lbbalance" runat="server" Text='<%# Eval("tempbalance","{0:n2}") %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                             <div >
                                                 <asp:Label ID="lblbalance" runat="server" />
                                             </div>
                                         </FooterTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Remain">
                                         <ItemTemplate>
                                             <asp:Label ID="lbremain" runat="server" Text='<%# Eval("remain","{0:n2}") %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                             <div >
                                                 <asp:Label ID="lblremain" runat="server" />
                                             </div>
                                         </FooterTemplate>
                                     </asp:TemplateField>
                                     <%--<asp:TemplateField HeaderText="CN / DN (SR)">
                        <ItemTemplate>
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                   <asp:TextBox ID="txamount" runat="server" Text='<%# Eval("arciAmount") %>'></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                                     <asp:TemplateField HeaderText="CN / DN (SR)">
                                         <ItemTemplate>
                                             <asp:Label ID="lbarciAmounst" runat="server" Text='<%# Eval("arciAmount","{0:n2}") %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                             <div >
                                                 <asp:Label ID="lblarciAmount" runat="server" />
                                             </div>
                                         </FooterTemplate>
                                         <EditItemTemplate>
                                             <asp:TextBox ID="txarciAmount" runat="server" HorizontalAlignment="right" Text='<%# Eval("arciAmount","{0:n2}") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                     </asp:TemplateField>
                                     <asp:TemplateField HeaderText="VAT Pct)">
                                         <ItemTemplate>
                                             <asp:Label ID="lbvat_pct" runat="server" Text='<%# Eval("vat_pct","{0:n2}") %>'></asp:Label>
                                         </ItemTemplate>
                                          <EditItemTemplate>
                                             <asp:TextBox ID="vatEdit" runat="server" HorizontalAlignment="right" Text='<%# Eval("vat_pct","{0:n2}") %>'></asp:TextBox>
                                         </EditItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="VAT Amount)">
                                         <ItemTemplate>
                                             <asp:Label ID="lbvat_amt" runat="server" Text='<%# Eval("vat_amt","{0:n2}") %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                             <div >
                                                 <asp:Label ID="lblvat_amt" runat="server" />
                                             </div>
                                         </FooterTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Description">
                                         <ItemTemplate>
                                             <asp:Label ID="lbarciDescription" runat="server" Text='<%# Eval("arciDescription") %>'></asp:Label>
                                         </ItemTemplate>
                                         <FooterTemplate>
                                             <div >
                                                 <asp:Label ID="lbltotalwithvat" runat="server" />
                                             </div>
                                         </FooterTemplate>
                                         <EditItemTemplate>
                                             <asp:TextBox ID="txarciDescription" runat="server" Text='<%# Eval("arciDescription") %>'></asp:TextBox>
                                         </EditItemTemplate> 
                                     </asp:TemplateField>
                                     <%--                <asp:TemplateField HeaderText="End Balance"></asp:TemplateField>--%>
                                     <asp:CommandField ShowEditButton="True" />
                                 </Columns>
                                 <EditRowStyle CssClass="table-edit" />
                                 <FooterStyle CssClass="table-footer text-center" HorizontalAlign="Center" />
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
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                    <asp:AsyncPostBackTrigger  ControlID="btinv" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
        </div>

        <div class="navi margin-bottom">
            <asp:Button ID="btinv" runat="server" OnClick="btinv_Click" Text="Button" style="display:none" />
            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnew" runat="server" CssClass="btn btn-success btn-add" OnClick="btnew_Click" Text="New" />
                    <asp:Button ID="btsave" runat="server" CssClass="btn btn-warning save" OnClick="btsave_Click" Text="Save" />
                    <asp:Button ID="btprint" runat="server" CssClass="btn btn-info btn-print" OnClick="btprint_Click" style="left: 0px; top: 0px" Text="Print" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btsave" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>

