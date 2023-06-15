<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_bor.aspx.cs" Inherits="fm_bor" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>
     <script>
         function ShowProgress() {
             $('#pnlmsg').show();
         }

         function HideProgress() {
             $("#pnlmsg").hide();
             return false;
         }
         $(document).ready(function () {
             $('#pnlmsg').hide();
         });

    </script>
    <style type="text/css">
        .auto-style1 {
            height: 29px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hditem" runat="server" />
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Branch Order Request</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Status</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbstatus" CssClass="form-control" runat="server"></asp:DropDownList>
                 </div>  
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-2">
                    <asp:Label ID="lbsalespoint" runat="server" Text="" CssClass="form-control"></asp:Label>
                </div>
                <label class="control-label col-md-1">BOR No</label>
                <div class="col-md-2">
                    <div class="input-group">
                        <asp:Label ID="txborno" runat="server" Text="" CssClass="form-control input-group-sm"></asp:Label>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearchbor" runat="server" CssClass="btn btn-primary"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                    </div>
                   
                </div>
                <label class="control-label col-md-1">Date</label>
                <div class="col-md-2 drop-down-date">
                    <asp:TextBox ID="dtbor" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtbor_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtbor">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Estimate Deliver</label>
                <div class="col-md-2 drop-down-date">
                    <asp:TextBox ID="dtestimate" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtestimate_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtestimate">
                    </asp:CalendarExtender>
                </div>
                 <label class="control-label col-md-1">Destination</label>
                <div class="col-md-2 drop-down">
                    <asp:DropDownList ID="cbdestinatio" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbdestinatio_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <table class="table table-fix"><tr><th>Option</th><th>description</th></tr>
                        <tr><td>1.Direct Customer</td><td>Send from factory to customer directly</td></tr>
                        <tr><td class="auto-style1">2.Depo</td><td class="auto-style1">Send from CWH/Factory to branch depo</td></tr>
                        <tr><td>3.Sub Depo</td><td>Send from factory to customer directly</td></tr>
                    </table>
                </div>
                <div class="col-md-3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                             <asp:TextBox ID="txcustomer" CssClass="form-control" runat="server"></asp:TextBox>
                             <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" MinimumPrefixLength="1" FirstRowSelected="false" EnableCaching="false" OnClientItemSelected="CustSelected" CompletionInterval="1" CompletionSetCount="10" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txcustomer" UseContextKey="True">
                             </asp:AutoCompleteExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   
                </div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Remark</label>
                <div class="col-md-11">
                    <asp:TextBox ID="txremark" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                 <label class="control-label col-md-1">Cust PO No</label>
                <div class="col-md-2">
                    <asp:TextBox ID="txcustpono" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                 <label class="control-label col-md-1">Cust PO Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtcustpo" CssClass="form-control" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="dtcustpo_CalendarExtender" runat="server" TargetControlID="dtcustpo" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
            </div>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-12">
                    <table class="mGrid"><tr style="background-color:silver"><th style="width:40%">Item</th><th>Size</th><th>Branded</th><th>Current Stock</th><th>Unit Price</th><th>Order</th><th>UOM</th><th>Action</th></tr>
                        <tr><td>
                            <asp:TextBox ID="txitemsearch" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txitemsearch" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionInterval="1" CompletionSetCount="10" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected"> 
                            </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                         <asp:Label ID="lbsize" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger EventName="click" ControlID="btsearch" />
                                    </Triggers>
                                </asp:UpdatePanel>
                               </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                 <ContentTemplate>
                                         <asp:Label ID="lbbranded" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger EventName="click" ControlID="btsearch" />
                                    </Triggers>
                                 </asp:UpdatePanel>
                            </td>
                            <td>
                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                 <ContentTemplate>
                                          <asp:Label ID="lbcurrentstock" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger EventName="click" ControlID="btsearch" />
                                    </Triggers>
                                 </asp:UpdatePanel>
                               
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbunitprice" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:TextBox ID="txqty" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <div class="drop-down">
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                          <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control drop-down"></asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                              </div>
                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" CssClass="btn btn-primary" OnClientClick="javascript:ShowProgress();" runat="server" OnClick="btadd_Click">ADD</asp:LinkButton>
                            </td>
                        </tr>
                    </table>

                </div>
                <div class="col-md-12">
                    <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%#Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded">
                                <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Stock Avl">
                                <ItemTemplate><%#Eval("stock_amt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty Order">
                                <ItemTemplate><%#Eval("qty") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unit Price">
                                <ItemTemplate><%#Eval("unitprice") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12" style="text-align:center">
                    <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" />
                    <asp:LinkButton ID="btnew" CssClass="btn btn-primary" runat="server">New</asp:LinkButton>
                    <asp:LinkButton ID="btsave" CssClass="btn btn-success" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" CssClass="btn btn-danger" runat="server">Print</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
      <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
</asp:Content>

