<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_EmpPettycashCashout.aspx.cs" Inherits="fm_EmpPettycashCashout" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <style>
        #excessAmountReturned
        {
          visibility: hidden;    
        }
        #excessAmountReturned2
        {
          visibility: hidden;    
        }
    </style>
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        //$(document).ready(function () {
        //    $('#pnlmsg').hide();
        //});

        function picSelected(sender, e) {
            $get('<%=hdpic.ClientID%>').value = e.get_value();
            $get('<%=btSelectPIC.ClientID%>').click();
        }

        function EmpAdvSelected(sender, e) {
            $get('<%=hdempadv.ClientID%>').value = e.get_value();
        }
    </script>
    <script>
        function SuppSelected(sender, e) {
            $get('<%=hdtax.ClientID%>').value = e.get_value();
        }

        function LookupSupplierSelected(sender, e) {
            $get('<%=hdlookuptax.ClientID%>').value = e.get_value();

        }
    </script>

    <script>
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 17) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '-5px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }



        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }


    </script>
    <script>
        function MakeStaticHeader1(gridId, height, width, headerHeight, isFooter) {
            var tbl1 = document.getElementById(gridId);
            if (tbl1) {
                var DivHR = document.getElementById('DivHeaderRow1');
                var DivMC = document.getElementById('DivMainContent1');
                var DivFR = document.getElementById('DivFooterRow1');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 17) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '-5px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl1.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl1.rows[tbl1.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****Copy Header in divHeaderRow****
                DivHR.appendChild(tbl1.cloneNode(true));
            }
        }



        function OnScrollDiv1(Scrollablediv) {
            document.getElementById('DivHeaderRow1').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow1').scrollLeft = Scrollablediv.scrollLeft;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdpic" runat="server" />
            <asp:HiddenField ID="hdtax" runat="server" />
            <asp:HiddenField ID="hdlookuptax" runat="server" />
            <asp:HiddenField ID="hdempadv" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <h4 class="divheader" style="background-color: #ADFF2F">Cash Advance Settlement</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="row margin-bottom">
                <%--<label class="control-label col-md-1">Process / Print</label>--%>
                <label class="control-label col-md-1">Process</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="ddlclaim" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlclaim_SelectedIndexChanged">
                        <asp:ListItem Value="No">Cash Advance Settlement Transaction</asp:ListItem>
                        <%--<asp:ListItem Value="Handover">Handover Cash Advance to Other Employee</asp:ListItem>--%>
                        <%--<asp:ListItem Value="Yes">Report</asp:ListItem>--%>
                    </asp:DropDownList>
                </div>
                <label class="control-label col-md-1 ">Employee</label>
                <%--<div class="col-md-3 drop-down">--%>
                <div class="col-md-2  drop-down">
                    <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlEmp_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-md-2">
                    <asp:LinkButton ID="btnShowData" runat="server" CssClass="btn btn-success" OnClick="btnShowData_Click"><i class="fa fa-print">&nbsp;Show Data</i></asp:LinkButton>
                </div>
            </div>

<%--            <asp:Panel runat="server" ID="PettycashRefno">
                <label class="col-md-1 col-sm-2 control-label">Pettycash Refno</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="ddlpettycashRefno" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlpettycashRefno_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
            </asp:Panel>--%>


            <div class="row margin-bottom">    
                <asp:Panel runat="server" id="panelToHide2">
                    <label class="control-label col-md-1">BALANCE PER EMPLOYEE</label>
                    <div class="col-md-2">
                        <div style="color: red; font-size: large">
                            <asp:Label ID="lbbalance" CssClass="control-label" runat="server" Text="0.00" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
<%--                <asp:Panel runat="server" id="panelToHide2_2">
                    <label class="control-label col-md-1">BALANCE PER REFNO</label>
                    <div class="col-md-2">
                        <div style="color: red; font-size: large">
                            <asp:Label ID="lbbalancerefno" CssClass="control-label" runat="server" Text="0.00" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>--%>   
                <asp:Panel runat="server" id="panel1">
                    <label class="control-label col-md-1">SETTLEMENT NOT YET APPROVED</label>
                    <div class="col-md-2">
                        <div style="color: red; font-size: large">
                            <asp:Label ID="lbbalanceNYA" CssClass="control-label" runat="server" Text="0.00" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>   
                <asp:Panel runat="server" id="panel2">
                    <label class="control-label col-md-1">AVAILABLE BALANCE</label>
                    <div class="col-md-2">
                        <div style="color: red; font-size: large">
                            <asp:Label ID="lbbalanceAVL" CssClass="control-label" runat="server" Text="0.00" Font-Bold="True" Font-Size="Large"></asp:Label>
                        </div>
                    </div>
                </asp:Panel>
            </div>

            <div class="row margin-bottom">  
<%--                <asp:Panel runat="server" id="panelToHide5">
                    <label class="col-md-1 col-sm-2 control-label">Pettycash Refno</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="ddlpettycashRefno" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlpettycashRefno_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </asp:Panel>  --%>                
                <asp:Panel runat="server" ID="hdnotclaim">

                    <label class="control-label col-md-1">Routine</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbroutine" CssClass="form-control" runat="server" AutoPostBack="True" onchange="javascript:ShowProgress();" OnSelectedIndexChanged="cbroutine_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <label class="control-label col-md-1">Cash Out Type</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbcategory" runat="server" CssClass="form-control" AutoPostBack="True" onchange="ShowProgress();" OnSelectedIndexChanged="cbcategory_SelectedIndexChanged"></asp:DropDownList>
                    </div>

                    <label class="control-label col-md-1">Cash Out Transaction</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbitem" runat="server" onchange="javascript:ShowProgress();" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="cbitem_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                                   
                </asp:Panel>


                <asp:Panel runat="server" id="HandoverEmployee">
                    <label class="control-label col-md-1 ">Handover To Employee</label>
                    <div class="col-md-2  drop-down">
                        <asp:DropDownList ID="ddlHandovertoemp" runat="server" CssClass="form-control input-sm" AutoPostBack="true" OnSelectedIndexChanged="ddlHandovertoemp_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </asp:Panel>      
                
                <asp:Panel runat="server" id="HandoverAmount">
                    <label class="control-label col-md-1">Amount To Handover</label>
                    <div class="col-md-2 require">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txamttohandover" CssClass="form-control require" style="z-index:1" runat="server" placeholder="Amount To Handover" ></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chvat" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
<%--                <asp:Panel runat="server" id="Remark">
                    <label class="control-label col-md-1 align-right">Remark</label>
                    <div class="col-md-5 require">
                        <asp:TextBox ID="txremark" CssClass="form-control" runat="server" placeholder="Remark"></asp:TextBox>
                    </div>  
                </asp:Panel>--%>
            </div>                

            <div class="row margin-bottom">    
                <asp:Panel runat="server" id="panelToHide1">
                    <label class="control-label col-md-1">Approval</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbapproval" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>

                    <label class="control-label col-md-1">PIC</label>
                    <div class="col-md-2 require">
                        <asp:TextBox ID="txpic" runat="server" CssClass="form-control small" placeholder="PIC"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txpic_AutoCompleteExtender" OnClientItemSelected="picSelected" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" ContextKey="true" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txpic" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                    </div>

                    <label class="control-label col-md-1">For Dept</label>
                    <div class="col-md-2">
                        <asp:TextBox ID="txdept" runat="server" CssClass="form-control small" placeholder="Dept" Enabled="false"></asp:TextBox>
                    </div>


                    <label class="control-label col-md-1">File</label>
                    <div class="col-md-2">
                        <asp:FileUpload ID="fucashout" CssClass="form-control" runat="server" />
                    </div>
                </asp:Panel>
            </div>

            <div class="row margin-bottom">
                <asp:Panel runat="server" id="ManualNo">                
                    <label class="control-label col-md-1 align-right">Manual No</label>
                    <div class="col-md-2 require">
                        <asp:TextBox ID="txmanualno" CssClass="form-control" runat="server" placeholder="Enter Manual No"></asp:TextBox>
                    </div> 
                </asp:Panel>
                <asp:Panel runat="server" id="TransactionDate">
                    <label class="control-label col-md-1">Transaction Date</label>
                    <div class="col-md-2 drop-down-date">
                        <asp:TextBox ID="dtcashout" CssClass="form-control" runat="server" Enabled="true"></asp:TextBox>
                        <asp:CalendarExtender ID="dtcashout_CalendarExtender" runat="server" TargetControlID="dtcashout" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </div>
                </asp:Panel>
                <asp:Panel runat="server" id="panelToHide3">
                    <label class="control-label col-md-1">Amount Spent</label>
                    <div class="col-md-2 require">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txamt" CssClass="form-control require" style="z-index:1" runat="server" placeholder="Amount"
                                    AutoPostBack="true" OnTextChanged="txamt_TextChanged" Text="0.00"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chvat" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1">Remark</label>
                    <div class="col-md-2">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txremark2" CssClass="form-control" runat="server" placeholder="Remark"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <label class="control-label col-md-1" id="excessAmountReturned">Excess Amount Returned</label>
                    <div class="col-md-2 require" id="excessAmountReturned2">
                        <asp:UpdatePanel runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txamtReturned" CssClass="form-control require" style="z-index:1" runat="server" placeholder="Amount Returned" >0.00</asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="chvat" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </asp:Panel>
          <%--      <asp:Panel runat="server" id="Remark2">  
                </asp:Panel>--%>
            </div>
            <div class="row margin-bottom">
                <asp:Panel runat="server" id="panelToHide4">
                    <div class ="col-sm-3">
                        <label class="control-label col-md-4">VAT included</label>
                        <div class="col-md-8">
                            <div class="input-group">
                                <div style="display:inline-block;">
                                    <asp:DropDownList ID="chvat" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="chvat_SelectedIndexChanged">
                                        <asp:ListItem Value="NONVAT">NON VAT</asp:ListItem>
                                        <asp:ListItem Value="VAT">WITH VAT</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                <div style="display:inline-block; vertical-align:top">
                                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                        <ContentTemplate>
                                            <asp:Label ID="lbvat" CssClass="control-label" runat="server" Text="0.00" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
                                        </ContentTemplate>
                                        <Triggers>
                                            <asp:AsyncPostBackTrigger ControlID="chvat" EventName="SelectedIndexChanged" />
                                        </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-12" >
                             <asp:UpdatePanel runat="server" style="display: flex;justify-content: space-between;align-items: center;">
                                <ContentTemplate>
                                    <asp:CheckBox ID="ckmunicipality" runat="server" Text="Municipality Tax" AutoPostBack="true" OnCheckedChanged="ckmunicipality_CheckedChanged" />
                                     <asp:Label ID="lblmunicipality" CssClass="control-label" runat="server" Text="0" Font-Bold="True" Font-Size="Large" ForeColor="Red"></asp:Label>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="ckmunicipality" EventName="CheckedChanged" />
                                </Triggers>
                            </asp:UpdatePanel>
                        </div>
                    </div> 
                    <div class="col-sm-6">
                        <label class="control-label col-md-4">Supplier</label>
                        <div class="col-md-8">
                            <table class="mGrid">
                                <tr>
                                    <th>Supplier Tax No:Supplier Name</th>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txtaxno" runat="server"></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="txtaxno_AutoCompleteExtender" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="SuppSelected" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txtaxno" UseContextKey="True">
                                                </asp:AutoCompleteExtender>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                    
                </asp:Panel>
                <div class="col-sm-3">
                     <label class="control-label col-md-4">Total Amount</label>
                    <div class="col-md-8">
                        <asp:UpdatePanel runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txtTotal" CssClass="form-control ro" runat="server" placeholder="Total" ></asp:TextBox>
                                    </ContentTemplate>
                            </asp:UpdatePanel>
                    </div>
                    <asp:Panel runat="server" id="ClaimCashout" visible="true" CssClass="col-sm-12 row" style="margin-top:10px;">
                        <label class="control-label col-sm-4">Paid Claim Cashout</label>
                        <div class="col-md-8 drop-down">
                                <asp:DropDownList ID="ddlClaimCashout" CssClass="form-control" runat="server">
                                    <asp:ListItem Value="No">No</asp:ListItem>
                                    <asp:ListItem Value="Yes">Yes</asp:ListItem>
                                </asp:DropDownList>
                        </div>
                    </asp:Panel>
                </div>
                
            </div>

            <div class="row margin-bottom padding-top" style="text-align: center">
                <div class="col-md-12" style="text-align: center">
                    <asp:LinkButton ID="btStatus" runat="server" CssClass="btn btn-sm btn-primary" OnClick="btStatus_Click">&nbsp;Check Status <span class="fa fa-search"></span></asp:LinkButton>
                    <asp:LinkButton ID="New" CssClass="btn btn-primary" runat="server" OnClick="New_Click"><i class="fa fa-plus">&nbsp;New</i></asp:LinkButton>
                    <asp:LinkButton ID="btsave" CssClass="btn btn-success" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;Save</i></asp:LinkButton>
                    <asp:LinkButton ID="btprint" CssClass="btn btn-warning" runat="server" OnClick="btprint_Click"><i class="fa fa-print">&nbsp;Print Pettycash Cash Out Report</i></asp:LinkButton>
                    <asp:Button ID="btSelectPIC" runat="server" Style="display: none" OnClick="btSelectPIC_Click" Text="Button" />
                </div>

            </div>


        </div>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="form-group">
                <div id="tbl" runat="server" class="table">
                    <div id="DivRoot" align="left">
                        <div style="overflow: hidden;" id="DivHeaderRow">
                        </div>
                        <div style="overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" Width="100%" ShowFooter="True" OnRowDataBound="grd_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Date">
                                        <ItemTemplate>
                                            <asp:Label ID="dtTrans" runat="server" Text='<%# Eval("transDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Type">
                                        <ItemTemplate>
                                            <asp:Label ID="doc_typ" runat="server" Text='<%# Eval("doc_typ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction No">
                                        <ItemTemplate>
                                            <asp:Label ID="doc_no" runat="server" Text='<%# Eval("doc_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cashout Code">
                                        <ItemTemplate>
                                            <asp:Label ID="cashout_cd" runat="server" Text='<%# Eval("cashout_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cashout Item Code">
                                        <ItemTemplate>
                                            <asp:Label ID="itemco_cd" runat="server" Text='<%# Eval("itemco_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cashout Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="itemco_nm" runat="server" Text='<%# Eval("itemco_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="amount" runat="server" Text='<%# Eval("amount") %>' Style="color: red"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotlabel" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit">
                                        <ItemTemplate>
                                            <asp:Label ID="debit" runat="server" Text='<%# Eval("debitX") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotdebit" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="credit" runat="server" Text='<%# Eval("creditX") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotcredit" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="balance" runat="server" Text='<%# Eval("currentbalance") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Manual Number">
                                        <ItemTemplate>
                                            <asp:Label ID="manual_number" runat="server" Text='<%# Eval("manual_number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pettycash Emp">
                                        <ItemTemplate>
                                            <asp:Label ID="PettycashEmp" runat="server" Text='<%# Eval("PettycashEmp") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbballabel" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pettycash Ref No.">
                                        <ItemTemplate>
                                            <asp:Label ID="PettycashRefno" runat="server" Text='<%# Eval("PettycashRefno") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbbalance" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Status">
                                        <ItemTemplate>
                                            <asp:Label ID="tran_status" runat="server" Text='<%# Eval("tran_status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="table-footer" BackColor="Yellow" Font-Bold="True" Font-Size="Larger" />
                            </asp:GridView>
                        </div>

                        <div id="DivFooterRow" style="overflow: hidden">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="container-fluid">
        <div class="row">
            <div class="form-group">
                <div id="tbl1" runat="server" class="table">
                    <div id="DivRoot1" align="left">
                        <div style="overflow: hidden;" id="DivHeaderRow1">
                        </div>
                        <div style="overflow: scroll;" onscroll="OnScrollDiv1(this)" id="DivMainContent1">
                            <asp:GridView ID="grd1" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" Width="100%" ShowFooter="True" OnRowDataBound="grd_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="No.">
                                        <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Date">
                                        <ItemTemplate>
                                            <asp:Label ID="dtTrans" runat="server" Text='<%# Eval("transDate") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Type">
                                        <ItemTemplate>
                                            <asp:Label ID="doc_typ" runat="server" Text='<%# Eval("doc_typ") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction No">
                                        <ItemTemplate>
                                            <asp:Label ID="doc_no" runat="server" Text='<%# Eval("doc_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cashout Item Code">
                                        <ItemTemplate>
                                            <asp:Label ID="itemco_cd" runat="server" Text='<%# Eval("itemco_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cashout Item Name">
                                        <ItemTemplate>
                                            <asp:Label ID="itemco_nm" runat="server" Text='<%# Eval("itemco_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PIC">
                                        <ItemTemplate>
                                            <asp:Label ID="manual_number" runat="server" Text='<%# Eval("pic") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="amount" runat="server" Text='<%# Eval("amount") %>' Style="color: red"></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotlabel" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Debit">
                                        <ItemTemplate>
                                            <asp:Label ID="debit" runat="server" Text='<%# Eval("debitX") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotdebit" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <ItemTemplate>
                                            <asp:Label ID="credit" runat="server" Text='<%# Eval("creditX") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotcredit" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
<%--                                    <asp:TemplateField HeaderText="Balance">
                                        <ItemTemplate>
                                            <asp:Label ID="balance" runat="server" Text='<%# Eval("currentbalance") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbtotAmount" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField HeaderText="Manual Number">
                                        <ItemTemplate>
                                            <asp:Label ID="manual_number" runat="server" Text='<%# Eval("manual_number") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pettycash Emp">
                                        <ItemTemplate>
                                            <asp:Label ID="PettycashEmp" runat="server" Text='<%# Eval("PettycashEmp") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbballabel" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Pettycash Ref No">
                                        <ItemTemplate>
                                            <asp:Label ID="PettycashRefNo" runat="server" Text='<%# Eval("PettycashRefNo") %>'></asp:Label>
                                        </ItemTemplate>
                                        <FooterTemplate>
                                            <asp:Label ID="lbbalance" runat="server"></asp:Label>
                                        </FooterTemplate>
                                        <HeaderStyle HorizontalAlign="Right" />
                                        <ItemStyle HorizontalAlign="Right" />
                                        <FooterStyle HorizontalAlign="Right" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Transaction Status">
                                        <ItemTemplate>
                                            <asp:Label ID="tran_status" runat="server" Text='<%# Eval("tran_status") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>

                                <FooterStyle CssClass="table-footer" BackColor="Yellow" Font-Bold="True" Font-Size="Larger" />
                            </asp:GridView>
                        </div>

                        <div id="DivFooterRow1" style="overflow: hidden">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" style="display: none" id="pnlmsg">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

