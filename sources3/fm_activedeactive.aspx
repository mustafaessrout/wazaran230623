<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_activedeactive.aspx.cs" Inherits="fm_activedeactive" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
    <style>
        th {
            position: sticky;
            top: 0;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">Activated / Deactivated Customer</div>
    <div class="container">
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">Salespoint</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbsalespoint" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label class="control-label input-sm col-sm-1">Salesman</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbsalesman" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
            </div>
            <label class="control-label input-sm col-sm-1">Customer</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txcustomer" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <div class="col-sm-1">
                <asp:LinkButton ID="btsearch" OnClientClick="ShowProgress();" CssClass="btn btn-primary btn-sm" runat="server" OnClick="btsearch_Click">Search</asp:LinkButton>
            </div>
        </div>
        <div class="row margin-bottom alert alert-info">
            <div class="col-sm-12 overflow-y" style="max-height: 360px;">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" EmptyDataText="No Customer Found !" ShowHeaderWhenEmpty="True" OnRowDataBound="grd_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Cust Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcustomercode" runat="server" Text='<%#Eval("cust_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Name">
                            <ItemTemplate><%#Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address">
                            <ItemTemplate>
                                <div class="input-group">
                                    <asp:TextBox ID="txaddress" CssClass="input-group-sm input-sm form-control" runat="server" Text='<%#Eval("addr") %>'></asp:TextBox>
                                    <div class="input-group-btn">
                                        <asp:LinkButton ID="btupdateaddress" runat="server" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" OnClick="btupdateaddress_Click">Update</asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="City">
                            <ItemTemplate>
                                <div class="input-group">
                                    <div class="input-group-sm drop-down">
                                         <asp:DropDownList ID="cbcity" OnSelectedIndexChanged="cbcity_SelectedIndexChanged" CssClass="form-control input-sm input-group-sm" AutoPostBack="true" onchange="ShowProgress();" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="input-group-btn">
                                        <asp:LinkButton ID="btupdatecity" OnClientClick="ShowProgress();" CssClass="btn btn-info btn-sm" OnClick="btupdatecity_Click" runat="server">Update</asp:LinkButton>
                                    </div>
                                </div>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sectory">
                            <ItemTemplate>
                                <div class="input-group">
                                    <div class="input-group-sm drop-down">
                                        <asp:DropDownList ID="cbdistrict" CssClass="input-group-sm form-control" runat="server"></asp:DropDownList>
                                    </div>
                                    <div class="input-group-btn">
                                        <asp:LinkButton ID="btupdatedistrict" OnClientClick="ShowProgress();" OnClick="btupdatedistrict_Click" runat="server" CssClass="btn btn-success btn-sm">Update</asp:LinkButton>
                                    </div>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Have Balance">
                            <ItemTemplate>
                                <asp:Label ID="lbbalance" runat="server" Text='<%#Eval("balance","{0:N2}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate>
                                <asp:Label ID="lbstatus" runat="server" Text='<%#Eval("cust_sta_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Activated">
                            <ItemTemplate>
                                <asp:LinkButton ID="btactivated" OnClientClick="ShowProgress();" OnClick="btactivated_Click" runat="server">Activate</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Deactivated">
                            <ItemTemplate>
                                <asp:LinkButton ID="btdeactivated" OnClientClick="ShowProgress();" OnClick="btdeactivated_Click" runat="server">Deactivate</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

