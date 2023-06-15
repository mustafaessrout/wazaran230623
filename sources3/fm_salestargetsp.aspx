<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salestargetsp.aspx.cs" Inherits="fm_salestargetsp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function openwindow() {
            var oNewWindow = window.open("lookup_SalesTargetSP.aspx", "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }

        function updpnl() {
            document.getElementById('<%=bttmp.ClientID%>').click();
            return (false);
        }
    </script>
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
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %><!--By othman-->  
    <div class="divheader">
        &nbsp;<asp:UpdatePanel ID="UpdatePanel6" runat="server">
            <ContentTemplate>
                Sales Target Sales Point
                <asp:Label ID="lbstatus" runat="server" BorderStyle="Solid" BorderWidth="1px" ForeColor="Red" style="padding:10px 10px 10px 10px"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>Target No.</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtargetno" runat="server" CssClass="ro">NEW</asp:TextBox>
                        <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClientClick="openwindow();return(false);" />
                    </ContentTemplate>
                     <Triggers><asp:AsyncPostBackTrigger ControlID="bttmp" EventName="Click" /></Triggers>
                </asp:UpdatePanel>
                <asp:Button ID="bttmp" runat="server" Text="Button" OnClick="bttmp_Click" style="display:none" />
                <asp:HiddenField ID="hdtargetno" runat="server" />
            </td>
            <td>Period</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbperiod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged" Width="20em">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Month</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbmonth" runat="server"></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbperiod" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
            <td>Year</td>
            <td>:</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbyear" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
                
            </td>
        </tr>
        <tr>
            <td>
                Salespoint
            </td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbsalespoint" runat="server" OnSelectedIndexChanged="Refreshgridview" Width="10em"></asp:DropDownList></td>
            <td>
                Salesman Name </td>
            <td>:</td>
   
            <td>
                <asp:DropDownList ID="cbsalesmancd" runat="server" OnSelectedIndexChanged="Refreshgridview" Width="20em"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                Level Group</td>
            <td>:</td>
            <td>
                <asp:CheckBox ID="chlevel" runat="server" AutoPostBack="True" Font-Bold="True" ForeColor="Red" OnCheckedChanged="chlevel_CheckedChanged" Text="Group Level 2 Only" />
            </td>
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        </table>
    <table>
        <tr style="background-color:silver">
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                Group 1 </td><td>
                Group 2</td>
            <td>
                &nbsp;</td>
            <td>
                Group 3</td>
            <td>
                Target From HO&nbsp;</td>
            <td>
                Quantity</td>
            <td>
                UOM</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                Product</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbprod1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbprod1_SelectedIndexChanged" Width="15em">
                </asp:DropDownList>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbprod2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbprod2_SelectedIndexChanged" Width="15em">
                </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbprod1" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                           <asp:DropDownList ID="cbprod3" runat="server" Width="15em">
                </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID ="chlevel" EventName="CheckedChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbprod2" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
             
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txtargetho" runat="server" CssClass="ro"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbprod2" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
                
             
            </td>
            <td>
                <asp:TextBox ID="txqty" runat="server" Width="50px"></asp:TextBox>  
                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" targetcontrolid="txqty"
                  filtertype="Numbers" validchars="0123456789" />
              
            </td>
            <td>
                <asp:DropDownList ID="cbuom" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click"/>
            </td>
        </tr>
        <tr>
            <td>Remark</td>
            <td>:</td>
            <td colspan="8">
                <asp:TextBox ID="txremark" runat="server" TextMode="MultiLine" Width="50em"></asp:TextBox></td>
        </tr>
    </table>

   

    <img src="div2.png" class="divid" />
    <div class="divgrid">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" CellPadding="0" ForeColor="#333333" GridLines="None" OnRowDataBound="grd_RowDataBound" OnRowDeleting="grd_RowDeleting" ShowFooter="True">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="No.">
                    <ItemTemplate>
                        <asp:Label ID="lblseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prod Code">
                    <ItemTemplate> 
                       <asp:Label ID="lblprodcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prod Name">
                    <ItemTemplate>
                        <asp:Label ID="lblprodname" runat="server" Text='<%# Eval("prod_nm") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty Target">
                    <ItemTemplate>
                        <div style="text-align: right;">
                        <asp:Label ID="lblqty" runat="server" Text='<%# Eval("qty") %>'></asp:Label>
                        </div>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtqty" runat="server" Text='<%# Eval("qty") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <FooterTemplate>
				    <div style="text-align: right;">
				    <asp:Label ID="lblTotalqty" runat="server" />
				    </div>
			        </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UOM">
                    <ItemTemplate>
                            <asp:Label ID="lblUOM" runat="server" Text='<%# Eval("UOM") %>'></asp:Label>
                    </ItemTemplate>
                   <EditItemTemplate>
                    <asp:DropDownList ID="cboUOM" runat="server" Width="90px">
                    </asp:DropDownList>
                </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark">
                    <ItemTemplate>
                        <asp:Label ID="lblremark" runat="server" Text='<%# Eval("remark") %>'></asp:Label>   
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtremark" runat="server" Text='<%# Eval("remark") %>'></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lbltarget_No" runat="server" Text='<%# Eval("target_No") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                    <ItemTemplate>
                        <asp:Label ID="lblsalespointCD" runat="server" Text='<%# Eval("salespointCD") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" HeaderText="Action" ShowDeleteButton="True" />
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
    <div class="navi">
        <asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="New" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click"/>
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
    </div>
    <div class="divmsg" id="dvshow">
    <img src="loader.gif" />
    </div>
</asp:Content>

