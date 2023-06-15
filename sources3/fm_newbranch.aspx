<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_newbranch.aspx.cs" Inherits="fm_newbranch" %>

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
        <h4 class="jajarangenjang">Setup For New Branch</h4>
        <div class="h-divider"></div>

        <div id="frmbranch" runat="server">
            <div class="col-sm-12">

                <div class="row">
                    <div class="col-sm-6">
                        <div class="form-group">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grdsalespoint" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grdsalespoint_PageIndexChanging" OnSelectedIndexChanging="grdsalespoint_SelectedIndexChanging" ShowFooter="True" CellPadding="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespointcd" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="True" HeaderText="Select"/>
                                    </Columns>
                                    <FooterStyle BackColor="Yellow" />
                                </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <h4 class="jajarangenjang" runat="server" id="jjsalespoint"><asp:Label ID="lbsalespoint" CssClass="form-control" runat="server" Text=""></asp:Label></h4>
                                <div class="h-divider"></div>
                            </ContentTemplate>
                         </asp:UpdatePanel>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-4 control-label titik-dua ">Start Date</label>
                                <div class="col-sm-8">
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="wazarandt" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                                        <asp:CalendarExtender CssClass="date" ID="wazarandt_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="wazarandt">
                                        </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <label class="col-sm-4 control-label titik-dua ">Balance Cashier</label>
                                <div class="col-sm-8">
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txcash" runat="server" CssClass="form-control" Enabled="true"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="form-group">
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grditem" Caption="Beginning Stock" CssClass="mGrid" runat="server" AutoGenerateColumns="False" ShowFooter="False" CellPadding="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbitem_cd" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbitem_nm" runat="server" Text='<%#Eval("item_shortname") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Qty Stock">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("qty") %>' CssClass="form-control input-sm"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            </div>
                        </div>

                    </div>
                </div>


            </div>
        </div>

        <div class="row margin-bottom">
            <div class="col-sm-12">
                <div class="text-center navi">
                    <asp:Button id="btback" runat="server" Text="Back" class="btn btn-warning btn-sm" OnClick="btback_Click" />
                    <asp:Button id="btnext" runat="server" Text="Next" class="btn btn-success btn-sm" OnClick="btnext_Click" />
                    <asp:Button id="btsave" runat="server" Text="Save" class="btn btn-success btn-sm" OnClick="btsave_Click" />
                </div>
            </div>
        </div>  

    </div>

</asp:Content>

