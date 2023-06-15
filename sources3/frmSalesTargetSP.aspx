<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmSalesTargetSP.aspx.cs" Inherits="frmSalesTargetSP" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_SalesTargetDet.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
    <script>
        function openwindow2() {
            var oNewWindow = window.open("fm_lookup_SalesTargetSP.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl2() {
            document.getElementById('<%=bttmp2.ClientID%>').click();
            return (false);
        }
    </script>
    <style type="text/css">


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
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>SALES TARGET BRANCH</h3>
    <table style="width:100%">
        <tr>
            <td>Target Branch Code</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txslsTargetSPCD" runat="server" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp2" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
           </td>
            <asp:Button ID="bttmp2" runat="server" Text="Button" OnClick="bttmp2_Click" style="display:none" />
            <td>
                <strong __designer:mapid="3a">
                <asp:Button ID="btsearch2" runat="server" CssClass="button2 search" OnClientClick="openwindow2();return(false);" style="left: 0px; top: 7px" Text="Search" />
                </strong>
            </td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel19" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txslsTargetSPID" runat="server" ReadOnly="True" Height="22px" style="display:none"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
        </tr>
        <tr><td>Target Code</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txslsTargetCD" runat="server" ReadOnly="True"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txslsTargetCD" ErrorMessage="Fill target code" Font-Bold="True" Font-Size="Medium" ForeColor="Red">**</asp:RequiredFieldValidator>
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
             </td>
            <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
            <td>
                <strong __designer:mapid="3a">
                <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClientClick="openwindow();return(false);" style="left: 0px; top: 7px" Text="..." />
                 </strong>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txrefSlsTargetDetID" runat="server" ReadOnly="True" style="display:none"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Period</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txMonthCD" runat="server" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
           </td>
            <td>Sales Point</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txSalesPointCD" runat="server" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Brand</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txprod_cd" runat="server" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Sub Brand</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txprod_cd2" runat="server" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Brand Name</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txprod_nm" runat="server" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Sub Brand Name</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txprod_nm2" runat="server" ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
           </td>
        </tr>
        <tr>
            <td>Target</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txQty" runat="server"  ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
             </td>
            <td>UOM</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txUOM" runat="server"  ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Remark</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txRemark" runat="server"  ReadOnly="True"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <%--<td colspan="11"></td>--%>
            <tr style="background-color:#0094ff;border-color:yellow;border:none">
               
                <td>Salesman</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbemp_cd" runat="server" Height="22px" Width="215px">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>Quantity</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txSTDQty" runat="server" ReadOnly="false"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>UOM</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbSTDuom" runat="server" Height="22px" Width="100px">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>Remark</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txSTDremark" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                    <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />   
                    </td>
            </tr>
        <tr>
            <td colspan="6">
                                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                                    <ContentTemplate>
                                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" OnSelectedIndexChanged="grd_SelectedIndexChanged" OnSelectedIndexChanging="grd_SelectedIndexChanging" Width="100%">
                                            <Columns>
                                                <asp:TemplateField HeaderText="No.">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField  Visible='false'>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbseqID" runat="server" Text='<%# Eval("seqID") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salesman Code">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbemp_cd" runat="server" Text='<%# Eval("emp_cd") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Salesman Name">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbemp_nm" runat="server" Text='<%# Eval("emp_nm") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Target">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbSTDqty" runat="server" Text='<%# Eval("STDqty","{0:0,00}") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="UOM">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbSTDUOM" runat="server" Text='<%# Eval("STDUOM") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Remark">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbSTDremark" runat="server" Text='<%# Eval("STDremark") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:CommandField ShowDeleteButton="True" ShowSelectButton="True" />
                                            </Columns>
                                            <SelectedRowStyle BackColor="#66FFFF" />
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
        </tr>
        
        <tr>
            <td>TOTAL</td>
            <td colspan="4">
                <asp:UpdatePanel ID="UpdatePanel21" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbltotal" runat="server" style="text-align: right; font-weight: 700;" Width="500px"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>

            </td>
        </tr>
        <tr>
            <td>
                  
              <asp:Button ID="btsave" runat="server" Text="SAVE" CssClass="button2 save" OnClick="btsave_Click"/>
                   
                </td>
            <td>
                <asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="New" />
                </td>
            <td>
                <asp:Button ID="btEdit" runat="server" CssClass="button2 Edit" OnClick="btnew_Click" Text="Edit" />
                </td>
            <td>
                <asp:Button ID="btDelete" runat="server" CssClass="button2 Delete" OnClick="btnew_Click" Text="Delete" />
                </td>
        </tr>
       </table> 
    <footer>

        <asp:UpdatePanel ID="UpdatePanel20" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblfooter" runat="server"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>

    </footer>
</asp:Content>

