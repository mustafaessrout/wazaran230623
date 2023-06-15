<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_exhibitioninttrf.aspx.cs" Inherits="promotor_fm_exhibitioninttrf" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function SelectedItem(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btprice.ClientID%>').click();
        }

        function RefreshData(sval)
        {
            $get('<%=hdtrfno.ClientID%>').value = sval;
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hditem" runat="server" />
            <asp:HiddenField ID="hdtrfno" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
    

        <div class="form-horizontal" style="font-family:Calibri;font-size:small">
            <h4 class="jajarangenjang">Internal Transfer</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                 <label class="control-label col-md-2">Internal Transfer Type</label>
                 <div class="col-md-6">
                     <asp:DropDownList ID="rdtrftype" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="rdtrftype_SelectedIndexChanged">
                         <asp:ListItem Value="DS">Depo To Section</asp:ListItem>
                         <asp:ListItem Value="SD">Section To Depo</asp:ListItem>
                     </asp:DropDownList>
                 </div>
            </div>
            <div class="form-group">
                  <label class="control-label col-md-1">Trf No</label>
                <div class="col-md-2">
                    <div class="input-group">
                        <asp:Label ID="lbtrfno" runat="server" CssClass="form-control" Text=""></asp:Label>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                        </div>
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                         <label class="control-label col-md-1" id="lbdepo" runat="server">From Depo</label>
                    </ContentTemplate>
                </asp:UpdatePanel>
               
                <div class="col-md-4">
                    <asp:DropDownList ID="cbexhibition" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbexhibition_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                         <label class="control-label col-md-1" id="lbsection" runat="server">To Section</label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdtrftype" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                              <asp:DropDownList ID="cbbooth" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbbooth_SelectedIndexChanged"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbexhibition" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                  
                </div>
                <div class="col-md-1">
                     <div class="col-md-3">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbpromoter" runat="server" Text=""></asp:Label>
                        </ContentTemplate>
                          <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdtrftype" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                </div>
                </div>
            </div>
            <%--<div class="form-group">
                <label class="control-label col-md-1">Promoter Section</label>
                <div class="col-md-3">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbpromoter" runat="server" Text=""></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>--%>
            <div class="form-group">
                 <label class="control-label col-md-1">Item</label>
                <div class="col-md-4">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txitem" CssClass="form-control" runat="server"></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" OnClientItemSelected="SelectedItem" ServiceMethod="GetCompletionList" TargetControlID="txitem" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true">
                            </asp:AutoCompleteExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
                 <label class="control-label col-md-1">Stock Avl</label>
                <div class="col-md-1">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbqty" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btprice" EventName="click" />
                        </Triggers>
                    </asp:UpdatePanel>
                    
                </div>
                 <label class="control-label col-md-1">Qty Trf</label>
                 <div class="col-md-1">
                     <asp:TextBox ID="txtrfamt" runat="server" CssClass="form-control"></asp:TextBox>
                 </div>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btadd" runat="server" CssClass="btn btn-primary" OnClick="btadd_Click">Add</asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-1"></div>
                <div class="col-md-11">
                    <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" RowStyle-CssClass="rows" HeaderStyle-CssClass="header" PagerStyle-CssClass="pager" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name"><ItemTemplate><%# Eval("item_nm") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Size"><ItemTemplate><%# Eval("size") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded"><ItemTemplate><%# Eval("branded_nm") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty"><ItemTemplate><%# Eval("qty") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM"><ItemTemplate><%# Eval("uom") %></ItemTemplate></asp:TemplateField>
                            <asp:CommandField HeaderText="Delete" ShowDeleteButton="True" />
                        </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12" style="text-align:center">
                    <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click">New</asp:LinkButton>
                    <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-danger" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-info" OnClick="btprint_Click">Print</asp:LinkButton>
                    <asp:Button ID="btprice" runat="server" Text="Button" OnClick="btprice_Click" style="display:none" />
                    <asp:Button ID="btlookup" runat="server" OnClick="btlookup_Click" Text="Button" />
                </div>
            </div>
        </div>
    
       <script>
           $(document).ready(function () {
               $("#<%=btsearch.ClientID%>").click(function () {
            PopupCenter('lookupexhibitinttrf.aspx', 'xtf', '900', '500');
        });
    });


    </script> 
</asp:Content>

