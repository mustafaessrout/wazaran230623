<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_listproposal.aspx.cs" Inherits="fm_listproposal" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="divheader">List Proposal</div>
    <div class="h-divider"></div>
    <div class="container-fluid">
        <div class="row">
            <div class="clearfix ">
                <div class="clearfix">
                    <label for="vendor" class="col-md-1 col-sm-2 control-label control-label-sm">Principal</label>    
                    <div class="col-md-9 col-sm-8 drop-down margin-bottom">
                        <asp:DropDownList ID="cbvendor" runat="server" CssClass="form-control input-sm"></asp:DropDownList> 
                    </div>
                </div>

                <div class="form-group clearfix">
                    <label for="Month" class="col-md-1 col-sm-2 control-label  control-label-sm">Month</label>    
                    <div class="col-sm-4 drop-down">
                        <asp:DropDownList ID="cbMonth" runat="server" CssClass="form-control input-sm">
                            <asp:ListItem Text="January" Value="1"></asp:ListItem>
                            <asp:ListItem Text="February" Value="2"></asp:ListItem>
                            <asp:ListItem Text="March" Value="3"></asp:ListItem>
                            <asp:ListItem Text="April" Value="4"></asp:ListItem>
                            <asp:ListItem Text="May" Value="5"></asp:ListItem>
                            <asp:ListItem Text="June" Value="6"></asp:ListItem>
                            <asp:ListItem Text="July" Value="7"></asp:ListItem>
                            <asp:ListItem Text="August" Value="8"></asp:ListItem>
                            <asp:ListItem Text="September" Value="9"></asp:ListItem>
                            <asp:ListItem Text="October" Value="10"></asp:ListItem>
                            <asp:ListItem Text="November" Value="11"></asp:ListItem>
                            <asp:ListItem Text="December" Value="12"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <label for="Year" class="col-md-1 col-sm-2 control-label  control-label-sm">Year</label>    
                    <div class="col-sm-4 drop-down">
                        <asp:DropDownList ID="cbYear" runat="server" CssClass="form-control input-sm">                           
                        </asp:DropDownList> 
                    </div>
                </div>

                <div class="clearfix">
                    <label for="vendor" class="col-md-1 col-sm-2 control-label control-label-sm">Proposal No</label>    
                    <div class="col-md-9 col-sm-8 drop-down margin-bottom">
                        <asp:TextBox ID="txProposal" runat="server" CssClass="form-control input-sm" Width="100%"></asp:TextBox>
                    </div>
                    
                </div>
                <div class="clearfix">
                    <div class="col-md-1 col-sm-2"></div>
                    <div class="col-sm-2 margin-bottom">
                        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary btn-search" OnClick="btnSearch_Click">
                            <i aria-hidden="true" class="fa fa-search"></i>Search
                        </asp:LinkButton>  
                    </div>
                    <div class="col-sm-2 margin-bottom">
                        <asp:LinkButton ID="btnNew" runat="server" CssClass="btn btn-primary btn-default" OnClick="btnNew_Click">
                            <i aria-hidden="true" class="fa fa-add"></i>New Proposal
                        </asp:LinkButton>  
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="form-group">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="margin-top">
                <ContentTemplate>
                <div class="overflow-y" style="max-height:270px;">
                    <asp:GridView ID="grdproposal" runat="server" CellPadding="0"  CssClass="table table-fix table-striped table-hover table-page-fix mygrid" data-table-page="#copy-fst" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" OnRowCommand="grdproposal_RowCommand" OnPageIndexChanging="grdproposal_PageIndexChanging" AllowPaging="True" PageSize="30" >  
                        <Columns>
                            <asp:TemplateField HeaderText="Proposal Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbproposal" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Principal (Vendor)"><ItemTemplate><%# Eval("vendor_nm") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate>
                                    <asp:Label ID="lbcust" runat="server" Text='<%# Eval("rdcust") %>' Visible="false"></asp:Label>
                                    <%# Eval("customer") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate>
                                    <asp:Label ID="lbitem" runat="server" Text='<%# Eval("rditem") %>' Visible="false"></asp:Label>
                                    <%# Eval("product") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Start Date"><ItemTemplate><%# Eval("start_dt") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="End Date"><ItemTemplate><%# Eval("end_dt") %></ItemTemplate></asp:TemplateField>
                            <asp:TemplateField HeaderText="Status"><ItemTemplate><%# Eval("approval") %></ItemTemplate></asp:TemplateField>
                                <asp:ButtonField ButtonType="Link"  Text="<i aria-hidden='true' class='fa fa-print'></i> View" ControlStyle-CssClass="btn default btn-xs purple" CommandName="view">
                            <ControlStyle CssClass="btn default btn-xs purple" />
                            </asp:ButtonField>  
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer"  />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />

                    </asp:GridView>
                </div>
                <div class="table-copy-page-fix" id="copy-fst"></div>

                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" EventName="Click" />
                </Triggers>
                </asp:UpdatePanel>
            </div>  
        </div>
    </div>

</asp:Content>

