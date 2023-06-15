<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_repacking.aspx.cs" Inherits="fm_repacking" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
    <div class="alert alert-info text-bold">Repacking</div>
    <div class="container">
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">Repack No</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="txrepackno" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <label class="control-label input-sm col-sm-1">Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtrepack" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtrepack_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtrepack">
                </asp:CalendarExtender>
            </div>
            <label class="control-label input-sm col-sm-1">Remark</label>
            <div class="col-sm-5 require">
                <asp:TextBox ID="txremark" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">Qty Produced(CTN)</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txqtyout" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label input-sm col-sm-1">BOM</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbbom" AutoPostBack="true" CssClass="form-control input-sm" onchange="ShowProgress();" runat="server" OnSelectedIndexChanged="cbbom_SelectedIndexChanged"></asp:DropDownList>
            </div>

            <div class="col-sm-6">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" ShowFooter="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty In CTN">
                            <ItemTemplate>
                                <asp:Label ID="lbqtyctn" Font-Bold="true" Font-Size="Medium" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotctn" runat="server" Text=""></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Pcs">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdqty" runat="server" Value='<%#Eval("qty") %>' />
                                <asp:Label ID="lbqty" Font-Bold="true" Font-Size="Medium" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotpcs" runat="server" Text=""></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate>
                                <asp:Label ID="lbuom" runat="server" Text='<%#Eval("uom") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <FooterStyle BackColor="Yellow" Font-Bold="True" />
                </asp:GridView>
            </div>
            <div class="h-divider"></div>
            <div class="row margin-bottom">
                <div class="col-sm-12 text-center alert-warning">
                    <asp:LinkButton ID="btnew" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                    <asp:LinkButton ID="btsave" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                    <asp:LinkButton ID="btprint" CssClass="btn btn-danger btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                </div>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

