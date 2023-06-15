<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_loadingsales.aspx.cs" Inherits="fm_loadingsales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function openwindow(url)
        {
            window.open(url, url, "toolbar=no;fullscreen=1", true);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Loading Warehouse
    </div>
    <img src="div2.png" class="divid" />
    <div>
        <table>
            <tr>
                <td>Sales Order No.</td>
                <td>:</td>
                <td>
                    <asp:TextBox ID="txsearchorder" runat="server" Width="200px"></asp:TextBox>
                    <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" />
                </td>
            </tr>
        </table>
    </div>
     <div class="divgrid">
         <asp:UpdatePanel ID="UpdatePanel2" runat="server">
             <ContentTemplate>

                 <asp:GridView ID="grd" runat="server" AllowPaging="True" AutoGenerateColumns="False" EmptyDataText="No Data" ForeColor="#333333" GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanged="grd_SelectedIndexChanged" PageSize="5" Width="100%">
                     <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                     <Columns>
                         <asp:TemplateField HeaderText="SO No.">
                             <ItemTemplate>
                                 <asp:Label ID="lbsocode" runat="server" Text='<%# Eval("so_cd") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Date">
                             <ItemTemplate>
                                 <%# Eval("so_dt","{0:d/M/yyyy}") %>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Status">
                             <ItemTemplate>
                                 <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("so_sta_nm") %>'></asp:Label>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Customer">
                             <ItemTemplate>
                                 <%# Eval("cust_nm") %>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Salesman">
                             <ItemTemplate>
                                 <%# Eval("emp_nm") %>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Ref No.">
                             <ItemTemplate>
                                 <%# Eval("ref_no") %>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:TemplateField HeaderText="Remark">
                             <ItemTemplate>
                                 <%# Eval("remark") %>
                             </ItemTemplate>
                         </asp:TemplateField>
                         <asp:CommandField SelectText="Loading" ShowSelectButton="True" />
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
     </div>
    <img src="div2.png" class="divid" />
    <div>
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                   <h2>SO No. <asp:Label ID="lbsono" runat="server" Text="-" Font-Bold="True" Font-Size="Large" ForeColor="Red" style="padding:10px 10px 10px 10px"></asp:Label>&nbsp;<asp:Label ID="lbstatus" runat="server" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="Large"></asp:Label>
                   </h2>
            </ContentTemplate>
        </asp:UpdatePanel>
     
    </div>
    <img src="div2.png" class="divid" />
    <div>
       <table>
           <tr>
               <td >
                   Loading Sheet No.</td>
               <td>
                   :</td>
               <td style="margin-left: 40px">
                   <asp:TextBox ID="txloadingno" runat="server" CssClass="makeitreadonly"></asp:TextBox>
               </td>
               <td>
                   </td>
               <td>
                   &nbsp;</td>
               <td>
                   &nbsp;</td>
           </tr>
           <tr>
               <td >
                   Manual No.
               </td>
               <td>
                   :
               </td>
               <td style="margin-left: 40px">
                   <asp:TextBox ID="txmanualno" runat="server"></asp:TextBox>
               </td>
               <td>
                   Date</td>
               <td>
                   :</td>
               <td>
                   <asp:TextBox ID="dtloading" runat="server" Height="19px" CssClass="makeitreadonly"></asp:TextBox>
               </td>
           </tr>
           <tr>
               <td >
                   Warehouse</td>
               <td>
                   :</td>
               <td style="margin-left: 40px">
                   <asp:DropDownList ID="cbwhs" runat="server" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged" Width="200px" AutoPostBack="True">
                   </asp:DropDownList>
               </td>
               <td>
                   Bin</td>
               <td>
                   :</td>
               <td>
                   <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                       <ContentTemplate>

                           <asp:DropDownList ID="cbbin" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbbin_SelectedIndexChanged" Width="200px">
                           </asp:DropDownList>

                       </ContentTemplate>
                       <Triggers>
                           <asp:AsyncPostBackTrigger ControlID="cbwhs" EventName="SelectedIndexChanged" />
                       </Triggers>
                   </asp:UpdatePanel>
               </td>
           </tr>
           <tr>
               <td >
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
    </div>
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grddtl" runat="server" AutoGenerateColumns="False" Caption="DETAIL ORDER" EmptyDataText="No Data" ForeColor="#333333" GridLines="None" ShowHeaderWhenEmpty="True" Width="100%" OnRowDataBound="grddtl_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Size">
                            <ItemTemplate><%# Eval("size") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branded">
                            <ItemTemplate><%# Eval("branded_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Stock">
                            <ItemTemplate>
                                <asp:Label ID="lbstock" runat="server" Text="0" Font-Bold="True" ForeColor="Red"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Order">
                            <ItemTemplate>
                                <asp:Label ID="lbqtyorder" runat="server" Text='<%# Eval("qty","{0:G92}") %>'></asp:Label>
                                </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Price">
                            <ItemTemplate>
                                <asp:Label ID="lbunitprice" runat="server" Text='<%# Eval("unitprice") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delivered">
                            <ItemTemplate>
                                <asp:Label ID="lbdelivered" runat="server" Text='<%# Eval("qty_delivered") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Qty Loading">
                            <ItemTemplate>
                                <asp:TextBox ID="txqtyloading" runat="server" Text='<%# Eval("qty","{0:G29}") %>' Width="50px"></asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="UOM"></asp:TemplateField>
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
                <asp:AsyncPostBackTrigger ControlID="grd" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="cbbin" EventName="SelectedIndexChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btloading" runat="server" Text="Print Loading" CssClass="button2 print"  OnClick="btloading_Click" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click"/>
    </div>
</asp:Content>

