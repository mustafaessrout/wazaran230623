<%@ Page Title="approval destroy" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_destryoapp.aspx.cs" Inherits="fm_destryoapp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <div class="divheader">
         <span style="float:right"><button type="button" class="btn btn-primary btn-sm" data-toggle="modal" data-target="#allApp">View All Aprrove Image</button></span>
        Destroy Approval
    </div>

    <div class="h-divider"></div>

    <div class="container-fluid top-devider">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div class="overflow-x">
                        <asp:GridView ID="grddoc" CssClass="table table-hover table-striped mygrid" runat="server"  OnSelectedIndexChanging="grddoc_SelectedIndexChanging" AutoGenerateColumns="False" EmptyDataText="There are no Destroy">
                            <AlternatingRowStyle  />
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction no.">
                                    <ItemTemplate>
                                        <asp:Label ID="trnstkno" runat="server" Text='<%# Eval("trnstkno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Whs">
                                    <ItemTemplate><%# Eval("whs_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bin">
                                    <ItemTemplate><%# Eval("bin_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remark">
                                    <ItemTemplate><%# Eval("trnstkremark") %></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Reason">
                                    <ItemTemplate><%# Eval("reason") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Transaction Date">
                                    <ItemTemplate><%# Eval("trnstkDate") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Trn Trnsaction Date">
                                    <ItemTemplate>
                                        <asp:Label ID="trn_trnstkdate" runat="server" Text='<%# Eval("trn_trnstkdate") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Spv">
                                    <ItemTemplate>
                                        <asp:Label ID="emp_nm" runat="server" Text='<%# Eval("emp_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowSelectButton="True" HeaderText="Action"/>
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle  />
                            <SelectedRowStyle CssClass="table-edit" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
   
    <div class="h-divider"></div>

    <div class="container-fluid top-devider">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div class="row col-md-6"  id="pnls2" runat="server">
                    <div class="clearfix margin-bottom">
                        <asp:Label ID="trsNoLbl" Text="Transaction no." runat="server" CssClass="control-label col-lg-2" />
                        <div class="col-lg-10 ">
                            <asp:Label ID="trsNo"  runat="server" CssClass="control-label text-primary text-bold" style="font-size:large" />
                        </div>
                        <asp:Label ID="prodSpvLbl" Text="Product SPV." runat="server" CssClass="control-label col-lg-2" />
                        <div class="col-lg-10 ">
                            <asp:Label ID="prodSpv"  runat="server" CssClass="control-label text-primary text-bold" style="font-size:large" />
                        </div>
                    </div>
                </div>
                <div class="row col-md-6" id="pnls1" runat="server">
                    <div class="col-lg-6">
                        <asp:Panel runat="server" ID="uplPnl">
                            <asp:FileUpload ID="upl" runat="server"  />
                        </asp:Panel>

                        <asp:HyperLink ID="hlpic" runat="server" class="example-image-link" data-lightbox="example-1" Text="Picture">
                            <asp:Label ID="lblemailid" runat="server" Font-Size="Small" ></asp:Label> 
                        </asp:HyperLink>
                    </div>
                    <div class="col-lg-6 text-right">
                        <asp:Button ID="btapprove" runat="server" Text="Approve" CssClass="btn btn-success btn-save" OnClick="btapp_Click" />
                    </div>
                </div>
                <div class="clearfix"></div>
                <div class="row col-md-6" id="pnls3" runat="server">
                    <asp:Label ID="Label1" Text="Branch Supervisor Approval" runat="server" CssClass="control-label col-lg-4" />
                    <div class="col-lg-8">
                        <asp:HyperLink  ID="dst1" runat="server" />
                    </div>
                    <div class="clearfix"></div>
                    <asp:Label ID="Label3" Text="Photo Before" runat="server" CssClass="control-label col-lg-4" />
                    <div class="col-lg-8 ">
                        <asp:HyperLink  ID="dst2" runat="server"  />
                    </div>
                </div>
                <div class="col-sm-12">
                    <asp:GridView ID="grdDtl" CssClass="table table-hover table-striped mygrid" runat="server" AutoGenerateColumns="False" EmptyDataText="There are no Data">
                            <AlternatingRowStyle  />
                            <Columns>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate><%# Eval("item_cd") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate><%# Eval("UOM") %></ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unitprice Date">
                                    <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle  />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                </div>
            </ContentTemplate>
             <Triggers>
               <asp:PostBackTrigger ControlID="btapprove" />
            </Triggers>
        </asp:UpdatePanel>
    </div>

    <div id="allApp" class="modal fade" role="dialog">
  <div class="modal-dialog modal-lg">

    <!-- Modal content-->
    <div class="modal-content">
      <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal">&times;</button>
        <h4 class="modal-title">Destroy Approve List</h4>
      </div>
      <div class="modal-body">
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <div class="overflow-x">
                        <asp:GridView ID="grdls" CssClass="table table-hover table-striped mygrid" runat="server"  AutoGenerateColumns="False" EmptyDataText="There are no Destroy">
                            <AlternatingRowStyle  />
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction no.">
                                    <ItemTemplate><%# Eval("trnstkno") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Produc SPV">
                                    <ItemTemplate><%# Eval("prod_spv") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="File">
                                    <ItemTemplate>
                                        <a href="/images/<%# Eval("filename") %>"><%# Eval("filename") %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page" />
                            <RowStyle  />
                            <SelectedRowStyle CssClass="table-edit" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
      </div>
      <div class="modal-footer">
        <button type="button" class="btn btn-danger" data-dismiss="modal">Close</button>
      </div>
    </div>

  </div>
</div>
</asp:Content>