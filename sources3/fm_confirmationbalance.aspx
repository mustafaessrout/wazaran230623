<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_confirmationbalance.aspx.cs" Inherits="fm_confirmationbalance" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            height: 20px;
        }
        .auto-style3 {
        }
        .auto-style4 {
            height: 20px;
            width: 108px;
        }
        .auto-style5 {
            width: 6px;
        }
        .auto-style6 {
            height: 20px;
            width: 6px;
        }
        .auto-style7 {
        }
        .auto-style8 {
            height: 20px;
            width: 245px;
        }
        .auto-style9 {
            width: 140px;
        }
        .auto-style10 {
            height: 20px;
            width: 140px;
        }
    </style>
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch4.ClientID%>').click();
        }
        function RefreshData(socb) {
            $get('<%=hdcb.ClientID%>').value = socb;
            $get('<%=txConfirmno.ClientID%>').value = socb;
            $get('<%=btrefresh.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="divheader">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
        Confirmation Balance Print
                             
    <asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red" style="padding:10px 10px 10px 10px"></asp:Label>
    </ContentTemplate>
                                 </asp:UpdatePanel>
                        </div>
    <img src="div2.png" class="divid" />
    <div>



        <table class="auto-style1">
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="lbConfirmno" runat="server" Text="Confirm NO."></asp:Label>
                </td>
                <td class="auto-style6">:</td>
                <td class="auto-style8">
                                    <asp:UpdatePanel ID="UpdatePanel22" runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="txConfirmno" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                        <asp:Button ID="btsearch3" runat="server" CssClass="button2 search" OnClientClick="openwindow3();return(false);" OnClick="btsearch3_Click" />
                    <asp:HiddenField ID="hdcb" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                </td>
                <td class="auto-style10">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                    <asp:Label ID="lbConfirmDate" runat="server" Text="Confirm Date"></asp:Label>
                        </ContentTemplate>
                        </asp:UpdatePanel>
                </td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="dtConfirmDate" runat="server"></asp:TextBox>
                    <asp1:CalendarExtender ID="dtConfirmDate_CalendarExtender" runat="server" BehaviorID="dtConfirmDate_CalendarExtender" TargetControlID="dtConfirmDate" Format="d/M/yyyy">
                    </asp1:CalendarExtender>
                        </ContentTemplate></asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="lbCust" runat="server" Text="Customer Name"></asp:Label>
                </td>
                <td class="auto-style5">:</td>
                <td class="auto-style7">
                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                    <asp:HiddenField ID="hdcust" runat="server" />
                    <asp:TextBox ID="txcust" runat="server" Width="300px"></asp:TextBox>                 
                    <asp1:AutoCompleteExtender ID="AutoCompleteExtender" runat="server" TargetControlID="txcust" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionListCust"  UseContextKey="True" CompletionListElementID="divwidth1" ShowOnlyCurrentWordInCompletionListItem="true">
                        </asp1:AutoCompleteExtender>
                        </ContentTemplate>
                         </asp:UpdatePanel>
                     <div id="divwidthi">

                </div>
                </td>
                <td class="auto-style9">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                    <asp:Label ID="lbConfirmValue" runat="server" Text="Confirm Value"></asp:Label>
                        </ContentTemplate></asp:UpdatePanel>
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="txConfirmValue" runat="server"></asp:TextBox>
                       </ContentTemplate></asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="lbPrintDate" runat="server" Text="Print Date"></asp:Label>
                </td>
                <td class="auto-style5">:</td>
                <td class="auto-style7">
                                         <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="dtPrintDate" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                    <asp1:CalendarExtender ID="dtPrintDate_CalendarExtender" runat="server" BehaviorID="dtPrintDate_CalendarExtender" Format="d/M/yyyy" TargetControlID="dtPrintDate">
                    </asp1:CalendarExtender>
                        </ContentTemplate>
                                             </asp:UpdatePanel>
                </td>
                <td class="auto-style9">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                    <asp:Label ID="lbDoc" runat="server" Text="Upload Doc."></asp:Label>
                    <asp:Label ID="lbdocv" runat="server" Text="Confirm Doc." Visible="False"></asp:Label>
                        </ContentTemplate></asp:UpdatePanel>
                </td>
                <td class="auto-style5">&nbsp;</td>
                <td>
                                        <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                    <ContentTemplate>
      <asp:FileUpload ID="upl" runat="server"></asp:FileUpload>
                        <asp:HyperLink ID="hpfile_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
      <asp:Label ID="lbfileloc" runat="server" Text='Confirmation Document'></asp:Label></asp:HyperLink>
                        </ContentTemplate>
                                            <Triggers>
          <asp:PostBackTrigger ControlID="btConfirm"  />
      </Triggers>
                                        </asp:UpdatePanel>           
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    <asp:Label ID="lbBalance" runat="server" Text="Balance"></asp:Label>
                </td>
                <td class="auto-style6">:</td>
                <td class="auto-style8">
                    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                        <ContentTemplate>
                    <asp:TextBox ID="txBalance" runat="server" CssClass="makeitreadonly"></asp:TextBox>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;</td>
                <td class="auto-style10">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style2">
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                    <asp:Button ID="btConfirm" runat="server" CssClass="button2 save" Text="Confirm" OnClick="btConfirm_Click" />
                        </ContentTemplate>                              
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="auto-style3">
                    <asp:Label ID="lbRemark" runat="server" Text="Remark"></asp:Label>
                </td>
                <td class="auto-style5">:</td>
                <td class="auto-style7" colspan="4">
                                         <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                    <asp:TextBox ID="txremark" runat="server" TextMode="MultiLine" Width="525px"></asp:TextBox>
                        </ContentTemplate>
                                             </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="auto-style4">
                    &nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style8">
                                     
                </td>
                <td class="auto-style10">&nbsp;</td>
                <td class="auto-style6">&nbsp;</td>
                <td class="auto-style2"></td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">&nbsp;</td>
                <td class="auto-style7">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                        <asp:Button ID="btsearch4" runat="server" CssClass="button2 search" OnClick="btsearch4_Click" style="display:none"/>
                            <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" style="display:none"/>
                        </ContentTemplate>                        
                    </asp:UpdatePanel>
                </td>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style3" colspan="6">
                    
                </td>
            </tr>
        </table>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" ShowFooter="True" OnRowDataBound="grd_RowDataBound" AutoGenerateColumns="False" >
                        <Columns>                            
                            
                            <asp:TemplateField HeaderText="INV NO">
                                <ItemTemplate>
                                <%# Eval("inv_no") %>
                                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="INV Date">
                                <ItemTemplate>
                                <%# Eval("inv_dt") %>
                                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Manual NO">
                                <ItemTemplate>
                                <%# Eval("manual_no") %>
                                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Tot Amount">
                                <ItemTemplate>
                                <%# Eval("totamt") %>
                                    </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Balance">
                                <ItemTemplate>
                        <asp:Label ID="lbbalance" runat="server" Text='<%# Eval("balance","{0:f2}") %>'></asp:Label>
                                    </ItemTemplate>
                                <FooterTemplate>
                        <asp:Label ID="lbtotbalance" runat="server"></asp:Label>                                   
                            </FooterTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>   
        <div class="navi">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <ContentTemplate>
                    <asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="New" />
            <asp:Button ID="btPrint" runat="server" CssClass="button2 print" Text="Print" OnClick="btPrint_Click" />
            <asp:Button ID="btsave" runat="server" CssClass="button2 save" Text="Save" OnClick="btsave_Click" />
                </ContentTemplate>
                        </asp:UpdatePanel>   

        </div>
    </div>
    <img src="div2.png" class="divid" />
</asp:Content>

