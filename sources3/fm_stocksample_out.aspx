<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stocksample_out.aspx.cs" Inherits="fm_stocksample_out" %>

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
    <div class="row margin-bottom margin-top">
        <label class="control-label input-sm col-sm-1">Salespoint</label>
        <div class="col-sm-2 drop-down">
            <asp:DropDownList ID="cbsalespoint" AutoPostBack="true" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
        </div>
    </div>
    <div class="alert alert-warning">Sample need to be processed</div>
    <div class="row margin-bottom margin-top">
        <div class="col-sm-12">
            <asp:GridView ID="grd" CssClass="table table-striped mGrid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                <Columns>
                    <asp:TemplateField HeaderText="Sample Code">
                        <ItemTemplate>
                            <asp:Label ID="lbsamplecode" runat="server" Text='<%#Eval("sample_cd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Date">
                        <ItemTemplate><%#Eval("sample_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Remark">
                        <ItemTemplate><%#Eval("remark") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" />
                </Columns>
                <SelectedRowStyle BackColor="Yellow" />
            </asp:GridView>
        </div>
    </div>
    <div class="alert alert-warning">Detail</div>
    <div class="row margin-bottom margin-top">
        <div class="col-sm-12">
            <asp:GridView ID="grddetail" CssClass="mGrid" runat="server" AutoGenerateColumns="False">
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
                    <asp:TemplateField HeaderText="Branded Name">
                        <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Qty">
                        <ItemTemplate><%#Eval("qty") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="UOM">
                        <ItemTemplate><%#Eval("uom") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                </Columns>
            </asp:GridView>
        </div>
        <div class="row margin-bottom margin-top">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btreceived" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" runat="server">Received</asp:LinkButton>
                <asp:LinkButton ID="btprint" CssClass="btn btn-warning btn-sm" OnClientClick="ShowProgress();" runat="server">Received</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

