<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_datarowcontractka.aspx.cs" Inherits="fm_datarowcontractka" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 1px;
        }
        .auto-style3 {
            width: 92px;
        }
        .auto-style4 {
            width: 92px;
            height: 20px;
        }
        .auto-style5 {
            width: 1px;
            height: 20px;
        }
        .auto-style6 {
            height: 20px;
        }
        .main-content #mCSB_2_container{
            min-height: 540px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Agreement & Credit Memo HO Data Row Report</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">
                <div class="col-md-offset-2 col-md-8 col-sm-12 clearfix no-padding margin-bottom">
                    <div class="margin-bottom clearfix">
                        <label class="control-label col-md-2 col-sm-4 titik-dua">TYPE</label>
                        <div class="col-md-10 col-sm-8 drop-down">
                            <asp:DropDownList ID="CBTYPE" runat="server" CssClass="drop-down form-control">
                               <asp:ListItem Value="0">AGREEMENT HO RAW DATA</asp:ListItem>
                                 <asp:ListItem Value="1">CREDIT MEMO HO RAW DATA</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lbmonth" runat="server" Text="Month" CssClass="control-label col-md-2 col-sm-4 titik-dua"></asp:Label>
                        <div class="col-md-4 col-sm-8 drop-down-date">
                            <asp:DropDownList ID="ddMonth" runat="server" CssClass="form-control">
                                <asp:ListItem Text="January" Value="01"></asp:ListItem>
                                <asp:ListItem Text="February" Value="02"></asp:ListItem>
                                <asp:ListItem Text="March" Value="03"></asp:ListItem>
                                <asp:ListItem Text="April" Value="04"></asp:ListItem>
                                <asp:ListItem Text="May" Value="05"></asp:ListItem>
                                <asp:ListItem Text="June" Value="06"></asp:ListItem>
                                <asp:ListItem Text="July" Value="07"></asp:ListItem>
                                <asp:ListItem Text="August" Value="08"></asp:ListItem>
                                <asp:ListItem Text="September" Value="09"></asp:ListItem>
                                <asp:ListItem Text="October" Value="10"></asp:ListItem>
                                <asp:ListItem Text="November" Value="11"></asp:ListItem>
                                <asp:ListItem Text="December" Value="12"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    <div>
                        <asp:Label ID="lbyear" runat="server" Text="Year" CssClass="control-label col-md-2 col-sm-4 titik-dua"></asp:Label>
                        <div class="col-md-4 col-sm-8 drop-down-date">
                            <asp:DropDownList ID="ddYear" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="h-divider"></div>
           
            <div class="navi margin-bottom">
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            </div>

        </div>

    </div>
    
</asp:Content>

