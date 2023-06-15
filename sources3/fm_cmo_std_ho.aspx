<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cmo_std_ho.aspx.cs" Inherits="fm_cmo_std_ho" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link rel="stylesheet" href="css/anekabutton.css" />
    <script>    
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }

        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
        }

        <%--$(document).ready(function () {
            $("#<%=btsearch.ClientID%>").click(function () {
                PopupCenter('fm_lookup_cmo_ho.aspx', 'xtf', '900', '500');

            });
        });--%>
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:HiddenField ID="hdstd" runat="server" />
    <asp:HiddenField ID="hditem" runat="server" />

     <div class="container">
        <h4 class="jajarangenjang">STD (Sales to Distributor)</h4>
        <div class="h-divider"></div>

        <div class="row">
            <div class="form-group">
                <div class="col-md-6">
                    <label class="control-label col-sm-3">GDN Date</label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="dtstd" CssClass="form-control input-sm" runat="server" AutoPostBack="true" OnTextChanged="dtstd_TextChanged"></asp:TextBox>
                        <asp:CalendarExtender ID="dtstd_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtstd">
                        </asp:CalendarExtender>
                    </div>
                </div>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
        <ContentTemplate>

        <div class="row">
            <div class="form-group">
                <div class="col-md-12">                
                    <table class="mGrid">
                        <tr>
                            <th>GDN No</th>
                            <th>From</th>
                            <th>Ship To</th>
                            <th>Vehicle</th>
                        </tr>
                        <tr>
                            <td>
                                <asp:TextBox ID="txstdref" runat="server" CssClass="form-control"></asp:TextBox>     
                            </td>
                            <td class="drop-down">
                                <asp:DropDownList ID="cbFrom" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                            <td class="drop-down">
                                <asp:DropDownList ID="cbTo" runat="server" CssClass="form-control">
                                </asp:DropDownList>
                            </td>
                            <td class="drop-down">
                                <asp:TextBox ID="txvehicle" runat="server" CssClass="form-control"></asp:TextBox> 
                            </td>
                        </tr>
                    </table>

                    <h5 class="jajarangenjang">Details</h5>
                    <div class="h-divider"></div>

                    <table class="mGrid">
                        <tr>
                            <th>Item Code</th>
                            <th>Qty</th>
                            <th>UOM</th>
                            <th></th>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txitem" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" TargetControlID="txitem" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetItemList" UseContextKey="True" OnClientItemSelected="ItemSelected">
                                        </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:TextBox ID="txqty" runat="server" CssClass="form-control"></asp:TextBox>   
                            </td>
                            <td>
                                <asp:DropDownList ID="cbuom" CssClass="form-control" runat="server"></asp:DropDownList>                      
                            </td>
                            <td>
                                <asp:Button ID="btadd" CssClass="btn btn-primary" runat="server" OnClick="btadd_Click" Text="Add" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="form-group">
                <div class="col-sm-12">
                    <asp:GridView ID="grditem" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowCreated="grditem_RowCreated" OnRowDeleting="grditem_RowDeleting" OnRowCancelingEdit="grditem_RowCancelingEdit" OnRowEditing="grditem_RowEditing" OnRowUpdating="grditem_RowUpdating" ShowFooter="True" CellPadding="0">
                        <Columns>
                            <asp:TemplateField HeaderText="GDN No.">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdids" runat="server" Value='<%#Eval("ids") %>' />
                                    <asp:Label ID="lbstdref" runat="server" Text='<%#Eval("std_ref") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <%#Eval("item_cd") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <%#Eval("item_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbqty" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txqty" Text='<%#Eval("qty") %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>                            
                                <FooterTemplate>
                                    <asp:Label ID="lbtotqty" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Uom">
                                <ItemTemplate>
                                    <%#Eval("uom") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Change Qty" ShowEditButton="True" />
                            <asp:CommandField ShowDeleteButton="True" />                        
                        </Columns>
                        <FooterStyle BackColor="Yellow" />
                    </asp:GridView>
                </div>
            </div>
        </div>

        </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dtstd" EventName="TextChanged" />
            </Triggers>
        </asp:UpdatePanel>

    </div>

</asp:Content>

