<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_trfsalespoint.aspx.cs" Inherits="fm_trfsalespoint" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="css/anekabutton.css" />
    <script>    
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div id="container">
        <h4 class="jajarangenjang">Trasfer Salespoint to Salespoint</h4>
        <div class="h-divider"></div>

        <div id="frmBranch" runat="server">
            <div class="col-sm-6">
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <label class="control-label col-sm-2">From</label>
                            <div class="col-sm-10">
                                <asp:DropDownList ID="cbfrom" runat="server" CssClass="form-control" >
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdfrm" CssClass="mGrid" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True" CellPadding="0">
                                        <Columns>
                                        </Columns>
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="toBranch" runat="server">
        <div class="col-sm-6">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label class="control-label col-sm-2">To</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="cbto" runat="server" CssClass="form-control" >
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grdto" CssClass="mGrid" runat="server" AutoGenerateColumns="False"  ShowHeaderWhenEmpty="True" CellPadding="0">
                                    <Columns>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>

        <div class="row margin-bottom">
            <div class="col-sm-12">
                <div class="text-center navi">
                    <asp:Button id="btchecking" runat="server" Text="Check" class="btn btn-warning btn-sm" OnClick="btchecking_Click" />
                    <asp:Button id="bttransfer" runat="server" Text="Transfer" class="btn btn-success btn-sm" OnClick="bttransfer_Click" />
                </div>
            </div>
        </div>  

    </div>

</asp:Content>

