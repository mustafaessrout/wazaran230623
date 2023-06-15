<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_returnfullinvoice.aspx.cs" Inherits="fm_returnfullinvoice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <script src="admin/js/bootstrap.min.js"></script>
    <script>
        function popupwindow(url) {
            mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=900,height=600");
            mywindow.moveTo(400, 200);
        }
        function InvSelected(invno) {
            $get('<%=hdinvoice.ClientID%>').value = invno;
             $get('<%=btlookup.ClientID%>').click();
             $get('<%=txinvoiceno.ClientID%>').value = invno;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdinvoice" runat="server" />

  <%--  <div class="divheader">Return Full Invoice</div>
    <div class="h-divider"></div>--%>
    <div class="alert text-bold alert-info">Return Full Invoice</div>

    <div class="container">
        <div class="row clearfix margin-bottom margin-top">
            <div class="col-sm-6 clearfix no-padding margin-bottom">
                <label class="control-label-sm col-sm-4 titik-dua">Invoice</label>
                <div class="col-md-8 col-md-10">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="input-group">
                        <ContentTemplate>
                            <asp:TextBox ID="txinvoiceno" runat="server" CssClass="form-control"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:LinkButton ID="btsearch" runat="server" CssClass="btn btn-primary" OnClick="btsearch_Click"><span class="fa fa-search"></span></asp:LinkButton>
                                
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="col-sm-6 clearfix no-padding margin-bottom">
                <label class="control-label-sm col-md-2 col-sm-4 titik-dua">Status</label>
                <div class="col-sm-8 col-md-10">
                    <asp:Label ID="lbstatus" runat="server" CssClass="form-control"></asp:Label>
                </div>
            </div>
        </div>

        <div class="row clearfix margin-bottom">

            <div class="h-divider"></div>
            <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                <label class="control-label-sm col-sm-4 titik-dua">Date</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbinvdate" runat="server" CssClass="form-control"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                <label class="control-label-sm col-sm-4 titik-dua">Due Date</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbduedate" CssClass="form-control" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                <label class="control-label-sm col-sm-4 titik-dua">Order No</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lborderno" CssClass="form-control" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                <label class="control-label-sm col-sm-4 titik-dua">Man No</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbmanno" CssClass="form-control" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                <label class="control-label-sm col-sm-4 titik-dua">Cust</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbcust" CssClass="form-control ellapsis2" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                <label class="control-label-sm col-sm-4 titik-dua">Salesman</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbsalesman" CssClass="form-control ellapsis2" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                <label class="control-label-sm col-sm-4 titik-dua">Tot Amt</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbtotamt" CssClass="form-control" runat="server"></asp:Label>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                <label class="control-label-sm col-sm-4 titik-dua">Balance</label>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server" class="col-sm-8">
                    <ContentTemplate>
                        <asp:Label ID="lbbalance" runat="server" CssClass="form-control"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

        </div>
        <div class="row clearfix margin-bottom">
            <div class="h-divider"></div>
            <div class="margin-bottom clearfix">
                <label class="control-label-sm col-md-1 col-sm-2">Reason</label>
                <div class="col-md-11 col-sm-10 drop-down">
                    <asp:DropDownList ID="cbreason" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
        </div>
        <div class="clearfix">
            <div class="col-md-9 col-sm-12  form-group">
                <asp:Label ID="lbconfrimdoc" runat="server" Text="Upload File" CssClass="control-label-sm col-md-2 col-sm-2"></asp:Label>
                <div class=" col-md-8 col-sm-10">
                    <asp:FileUpload ID="fuconfirmation" runat="server" />
                </div>
            </div>
        </div>
        <div class="row">
            <div class="divheader subheader subheader-bg">Order Detail</div>
            <div class="margin-bottom">
                <div class="">
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="table table-striped mygrid" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded">
                                <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unitprice">
                                <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amt">
                                <ItemTemplate>
                                    <%# Eval("amt") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotamt" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="divheader subheader subheader-bg">Free Details</div>

            <div class="form-group">
                <div class="margin-bottom">
                    <asp:GridView ID="grddtl" runat="server" AutoGenerateColumns="False" CellPadding="2" CssClass="table table-striped mygrid" EmptyDataText="No Free Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded">
                                <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty Free">
                                <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                    </asp:GridView>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">1. Invoice can not returned if already received by customer</div>
            </div>
            <div class="form-group">
                <div class="col-md-12 navi margin-bottom margin-top">
                    <asp:Button ID="btlookup" runat="server" OnClick="btlookup_Click" CssClass="divhid" />
                    <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-success" OnClick="btnew_Click">New</asp:LinkButton>
                    <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-warning" OnClick="btsave_Click">RETURN NOW!</asp:LinkButton>
                    <asp:LinkButton ID="btsearchApproval" runat="server" CssClass="btn btn-info" OnClick="btsearchApproval_Click">Show Approval</asp:LinkButton>
                    <asp:LinkButton ID="btprint" runat="server" CssClass="btn btn-info" OnClick="btprint_Click">PRINT</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

