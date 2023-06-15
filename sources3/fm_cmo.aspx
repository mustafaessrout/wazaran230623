<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cmo.aspx.cs" Inherits="fm_cmo" %>

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

        function SelectCMO(sVal) {
            ShowProgress();
            <%--$get('<%=hdcmo.ClientID%>').value = sVal;
            $get('<%=btselect.ClientID%>').click();--%>
        }

        $(document).ready(function () {
            <%--$("#<%=btsearch.ClientID%>").click(function () {
                PopupCenter('fm_lookup_cmo_ho.aspx', 'xtf', '900', '500');

            });--%>
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdcmo" runat="server" />

    <div class="container">
        <h4 class="jajarangenjang">CMO / CWO (Confirm Monthly / Weekly Order)</h4>
        <div class="h-divider"></div>

        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label class="control-label col-sm-2">Batch Seq#</label>
                    <div class="col-sm-10">
                        <div class="input-group">
                        <asp:Label ID="lbcmono" CssClass="form-control input-group-sm ro" runat="server" Text=""></asp:Label>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                    </div>
                </div>
                </div>
            </div>
            <div class="col-sm-6 ">
                <div class="form-group">
                <label class="control-label col-sm-2">For Period</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" CssClass="form-control"  OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>                         
                </div>
                </div>
            </div>
        </div>
        <div class="row">
             <div class="col-sm-6 ">
                <div class="form-group">
                <label class="control-label col-sm-2">Type</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbType" runat="server" AutoPostBack="True" CssClass="form-control"  OnSelectedIndexChanged="cbType_SelectedIndexChanged">
                                <asp:ListItem Value="M">Monthly</asp:ListItem>
                                <asp:ListItem Value="W">Weekly</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>                         
                </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                <label class="control-label col-sm-2">Week</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbWeek" runat="server" CssClass="form-control" >
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>                         
                </div>
                </div>
            </div>
        </div>
        
        <h5 class="jajarangenjang">Data Details</h5>
        <div class="h-divider"></div>

        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowCancelingEdit="grd_RowCancelingEdit" ShowFooter="True" CellPadding="0">
                            <Columns>
                                <asp:TemplateField HeaderText="Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lbperiod" runat="server" Text='<%#Eval("period") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemnm" runat="server" Text='<%#Eval("item_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txQty" runat="server" Text='<%#Eval("qty")%>'></asp:TextBox>
                                        <asp:Label ID="lbuom" runat="server" Text='<%#Eval("uom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                

                                <%--<asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%#Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branded">
                                    <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lbqty" runat="server" Text='<%#Eval("qtyorder") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotqtyorder" runat="server" Text=""></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lbuom" runat="server" Text='<%#Eval("uom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lbqtyshipment" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txqty" runat="server" Text='<%#Eval("qty") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotqtyshipment" runat="server" Text=""></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="Change Qty" ShowEditButton="False" />--%>
                            </Columns>
                            <FooterStyle BackColor="Yellow" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>



    </div>

</asp:Content>

