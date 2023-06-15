<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_pobranchlist.aspx.cs" Inherits="fm_pobranchlist" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function PoSelected(sender, e)
        {
            $get('<%=hdpo.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        PO Branches
    </div>
    <div>
        <img src="div2.png" class="divid" />
    </div>
    <div>
        PO Status : <asp:DropDownList ID="cbpostatus" runat="server" Width="300px"></asp:DropDownList>
    </div>
    <div style="padding-top:5px;padding-bottom:5px">
        PO Search : 
        <asp:HiddenField ID="hdpo" runat="server" />
        <asp:TextBox ID="txsearch" runat="server" Width="300px" CssClass="makeitupper"></asp:TextBox>
        <asp:AutoCompleteExtender ID="txsearch_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsearch" UseContextKey="True" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" OnClientItemSelected="PoSelected">
        </asp:AutoCompleteExtender>
        <asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" />
    </div>
    <div>
        <asp:GridView ID="grdpo" runat="server" AutoGenerateColumns="False" Width="100%" AllowPaging="True" GridLines="Horizontal" OnSelectedIndexChanged="grdpo_SelectedIndexChanged" OnPageIndexChanging="grdpo_PageIndexChanging" CellPadding="0" CssClass="mygrid">
            <Columns>
                <asp:TemplateField HeaderText="PO No.">
                    <ItemTemplate>
                        <asp:Label ID="lbpono" runat="server" Text='<%# Eval("po_no") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate><%# Eval("po_dt","{0:dd-MMM-yyyy}") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Vendor">
                    <ItemTemplate><%# Eval("vendor_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate><%# Eval("po_sta_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark">
                    <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
            </Columns>

        </asp:GridView>
    </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btnew" runat="server" Text="New PO" CssClass="button2 add" OnClick="btnew_Click" />
    </div>
</asp:Content>

