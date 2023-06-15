<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="admin_Default" MasterPageFile="~/admin/adm.master"%>
 <asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <link href="../css/anekabutton.css" rel="stylesheet" />
     <link href="../css/sweetalert.css" rel="stylesheet" />
     <script src="../js/sweetalert.min.js"></script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
   <h3>User Access Setup</h3>
    <h5> Role to be given : <asp:DropDownList ID="cbrole" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbrole_SelectedIndexChanged" CssClass="form-control-static"></asp:DropDownList></h5>
    <img src="../div2.png" class="divid" />
     <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" OnRowDataBound="grd_RowDataBound" Width="100%" CellPadding="4" ForeColor="#333333" GridLines="None" class="mygrid">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Menu Code">
                    <ItemTemplate>
                        <asp:Label ID="lbmenucode" runat="server" Text='<%# Eval("menu_cd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Menu Caption">
                    <ItemTemplate><%# Eval("menu_nm") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Give Access">
                    <ItemTemplate>
                        <asp:CheckBox ID="chaccess" runat="server" />
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
    </div>
        <div class="navi">
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-default" OnClick="btsave_Click" />
        </div>
</asp:Content>
  
   
