<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_SkipPrioirtyItemsLog.aspx.cs" Inherits="fm_SkipPrioirtyItemsLog" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        
    </script>
    <div class="divheader">
        Skip Priority Items Log
    </div>
    <img src="div2.png" class="divid" />
    <div>
        Salespoint :
        <asp:dropdownlist id="cbsalespoint" runat="server" width="20em" autopostback="True" onselectedindexchanged="cbsalespoint_SelectedIndexChanged"></asp:dropdownlist>
        &nbsp;Period :
        <asp:dropdownlist id="cbperiod" runat="server" width="10em" autopostback="True" onselectedindexchanged="cbperiod_SelectedIndexChanged">
        </asp:dropdownlist>

    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:updatepanel id="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd"  runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="1" ForeColor="#333333" GridLines="None" >
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction Number ">
                                    <ItemTemplate>
                                        <asp:Label ID="lbTransactionNumber" runat="server" Text='<%# Eval("TransactionNumber") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Branch Name ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBranchName" runat="server" Text='<%# Eval("BranchName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Salesman ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSalesmanName" runat="server" Text='<%# Eval("SalesmanName") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                                                <asp:TemplateField HeaderText="Skip Date ">
                                    <ItemTemplate>
                                        <asp:Label ID="blbapp_dt" runat="server" Text='<%# Eval("app_dt") %>'></asp:Label>
                                    </ItemTemplate>
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
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbperiod" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:updatepanel>

            </td>
        </tr>
    </table>
    <div class="navi">
       <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
       
    </div>
</asp:Content>

