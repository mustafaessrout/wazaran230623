<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="grp_vendorlist.aspx.cs" Inherits="grp_vendorlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Group Vendor</h2>
    <asp:GridView ID="grd" runat="server" Width="100%" AutoGenerateColumns="False" style="font-size:small;border:none" GridLines="None" CellPadding="4" ForeColor="#333333" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDeleting="grd_RowDeleting" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:TemplateField HeaderText="GROUP CODE">
                <ItemTemplate><%# Eval("grp_cd") %>
                <asp:HiddenField runat="server" id="hdgrp_cd" value='<%# Eval("grp_cd") %>'></asp:HiddenField>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="GROUP NAME">
                <ItemTemplate><%# Eval("grp_nm") %></ItemTemplate>
                 <EditItemTemplate>
                                <asp:TextBox ID="txgrp_nm" runat="server"></asp:TextBox>
                                                 </EditItemTemplate>

            </asp:TemplateField>
            <asp:TemplateField HeaderText="OPT 1">
                <ItemTemplate><%# Eval("udf1") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OPT 2">

                <ItemTemplate><%# Eval("udf2") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ACTIVE">
                <ItemTemplate><%# Eval("isactive") %></ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ACTION" ShowHeader="False">
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
    <div class="navi">
        <asp:Button ID="btadd" runat="server" Text="NEW"  style="border-radius:0" Height="26px" Width="83px" OnClick="btadd_Click" CssClass="button2 add"/>
        <asp:Button ID="btprint" runat="server" Text="PRINT"  style="border-radius:0" Height="26px" Width="83px" OnClick="btprint_Click" CssClass="button2 print"/></div>
    <br />
    <img src="div2.png" class="divid" />
    </asp:Content>

