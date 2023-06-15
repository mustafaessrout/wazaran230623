<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_faq.aspx.cs" Inherits="fm_faq" %>

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
    <div class="divheader">FAQ (User Guides) </div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix">
                <div class="col-md-offset-2 col-md-8 col-sm-12 clearfix no-padding margin-bottom">
                    <div class="margin-bottom clearfix">
                        <label class="control-label col-md-2 col-sm-4 titik-dua">TYPE</label>
                        <div class="col-md-10 col-sm-8 drop-down">
                            <asp:DropDownList ID="CBTYPE" runat="server" CssClass="drop-down form-control" AutoPostBack="true" OnSelectedIndexChanged="CBTYPE_SelectedIndexChanged">
                                <asp:ListItem Value="CLAIM">CLAIM</asp:ListItem>
                                <asp:ListItem Value="ACC">ACCOUNTING</asp:ListItem>
                                <asp:ListItem Value="SALES">SALES & MARKETING</asp:ListItem>
                                <asp:ListItem Value="LOG">LOGISTIC</asp:ListItem>
                                <asp:ListItem Value="HR">HR</asp:ListItem>
                                <asp:ListItem Value="OT">OTHERS</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>

            <div class="h-divider"></div>

        </div>

        <div class="row">
            <div class="">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                    <div class="table-responsive">
                        <asp:GridView ID="grdfaq" runat="server"  CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                            <Columns>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtype" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Document">
                                    <ItemTemplate>
                                        <a class="example-image-link" href="/images/faq/<%# Eval("type") %>/<%# Eval("fileloc") %>" data-lightbox="example-1<%# Eval("fileloc") %>">
                                            <asp:Label ID="lbfileinv" runat="server" Text='<%# Eval("fileloc") %>'></asp:Label>
                                        </a>
                                    </ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="CBTYPE" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>  
            </div>
        </div>

    </div>


</asp:Content>

