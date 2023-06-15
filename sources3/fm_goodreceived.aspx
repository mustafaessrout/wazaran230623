<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_goodreceived.aspx.cs" Inherits="fm_goodreceived" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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
            $get('<%=btitem.ClientID%>').click();
        }

        function SelectData(x) {
            $get('<%=hdreceipt.ClientID%>').value = x;
            $get('<%=btlookup.ClientID%>').click();
        }

    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hditem" runat="server" />
    <asp:HiddenField ID="hdreceipt" runat="server" />
    <div class="alert alert-info text-bold">
        <asp:Label ID="lbcaption" runat="server" Text="Good Received"></asp:Label>
    </div>
    <div class="container">
        <div class="row margin-top">

            <label class="control-label-sm input-sm col-sm-1">Type Of Stock In</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbtypeofstockin" onKeyUp="this.blur();" AutoPostBack="true" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbtypeofstockin_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label class="control-label-sm input-sm col-sm-1">GDN Factory Post</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="dtpostgdn" CssClass="form-control input-group-sm input-sm" runat="server"></asp:TextBox>
                    <asp:CalendarExtender ID="CalendarExtender1" Format="d/M/yyyy" runat="server" TargetControlID="dtpostgdn">
                    </asp:CalendarExtender>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearchgdn" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsearchgdn_Click"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>

            </div>
            <label class="control-label-sm input-sm col-sm-1">Register/Stock Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtreceipt" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtreceipt_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtreceipt">
                </asp:CalendarExtender>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Received By Whs Date</label>
            <div class="col-sm-2 drop-down-date">
                <asp:TextBox ID="dtwarehouse" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtwarehouse_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtwarehouse">
                </asp:CalendarExtender>
            </div>
            <%--<label class="control-label-sm input-sm col-sm-1">Driver</label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txdrivername" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>--%>
        </div>
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm input-sm col-sm-1">Driver</label>
            <div class="col-sm-2 require">
                <asp:TextBox ID="txdrivername" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Vendor</label>
            <div class="col-sm-2 require drop-down">
                <asp:DropDownList ID="cbvendor" CssClass="form-control input-sm" onchange="ShowProgress();" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbvendor_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label class="control-label-sm input-sm col-sm-1">Remark</label>
            <div class="col-sm-5">
                <asp:TextBox ID="txremark" CssClass="form-control input-sm" runat="server"></asp:TextBox>
            </div>
        </div>
        <asp:Panel ID="pnlnav" runat="server">
            <div class="row margin-bottom margin-top">
                <div class="col-sm-12">
                    <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnRowDataBound="grd_RowDataBound" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
                        <Columns>
                            <asp:TemplateField HeaderText="GDN No">
                                <ItemTemplate>
                                    <asp:Label ID="lbdono" runat="server" Text='<%#Eval("No") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Date">
                                <ItemTemplate>
                                    <%#Eval("Order_date","{0:d/M/yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Posting Date">
                                <ItemTemplate><%#Eval("Posting_date","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Shipment Date">
                                <ItemTemplate><%#Eval("Shipment_date","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reference">
                                <ItemTemplate><%#Eval("Reference_No") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbbranchcode" runat="server" Text='<%#Eval("DO_Branch_Code") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Driver">
                                <ItemTemplate>
                                    <%#Eval("Driver_Name") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbtotqty" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                        <SelectedRowStyle BackColor="Yellow" />
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <asp:Panel ID="pnlgoodreceivedbranch" runat="server">
            <div class="row margin-bottom margin-top">
                <div class="col-sm-12">
                    <asp:GridView ID="grdgrbranch" CssClass="mGrid" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Transfer No">
                                <ItemTemplate>
                                    <asp:Label ID="lbtransferno" runat="server" Text='<%#Eval("trf_no") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Code">
                                <ItemTemplate><%#Eval("salespointcd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branch Name">
                                <ItemTemplate><%#Eval("salespoint_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark"></asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </asp:Panel>
        <div class="row alert alert-info">
            Detail GDN :
            <asp:Label ID="lbgdnnav" Font-Bold="true" Font-Size="Medium" ForeColor="Red" runat="server" Text=""></asp:Label> , 
           PO Distribution :
             <asp:Label ID="lbrefno" Font-Bold="true" Font-Size="Medium" ForeColor="Red" runat="server" Text=""></asp:Label>
        </div>
        <div class="row margin-bottom margin-top">
            <label class="control-label-sm input-sm col-sm-1">GRN No.</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="txgrnno" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" OnClientClick="PopupCenter('lookupgr.aspx','Lookup Good Received',800,800);" CssClass="btn btn-primary btn-sm" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>

            </div>
            <label class="control-label-sm input-sm col-sm-1">GDN Factory No.</label>
            <div class="col-sm-2">
                <div class="input-group">
                    <asp:TextBox ID="txgdnav" CssClass="form-control input-sm input-group-sm" runat="server"></asp:TextBox>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btgdnnav" OnClientClick="PopupCenter('lookupgrnav.aspx','Lookup Good Received',800,800);" CssClass="btn btn-primary btn-sm" runat="server"><span class="fa fa-search"></span></asp:LinkButton>
                    </div>
                </div>

            </div>
            <label class="control-label input-sm col-sm-1">Send Warehouse</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbwhs" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label class="control-label input-sm col-sm-1">Bin</label>
            <div class="col-sm-2 drop-down require">
                <asp:DropDownList ID="cbbin" CssClass="form-control input-sm" onchange="ShowProgress();" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbbin_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="row margin-bottom margin-top">
            <div class="col-sm-12">
                <table class="mGrid">
                    <thead>
                        <tr>
                            <th style="width: 40%">Item</th>
                            <th style="width: 10%">Qty</th>
                            <th style="width: 10%">Uom</th>
                            <th style="width: 10%">Prod Date</th>
                            <th style="width: 10%">Expire Date</th>
                            <th style="width: 10%">Lot No</th>
                            <th>Add</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>
                                <asp:TextBox ID="txitemsearch" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txitemsearch_AutoCompleteExtender" runat="server" TargetControlID="txitemsearch" ShowOnlyCurrentWordInCompletionListItem="true" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" ServiceMethod="GetItemList" OnClientItemSelected="ItemSelected" UseContextKey="true" MinimumPrefixLength="1">
                                </asp:AutoCompleteExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txqty" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </td>
                            <td class="drop-down">
                                <asp:DropDownList ID="cbuom" CssClass="form-control input-sm" runat="server"></asp:DropDownList>
                            </td>
                            <td class="drop-down-date">
                                <asp:TextBox ID="dtprod" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="dtprod_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtprod">
                                </asp:CalendarExtender>
                            </td>
                            <td class="drop-down-date">
                                <asp:TextBox ID="dtexpire" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                                <asp:CalendarExtender ID="dtexpire_CalendarExtender" Format="d/M/yyyy" runat="server" TargetControlID="dtexpire">
                                </asp:CalendarExtender>
                            </td>
                            <td>
                                <asp:TextBox ID="txlotno" CssClass="form-control input-sm" runat="server"></asp:TextBox>
                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btadd_Click">Add</asp:LinkButton>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
        <div class="row margin-bottom margin-top">
            <div class="col-sm-12">
                <asp:Panel ID="pnlgrdnav" runat="server">
                    <asp:GridView ID="grddetail" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDataBound="grddetail_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code Navision">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcodenav" runat="server" Text='<%#Eval("DO_Item_No_1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Code Wazaran">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcodewazaran" Font-Bold="true" Font-Size="Medium" ForeColor="Red" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%#Eval("DO_Description") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <asp:Label ID="lbsize" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate>
                                    <asp:Label ID="lbqty" runat="server" Text='<%#Eval("DO_Qty") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate>
                                    <asp:Label ID="lbuom" runat="server" Text='<%#Eval("DO_Uom") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prod Date">
                                <ItemTemplate>
                                    <div class="drop-down-date">
                                        <asp:TextBox ID="dtprod" BackColor="Yellow" CssClass="form-control input-sm" runat="server" Text='<%#Eval("DO_ProdDate") %>'></asp:TextBox>
                                        <asp:CalendarExtender ID="dtprod_calender" TargetControlID="dtprod" Format="M/d/yyyy" runat="server"></asp:CalendarExtender>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exp Date">
                                <ItemTemplate>
                                    <div class="drop-down-date">
                                        <asp:TextBox ID="dtexp" BackColor="Yellow" runat="server" CssClass="form-control input-sm" Text='<%#Eval("DO_ExpDate") %>'></asp:TextBox>
                                        <asp:CalendarExtender ID="dtexp_calender" TargetControlID="dtexp" Format="M/d/yyyy" runat="server"></asp:CalendarExtender>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lot No">
                                <ItemTemplate>
                                    <b>
                                        <asp:Label ID="lblotno" Font-Bold="true" Font-Size="Medium" runat="server" Text='<%#Eval("DO_LotNo") %>'></asp:Label>
                                    </b>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </asp:Panel>
                <asp:Panel ID="pnlgrdnormal" runat="server">
                    <asp:GridView ID="grddetailnormal" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grddetailnormal_RowDeleting" OnRowDataBound="grddetailnormal_RowDataBound" ShowFooter="True">
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate>
                                    <asp:Label ID="lbsize" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded">
                                <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Qty">
                                <ItemTemplate><%#Eval("qty","{0:N2}") %></ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotalqty" Font-Bold="true" Font-Size="Medium" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="UOM">
                                <ItemTemplate><%#Eval("uom") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Prod Date">
                                <ItemTemplate><%#Eval("prod_date","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Exp Date">
                                <ItemTemplate><%#Eval("expire_date","{0:d/M/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Lot No">
                                <ItemTemplate><%#Eval("lot_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                        <FooterStyle BackColor="Yellow" />
                    </asp:GridView>
                </asp:Panel>

            </div>
        </div>
        <div class="row margin-top margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btreceived" CssClass="btn btn-danger btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btreceived_Click">Received GDN Factory</asp:LinkButton>
                <asp:LinkButton ID="btprint" CssClass="btn btn-warning btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                <asp:Button ID="btitem" OnClientClick="ShowProgress();" Style="display: none" runat="server" OnClick="btitem_Click" Text="Button" />
                <asp:LinkButton ID="btsavestockin" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsavestockin_Click">Save</asp:LinkButton>
                <asp:Button ID="btlookup" OnClientClick="ShowProgress();" Style="display: none" runat="server" Text="Button" OnClick="btlookup_Click" />
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

