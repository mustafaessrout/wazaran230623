<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_claimcashout.aspx.cs" Inherits="fm_claimcashout" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>

    <%--  <script src="js/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />--%>
    <%--<style type="text/css">
.h-divider{
 margin-top:5px;
 margin-bottom:5px;
 height:1px;
 width:100%;
 border-top:1px solid gray;
}
</style>--%>
    <script type="text/javascript">
       <%-- 
            $(document).ready(function () {
            console.log('temp');
            $('<%=ddlPaySrc.ClientID%>').change(function () {
                console.log(this.value);
            });
        });--%>



        function PropSelected(sender, e) {
            $get('<%=hdprop.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function EmpSelected(sender, e) {
            $get('<%=hdemp.ClientID%>').value = e.get_value();
            $get('<%=btsearch2.ClientID%>').click();
        }
        function CusSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }

        function ClaimSelected(sclaimno) {
            $get('<%=hdclaimno.ClientID%>').value = sclaimno;
            $get('<%=btsearch.ClientID%>').click();
            $get('<%=lbclaimno.ClientID%>').value = sclaimno;
        }

        function ContractSelected(scontract) {
            $get('<%=hdcontract.ClientID%>').value = scontract;
            $get('<%=txcontract.ClientID%>').value = scontract;
            $get('<%=btsearch3.ClientID%>').click();
        }

        function SelectProposal(sProp) {
            $get('<%=hdprop.ClientID%>').value = sProp;
            $get('<%=btsearch.ClientID%>').click();
        }

        function SelectPaySrc() {
            $get('<%=btPaySrc.ClientID%>').click();
        }

        function SelectRdrcpt() {
            $get('<%=btRdrcpt.ClientID%>').click();
        }

        function alert() {
            swal({
                title: "Amount must be less than Employee Pettycash Balance!",
                text: "Please use Regular Payment Source",
                type: "warning",
                showOKButton: true,
            },
                function () {
                    $get('<%=btPaySrc.ClientID%>').click();
                });
        }

        function alert1() {
            swal({
                title: "This Pettycash Employee have no balance because there is pending Cashout claim equals the balance not yet Approved!",
                text: "Please use Regular Payment Source",
                type: "warning",
                showOKButton: true,
            },
                function () {
                    $get('<%=btPaySrc.ClientID%>').click();
                });
        }

        function alert2() {
            swal({
                title: "This Pettycash Employee have pending Cashout claim not yet Approved!",
                text: "Please Approve and Try Again or use Regular Payment Source",
                type: "warning",
                showOKButton: true,
            },
                function () {
                    $get('<%=btPaySrc.ClientID%>').click();
                });
        }
    </script>


    <style>
        .input-sm.radio label {
            padding-top: 3px !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdclaimno" runat="server" />
    <asp:HiddenField ID="hdprop" runat="server" />
    <asp:HiddenField ID="hdcontract" runat="server" />
    <script src="https://code.jquery.com/jquery-1.10.2.js"></script>


    <%--<div class="divheader">Claim Cash Out Request</div>--%>
    <div class="divheader" id="TittleX" runat="server">Claim Cash Out Request</div>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row">
            <div class="margin-bottom clearfix">
    <%--            <div>
                    <label class="col-md-1 col-sm-2 control-label">Clm No</label>
                    <div class="col-md-3 col-sm-4">
                        <div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="input-group">
                                <ContentTemplate>
                                    <asp:Label ID="lbclaimno" runat="server" CssClass="form-control input-sm">New</asp:Label>
                                    <div class="input-group-btn">
                                        <asp:LinkButton ID="btsearchclaim" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchclaim_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </div>
                    </div>
                </div>

                <div id="vContract" runat="server">
                    <label class="col-md-1 col-sm-2 control-label">Contract</label>
                    <div class="col-md-3 col-sm-4">

                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="input-group">
                            <ContentTemplate>
                                <asp:TextBox ID="txcontract" runat="server" CssClass="form-control input-sm" OnTextChanged="txcontract_TextChanged"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btsearchcontract" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchcontract_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
                <div>
                    <label class="col-md-1 col-sm-2 control-label">Proposal</label>
                    <div class="col-md-3 col-sm-4">

                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="input-group">
                            <ContentTemplate>
                                <asp:TextBox ID="txprop" runat="server" CssClass="form-control input-sm" OnTextChanged="txprop_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender OnClientItemSelected="PropSelected" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txprop" UseContextKey="True" MinimumPrefixLength="1" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="1" CompletionInterval="10">
                                </asp:AutoCompleteExtender>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchprop_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>


                    </div>
                </div>
                <div>
                    <label class="col-md-1 col-sm-2 control-label">Proposal Budget Limit (Total All Branch)</label>
                    <label class="col-md-1 col-sm-2 control-label">SBTC Percentage, Principal Percentage</label>
                    <div class="col-md-3 col-sm-4">

                        <asp:UpdatePanel ID="txpropbudget" runat="server" class="input-group">
                            <ContentTemplate>
                                <asp:Panel runat="server" id="panelPropBudget">
                                    <div class="col-md-2">
                                        <div style="color: red; font-size: large">
                                            <asp:Label ID="lbpropbudget" CssClass="control-label" runat="server" Text="0.00" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" class="input-group">
                            <ContentTemplate>
                                <asp:Panel runat="server" id="panelpropsbtc">
                                    <div class="col-md-2">
                                        <div style="color: red; font-size: large">
                                            <asp:Label ID="lbpropsbtc" CssClass="control-label" runat="server" Text="0.00" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                        </div>
                                    </div>
                                    <span>
                                    </span>
                                    <div>
                                        <div style="color: red; font-size: large">
                                            <asp:Label ID="lbpropprincipal" CssClass="control-label" runat="server" Text="0.00" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>                        
                    </div>                  
                </div>--%>


                <asp:UpdatePanel runat="server">
                    <ContentTemplate>
                        <div class="clearfix margin-bottom">
                            <div>
                                <label class="col-md-1 col-sm-1 control-label">Clm No</label>
                                <div class="col-md-2 col-sm-2">
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="input-group">
                                            <ContentTemplate>
                                                <asp:Label ID="lbclaimno" runat="server" CssClass="form-control input-sm">New</asp:Label>
                                                <div class="input-group-btn">
                                                    <asp:LinkButton ID="btsearchclaim" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchclaim_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>
                            <div>
                                <div id="vContract" runat="server">
                                    <label class="col-md-1 col-sm-1 control-label">Contract</label>
                                    <div class="col-md-2 col-sm-2">

                                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="input-group">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txcontract" runat="server" CssClass="form-control input-sm" OnTextChanged="txcontract_TextChanged"></asp:TextBox>
                                                <div class="input-group-btn">
                                                    <asp:LinkButton ID="btsearchcontract" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchcontract_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                            </div>
                            <div>
                                <label class="col-md-1 col-sm-1 control-label">Proposal</label>
                                <div class="col-md-2 col-sm-2">

                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="input-group">
                                        <ContentTemplate>
                                            <asp:TextBox ID="txprop" runat="server" CssClass="form-control input-sm" OnTextChanged="txprop_TextChanged"></asp:TextBox>
                                            <asp:AutoCompleteExtender OnClientItemSelected="PropSelected" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="AutoCompleteExtender1" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txprop" UseContextKey="True" MinimumPrefixLength="1" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="1" CompletionInterval="10">
                                            </asp:AutoCompleteExtender>
                                            <div class="input-group-btn">
                                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btsearchprop_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            <div>
                                <label class="col-md-1 col-sm-1 control-label">SBTC Percentage</label>
                                <div class="col-md-2 col-sm-2">
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel11" runat="server" class="input-group">
                                            <ContentTemplate>
                                                 <div style="color: red; font-size: large">
                                                    <asp:Label ID="lbpropsbtc" CssClass="control-label" runat="server" Text="0.00" Font-Bold="True" Font-Size="X-Large"></asp:Label>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>

                                    </div>
                                </div>
                            </div>
                            <div>
                                <label class="col-md-1 col-sm-1 control-label">Principal Percentage</label>
                                <div class="col-md-2 col-sm-2">
                                    <div>
                                        <asp:UpdatePanel ID="UpdatePanel12" runat="server" class="input-group">
                                            <ContentTemplate>
                                                 <div style="color: red; font-size: large">
                                                    <asp:Label ID="lbpropprincipal" CssClass="control-label" runat="server" Text="0.00" Font-Bold="True" Font-Size="X-Large"></asp:Label>
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



            <div class="margin-bottom clearfix">
                <div>
                    <label class="col-md-1 col-sm-2 control-label">Month</label>
                    <div class="col-md-3 col-sm-4">
                        <div class="drop-down">
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
                    </div>
                </div>

                <div>
                    <label class="col-md-1 col-sm-2 control-label">Year</label>
                    <div class="col-md-3 col-sm-4 drop-down">
                        <asp:DropDownList ID="ddYear" runat="server" CssClass="form-control input-sm">
                        </asp:DropDownList>
                    </div>
                </div>

            </div>


            <label class="col-md-1 col-sm-2 control-label">Payment Source</label>
            <div class="col-md-3 col-sm-4">

                <asp:UpdatePanel ID="UpdatePanel6" runat="server" class="drop-down">
                    <ContentTemplate>
                        <asp:DropDownList ID="ddlPaySrc" runat="server" AutoPostBack="true" CssClass="form-control input-sm" OnSelectedIndexChanged="ddlPaySrc_SelectedIndexChanged">
                            <asp:ListItem Text="Regular" Value="Regular"></asp:ListItem>
                            <asp:ListItem Text="Employee Pettycash" Value="Pettycash"></asp:ListItem>
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>



            <div class="margin-bottom clearfix">

                <asp:Panel runat="server" ID="Emp">
                    <label class="col-md-1 col-sm-2 control-label">Employee</label>
                    <div class="col-md-3 col-sm-4">

                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                     
                    <div class="margin-bottom clearfix"></div>
                   <asp:Panel runat="server" ID="PettycashRefno">
                        <label class="col-md-1 col-sm-2 control-label">Pettycash Refno</label>
                        <div class="col-md-3 col-sm-4">

                            <asp:UpdatePanel ID="UpdatePanel10" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="ddlpettycashRefno" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlpettycashRefno_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        <asp:HiddenField ID="hdpettycashRefno" runat="server" />
                        </div>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="Balance">
                        <label class="col-md-1 col-sm-2 control-label">Trans Date</label>
                        <div class="col-md-3 col-sm-4 drop-down-date">
                            <asp:TextBox ID="dtTransdate" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            <asp:CalendarExtender ID="dtTransdate_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtTransdate" Format="dd/MM/yyyy">
                            </asp:CalendarExtender>
                        </div>


                        <label class="col-md-1 col-sm-2 control-label">Balance</label>
                        <div class="col-md-3 col-sm-4 ">
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server" class="label">
                                <ContentTemplate>
                                    <asp:Label ID="lblBalance" runat="server" Font-Size="Large" Font-Bold="true" ForeColor="Red"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </asp:Panel>
                </asp:Panel>
            </div>







            <div class="clearfix margin-bottom">

                <div>
                    <label class="col-md-1 col-sm-2 control-label">Type</label>
                    <div class="col-md-3 col-sm-4">
                        <asp:Label ID="lbtype" runat="server" CssClass="form-control input-sm"></asp:Label>
                    </div>
                </div>
                <asp:Panel runat="server" ID="cbreceivePanel" Visible="False">
                    <label class="col-md-1 col-sm-2 control-label">Customer Name</label>
                    <div class="col-md-3 col-sm-4">
                        <asp:DropDownList ID="cbreceive" runat="server" CssClass="form-control input-sm" Visible="False"></asp:DropDownList>
                        <asp:TextBox ID="txreceive" runat="server" CssClass="form-control input-sm" Visible="False"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txreceive_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList3" TargetControlID="txreceive" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="10" CompletionInterval="1" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="CusSelected">
                        </asp:AutoCompleteExtender>
                        <asp:HiddenField ID="hdcust" runat="server" />
                    </div>
                </asp:Panel>
                <div class="clearfix margin-bottom">
                    <div>
                        <label class="col-md-1 col-sm-2 control-label">CDV No</label>
                        <div class="col-md-3 col-sm-4">
                            <asp:TextBox ID="txcdv" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>

            <div class="clearfix margin-bottom">
                <div>
                    <label class="col-md-1 col-sm-2 control-label">Rcpt</label>
                    <div class="col-md-3 col-sm-4">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:RadioButtonList ID="rdrcpt" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" OnSelectedIndexChanged="rdrcpt_SelectedIndexChanged">
                                    <asp:ListItem Value="I">Internal</asp:ListItem>
                                    <asp:ListItem Value="E">External</asp:ListItem>
                                </asp:RadioButtonList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div>
                    <label class="col-md-1 col-sm-2 control-label">Employee Name</label>
                    <div class="col-md-3 col-sm-4">
                        <asp:TextBox ID="txname" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txname_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txname" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="10" CompletionInterval="1" MinimumPrefixLength="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="EmpSelected">
                        </asp:AutoCompleteExtender>
                        <asp:HiddenField ID="hdemp" runat="server" />
                    </div>
                </div>

                <div>
                    <label class="col-md-1 col-sm-2 control-label">Phone</label>
                    <div class="col-md-3 col-sm-4">
                        <asp:TextBox ID="txphone" runat="server" CssClass="form-control input-sm" Enabled="false"></asp:TextBox>
                    </div>
                </div>
            </div>

            <div class="clearfix margin-bottom">
                <div>
                    <label class="control-label col-md-1">Approval</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbapproval" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <ContentTemplate>
                    <div class="clearfix margin-bottom" id="dvRegular" runat="server">
                        <div class="h-divider"></div>
                        <div>
                            <label class="col-md-1 col-sm-2 control-label">CR Dt</label>
                            <div class="col-md-2 col-sm-4 drop-down-date">
                                <asp:TextBox ID="dtcreate" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:CalendarExtender ID="dtcreate_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtcreate" Format="d/M/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div>
                            <label class="col-md-1 col-sm-2 control-label">Start Dt</label>
                            <div class="col-md-2 col-sm-4 drop-down-date">
                                <asp:TextBox ID="dtstart" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:CalendarExtender ID="dtstart_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtstart" Format="d/M/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div>
                            <label class="col-md-1 col-sm-2 control-label">End Dt</label>
                            <div class="col-md-2 col-sm-4">
                                <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label class="col-md-1 col-sm-2 control-label">Dlvry Dt</label>
                            <div class="col-md-2 col-sm-4">
                                <asp:TextBox ID="dtdelivery" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:CalendarExtender ID="dtdelivery_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtdelivery" Format="d/M/yyyy">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                <ContentTemplate>
                    <div class="clearfix margin-bottom" id="dvRegular1" runat="server">
                        <div>
                            <label class="col-md-1 col-sm-2 control-label">Ref</label>
                            <div class="col-md-2 col-sm-4 ">
                                <asp:TextBox ID="txref" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label class="col-md-1 col-sm-2 control-label">ApproveBy</label>
                            <div class="col-md-2 col-sm-4 ">
                                <asp:TextBox ID="txpaid" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label class="col-md-1 col-sm-2 control-label">Budget</label>
                            <div class="col-md-2 col-sm-4 ">
                                <asp:TextBox ID="txbudget" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label class="col-md-1 col-sm-2 control-label">CR Dt</label>
                            <div class="col-md-2 col-sm-4 ">
                                <asp:TextBox ID="txpromotype" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="h-divider"></div>

            <div class="clearfix margin-bottom">

                <label class="col-md-1 col-sm-2 control-label">Promo</label>
                <div class="col-md-11 col-sm-8">
                    <asp:TextBox ID="txpromogroup" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <label class="col-md-1 col-sm-2 control-label">Remark</label>
                <div class="col-md-11 col-sm-8">
                    <asp:Label ID="lbremark" runat="server" CssClass="form-control input-sm"></asp:Label>
                </div>
            </div>

            <div class="h-divider"></div>


            <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="clearfix margin-bottom">
                        <div>
                            <label class="col-md-1 col-sm-1 control-label">Payment</label>
                            <div class="col-md-2 col-sm-2 drop-down">
                                <asp:DropDownList ID="cbpaymentschedule" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </div>
                        </div>
                        <div>
                            <label class="col-md-1 col-sm-1 control-label">Amount</label>
                            <div class="col-md-2 col-sm-2 ">
                                <asp:TextBox ID="txamt" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </div>
                        </div>
                        <div>
                            <label class="col-md-1 col-sm-1 control-label">Date</label>
                            <div class="col-md-2 col-sm-2 drop-down-date">
                                <asp:TextBox ID="dtpaid" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="dtpaid_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtpaid">
                                </asp:CalendarExtender>
                            </div>
                        </div>
                        <div>
                            <label class="col-md-1 col-sm-1 control-label">VAT</label>
                            <div class="col-md-2 col-sm-2 drop-down-date">
                                <asp:RadioButtonList ID="rdVat" runat="server" CssClass="form-control input-sm radio radio-space-around no-margin" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="1">On</asp:ListItem>
                                    <asp:ListItem Value="0">Off</asp:ListItem>
                                </asp:RadioButtonList>
                            </div>
                        </div>
                        <div>
                            <div class="col-md-1 col-sm-1">
                                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn btn-success btn-add" OnClick="btadd_Click" />
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            <div class="margin-bottom " style="margin-top: 25px;">
                <div class="text-bold">Proposal Payment Schedule:</div>
                <div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" runat="server" CssClass="table table-stripd mygrid" AutoGenerateColumns="False" CellPadding="0" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowDataBound="grd_RowDataBound" OnRowCancelingEdit="grd_RowCancelingEdit">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lbschedule" runat="server" Text='<%# Eval("payment_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sequence">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsequence" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Amt To Be Paid">
                                        <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Amt To Be Paid">
                                        <ItemTemplate>
                                            <asp:Label ID="lbamt" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txamt" runat="server" Text='<%# Eval("qty") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vat">
                                        <ItemTemplate>
                                            <asp:Label ID="lbvat" runat="server" Text='<%# Eval("vat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtotal" runat="server" Text='<%# Eval("total") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paid Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpaiddate" runat="server" Text='<%# Eval("payment_dt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="dtpaidgrd" runat="server" Text='<%# Eval("payment_dt") %>'></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtpaidgrd" Format="d/M/yyyy"></asp:CalendarExtender>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <%--asp:CommandField HeaderText="Paid" ShowEditButton="True" />---%>
                                    <%--asp:CommandField HeaderText="Paid" ShowEditButton="True" />---%>
                                    <asp:CommandField HeaderText="Paid" ShowEditButton="True" />

                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle />
                                <SelectedRowStyle CssClass="table-edit" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div>
                    <asp:UpdatePanel runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd2" runat="server" CssClass="table table-stripd mygrid" AutoGenerateColumns="False" CellPadding="0" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowDataBound="grd_RowDataBound" OnRowCancelingEdit="grd_RowCancelingEdit">
                                <Columns>
                                    <asp:TemplateField HeaderText="Date Payment">
                                        <ItemTemplate>
                                            <asp:Label ID="lbschedule" runat="server" Text='<%# Eval("payment_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sequence">
                                        <ItemTemplate>
                                            <asp:Label ID="lbsequence" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="Amt To Be Paid">
                                        <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Amt To Be Paid">
                                        <ItemTemplate>
                                            <asp:Label ID="lbamt" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txamt" runat="server" Text='<%# Eval("qty") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Vat">
                                        <ItemTemplate>
                                            <asp:Label ID="lbvat" runat="server" Text='<%# Eval("vat") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtotal" runat="server" Text='<%# Eval("total") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Paid Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpaiddate" runat="server" Text='<%# Eval("payment_dt") %>'></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="dtpaidgrd" runat="server" Text='<%# Eval("payment_dt") %>'></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtpaidgrd" Format="d/M/yyyy"></asp:CalendarExtender>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PettyCash">
                                        <ItemTemplate>
                                            <asp:Label ID="lbPettyCash" runat="server" Text='<%# Eval("isPettycash") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PettyCash Employee">
                                        <ItemTemplate>
                                            <asp:Label ID="lbPettycashEmp_cd" runat="server" Text='<%# Eval("PettycashEmp_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <%--asp:CommandField HeaderText="Paid" ShowEditButton="True" />---%>
                                    <%--asp:CommandField HeaderText="Paid" ShowEditButton="True" />---%>
                                    <asp:CommandField HeaderText="Paid" ShowEditButton="True" />

                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle />
                                <SelectedRowStyle CssClass="table-edit" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

        </div>


    <asp:UpdatePanel runat="server" ID="uploadDoc">
        <ContentTemplate>
            <div class="h-divider"></div>
            <div class="clearfix margin-bottom">
                <label class="col-md-1 col-sm-4 control-label">Upload Document Sales</label>
                <div class="col-md-11 col-sm-8">
                    <asp:FileUpload ID="upl" runat="server"></asp:FileUpload>
                    <asp:HyperLink ID="hpfile_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                        <asp:Label ID="lblocfile" runat="server" Text='Sales Document'></asp:Label>
                    </asp:HyperLink>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="margin-bottom  margin-top padding-bottom row navi" style="padding-top: 45px;">
        <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" CssClass="divhid" />
        <asp:Button ID="btsearch2" runat="server" Text="Button" OnClick="btsearch2_Click" CssClass="divhid" />
        <asp:Button ID="btsearch3" runat="server" Text="Button" OnClick="btsearch3_Click" CssClass="divhid" />
        <asp:Button ID="btPaySrc" runat="server" Text="Button" OnClick="btPaySrc_Click" CssClass="divhid" />
        <asp:Button ID="btRdrcpt" runat="server" Text="Button" OnClick="btRdrcpt_Click" CssClass="divhid" />
        <asp:Button ID="bnew" runat="server" Text="New" CssClass="btn-success button2 btn add" OnClick="bnew_Click" />
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info  btn btn-print" OnClick="btprint_Click" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning  btn btn-save" OnClick="btsave_Click" />
    </div>

    </div>
</asp:Content>

