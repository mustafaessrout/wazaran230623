<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_hrdIncetiveRangeArabic.aspx.cs" Inherits="fm_hrdIncetiveRangeArabic" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">نطاق الحوافز</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0"
                            OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing"
                            OnRowUpdating="grd_RowUpdating">
                            <Columns>
                                <asp:TemplateField HeaderText="نوع الجنسية">
                                    <ItemTemplate>
                                        
                                        <asp:Label ID="lblnationalityType" runat="server" Text='<%#Eval("nationalityTypeValue") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:HiddenField ID="hdfincetiveRangeID" runat="server" Value='<%#Eval("incetiveRangeID")%>' />
                                        <asp:TextBox ID="txnationalityType" Text='<%#Eval("nationalityTypeValue")%>' runat="server" ReadOnly="true"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="نوع الحافز">
                                    <ItemTemplate>
                                        <asp:Label ID="lblincetiveType" runat="server" Text='<%#Eval("incetiveTypeValue") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txincetiveType" Text='<%#Eval("incetiveTypeValue")%>' runat="server" ReadOnly="true"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="الحد الأدنى للنطاق">
                                    <ItemTemplate>
                                        <asp:Label ID="lblminRange" runat="server" Text='<%#Eval("minRange") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txminRange" Text='<%#Eval("minRange")%>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="الحد الأعلى للنطاق">
                                    <ItemTemplate>
                                        <asp:Label ID="lblmaxRange" runat="server" Text='<%#Eval("maxRange") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txmaxRange" Text='<%#Eval("maxRange")%>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="مبلغ الحافز">
                                    <ItemTemplate>
                                        <asp:Label ID="lblincetiveAmount" runat="server" Text='<%#Eval("incetiveAmount") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txincetiveAmount" Text='<%#Eval("incetiveAmount")%>' runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="true" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
