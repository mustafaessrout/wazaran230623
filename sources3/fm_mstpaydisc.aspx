<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstpaydisc.aspx.cs" Inherits="fm_mstpaydisc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Payment Discount Schema
    </div>
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <table>
            <tr>
                <td>
                    Payment Discount No.
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txpaydiscno" runat="server"></asp:TextBox>
                </td>
                <td>
                    Date
                </td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="dtpaydisc" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Percentage</td>
                <td>:</td>
                <td style="margin-left: 40px">
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                </td>
                <td>
                    Qty Canvas</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Start Date</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                </td>
                <td>
                    End Date</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Delivery Date</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                </td>
                <td>
                    Max Duration</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txduration" runat="server"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>
                    <asp:DropDownList ID="cbsp" runat="server" Width="20em"></asp:DropDownList><asp:CheckBox ID="chall" runat="server" Text="All" />
                    <asp:Button ID="btaddsp" runat="server" Text="Add" CssClass="button2 add" OnClick="btaddsp_Click" />
                </td>
            </tr>
        </table>
        <img src="div2.png" class="divid" />
        <table>
            <tr><td style="vertical-align:top">Valid For Salespoint</td>
                <td style="vertical-align:top">:</td>
                <td style="vertical-align:top">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                              <asp:GridView ID="grdsp" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="grdsp_RowDeleting">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="Salespoint Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salespoint Name">
                                <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
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
                  

                </td>
            </tr>
            <tr><td style="vertical-align:top">Valid For Customer</td>
                <td style="vertical-align:top">:</td>
                <td style="vertical-align:top">

                    <asp:GridView ID="grdcust" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle BackColor="White" />
                        <Columns>
                            <asp:TemplateField HeaderText="Code"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Name"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Address"></asp:TemplateField>
                            <asp:TemplateField HeaderText="City"></asp:TemplateField>
                        </Columns>
                        <EditRowStyle BackColor="#7C6F57" />
                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#E3EAEB" />
                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                    </asp:GridView>
                  

                </td>
            </tr>
            <tr><td style="vertical-align:top">Valid For Group Customer</td>
                <td style="vertical-align:top">:</td>
                <td style="vertical-align:top">

                    <asp:GridView ID="grdcustgrd" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Group Code"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Name"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                  

                </td>
            </tr>
            <tr><td style="vertical-align:top">Valid For Customer Type</td>
                <td style="vertical-align:top">:</td>
                <td style="vertical-align:top">

                    <asp:GridView ID="grdcusttype" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Code"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Type Name"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                  

                </td>
            </tr>
            <tr><td style="vertical-align:top">Group Product </td>
                <td style="vertical-align:top">:</td>
                <td style="vertical-align:top">

                    <asp:GridView ID="grdgroupitem" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Code"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Item Name"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                  

                </td>
            </tr>
            <tr><td style="vertical-align:top">Item</td>
                <td style="vertical-align:top">:</td>
                <td style="vertical-align:top">

                    <asp:GridView ID="grditem" runat="server" AutoGenerateColumns="False">
                        <Columns>
                            <asp:TemplateField HeaderText="Code"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name"></asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                  

                </td>
            </tr>
        </table>
    </div>
    <div class="navi">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
    </div>
</asp:Content>

