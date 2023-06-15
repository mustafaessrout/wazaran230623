<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_addlose_van.aspx.cs" Inherits="fm_addlose_van" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        function SalesmanSelected(sender, e) {
            $get('<%=hdsalesman.ClientID%>').value = e.get_value();
        }
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btnprice.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdsalesman" runat="server" />
    <asp:HiddenField ID="hditem" runat="server" />
    <div class="alert alert-info text-bold">Stock Add or Lose for VAN Salesman</div>
    <div class="container">
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm input-sm col-sm-1">Trans No.</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="txaddlosenumber" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();PopupCenter('lookupwsstockin.aspx','Lookup',800,600);" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtaddlose" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtaddlose_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtaddlose">
                </asp:CalendarExtender>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Transaction Type</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbtranstype" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Salesman</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbsalesman" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label-sm input-sm col-sm-1">Remark</label>
            <div class="col-sm-11">
                <asp:TextBox ID="txremark" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-9">
                <table class="mGrid">
                    <tr>
                        <th style="width: 40%">Item</th>
                        <th style="width: 10%">Qty Ctn</th>
                        <th style="width: 10%">Qty Pcs</th>
                        <th style="width: 10%">Unitprice CTN</th>
                        <th style="width: 10%">Unitprice PCS</th>
                        <th style="width: 20%">VAT Rate</th>
                        <th>Add</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txitem" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected" ServiceMethod="GetItemList" UseContextKey="true" TargetControlID="txitem">
                            </asp:AutoCompleteExtender>
                        </td>

                        <td>
                            <asp:TextBox ID="txctn" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txpcs" runat="server" CssClass="form-control input-sm"></asp:TextBox></td>

                        <td>
                            <asp:TextBox ID="txunitpricecarton" runat="server"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txunitpricepcs" runat="server"></asp:TextBox>
                        </td>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbvatrate" CssClass="form-control input-sm" runat="server"></asp:DropDownList>

                        </td>
                        <td>
                            <asp:LinkButton ID="btadd" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btadd_Click">Add</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-sm-3" style="background-color: yellow">
                Loss will increasing salesman balance, Add will decrease salesman balance
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Ctn">
                            <ItemTemplate><%#Eval("qty_ctn") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Pcs">
                            <ItemTemplate><%#Eval("qty_pcs") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unitprice Ctn">
                            <ItemTemplate><%#Eval("unitprice_ctn") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unitprice Pcs">
                            <ItemTemplate><%#Eval("unitprice_pcs") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row margin-bottom margin-top">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btsave" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                <asp:LinkButton ID="btprint" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" runat="server">Print</asp:LinkButton>
                <asp:Button OnClientClick="ShowProgress();" ID="btnprice" runat="server" OnClick="btnprice_Click" Text="Button" />
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

