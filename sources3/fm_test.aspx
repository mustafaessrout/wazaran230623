<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_test.aspx.cs" Inherits="fm_test" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/bootstrap.min.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="../css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">CN DN Adjustment</h4>
        <div class="h-divider"></div>
        <div class="container">
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-md-1">CN/DN No</label>
                    <div class="col-md-2">
                        <div class="input-group">
                            <asp:Label ID="lbltemp" runat="server"></asp:Label>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>

