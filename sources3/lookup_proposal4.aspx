﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_proposal4.aspx.cs" Inherits="lookup_proposal4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
      <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>


    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/index.js"></script>
    <script src="js/jquery.floatThead.js"></script>
</head>
<body>
    <form id="form1" runat="server">
         <div class="containers bg-white">
            <div class="divheader">Proposal List</div>
            <div class="h-divider"></div>



             <div class="clearfix">
                <div class="col-sm-6">
                    <label class="control-label">Proposal No.</label>
                    <div class="input-group">
                        <asp:TextBox ID="txsearchprop" runat="server" CssClass="form-control"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                        </div>
                    </div>
                </div>
            </div>

            <div class="divgrid margin-top margin-bottom">
                <div class="overflow-y" style="height:450px;">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0"   GridLines="None"  >
                        <AlternatingRowStyle  />
                        <Columns>
                            <asp:TemplateField HeaderText="Proposal No.">
                                <ItemTemplate>
                                    <a href="javascript:window.opener.RefreshData1('<%# Eval("prop_no") %>');window.close();">
                                        <asp:Label ID="lbtono" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label></a>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contract No">
                                <ItemTemplate><%# Eval("contract_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate><%# Eval("start_dt","{0:dd/MM/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate><%# Eval("end_dt","{0:dd/MM/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate><%# Eval("customer") %></ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page"/>
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
            </div>

        </div>

        <div class="divgrid">
            
        </div>
    </form>
</body>
</html>
