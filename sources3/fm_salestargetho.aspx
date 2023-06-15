<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salestargetho.aspx.cs" Inherits="fm_salestargetho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %><!--By othman-->
    <div class="divheader">
        Sales Target Head Office
    </div>
    <img src="div2.png" class="divid" />
    <table>
        <tr>
            <td>Month</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbmonth" runat="server" AutoPostBack="True"></asp:DropDownList></td>
            <td>Year</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbyear" runat="server" AutoPostBack="True" Width="4em"></asp:DropDownList></td>
        </tr>
        <tr>
            <td>
                Salespoint
            </td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbsalespoint" runat="server" AutoPostBack="True" Width="20em"></asp:DropDownList></td>
            <td>Target Code</td>
            <td>:</td>
            <td>
                <asp:Label ID="lb_taget_cd" runat="server" Font-Bold="True" Font-Size="Medium" ForeColor="Red">New</asp:Label>
            </td>
        </tr>
        </table>
    <table>
        <tr style="background-color:silver">
            <td>
                &nbsp;</td>
            <td>&nbsp;</td>
            <td>
                Group 1</td>
            <td>
                Group 2</td>
            <td>
                &nbsp;</td>
            <td>
                Group 3</td>
            <td>
                Quantity</td>
            <td>
                UOM</td>
            <td class="auto-style1">
                &nbsp;</td>
                </tr>
        <tr>
            <td>
                Product</td>
            <td>:</td>
            <td>
                <asp:DropDownList ID="cbprod1" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbprod1_SelectedIndexChanged" Height="22px" Width="10em">
                </asp:DropDownList>
            </td>
            <td>
                <asp:DropDownList ID="cbprod2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbprod2_SelectedIndexChanged" Height="22px" Width="10em">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;</td>
            <td>
                <asp:DropDownList ID="cbprod3" runat="server" Height="22px" Width="10em">
                </asp:DropDownList>
            </td>
            <td>
                <asp:TextBox ID="txqty" runat="server" Width="5em"></asp:TextBox>
                 <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" targetcontrolid="txqty"
                  filtertype="Numbers" validchars="0123456789" />
            </td>
            <td>
                <asp:DropDownList ID="cbuom" runat="server" Height="22px" Width="10em" Enabled="False">
                </asp:DropDownList>
            </td>
            <td class="auto-style1">
                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click"/>
            </td>
        </tr>
        <tr>
            <td colspan="9">
                <img src="div2.png" class="divid" />
    <div class="divgrid">
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="100%" OnRowUpdating="grd_RowUpdating" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowDeleting="grd_RowDeleting" CellPadding="0" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Salespoint">
                   <ItemTemplate><asp:HiddenField runat="server" id="hdsalespoint_nm" value='<%# Eval("salespointcd") %>'></asp:HiddenField><asp:Label ID="lbsalespoint_nm" runat="server" Text='<%# Eval("salespoint_nm") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Product">
                    <ItemTemplate><asp:HiddenField runat="server" id="hdprod_nm" value='<%# Eval("prod_cd") %>'></asp:HiddenField><asp:Label ID="lbprod_nm" runat="server" Text='<%# Eval("prod_nm") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                    <EditItemTemplate>
                                <asp:TextBox ID="txqty" runat="server"></asp:TextBox>
                                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" targetcontrolid="txqty"
                  filtertype="Numbers" validchars="0123456789" />
                                
                            </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                    <EditItemTemplate>
                        <asp:LinkButton ID="LinkButton4" runat="server" CausesValidation="True" CommandName="Update" Text="Update" OnClientClick="return confirm('Are you sure you want to update?'); "></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton3" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel"></asp:LinkButton>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Edit" Text="Edit"></asp:LinkButton>
                        &nbsp;<asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?'); "></asp:LinkButton>
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
    </div></td>
        </tr>
        <tr>
            <td colspan="9"  style="text-align:center">
                <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
                <asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="New" />
            </td>
        </tr>
        </table>
    
</asp:Content>

