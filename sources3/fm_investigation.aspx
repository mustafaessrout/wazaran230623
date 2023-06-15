<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_investigation.aspx.cs" Inherits="fm_investigation" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/jquery-ui.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
    <script src="css/jquery-ui.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .divmsg{
       /*position:static;*/
       top:30%;
       right:50%;
       left:50%;
       width:200px;
       height:200px;
       position:fixed;
       /*background-color:greenyellow;*/
       overflow-y:auto;
                }
        .divhid {
            display:none;
        }

        .divnormal {
            display:normal;
        }
    </style> 
    <script>
        function Selecteditem(sender, e) {
            $get('<%=hdivgdrefcd.ClientID%>').value = e.get_value();
            //dv.attributes["class"].value = "showdiv";
        }
    </script>
    <script>
        function SetContextKey() {     
            $find('<%=AutoCompleteExtender1.ClientID%>').set_contextKey($get("<%=cbSalesPointCD.SelectedValue%>").value);
    }
</script>
<script>
    function openwindow() {
        var oNewWindow = window.open("fm_lookup_investigation.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
    }

    function updpnl() {
        document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                Investigation Form
                <asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red" style="padding:10px 10px 10px 10px"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>Ivg No</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txivgno" runat="server" CssClass="makeitreadonly" ReadOnly="True"></asp:TextBox>
                        <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClientClick="openwindow();return(false);" />
                    </ContentTemplate>
                    <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
                <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
            </td>
            <td>SalespointCD</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="makeitreadonly" Height="20px" Width="195px" OnSelectedIndexChanged="cbSalesPointCD_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Date</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txivgdate" runat="server"></asp:TextBox>
                        <asp:CalendarExtender ID="txivgdate_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="txivgdate" TodaysDateFormat="d/M/yyyy">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Type</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbinvtype" runat="server" Width="150" AutoPostBack="True" OnSelectedIndexChanged="cbinvtype_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Investigator Nm</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbemployee" runat="server" AutoPostBack="True" Enabled="False" Width="15em">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Reason</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txivgreason" runat="server" Width="300px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <img src="div2.png" class="divid" />
    <div style="padding-bottom:10px;padding-top:10px">
        <strong>Investigation List</strong>
    </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdlist" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" PageSize="5" Width="80%" ForeColor="#333333" ShowFooter="True" OnRowCancelingEdit="grdlist_RowCancelingEdit" OnRowEditing="grdlist_RowEditing" OnRowUpdating="grdlist_RowUpdating">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbfld_valu" runat="server" Text='<%# Eval("fld_valu") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <%# Eval("fld_desc") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lbivgno" runat="server" Text='<%# Eval("ivgno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Check">
                                     <ItemTemplate>
                                         <asp:Label ID="lblivgcheck" runat="server" Text='<%# Eval("ivgcheck") %>'></asp:Label>
                                     </ItemTemplate>
                                     <EditItemTemplate>
                                         <asp:DropDownList ID="cboivgcheck" runat="server" Width="90px">
                                         </asp:DropDownList>
                                     </EditItemTemplate>
                                 </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                    <EditRowStyle BackColor="#999999" />
                    <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
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
    <table>
        <tr style="background-color:silver;border-color:yellow;border:none">
            <td>RefCD</td>
            <td>Description</td>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtsearchivgdrefcd" runat="server" AutoPostBack="True" OnKeyUp="SetContextKey()" Width="200px" ></asp:TextBox>
                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="Selecteditem" ServiceMethod="GetCompletionList" TargetControlID="txtsearchivgdrefcd" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:HiddenField ID="hdivgdrefcd" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtivgddescription" runat="server" Width="500px"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                        <strong>
                        <asp:Button ID="btadd" runat="server" CssClass="button2 add" OnClick="btadd_Click" style="left: 0px; top: 0px" Text="Add" />
                        </strong>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDataBound="grd_RowDataBound" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" ShowFooter="True" Width="75%">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <asp:Label ID="lblseqno" runat="server" Text='<%# Eval("seqno")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Ref Code">
                            <ItemTemplate>
                                <asp:Label ID="lbivgdrefcd" runat="server" Text='<%# Eval("ivgdrefcd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Description">
                            <ItemTemplate>
                                         <%# Eval("ivgddescription") %>
                                     </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblivgno" runat="server" Text='<%# Eval("ivgno") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblsalespointCD" runat="server" Text='<%# Eval("salespointCD") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblinvtype" runat="server" Text='<%# Eval("invtype") %>'></asp:Label>
                            </ItemTemplate>
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
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">

                <asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="NEW" />
                <asp:Button ID="btsave" runat="server" CssClass="button2 save" OnClick="btsave_Click" Text="Save" OnClientClick="dvshow.setAttribute('class','divmsg');"/>
                <asp:Button ID="btDelete" runat="server" CssClass="button2 delete" OnClick="btDelete_Click" Text="Delete" Visible="False" />
                <asp:Button ID="btprint" runat="server" CssClass="button2 print" OnClick="btprint_Click" Text="Print" />

    </div>
    <div class="divmsg" id="dvshow">
    <img src="loader.gif" />
    </div>
</asp:Content>

