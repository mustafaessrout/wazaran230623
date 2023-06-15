<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_exhibitdiscount.aspx.cs" Inherits="fm_exhibitdiscount" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
        }

        function SelectData(sVal)
        {
            $get('<%=hddisccode.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hditem" runat="server" />
    <asp:HiddenField ID="hddisccode" runat="server" />
    <%--<div class="container">--%>
        <div class="form-horizontal" style="font-family:Calibri;font-size:small">
            <h4 class="jajarangenjang">Exhibition Discount</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Disc Code</label>
                <div class="col-md-2">
                    <div class="input-group">
                        <asp:Label ID="lbdisccode" runat="server" Text="" CssClass="form-control input-group-sm"></asp:Label>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <label class="control-label col-md-1">Exhibition</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbexhibition" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbexhibition_SelectedIndexChanged"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1">Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtdiscount" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="form-group">
                 <label class="control-label col-md-1">Remark</label>
                <div class="col-md-11">
                    <asp:TextBox ID="txremark" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
             <h5 class="jajarangenjang">Item Tobe Hit By Discount</h5>
            <div class="h-divider">
                
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Item</label>
                <div class="col-md-8">
                    <asp:TextBox ID="txitem" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" OnClientItemSelected="ItemSelected" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txitem" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" ShowOnlyCurrentWordInCompletionListItem="true" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1"> 
                                </asp:AutoCompleteExtender>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btadditem" runat="server" CssClass="btn btn-primary" OnClick="btadditem_Click">Add</asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-1"></div>
                <div class="col-md-11">
                    <asp:GridView ID="grditem" runat="server" CssClass="mydatagrid" HeaderStyle-CssClass="header" 
                        RowStyle-CssClass="rows" PagerStyle-CssClass="pager" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size"><ItemTemplate><%# Eval("size") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded"><ItemTemplate><%# Eval("branded_nm") %></ItemTemplate></asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                    </asp:GridView>
                </div>
            </div>
            <h5 class="jajarangenjang">Discount Formula</h5>
            <div class="h-divider"></div>

            <div class="form-group">
                <div class="col-md-1"></div>
                <div class="col-md-11">
                    <table style="width:100%">
                        <tr>
                            <th class="header" style="width:20%">Min Qty</th>
                            <th class="header" style="width:15%">UOM</th>
                            <th class="header" style="width:10%">Free Cash</th>
                            <th class="header" style="width:10%">Type</th>
                            <th class="header" style="width:5%">Add</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txminqty" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="cbuom" CssClass="form-control" runat="server"></asp:DropDownList>
                            </td>
                             <td>
                                <asp:TextBox ID="txfreecash" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="cbdiscmethod" CssClass="form-control" runat="server"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" CssClass="btn btn-primary" runat="server" OnClick="btadd_Click">Add</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-1"></div>
                <div class="col-md-11">
                    <asp:GridView ID="grd" CssClass="mydatagrid" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Min Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbminqty" runat="server" Text='<%# Eval("minqty") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Cash">
                                <ItemTemplate><%# Eval("freecash") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate><%#Eval("disc_method") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" ShowDeleteButton="True" />
                        </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12" style="text-align:center">
                    <asp:LinkButton ID="btnew" CssClass="btn btn-primary" runat="server">New</asp:LinkButton>
                    <asp:LinkButton ID="btsave" CssClass="btn btn-info" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" CssClass="btn btn-danger" runat="server">Print</asp:LinkButton>
                    <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" style="display:none" />
                </div>
            </div>
        </div>
    <%--</div>--%>
        <script>
            $(document).ready(function () {
                $("#<%=btsearch.ClientID%>").click(function () {
             PopupCenter('lookupdiscount.aspx', 'xtf', '900', '500');
         });
     });


    </script> 
</asp:Content>

