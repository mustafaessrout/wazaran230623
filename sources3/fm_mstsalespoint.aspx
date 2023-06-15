<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstsalespoint.aspx.cs" Inherits="fm_mstsalespoint" %>

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
        <h4 class="jajarangenjang">Branch</h4>
        <div class="h-divider"></div>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        <div id="newBranch" runat="server">
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Branch Code</label>
                        <div class="col-sm-10">
                        <div class="input-group">
                            <asp:TextBox ID="txsalespoint_cd" CssClass="form-control" runat="server"></asp:TextBox>
                            <%--<asp:Label ID="lbsalespoint_cd" CssClass="form-control input-group-sm ro" runat="server" Text=""></asp:Label>--%>
                        </div>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Type</label>
                        <div class="col-sm-10">
                            <asp:DropDownList ID="cbType" runat="server" CssClass="form-control" >
                                <asp:ListItem Value="SP">Branch</asp:ListItem>
                            </asp:DropDownList>
                        </div>
                    </div>
                </div>
            </div>     
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Branch Name</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txsalespoint_nm" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Branch ShortName</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txsalespoint_sn" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div> 
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Phone No</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txphone" CssClass="form-control" runat="server" type="number"></asp:TextBox>
                        </div>
                    </div>
                </div>
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Email</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txemail" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div> 
            <div class="row">
                <div class="col-sm-6">
                    <div class="form-group">
                        <label class="control-label col-sm-2">Address</label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txaddress" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
                        </div>
                    </div>
                </div>
            </div> 
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>
        <div class="h-divider"></div>

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

        <div id="listBranch" runat="server">
            <div class="row">
                <div class="col-md-12">
                    <div class="form-group">                
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnRowCreated="grd_RowCreated" ShowFooter="True" CellPadding="0">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex + 1 %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespointcd" runat="server" Text='<%#Eval("salespointcd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespoint_nm" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branch ShortName">
                                            <ItemTemplate>
                                                <asp:Label ID="lbsalespoint_sn" runat="server" Text='<%#Eval("salespoint_sn") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate>
                                                <asp:Label ID="lbaddr" runat="server" Text='<%#Eval("addr1") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Phone No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbphone" runat="server" Text='<%#Eval("phone_no") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Email">
                                            <ItemTemplate>
                                                <asp:Label ID="lbemail" runat="server" Text='<%#Eval("email") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Customer">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtot_customer" runat="server" Text='<%#Eval("tot_customer") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<ItemTemplate><%# Eval("tot_customer") %></ItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total Employee">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtot_salesman" runat="server" Text='<%#Eval("tot_salesman") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<ItemTemplate><%# Eval("tot_salesman") %></ItemTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total User">
                                            <ItemTemplate>
                                                <asp:Label ID="lbtot_user" runat="server" Text='<%#Eval("tot_user") %>'></asp:Label>
                                            </ItemTemplate>
                                            <%--<ItemTemplate><%# Eval("tot_salesman") %></ItemTemplate>--%>
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
        

    </div>
</asp:Content>

