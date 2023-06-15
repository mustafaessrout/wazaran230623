<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_stockopnameentrycheck.aspx.cs" Inherits="fm_stockopnameentrycheck" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .divmsg{
       /*position:static;*/
       top:30%;
       right:50%;
       left:50%;
       width:200px;
       height:200px;
       position:fixed;
       /*background-color:greenyellow;*/
       overflow-y:auto;
                }
        .divhid {
            display:none;
        }

        .divnormal {
            display:normal;
        }
        .containers.bg-white{
            min-height:510px !important;
        }
    </style> 
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_stockOpnamecheck.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function ItemSelecteditem_cd(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Stock Opname For Check</div>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row ">
            <div class="clearfix">
                <label class="col-sm-2 control-label titik-dua">Stk Opnm No.</label>
                <div class="col-sm-4 margin-bottom">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" class="input-group">
                        <ContentTemplate>
                            <asp:TextBox ID="txstockno" runat="server" CssClass="makeitreadonly ro form-control" Enabled="false"></asp:TextBox>
                            <div class="input-group-btn">
                                <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClientClick="openwindow();return(false);" OnClick="btsearch_Click" />
                            </div>
                        </ContentTemplate>
                        <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                    </asp:UpdatePanel>
                    <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                </div>

                <label class="col-sm-2 control-label titik-dua">Warehouse Type</label>
                <div class="col-sm-4 margin-bottom">
                    <asp:UpdatePanel ID="UpdatePanel15" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbwhstype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbwhstype_SelectedIndexChanged" CssClass="form-control">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbwhstype" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>
                </div>

                <label class="col-sm-2 control-label titik-dua">Date</label>
                <div class="col-sm-4 margin-bottom">
                     <asp:UpdatePanel ID="UpdatePanel17" runat="server" class="drop-down-date">
                        <ContentTemplate>
                            <asp:CalendarExtender CssClass="date" ID="dtstocktopname_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstocktopname">
                            </asp:CalendarExtender>
                            <asp:TextBox ID="dtstocktopname" runat="server" CssClass="makeitreadonly form-control ro" Enabled="false"></asp:TextBox>
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <label class="col-sm-2 control-label titik-dua">Warehouse/Van</label>
                <div class="col-sm-4 margin-bottom">
                    <asp:UpdatePanel ID="UpdatePanel16" runat="server" class="drop-down">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbwhs" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged" >
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <label class="col-sm-2 control-label titik-dua">Bin</label>
                <div class="col-sm-4 margin-bottom clearfix">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                        <ContentTemplate>
                            <div class="drop-down col-sm-7 no-padding-left">
                                <asp:DropDownList ID="cbbinb" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbbin_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="col-sm-5 no-padding">
                                <asp:Button ID="btgenerate" runat="server" CssClass="btn-success btn-block btn add" OnClick="btgenerate_Click" Text="Create Form" />
                            </div>
                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="h-divider"></div>

            <table class="table mygrid margin-bottom">
                <tr style="background-color:silver">
                    <th>Item Name</th>
                    <th>Location</th>
                    <th>Qty System</th>
                    <th>Qty Actual</th>
                    <th>OUM</th>
                    <th>Expire Date</th>
                    <th>Bin</th>
                    <th>Reason</th>
                    <th>Action</th>
                </tr>
                <tr>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="txitem_cdPnl">
                                    <asp:TextBox ID="txitem_cd" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txitem_cd_TextChanged"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txitem_cd_AutoCompleteExtender" runat="server" TargetControlID="txitem_cd" ServiceMethod="GetCompletionListitem_cd" MinimumPrefixLength="1" 
                                EnableCaching="false" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelecteditem_cd" UseContextKey="True">
                                    </asp:AutoCompleteExtender>
                                     <asp:HiddenField ID="hditem" runat="server" />
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
            
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txlocation" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txqty_system" runat="server"   CssClass="makeitreadonly ro form-control input-sm" Enabled="False"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="txqty_actualPnl">
                                    <asp:TextBox ID="txqty_actual" runat="server" CssClass="form-control input-sm" type="number" min="0"></asp:TextBox>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td Width="100px">
                        <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                            <ContentTemplate>
                                <asp:Panel runat="server" ID="cbUOMPnl">
                                    <asp:DropDownList ID="cbUOM" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbUOM_SelectedIndexChanged" CssClass="form-control input-sm">
                                    </asp:DropDownList>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txexpire_dt" runat="server" CssClass="form-control input-sm" AutoPostBack="True" OnTextChanged="txexpire_dt_TextChanged"></asp:TextBox>
                                <asp:CalendarExtender ID="txexpire_dt_CalendarExtender" CssClass="date" runat="server" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TargetControlID="txexpire_dt" TodaysDateFormat="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="width:100px;">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbbin" runat="server" OnSelectedIndexChanged="cbbin_SelectedIndexChanged" CssClass="form-control input-sm" AutoPostBack="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txreason" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                            <ContentTemplate>
                                <strong>
                                <asp:Button ID="btadd" runat="server" CssClass="btn-success btn-sm btn btn-add" OnClick="btadd_Click"  Text="Add" />
                                </strong>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>



            <div class="divgrid">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnPageIndexChanging="grd_PageIndexChanging" DataKeyNames="seqID" OnRowDeleting="grd_RowDeleting" OnRowCancelingEdit="grd_RowCancelingEdit" CellPadding="0" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Brand">
                            <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate><%# Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="System Stock">
                            <ItemTemplate><%# Eval("qty_system") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Location">
                            <ItemTemplate><%# Eval("location") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtlocation" runat="server" Text='<%# Eval("location") %>' CssClass="form-control input-sm"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Actual Stock">
                            <ItemTemplate><%# Eval("qty_actual") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtqtyactual" runat="server" Text='<%# Eval("qty_actual") %>' CssClass="form-control input-sm"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField> 
                        <asp:TemplateField HeaderText="OUM">
                            <ItemTemplate>  
                                <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="cboUOM" runat="server"  CssClass="form-control input-sm"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Expire Date">
                            <ItemTemplate><%# Eval("expire_dt","{0:d/M/yyyy}") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtexpire_dt" runat="server" Text='<%# Eval("expire_dt","{0:d/M/yyyy}") %>' CssClass="form-control input-sm"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Bin CD">
                            <ItemTemplate>
                                <asp:Label ID="lblbin_cd" runat="server" Text='<%# Eval("bin_cd") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="cbobincd" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reason">
                            <ItemTemplate><%# Eval("reason") %></ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtreason" runat="server" Text='<%# Eval("reason") %>' CssClass="form-control input-sm"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbseqID" runat="server" Text='<%# Eval("seqID") %>'></asp:Label>
                            </ItemTemplate>
                            </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" ShowDeleteButton="True"/>
                    </Columns>
                        <EditRowStyle CssClass="table-edit"/>
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page"/>
                        <RowStyle   />
                        <SelectedRowStyle CssClass="table-edit" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

   
            <div class="navi margin-bottom">
                <asp:Button ID="btnew" runat="server" Text="NEW" CssClass="btn-success btn add" OnClick="btnew_Click" />
                <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn save" OnClick="btsave_Click"/>
                <asp:Button ID="btDelete" runat="server" CssClass="btn-danger btn delete" OnClick="btDelete_Click" Text="Delete" Visible="False" />
                <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn-info btn print" OnClick="btprint_Click" />
                    <asp:Button ID="btautoadj" runat="server" CssClass="btn-primary btn" OnClick="btautoadj_Click" Text="Auto Adj" Visible="False" />
            </div>
        </div>
    </div>

    
    <div class="divmsg loading-cont" id="dvshow" >
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

