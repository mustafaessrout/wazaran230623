<%@ Page Title="" Language="C#" MasterPageFile="~/eis/eis.master" AutoEventWireup="true" CodeFile="fm_contribute.aspx.cs" Inherits="eis_fm_contribute" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="form-horizontal" style="font-size:small;font-family:Calibri">
        <h4 class="jajarangenjang">Contribution</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Salespoint</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Start Date</label>
            <div class="col-md-3">
                <asp:TextBox ID="dtstart" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" TargetControlID="dtstart" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
             <label class="control-label col-md-1">End Date</label>
            <div class="col-md-3">
                <asp:TextBox ID="dtend" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtend">
                </asp:CalendarExtender>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Product Group</label>
            <div class="col-md-3">
                  <asp:DropDownList ID="cbprodgroup" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbprodgroup_SelectedIndexChanged"></asp:DropDownList>
            </div>
             <label class="control-label col-md-1">Item Code</label>
            <div class="col-md-3">
                  <asp:DropDownList ID="cbitem" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
              <label class="control-label col-md-1">Outlet Type</label>
            <div class="col-md-3">
                  <asp:DropDownList ID="cbchannel" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Salesman</label>
            <div class="col-md-3">
                 <asp:DropDownList ID="cbsalesman" runat="server" CssClass="form-control"></asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:LinkButton ID="btview" OnClientClick="javascript:ShowProgress();" runat="server" CssClass="btn btn-primary" OnClick="btview_Click">View</asp:LinkButton>
            </div>

        </div>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">Total Customer</label>
            <div class="col-md-2">
                <asp:Label ID="lbtotcustomer" runat="server" CssClass="form-control"></asp:Label>
            </div>
            <label class="control-label col-md-1">Total Buy Cust</label>
            <div class="col-md-2">
                <asp:Label ID="lbtotcustbuy" runat="server" CssClass="form-control"></asp:Label>
            </div>
            <label class="control-label col-md-1">Total Not Buy</label>
            <div class="col-md-2">
                <asp:Label ID="lbtotnotbuy" runat="server" CssClass="form-control"></asp:Label>
            </div>
             <label class="control-label col-md-1">Total Customer</label>
            <div class="col-md-2">
                <asp:Label ID="lbcontribution" runat="server" CssClass="form-control"></asp:Label>
            </div>
        </div>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel runat="server" ID="updgrdcustomer">
                    <ContentTemplate>
                        <asp:GridView ID="grd" CssClass="mydatagrid" RowStyle-CssClass="rows" PagerStyle-CssClass="pager" HeaderStyle-CssClass="header" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0" OnPageIndexChanging="grd_PageIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Cust Code">
                                    <ItemTemplate><%#Eval("cust_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cust Name">
                                    <ItemTemplate><%#Eval("cust_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Address">
                                    <ItemTemplate><%#Eval("addr") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="City">
                                    <ItemTemplate><%#Eval("city") %></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>                
            </div>
           </div>
        <div class="form-group">
            <div class="col-md-12" style="text-align:center">
                <asp:LinkButton ID="btprint" CssClass="btn btn-primary" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
            </div>
        </div>
       
    </div>
     <p id="pnlmsg" class="loading-cont">
                <img src="/image/loading.gif" />
     </p>
</asp:Content>

