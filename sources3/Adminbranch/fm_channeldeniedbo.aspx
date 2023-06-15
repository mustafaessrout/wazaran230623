<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_channeldeniedbo.aspx.cs" Inherits="Adminbranch_fm_channeldeniedbo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />

     <script>
         function ShowProgress() {
             $('#pnlmsg').show();
         }

         function HideProgress() {
             $("#pnlmsg").hide();
             return false;
         }
         $(document).ready(function () {
             $('#pnlmsg').hide();
         });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <h4 class="jajarangenjang">Channel Setup for denied Sales/Canvas from Back Office</h4>
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-md-12">
                <table><tr><th>Name</th><th>Add</th><th></th></tr>
                <tr><td>
                    <asp:DropDownList ID="cbchannel" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbchannel_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        <asp:LinkButton ID="btadd" CssClass="btn btn-primary" runat="server" OnClientClick="javascript:ShowProgress();" OnClick="btadd_Click"><i class="fa fa-plus"></i>ADD</asp:LinkButton>
                    </td>
                    <td>
                        *)Eling Hen! Add mean this channel will be denied from Back Office Transaction, reversed for Deleted
                    </td>
                </tr>

                </table>
            </div>
        </div>
        <div class="form-group">
            <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" CellPadding="0">
                <Columns>
                     <asp:TemplateField HeaderText="S#">
                        <ItemTemplate>
                            <asp:Label ID="lbSNo" runat="server" Text='<%#Eval("ItemNo") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Code">
                        <ItemTemplate>
                            <asp:Label ID="lbchannelcode" runat="server" Text='<%#Eval("otlcd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Channel Name">
                        <ItemTemplate>
                            <%#Eval("otlcd_nm") %>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Reason For Deleted">
                        <ItemTemplate>
                            <asp:TextBox ID="txreason" runat="server"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowDeleteButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
</asp:Content>

