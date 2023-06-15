<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookupcontract.aspx.cs" Inherits="fm_lookupcontract" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />


    <script src="js/jquery.min.js"></script>
     <script src="js/bootstrap.min.js"></script> 
    <script src="js/index.js"></script>
    <script src="js/jquery.floatThead.js"></script>
   
</head>
<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="divheader">Lookup Contract </div>
            <div class="h-divider"></div>
            <div class="clearfix margin-bottom">
                <div class="col-sm-6 no-padding">
                    <label class="control-label col-sm-4">Proposal No. / Contract No. </label> 
                    <div class=" col-sm-8 input-group">
                        <asp:TextBox ID="txsearchprop" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                        </div>
                    </div>
                </div>
            </div>
            
            <div class="divgrid margin-bottom">
                <div class="overflow-y" style="max-height:380px;">
                    <asp:GridView ID="grdcontract" CssClass="table table-striped mygrid table-fix mygrid table-page-fix" data-table-page="#copytable"  OnPageIndexChanging="grdcontract_PageIndexChanging" runat="server" AutoGenerateColumns="False" CellPadding="0"  GridLines="None" EmptyDataText="NO DATA FOUND" ShowHeaderWhenEmpty="True" AllowPaging="True" PageSize="33" >
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Contract No">
                                <ItemTemplate><a href="javascript:window.opener.ContractSelected('<%# Eval("contract_no") %>');window.close();"> <%# Eval("contract_no") %></a></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Proposal No"><ItemTemplate><%# Eval("prop_no") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Type"><ItemTemplate><%# Eval("type") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date"><ItemTemplate><%# Eval("start_dt") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date"><ItemTemplate><%# Eval("end_dt") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Status"><ItemTemplate><%# Eval("customer") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Status"><ItemTemplate><%# Eval("fld_desc") %></ItemTemplate></asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit"/>
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
                <div class="table-copy-page-fix" id="copytable"></div>

            </div>
        
        </div>
    </form>
</body>
</html>
