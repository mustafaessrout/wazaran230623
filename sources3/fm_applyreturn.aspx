<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_applyreturn.aspx.cs" Inherits="fm_applyreturn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function tableScroll() {
            tabFix();
            $(".table-auto-scrooll ").scrollTop('.table-edit');
            var container = $(".table-auto-scrooll");
            var scrollTo = $('.table-edit');

            container.animate({ scrollTop: scrollTo.offset().top - container.offset().top + container.scrollTop() - 30, scrollLeft: 0 }, 10);
        }
    </script>
    <style>
        .divgrid a:visited {
            color: blue;
        }
    </style>

    <script src="v4-alpha/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
        <div class="divheader">Apply Sales Return</div>
        <div class="h-divider no-margin-bottom"></div>

        <div class="divheader subheader subheader-bg" style="margin-bottom: 10px !important;">List Of Return Already Approved</div>
        
        <div class="container">
             <asp:UpdatePanel runat="server">
                <ContentTemplate>
                    <div class="margin-bottom row clearfix">
                        <div class="overflow-y table-auto-scrooll " style="width:100%; max-height:300px;">
                            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" Width="100%" OnSelectedIndexChanging="grd_SelectedIndexChanging" CssClass="table table-hover table-striped mygrid table-fix">
                                <AlternatingRowStyle  />
                                <Columns>
                                    <asp:TemplateField HeaderText="Payment No.">
                                        <ItemTemplate>
                                            <%--<a href="fm_applyreturn.aspx?ids=<%# Eval("payment_no") %>"><asp:Label ID="lbpaymentno" runat="server" Text='<%# Eval("payment_no") %>'></asp:Label></a>--%>
                                            <asp:Label ID="lbpaymentno" runat="server" Text='<%# Eval("payment_no") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Man No.">
                                        <ItemTemplate>
                                            <%# Eval("retur_no") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sys No.">
                                        <ItemTemplate>
                                            <%# Eval("man_retur_no") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpaymentdt" runat="server" Text='<%# Eval("payment_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbpaymentcst" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Cust Name">
                                        <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Sales Code">
                                        <ItemTemplate><%# Eval("salesman_cd") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Salesman Name">
                                        <ItemTemplate><%# Eval("salesman_nm") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Total Amt">
                                        <ItemTemplate>
                                            <%# Eval("totamt") %>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" />
                                </Columns>
                                <EditRowStyle CssClass="table-edit" />
                                <FooterStyle CssClass="table-footer" />
                                <HeaderStyle CssClass="table-header" />
                                <PagerStyle CssClass="table-page" />
                                <RowStyle />
                                <SelectedRowStyle  CssClass="table-edit" />
                                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                            </asp:GridView>             
                        </div>
                    </div>
                    <div class="clearfix">
                        <div class="col-md-6">
                            <label class="control-label">Total available amount to paid : <asp:Label ID="lbavailpaid" CssClass="" runat="server" ForeColor="#0033CC" Text="0"></asp:Label></label>
                            <br />
                            <label class="form-control-label">Suspense : </label>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lbsuspense" runat="server" Text=""></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                        <div class="col-md-6">
                            <label class="col-md-3 control-label titik-dua" >Reduce Return Amount </label>
                            <div class="col-md-9 input-group" style="padding-left:15px">
                                <asp:TextBox ID="txdisc" CssClass="form-control" runat="server"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:Button ID="btpaid" runat="server" Text="PAID" style="float:none" OnClick="btpaid_Click" CssClass=" btn btn-primary" />
                                </div>
                            </div>
                    
                        </div>
                    </div>
                    <div class="clearfix row">
                         <asp:GridView ID="grdinv" CssClass="table tabel-hover table-striped mygrid " runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="0" GridLines="None" OnRowDataBound="grdinv_RowDataBound">
                            <AlternatingRowStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Sys NO.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbinvno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Invoice No">
                                    <ItemTemplate><%# Eval("manual_no") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Inv Date">
                                    <ItemTemplate><%#Eval("inv_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Amt">
                                    <ItemTemplate><%# Eval("totamt") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Temp Balance">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtempbalance" runat="server" Text='<%#Eval("tempbalance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remain">
                                    <ItemTemplate>
                                        <asp:Label ID="lbbalance" runat="server" Text='<%# Eval("balance") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Paid">
                                    <ItemTemplate>
                                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                            <ContentTemplate>
                                                <asp:TextBox ID="txpaid" runat="server" CssClass="form-control input-sm" Text="0"></asp:TextBox>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Discount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txdiscount" runat="server" Text="0" CssClass="form-control input-sm"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle />
                            <SelectedRowStyle  CssClass="table-edit" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                    <div class="navi row margin-top margin-bottom text-center">
                        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-warning btn-save" OnClick="btsave_Click" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>    


       
        
        


</asp:Content>

