<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_exhibitorder.aspx.cs" Inherits="promotor_fm_exhibitorder" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btprice.ClientID%>').click();
        }

        function SelectData(sVal)
        {
            $get('<%=hdso.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();

        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
 
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hditem" runat="server" />
            <asp:HiddenField ID="hdso" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    
    
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Order</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Order No</label>
                <div class="col-md-2">
                    <div class="input-group">
                            <asp:Label ID="lborderno" runat="server" Text="" CssClass="form-control input-group-sm"></asp:Label>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                            </div>
                    </div>
                
                </div>
                  <label class="control-label col-md-1">Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtorder" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                  <label class="control-label col-md-1">Exhibition</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                             <asp:DropDownList ID="cbexhibition" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbexhibition_SelectedIndexChanged"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                   
                </div>
                  <label class="control-label col-md-1">Section</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbbooth" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbbooth_SelectedIndexChanged"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
            </div>
            <h5 class="jajarangenjang">Item</h5>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-12">
                    <table style="width:100%">
                        <tr>
                            <th class="header" style="width:40%">Item</th>
                            <th class="header" style="width:10%">Stock Avl</th>
                            <th class="header" style="width:10%">Qty</th>
                            <th class="header" style="width:10%">Uom</th>
                            <th class="header" style="width:10%">Unitprice</th>
                            <th class="header" style="width:10%">Add</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txitem" CssClass="form-control" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txitem" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" ShowOnlyCurrentWordInCompletionListItem="true" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelected">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbstockavl" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btprice" EventName="click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:TextBox ID="txqty" CssClass="form-control" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:DropDownList ID="cbuom" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbuom_SelectedIndexChanged"></asp:DropDownList>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>
                                         <asp:Label ID="lbunitprice" runat="server" Text="" CssClass="form-control"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbuom" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                               
                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" CssClass="btn btn-primary" runat="server" OnClick="btadd_Click">Add</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" RowStyle-CssClass="rows" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%#Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded">
                                <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unitprice">
                                <ItemTemplate>
                                    <asp:Label ID="lbunitprice" runat="server" Text='<%#Eval("unitprice") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shipment">
                                <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Subtotal">
                                <ItemTemplate>
                                    <%#Eval("subtotal") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" />
                        </Columns>

<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>

                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12" style="text-align:center">
                    <asp:LinkButton ID="btdisccalc" runat="server" CssClass="btn btn-primary" OnClick="btdisccalc_Click">Discount Calculation</asp:LinkButton>
                </div>
            </div>
            <h5 class="jajarangenjang">Free Cash Discount</h5>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-4">
                
                    <asp:GridView ID="grddiscount" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Vertical" EmptyDataText="No Discount For This Order!" ShowHeaderWhenEmpty="True">
                        <AlternatingRowStyle BackColor="#DCDCDC" />
                        <Columns>
                            <asp:TemplateField HeaderText="Disc Code">
                                <ItemTemplate><%#Eval("disc_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate><%#Eval("remark") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Cash">
                                <ItemTemplate><%#Eval("freecash","{0:G2}") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
                        <HeaderStyle BackColor="#000084" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                        <RowStyle BackColor="#EEEEEE" ForeColor="Black" />
                        <SelectedRowStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                        <SortedAscendingHeaderStyle BackColor="#0000A9" />
                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                        <SortedDescendingHeaderStyle BackColor="#000065" />
                    </asp:GridView>
                
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12" style="text-align:center">
                    <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click">New</asp:LinkButton>
                    <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-danger" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-info" OnClick="btprint_Click">Print</asp:LinkButton>
                    <asp:Button ID="btprice" runat="server" Text="Button" OnClick="btprice_Click" />
                    <asp:Button ID="btlookup" runat="server" OnClick="btlookup_Click" Text="Button" />
                </div>
            </div>
        </div>
   
    <script>
     $(document).ready(function () {
            $("#<%=btsearch.ClientID%>").click(function () {
               PopupCenter('lookupexhso.aspx', 'xtf', '900', '500');  
            });
        });

       
    </script>  
</asp:Content>

