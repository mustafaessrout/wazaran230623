<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rp_return.aspx.cs" Inherits="fm_rp_return" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <h3>RETURN        
    </h3>
    <div class="form-group h-divider"></div>
    <div class="container">
        <div class="form-horizontal">
            <div class="form-group">
                <label class="control-label col-md-2">Type:</label>
                <div class="col-md-2">
                    <asp:RadioButtonList ID="rdreporttype" runat="server"  RepeatDirection="Horizontal">
                        <asp:ListItem Value="0">Item</asp:ListItem>
                        <asp:ListItem Value="1">Product</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="form-group">
                <label class="control-label col-md-2">PERIOD:</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbperiod" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                </div>
            </div>

            <div class="form-group">

                <label class="control-label col-md-2">Branch:</label>
                <div class="col-md-4">
                    <asp:DropDownList ID="cbbranch" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                </div>
            </div>          
            <div class="navi">
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-info btn-lg" OnClick="btprint_Click" />
            </div>
        </div>
    </div>
</asp:Content>

