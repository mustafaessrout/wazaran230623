<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_proposal.aspx.cs" Inherits="lookup_proposal" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/anekabutton.css" rel="stylesheet" />
</head>
<body style="font-family:Calibri">
    <form id="form1" runat="server">
    <div>
       <div class="divheader">
           Proposal List
       </div>
       <div>
           Proposal No.<asp:TextBox ID="txsearchprop" runat="server" Width="20em"></asp:TextBox><asp:Button ID="btsearch" runat="server" CssClass="button2 search" OnClick="btsearch_Click" />
       </div>
    </div>
      <img src ="div2.png" class="divid" />
        <div class="divgrid">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" PageSize="30">
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" Wrap="True" />
                <Columns>
                    <asp:TemplateField HeaderText="Proposal No.">
                        <ItemTemplate>
                          <a href="javascript:window.opener.RefreshData('<%# Eval("proposal") %>','<%# Eval("benefitpromotion") %>','<%# Eval("disc_cd") %>');window.close();">
                              <asp:Label ID="lbtono" runat="server" Text='<%# Eval("proposal") %>'></asp:Label></a>  
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Discount Code">
                        <ItemTemplate><%# Eval("disc_cd") %></ItemTemplate>
                    </asp:TemplateField>
  
                    <asp:TemplateField HeaderText="End Date">
                        <ItemTemplate><%# Eval("end_dt","{0:dd/MM/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Program">
                        <ItemTemplate><%# Eval("remark") %></ItemTemplate>
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
        </div>
    </form>
</body>
</html>
