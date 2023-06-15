<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmARRecDev.aspx.cs" Inherits="frmARRecDev" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


.makeitreadonly {
    background-color:silver;
}

    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
        <h3><strong>RECEIPT BY SALESMAN</strong></h3>
    </div>
    <table>
        <tr>    
            <td>Receipt Date</td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txrecDate" runat="server" OnTextChanged="txrecDate_TextChanged" Width="130px" CssClass="auto-style3" AutoPostBack="True"></asp:TextBox>
                                <asp:CalendarExtender ID="txrecDate_CalendarExtender" runat="server" TargetControlID="txrecDate" Format="d/M/yyyy">
                                </asp:CalendarExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
            <td>Sales Point</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" CssClass="makeitreadonly" Enabled="False" Height="20px" Width="195px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Payment Type</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbARCType" runat="server" AutoPostBack="True" CssClass="auto-style3" Height="20px" OnSelectedIndexChanged="cbARCType_SelectedIndexChanged" Width="195px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Salesman</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbSalesCD" runat="server" AutoPostBack="True" CssClass="auto-style3" Height="20px" Width="195px" OnSelectedIndexChanged="cbSalesCD_SelectedIndexChanged">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    
        <div>

            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="1031px">
                        <Columns>
                            <asp:TemplateField HeaderText="Receipt No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblARRecCD" runat="server" Text='<%# Eval("ARRecCD") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice CD">
                                <ItemTemplate>
                                    <asp:Label ID="lblSOCD" runat="server" Text='<%# Eval("SOCD") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer">
                                <ItemTemplate>
                                    <asp:Label ID="lblcust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate>
                                    <asp:Label ID="lblARCAmt" runat="server" Text='<%# Eval("ARCAmt","{0:n0}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Bank">
                                <ItemTemplate>
                                    <asp:Label ID="lblbanName" runat="server" Text='<%# Eval("banName") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trans No">
                                <ItemTemplate>
                                    <asp:Label ID="lblARCDocNo" runat="server" Text='<%# Eval("ARCDocNo") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Trans Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblARCDate" runat="server" Text='<%# Eval("ARCDate","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CH Due Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblARCDueDate" runat="server" Text='<%# Eval("ARCDueDate","{0:d/M/yyyy}") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate>
                                    <asp:Label ID="lblARCDescription" runat="server" Text='<%# Eval("ARCDescription") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblSeqID" runat="server" Text='<%# Eval("SeqID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblARCRefID" runat="server" Text='<%# Eval("ARCRefID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="lblBankID" runat="server" Text='<%# Eval("BankID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <SelectedRowStyle BackColor="#3399FF" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    
</asp:Content>

