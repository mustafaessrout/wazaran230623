<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_duedate.aspx.cs" Inherits="fm_duedate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 86px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Due Date Report</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row clearfix">
            <div class="col-md-offset-3 col-md-6 col-sm-offset-2 col-sm-8">
             
                <div class="clearfix form-group">
                    <asp:Label ID="lbdate" runat="server" Text="Salespoint" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10">
                        <div class="drop-down">
                            <asp:DropDownList ID="cbsalespointcd" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
                   
              
              
                <div class="clearfix form-group">
                    <asp:Label ID="lbdate2" runat="server" Text="Period" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbperiod" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
               
            </div>         
        </div>

         <div class="navi row margin-top margin-bottom">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>

</asp:Content>

