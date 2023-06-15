<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmSalesTarget.aspx.cs" Inherits="frmSalesTarget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function openwindow() {
            var oNewWindow = window.open("fm_lookup_SalesTarget.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
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
    background: -moz-linear-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
    border: solid 1px #dcdcdc;
    border-radius: 2px;
    -webkit-border-radius: 2px;
    -moz-border-radius: 2px;
    margin-right: 10px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3> SALES TARGET BY SALESPOINT</h3>
    <p> 
        <table style="width:100%;">
            <tr>
                <td>Target Code</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txslsTargetCD" runat="server" ReadOnly="True"></asp:TextBox>
                            <strong>
                            <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClientClick="openwindow();return(false);" style="left: 0px; top: 7px" Text="Search" />
                            </strong>
                        </ContentTemplate>
                        <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                    </asp:UpdatePanel>
                </td>
                <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                <td>
                    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txslsTargetID" runat="server" ReadOnly="True"  style="display:none ;width: 40px;"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>Period :</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbMonthCD" runat="server" Height="20px" Width="295px" AutoPostBack="true">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td>SalesPoint :</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbSalespointCD" runat="server" Height="20px" Width="295px" AutoPostBack="true">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
            <tr style="background-color:#0094ff;border-color:yellow;border:none">
                <td>Brand</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbprod_cd" runat="server" Height="20px" Width="295px" AutoPostBack="true" OnSelectedIndexChanged="cbprod_cd_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>Sub Brand</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbprod_cd2" runat="server" AutoPostBack="true" Height="22px" Width="294px" OnSelectedIndexChanged="cbprod_cd2_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>Target</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txQty" style="width: 40px;" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>UOM</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbuom" runat="server" Height="22px" Width="110px">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>Remark</td>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txremark" runat="server"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <td>
                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />   
                </td>
             </tr>
            <tr>
                <td colspan="4">
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" Width="100%" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnSelectedIndexChanged="grd_SelectedIndexChanged">
                                    <Columns>
                                        <asp:TemplateField HeaderText="No.">
                                            <ItemTemplate>
                                                <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="ID">
                                            <ItemTemplate>
                                                <asp:Label ID="lbslstargetDetID" runat="server" Text='<%# Eval("slstargetDetID") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbprod_cd" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Brand Name">
                                            <ItemTemplate>
                                                <%# Eval("prod_NM") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SubBrand Code">
                                            <ItemTemplate>
                                                <asp:Label ID="lbprod_cd2" runat="server" Text='<%# Eval("prod_cd2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SubBrand Name">
                                            <ItemTemplate>
                                                <asp:Label ID="lbprod_NM2" runat="server" Text='<%# Eval("prod_NM2") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Target">
                                            <ItemTemplate>
                                                <asp:Label ID="lbqty" runat="server" Text='<%# Eval("qty","{0:0,00}") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="UOM">
                                            <ItemTemplate>
                                                <asp:Label ID="lbUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Remark">
                                            <ItemTemplate>
                                                <asp:Label ID="lbremark" runat="server" Text='<%# Eval("remark") %>'></asp:Label>
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
                <td>            
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <asp:Label ID="lbltotal" runat="server" style="font-weight: 700" Text=""></asp:Label>
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
                    <asp:Button ID="btdel" runat="server" CssClass="button2 Delete" OnClick="btdel_Click" Text="Delete" style="display:none"/>
                </td>
            </tr>
            </Table>
    </p>
    <footer>

                            <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                <ContentTemplate>
                                    <asp:Label ID="lblfooter" runat="server"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </footer>
</asp:Content>

