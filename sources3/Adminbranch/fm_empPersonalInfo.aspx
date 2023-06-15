<%@ Page Title="" Language="C#" MasterPageFile="~/Adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_empPersonalInfo.aspx.cs" Inherits="fm_empPersonalInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="divheader">Employees' Login Personal Information</div>
    <div class="h-divider"></div>
    <div class="container">
        <div class="form-group">
            <div class="table">
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="2" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate><%# Container.DataItemIndex + 1 %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Employee">
                            <ItemTemplate>
                                <asp:HiddenField ID="hdids" Value='<%# Eval("infoID") %>' runat="server" />
                                <asp:Label ID="emp" runat="server" Text='<%# Eval("emp") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Id (Iqama) Number">
                            <ItemTemplate><%# Eval("iqamaNumber") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Birth Date d/M/yyyy">
                            <ItemTemplate><%# Eval("dob","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Mobile No">
                            <ItemTemplate><%# Eval("mobilenumber") %></ItemTemplate>
                        </asp:TemplateField>
                        <%--<asp:TemplateField HeaderText="Branch ">
                            <ItemTemplate><%#  Eval("branch") %></ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Profile ">
                            <ItemTemplate><%#  Eval("roleName") %></ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>


</asp:Content>

