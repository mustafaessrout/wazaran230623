<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_exhibitionother.aspx.cs" Inherits="fm_exhibitionother" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=cbuom.ClientID%>').value = "";
            $get('<%=lbunitprice.ClientID%>').value = "";
            $get('<%=txqty.ClientID%>').value = "";
            $get('<%=btlookup.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hditem" runat="server" />
      <%--<div class="container">--%>
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Other Transaction Exhibition</h4>
            <div class="h-divider"></div>
             <div class="form-group">
                 <label class="control-label col-md-1">Trans No</label>
                 <div class="col-md-2">
                         <div class="input-group">
                             <asp:Label ID="lbtransno" runat="server" Text="" CssClass="form-control input-group-sm"></asp:Label>
                             <div class="input-group-btn">
                                 <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                             </div>
                         </div>
                 </div>
                 <label class="control-label col-md-1">Trans Type</label>
                 <div class="col-md-2">
                     <asp:DropDownList ID="cbtranstype" runat="server" CssClass="form-control"></asp:DropDownList>
                 </div>
                 <label class="control-label col-md-1">Exhibition</label>
                 <div class="col-md-4">
                     <asp:DropDownList ID="cbexhibition" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbexhibition_SelectedIndexChanged"></asp:DropDownList>
                 </div>
             </div>
             <div class="form-group">
                  <label class="control-label col-md-1">Remark</label>
                 <div class="col-md-6">
                     <asp:TextBox ID="txremark" CssClass="form-control" TextMode="MultiLine" runat="server"></asp:TextBox>
                 </div>
                <label class="control-label col-md-1">Section</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="cbbooth" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbbooth_SelectedIndexChanged"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <table style="width:100%" class="mydatagrid">
                        <tr>
                            <th class="header" style="width:40%;text-align:left">Item</th>
                             <th class="header" style="width:5%;text-align:left">Stock Avl</th>
                            <th class="header" style="width:5%;text-align:left">qty</th>
                            <th class="header" style="width:10%;text-align:left">uom</th>
                            <th class="header" style="width:10%;text-align:left">Unitprice</th>
                            <th class="header" style="text-align:left">Add</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txitem" runat="server" CssClass="form-control" placeholder="Enter code or name"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txitem" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" MinimumPrefixLength="1" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelected">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                           <asp:Label ID="lbstockavl" CssClass="form-control" runat="server"></asp:Label>
                                    </ContentTemplate>
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
                                         <asp:Label ID="lbunitprice" CssClass="form-control" runat="server" Text=""></asp:Label>
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
                   <asp:GridView ID="grd" CssClass="mydatagrid" RowStyle-CssClass="rows" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" runat="server" AutoGenerateColumns="False">
                       <Columns>
                           <asp:TemplateField HeaderText="Item Code">
                               <ItemTemplate>
                                   <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Item Name">
                               <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Size">
                               <ItemTemplate><%#Eval("size") %></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Branded">
                               <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="Qty">
                               <ItemTemplate><%#Eval("qty") %></ItemTemplate>
                           </asp:TemplateField>
                           <asp:TemplateField HeaderText="UOM">
                               <ItemTemplate><%#Eval("uom") %></ItemTemplate>
                           </asp:TemplateField>
                       </Columns>

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
</asp:Content>

