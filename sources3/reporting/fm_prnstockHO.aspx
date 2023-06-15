<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_prnstockHO.aspx.cs" Inherits="fm_prnstockHO" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
<link href="css/anekabutton.css" rel="stylesheet" />
<script src="css/jquery-1.9.1.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
    <div class="form-horizontal" style="font-size:small;font-family:Calibri">
        <h4 class="jajarangenjang"> Branch Stock Monitoring	Report</h4>														 
    </div>
      <div class="form-group">
</div>
<div class="container">
<div class="form-group">
    <label class="control-label col-md-1">Date :</label>
  <div class="col-md-2">
    <div class="input-group">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                </asp:UpdatePanel>
                <asp:TextBox ID="dtfr" runat="server" CssClass="form-control" Height="100%" Width="100%" OnTextChanged="dtfr_TextChanged" AutoPostBack="True"></asp:TextBox>
                <asp:CalendarExtender ID="dtfr_CalendarExtender" runat="server" TargetControlID="dtfr" DaysModeTitleFormat="d/MMM/yyyy" Format="d/M/yyyy" TodaysDateFormat="d/MM/yyyy">
            </asp:CalendarExtender>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </div>
    <div class="col-xs-1"> 
    <asp:LinkButton ID="btPrint" 
                runat="server" 
                CssClass="btn btn-info btn-sm"    
                OnClick="btPrint_Click"> <span aria-hidden="true" class="glyphicon glyphicon-print"></span>Print </asp:LinkButton>        
    </div>
  </div>
</div>
</div>
    <div class="row">
    <div class="form-group">
        <div class="h-divider"></div>
        <h4>
        <label class="control-label col-md-4">CHECKING DIFFERENCE DATA</label>
        </h4>
    </div>
    </div>
<div class="container">
    <div class="row">
        <div class="form-group">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" EmptyDataText="No Data Found" ShowHeaderWhenEmpty="true" Width="100%">
                        <Columns>
                            <asp:TemplateField HeaderText="SP CD.">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                            <asp:TemplateField HeaderText="Salespoint Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbsp_nm" runat="server" Text='<%# Eval("sp_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Data In HO">
                                <ItemTemplate>
                                    <asp:Label ID="lbqtyho" runat="server" Text='<%# Eval("qtyho") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Data In Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lbqty_branch" runat="server" Text='<%# Eval("qty_branch") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Difference">
                                <ItemTemplate>
                                    <asp:Label ID="lbdiff" runat="server" Text='<%# Eval("diff") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                 </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="dtfr" EventName="TextChanged"/>
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
</div>
</asp:Content>

