<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashoutlist.aspx.cs" Inherits="fm_cashoutlist" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style>
         .modal
        {
            position: fixed;
            z-index: 999;
            height: 100%;
            width: 100%;
            top: 0;
            background-color: Black;
            filter: alpha(opacity=60);
            opacity: 0.6;
            -moz-opacity: 0.8;
        }
        .center
        {
            z-index: 1000;
            margin: 300px auto;
            padding: 10px;
            width: 130px;
            background-color: White;
            border-radius: 10px;
            filter: alpha(opacity=100);
            opacity: 1;
            -moz-opacity: 1;
        }
        .center img
        {
            height: 128px;
            width: 128px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Cashout Request</div>
    <div class="h-divider"></div>
   
     <div class="container-fluid">
        <div class="divgrid row">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grd" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" CellPadding="4" GridLines="None" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnRowEditing="grd_RowEditing">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Request No.">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hdcashout" Value='<%# Eval("ids") %>' runat="server" />
                                    <asp:Label ID="lbrequestno" runat="server" Text='<%# Eval("casregout_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Salespoint">
                                 <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                             </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <%# Eval("cashout_dt","{0:d/M/yyyy}") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approve/Reject">
                                <ItemTemplate>
                                    <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("cashout_sta_nm") %>'></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                      <asp:DropDownList ID="cbstatus" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowSelectButton="True" ShowEditButton="True" />
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page" />
                        <RowStyle />
                        <SelectedRowStyle BackColor="Gray" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
        
        </div>
    </div>
    
    <div class="divheader subheader subheader-bg margin-top">Detail</div>

    <div class="container-fluid">
        <div class="divgrid row">
            <asp:UpdateProgress ID="UpdateProgress1" runat="server" AssociatedUpdatePanelID="UpdatePanel1">
                <ProgressTemplate>
                    <div class="modal">
                        <div class="center">
                            <img alt="" src="loader.gif" />
                        </div>
                    </div>
                </ProgressTemplate>
            </asp:UpdateProgress>

            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <asp:GridView ID="grddtl" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                        <AlternatingRowStyle />
                        <Columns>
                            <asp:TemplateField HeaderText="Cashout Code">
                                <ItemTemplate><%# Eval("itemco_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate><%# Eval("itemco_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Ref">
                                <ItemTemplate><%# Eval("ref_no") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PIC">
                                <ItemTemplate><%# Eval("emp_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approval"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Asset"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Claim To HO"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Routine"></asp:TemplateField>
                        </Columns>
                        <EditRowStyle CssClass="table-edit" />
                        <FooterStyle CssClass="table-footer" />
                        <HeaderStyle CssClass="table-header" />
                        <PagerStyle CssClass="table-page"/>
                        <RowStyle/>
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grd" EventName="SelectedIndexChanging" />
                </Triggers>
            </asp:UpdatePanel>
       
        </div>
    </div>
    
</asp:Content>

