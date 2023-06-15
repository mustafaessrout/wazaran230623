<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_upload_transaction_bkp.aspx.cs" Inherits="fm_upload_transaction_bkp" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
    <script>    
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdsalespoint" runat="server" />

    <div id="container">
        <h4 class="jajarangenjang">Branch Transaction - Upload </h4>
        <div class="h-divider"></div>

        <div id="paramtrans" runat="server">
            <div class="row">
                <div class="col-sm-6">
                    <div class="clearfix form-group" runat="server" id="listSalespoint">
                        <label class="control-label col-sm-2">Salespoint</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbSalesPointCD" runat="server" CssClass="form-control  " AutoPostBack="true" OnSelectedIndexChanged="cbSalesPointCD_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>                         
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="clearfix form-group" runat="server" id="typeTrans">
                        <label class="control-label col-sm-2">Transaction Type</label>
                        <div class="col-sm-10">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="drop-down">
                                <ContentTemplate>
                                    <asp:DropDownList ID="cbtypetrans" runat="server" CssClass="form-control  " AutoPostBack="true" OnSelectedIndexChanged="cbtypetrans_SelectedIndexChanged">
                                        <asp:ListItem Value="t1"> Take Order with Payment</asp:ListItem>
                                    </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>                         
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-6">
                    <label class="control-label col-sm-2">Branch Date</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtbranch" runat="server" CssClass="form-control" ReadOnly="true" ></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="col-sm-6">
                    <label class="control-label col-sm-2">File Upload</label>
                    <div class="col-sm-10">
                        <div class="input-group">
                            <%--<asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>--%>
                            <asp:FileUpload ID="fut" CssClass="form-control" runat="server" />
                            <div class="input-group-btn">
                                <asp:LinkButton ID="btn_upload" CssClass="btn btn-success" OnClientClick="ShowProgress();" runat="server" OnClick="btn_upload_Click">Upload</asp:LinkButton>
                                <%--<asp:button id="btn_upload" runat="server" cssclass="btn btn-primary btn-upload" Text="Upload" onclick="btn_upload_Click" />--%>
                            </div>
                            <%--</ContentTemplate>
                            </asp:UpdatePanel>   --%> 
                        </div>
                    </div>
                </div>
            </div>
        </div>   

        <div class="h-divider"></div>
        <div class="clearfix" id="listtrans" runat="server">
            <div class="overflow-x overflow-y">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                <asp:GridView ID="grdto" runat="server" CellPadding="0" GridLines="None" CssClass="table table-striped table-page-fix table-fix mygrid" data-table-page="#copy-fst"  AutoGenerateColumns="False" ShowFooter="false" AllowPaging="True">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Branch">
                            <ItemTemplate>
                                <asp:Label ID="lbsp" runat="server" Text='<%# Eval("branch") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate>
                                <asp:Label ID="lbsalesman" runat="server" Text='<%# Eval("Salesman") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <asp:Label ID="lbcustomer" runat="server" Text='<%# Eval("Customer") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lbso_dt" runat="server" Text='<%# Eval("trx_dt","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ST 1%">
                            <ItemTemplate>
                                <asp:Label ID="lbst" runat="server" Text='<%# Eval("discount_vat") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Order Type">
                            <ItemTemplate>
                                <asp:Label ID="lbordertyp" runat="server" Text='<%# Eval("order_type") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item">
                            <ItemTemplate>
                                <asp:Label ID="lbitem" runat="server" Text='<%# Eval("lbitem") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:Label ID="lbitem" runat="server" Text='<%# Eval("lbitem") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                    <HeaderStyle CssClass="table-header" />
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>

                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btn_upload" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="cbSalesPointCD" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbtypetrans" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

    </div>

</asp:Content>

