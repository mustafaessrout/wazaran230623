<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_pedndinginvoices.aspx.cs" Inherits="fm_pedndinginvoices" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="divheader">List Of Pending invoices</div>
    <div class="h-divider"></div>
    

    <div class="container-fluid">
        <div class="row clearfix">
            <div class="col-md-offset-3 col-md-6 col-sm-offset-2 col-sm-8">
             
                <div class="clearfix form-group">
                    <asp:Label ID="lbdate" runat="server" Text="Salespoint" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10">
                        <div class="drop-down">
                            <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                        </div>
                    </div>
                </div>
                   
              
              
                <div class="clearfix form-group">
                    <asp:Label ID="lbdate2" runat="server" Text="Report type" CssClass="control-label col-sm-2"></asp:Label>
                    <div class="col-sm-10">
                        <div class="drop-down">
                            <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control">
                                <asp:ListItem Value="0">Pending Invoices</asp:ListItem>
                                <asp:ListItem Value="1">Daily Transaction</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
               
            </div>         
        </div>

        <div class="navi row margin-top margin-bottom">
            <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
    </div>

</asp:Content>

