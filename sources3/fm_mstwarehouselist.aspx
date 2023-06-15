<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstwarehouselist.aspx.cs" Inherits="fm_mstwarehouselist" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Warehouse List</div>
    <div class="h-divider"></div>

    <div class="container-fluid">

        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label class="control-label col-sm-2">Branch</label>
                    <div class="input-group">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" class="sd-bar-container">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbBranch" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbBranch_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="sd-bar col-sm-4 col-md-2 margin-bottom padding-bottom no-padding-left">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="sd-bar-container">
                <ContentTemplate>
                     <asp:TreeView ID="tvw" runat="server"  ShowExpandCollapse="False" OnSelectedNodeChanged="tvw_SelectedNodeChanged">
                       <%-- <HoverNodeStyle Font-Underline="True" ForeColor="#FFF" BackColor="#5D7B9D" />--%>
                        <NodeStyle  />
                        <ParentNodeStyle />
                        <SelectedNodeStyle CssClass="active"/>
                    </asp:TreeView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="col-sm-8 col-md-10">
                 <div style="color:red;font-weight:bolder;padding:5px 5px 5px 5px">WAREHOUSE</div>
                <div>
                    <table class="table  table-striped mygrid row-no-padding">
                        <tr >
                            <th>Warehouse Code</th>
                            <th>Warehouse Name</th>
                            <th>Address</th>
                            <th>City/Area</th>
                            <th>Level</th>
                            <th>Parent</th>
                            <th>Action</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txwhscode" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txwhsname" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txaddress" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td class="">
                                <asp:DropDownList ID="cbcity" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                            <td class="">
                                <asp:DropDownList ID="cblevel" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                            <td class="">
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                            <asp:DropDownList ID="cbparent" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Button ID="btaddwhs" runat="server" Text="Add" CssClass=" btn btn-success btn-sm btn-add" OnClick="btaddwhs_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" AllowPaging="True" GridLines="None" OnSelectedIndexChanging="grd_SelectedIndexChanging" Width="100%" CellPadding="4" OnSelectedIndexChanged="grd_SelectedIndexChanged" CssClass="table table-hover table-striped margin-top margin-bottom" >
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="Warehouse Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbcode" runat="server" Text='<%# Eval("whs_cd") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate><%# Eval("whs_nm") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Sales Point">
                                            <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Address">
                                            <ItemTemplate><%# Eval("addr") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="City">
                                            <ItemTemplate><%# Eval("loc_cd") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Level">
                                            <ItemTemplate><%# Eval("level_no") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:CommandField ShowSelectButton="True" ShowEditButton="True" />
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle  />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>   
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="tvw" EventName="SelectedNodeChanged" />
                                <asp:AsyncPostBackTrigger ControlID="btaddwhs" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
                <div class="h-divider"></div>
                <div style="color:red;font-weight:bolder;padding:5px 5px 5px 5px">STORAGE BIN DETAILS</div>
                    <div>
                        <table class="table table-striped mygrid row-no-padding">
                            <tr style="background-color:silver">
                                <th>Bin Code</th>
                                <th>Bin Name</th>
                                <th>Stock Type</th>
                                <th>Last Stock</th>
                                <th>Action</th>
                            </tr>
                            <tr>
                                <td >
                                    <asp:TextBox ID="txcode" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </td>
                                <td >
                                    <asp:TextBox ID="txname" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </td> 
                                <td class="drop-down">
                                    <asp:DropDownList ID="cbstocktype" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                                </td>
                                <td>
                                    <asp:TextBox ID="txlaststock" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                </td>
                                <td style="width: 40px;">
                                    <asp:Button ID="btaddbin" runat="server" Text="Add" CssClass="btn-success btn-sm btn btn-add" OnClick="btaddbin_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grddtl" runat="server" AutoGenerateColumns="False" CellPadding="4" CssClass="table table-hover table-striped margin-top margin-bottom"  GridLines="None" Width="100%">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbbincode" runat="server" Text='<%# Eval("bin_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Storage Bin">
                                    <ItemTemplate><%# Eval("bin_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Stock Type">
                                    <ItemTemplate><%# Eval("stock_typ_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Last Stock Amout">
                                    <ItemTemplate><%# Eval("last_stock") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" />
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
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="btaddbin" EventName="Click" />
                        </Triggers>
                    </asp:UpdatePanel>
            </div>
            
        </div>
    </div>

  
</asp:Content>

