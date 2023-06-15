<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstpricelevel2.aspx.cs" Inherits="fm_mstpricelevel2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function ItemSelected(sender, e)
        {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    <style type="text/css">
        .showdiv
        {
             border-color:#0094ff;
             border-style:groove;
             padding:10px;
             display:normal;
        }

        .hiddiv {
            display:none;
        }
    </style>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div><strong>Master Price Level</strong></div>
    <img src="div2.png" alt="x" class="divid">
    <div style="padding:10px">
        <asp:TextBox ID="txsearchitem" runat="server" Width="50%" CssClass="makeitupper"></asp:TextBox>
        <asp:AutoCompleteExtender ID="txsearchitem_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" 
            TargetControlID="txsearchitem" UseContextKey="True" FirstRowSelected="false" 
            CompletionInterval="10" CompletionSetCount="1" OnClientItemSelected="ItemSelected"> 
        </asp:AutoCompleteExtender>
        <asp:Button ID="btsearch" runat="server" Text="SEARCH" CssClass="button2 search" OnClick="btsearch_Click" />
        <asp:HiddenField ID="hditem" runat="server" />
    </div>    
    <div id="dv" class="hiddiv">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:Label ID="lbitemcode" runat="server" Text="" CssClass="auto-style1"></asp:Label>
                <asp:Label ID="lbitemname" runat="server" Text="" CssClass="auto-style1"></asp:Label>
                <asp:Label ID="lbsize" runat="server" CssClass="auto-style1" Text=""></asp:Label>
                <asp:DropDownList ID="cbpricelevel" runat="server" ToolTip="Pricelevel Code"></asp:DropDownList>
                <asp:TextBox ID="txprice" runat="server" ToolTip="Price" Width="5%"></asp:TextBox>
                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="button2 add" OnClick="btadd_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
       
       
       
    </div>
    <div style="padding:10px">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdpricelevel" runat="server" AutoGenerateColumns="False" OnRowDeleting="grdpricelevel_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="PL Code">
                            <ItemTemplate><%# Eval("pricelevel_cd") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PL Name">
                            <ItemTemplate><%# Eval("pricelevel_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Unit Price">
                            <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date">
                            <ItemTemplate><%# Eval("start_dt","{0:dd-MMM-yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date">
                             <ItemTemplate><%# Eval("end_dt","{0:dd-MMM-yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btsearch" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
    <div class="navi">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" />
    </div>
</asp:Content>

