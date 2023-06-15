<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstsupplier.aspx.cs" Inherits="fm_mstsupplier" %>

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
    <asp:HiddenField ID="hdcity" runat="server" />
    <div class="alert alert-info text-bold">Supplier </div>
    <div class="container">
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm input-sm col-sm-1">Supplier Code</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="txsuppliercode" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();PopupCenter('lookupsupplier.aspx','Lookup',800,600);" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Supplier Name</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txsuppliername" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Address</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txaddress" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Comp Registration Name</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txvatname" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row margin-bottom">
            <label class="control-label-sm input-sm col-sm-1">City</label>
            <div class="col-sm-2">
                <asp:TextBox ID="City" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Contact Number</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txcontactnumber" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Contact Name</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txcontactname" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Tax No</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txtaxno" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-primary btn-sm" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btsave" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-info btn-sm" OnClick="btsave_Click">Save</asp:LinkButton>
                <asp:LinkButton ID="btprint" OnClientClick="ShowProgress();" runat="server" CssClass="btn btn-danger btn-sm">Print</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

