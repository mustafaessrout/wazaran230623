<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstvehicles.aspx.cs" Inherits="fm_mstvehicles" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Vehicle Entry</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">
                <div class="no-padding margin-bottom col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Vehicle Code</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txvhccode" runat="server" CssClass="makeitreadonly ro form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="no-padding margin-bottom col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua"> Vehicle No.</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txvhcno" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="no-padding margin-bottom col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Type</label>
                    <div class="col-sm-8 drop-down">
                         <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="no-padding margin-bottom col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Salespoint</label>
                    <div class="col-sm-8 drop-down">
                         <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="no-padding margin-bottom col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Status </label>
                    <div class="col-sm-8 drop-down">
                         <asp:DropDownList ID="cbstatus" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="no-padding margin-bottom col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Salesman / Driver </label>
                    <div class="col-sm-8 drop-down">
                         <asp:DropDownList ID="cbemployee" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="no-padding margin-bottom col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">Capacity</label>
                    <div class="col-sm-8 drop-down">
                        <asp:TextBox ID="txcapacity" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="no-padding margin-bottom col-sm-6 clearfix">
                    <label class="col-sm-4 control-label titik-dua">UOM</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control">
                        </asp:DropDownList>
                    </div>
                </div>


            </div>
        </div>
    </div>
 
    <div class="navi margin-bottom">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save"  OnClick="btsave_Click" />
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" />
    </div>
</asp:Content>

