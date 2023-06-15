<%@ Page Title="" Language="C#" MasterPageFile="~/eis/eis.master" AutoEventWireup="true" CodeFile="fm_sales.aspx.cs" Inherits="eis_fm_sales" %>

<%@ Register Assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" Namespace="System.Web.UI.DataVisualization.Charting" TagPrefix="asp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>
 <%--   <script>
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

    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <div class="form-horizontal" style="font-size:small;font-family:Calibri">
        <h4 class="jajarangenjang">Sales Achievement</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Salespoint</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbsalespoint" CssClass="form-control" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Start Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtstart" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                </asp:CalendarExtender>
            </div>
              <label class="control-label col-md-1">End Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtend" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
            <div class="col-md-1">
                <asp:LinkButton ID="btview" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btview_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
            </div><div class="col-md-1">
                <asp:LinkButton ID="btgraph" CssClass="btn btn-primary" runat="server" OnClick="btgraph_Click"><span class="glyphicon glyphicon-import"></span></asp:LinkButton>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
            <asp:GridView ID="grd" CssClass="mydatagrid" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Item Code">
                        <ItemTemplate><%#Eval("item_cd") %></ItemTemplate>
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
                    <asp:TemplateField HeaderText="Qty Sold">
                        <ItemTemplate>
                            <%#Eval("qty","{0:#,0.00}") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate>CTN</ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Amount Sold">
                        <ItemTemplate><%#Eval("amt","{0:#,0.00}") %></ItemTemplate>
                    </asp:TemplateField>
                </Columns>
<HeaderStyle CssClass="header"></HeaderStyle>

<PagerStyle CssClass="pager"></PagerStyle>

<RowStyle CssClass="rows"></RowStyle>
            </asp:GridView>
            </div>
        </div>
        
        <div class="form-group">
            <div class="col-md-12" style="text-align:center">
                <asp:LinkButton ID="btprint" CssClass="btn btn-primary" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
            </div>
        </div>
    </div>
  <%--  <p id="pnlmsg" style="position:absolute; top: 50%;left: 50%;margin-top: -50px; margin-left: -50px;width: 120px;height: 120px;opacity:1 !important;z-index:100">
                <img src="/image/loading2.gif" />
     </p>--%>

 <%--   <div class="divmsg loading-cont" id="pnlmsg" >
    <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>--%>
</asp:Content>

