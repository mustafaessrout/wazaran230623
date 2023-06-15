<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_hrdIncetiveRangeMerchandiser.aspx.cs" Inherits="fm_hrdIncetiveRangeMerchandiser" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">Incentive Range</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <label class="control-label col-md-1">Job Title</label>
                <div class="col-md-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbjobtitle" CssClass="form-control" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="cbjobtitle_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <label class="control-label col-md-1">Level</label>
                <div class="col-md-2 drop-down">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cblevel" CssClass="form-control" runat="server" AutoPostBack="True"
                                OnSelectedIndexChanged="cblevel_SelectedIndexChanged">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>

            <div class="form-group">
                <div class="row">
                    <div class="col-md-12">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0"
                                    OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing"
                                    OnRowUpdating="grd_RowUpdating">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Nationality Type">
                                            <ItemTemplate>

                                                <asp:Label ID="lblnationalityType" runat="server" Text='<%#Eval("nationalityTypeValue") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:HiddenField ID="hdfincetiveRangeID" runat="server" Value='<%#Eval("incetiveRangeID")%>' />
                                                <asp:TextBox ID="txnationalityType" Text='<%#Eval("nationalityTypeValue")%>' runat="server" ReadOnly="true"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Incentive Type">
                                            <ItemTemplate>
                                                <asp:Label ID="lblincetiveType" runat="server" Text='<%#Eval("incetiveTypeValue") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txincetiveType" Text='<%#Eval("incetiveTypeValue")%>' runat="server" ReadOnly="true"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Min Range">
                                            <ItemTemplate>
                                                <asp:Label ID="lblminRange" runat="server" Text='<%#Eval("minRange") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txminRange" Text='<%#Eval("minRange")%>' runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Max Range">
                                            <ItemTemplate>
                                                <asp:Label ID="lblmaxRange" runat="server" Text='<%#Eval("maxRange") %>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txmaxRange" Text='<%#Eval("maxRange")%>' runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Incentive Amount">
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
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
