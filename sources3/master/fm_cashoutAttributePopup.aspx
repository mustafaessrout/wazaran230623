<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_cashoutAttributePopup.aspx.cs" Inherits="fm_cashoutAttributePopup" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<body style="font-family: Tahoma,Verdana; font-size: small">

    <form id="form1" runat="server">
        <asp:ToolkitScriptManager ID="tsmanager" runat="server">
        </asp:ToolkitScriptManager>
        <div class="div-table">
            <link href="../css/lightbox.css" rel="stylesheet" />
            <link href="../css/sweetalert.css" rel="stylesheet" />
            <link rel="stylesheet" type="text/css" href="../css/component.css" />
            <script src="../js/modernizr.custom.js"></script>

            <script src="../js/sweetalert.min.js"></script>
            <script src="../js/sweetalert-dev.js"></script>
            <script src="../js/modernizr.js"></script>
            <link href="../css/anekabutton.css" rel="stylesheet" />
            <link href="../css/anekabutton.css" rel="stylesheet" />
            <link href="../css/bootstrap.min.css" rel="stylesheet" />
            <link href="Content/beatifullcontrol.css" rel="stylesheet" />
            <strong>Cashout Attribute Mapping</strong>
            
            <script>
                function closewin() {
                    window.opener.updpnl();
                    window.close();
                }
                function OpenAddControl() { }
            </script>
            <style>
            </style>
            <div class="container-fluid">
                <div class="row">
                    <div class="col-md-2 col-sm-6" >Cash Out Code</div>
                    <div class="col-md-10 col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblCashOutCode" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-6"></div>
                    <div class="col-md-6"></div>
                </div>
                <div class="row">
                    <div class="col-md-2 col-sm-6" >Cash Out For</div>
                    <div class="col-md-10 col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblCashoutFor" runat="server" Text="Branches Cash IN OUT"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2 col-sm-6">InOut</div>
                    <div class="col-md-10 col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblInOut" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 col-sm-6" >Routine/Non Routine</div>
                    <div class="col-md-10 col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblRoutineNonRoutine" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col-md-2 col-sm-6">Category</div>
                    <div class="col-md-10 col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lblCategory" runat="server"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-2 col-sm-6" >Attribute Name</div>
                    <div class="col-md-10 col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txtAttributeName" runat="server" Text=""></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col3"></div>
                    <div class="col4"></div>
                </div>
                <div class="row">
                    <div class="col-md-2 col-sm-6" >Data Type</div>
                    <div class="col-md-10 col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="ddlDataType" AutoPostBack="true" runat="server" OnSelectedIndexChanged="ddlDataType_SelectedIndexChanged"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="col3"></div>
                    <div class="col4"></div>
                </div>

                <div class="row">
                    <div class="col-md-2 col-sm-6" >Mapping Details</div>
                    <div class="colFull">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grd" OnRowDeleting="grd_RowDeleting" OnSelectedIndexChanging="grd_SelectedIndexChanging" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="1"
                                    ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Attribute  Name ">
                                            <ItemTemplate>
                                                <asp:HiddenField ID="hdIDS" Value='<%# Eval("IDS") %>' runat="server" />
                                                <asp:Label ID="lbAttributeName" runat="server" Text='<%# Eval("attribute_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Data Type ">
                                            <ItemTemplate>
                                                <asp:Label ID="lbDatatypeName" runat="server" Text='<%# Eval("datatypeName") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                        <asp:CommandField ShowDeleteButton="True" HeaderText="Action" ShowSelectButton="true" />
                                    </Columns>

                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                                </asp:GridView>


                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12 col-sm-12 text-center">
                        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-success" OnClick="btsave_Click"  />
                        <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="btn btn-danger" OnClick="btnClose_Click"  />
                    </div>
                </div>
            </div>

        </div>
    </form>
</body>
