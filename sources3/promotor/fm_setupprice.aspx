<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_setupprice.aspx.cs" Inherits="promotor_fm_setupprice" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hiditem.ClientID%>').value = e.get_value();
            $get('<%=btprice.ClientID%>').click();
        }
    </script>
    <div class="container">
        <div class="form-horizontal" style="font-family:Calibri;font-size:small">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:HiddenField ID="hiditem" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            
            <h4 class="jajarangenjang">Setup Exhibition Price</h4>
            <div class="h-divider">
                 </div>
            <div class="form-group">
                <label class="control-label col-md-1">Exhibition</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbexhibition" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbexhibition_SelectedIndexChanged"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1">Location</label>
                <div class="col-md-3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                             <asp:Label ID="lblocation" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbexhibition" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                   
                </div>
                
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Item</label>
                <div class="col-md-12">
                    <table style="width:100%">
                        <tr><th class="header" style="width:40%">Item</th><th class="header">Buy Price</th><th class="header">Sell Price</th><th class="header">Save</th></tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txitem" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txitem" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="ItemSelected">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                          <asp:TextBox ID="txbuyprice" runat="server" CssClass="form-control ro"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btprice" EventName="click" />
                                    </Triggers>
                                </asp:UpdatePanel>
                              </td>
                            <td>
                                <asp:TextBox ID="txsellprice" runat="server" CssClass="form-control"></asp:TextBox>
                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" runat="server" CssClass="btn btn-inf" OnClick="btadd_Click">Save</asp:LinkButton>
                                <asp:Button ID="btprice" runat="server" OnClick="btprice_Click" Text="Button" style="display:none" />
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                             <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header"
                        RowStyle-CssClass="rows" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded"><ItemTemplate><%# Eval("branded_nm") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Buy Price">
                                <ItemTemplate><%# Eval("buyprice") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Sell Price">
                                <ItemTemplate><%# Eval("sellprice") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" HeaderText="Select" />
                        </Columns>
                        <HeaderStyle CssClass="header"></HeaderStyle>
                        <PagerStyle CssClass="pager"></PagerStyle>
                        <RowStyle CssClass="rows"></RowStyle>
                    </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbexhibition" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                   
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12" style="text-align:center">
                    <asp:LinkButton ID="btprint" CssClass="btn btn-primary" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

