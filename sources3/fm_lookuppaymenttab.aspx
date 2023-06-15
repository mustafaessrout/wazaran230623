<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_lookuppaymenttab.aspx.cs" Inherits="fm_lookuppaymenttab" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />


    <script src="js/jquery.min.js"></script>
     <script src="js/bootstrap.min.js"></script> 
    <script src="js/index.js"></script>
    <script src="js/jquery.floatThead.js"></script>
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="divheader">TABLET Payment Receipt</div>
            <div class="h-divider"></div>
            <div class="divgrid">
                <div class="clearfix margin-bottom">
                    <div class="col-sm-6 no-padding">
                        <label class="col-sm-4 control-label titik-dua">Status</label>
                        <div class="col-sm-8 drop-down">
                            <asp:DropDownList ID="cbstatus" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" ></asp:DropDownList>
                       </div>
                    </div>
      
                    <div class="col-sm-6 no-padding">
                        <label class="col-sm-4 control-label titik-dua">Salesman</label>
                        <div class="col-sm-8 drop-down">
                            <asp:DropDownList ID="cbsalesman" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged"></asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
       
            <div>
                <div class="overflow-y margin-bottom" style="max-height:280px;">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None"  OnSelectedIndexChanging="grd_SelectedIndexChanging" CssClass="table table-striped table-fix mygrid" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDataBound="grd_RowDataBound" ShowFooter="True">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Select">
                                <HeaderTemplate>
                                    <asp:CheckBox ID="chkboxSelectAll" runat="server" AutoPostBack="true" OnCheckedChanged="chkboxSelectAll_CheckedChanged" />
                                </HeaderTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="chk" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Payment No.">
                                <ItemTemplate>
                                  <%--<a href="javascript:window.close();window.opener.clicksearch('<%# Eval("tab_no") %>');"> <asp:Label ID="lbtab_no" runat="server" Text='<%# Eval("tab_no") %>'></asp:Label></a>--%> 
                                    <asp:Label ID="lbtab_no" runat="server" Text='<%# Eval("tab_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tab No.">
                                <ItemTemplate>
                                  <%--<a href="javascript:window.close();window.opener.clicksearch('<%# Eval("tab_no") %>');"> <asp:Label ID="lbtab_no" runat="server" Text='<%# Eval("tab_no") %>'></asp:Label></a>--%> 
                                    <asp:Label ID="lbso_cd" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Man No.">
                                <ItemTemplate>
                                    <%# Eval("manual_no") %>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txmanualno" runat="server" Text='<%# Eval("manual_no") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lbtab_dt" runat="server" Text='<%# Eval("tab_dt","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcust_cd" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Name">
                                <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salesman">
                                <ItemTemplate><%# Eval("salesman_nm") %></ItemTemplate>
                                <FooterTemplate>
				                    <div style="text-align: right;">
				                    <asp:Label  Text="Total Amount:" runat="server" />
				                    </div>
			                    </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Total Paid">
                                <ItemTemplate>
                                    <div style="text-align: right;">
                                    <asp:Label ID="lbtotamt" runat="server" Text='<%# Eval("totamt") %>'></asp:Label>
                                    </div>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtotamt" runat="server" Text='<%# Eval("totamt")%>'></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
				                    <div style="text-align: right;">
				                    <asp:Label ID="lbtotamtsum" runat="server" />
				                    </div>
			                    </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Suspense Amt">
                                <ItemTemplate><%#Eval("suspense_amt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="IV Type">
                                <ItemTemplate>
                                    <asp:Label ID="lbinvtype" runat="server" Text='<%# Eval("inv_typ")%>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
				                    <div style="text-align: right;">
				                    <asp:Label  Text="Count:" runat="server" />
				                    </div>
			                    </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Pymnt Type">
                                <ItemTemplate>
								<asp:Label ID="lbpayment" runat="server" Text='<%# Eval("payment_type") %>'></asp:Label>
                                    </ItemTemplate>                               
                                <FooterTemplate>
				                    <div style="text-align: right;">
				                    <asp:Label ID="lbcounttran" runat="server" />
				                    </div>
			                    </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                    <ItemTemplate> <asp:Label ID="lbpaymenttype" runat="server" Text='<%# Eval("payment_typ") %>' Visible="false"></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" SelectText="Detail" ShowEditButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </div>
                <asp:GridView ID="grddtl" runat="server" AutoGenerateColumns="False"  CaptionAlign="Left" CellPadding="0" EmptyDataText="NO DATA FOUND" GridLines="None" CssClass="table table-striped mygrid top-devider">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Invoice Code">
                            <ItemTemplate>
                                <asp:Label ID="lbinv_no" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Name">
                            <ItemTemplate>
                                <%# Eval("cust_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amt">
                            <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc Amt">
                            <ItemTemplate><%# Eval("disc_amt") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Disc 1% ">
                            <ItemTemplate>
                                <%# Eval("disconepct") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                </asp:GridView>
                <div class="h-divider"></div>
                <div class="navi margin-bottom">
                    <asp:Button ID="btapply" runat="server" Text="Transfer To Back Office" CssClass="btn-success btn btn-add" OnClick="btapply_Click"/>
                    <asp:Button ID="btcancel" runat="server" CssClass="btn-danger btn btn-delete" OnClick="btcancel_Click" Text="Cancel  Payment" />
                    <asp:Button ID="btupdate" runat="server" CssClass="btn-warning btn btn-edit" OnClick="btupdate_Click" Text="Change to next day" />
                </div>
            </div>
        </div>
    </form>
</body>
</html>
