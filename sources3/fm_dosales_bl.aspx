<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_dosales_bl.aspx.cs" Inherits="fm_dosales_bl" %>

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
        function fn_getmanualinv() {
            var i = window.prompt('ENTER MANUAL INVOICE NO.');
            $get('<%=btprintinvoice2.ClientID%>').click();
        }

    </script>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">Bon De Livraison</div>
    <div class="container">
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnDataBound="grd_DataBound" OnRowDataBound="grd_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="TO No">
                            <ItemTemplate>
                                <asp:HiddenField ID="hddono" Value='<%#Eval("do_no") %>' runat="server" />
                                <asp:Label ID="lbsocd" runat="server" Text='<%#Eval("so_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%#Eval("so_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate><%#Eval("cust") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate><%#Eval("remark") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delivered">
                            <ItemTemplate>
                                <asp:LinkButton  OnClientClick="ShowProgress();" ID="bthist" CssClass="btn btn-info btn-sm" OnClick="bthist_Click" runat="server">Delivered</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
                    </Columns>
                    <SelectedRowStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
        </div>
        <div class="alert alert-danger">Detail Delivery</div>
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">DO No.</label>
            <div class="col-sm-2">
                <asp:Label ID="lbdonoselected" CssClass="form-control input-sm" runat="server"></asp:Label>
            </div>
            <label class="control-label input-sm col-sm-1">Order No.</label>
            <div class="col-sm-2">
                <asp:Label ID="lbsocd" CssClass="form-control input-sm" runat="server"></asp:Label>
            </div>
            <label class="control-label input-sm col-sm-1">BL No.</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="txbl" CssClass="input-group-sm form-control input-sm" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <label class="control-label input-sm col-sm-1">Driver</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbdriver" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grddtl" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDataBound="grddtl_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Order">
                            <ItemTemplate>
                                <asp:Label ID="lbqty" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Delivered">
                            <ItemTemplate>
                                <asp:Label ID="lbdelivered" Font-Bold="true" BackColor="Yellow" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Delivery">
                            <ItemTemplate>
                                <asp:TextBox ID="txdelivery" TextMode="Number" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expire Date">
                            <ItemTemplate>
                                <div class="drop-down-date">
                                    <asp:TextBox ID="dtexpire" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                    <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="dtexpire"></asp:CalendarExtender>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate>
                                <asp:Label ID="lbuom" runat="server" Text='<%#Eval("uom") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btsave" OnClientClick="ShowProgress();" CssClass="btn btn-info btn-sm" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                <asp:LinkButton ID="btprint" OnClientClick="ShowProgress();" CssClass="btn btn-warning btn-sm" runat="server" OnClick="btprint_Click">Print BL</asp:LinkButton>
                <asp:LinkButton ID="btprintinvoice" OnClientClick="ShowProgress();fn_getmanualinv();return false;" CssClass="btn btn-primary btn-sm" runat="server">Print Invoice</asp:LinkButton>
                <asp:Button ID="btprintinvoice2" style="display:none" runat="server" OnClick="btprintinvoice2_Click" Text="Button" />
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

