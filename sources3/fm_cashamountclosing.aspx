<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashamountclosing.aspx.cs" Inherits="fm_cashamountclosing" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_cashamountclosing.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
    <div class="alert alert-info text-bold">Closing Cashier</div>
    <div id="container">
      <%--  <h4 class="jajarangenjang">Closing Cashier</h4>
        <div class="h-divider"></div>--%>


        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label class="control-label col-sm-2">Seq#</label>
                    <div class="col-sm-10">
                        <div class="input-group">
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbchclosingno" CssClass="form-control input-group-sm ro" runat="server" Text=""></asp:Label>
                            </ContentTemplate>
                        <%--<Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>--%>
                        </asp:UpdatePanel>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClientClick="openwindow();return(false);"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 ">
                <div class="form-group">
                <label class="control-label col-sm-2">Date</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:TextBox ID="lbchclosing_dt" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                            <asp:CalendarExtender ID="lbchclosing_dt_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="lbchclosing_dt">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>                         
                </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label class="control-label col-sm-2">Total Amount</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtotalAmount" runat="server" CssClass="form-control input-sm" ReadOnly="true"></asp:TextBox>
                                <%--<asp:Label ID="lbtotalamount" runat="server" Font-Bold="True"></asp:Label>--%>
                            </ContentTemplate>
                        </asp:UpdatePanel> 
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label class="control-label col-sm-2">Note</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txchclosing_description" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div id="listmoney" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">    
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="true">
                        <ContentTemplate>
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="0">
                            <Columns>
                                <asp:TemplateField HeaderText="Currency">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcur_nm" runat="server" Text='<%#Eval("cur_nm") %>'></asp:Label>                                        
                                        <asp:HiddenField ID="hdcur_cd" runat="server" Value='<%# Eval("cur_cd") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Nominal">
                                    <ItemTemplate>
                                        <asp:HiddenField runat="server" ID="hddn_cd" Value='<%# Eval("dn_cd") %>' />
                                        <asp:HiddenField ID="hddn_amt" runat="server" Value='<%# Eval("dn_amount") %>' />
                                        <asp:Label ID="lbdn_desc" runat="server" Text='<%#Eval("dn_desc") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txamount" Text='<%#Eval("amount") %>' runat="server" AutoPostBack="true" OnTextChanged="txamount_TextChanged" Width="100%"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtotal" runat="server" Text='<%#Eval("subtotal") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotsubtotal" runat="server"></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="Yellow" />
                        </asp:GridView>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row margin-bottom">
            <label class="control-label col-md-1">File</label>
            <div class="col-md-2">
                <asp:FileUpload ID="fucashier" CssClass="form-control" runat="server" />
            </div>
        </div>

        <div class="row">
                <asp:Label ID="lbalert" runat="server"></asp:Label>
        </div>


        <div class="row margin-bottom">
            <div class="col-sm-12">
                <div class="text-center navi">
                    <asp:Button id="btsave" runat="server" Text="Save" class="btn btn-success btn-sm" OnClick="btsave_Click" />
                </div>
            </div>
        </div>  

    </div>
</asp:Content>

