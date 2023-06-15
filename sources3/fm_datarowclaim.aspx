<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_datarowclaim.aspx.cs" Inherits="fm_datarowclaim" %>
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
    <div class="divheader">Claim Detail Data Row Report</div>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">
                <div class="col-md-offset-2 col-md-8 col-sm-12 clearfix no-padding margin-bottom">
                    <div class="margin-bottom clearfix">
                        <label class="control-label col-md-2 col-sm-4 titik-dua">YEAR</label>
                        <div class="col-md-10 col-sm-8 drop-down">
                            <asp:DropDownList ID="cbYear" runat="server" CssClass="drop-down form-control">
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

