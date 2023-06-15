<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstcontractKA.aspx.cs" Inherits="fm_mstcontractKA" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function ContractSelected(dt) {
            $get('<%=hdcontract.ClientID%>').value = dt;
            $get('<%=btlookcontract.ClientID%>').click();
        }
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
        function PropSelected(sender, e) {
            $get('<%=hdprop.ClientID%>').value = e.get_value();
            $get('<%=btlookproposal.ClientID%>').click();
        }
        <%--$(document).ready(function () {
            $("#<%=btsearchhoag.ClientID%>").click(function () {
                PopupCenter('fm_lookupcontractKA.aspx', 'xtf', '900', '500');

            });
            //HideProgress();
        });--%>
    </script>
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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:HiddenField ID="hdcontract" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hdprop" runat="server" />

    <div class="divheader">Business Agreement Key Account <asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red"></asp:Label></div> 
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="form-horizontal">
            <fieldset class="hover-title">
                <div class="row">
                    <div class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">HO Agreement</div>
                </div>
                <div class="content-hover" id="ha">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row margin-top">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="contractNo" class="col-xs-4 col-form-label col-form-label-sm">HO Agreement Code</label>
                                        <div class="col-xs-8">
                                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                                    <ContentTemplate>
                                                        <div class="input-group">
                                                            <asp:Label ID="lbhoag_no" runat="server" CssClass="form-control input-sm" BorderStyle="Solid" BorderWidth="1px">New</asp:Label>
                                                            <div class="input-group-btn">
                                                                <asp:LinkButton ID="btsearchhoag" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchhoag_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                                            </div>
                                                        </div>
                                                    </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row margin-top">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="secondParty" class="col-xs-4 col-form-label col-form-label-sm">Transaction Period</label>
                                        <div class="col-sm-4 drop-down">
                                            <asp:DropDownList ID="ddMonth" runat="server" CssClass="form-control input-sm">  
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
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:DropDownList ID="ddYear" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
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
                    <div class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Customer File</div>
                </div>
                <div class="content-hover" id="ci">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row margin-top">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="branch" class="col-xs-4 col-form-label col-form-label-sm">Branch</label>
                                        <div class="col-xs-6">
                                            <asp:DropDownList ID="cbbranch" runat="server" CssClass="form-control input-sm" >
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-2">
                                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:Button CssClass="btn btn-default btn-sm btn-success" runat="server" ID="btnaddbranch" Text="Add Branch" OnClick="btnaddbranch_Click" />
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                            <asp:GridView ID="grdbranch" AllowPaging="True" OnRowDeleting="grdbranch_RowDeleting" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Salespoint Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblslspointcd" runat="server" Text='<%# Eval("salespoint_cd") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Salespoint Name">
                                                        <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
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
                            <div class="row margin-top">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="secondParty" class="col-xs-4 col-form-label col-form-label-sm">Customer</label>
                                        <div class="col-sm-4 drop-down">
                                            <asp:DropDownList ID="cbCustomer" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbCustomer_SelectedIndexChanged" CssClass="form-control input-sm"  >
                                                <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                                                <asp:ListItem Text="Customer Group" Value="G"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txCustomer" runat="server" CssClass="form-control input-sm"  ></asp:TextBox>
                                                <asp:AutoCompleteExtender  CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item"  ID="txCustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetListCustomer" TargetControlID="txCustomer" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="CustSelected" >
                                                </asp:AutoCompleteExtender>
                                                <asp:DropDownList ID="cbCusGrp" runat="server" CssClass="form-control input-sm"   ></asp:DropDownList>
                                            </ContentTemplate>
                                                <Triggers>
                                                    <asp:AsyncPostBackTrigger ControlID="cbCustomer" EventName="SelectedIndexChanged" />
                                                </Triggers>
                                            </asp:UpdatePanel>
                                        </div>
                                        <div class="col-xs-2">
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                            <ContentTemplate>
                                                <asp:Button CssClass="btn btn-default btn-sm btn-success" runat="server" ID="btnaddcustomer" Text="Add Customer" OnClick="btnaddcustomer_Click" />
                                            </ContentTemplate>
                                            </asp:UpdatePanel>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                        <ContentTemplate>
                                            <div class="table-responsive">
                                            <asp:GridView ID="grdcust" AllowPaging="True" OnRowDeleting="grdcust_RowDeleting" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Customer Name">
                                                        <ItemTemplate>
                                                            <asp:HiddenField ID="lbcustcd" Value='<%#Eval("cust_cd") %>' runat="server" />
                                                            <asp:HiddenField ID="lbsalespointcd" Value='<%#Eval("salespoint_cd") %>' runat="server" />
                                                            <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("customer") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Branch">
                                                        <ItemTemplate><%# Eval("branch") %></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:CommandField ShowDeleteButton="True" />
                                                </Columns>
                                            </asp:GridView>
                                            <asp:GridView ID="grdcusgrcd" AllowPaging="True" OnRowDeleting="grdcusgrcd_RowDeleting" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Group Code">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cusgrcd") %>'></asp:Label></ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Group Name">
                                                        <ItemTemplate><%# Eval("cusgrnm") %></ItemTemplate>
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
            <fieldset class="hover-title">
                <div class="row ">
                    <div class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Agreement Information</div>
                </div>
                <div class="content-hover" id="ai">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row margin-top">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="agsbtcno" class="col-xs-4 col-form-label col-form-label-sm">AG SBTC NO.</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="txAgSbtcNo" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="agcustno" class="col-xs-4 col-form-label col-form-label-sm">AG CUSTOMER NO.</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="txAgCustNo" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="agtype" class="col-xs-4 col-form-label col-form-label-sm">ATL/BTL</label>
                                        <div class="col-xs-8">
                                            <asp:DropDownList ID="cbpromokind" runat="server" AutoPostBack="True" CssClass="form-control input-sm" OnSelectedIndexChanged="cbpromokind_SelectedIndexChanged">
                                                <asp:ListItem Value="ATL">ATL</asp:ListItem>
                                                <asp:ListItem Value="BTL">BTL</asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="prmotype" class="col-xs-4 col-form-label col-form-label-sm">Promotion</label>
                                        <div class="col-xs-4">
                                            <asp:DropDownList ID="cbpromogroup" runat="server" AutoPostBack="True" CssClass="form-control input-sm" OnSelectedIndexChanged="cbpromogroup_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </div>
                                        <div class="col-xs-4">
                                            <asp:DropDownList ID="cbpromotype" runat="server" AutoPostBack="True" CssClass="form-control input-sm" >
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="text-center navi">
                                        <div class="col-xs-12">
                                        <asp:Button CssClass="btn btn-default btn-sm btn-success" runat="server" ID="btnaddAg" Text="Add Agreement" OnClick="btnaddAg_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                        <asp:GridView ID="grdAgreement" AllowPaging="True" OnRowDeleting="grdAgreement_RowDeleting" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Select">
                                                    <ItemTemplate>
                                                        <asp:checkbox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="chkSelect_CheckedChanged"></asp:checkbox>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="lbcontractagno" Value='<%#Eval("contract_ag_no") %>' runat="server" />
                                                        <asp:HiddenField ID="lbcontractno" Value='<%#Eval("contract_no") %>' runat="server" />
                                                        <asp:HiddenField ID="lbagsbtcno" Value='<%#Eval("contract_sbtc_no") %>' runat="server" />
                                                        <asp:HiddenField ID="lbagcustno" Value='<%#Eval("contract_cust_no") %>' runat="server" />
                                                        <asp:Label ID="lbseq" runat="server" Text='<%# Eval("seq_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AG SBTC No.">
                                                    <ItemTemplate><%# Eval("contract_sbtc_no") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AG Cust No.">
                                                    <ItemTemplate><%# Eval("contract_cust_no") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="ATL/BTL">
                                                    <ItemTemplate><%# Eval("promotype") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Promotion">
                                                    <ItemTemplate><%# Eval("promotion") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
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
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
            <asp:UpdatePanel runat="server">
            <ContentTemplate>
            <fieldset class="hover-title" runat="server" id="budgetProposal">
                <div class="row ">
                    <div class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Proposal Information</div>
                </div>
                <div class="content-hover" id="bp">
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <div class="row margin-top">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="propno" class="col-xs-4 col-form-label col-form-label-sm">Proposal NO.</label>
                                        <div class="col-xs-8">
                                            <asp:TextBox ID="txbgtpropno" runat="server" CssClass="form-control input-sm" Font-Bold="true" Enabled="true" ></asp:TextBox>
                                            <asp:AutoCompleteExtender ID="txbgtpropno_AutoCompleteExtender" runat="server" ServiceMethod="GetListProposal" TargetControlID="txbgtpropno" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="PropSelected">
                                            </asp:AutoCompleteExtender>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="totalbudget" class="col-xs-4 col-form-label col-form-label-sm">Amount Prop.</label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txbgtamt" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                        </div>                                        
                                        <div class="col-xs-2">
                                        <asp:Button CssClass="btn btn-default btn-sm btn-success" runat="server" ID="btnaddprop" Text="Add Proposal" OnClick="btnaddprop_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="form-group">
                                        <label for="totalbudget" class="col-xs-4 col-form-label col-form-label-sm">Budget Prop.</label>
                                        <div class="col-xs-6">
                                            <asp:TextBox ID="txbgtprop" runat="server" CssClass="form-control input-sm" ReadOnly="true" ></asp:TextBox>
                                        </div>  
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <div class="table-responsive">
                                        <asp:GridView ID="grdProposal" AllowPaging="True" OnRowDeleting="grdProposal_RowDeleting" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                                            <Columns>
                                                <asp:TemplateField HeaderText="AG SBTC No.">
                                                    <ItemTemplate><%#Eval("contract_sbtc_no") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="AG Cust No.">
                                                    <ItemTemplate><%#Eval("contract_cust_no") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Proposal No.">
                                                    <ItemTemplate>
                                                        <asp:HiddenField ID="lbcontractagno" Value='<%#Eval("contract_ag_no") %>' runat="server" />
                                                        <asp:HiddenField ID="lbcontractno" Value='<%#Eval("contract_no") %>' runat="server" />
                                                        <asp:HiddenField ID="lbagsbtcno" Value='<%#Eval("contract_sbtc_no") %>' runat="server" />
                                                        <asp:HiddenField ID="lbagcustno" Value='<%#Eval("contract_cust_no") %>' runat="server" />
                                                        <asp:Label ID="lbpropno" runat="server" Text='<%# Eval("proposal_no") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Amount">
                                                    <ItemTemplate><%#Eval("amount") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" />
                                            </Columns>
                                        </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </fieldset>
            </ContentTemplate>
            </asp:UpdatePanel>

            <asp:UpdatePanel runat="server">
            <ContentTemplate>
            <fieldset class="hover-title" runat="server" id="documentSupport">
                <div class="row ">
                    <div class="divheader subheader subheader-bg" style="margin-bottom: 0 !important;">Document Support</div>
                </div>
                <div class="content-hover" id="ds">
                    <div class="row margin-top">
                        <div class="col-md-6">
                            <div class="form-group">
                                <label for="uploadDoc" class="col-xs-4 col-form-label col-form-label-sm">Upload Document</label>
                                <div class="col-xs-8">
                                    <asp:FileUpload ID="upl" runat="server"></asp:FileUpload>
                                        <asp:HyperLink ID="hpfile_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                                        <asp:Label ID="lblocfile" runat="server" Text='Agreement Document'></asp:Label></asp:HyperLink>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </fieldset>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

    <div class="row margin-bottom">
      <div class="col-sm-12">
        <div class="text-center navi">
            <asp:UpdatePanel ID="UpdatePanel26" runat="server">
            <ContentTemplate>
                <button type="submit" class="btn btn-default btn-sm" runat="server" id="btnew" onserverclick="btnew_ServerClick" >
                <span class="glyphicon glyphicon-plus" aria-hidden="true"></span> New</button>
                <button type="submit" class="btn btn-primary btn-sm" runat="server" id="btsave" onserverclick="btsave_Click" >
                <span class="glyphicon glyphicon-save" aria-hidden="true"></span> Save</button>
                 <button type="submit" class="btn btn-danger btn-sm" runat="server" id="btupdate" onserverclick="btupdate_ServerClick" >
                <span class="glyphicon glyphicon-edit" aria-hidden="true"></span> Update</button>
                <%--<asp:Button ID="btsave" runat="server" Text="Save" class="btn btn-warning btn-sm" OnClick="btsave_Click" />--%>
                <asp:Button ID="btlookcontract" runat="server" Text="Button" OnClick="btlookcontract_Click" style="display:none"/>
                <asp:Button ID="btlookproposal" runat="server" Text="Button" OnClick="btlookproposal_Click" style="display:none"/>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
      </div>
    </div>    

</asp:Content>

