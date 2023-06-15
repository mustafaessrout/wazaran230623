<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_goodreceived_brn.aspx.cs" Inherits="fm_goodreceived_brn" %>

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

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">Good Received From Depo/Sub Depo</div>
    <div class="container">
        <div class="row margin-bottom">
            <label class="control-label input-sm col-sm-1">Branch / Depo</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbdepo" AutoPostBack="true" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbdepo_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="alert alert-warning">List Of Internal Transfer Need Received</div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="table table-bordered table-sm input-sm" runat="server" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" EmptyDataText="No data  Internal transfer found !" ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Transfer No.">
                            <ItemTemplate>
                                <asp:Label ID="lbtransferno" runat="server" Text='<%#Eval("trf_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Date">
                            <ItemTemplate><%#Eval("trf_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Source Warehouse">
                            <ItemTemplate><%#Eval("whs_from_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate><%#Eval("remark") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Detail">
                            <ItemTemplate>
                                <asp:GridView ID="grddetail" CssClass="mGrid overflow-y" style="max-height:450px" runat="server" AutoGenerateColumns="false">
                                 <Columns>
                                     <asp:TemplateField HeaderText="Code">
                                         <ItemTemplate>
                                             <%#Eval("item_cd") %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Name">
                                         <ItemTemplate>
                                             <%#Eval("item_nm") %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Qty">
                                         <ItemTemplate>
                                             <%#Eval("qty","{0:N2}") %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                      <asp:TemplateField HeaderText="UOM">
                                         <ItemTemplate>
                                             <%#Eval("uom") %>
                                         </ItemTemplate>
                                     </asp:TemplateField>
                                 </Columns>
                                </asp:GridView>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <div class="drop-down">
                                    <asp:DropDownList ID="cbaction" runat="server" CssClass="form-control input-sm">
                                        <asp:ListItem Value="" Text="----Select----"></asp:ListItem>
                                        <asp:ListItem Value="C" Text="Receive Full"></asp:ListItem>
                                        <asp:ListItem Value="R" Text="Reject All"></asp:ListItem>
                                    </asp:DropDownList>
                                </div>

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Post">
                            <ItemTemplate>
                                <asp:LinkButton ID="btpost" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btpost_Click">Post</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataRowStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

