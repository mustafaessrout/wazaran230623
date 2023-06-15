<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_stockin.aspx.cs" Inherits="fm_stockin" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
        }

        function SelectData(sVal)
        {
            $get('<%=txstockin.ClientID%>').value = sVal;
            $get('<%=hdstockin.ClientID%>').value = sVal;
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hditem" runat="server" />
    <asp:HiddenField ID="hdstockin" runat="server" />
    <div class="container">
           <div class="form-horizontal">
            <h4 class="jajarangenjang">Stock In Exhibition</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                 <label class="control-label col-md-1">StockIn No</label>
                <div class="col-md-3">
                    <div class="input-group">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                  <asp:TextBox ID="txstockin" runat="server" CssClass="form-control input-group-sm ro"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                       
                       <div class="input-group-btn">
                           <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                       </div>
                   </div>
                </div>
                <label class="control-label col-md-1">Exhibition</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbexhibition" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbexhibition_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Man No</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txmanualno" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
            </div>
               <%--<div class="form-group">
               <%-- <label class="control-label col-md-1">Section</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbboth" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbboth_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Promoter Sect</label>
                 <div class="col-md-3">
                     <asp:Label ID="lbpromoter" runat="server" Text="" CssClass="form-control"></asp:Label>
                 </div>
               </div>--%>
            <div class="form-group">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1">Warehouse</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbwarehouse" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbwarehouse_SelectedIndexChanged"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1">Bin</label>
                <div class="col-md-3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                              <asp:DropDownList ID="cbbin" runat="server" CssClass="form-control"></asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbwarehouse" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                  
                </div>
            </div>
            <h5 class="jajarangenjang">Section Available</h5>
               <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView Width="50%" ID="grdprod" CssClass="mGrid" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            
                            <asp:TemplateField HeaderText="Product Name / Section">
                                <ItemTemplate><%#Eval("prod_desc") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Promoter">
                                <ItemTemplate><%#Eval("emp_desc") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>


                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                <table style="width:100%">
                    <tr class="header"><th style="width:40%">Item</th><th style="width:10%">qty</th><th style="width:20%">UOM</th><th style="width:10%">Add</th></tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txitemname" runat="server" CssClass="form-control"></asp:TextBox>
                           <asp:AutoCompleteExtender ID="txitemname_AutoCompleteExtender"  runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txitemname" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected" ShowOnlyCurrentWordInCompletionListItem="true">
                             </asp:AutoCompleteExtender>
                        </td>
                        <td>
                            <asp:TextBox ID="txqty" runat="server" CssClass="form-control"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control"></asp:DropDownList>
                        </td>
                        <td>
                             <asp:LinkButton ID="btadd" runat="server" CssClass="btn btn-primary" OnClick="btadd_Click">Add</asp:LinkButton>
                        </td>
                    </tr>
                </table>
                </div>
            </div>
               <div class="form-group">
             <div class="col-md-12">
                 <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" HeaderStyle-CssClass="header" 
                     RowStyle-CssClass="rows" PagerStyle-CssClass="pager" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                     <Columns>
                         <asp:TemplateField HeaderText="Item Code">
                             <ItemTemplate>
                                 <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label></ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Item Name">
                             <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Size">
                             <ItemTemplate><%# Eval("size") %></ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Branded">
                             <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Qty">
                             <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="UOM">
                             <ItemTemplate><%# Eval("uom") %></ItemTemplate>
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
                <div class="col-md-12">
                    <div style="text-align:center">
                        <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click">New</asp:LinkButton>
                        <asp:LinkButton ID="btsaved" runat="server" CssClass="btn btn-danger" OnClick="btsaved_Click">Save</asp:LinkButton>
                        <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-info" OnClick="btprint_Click">Print</asp:LinkButton>
                        <asp:Button ID="btlookup" runat="server" Text="Button" OnClick="btlookup_Click" style="display:none" />
                    </div>
                </div>
            </div>
        </div>
    </div>
        <script>
            $(document).ready(function () {
                $("#<%=btsearch.ClientID%>").click(function () {
            PopupCenter('lookupstockin.aspx', 'xtf', '900', '500');
        });
    });


    </script>  
</asp:Content>

