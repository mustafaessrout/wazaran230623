<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_appreturnho.aspx.cs" Inherits="fm_appreturnho" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/styles.css" rel="stylesheet" />
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="form-horizontal">
        <h4 class="jajarangenjang">List Of Return HO Need Approval</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grd" runat="server" CssClass="mGrid" AutoGenerateColumns="False">
                    <Columns>
                        <asp:TemplateField HeaderText="Request Date"></asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Value"></asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

