<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_distributionplan.aspx.cs" Inherits="fm_distributionplan" %>

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
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hditem" runat="server" />
    <div class="container">
        <h4 class="jajarangenjang">Distribution Plan</h4>
        <div class="h-divider"></div>

        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label class="control-label col-sm-2">Batch Seq#</label>
                    <div class="col-sm-10">
                        <div class="input-group">
                        <asp:Label ID="lbppno" CssClass="form-control input-group-sm ro" runat="server" Text=""></asp:Label>
                        <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-sm-6 ">
                <div class="form-group">
                <label class="control-label col-sm-2">Date</label>
                <div class="col-sm-10">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:TextBox ID="dtplan" CssClass="form-control" runat="server" Enabled="false"></asp:TextBox>
                            <asp:CalendarExtender ID="dtplan_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtplan">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>                         
                </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label class="control-label col-sm-2">Delivery</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:TextBox ID="dtdelivery" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dtdelivery_CalendarExtender" runat="server" Format="d/MM/yyyy" TargetControlID="dtdelivery" TodaysDateFormat="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                        </asp:UpdatePanel>   
                    </div>
                </div>
            </div>
        </div>

        <h4 class="jajarangenjang">Summary PO</h4>
        <div class="row">
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grdpo" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1%>.</ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespoint") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_desc") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbqty" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                                    <asp:Label ID="lbuom" runat="server" Text='<%#Eval("uom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
        <div class="h-divider"></div>

        <div class="row">    
            <div class="form-group">
                <div class="col-md-12">
                    <table class="mGrid">
                        <tr>
                            <th>Branch</th>
                            <th>Item</th>
                            <th>Uom</th>
                            <th>Qty Received</th>
                            <th>Add</th>
                        </tr>
                        <tr>
                            <td class="drop-down">
                                <asp:DropDownList ID="cbsalespoint" runat="server"  CssClass="form-control input-sm" >
                                </asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txitem" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" ServiceMethod="GetListItem" TargetControlID="txitem" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionInterval="1" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:DropDownList ID="cbuom" runat="server"  CssClass="form-control input-sm" >
                                    <asp:ListItem Value="CTN">CTN</asp:ListItem>
                                </asp:DropDownList> 
                            </td>
                            <td>
                                <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </td>
                            <td>
                                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btadd_Click"  />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grdplan" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdplan_RowDeleting" ShowFooter="True" CellPadding="0">
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate><%# Container.DataItemIndex + 1%>.</ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespoint_cd") %>'></asp:Label>
                                    - 
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("salespoint_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemnm" runat="server" Text='<%#Eval("item_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbqty" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                                    <asp:Label ID="lbuom" runat="server" Text='<%#Eval("uom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <FooterStyle BackColor="Yellow" />
                    </asp:GridView>
                </div>
            </div>
        </div>



        <%--<div class="row">
            <div class="col-sm-12">
                <div class="form-group">  
                <div class="col-sm-2">
                </div>
                <div class="col-sm-4">
                    
                </div>
                <div class="col-sm-2">
                    
                </div>
                <div class="col-sm-2">
                    
                </div>
                <div class="col-sm-2">
                    
                </div>
            </div>
            </div>
        </div>--%>
        <%--<div class="row">
            <div class="col-sm-12">
                <asp:GridView ID="grdplan" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdplan_RowDeleting" ShowFooter="True" CellPadding="0">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1%>.</ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch">
                            <ItemTemplate>
                                <asp:Label ID="lbsalespoint" runat="server" Text='<%#Eval("salespoint") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <asp:Label ID="lbitemnm" runat="server" Text='<%#Eval("item_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                                <asp:Label ID="lbqty" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                                <asp:Label ID="lbuom" runat="server" Text='<%#Eval("uom") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                    <FooterStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
        </div>--%>

        <div class="h-divider"></div>
            <div class="navi margin-bottom">
                <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-default btn btn-add" OnClick="btnew_Click"/>
                <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-success btn btn-add" OnClick="btsave_Click"/>
            </div>

        <div class="h-divider"></div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdori" CssClass="mGrid" runat="server" OnRowCreated="grdori_RowCreated" AutoGenerateColumns="False" ShowFooter="True" CellPadding="0">
                        </asp:GridView>
                        <asp:GridView ID="grdpivot" CssClass="mGrid" runat="server" AutoGenerateColumns="False" ShowFooter="True" CellPadding="0">
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
            </div>
        </div>

    </div>


</asp:Content>

