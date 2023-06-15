<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_pricereport.aspx.cs" Inherits="fm_pricereport" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

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

    <div class="divheader">Customer & Adjustment Price Report</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">
                <div class="col-md-offset-2 col-md-8 col-sm-12 clearfix no-padding margin-bottom">
                    <div class="margin-bottom clearfix">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="margin-top">
                        <ContentTemplate>
                        <label class="control-label col-md-2 col-sm-4 titik-dua">TYPE</label>
                        <div class="col-md-10 col-sm-8 drop-down">
                            <asp:DropDownList ID="CBTYPE" runat="server" CssClass="drop-down form-control" AutoPostBack="true" OnSelectedIndexChanged="CBTYPE_SelectedIndexChanged">
                               <asp:ListItem Value="0">CUSTOMER PRICE</asp:ListItem>
                               <asp:ListItem Value="1">ADJUSTMENT PRICE</asp:ListItem>                                
                               <%--<asp:ListItem Value="2">CUSTOMER PRICE HISTORY</asp:ListItem>
                               <asp:ListItem Value="3">ADJUSTMENT PRICE HISTORY</asp:ListItem> --%>  
                            </asp:DropDownList>
                        </div>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="margin-top">
                        <ContentTemplate>
                    <div runat="server" id="vCustomer" style="display: none">                        
                        <asp:Label ID="lbcustomer" runat="server" Text="TYPE" CssClass="control-label col-md-2 col-sm-4 titik-dua"></asp:Label>
                        <div class="col-md-4 col-sm-8 dropdown-button">
                            <asp:DropDownList ID="ddCustomer" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddCustomer_SelectedIndexChanged">
                                <asp:ListItem Text="Group Customer" Value="G"></asp:ListItem>
                                <asp:ListItem Text="Customer" Value="C"></asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                    </ContentTemplate>
                            <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="CBTYPE" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="margin-top">
                        <ContentTemplate>
                    <div runat="server" id="vSalespoint" style="display: none">
                        
                        <asp:Label ID="lbsalespoint" runat="server" Text="Salespoint" CssClass="control-label col-md-2 col-sm-4 titik-dua"></asp:Label>
                        <div class="col-md-4 col-sm-8 dropdown-button">
                            <asp:DropDownList ID="ddSalespoint" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddCustomer" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="margin-top">
                        <ContentTemplate>
                    <div runat="server" id="vGroupCust" style="display: none">
                       
                        <asp:Label ID="lbcusgrcd" runat="server" Text="Group Name" CssClass="control-label col-md-2 col-sm-4 titik-dua"></asp:Label>
                        <div class="col-md-4 col-sm-8 dropdown-button">
                            <asp:DropDownList ID="ddCusgrcd" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                        </div>
                    </div>
                        </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="ddCustomer" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                </div>
            </div>

            <div class="h-divider"></div>
           
            <div class="navi margin-bottom">
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
            </div>

        </div>

    </div>

</asp:Content>

