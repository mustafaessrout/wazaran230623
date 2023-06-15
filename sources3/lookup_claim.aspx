<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_claim.aspx.cs" Inherits="lookup_claim" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
     <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
     <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/animate.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>

</head>
<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="divheader">
                Proposal List
            </div>
            <div class="h-divider"></div>
            <div class="clearfix">
                <label class="control-label col-sm-2">Proposal No.</label>
                <div class="input-group col-sm-8">
                    <asp:TextBox ID="txsearchprop" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:Button ID="btsearch" runat="server" CssClass="btn btn-sm btn-primary search" Text="Search" OnClick="btsearch_Click" />
                    </div>
                </div>
            </div>
            <div class="h-devider"></div>
            <div class="divgrid">
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" PageSize="30" CssClass="table table-striped table-bordered mygrid">
                    <AlternatingRowStyle  Wrap="True" />
                    <Columns>
                        <asp:TemplateField HeaderText="Claim NO.">
                            <ItemTemplate>
                                <a href="javascript:window.opener.RefreshData('<%# Eval("claim_no") %>');window.close();">
                                  <asp:Label ID="lbclaimno" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label></a>
                            </ItemTemplate>

                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CCNR">
                            <ItemTemplate><%# Eval("ccnr_no") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proposal No.">
                            <ItemTemplate>
                             <asp:Label ID="lbtono" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Discount Code">
                            <ItemTemplate><%# Eval("disc_cd") %></ItemTemplate>
                        </asp:TemplateField>
  
                        <asp:TemplateField HeaderText="End Date">
                            <ItemTemplate><%# Eval("end_dt","{0:dd/MM/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Program">
                            <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header"/>
                    <PagerStyle CssClass="table-page"/>
                    <RowStyle/>
                    <SelectedRowStyle CssClass="table-edit" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>
