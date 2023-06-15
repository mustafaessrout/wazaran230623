<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_itemmerchan.aspx.cs" Inherits="fm_itemmerchan" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <h4 class="jajarangenjang">Master Item Merchandiser Configuration</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="row margin-bottom">
                <label class="control-label col-md-1">Item Code</label>
                <div class="col-md-3">
                    <asp:TextBox ID="txitemcode" CssClass="form-control" runat="server"></asp:TextBox>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btsearch" OnClientClick="javascript:ShowProgress();" runat="server" CssClass="btn btn-primary" OnClick="btsearch_Click"><span class="fa fa-search"></span></asp:LinkButton>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-md-12">
                    <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" OnRowDataBound="grd_RowDataBound" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
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
                            <asp:TemplateField HeaderText="Used By Merchandiser Device">
                                <ItemTemplate>
                                    <asp:CheckBox ID="chmerchan" onChange="javascript:ShowProgress();" runat="server" AutoPostBack="true" OnCheckedChanged="chkmerchan_CheckedChanged" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="row margin-bottom">
                <div class="col-md-12 center">
                    <asp:LinkButton ID="btprint" CssClass="btn btn-info" runat="server" OnClick="btprint_Click"><span class="fa fa-print"></span></asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

