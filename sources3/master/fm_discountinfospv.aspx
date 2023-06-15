
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_discountinfospv.aspx.cs" Inherits="fm_discountinfospv" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
     <script>
         function openwindow(url)
         {
             mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=800,height=700");
             mywindow.moveTo(330, 130);
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Discount Information (Product Spv)
    </div>
    <img src="div2.png" class="divid" />
    <div style="padding-bottom:2em;padding-top:2em">Discount created by Prod SPV :
        <asp:Label ID="lbspv" runat="server" Text="Label"></asp:Label>
    </div>
    <asp:GridView ID="grd" runat="server" CellPadding="0" CssClass="mygrid" Width="100%" AllowPaging="True" AutoGenerateColumns="False" OnPageIndexChanging="grd_PageIndexChanging" PageSize="20">
        <Columns>
            <asp:TemplateField HeaderText="Disc Cd">
                <ItemTemplate>
                   <a href="javascript:openwindow('fm_discountinfo.aspx?dc=<%# Eval("disc_cd") %>');">  <%# Eval("disc_cd") %> </a>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Proposal No">
                <ItemTemplate><%# Eval("proposal_no") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Remark">
                <ItemTemplate><%# Eval("remark") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Mechanism"><ItemTemplate><%# Eval("discount_mec_nm") %></ItemTemplate></asp:TemplateField>
            <asp:TemplateField HeaderText="Start Date">
                <ItemTemplate><%# Eval("start_dt","{0:d/M/yyyy}") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="End Date">
                <ItemTemplate><%# Eval("end_dt","{0:d/M/yyyy}") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate><%# Eval("disc_sta_nm") %></ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>

