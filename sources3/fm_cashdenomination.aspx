<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashdenomination.aspx.cs" Inherits="fm_cashdenomination" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 77px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="divheader">Cash Denomination Report</div>
    <div class="divheader subheader subheader-bg">cbsalespoint</div>
    
    <div class="container-fluid">
        <div class="row">
            <div class="clearfix margin-bottom col-md-6">
                <label class="col-sm-2 control-label">Salespoint</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                     
                </div>
            </div>
        </div>
        <div class="row col-md-6 navi text-left col-md-offset-1 margin-bottom col-xs-offset-2">
             <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
    </div>
    
</asp:Content>

