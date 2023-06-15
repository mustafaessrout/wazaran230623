<%@ Page Title="" Language="C#" MasterPageFile="~/promotor/promotor2.master" AutoEventWireup="true" CodeFile="fm_closing.aspx.cs" Inherits="promotor_fm_closing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Closing The Day</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="col-md-1">Today Date</div>
                <div class="col-md-6">
                    <asp:Label ID="lbexhibitdate" runat="server" Text="" CssClass="form-control"></asp:Label>
                </div>
            </div>
             <div class="form-group">
                <div class="col-md-12" style="text-align:center">
                    <asp:LinkButton ID="btclose" CssClass="btn btn-primary" runat="server" OnClick="btclose_Click">CLOSING NOW !</asp:LinkButton></div>
                
            </div>
        </div>
    </div>
</asp:Content>

