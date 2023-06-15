<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmTranStockAdj.aspx.cs" Inherits="frmTranStockAdj" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_StockAdj.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function Selecteditem_cd_fr(sender, e) {
            $get('<%=hditem_cd_fr.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    <script>
        function Selecteditem_cd_to(sender, e) {
            $get('<%=hditem_cd_to.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
      <script>
          function ShowProgress() {
              $('#pnlmsg').show();
          }

          function HideProgress() {
              $("#pnlmsg").hide();
              return false;
          }
      </script>
    <style type="text/css">
        .divmsg {
            /*position:static;*/
            top: 7%;
            right: 36%;
            left: 55%;
            width: 200px;
            height: 200px;
            position: fixed;
            opacity: 0.9;
            overflow-y: auto;
            /*-webkit-transition: background-color 0;
    transition: background-color 0;*/
        }

        .divhid {
            display: none;
        }

        .divnormal {
            display: normal;
        }

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #FFFFFF;
            border-width: 3px;
            border-style: solid;
            border-color: black;
            padding-top: 10px;
            padding-left: 10px;
            width: 500px;
            height: 200px;
            font-family: Calibri;
            font-size: small;
        }
    </style> 
   
         <style>
             .switch {
                 position: relative;
                 display: inline-block;
                 width: 70px;
                 height: 35px;
             }

                 /* Hide default HTML checkbox */
                 .switch input {
                     display: none;
                 }

             /* The slider */
             .slider {
                 position: absolute;
                 cursor: pointer;
                 top: 0;
                 left: 0;
                 right: 0;
                 bottom: 0;
                 background-color: #ccc;
                 -webkit-transition: .4s;
                 transition: .4s;
             }

                 .slider:before {
                     position: absolute;
                     content: "";
                     height: 34px;
                     width: 34px;
                     left: 3px;
                     bottom: 1px;
                     background-color: white;
                     -webkit-transition: .4s;
                     transition: .4s;
                 }

             input:checked + .slider {
                 background-color: #2196F3;
             }

             input:focus + .slider {
                 box-shadow: 0 0 1px #2196F3;
             }

             input:checked + .slider:before {
                 -webkit-transform: translateX(32px);
                 -ms-transform: translateX(32px);
                 transform: translateX(32px);
             }

             /* Rounded sliders */
             .slider.round {
                 border-radius: 25px;
             }

                 .slider.round:before {
                     border-radius: 50%;
                 }
         </style> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                Stock Adjusment Entry
                <asp:Panel runat="server" ID="lbstatusPnl"><asp:Label ID="lbstatus" runat="server" CssClass="badge danger"></asp:Label></asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix col-sm-6 form-group"">
                <label class="control-label col-sm-2">Adj No.</label>
                <div class="col-sm-10 ">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" Class="input-group">
                        <ContentTemplate>
                            <asp:TextBox ID="txstkadjno" runat="server" CssClass="makeitreadonly ro form-control" ReadOnly="True" Enabled="false"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClientClick="openwindow();return(false);" />
                            </div>
                        </ContentTemplate>
                        <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                    </asp:UpdatePanel>
                    <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                </div>
            </div>
            <div class="clearfix col-sm-6 form-group">
                <label class="control-label col-sm-2">Sales Point Cd</label>
                <div class="col-sm-10 drop-down">
                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="makeitreadonly ro form-control" Enabled="False">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>
            <div class="clearfix col-sm-6 form-group"">
                <label class="control-label col-sm-2">Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Panel runat="server" ID="dtstkadjdatePnl">
                                <asp:TextBox ID="dtstkadjdate" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="dtstkadjdate_CalendarExtender" runat="server" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TargetControlID="dtstkadjdate" TodaysDateFormat="d/M/yyyy">
                                </asp:CalendarExtender>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>
            <div class="clearfix col-sm-6 form-group"">
                <label class="control-label col-sm-2">Whs Cd</label>
                <div class="col-sm-10 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbwhs_cd" runat="server" AutoPostBack="True" CssClass="form-control">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                     
                </div>
            </div>
            <div class="clearfix col-sm-6 form-group"">
                <label class="control-label col-sm-2">M No.</label>
                <div class="col-sm-10 ">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txstkadjmanno" runat="server" CssClass="form-control"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </div>
            </div>
            <div class="clearfix col-sm-6 form-group"">
                <label class="control-label col-sm-2">Prev Stock</label>
                <div class="col-sm-10 ">
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <label class="switch">
                                <asp:CheckBox ID="chprevstk" runat="server" AutoPostBack="true" OnCheckedChanged="chprevstk_CheckedChanged" />
                                <div class="slider round"></div>
                            </label>
                       </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
        
        <div class="row padding-top ">
            <div class="overflow-x">
                <table class="table table-striped mygrid row-no-padding table-title-sm">
                    <tr >
                        <th>Item Name Decrease (-)</th>
                        <th>BIN From</th>
                        <th>Prod grp</th>
                        <th>Pckg</th>
                        <th>Price</th>
                        <th>Item Name (Increase (+)</th>
                        <th>BIN To</th>
                        <th>Prod grp</th>
                        <th>Packing</th>
                        <th>Price</th>
                        <th>Qty</th>
                        <th>UOM</th>
                        <th>Reason</th>
                        <th>Action</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txitem_cd_frPnl">
                                        <asp:TextBox ID="txitem_cd_fr" runat="server" AutoPostBack="True" OnTextChanged="txitem_cd_fr_TextChanged" CssClass="form-control input-sm" Width="200px"></asp:TextBox>
                                        <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list auto-complate-sm" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txitem_cd_fr_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="Selecteditem_cd_fr" ServiceMethod="GetCompletionList" TargetControlID="txitem_cd_fr" UseContextKey="True">
                                        </asp:AutoCompleteExtender>
                                        <asp:HiddenField ID="hditem_cd_fr" runat="server" />
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                       </td> 
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="cbbin_cd_frPnl">
                                        <asp:DropDownList ID="cbbin_cd_fr" runat="server" Width="100px" AutoPostBack="True" CssClass="form-control input-sm" >
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txprod_cd_frPnl">
                                        <asp:TextBox ID="txprod_cd_fr" runat="server" Width="50px" CssClass="makeitreadonly ro form-control input-sm" Enabled="False" ></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txpacking_frPnl">
                                        <asp:TextBox ID="txpacking_fr" runat="server" Width="50px"  CssClass="makeitreadonly ro form-control input-sm" Enabled="False"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txunitprice_frPnl">
                                        <asp:TextBox ID="txunitprice_fr" runat="server" Width="50px"  CssClass="makeitreadonly ro form-control input-sm" Enabled="False"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txitem_cd_to" runat="server" AutoPostBack="True"  Width="200px" OnTextChanged="txitem_cd_to_TextChanged" CssClass="form-control input-sm" ></asp:TextBox>
                                    <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list auto-complate-sm" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txitem_cd_to_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="Selecteditem_cd_to" ServiceMethod="GetCompletionListitem_cd_to" TargetControlID="txitem_cd_to" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                    <asp:HiddenField ID="hditem_cd_to" runat="server" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="cbbin_cd_toPnl">
                                        <asp:DropDownList ID="cbbin_cd_to" runat="server" AutoPostBack="True" Width="100px"  CssClass="form-control input-sm" >
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txprod_cd_toPnl">
                                        <asp:TextBox ID="txprod_cd_to" runat="server" Width="50px" CssClass="makeitreadonly ro form-control input-sm" Enabled="False"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txpacking_toPnl">
                                        <asp:TextBox ID="txpacking_to" runat="server" Width="50px" CssClass="makeitreadonly ro form-control input-sm" Enabled="False"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txunitprice_toPnl">
                                        <asp:TextBox ID="txunitprice_to" runat="server" Width="50px" CssClass="makeitreadonly ro form-control input-sm" Enabled="False"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txqtyPnl">
                                        <asp:TextBox ID="txqty" runat="server" Width="80px" CssClass="form-control input-sm" ></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="cbuomPnl">
                                        <asp:DropDownList ID="cbuom" runat="server" Width="100px"  CssClass="form-control input-sm" >
                                        </asp:DropDownList>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txreason" runat="server" Width="150px" CssClass="form-control input-sm"></asp:TextBox>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                                <asp:Button ID="btadd" OnClientClick="ShowProgress();" runat="server" CssClass="btn-success btn btn-sm btn-add" OnClick="btadd_Click"  Text="Add" />
                        </td>
                    </tr>
                </table>
            </div>
            
        </div>
    </div>

    
    
    <div class="container-fluid top-devider">
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel16" runat="server" class="overflow-x">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowDataBound="grd_RowDataBound" ShowFooter="True" CssClass="table table-hover table-striped mygrid">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitem_cd_fr" runat="server" Text='<%# Eval("item_cd_fr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BIN">
                            <ItemTemplate>
                                <asp:Label ID="lbBIN_nm_to" runat="server" Text='<%# Eval("BIN_nm_to") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                              <%# Eval("item_nm_fr") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P Group">
                            <ItemTemplate>
                              <%# Eval("prod_nm_fr") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pack">
                            <ItemTemplate>
                              <%# Eval("packing_fr") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="U Price">
                            <ItemTemplate>
                              <%# Eval("unitprice_fr") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText=" | " ItemStyle-ForeColor="Red" ControlStyle-Font-Bold="true">
                            <ItemTemplate>
                               <div  style="color: red;font:bold;" >
                              <asp:Label  Text=" | " runat="server" />
                                </div>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitem_cd_to" runat="server" Text='<%# Eval("item_cd_to") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="BIN">
                            <ItemTemplate>
                                <asp:Label ID="lbBIN_nm_fr" runat="server" Text='<%# Eval("BIN_nm_fr") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                              <%# Eval("item_nm_To") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="P Group">
                            <ItemTemplate>
                              <%# Eval("prod_nm_to") %>
                            </ItemTemplate>
                            <FooterTemplate>
				            <div style="text-align: right;">
				            <asp:Label ID="lbltotal" Text="Total : " runat="server" />
				            </div>
			                </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pack">
                            <ItemTemplate>
                              <%# Eval("packing_to") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Uom">
                            <ItemTemplate>
                              <%# Eval("uom") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty">
                            <ItemTemplate>
                               <asp:Label ID="lblqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                             </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("qty") %>' CssClass="form-control input-sm" type="number"></asp:TextBox>
                            </EditItemTemplate>
                        <FooterTemplate>
				        <div style="text-align: right;">
				        <asp:Label ID="lblTotalqty" runat="server" />
				        </div>
			            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="U Price">
                            <ItemTemplate>
                              <%# Eval("unitprice_to") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Amount">
                            <ItemTemplate>
                               <asp:Label ID="lblAmount" runat="server" Text='<%# Eval("Amount","{0:n2}") %>'></asp:Label>
                             </ItemTemplate>
                        <FooterTemplate>
				        <div style="text-align: right;">
				        <asp:Label ID="lblTotalAmount" runat="server" />
				        </div>
			            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate>
                              <%# Eval("reason") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblstkadjno" runat="server" Text='<%# Eval("stkadjno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" ShowEditButton="True" />
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
            </ContentTemplate>
        </asp:UpdatePanel>
        </div> 
    </div>
    
    <div class="container-fluid">
        <div class="navi row margin-bottom">
            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                <ContentTemplate>
                    <asp:LinkButton ID="btnew" OnClientClick="ShowProgress();" runat="server" CssClass="btn-success btn btn-sm" OnClick="btnew_Click" Text="NEW" />
                    <asp:LinkButton ID="btsave" runat="server" OnClientClick="ShowProgress();" CssClass="btn-warning btn btn-sm" OnClick="btsave_Click" Text="Save" />
                    <asp:LinkButton ID="btprint" runat="server" CssClass="btn-info btn btn-sm" OnClientClick="ShowProgress();" OnClick="btprint_Click" Text="Print" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
  
 <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

