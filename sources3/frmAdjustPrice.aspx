<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmAdjustPrice.aspx.cs" Inherits="fm_adjustpricelist" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
<link href="css/jquery-ui.css" rel="stylesheet" />
<script src="css/jquery-1.9.1.js"></script>
<script src="css/jquery-ui.js"></script>
 <script>
    function Left(str, n) {
        if (n <= 0)
            return "";
        else if (n > String(str).length)
            return str;
        else
            return String(str).substring(0, n);
    }

    function Right(str, n) {
        if (n <= 0)
            return "";
        else if (n > String(str).length)
            return str;
        else {
            var iLen = String(str).length;
            return String(str).substring(iLen, iLen - n);
        }
    }
    function Len(string_variable) {

        return string_variable.length;

    }
    function Search(str, p) {
        return str.search(p);
    }
</script>
<script>
    function openwindow() {
        var oNewWindow = window.open("fm_lookup_adjustPrice.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
    }

    function updpnl() {
        document.getElementById('<%=bttmp.ClientID%>').click();
        return (false);
    }
</script>
<script>
    function ItemSelectedcust(sender, e) {
        $get('<%=hdcust_cd.ClientID%>').value = Left(e.get_value(), Search(e.get_value(), '#'));
        $get('<%=hdSalesPointCD.ClientID%>').value = Right(e.get_value(), Len(e.get_value()) - Search(e.get_value(), '#') - 1);
        dv.attributes["class"].value = "showdiv";
        }
    </script>
<script>
    function ItemSelecteditem(sender, e) {
        $get('<%=hditem_cd.ClientID%>').value = e.get_value();
        $get('<%=hdotlcd.ClientID%>').value = "";
        $get('<%=hdcusgrcd.ClientID%>').value ="";
        dv.attributes["class"].value = "showdiv";
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div  class="divheader">Adjusment Price</div>
    <div class="h-divider"></div>

    <div class="container">
        <div class="row">
            <div class="col-md-6 margin-bottom clearfix">
                    <label class="control-label col-sm-2 no-padding-right">Code</label>
                    <div class="col-sm-10">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="input-group">
                        <ContentTemplate>
                            <asp:TextBox ID="txadjp_cd" runat="server" CssClass="makeitreadonly form-control" Enabled="False"></asp:TextBox>
                            <div class="input-group-btn">
                                 <asp:Button ID="btsearch" runat="server" CssClass="btn btn-primary btn-search" Style="padding-top:7px;" Text="Search" OnClientClick="openwindow();return(false);" />
                            </div>
                        </ContentTemplate>
                        <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                        </asp:UpdatePanel>
                        <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                    </div>
                </div>
            <div class="col-md-6 margin-bottom clearfix"> 
                <label for="UpdatePanel10" class="control-label col-sm-2"> Adj By</label> 
                <div class="col-sm-10 drop-down"> 
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbadjp_type" runat="server" CssClass="form-control" OnSelectedIndexChanged="cbadjp_type_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
             
                </div> 

            </div>
            <div class="col-md-6 margin-bottom clearfix">
                <label for="UpdatePanel4" class="control-label col-sm-2">Start Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txstart_dt" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="txstart_dt_CalendarExtender" runat="server" TargetControlID="txstart_dt" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  
                </div>
                </div>
            <div class="col-md-6 margin-bottom clearfix"> 
                    <label for="UpdatePanel3" class="control-label col-sm-2"> Cust CD</label> 
                    <div class="col-sm-10"> 
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txsearchcust" runat="server" Width="100%" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txsearchcust_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" runat="server" TargetControlID="txsearchcust" ServiceMethod="GetCompletionListcust" MinimumPrefixLength="1" 
                                EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelectedcust" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                <asp:HiddenField ID="hdcust_cd" runat="server" ClientIDMode="Static" />
                                <asp:DropDownList ID="cbotlcd" runat="server" OnSelectedIndexChanged="cbotlcd_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:DropDownList ID="cbcusgrcd" runat="server" OnSelectedIndexChanged="cbcusgrcd_SelectedIndexChanged" AutoPostBack="True" CssClass="form-control">
                                </asp:DropDownList>
                                <asp:HiddenField ID="hdSalesPointCD" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdotlcd" runat="server" ClientIDMode="Static" />
                                <asp:HiddenField ID="hdcusgrcd" runat="server" ClientIDMode="Static" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
        </div>
        <div class="row">
             
            <div class="col-md-12 no-padding">
                <table class="table table-striped mygrid">
                    <tr>
                        <th>Item Name</th>
                        <th>Amount</th>
                        <th>Action</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txsearchitemPnl">
                                        <asp:TextBox ID="txsearchitem" runat="server" AutoPostBack="True" CssClass="form-control input-sm"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item"  runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1"  ServiceMethod="GetCompletionListitem" TargetControlID="txsearchitem" OnClientItemSelected="ItemSelecteditem" UseContextKey="True">
                                        </asp:AutoCompleteExtender>
                                        <asp:HiddenField ID="hditem_cd" runat="server" />
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:Panel runat="server" ID="txamtPnl">
                                         <asp:TextBox ID="txamt" runat="server" CssClass="form-control input-sm" type="number" value="0"></asp:TextBox>
                                    </asp:Panel>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="width:80px;">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                              
                                    <asp:Button ID="btadd" runat="server" CssClass="btn-block btn-success btn-sm btn btn-add" OnClick="btadd_Click" Text="Add" />
                              
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
            </div>

            <div class="col-md-12 no-padding">
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-hover table-striped mygrid" GridLines="None" OnRowDeleting="grd_RowDeleting" OnSelectedIndexChanging="grd_SelectedIndexChanging" Width="100%">
                            <AlternatingRowStyle />
                            <Columns>
                    
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitem_cd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitem_nm" runat="server" Text='<%# Eval("item_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate>
                                        <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbadjp_cd" runat="server" Text='<%# Eval("adjp_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lbseqID" runat="server" Text='<%# Eval("seqID") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" />
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

        <div class="navi row margin-top margin-bottom padding-bottom">
            <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnew" runat="server" CssClass="btn btn-success btn-new" OnClick="btnew_Click" Text="NEW" />
                    <asp:Button ID="btsave" runat="server" CssClass=" btn btn-warning btn-save" OnClick="btsave_Click" Text="Save" />
                    <asp:Button ID="btDelete" runat="server" CssClass="btn btn-danger btn-delete" OnClick="btDelete_Click" Text="Delete" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
   
</asp:Content>

