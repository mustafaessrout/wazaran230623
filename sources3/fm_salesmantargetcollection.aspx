<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesmantargetcollection.aspx.cs" Inherits="fm_salesmantargetcollection" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
     
        </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %><!--By othman-->  
    <div class="divheader">Sales Man Collection Target</div>
    <div class="h-divider"></div>
    <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row padding-bottom">
                    <table class="table table-striped  mygrid row-no-padding">
                        <tr>
                            <th>Menu</th>
                            <th>Month</th>
                            <th>Year</th>
                            <th>&nbsp;</th>
                            <th>Amount</th>
                            <th>Action</th>
                        </tr>
                        <tr>
                            <td >
                                INFO</td>
                  
                            <td class="drop-down-sm">
                                <asp:DropDownList ID="cbmonth" runat="server" AutoPostBack="True" CssClass="form-control input-sm" OnSelectedIndexChanged="refreshgridview"></asp:DropDownList>
                            </td>
                            <td class="drop-down-sm">
                                <asp:DropDownList ID="cbyear" runat="server" AutoPostBack="True" CssClass="form-control input-sm" OnSelectedIndexChanged="refreshgridview" ></asp:DropDownList>
                            </td>
                            <td class="auto-style5">
                                </td>
                            <td class="auto-style5">
                                <asp:Panel runat="server" ID="txqtyPnl">
                                    <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm"></asp:TextBox>  
                                     <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" targetcontrolid="txqty" filtertype="Numbers" validchars="0123456789" />
                                </asp:Panel>
                            </td>
                            <td class="auto-style4">
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style2">Salesman Name</td>
                   
                            <td colspan="4" class="drop-down-sm">
                                <asp:DropDownList ID="cbsalesmancd" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                            </td>
                            <td class="auto-style1">
                                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-sm btn-success btn btn-add" OnClick="btadd_Click"/>
                            </td>
                        </tr>
                    </table>
                </div>   
            </div>

            <div class="divheader subheader top-devider no-margin-bottom no-padding"></div>
    
            <div class="container-fluid">
                <div class="row  margin-bottom">
                    <div class="divgrid overflow-y" style="width:100%; max-height:200px;">
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-fix table-hover mygrid" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" AllowSorting="True">
                            <Columns>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lbmonthcd" runat="server" Text='<%# Eval("monthcd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate> <asp:Label ID="lbyearcd" runat="server" Text='<%# Eval("yearcd") %>'>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Salesman Name">
                                          <ItemTemplate><asp:HiddenField runat="server" id="hdsalesman_nm" value='<%# Eval("salesman_cd") %>'></asp:HiddenField><asp:Label ID="lbsalesman_nm" runat="server" Text='<%# Eval("salesman_nm") %>'></asp:Label></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Amount">
                                    <ItemTemplate><%# Eval("amt") %></ItemTemplate>                                                
                                    <EditItemTemplate>
                                        <asp:Panel runat="server" ID="amtPnl">
                                            <asp:TextBox ID="amt" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" targetcontrolid="amt" filtertype="Custom" validchars=".0123456789" />
                                        </asp:Panel>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" HeaderText="Action" />
                            </Columns>
                            <EditRowStyle CssClass="table-edit" />
                            <FooterStyle CssClass="table-footer" />
                            <HeaderStyle CssClass="table-header" />
                            <PagerStyle CssClass="table-page"/>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
</asp:Content>

