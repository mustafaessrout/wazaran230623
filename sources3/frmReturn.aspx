<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmReturn.aspx.cs" Inherits="frmReturn" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_return.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    
    <%--  <script type="text/javascript">
        $(function () {
            $("#<%=txinvDocDate.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
        });
      </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txinvDate.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
        });
      </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txinpexpireDate.ClientID%>").datepicker({ dateFormat: "mm/dd/yy" }).val();
        });
      </script>--%>
     <script>
         function ItemSelected(sender, e) {
             $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    <script>
        function ItemSelectedCust(sender, e) {
            $get('<%=hdcust_cd.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }

    </script>
    <script>
        function ItemSelectedsiteCD(sender, e) {
            $get('<%=hdsiteCD.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }

    </script>
    <script>
        function ItemSelectedsiteCDVan(sender, e) {
            $get('<%=hdsiteCDVan.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }

    </script>
    <style type="text/css">


        .button2.add {
    background: #f3f3f3 url('css/5Fm069k.png') no-repeat 10px -27px;
    padding-left: 30px;
    border-radius:8px;
}

.button2 {
    color: #6e6e6e;
    font: bold 12px Helvetica, Arial, sans-serif;
    text-decoration: none;
    padding: 7px 12px;
    position: relative;
    display: inline-block;
    text-shadow: 0 1px 0 #fff;
    -webkit-transition: border-color .218s;
    -moz-transition: border .218s;
    -o-transition: border-color .218s;
    transition: border-color .218s;
    background: #f3f3f3;
    background: -webkit-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
    background: #f3f3f3;
    border: solid 1px #dcdcdc;
    border-radius: 2px;
    -webkit-border-radius: 2px;
    -moz-border-radius: 2px;
    margin-right: 10px;
            top: 0px;
            left: 0px;
            height: 39px;
            width: 130px;
        }
        
.button2 {
    color: #6e6e6e;
    font: bold 12px Helvetica, Arial, sans-serif;
    text-decoration: none;
    padding: 7px 12px;
    position: relative;
    display: inline-block;
    text-shadow: 0 1px 0 #fff;
    -webkit-transition: border-color .218s;
    -moz-transition: border .218s;
    -o-transition: border-color .218s;
    transition: border-color .218s;
    background: #f3f3f3;
    background: -webkit-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
    background: -moz-linear-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
    border: solid 1px #dcdcdc;
    border-radius: 2px;
    -webkit-border-radius: 2px;
    -moz-border-radius: 2px;
    margin-right: 10px;
}
        .auto-style1 {
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content  ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td><h3>SALES RETURN PROCESS</h3></td>
         </tr>
    </table>
    <table>
        <tr>
            <td>Return No</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txreturnCD" runat="server" CssClass="makeitreadonly" ReadOnly="True" Width="130px"></asp:TextBox>
                        <strong>
                        <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClientClick="openwindow();return(false);" style="left: 0px; top: 7px; width: 99px;" Text="Search" />
                        <asp:TextBox ID="txKey" runat="server" CssClass="auto-style3" Width="40px" style="display:none"></asp:TextBox>
                        </strong>
                    </ContentTemplate>
                     <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                   </asp:UpdatePanel>
                <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
             </td>
            <td>Sales Point</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="makeitreadonly" Enabled="False" Height="20px" Width="195px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Return Doc Date</td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txinvDocDate" runat="server" CssClass="auto-style3" Width="130px"></asp:TextBox>
                                <asp:CalendarExtender ID="txinvDocDate_CalendarExtender" runat="server" TargetControlID="txinvDocDate">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="d/M/YYYY" Font-Bold="True" Font-Size="Medium" ForeColor="Red">d/M/YYYY</asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
            <td>Salesman</td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbSalesCD" runat="server" Height="20px" Width="195px" CssClass="auto-style3" AutoPostBack="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
        </tr>
        <tr>
            <td class="auto-style1">Return Date</td>
            <td class="auto-style1">
                        <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txinvDate" runat="server" CssClass="auto-style3" Width="130px"></asp:TextBox>
                                <asp:CalendarExtender ID="txinvDate_CalendarExtender" runat="server" TargetControlID="txinvDate">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="d/M/YYYY" Font-Bold="True" Font-Size="Medium" ForeColor="Red">d/M/YYYY</asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
            <td class="auto-style1">Reference</td>
            <td class="auto-style1">
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txinvReference" runat="server" CssClass="auto-style3" Width="130px"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
        </tr>
        <tr>
            <td>Customer</td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txsearchCust" runat="server" AutoPostBack="True" OnTextChanged="txsearchCust_TextChanged" Width="356px"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txsearchCust_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelectedCust" ServiceMethod="GetCompletionListCust" TargetControlID="txsearchCust" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="**" Font-Bold="True" ForeColor="Red">**</asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdcust_cd" runat="server" ClientIDMode="Static" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
            <td>site Dest</td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbsiteDest" runat="server" Height="20px" Width="195px"  AutoPostBack="True" OnSelectedIndexChanged="cbsiteDest_SelectedIndexChanged">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
        </tr>
        <tr>
            <td>Price Level</td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbpricelevel_cd" runat="server" AutoPostBack="True" CssClass="auto-style3" Height="20px" Width="195px">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
            <td>site</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txsearchsiteCD" runat="server" Width="250px"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txsearchsiteCD_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelectedsiteCD" ServiceMethod="GetCompletionListsiteCD" TargetControlID="txsearchsiteCD" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:HiddenField ID="hdsiteCD" runat="server" ClientIDMode="Static" />
                        <asp:TextBox ID="txsearchsiteCDVan" runat="server" Width="250px"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txsearchsiteCDVan_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelectedsiteCDVan" ServiceMethod="GetCompletionListsiteCDVan" TargetControlID="txsearchsiteCDVan" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:HiddenField ID="hdsiteCDVan" runat="server" ClientIDMode="Static" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                    <ContentTemplate>
                        Note
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel27" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txinvNote" runat="server" CssClass="auto-style3" Width="130px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                    <ContentTemplate>
                        BIN Type
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbsiteType" runat="server" AutoPostBack="True" Height="20px" Width="195px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>
    <table>
        <tr style="background-color:silver;border-color:yellow;border:none">
          <td>Item Name</td>
          <td>UOM</td>
          <td>Quantity</td>
          <td>Qty Receipt</td>
          <td>Price</td>
          <td>Amount</td>
          <td>Expire Date</td>
          <td>Reason</td>
          
          <td>
                            <asp:Button ID="btAdd" runat="server" CssClass="button2 add" OnClick="btAdd_Click" Text="Add" />
                        </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txsearchitem" runat="server" AutoPostBack="True" Width="356px" OnTextChanged="txsearchitem_TextChanged"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelected" ServiceMethod="GetCompletionList" TargetControlID="txsearchitem" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:HiddenField ID="hditem" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbUOM" runat="server" Height="20px" Width="95px" CssClass="auto-style3" AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txinpQuantity" runat="server" AutoPostBack="True" CssClass="auto-style3" Width="49px" OnTextChanged="txinpQuantity_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txinpQuantityRec" runat="server" AutoPostBack="True" CssClass="auto-style3" Width="49px" OnTextChanged="txinpQuantityRec_TextChanged"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txinpPrice" runat="server" CssClass="auto-style3" Width="130px" OnTextChanged="txinpPrice_TextChanged" AutoPostBack="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txinpAmount" runat="server" CssClass="auto-style3" Width="130px" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txinpexpireDate" runat="server" CssClass="auto-style3" Width="130px"></asp:TextBox>
                        <asp:CalendarExtender ID="txinpexpireDate_CalendarExtender" runat="server" TargetControlID="txinpexpireDate" Format="d/M/yyyy">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbreasn_cd" runat="server" Height="20px" Width="95px" CssClass="auto-style3" AutoPostBack="True">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            
        </tr>
        
    </table>
    <table style="width: 100%;">
        <tr>
            <td>
                            <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" OnSelectedIndexChanging="grd_SelectedIndexChanging" Width="1031px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbinpSeq" runat="server" Text='<%# Eval("inpSeq") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="item Code">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitem_cd" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="item name">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblitem_nm" runat="server" Text='<%# Eval("itmDesc") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="UoM">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblUOMCD" runat="server" Text='<%# Eval("UOMCD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinpQuantity" runat="server" Text='<%# Eval("inpQuantity","{0:n0}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Qty Rec">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinpQuantityRec" runat="server" Text='<%# Eval("inpQuantityRec","{0:n0}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Price">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinpPrice" runat="server" Text='<%# Eval("inpPrice","{0:n0}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinpAmount" runat="server" Text='<%# Eval("inpAmount","{0:n0}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expire Date">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblinpexpireDate" runat="server" Text='<%# Eval("inpexpireDate","{0:d/M/yyyy}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Reason">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblreasn_cd" runat="server" Text='<%# Eval("reasn_cd") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbSeqID" runat="server" Text='<%# Eval("SeqID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:CommandField HeaderText="Action" ShowDeleteButton="True" ShowSelectButton="True" />
                                        </Columns>
                                        <SelectedRowStyle BackColor="#3399FF" />
                                    </asp:GridView>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
        </tr>
        </table>
    <table style="width: 100%;">
        <tr>
            <td>
                        <asp:Button ID="btsave" runat="server" Text="SAVE" OnClick="btsave_Click" CssClass="button2 save" style="top: 0px; left: 0px; " />
                    </td>
            <td>
                        <asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="New" />
                    </td>
            <td>
                        <asp:Button ID="btDelete" runat="server" CssClass="button2 delete" OnClick="btDelete_Click" Text="Delete" />
                    </td>
            <td>
                        <asp:Button ID="btprint" runat="server" CssClass="button2 print" OnClick="btprint_Click" style="left: 0px; top: 0px" Text="Print" />
                    </td>
        </tr>
    </table>
    </asp:Content>

