<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_loadinglist.aspx.cs" Inherits="fm_loadinglist" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function DoSelected(sender, e)
        {
            $get('<%=hddo.ClientID%>').value = e.get_value();  
            $get('<%=btload.ClientID%>').click();
        }
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Loading to vehicle / Print Invoice
    </div>

    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>
                Driver&nbsp;
            </td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbdriver" runat="server" OnSelectedIndexChanged="cbdriver_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            </td>
            <td>
                Trip Date
            </td>
            <td>
                :</td>
            <td style="margin-left: 40px">
                <asp:TextBox ID="dttrip" runat="server" CssClass="makeitreadonly"></asp:TextBox>
            </td>
            <td>
                Trip No.</td>
            <td>
                :</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                         <asp:TextBox ID="txtripno" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
               
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td style="margin-left: 40px">
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    <div style="padding:10px 10px 10px 10px;border:1px solid black">
        <table style="width:75%">
            <tr>
                <td style="text-align:right">Vehicle Code</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbvehiclecode" runat="server" Text="Label" Font-Bold="True" ForeColor="#FF3300"></asp:Label></td>
                <td style="text-align:right">
                    Plate No.
                </td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbplate" runat="server" Text="Label" Font-Bold="True" ForeColor="#FF3300"></asp:Label></td>
                <td>
                    Vehicle Type </td>
                <td>
                    :</td>
                <td>
                    <asp:Label ID="lbtype" runat="server" Text="Label" Font-Bold="True" ForeColor="#FF3300"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align:right">Capacity</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbcapacity" runat="server" Text="Label" Font-Bold="True" ForeColor="#FF3300"></asp:Label></td>
                <td style="text-align:right">
                    UOM</td>
                <td>:</td>
                <td>
                    <asp:Label ID="lbuom" runat="server" Text="Label" Font-Bold="True" ForeColor="#FF3300"></asp:Label></td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </div>
    <table style="width:80%">
        <tr style="background-color:silver">
          <td>Loading Sheet No.</td>
          <td>Loading Sheet Manual </td>
          <td>Invoice No.</td>
          <td>Manual Invoice No.</td>
          <td>Total Pallette Loading</td>
          <td>Add</td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txloadingno" runat="server" Width="300px"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txloadingno_AutoCompleteExtender" runat="server" TargetControlID="txloadingno" MinimumPrefixLength="1" ServiceMethod="GetCompletionList" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="DoSelected">
                </asp:AutoCompleteExtender>
                <asp:HiddenField ID="hddo" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                
                
                <div id="divwidth">

                </div>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                          <asp:Label ID="lbloadingmanual" runat="server" Font-Bold="True" ForeColor="Red"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
              
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                          <asp:Label ID="lbinvoiceno" runat="server" Text=""></asp:Label>
                          <asp:Button ID="btload" runat="server" OnClick="btload_Click" Text="Button" style="display:none"/>
                    </ContentTemplate>
                </asp:UpdatePanel>
              
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                         <asp:TextBox ID="txmanualinvoice" runat="server"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
               
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbpallette" runat="server" Text="0"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </td>
            <td>
                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />
            </td>
        </tr>
    </table>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
            <ContentTemplate>

                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" OnSelectedIndexChanging="grd_SelectedIndexChanging" Width="80%" AllowPaging="True" Caption="INVOICE" CaptionAlign="Top">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Loading No.">
                            <ItemTemplate>
                                <asp:Label ID="lbdono" runat="server" Text='<%# Eval("do_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual No.">
                            <ItemTemplate>
                                <%# Eval("ref_no") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Invoice No.">
                            <ItemTemplate>
                                <asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Manual Invoice">
                            <ItemTemplate>
                                <%# Eval("inv_manual_no") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Pallette">
                            <ItemTemplate>
                                <%# Eval("pallette","{0:0.##}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowSelectButton="True" />
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
                <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel9" runat="server">
            <ContentTemplate>

                <asp:GridView ID="grddtl" runat="server" AutoGenerateColumns="False" CellPadding="0" EmptyDataText="NO DATA FOUND" Width="80%" AllowPaging="True" Caption="DETAIL INVOICE" CaptionAlign="Top">
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# Eval("item_cd") %>'>
                                       </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate>
                                <%# Eval("item_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Loading">
                            <ItemTemplate>
                                <%# Eval("qty") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Actual">
                            <ItemTemplate>
                                <asp:Label ID="lbqtyactual" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
                <asp:Button ID="btprint" runat="server" Text="Print Trip Letter" CssClass="button2 print" OnClick="btprint_Click" />
                <asp:Button ID="btprintinvoice" runat="server" CssClass="button2 print" Text="Print Invoice" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

