<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockconfirm.aspx.cs" Inherits="fm_stockconfirm" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Stock Confirmation as 
        <asp:TextBox ID="dtstock" runat="server" AutoPostBack="True" Enabled="False" BorderStyle="None" CssClass="text-red"></asp:TextBox>
        <asp:CalendarExtender ID="dtstock_CalendarExtender" runat="server" TargetControlID="dtstock" Format="d/M/yyyy" CssClass="black">
        </asp:CalendarExtender>
    </div>
    <div class="h-divider"></div>

    <div class="container">
        <div class="row">
            <div class=" center">
                <div class="overflow-y" style="max-height:480px; width:1000px;">
                  <asp:GridView ID="grd" runat="server" CssClass="table table-striped table-fix mygrid" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" GridLines="None">
                        <AlternatingRowStyle  />
                        <Columns>
                            <asp:TemplateField HeaderText="Old Code">
                                <ItemTemplate> 
                                    <asp:Label ID="lbitemcode_old" runat="server" Text='<%# Eval("item_cd_old") %>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate> 
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded">
                                <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bin Cd">
                                <ItemTemplate> 
                                    <asp:Label ID="lbbin_cd" runat="server" Text='<%# Eval("bin_cd") %>'></asp:Label>  
                                </ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="opening">
                                <ItemTemplate>
                                    <asp:Label ID="lbopening" runat="server" Text='<%# Eval("opening") %>'></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Rec">
                                <ItemTemplate>
                                    <asp:Label ID="lbrec" runat="server" Text='<%# Eval("rec") %>'></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Return">
                                <ItemTemplate>
                                    <asp:Label ID="lbret" runat="server" Text='<%# Eval("retur") %>'></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="StkIn">
                                <ItemTemplate>
                                    <asp:Label ID="lbstkIn" runat="server" Text='<%# Eval("stkin") %>'></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales">
                                <ItemTemplate>
                                    <asp:Label ID="lbsales" runat="server" Text='<%# Eval("sales") %>'></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="StkOut">
                                <ItemTemplate>
                                    <asp:Label ID="lbstkout" runat="server" Text='<%# Eval("stkout") %>'></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Closing">
                                <ItemTemplate>
                                    <asp:Label ID="lbclosing" runat="server" style="background-color:#ecec07" Text='<%# Eval("closing") %>'></asp:Label></ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value">
                                <ItemTemplate>
                                    <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Right" />
                                <ItemStyle HorizontalAlign="Right" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:TextBox ID="txqty" runat="server" style="background-color:#ecec07" CssClass="form-control input-sm"></asp:TextBox>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdwhs_cd" runat="Server" Value='<%# Eval("whs_cd") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdbin_cd" runat="Server" Value='<%# Eval("bin_cd") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdqty" runat="Server" Value='<%# Eval("qty") %>' />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle  />
                        <SortedAscendingHeaderStyle  />
                        <SortedDescendingCellStyle  />
                        <SortedDescendingHeaderStyle />
                    </asp:GridView>
            </div>
            </div>   
          
            
            <div class="alert alert-anger text-center">
                <asp:Label ID="lbcap" runat="server" CssClass="text-bold text-red" Text="Hereby state that all stock in this day is CORRECT"></asp:Label>
            </div>
            <div class="h-divider"></div>
            <div class="navi margin-bottom">
                <asp:Button ID="btpostpone" runat="server" CssClass="btn-danger btn btn-cancel" OnClick="btpostpone_Click" Text="Postpone (you have chance until 5 days)" />
                <asp:Button ID="btconfirm" runat="server" CssClass="btn-primary btn btn-confirm" OnClick="btconfirm_Click" Text="Confirm" />
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click"/>
            </div>
        </div>
    </div>
    
    
</asp:Content>

