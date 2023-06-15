<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Fm_rptclosingstockjaretmonthly.aspx.cs" Inherits="Fm_rptclosingstockjaretmonthly" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="js/jquery-1.3.1.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/sweetalert.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="margin-top padding-top container-fluid">
        <div class="row">
            <div class="col-sm-offset-2 col-sm-8 form-group clearfix">
                <label class="col-sm-2 control-label">Period</label>
                <div class="col-sm-6 drop-down rs-sm-margin-bottom">
                    <asp:DropDownList ID="cbMonthCD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbMonthCD_SelectedIndexChanged" CssClass="form-control">
                    </asp:DropDownList>
                     
                </div>
                <div class="col-md-4 col-sm-4">
                    <asp:TextBox ID="txto" runat="server" CssClass="makeitreadonly ro form-control" Enabled="False"></asp:TextBox>
                </div>
            </div>

            <div class="col-sm-offset-2 col-sm-8 form-group clearfix">
                <label class=" col-sm-2 control-label">Whs Type</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbwhs_type" runat="server" AutoPostBack="True" CssClass="form-control">
                    </asp:DropDownList>
                </div>
            </div>

        </div>
                 
         <div class="navi row padding-bottom">
            <asp:Button ID="btsave" runat="server" CssClass="btn-info btn btn-print" OnClick="btsave_Click" Text="Print" />
            <asp:Button ID="btclose" runat="server" CssClass="btn-danger btn btn-close" OnClick="btclose_Click" Text="Close" />      
        </div>
    </div>
  
  
   
</asp:Content>

