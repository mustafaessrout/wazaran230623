<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmARRec.aspx.cs" Inherits="frmARRec" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_ARRec.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function openwindow2() {
           
            var input = "ARRecID=" + document.getElementById('<%=txKey.ClientID%>').value + "&RecDate=" + document.getElementById('<%=txrecDate.ClientID%>').value + "&SalesPointCD=" + document.getElementById('<%=cbSalesPointCD.ClientID%>').value + "&CustCD=" + document.getElementById('<%=hdcust_cd.ClientID%>').value + "&salesCD=" + document.getElementById('<%=cbSalesCD.ClientID%>').value + "&ARCType=" + document.getElementById('<%=cbARCType.ClientID%>').value;
            var oNewWindow = window.open("fm_lookup_ARRecAddDtl.aspx?"+input, "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
            
        }

        function updpnl2() {
            document.getElementById('<%=bttmp2.ClientID%>').click();
            return (false);
        }
    </script>
    <%-- <script type="text/javascript">
        $(function () {
            $("#<%=txrecDate.ClientID%>").datepicker({ dateFormat: "dd/mm/yy" }).val();
         });
      </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txARCDate.ClientID%>").datepicker({ dateFormat: "dd/mm/yy" }).val();
        });
      </script>
    <script type="text/javascript">
        $(function () {
            $("#<%=txARCDueDate.ClientID%>").datepicker({ dateFormat: "dd/mm/yy" }).val();
        });
      </script>--%>
    <script>
        function ItemSelectedCust(sender, e) {
            $get('<%=hdcust_cd.ClientID%>').value = e.get_value();
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
            top: 1px;
            left: 7px;
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
    </style>
</asp:Content>
<asp:Content  ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table>
        <tr>
            <td><h3>PAYMENT RECEIPT PROCESS</h3></td>
         </tr>
    </table>
    <table>
        <tr>
            <td>Receipt No</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txARRecCD" runat="server" CssClass="makeitreadonly" ReadOnly="True" Width="130px"></asp:TextBox>
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
            <td>Receipt Date</td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txrecDate" runat="server" CssClass="auto-style3" Width="130px"></asp:TextBox>
                                <asp:CalendarExtender ID="txrecDate_CalendarExtender" runat="server" TargetControlID="txrecDate" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                                </asp:CalendarExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="d/M/yyyy" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ControlToValidate="txrecDate">**</asp:RequiredFieldValidator>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </td>
            <td>Salesman</td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbSalesCD" runat="server" Height="20px" Width="195px" CssClass="auto-style3" AutoPostBack="True">
                                </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
        </tr>
        <tr>
            <td>Customer</td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel26" runat="server">
                            <ContentTemplate>
                                
                                <asp:TextBox ID="txsearchCust" runat="server" AutoPostBack="True" OnTextChanged="txsearchCust_TextChanged" Width="356px"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txsearchCust_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelectedCust" ServiceMethod="GetCompletionListCust" TargetControlID="txsearchCust" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Fill Customer" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ControlToValidate="txsearchCust">**</asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdcust_cd" runat="server" ClientIDMode="Static" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
            </td>
            <td>Payment Type</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel28" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbARCType" runat="server" AutoPostBack="True" CssClass="auto-style3" Height="20px" OnSelectedIndexChanged="cbARCType_SelectedIndexChanged" Width="195px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        </table>
    <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel29" runat="server">
                    <ContentTemplate>
                     <asp:Button ID="btAddDetail" runat="server" CssClass="button2 search2" OnClientClick="openwindow2();return(false);" style="left: 0px; top: 7px; width: 112px;" Text="       Add Detail" />
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp2" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
                <asp:Button ID="bttmp2" runat="server" Text="Button" OnClick="bttmp2_Click" style="display:none" />
            </td>
        </tr>
    </table>
    <table>
        <tr style="background-color:silver;border-color:yellow;border:none">
          <td>&nbsp;</td>
          <td>Invoice</td>
          <td>Amount</td>
             <td>Description</td>
            <td></td>
        </tr>
        <tr>
              <td>
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbARCRefID" runat="server" AutoPostBack="True" CssClass="auto-style3" Height="20px" Width="195px" OnTextChanged="cbARCRefID_TextChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txARCAmt" runat="server" CssClass="auto-style3" Width="80px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txARCDescription" runat="server" CssClass="auto-style3" Width="130px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                            <asp:Button ID="btAdd" runat="server" CssClass="button2 add" OnClick="btAdd_Click" Text="Add" />
                        </td>
        </tr>
    </table>
        <table>
        <tr>
            <td>
                            <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                                <ContentTemplate>
                                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" OnSelectedIndexChanging="grd_SelectedIndexChanging" Width="1031px">
                                        <Columns>
                                            <asp:TemplateField HeaderText="No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblARCSeq" runat="server" Text='<%# Eval("ARCSeq") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Invoice CD">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSOCD" runat="server" Text='<%# Eval("SOCD") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblARCAmt" runat="server" Text='<%# Eval("ARCAmt","{0:n0}") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remark">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblARCDescription" runat="server" Text='<%# Eval("ARCDescription") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblSeqID" runat="server" Text='<%# Eval("SeqID") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="false">
                                                <ItemTemplate>
                                                    <asp:Label ID="lblARCRefID" runat="server" Text='<%# Eval("ARCRefID") %>'></asp:Label>
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
    <table>
        <tr style="background-color:silver;border-color:yellow;border:none">
           
            <td>
              <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                  <ContentTemplate>
                      <asp:Label ID="lbBankID" runat="server" Text="Bank"></asp:Label>
                  </ContentTemplate>
              </asp:UpdatePanel>
            </td>
          <td>
              <asp:UpdatePanel ID="UpdatePanel23" runat="server">
                  <ContentTemplate>
                      <asp:Label ID="lbrscDocNo" runat="server" Text="Doc No"></asp:Label>
                  </ContentTemplate>
              </asp:UpdatePanel>
            </td>
          <td>
              <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                  <ContentTemplate>
                      <asp:Label ID="lbrscDocDate" runat="server" Text="Date"></asp:Label>
                  </ContentTemplate>
              </asp:UpdatePanel>
            </td>
          <td>
              <asp:UpdatePanel ID="UpdatePanel25" runat="server">
                  <ContentTemplate>
                      <asp:Label ID="lbrscDueDate" runat="server" Text="Due Date"></asp:Label>
                  </ContentTemplate>
              </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel35" runat="server">
                    <ContentTemplate>
                      <asp:Label ID="lbrscAmount" runat="server" Text="Amount"></asp:Label>
                  </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel30" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbBankID" runat="server" AutoPostBack="True" Height="20px" Width="195px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel31" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txrscDocNo" runat="server"  Width="100px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel32" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txrscDocDate" runat="server"  Width="70px"></asp:TextBox>
                        <asp:CalendarExtender ID="txrscDocDate_CalendarExtender" runat="server" TargetControlID="txrscDocDate" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel33" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txrscDueDate" runat="server"  Width="70px"></asp:TextBox>
                        <asp:CalendarExtender ID="txrscDueDate_CalendarExtender" runat="server" TargetControlID="txrscDueDate" DaysModeTitleFormat="d/M/yyyy" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel34" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txrscAmount" runat="server"  Width="80px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel37" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btAddCH" runat="server" CssClass="button2 add" OnClick="btAddCH_Click" Text="Add" />
                    </ContentTemplate>
                  </asp:UpdatePanel>     
             </td>
        </tr>
    </table>
    
        <table>
            <tr>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel36" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="grdCH" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdCH_RowDeleting" OnSelectedIndexChanging="grdCH_SelectedIndexChanging">
                                <Columns>
                                    <asp:TemplateField HeaderText="No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrscSeq" runat="server" Text='<%# Eval("rscSeq") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    
                                    <asp:TemplateField HeaderText="Bank">
                                        <ItemTemplate>
                                            <asp:Label ID="lblbanName" runat="server" Text='<%# Eval("banName") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trans No">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrscDocNo" runat="server" Text='<%# Eval("rscDocNo") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Trans Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrscDocDate" runat="server" Text='<%# Eval("rscDocDate","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CH Due Date">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrscDueDate" runat="server" Text='<%# Eval("rscDueDate","{0:d/M/yyyy}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblrscAmount" runat="server" Text='<%# Eval("rscAmount","{0:n0}") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSeqID" runat="server" Text='<%# Eval("SeqID") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="false">
                                        <ItemTemplate>
                                            <asp:Label ID="lblBankID" runat="server" Text='<%# Eval("BankID") %>'></asp:Label>
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
    <div class="navi">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="New" />
                    </td>
                    <td>
                        <asp:Button ID="btsave" runat="server" CssClass="button2 save"   OnClick="btsave_Click" Text="SAVE" />
                    </td>
                    <td>
                        <asp:Button ID="btDelete" runat="server" CssClass="button2 delete" OnClick="btDelete_Click" Text="Delete" />
                    </td>
                    <td>
                        <asp:Button ID="btprint" runat="server" CssClass="button2 print" OnClick="btprint_Click"  Text="Print" />
                    </td>
                </tr>
                    </table>
    </div>
    </asp:Content>

