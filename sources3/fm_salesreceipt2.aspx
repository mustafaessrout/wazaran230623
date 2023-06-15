<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesreceipt2.aspx.cs" Inherits="fm_salesreceipt2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />

    <script>
        function PopupCenter(url, title, w, h) {
            // Fixes dual-screen position                         Most browsers      Firefox
            var dualScreenLeft = window.screenLeft != undefined ? window.screenLeft : screen.left;
            var dualScreenTop = window.screenTop != undefined ? window.screenTop : screen.top;

            var width = window.innerWidth ? window.innerWidth : document.documentElement.clientWidth ? document.documentElement.clientWidth : screen.width;
            var height = window.innerHeight ? window.innerHeight : document.documentElement.clientHeight ? document.documentElement.clientHeight : screen.height;

            var left = ((width / 2) - (w / 2)) + dualScreenLeft;
            var top = ((height / 2) - (h / 2)) + dualScreenTop;
            var newWindow = window.open(url, title, 'scrollbars=yes, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);

            // Puts focus on the newWindow
            if (window.focus) {
                newWindow.focus();
            }
        }

        function SelectInvoice(sVal) {
            ShowProgress();
            $get('<%=hdinv.ClientID%>').value = sVal;
            $get('<%=btinv.ClientID%>').click();
        }

        function confirmInvoice(Val) {
            $get('<%=hdconfirm.ClientID%>').value = Val;
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
        $(document).ready(function () {
            $("#<%=btsearch.ClientID%>").click(function () {
                PopupCenter('lookupdo.aspx', 'xtf', '900', '500');

            });
            //HideProgress();
        });
    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hdinv" runat="server" />
            <asp:HiddenField ID="hdconfirm" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

    <div class="form-horizontal" style="font-family: Calibri">
        <h4 class="jajarangenjang"><asp:Label ID="lbtitle" runat="server" Text=""></asp:Label></h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">PInvoice</label>
            <div class="col-md-2">
                <div class="input-group">
                    <asp:Label ID="lbinvno" CssClass="form-control input-group-sm ro" runat="server" Text=""></asp:Label>
                    <div class="input-group-btn">
                        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><i class="fa fa-search"></i></asp:LinkButton>
                    </div>
                </div>

            </div>
            <div class="col-md-9">
                <label style="color: red" class="control-label">Edit quantity received, as invoice received back from Customer!</label>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Cust Info</label>
            <div class="col-md-11">
                <table class="mGrid">
                    <tr>
                        <th>Cust</th>
                        <th>Salesman</th>
                        <th>Inv Date</th>
                        <th>Delivery Date</th>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lbcust" runat="server" Text="" CssClass="control-label"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbsalesman" runat="server" Text="" CssClass="control-label"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbinvdate" CssClass="control-label" runat="server" Text=""></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbdeliverydate" runat="server" Text="" CssClass="control-label"></asp:Label>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="h-divider"></div>
        <h5 class="jajarangenjang">Detail Order</h5>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" OnRowCancelingEdit="grd_RowCancelingEdit" ShowFooter="True" CellPadding="0">
                            <Columns>
                                <asp:TemplateField HeaderText="Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Item Name">
                                    <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                    <ItemTemplate><%#Eval("size") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Branded">
                                    <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty Order">
                                    <ItemTemplate>
                                        <asp:Label ID="lbqty" runat="server" Text='<%#Eval("qtyorder") %>'></asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotqtyorder" runat="server" Text=""></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        <asp:Label ID="lbuom" runat="server" Text='<%#Eval("uom") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty Received">
                                    <ItemTemplate>
                                        <asp:Label ID="lbqtyshipment" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txqty" runat="server" Text='<%#Eval("qty") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <FooterTemplate>
                                        <asp:Label ID="lbtotqtyshipment" runat="server" Text=""></asp:Label>
                                    </FooterTemplate>
                                </asp:TemplateField>
                                <asp:CommandField HeaderText="Change Qty" ShowEditButton="False" />
                            </Columns>
                            <FooterStyle BackColor="Yellow" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>


            </div>
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <ContentTemplate>
        <div id="freeGoods" runat="server">
            <strong>Free Goods</strong>
            <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grdfree" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" EmptyDataText="Free Item Not Found" ShowHeaderWhenEmpty="True" ShowFooter="True" OnRowCancelingEdit="grdfree_RowCancelingEdit" OnRowEditing="grdfree_RowEditing" OnRowUpdating="grdfree_RowUpdating" >
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:HiddenField ID="hddiscount" Value='<%#Eval("disc_cd") %>' runat="server" />
                                <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate><%#Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branded">
                            <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Free">
                            <ItemTemplate>
                                <asp:Label ID="lbqtyfree" runat="server" Text='<%#Eval("qtyfree") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotqtyfree" runat="server" Text=""></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM">
                            <ItemTemplate>
                                <asp:Label ID="lbuom" runat="server" Text='<%#Eval("uom") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Received">
                            <ItemTemplate>
                                <asp:Label ID="lbqtyreceived" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txqty" Text='<%#Eval("qty") %>' runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <FooterTemplate>
                                <asp:Label ID="lbtotreceived" runat="server" Text=""></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Change Qty" ShowEditButton="False" />
                    </Columns>
                    <FooterStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
        </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
        <ContentTemplate>
        <div id="freeCash" runat="server">
            <strong>Free Cash</strong>
            <div class="form-group">
                <div class="col-md-12">
                    <asp:GridView ID="grdcash" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" EmptyDataText="Free Cash Not Found" ShowHeaderWhenEmpty="True" ShowFooter="True" OnRowCancelingEdit="grdcash_RowCancelingEdit" OnRowEditing="grdcash_RowEditing" OnRowUpdating="grdcash_RowUpdating" >
                        <Columns>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hddiscount" Value='<%#Eval("disc_cd") %>' runat="server" />
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%#Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%#Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded">
                                <ItemTemplate><%#Eval("branded_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount">
                                <ItemTemplate>
                                    <asp:Label ID="lbdiscount" runat="server" Text='<%#Eval("discount") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount Received">
                                <ItemTemplate><%#Eval("amt") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txdiscount" Text='<%#Eval("amt") %>' runat="server"></asp:TextBox>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:Label ID="lbtotreceived" runat="server" Text=""></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateField>
                            <asp:CommandField HeaderText="Change Discount" ShowEditButton="False" />
                        </Columns>
                        <FooterStyle BackColor="Yellow" />
                    </asp:GridView>
                </div>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>

        <strong>Discount Calculation</strong>
        <div class="form-group">
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rddisc" runat="server" CssClass="form-control input-group-sm" style="background-color:silver" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" Width="100%">
                            <asp:ListItem Value="M">Manual</asp:ListItem>
                            <asp:ListItem Value="A" Selected="True">Automatic</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="form-group">
            <label class="control-label col-md-2 col-md-offset-4">TOTAL QUANTITY DELIVERY</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbdelivery" CssClass="control-label" Style="font-size: xx-large; font-weight: bold" runat="server" BackColor="Silver"></asp:Label>
                        <asp:Label ID="lbdeliveryuom" CssClass="control-label" Style="font-size: xx-large; font-weight: bold" runat="server" BackColor="Silver"> CTN</asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </div>
        </div>

        <h5 class="jajarangenjang">Driver Received</h5>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-6">
                <table class="mGrid">
                    <tr>
                        <th>Driver</th>
                        <th>Qty Received</th>
                        <th>Add</th>
                    </tr>
                    <tr>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbdriver" CssClass="form-control" runat="server"></asp:DropDownList></td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txqty" runat="server" CssClass="form-control"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="grd" EventName="RowUpdating" />
                                </Triggers>
                            </asp:UpdatePanel>
                         
                        </td>
                        <td>
                            <asp:LinkButton ID="btadd" CssClass="btn btn-primary" runat="server" OnClientClick="javascript:ShowProgress();" OnClick="btadd_Click"><i class="fa fa-cart-plus">&nbsp;Add Driver Received</i></asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="col-md-6">
                <label class="control-label" style="color: red">Specify qty for driver, for items delivered</label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grddriver" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grddriver_RowDeleting" OnRowCancelingEdit="grddriver_RowCancelingEdit" OnRowEditing="grddriver_RowEditing" OnRowUpdating="grddriver_RowUpdating" ShowFooter="True" CellPadding="0">
                    <Columns>
                        <asp:TemplateField HeaderText="Emp Code">
                            <ItemTemplate>
                                <asp:Label ID="lbempcode" runat="server" Text='<%#Eval("emp_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Driver Name">
                            <ItemTemplate>
                                <%#Eval("emp_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Bring by driver">
                            <ItemTemplate>
                                <asp:Label ID="lbqtydriver" runat="server" Text='<%#Eval("qty") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txqtydriver" Text='<%#Eval("qty") %>' runat="server"></asp:TextBox>
                            </EditItemTemplate>                            
                            <FooterTemplate>
                                <asp:Label ID="lbtotqty" runat="server" Text=""></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Change Qty" ShowEditButton="True" />
                        <asp:CommandField ShowDeleteButton="True" />                        
                    </Columns>
                    <FooterStyle BackColor="Yellow" />
                </asp:GridView>
            </div>
        </div>

        <asp:UpdatePanel ID="UpdatePanel8" runat="server">
        <ContentTemplate>
        <div id="confirmReason" runat="server">
            <div class="h-divider"></div>
            <h5 class="jajarangenjang">Reason for Confirm Invoice</h5>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Reason</label>
                <div class="col-md-3">
                    <div class="input-group">
                        <asp:DropDownList ID="cbreason" CssClass="form-control" runat="server"></asp:DropDownList></td>
                    </div>

                </div>
                <div class="col-md-8">
                    <label style="color: red" class="control-label">Select the Reason, when edit the invoice !</label>
                </div>
            </div>
        </div>
        </ContentTemplate>
        </asp:UpdatePanel>

        <div class="form-group">
            <div class="col-md-12" style="text-align: center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-success" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btreceived" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btreceived_Click">RECEIVED INVOICE</asp:LinkButton>
                <asp:LinkButton ID="btdriver" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btdriver_Click">RECEIVED DRIVER</asp:LinkButton>
                <asp:LinkButton ID="btpostpone" OnClientClick="x=confirm ('Postpone this Invoice ?');if (x==true) { confirmInvoice('true');} else {confirmInvoice('false'); }" CssClass="btn btn-danger" runat="server" OnClick="btpostpone_Click">POSTPONE INVOICE</asp:LinkButton>
                <asp:LinkButton ID="btconfirm" OnClientClick="x=confirm ('Confirm this Invoice ?');if (x==true) { confirmInvoice('true');} else {confirmInvoice('false'); }" CssClass="btn btn-primary" runat="server" OnClick="btconfirm_Click">CONFIRMED INVOICE</asp:LinkButton>
                <asp:LinkButton ID="btcancel" OnClientClick="x=confirm ('Cancel Edit this Invoice ?');if (x==true) { confirmInvoice('true');} else {confirmInvoice('false'); }" CssClass="btn btn-danger" runat="server" OnClick="btcancel_Click">CANCELED EDIT INVOICE</asp:LinkButton>
                <asp:LinkButton ID="btprintedit" CssClass="btn btn-success" runat="server" OnClick="btprintedit_Click"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
                <asp:LinkButton ID="btprint" CssClass="btn btn-success" runat="server" OnClick="btprint_Click"><i class="fa fa-print">&nbsp;Print Commercial Invoice Final</i></asp:LinkButton>                
            </div>
        </div>

    </div>
    <asp:Button ID="btinv" runat="server" Text="Button" Style="display: none" OnClick="btinv_Click" />
    <div class="divmsg loading-cont" style="display: none" id="pnlmsg">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>. 
</asp:Content>

