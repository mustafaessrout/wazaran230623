<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstCM.aspx.cs" Inherits="fm_mstCM" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <style>
        .product-border{
            font-size:16px;
            margin-top: -10px;
            background-color:#fff;
            padding: 5px;
            font-weight: bold;
        }
        fieldset.product-border {
            border: 1px solid #bebebe !important;
            padding: 0 1.4em 0 1.4em !important;
            margin: 0 0 1.5em 0 !important;
            border-radius: 5px;
        }
        .content-hover{
            /*display:none;*/
        }
        .hover-title .subheader{
            cursor:pointer;
            position:relative;
        }
        .hover-title .subheader::after{
            content: "\f063";
            font-family: fontawesome;
            position: absolute;
            color: #fff;
            font-size: 14px;
            right: 20px;
            top: 13px;
        }
    </style>
    <script>
        function AgSelected(sender, e) {
            $get('<%=hdhoag.ClientID%>').value = e.get_value();
            $get('<%=btlookupag.ClientID%>').click();
        }
        function CustSelected(sender, e) {
            $get('<%=hdcdcust.ClientID%>').value = e.get_value();
        }
        function CMSelected(dt) {
            $get('<%=hdcm.ClientID%>').value = dt;
            $get('<%=btlookupcm.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:HiddenField ID="hdcm" runat="server" />
    <asp:HiddenField ID="hdhoag" runat="server" />
    <asp:HiddenField ID="hdcdcust" runat="server" />
    <asp:Button ID="btlookupag" runat="server" Text="Button" OnClick="btlookupag_Click" style="display:none"/>
    <asp:Button ID="btlookupcm" runat="server" Text="Button" OnClick="btlookupcm_Click" style="display:none"/>

    <div class="divheader">Credit Memo <asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red"></asp:Label></div> 
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="form-horizontal">
            <fieldset class="hover-title">
                <div class="row">
                    <div class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">CREDIT MEMO</div>
                </div>
                <div class="content-hover" id="ha">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row margin-top">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="CMNo" class="col-xs-4 col-form-label col-form-label-sm">HO Credit Memo Code</label>
                                        <div class="col-xs-8">
                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:Label ID="lbhocm_no" runat="server" CssClass="form-control input-sm" BorderStyle="Solid" BorderWidth="1px">New</asp:Label>
                                                            <div class="input-group-btn">
                                                                <asp:LinkButton ID="btsearchhoag" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchhoag_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="CMDate" class="col-xs-4 col-form-label col-form-label-sm">HO Credit Memo Date</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="dtCMDate" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                            <asp:CalendarExtender CssClass="date" ID="dtCMDate_CalendarExtender" runat="server" BehaviorID="dtCMDate_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtCMDate">
                            </asp:CalendarExtender>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="cmrefno" class="col-xs-4 col-form-label col-form-label-sm">REF NO.</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="txcmrefno" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
            <fieldset class="hover-title">
                <div class="row ">
                    <div class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">According to the following Attachments:
                    </div>
                </div>
                <div class="content-hover" id="py">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>

                            <div class="row margin-top" runat="server" style="display:none">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="typePayment" class="col-xs-4 col-form-label col-form-label-sm">Type</label>
                                        <div class="col-xs-8 drop-down">
                                            <asp:DropDownList ID="cbpayment" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbpayment_SelectedIndexChanged" CssClass="form-control input-sm">  
                                                <asp:ListItem Text="Cash" Value="CH"></asp:ListItem>
                                                <asp:ListItem Text="Bank Transfer" Value="BT"></asp:ListItem>
                                                <asp:ListItem Text="Clearence of Debt" Value="CD"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <h5 class="jajarangenjang">A. CASH</h5>
                            <div class="h-divider"></div>
                            <div runat="server" id="CHscreen">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="chno" class="col-xs-4 col-form-label col-form-label-sm">NO.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txchno" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="chbank" class="col-xs-4 col-form-label col-form-label-sm">BANK.</label>
                                            <div class="col-xs-8">
                                                <asp:DropDownList ID="cbchbank" runat="server" CssClass="form-control input-sm" >
                                                </asp:DropDownList>
                                            </div>                                          
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="chrvno" class="col-xs-4 col-form-label col-form-label-sm">RV NO.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txrvno" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="chvalue" class="col-xs-4 col-form-label col-form-label-sm">VALUE.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txchvalue" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label for="chnotes" class="col-xs-3 col-form-label col-form-label-sm">NOTES.</label>
                                            <div class="col-xs-9">
                                                <asp:TextBox ID="txchnotes" runat="server" CssClass="form-control input-sm" Enabled="true" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button CssClass="btn btn-default btn-sm btn-success" runat="server" ID="btnaddch" Text="Add" OnClick="btnaddch_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                <asp:GridView ID="grdch" AllowPaging="True" OnRowDeleting="grdch_RowDeleting" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdchcm" Value='<%#Eval("cm_no") %>' runat="server" />
                                                                <asp:Label ID="lbchno" runat="server" Text='<%# Eval("cash_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bank">
                                                            <ItemTemplate><%# Eval("bank") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RV No.">
                                                            <ItemTemplate><%# Eval("rv_no") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Notes">
                                                            <ItemTemplate><%# Eval("cash_notes") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value">
                                                            <ItemTemplate><%# Eval("amount") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="True" />
                                                    </Columns>
                                                </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <h5 class="jajarangenjang">B. BANK TRANSFER </h5>
                            <div class="h-divider"></div>
                            <div runat="server" id="BTscreen">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="btno" class="col-xs-4 col-form-label col-form-label-sm">NO.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txbtno" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="chbank" class="col-xs-4 col-form-label col-form-label-sm">BANK.</label>
                                            <div class="col-xs-8">
                                                <asp:DropDownList ID="cbbtbank" runat="server" CssClass="form-control input-sm" >
                                                </asp:DropDownList>
                                            </div>                                          
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="btrvno" class="col-xs-4 col-form-label col-form-label-sm">RV NO.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txbtrvno" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="btvalue" class="col-xs-4 col-form-label col-form-label-sm">VALUE.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txbtvalue" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label for="btnotes" class="col-xs-3 col-form-label col-form-label-sm">NOTES.</label>
                                            <div class="col-xs-9">
                                                <asp:TextBox ID="txbtnotes" runat="server" CssClass="form-control input-sm" Enabled="true" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button CssClass="btn btn-default btn-sm btn-success" runat="server" ID="btnaddbt" Text="Add" OnClick="btnaddbt_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                <asp:GridView ID="grdbt" AllowPaging="True" OnRowDeleting="grdbt_RowDeleting" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="No">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdbtcm" Value='<%#Eval("cm_no") %>' runat="server" />
                                                                <asp:Label ID="lbbtno" runat="server" Text='<%# Eval("bank_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Bank">
                                                            <ItemTemplate><%# Eval("bank") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="RV No.">
                                                            <ItemTemplate><%# Eval("rv_no") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Notes.">
                                                            <ItemTemplate><%# Eval("bank_notes") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value">
                                                            <ItemTemplate><%# Eval("amount") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="True" />
                                                    </Columns>
                                                </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <h5 class="jajarangenjang">C. CLEARANCE Of DEBT</h5>
                            <div class="h-divider"></div>
                            <div runat="server" id="CDscreen">
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="cdno" class="col-xs-4 col-form-label col-form-label-sm">DIFF HO NO.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txcdno" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="cdbranch" class="col-xs-4 col-form-label col-form-label-sm">BRANCH.</label>
                                            <div class="col-xs-8">
                                                <asp:DropDownList ID="cbcdbranch" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="cbcdbranch_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </div>                                          
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="chrvno" class="col-xs-4 col-form-label col-form-label-sm">CUSTOMER NO.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txcdcustno" runat="server" CssClass="form-control input-sm" Enabled="true" ></asp:TextBox>
                                                <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txcdcustno_AutoCompleteExtender" runat="server" ServiceMethod="GetListCustomer" TargetControlID="txcdcustno" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="CustSelected" >
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-6">
                                        <div class="form-group">
                                            <label for="cdvalue" class="col-xs-4 col-form-label col-form-label-sm">VALUE.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txcdvalue" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-8">
                                        <div class="form-group">
                                            <label for="cdnotes" class="col-xs-3 col-form-label col-form-label-sm">NOTES.</label>
                                            <div class="col-xs-9">
                                                <asp:TextBox ID="txcdnotes" runat="server" CssClass="form-control input-sm" Enabled="true" TextMode="MultiLine" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <asp:Button CssClass="btn btn-default btn-sm btn-success" runat="server" ID="btnaddcd" Text="Add" OnClick="btnaddcd_Click" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                <asp:GridView ID="grdcd" AllowPaging="True" OnRowDeleting="grdcd_RowDeleting" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Diff HO No">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hddtcm" Value='<%#Eval("cm_no") %>' runat="server" />
                                                                <asp:Label ID="lbdtno" runat="server" Text='<%# Eval("debt_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Branch">
                                                            <ItemTemplate><%# Eval("branch") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Cust No.">
                                                            <ItemTemplate><%# Eval("customer") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Notes.">
                                                            <ItemTemplate><%# Eval("debt_notes") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value">
                                                            <ItemTemplate><%# Eval("amount") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="True" />
                                                    </Columns>
                                                </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            

                            <h5 class="jajarangenjang">D. AGREEMENTS</h5>
                            <div class="h-divider"></div>
                            <div runat="server" id="AGscreen">
                                <div class="row">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="agno" class="col-xs-4 col-form-label col-form-label-sm">AGREEMENT NO.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txagno" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" AutoPostBack="true" ></asp:TextBox>
                                                <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txagno_AutoCompleteExtender" runat="server" ServiceMethod="GetListHoAg" TargetControlID="txagno" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="AgSelected" >
                                                </asp:AutoCompleteExtender>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="agprop" class="col-xs-4 col-form-label col-form-label-sm">PROPOSAL NO.</label>
                                            <div class="col-xs-8">
                                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                                <ContentTemplate>
                                                <asp:DropDownList ID="cbagprop" runat="server" CssClass="form-control input-sm" >
                                                </asp:DropDownList>
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>                                          
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label for="agvalue" class="col-xs-4 col-form-label col-form-label-sm">VALUE.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txagvalue" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-2">
                                        <div class="form-group">
                                            <label for="agvalue" class="col-xs-4 col-form-label col-form-label-sm">VAT</label>
                                            <div class="col-xs-8">
                                                <asp:DropDownList ID="cbvat" runat="server" CssClass="form-control input-sm" >
                                                    <asp:ListItem Text="ON" Value="1"></asp:ListItem>
                                                    <asp:ListItem Text="OFF" Value="0"></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Button CssClass="btn btn-default btn-sm btn-success" runat="server" ID="btnaddag" Text="Add" OnClick="btnaddag_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                <asp:GridView ID="grdag" AllowPaging="True" OnRowDeleting="grdag_RowDeleting" OnPageIndexChanging="grdag_PageIndexChanging" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="HO AG NO">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdagcm" Value='<%#Eval("cm_no") %>' runat="server" />
                                                                <asp:Label ID="lbagno" runat="server" Text='<%# Eval("ag_no") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Proposal">
                                                            <ItemTemplate><%# Eval("prop_no") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Before Vat">
                                                            <ItemTemplate><%# Eval("amount") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Vat Out">
                                                            <ItemTemplate><%# Eval("vat") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="After Vat">
                                                            <ItemTemplate><%# Eval("total") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="True" />
                                                    </Columns>
                                                </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>


                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
            <fieldset class="hover-title">
                <div class="row">
                    <div class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Please make the following registrations:
                    </div>
                </div>
                <div class="content-hover" id="br">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div runat="server" id="BrScreen">
                                <div class="row margin-top">
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="cdbranch" class="col-xs-4 col-form-label col-form-label-sm">BRANCH.</label>
                                            <div class="col-xs-8">
                                                <asp:DropDownList ID="cbbranch" runat="server" CssClass="form-control input-sm" >
                                                </asp:DropDownList>
                                            </div>   
                                        </div>
                                    </div>
                                    <div class="col-md-4">
                                        <div class="form-group">
                                            <label for="agprop" class="col-xs-4 col-form-label col-form-label-sm">CUSTOMER NAME.</label>
                                            <div class="col-xs-8">
                                                <asp:DropDownList ID="cbbrcustomer" runat="server" CssClass="form-control input-sm" >
                                                </asp:DropDownList>
                                            </div>                                          
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <div class="form-group">
                                            <label for="agvalue" class="col-xs-4 col-form-label col-form-label-sm">VALUE.</label>
                                            <div class="col-xs-8">
                                                <asp:TextBox ID="txbrvalue" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-md-1">
                                        <div class="form-group">
                                            <asp:Button CssClass="btn btn-default btn-sm btn-success" runat="server" ID="btnaddbr" Text="Add" OnClick="btnaddbr_Click" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                            <ContentTemplate>
                                                <div class="table-responsive">
                                                <asp:GridView ID="grdbr" AllowPaging="True" OnRowDeleting="grdbr_RowDeleting" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="BRANCH">
                                                            <ItemTemplate>
                                                                <asp:HiddenField ID="hdbrcm" Value='<%#Eval("salespoint_cd") %>' runat="server" />
                                                                <asp:Label ID="lbbrcm" runat="server" Text='<%# Eval("salespoint_nm") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Customer">
                                                            <ItemTemplate><%# Eval("customer") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Value">
                                                            <ItemTemplate><%# Eval("amount") %></ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:CommandField ShowDeleteButton="True" />
                                                    </Columns>
                                                </asp:GridView>
                                                </div>
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
            <fieldset class="hover-title">
                <div class="row">
                    <div class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Document Supported:
                    </div>
                </div>
                <div class="content-hover" id="doc">
                    <%--<asp:UpdatePanel runat="server">
                        <ContentTemplate>--%>
                            <div runat="server" id="DocSreen">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="form-group">
                                            <%--<asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                            <ContentTemplate>--%>
                                                <div class="table-responsive">
                                                <asp:GridView ID="grddoc" AllowPaging="True" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Document Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbdocname" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Upload">
                                                            <ItemTemplate>
                                                                <div class="text-center">
                                                                    <asp:FileUpload ID="upl" runat="server" />  
                                                                </div>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                </div>
                                                <div class="navi">
                                                    <asp:Button ID="btupload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btupload_Click" />
                                                </div>
                                                <div class="table-responsive">
                                                <asp:GridView ID="grdviewdoc" AllowPaging="True" runat="server" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Document Code">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbdoccode" runat="server" Text='<%# Eval("doc_cd") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="Document Name">
                                                            <ItemTemplate>
                                                                <asp:Label ID="lbdocname" runat="server" Text='<%# Eval("doc_nm") %>'></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="File location">
                                                            <ItemTemplate>
                                                                <a class="example-image-link" href="/images/creditmemo_doc/<%# Eval("fileloc") %>" data-lightbox="example-1<%# Eval("fileloc") %>" style="color: blue;">
                                                                    <asp:Label ID="lbfileloc" runat="server" Text='Picture'></asp:Label>
                                                                </a>
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                                </div>
                                            <%--</ContentTemplate>
                                            </asp:UpdatePanel>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </fieldset>
        </div>
    </div>

    <div class="row margin-bottom">
        <div class="col-sm-12">
        <div class="text-center navi">
            <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success button2 btn add" OnClick="btnew_Click" />
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning  btn btn-save" OnClick="btsave_Click" />
            <asp:Button ID="btedit" runat="server" Text="Edit" CssClass="btn-warning  btn btn-danger" OnClick="btedit_Click" />
            <asp:Button ID="btdelete" runat="server" Text="Delete" CssClass="btn-warning  btn btn-danger" OnClick="btdelete_Click" />
            <asp:Button ID="btapprove" runat="server" Text="Approve" CssClass="btn-default  btn btn-default" OnClick="btapprove_Click" />
            <asp:Button ID="btreject" runat="server" Text="Reject" CssClass="btn-warning  btn btn-danger" OnClick="btreject_Click" />
        </div>
        </div>
    </div>   

</asp:Content>

