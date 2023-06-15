<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_msttax.aspx.cs" Inherits="fm_msttax" %>

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
        <h4 class="jajarangenjang">Tax</h4>
        <div class="h-divider"></div>

        <div id="listTax" runat="server">
            <div class="row">
            <div class="form-group">
                <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging" ShowFooter="True" CellPadding="0">
                                <Columns>
                                    <asp:TemplateField HeaderText="Tax Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtaxcode" runat="server" Text='<%#Eval("tax_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax Name">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtaxnm" runat="server" Text='<%#Eval("tax_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Tax Description">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtaxdesc" runat="server" Text='<%#Eval("tax_desc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Type">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtaxtype" runat="server" Text='<%#Eval("tax_typedesc") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Formula">
                                        <ItemTemplate>
                                            <asp:Label ID="lbtaxformula" runat="server" Text='<%#Eval("tax_formula") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <ItemTemplate><%# Eval("status") %></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:CommandField ShowSelectButton="True" HeaderText="Select"/>
                                </Columns>
                                <FooterStyle BackColor="Yellow" />
                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            </div>
        </div>
        <div class="h-divider"></div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="newTax" runat="server">
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Tax Code#</label>
                        <div class="col-sm-10">
                        <div class="input-group">
                        <asp:Label ID="lbtaxcode" CssClass="form-control input-group-sm ro" runat="server" Text=""></asp:Label>
                        </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Tax Name</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtaxnm" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div>     
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Formula</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtaxformula" CssClass="form-control" runat="server" TextMode="Number"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Tax Description</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txtaxdesc" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div> 
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Tax Type</label>
                        <div class="col-sm-10">
                            <asp:DropDownList runat="server" ID="cbtaxtype" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                    </div>
                </div>
            </div> 
        </div>
        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grd" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <div class="text-center navi">
                    <asp:Button id="btnew" runat="server" Text="New" class="btn btn-warning btn-sm" OnClick="btnew_Click" />
                    <asp:Button id="btsave" runat="server" Text="Save" class="btn btn-success btn-sm" OnClick="btsave_Click" />
                </div>
            </div>
        </div>  
        </ContentTemplate>
        </asp:UpdatePanel>

    </div>

</asp:Content>

