<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_reqitemlist.aspx.cs" Inherits="fm_reqitemlist" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style>
        .main-content #mCSB_2_container{
            min-height: 520px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader"> Verify Item Request </div>
    <div class="h-divider"></div>

    <div class="container-fluid ">
        <div class="divgrid row">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                     <asp:GridView ID="grd" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" ForeColor="#333333" GridLines="None" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging" PageSize="5">
                         <AlternatingRowStyle />
                         <Columns>
                            <asp:TemplateField HeaderText="Request No.">
                                <ItemTemplate>
                                    <asp:Label ID="lbrequestno" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Arabic">
                                <ItemTemplate><%# Eval("item_arabic") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Size">
                                <ItemTemplate><%# Eval("size") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Branded"></asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ITEM NO.">
                                <HeaderStyle BackColor="#FFFFCC" ForeColor="Black" />
                                <ItemTemplate>Click Edit</ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txitemno" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" SelectText="Detail" ShowSelectButton="True" />
                        </Columns>
                         <EditRowStyle CssClass="table-edit" />
                         <FooterStyle CssClass="table-footer" />
                         <HeaderStyle CssClass="table-header" />
                         <PagerStyle CssClass="table-page"/>
                         <RowStyle />
                         <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                         <SortedAscendingCellStyle BackColor="#E9E7E2" />
                         <SortedAscendingHeaderStyle BackColor="#506C8C" />
                         <SortedDescendingCellStyle BackColor="#FFFDF8" />
                         <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
                </ContentTemplate>
            </asp:UpdatePanel>
       
        </div>

        <div class="divgrid row">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="clearfix form-group col-sm-6">
                        <label class="col-sm-2 control-label">Short Name </label>
                        <div class="col-sm-10">
                            <asp:TextBox ID="txshortname" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix form-group col-sm-6">
                        <label class="col-sm-2 control-label">UOM </label>
                        <div class="col-sm-10 drop-down">
                            <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix form-group col-sm-6">
                        <label class="col-sm-2 control-label">Packing </label>
                        <div class="col-sm-10 drop-down">
                            <asp:DropDownList ID="cbpacking" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix form-group col-sm-6">
                        <label class="col-sm-2 control-label">Remark </label>
                        <div class="col-sm-10">
                             <asp:TextBox ID="txremark" runat="server" CssClass="form-control"></asp:TextBox>
                        </div>
                    </div>
                    <div class="clearfix form-group col-sm-6">
                        <label class="col-sm-2 control-label">Product Type </label>
                        <div class="col-sm-10 drop-down">
                            <asp:DropDownList ID="cbprodtype" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix form-group col-sm-6">
                        <label class="col-sm-2 control-label">Vendor </label>
                        <div class="col-sm-10 drop-down">
                            <asp:DropDownList ID="cbvendor" runat="server" CssClass="form-control"></asp:DropDownList>
                        </div>
                    </div>
                    <div class="clearfix form-group col-sm-6">
                        <label class="col-sm-2 control-label">Start Date </label>
                        <div class="col-sm-10 drop-down-date">
                            <asp:TextBox ID="dtstart" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:CalendarExtender CssClass="date" ID="dtstart_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtstart">
                                </asp:CalendarExtender>
                        </div>
                    </div>
                    <div class="clearfix form-group col-sm-6">
                        <label class="col-sm-2 control-label">End Date </label>
                        <div class="col-sm-10 drop-down-date">
                            <asp:TextBox ID="dtEnd" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dtEnd_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtEnd">
                            </asp:CalendarExtender>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="navi row margin-bottom margin-top">
            <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn-warning btn btn-save" />
        </div>
    </div>

    
</asp:Content>

