<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_goodreceipt_ho.aspx.cs" Inherits="fm_goodreceipt_ho" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookupdo.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
            return (false);
        }
        function ItemSelected(sender, e)
        {
            $get('<%=hddo.ClientID%>').value = e.get_value();
        }
    </script>
    <script type="text/javascript">
        <%--$("#btsearch").live("click", function () {
            var do_header = {};
            //do_header.do_no = $get('<%=txgoodreceipt.ClientID%>').value;
            $.ajax({
                type: 'GET',
                url: 'https://nav.transworld.com.eg/api/nav_do/header.php?do_no=' + $get('<%=txgoodreceipt.ClientID%>').value,
                data: "{" + JSON.stringify(do_header) + "}",
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (r) {
                    alert(r.d.DO_No);
                    alert(r.d.Order_Date);
                }
            });
        });--%>

        function BindData() {
            var do_no = $('#' + '<%= txdosearch.ClientID%>').val();
            var custUrl = 'https://nav.transworld.com.eg/api/nav_do/header.php?do_no=' + do_no;
            var data_h;

            //$.ajax({
            //    type: 'GET',
            //    url: custUrl,
            //    contentType: 'application/json; charset=utf-8',
            //    dataType: 'json',
            //    success: function (r) {
            //        alert(r.d.DO_No);
            //        alert(r.d.Order_Date);
            //    }
            //});

            $.ajax({

                url: custUrl,
                type: 'GET',
                dataType: 'json',
                success: function (data, textStatus, xhr) {

                    $.each(data, function (index, result) {
                        console.log(result.DO_No);
                        alert(result.DO_No)
                    });

                    $("#pnlmsg").hide();
                    sweetAlert('Successfully', 'DO No Available : ' + data, 'success');
                    $('#dvsync').html(status);
                },
                error: function (xhr, textStatus, errorThrown) {
                    $("#pnlmsg").hide();
                    alert("Search DO Failed, Check Error");
                    console.log('Error in Operation');

                }
            });

            
        }
    </script>
  
      
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Good Received - Branches</div>
    <div class="h-divider"></div>
    <div class="divgrid container-fluid">
        <div class="row clearfix margin-bottom">
            <div class="clearfix col-md-8 col-sm-12 no-padding">
                <div class="col-sm-6 no-padding">
                    <label class="titik-dua control-label control-label-sm col-sm-4">Good Receipt No.</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txgoodreceipt" runat="server" CssClass="form-control input-sm ro" Enabled="false"></asp:TextBox>
                    </div>
                </div>
                <div class="col-sm-6  no-padding">
                    <label class="titik-dua control-label control-label-sm col-sm-4">Date</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="dtgoodreceipt" runat="server" CssClass="form-control input-sm ro" Enabled="False" ReadOnly="True"></asp:TextBox>
                        <asp:CalendarExtender ID="dtgoodreceipt_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtgoodreceipt" TodaysDateFormat="d/M/yyyy">
                        </asp:CalendarExtender>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="h-divider"></div>
        </div>
        <div class="row clearfix ">
            <div class="clearfix margin-bottom">
                <div class=" col-sm-6 no-padding">
                    <label class="titik-dua control-label col-sm-4">Search DO</label>
                    <div class="col-sm-8">
                        <div class="input-group">
                            <asp:TextBox ID="txdosearch" runat="server" CssClass="form-control"></asp:TextBox>
                            <div id="divwidth"></div>
                            <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txdosearch_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txdosearch" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="ItemSelected" MinimumPrefixLength="1" CompletionListElementID="divwidth"> 
                            </asp:AutoCompleteExtender>
                            <div class="input-group-btn">
                                <asp:Button ID="btsearch" runat="server" CssClass="btn btn-primary btn-search" Text="search" OnClientClick="BindData();"  />
                            </div>
                            <asp:HiddenField ID="hddo" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="clearfix col-sm-6  no-padding">
                    <label class="titik-dua control-label col-sm-4">Date</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control input-sm ro" Enabled="False" ReadOnly="True"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="d/M/yyyy" TargetControlID="dtgoodreceipt" TodaysDateFormat="d/M/yyyy">
                        </asp:CalendarExtender>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="clearfix col-sm-6  no-padding">
                    <label class="titik-dua control-label col-sm-4">DO Date</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                  <asp:Label ID="lbdodate" runat="server" CssClass="padding-top-4 block text-red text-bold"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="clearfix col-sm-6  no-padding">
                    <label class="titik-dua control-label col-sm-4">Invoice No.</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Label ID="lbinvoiceno" runat="server" CssClass="padding-top-4 block text-red text-bold"></asp:Label>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="clearfix col-sm-6  no-padding">
                    <label class="titik-dua control-label col-sm-4">Status DO</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                 <asp:Label ID="lbdostaid" runat="server" CssClass="padding-top-4 block text-red text-bold"></asp:Label>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="clearfix col-sm-6  no-padding">
                    <label class="titik-dua control-label col-sm-4">Delivery From</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txfrom" runat="server" CssClass="form-control ro" Enabled="False"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="clearfix col-sm-6  no-padding">
                    <label class="titik-dua control-label col-sm-4">Send To Warehouse</label>
                    <div class="col-sm-8 drop-down">
                        <asp:DropDownList ID="cbwarehouse" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbwarehouse_SelectedIndexChanged" AutoPostBack="true">
                        </asp:DropDownList>
                    </div>
                </div>
            </div>
            <div class="clearfix margin-bottom">
                <div class="clearfix col-sm-6  no-padding">
                    <label class="titik-dua control-label col-sm-4">GDN</label>
                    <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txgdn" runat="server" CssClass="form-control ro" Enabled="False"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
                            </Triggers>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
        
     </div>
   
    <div class="h-divider"></div>

    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                 <%--<asp:GridView ID="grd" CssClass="table table-striped mygrid" runat="server" AutoGenerateColumns="False" GridLines="None" CellPadding="0" OnRowDataBound="grd_RowDataBound" ShowFooter="True">
                     <AlternatingRowStyle  />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate><%# Eval("item_nm")%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate><%# Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Shipment">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                <asp:Label ID="lbqtyshipment" runat="server" Text='<%# Eval("qty")%>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
				                <div style="text-align: right;">
				                <asp:Label ID="lbltotalqtyshipment" runat="server" />
				                </div>
			                </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM Shipment">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                <asp:Label ID="lbuom" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Received">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                <asp:TextBox ID="txreceived" runat="server" Width="50"></asp:TextBox>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
				                <div style="text-align: right;">
				                <asp:Label ID="lbltotalqtyreceived" runat="server" />
				                </div>
			                </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM Received" ControlStyle-Width="80">
                            <ItemTemplate>
                                <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control input-sm" style="background-color:#ffd800">
                                </asp:DropDownList>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bin Destination">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbdestination" runat="server" Width="150px"></asp:DropDownList>
                                    </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbwarehouse" EventName="SelectedIndexChanged" />
                                </Triggers>
                                </asp:UpdatePanel>     
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Production Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtproddate" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="txtproddate_CalendarExtender" runat="server" Format="d/MM/yyyy" TargetControlID="txtproddate">
                            </asp:CalendarExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expire Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtExpDate" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="txtExpDate_CalendarExtender" runat="server" Format="d/MM/yyyy" TargetControlID="txtExpDate">
                            </asp:CalendarExtender>
                        </ItemTemplate>
                    </asp:TemplateField>                        
                    </Columns>
                     <EditRowStyle CssClass="table-edit" />
                     <FooterStyle CssClass="table-footer" />
                     <HeaderStyle CssClass="table-header" />
                     <PagerStyle CssClass="table-page"/>
                     <RowStyle  />
                     <SelectedRowStyle CssClass="table-edit" />
                     <SortedAscendingCellStyle BackColor="#E9E7E2" />
                     <SortedAscendingHeaderStyle BackColor="#506C8C" />
                     <SortedDescendingCellStyle BackColor="#FFFDF8" />
                     <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
        </asp:GridView>--%>
                <asp:GridView ID="grd" CssClass="table table-striped mygrid" runat="server" AutoGenerateColumns="False" GridLines="None" CellPadding="0" OnRowDataBound="grd_RowDataBound" ShowFooter="True">
                     <AlternatingRowStyle  />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate><%# Eval("item_nm")%></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate><%# Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Shipment">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lbqtyshipment" runat="server" Text='<%# Eval("qty_conv")%>'></asp:Label>
                                    <asp:HiddenField ID="hdqtyshipment" runat="server" Value='<%#Eval("qty") %>'></asp:HiddenField>
                                    <asp:HiddenField ID="hduomshipment" runat="server" Value='<%#Eval("uom") %>'></asp:HiddenField>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
				                <div style="text-align: right;">
				                <asp:Label ID="lbltotalqtyshipment" runat="server" />
				                </div>
			                </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Received">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdqtyreceived" runat="server" Value='<%#Eval("qty") %>'></asp:HiddenField>
                                <asp:HiddenField ID="hdqtyreceived_conv" runat="server" Value='<%#Eval("qty_conv") %>'></asp:HiddenField>
                                <asp:TextBox ID="txqty_ctn" Text='<%#Eval("qty_ctn") %>' runat="server" TextMode="Number" Width="20%"></asp:TextBox>
                                    <asp:DropDownList runat="server" ID="cbuom_ctn" style="background-color:#ffd800"></asp:DropDownList>
                                    <asp:TextBox ID="txqty_pcs" Text='<%#Eval("qty_pcs") %>' runat="server" TextMode="Number" Width="20%"></asp:TextBox>
                                <asp:DropDownList runat="server" ID="cbuom_pcs" style="background-color:#ffd800"></asp:DropDownList>
                            </ItemTemplate>
                            <FooterTemplate>
				                <div style="text-align: right;">
				                <asp:Label ID="lbltotalqtyreceived" runat="server" />
				                </div>
			                </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bin Destination">
                            <ItemTemplate>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbdestination" runat="server" Width="150px"></asp:DropDownList>
                                    </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbwarehouse" EventName="SelectedIndexChanged" />
                                </Triggers>
                                </asp:UpdatePanel>     
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Production Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtproddate" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="txtproddate_CalendarExtender" runat="server" Format="d/MM/yyyy" TargetControlID="txtproddate">
                            </asp:CalendarExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expire Date">
                        <ItemTemplate>
                            <asp:TextBox ID="txtExpDate" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="txtExpDate_CalendarExtender" runat="server" Format="d/MM/yyyy" TargetControlID="txtExpDate">
                            </asp:CalendarExtender>
                        </ItemTemplate>
                    </asp:TemplateField>                        
                    </Columns>
                     <EditRowStyle CssClass="table-edit" />
                     <FooterStyle CssClass="table-footer" />
                     <HeaderStyle CssClass="table-header" />
                     <PagerStyle CssClass="table-page"/>
                     <RowStyle  />
                     <SelectedRowStyle CssClass="table-edit" />
                     <SortedAscendingCellStyle BackColor="#E9E7E2" />
                     <SortedAscendingHeaderStyle BackColor="#506C8C" />
                     <SortedDescendingCellStyle BackColor="#FFFDF8" />
                     <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
       
    </div>
    
    <div class="h-divider"></div>

    <div>
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grddtl" CssClass="table table-striped mygrid" runat="server" AutoGenerateColumns="False" CellPadding="0"  GridLines="None" OnRowDataBound="grddtl_RowDataBound" ShowFooter="True" >
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                        <%# Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lblitem_cd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <%# Eval("item_nm")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate>
                                <%# Eval("size") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Shipment">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lblqty" runat="server" Text='<%# Eval("qty")%>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lbltotalqty" runat="server" />
                                </div>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM Shipment">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lbuomshipment" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Received">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lblqty_received" runat="server" Width="50" Text='<%# Eval("qty_received")%>'></asp:Label>
                                </div>
                            </ItemTemplate>
                            <FooterTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lbltotalqty_received" runat="server" />
                                </div>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM Received">
                            <ItemTemplate>
                                <div style="text-align: right;">
                                    <asp:Label ID="lbuomreceived" runat="server" Text='<%# Eval("uom")%>'></asp:Label>
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Whs_cd">
                            <ItemTemplate>
                                <%# Eval("whs_cd") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bin_cd">
                            <ItemTemplate>
                                <%# Eval("Bin_cd") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expire Date">
                            <ItemTemplate>
                                <asp:Label ID="lbexpdate" runat="server" Text='<%# Eval("expire_date","{0:d/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Production Date">
                            <ItemTemplate>
                                <asp:Label ID="lbproddate" runat="server" Text='<%# Eval("prod_date","{0:d/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page"/>
                    <RowStyle  />
                    <SelectedRowStyle CssClass="table-edit" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="navi margin-bottom margin-top ">
        <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn btn-success btn-new" OnClick="btnew_Click" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-warning btn-save" OnClick="btsave_Click"/>
        <asp:Button ID="btclose" runat="server" CssClass="btn btn-danger btn-close" OnClick="btclose_Click" Text="Close" />
        <asp:Button ID="btprint" runat="server" Text="Print Good Receipt" CssClass="btn btn-info btn-print" OnClick="btprint_Click"/>
    </div>
</asp:Content>

