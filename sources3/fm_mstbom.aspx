<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstbom.aspx.cs" Inherits="fm_mstbom" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
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
    <div class="alert alert-info text-bold">Build Of Material</div>
    <div class="container">
        <div class="row margin-bottom">
            <asp:Label ID="lbbomcode" CssClass="control-label input-sm col-sm-1" runat="server" Text="BOM Code"></asp:Label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="txbomcode" CssClass="form-control input-sm input-group-sm" placeholder="Code BOM" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
             <asp:Label ID="Label1" CssClass="control-label input-sm col-sm-1" runat="server" Text="Pack For"></asp:Label>
            <div class="col-sm-8 require">
                <asp:TextBox ID="txremark" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>

        </div>
        <div class="row margin-bottom">
            <asp:Label ID="lb" CssClass="control-label input-sm col-sm-1" runat="server" Text="Item Packaged"></asp:Label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbitemto" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
             <asp:Label ID="Label3" CssClass="control-label input-sm col-sm-1" runat="server" Text="Qty produced"></asp:Label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txqtyout" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <asp:Label ID="Label2" CssClass="control-label input-sm col-sm-1" runat="server" Text="Uom"></asp:Label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbuomto" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <table class="mGrid">
                    <tr>
                        <th>Item Code</th>
                        <th>Qty</th>
                        <th>UOM</th>
                        <th></th>
                    </tr>
                    <tr>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbitemfrom" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="txqty" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                        </td>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbuomfrom" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:LinkButton ID="btnadd" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btnadd_Click">Add</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="table table-sm table-bordered" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate><%#Eval("qty","{0:N2}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate><%#Eval("uom") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                 <asp:LinkButton ID="btsave" CssClass="btn btn-danger btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                 <asp:LinkButton ID="btprint" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

