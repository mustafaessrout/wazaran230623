<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_itemblock.aspx.cs" Inherits="fm_itemblock" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hditem" runat="server" />
    <%--<div class="container container-fluid">--%>
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Item Blocked</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1" style="font-size:small">Salespoint</label>
                <div class="col-md-4">
                    <div class="input-group">
                         <asp:DropDownList ID="cbsp" runat="server" CssClass="form-control-static input-group-sm"></asp:DropDownList>
                         <asp:CheckBox ID="chkall" runat="server" CssClass="form-control-static input-group-sm"/> 
                         <label class="control-label input-group-sm">All Salespoint</label>
                    </div>
                   
                </div>
                 <label class="control-label col-md-1" style="font-size:small">Block For</label>
                <asp:RadioButtonList ID="rdcust" runat="server" CellPadding="1" CellSpacing="1" RepeatDirection="Horizontal" CssClass="form-control-static input-sm" AutoPostBack="True" OnSelectedIndexChanged="rdcust_SelectedIndexChanged">
                    <asp:ListItem Value="G">Cust Group</asp:ListItem>
                    <asp:ListItem Value="T">Cust Channel</asp:ListItem>
                    <asp:ListItem Value="C">Direct Cust</asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="form-group">
                
                 <div class="col-md-3">
                     <asp:TextBox ID="txitemsearch" runat="server" CssClass="form-control input-sm" Width="100%" Height="100%"></asp:TextBox>
                     <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txitemsearch" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected">
                     </asp:AutoCompleteExtender>
                 </div>
                <label class="control-label col-md-1">Cust</label>
                <div class="col-md-3">
                    <asp:DropDownList ID="cbcusgrcd" runat="server" CssClass="form-control-static input-sm"></asp:DropDownList>
                    <asp:TextBox ID="txcust" runat="server"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txcust" UseContextKey="True">
                    </asp:AutoCompleteExtender>
                </div>
                <label class="control-label col-md-1">Start Date</label>
                <div class="col-md-1">
                    <asp:TextBox ID="dtstart" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                    </asp:CalendarExtender>
                </div>
                <label class="control-label col-md-1">End Date</label>
                <div class="col-md-1">
                    <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                    </asp:CalendarExtender>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btadd" runat="server" CssClass="btn btn-primary" OnClick="btadd_Click">ADD</asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="2" Width="100%" OnRowDeleting="grd_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdids" Value='<%# Eval("IDS") %>' runat="server" />
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded"><ItemTemplate><%# Eval("branded_nm") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Block For"><ItemTemplate><%# Eval("cusgrcd") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Start"><ItemTemplate><%# Eval("start_dt","{0:d/M/yyyy}") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="End"><ItemTemplate><%# Eval("end_dt","{0:d/M/yyyy}") %></ItemTemplate></asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    <%--</div>--%>
</asp:Content>

