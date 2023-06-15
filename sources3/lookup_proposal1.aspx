<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_proposal1.aspx.cs" Inherits="lookup_proposal1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
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
                    <asp:GridView ID="grd" runat="server" CssClass="table table-striped mygrid table-fix" AutoGenerateColumns="False" CellPadding="0" GridLines="None" Width="100%" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" PageSize="30" OnRowDataBound="grd_RowDataBound">
                        <AlternatingRowStyle/>
                        <Columns>
                            <asp:TemplateField HeaderText="Proposal No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbtono" runat="server" Text='<%# Eval("proposal") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Discount Code">
                                <ItemTemplate><%# Eval("disc_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date">
                                <ItemTemplate><%# Eval("start_dt","{0:dd/MM/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date">
                                <ItemTemplate><%# Eval("end_dt","{0:dd/MM/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbdue" runat="server" Text='<%# Eval("due_dt")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Program">
                                <ItemTemplate>
                                    <div class="ellapsis" title="<%# Eval("remark") %>" style="width:150px;"><%# Eval("remark") %></div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <a href="javascript:window.opener.RefreshData('<%# Eval("proposal") %>','<%# Eval("benefitpromotion") %>','<%# Eval("disc_cd") %>');window.close();">Select</a>
                                </ItemTemplate>
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
    </form>
</body>
</html>
