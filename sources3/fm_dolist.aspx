<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_dolist.aspx.cs" Inherits="fm_dolist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">PO Branch List</div>
    <div class="h-divider"></div>
    
    <div class="container-fluid">
        <div class="row">
            <div class="col-sm-7 form-group">
                <label class="control-label col-sm-2" >Salespoint </label>
                <div class="col-sm-10 input-group">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <strong>
                                <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
                            </strong>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-7 form-group">
                <label class="control-label col-sm-2" >Search DO </label>
                <div class="col-sm-10 input-group">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="txsearch" runat="server" CssClass="form-control"></asp:TextBox> 
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="input-group-btn">
                        <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grddo" runat="server" CssClass="mygrid table table-striped table-hover" AllowPaging="True" AutoGenerateColumns="False" Width="100%" OnPageIndexChanging="grddo_PageIndexChanging" OnSelectedIndexChanging="grddo_SelectedIndexChanging" CellPadding="0" ForeColor="#333333" GridLines="None" EmptyDataText="NO DATA FOUND">
                        <AlternatingRowStyle  />
                        <Columns>
                            <asp:TemplateField HeaderText="DO No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbdono" runat="server" Text='<%# Eval("do_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate><%# Eval("do_dt","{0:dd-MMM-yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <ItemTemplate><%# Eval("do_sta_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PO No.">
                                <ItemTemplate><%# Eval("po_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice No.">
                                <ItemTemplate><%# Eval("invoice_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To">
                                <ItemTemplate><%# Eval("to_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Address">
                                <ItemTemplate><%# Eval("to_addr") %></ItemTemplate>
                                <ItemTemplate>
                                    <%# Eval("to_addr") %>
                                </ItemTemplate>
                                <ItemTemplate>
                                    <%# Eval("to_addr") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" HeaderText="Select"/>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page"/>
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    
 
    <div class="navi">
        <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success btn btn-new" OnClick="btnew_Click" />
    </div>
</asp:Content>

