<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_do.aspx.cs" Inherits="fm_do" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookuppo.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function ItemSelected(sender, e)
        {
            $get('<%=hdpo.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function Item2Selected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=btcheckstock.ClientID%>').click();
        }
    </script>
    
         
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">DELIVERY ORDER</div>
    <div class="h-divider"></div>
    
    <div class="container-fluid">
        <div class="row">
            <div class="well well-sm no-margin clearfix">
                <div class="col-sm-4 no-padding-left">
                    <label class="col-sm-4 control-label titik-dua">DO Number</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                 <asp:TextBox ID="txdono" runat="server" CssClass="ro form-control input-sm" Enabled="false"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <div class="col-sm-4 no-padding">
                    <label class="col-sm-4 control-label titik-dua"> DO Status</label>
                    <div class="col-sm-8">
                        <asp:Label ID="lbdosta" runat="server" CssClass="text-white badge badge-lg danger" Text="NEW"></asp:Label>      
                    </div>
                </div>

                <div class="col-sm-4 no-padding-right">
                    <label class="col-sm-4 control-label titik-dua">System Date</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                        <ContentTemplate>
                        <asp:TextBox ID="dtdo" runat="server" CssClass="makeitreadonly ro form-control input-sm" Enabled="false"></asp:TextBox>
                        <asp:CalendarExtender CssClass="date" ID="dtdo_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtdo">
                        </asp:CalendarExtender>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>

            <div class="clearfix margin-top margin-bottom">
                <div class="col-sm-8">
                    <label class="col-sm-4 col-md-2 control-label">Salespoint</label>
                    <div class="col-sm-8">
                        <asp:Panel runat="server" ID="Panel1">
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server" class="drop-down">
                            <ContentTemplate>
                            <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div class="col-sm-4">
                    <label class="col-sm-4 control-label titik-dua">GDN Date</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="dtgdn" runat="server" CssClass="form-control input-sm" Enabled="true" ></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="dtgdn_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtgdn"></asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div class="clearfix margin-top margin-bottom">
                <div class="col-sm-8">
                    <label class="col-sm-4 col-md-2 control-label">PO Number</label>
                    <div class="col-sm-8">
                        <asp:Panel runat="server" ID="txpoPnl">
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                            <ContentTemplate>
                            <asp:TextBox ID="txpo" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:Button ID="btsearch" runat="server" OnClick="btsearch_Click" Text="Search" style="left: 0px; top: 0px;display:none"/>
                            <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txpo_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txpo" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="ItemSelected" MinimumPrefixLength="1" CompletionListElementID="divwidth">
                            </asp:AutoCompleteExtender>
                            <asp:HiddenField ID="hdpo" runat="server" />
                            <div id="divwidth" style="position:relative"></div>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </asp:Panel>
                    </div>
                </div>
                <div class="col-sm-4">
                    <label> PO Status</label>
                    <div>
                         <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbstatus" runat="server" CssClass="text-bold text-white badge badge-lg danger" ></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <div class="clearfix">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="col-sm-12 well well-sm ">
                    <ContentTemplate>
                        <asp:Label ID="Label1" runat="server" Text="TO : " CssClass="control-label padding-right"></asp:Label> - 
                        <asp:Label ID="lbtoname" runat="server" CssClass="text-bold text-red padding-top-4 padding-right"></asp:Label> - 
                        <asp:Label ID="lbtoaddress" runat="server" CssClass="text-bold text-primary padding-top-4 padding-right"></asp:Label>
                        <label class="control-label padding-right">: SALESPOINT :</label>
                        <asp:Label ID="lbsalespoint" runat="server" CssClass="text-bold text-primary padding-right"></asp:Label>
                        <asp:HiddenField ID="hdsp" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="clearfix">
                <div class="clearfix form-group">
                    <label class="control-label titik-dua col-sm-4">Delivery From warehouse </label>
                    <div class="col-sm-6 drop-down ">
                        <asp:DropDownList ID="cbwarehouse" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbwarehouse_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="col-sm-2">
                        <asp:Button ID="btprint" runat="server" Text="Print PO" CssClass="btn-info btn btn-print" OnClick="btprint_Click"  />
                    </div>
                </div>
            
                <div class="clearfix form-group">
                    <label class="control-label titik-dua col-sm-4">Manual Invoice </label>
                    <div class="col-sm-6  ">
                        <asp:TextBox ID="txmanualinvoice" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                             <asp:Label ID="lbcap" runat="server" CssClass="control-label titik-dua col-sm-4"></asp:Label>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbwarehouse" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                    <div class="col-sm-6 ">
                         <asp:TextBox ID="txgdn" runat="server" CssClass="form-control"></asp:TextBox>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <label class="control-label titik-dua col-sm-4">Expedition Type</label>
                    <div class="col-sm-6 drop-down ">
                        <asp:DropDownList ID="cbexpedition" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbexpedition_SelectedIndexChanged">
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <label class="control-label titik-dua col-sm-4">Expedition Company</label>
                    <div class="col-sm-6 drop-down ">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbcompany" runat="server" CssClass="form-control"></asp:DropDownList>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbexpedition" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <label class="control-label titik-dua col-sm-4">Driver Name</label>
                    <div class="col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="cbdriverPanel" CssClass="drop-down">
                                    <asp:DropDownList ID="cbdriver" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbdriver_SelectedIndexChanged" AutoPostBack="True">
                                    </asp:DropDownList>
                                </asp:Panel>
                                 
                                <asp:TextBox ID="txdrivername" runat="server" CssClass="form-control"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbexpedition" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <label class="control-label titik-dua col-sm-4">Trella/Truck/Van No.</label>
                    <div class="col-sm-6">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbtrella" runat="server" CssClass="form-control"></asp:DropDownList>
                                <asp:TextBox ID="txtrella" runat="server" CssClass="form-control"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbexpedition" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
                <div class="clearfix form-group">
                    <label class="control-label titik-dua col-sm-4">Trailer Box</label>
                    <div class="col-sm-6 drop-down ">
                        <asp:DropDownList ID="cbbox" runat="server" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
            </div>

            <table class="table mygrid">
                <tr>
                    <th>Item</th>
                    <th>Qty Ordered</th>
                    <th>Stock</th>
                    <th>Add</th>
                </tr>
                <tr>
                    <td style="position:relative">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="txitemsearchPnl">
                                    <asp:TextBox ID="txitemsearch" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                    <div id="divwidthi"></div>
                                    <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txitemsearch" UseContextKey="True" MinimumPrefixLength="1" CompletionSetCount="1" CompletionInterval="10" EnableCaching="false" FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true" CompletionListElementID="divwidthi" OnClientItemSelected="Item2Selected">
                                    </asp:AutoCompleteExtender>
                                    <asp:HiddenField ID="hditem" runat="server" />
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
               
                    </td>
                    <td>
                        <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                        <asp:Button ID="btcheckstock" runat="server" OnClick="btcheckstock_Click" Text="Button" style="display:none"/>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbstock" runat="server" CssClass="text-red"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btcheckstock" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                
                    </td>
                    <td>
                        <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-success btn-sm btn btn-add" OnClick="btadd_Click" />
                    </td>
                </tr>
            </table>

            <div class="divgrid">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>

                                <asp:GridView ID="grdpodtl" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" OnRowDataBound="grdpodtl_RowDataBound" CssClass="table table-striped mygrid" ShowFooter="True">
                                    <AlternatingRowStyle />
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <%# Container.DataItemIndex+1 %>.
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Item Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbitemname" runat="server" Text='<%# Eval("item_nm") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Size">
                                            <ItemTemplate>
                                                <%# Eval("size") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Branded">
                                            <ItemTemplate>
                                                <%# Eval("branded_nm") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Stock CWH">
                                            <ItemTemplate>
                                                <asp:Label ID="lbstock" runat="server" Font-Bold="True" ForeColor="#0033CC" Text=' <%# Eval("stock_amt","{0:#}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Ordered">
                                            <ItemTemplate>
                                                <div style="text-align: right;">
                                                <asp:Label ID="lbqty" runat="server" style="font-weight:bold;color:blue" Text='<%# Eval("qty_order") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
				                                <div style="text-align: right;">
				                                <asp:Label ID="lblTotalqty" runat="server" />
				                                </div>
			                                </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM Ordered">
                                            <ItemTemplate>
                                                <div style="text-align: right;">
                                                <asp:Label ID="lbuom" runat="server" style="font-weight:bold;color:blue" Text='<%# Eval("uom") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delivered">
                                            <ItemTemplate>
                                                <div style="text-align: right;">
                                                <asp:Label ID="lbqtydeliver" runat="server" Text='<%# Eval("qty_deliver") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
				                                <div style="text-align: right;">
				                                <asp:Label ID="lblTotalqtydeliver" runat="server" />
				                                </div>
			                                </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Balanced">
                                            <ItemTemplate>
                                                <asp:Label ID="lbbalance" runat="server" Font-Bold="True"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price">
                                            <ItemTemplate>
                                                <div style="text-align: right;">
                                                <asp:Label ID="lbunitprice" runat="server" Text='<%# Eval("price_sell") %>'></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Subtotal">
                                            <ItemTemplate>
                                                <div style="text-align: right;">
                                                <asp:Label ID="lbsubtotal" runat="server"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <FooterTemplate>
				                                <div style="text-align: right;">
				                                <asp:Label ID="lblTotalsubtotal" runat="server" />
				                                </div>
			                                </FooterTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Shipment" ControlStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txdeliver" runat="server" style="background-color:#ffd800"  CssClass="form-control input-sm"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM Shipment" ControlStyle-Width="80">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control input-sm" style="background-color:#ffd800">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle CssClass="table-edit" />
                                    <FooterStyle CssClass="table-footer" />
                                    <HeaderStyle CssClass="table-header" />
                                    <PagerStyle CssClass="table-page" />
                                    <RowStyle />
                                    <SelectedRowStyle CssClass="table-edit" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>

                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btsave" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>


            <div class="navi margin-bottom margin-top">
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                         <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn-success btn btn-add" OnClick="btnew_Click"/>
                        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" OnClick="btsave_Click" />
                        <asp:Button ID="btprintdo" runat="server" Text="Print DO" cssclass="btn-info btn btn-print" OnClick="btprintdo_Click2" />
                        <asp:Button ID="btprintinvoice" runat="server" Text="Print Invoice" cssclass="btn-info btn btn-print" OnClick="btprintinvoice_Click"/>
                    </ContentTemplate>
                </asp:UpdatePanel>
        
            </div>

        </div>
    </div>

 
    
  

</asp:Content>

