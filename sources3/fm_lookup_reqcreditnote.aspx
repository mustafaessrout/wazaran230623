<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="fm_lookup_reqcreditnote.aspx.cs" Inherits="fm_lookup_reqcreditnote" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<head>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
   
    <script src="js/jquery.min.js"></script>
     <script src="js/bootstrap.min.js"></script> 
    <script src="js/index.js"></script>
    <script src="js/jquery.floatThead.js"></script>


    <style>
        .table-header{
            font-weight:normal;
        }
    </style>

    <script>
        function closewin() {
            window.opener.updpnl();
            window.close();
        }
    </script>
</head>

<body >
    <form id="form1" runat="server">
        <div class="containers bg-white">
            <div class="clearfix margin-bottom">
                <asp:ToolkitScriptManager ID="tsmanager" runat="server">
                </asp:ToolkitScriptManager>

                <div class="divheader">Search CN/DN</div>
                <div class="h-divider"></div>

                <div class="clearfix col-sm-6">
                    <label class="col-sm-2 control-label">Search</label>
                    <div class="col-sm-8">
                        <div class="input-group ">
                             <div class="input-group-btn">
                                <asp:TextBox ID="txsearch" runat="server" CssClass="form-control" style="border-radius:5px 0 0 5px;"></asp:TextBox>
                                <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn search" Text="Search" OnClick="btsearch_Click" />
                            </div>
                        </div>
                    </div>
                </div>

                <div class="clearfix col-sm-6">
                    <label class="col-sm-4 control-label"> Type CN/DN</label>
                    
                        
                        <div class="col-sm-8">
                            <div class=" drop-down">
                                <asp:DropDownList ID="cbcn" runat="server" CssClass="form-control radius5">
                                </asp:DropDownList>
                            </div>
                        </div>
                    
                </div>
            </div>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="overflow-y" style="max-height:490px;">
                        <asp:GridView ID="grd" runat="server" CssClass="table table-striped table-fix mygrid" AutoGenerateColumns="False"  OnSelectedIndexChanged="grd_SelectedIndexChanged">
                            <Columns>
                                <asp:TemplateField HeaderText="CN/DN No.">
                                    <ItemTemplate>
                                        <asp:Label ID="lbarcn_no" runat="server" Text='<%# Eval("arcn_no") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lbarcn_date" runat="server" Text='<%# Eval("arcn_date","{0:d/M/yyyy}") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer">
                                    <ItemTemplate>
                                        <asp:label ID="lbcust_nm" runat="server" Text='<%# Eval("cust_nm")%>'></asp:label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Type">
                                    <ItemTemplate>
                                        <asp:label ID="lbarcn_type" runat="server" Text='<%# Eval("arcn_type")%>'></asp:label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:label ID="lbstatus" runat="server" Text='<%# Eval("status")%>'></asp:label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="True" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
                <Triggers>
                  <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                </Triggers>
            </asp:UpdatePanel>
            <div style="text-align:center;padding:10px"></div>
        </div>
    </form>
</body>

