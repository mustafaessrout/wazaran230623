<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_outlettarget.aspx.cs" Inherits="fm_outlettarget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function editFst() {
            $('.table .table-edit').each(function () {
                $(this).find(".fst-inpt").focus();
                $(this).find(".fst-inpt").addClass('ssss');
            });
        }
    </script>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %><!--By othman-->  
    <div class="divheader">Outlet Targret</div>
    <div class="h-divider"></div>

     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
        <ContentTemplate>
            <div class="container-fluid">
                <div class="row">
                    <div class="clearfix">
                        <table class="table mygrid ">
                            <tr>
                                <th style="width:150px;">Menu</th>
                                <th>Month</th>
                                <th>Year</th>
                                <th>Quantity</th>
                            </tr>
                            <tr>
                                <td style="width:90px;text-align:left;">INFO</td>
                                <td class="drop-down-sm">
                                    <asp:DropDownList ID="cbmonth" CssClass="form-control input-sm" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="refreshgridview"></asp:DropDownList>
                                </td>
                                <td class="drop-down-sm">
                                    <asp:DropDownList ID="cbyear" CssClass="form-control input-sm" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="refreshgridview" ></asp:DropDownList>
                                </td>
            
                                <td style="width:120px;">
                                    <asp:Panel runat="server" ID="txqtyPnl">
                                        <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm"></asp:TextBox>  
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" targetcontrolid="txqty"
                                        filtertype="Numbers" validchars="0123456789" />
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td style="width:90px;text-align:left;">Salesman Name</td>
                                <td colspan="2" class="drop-down-sm"><asp:DropDownList ID="cbsalesmancd" runat="server"  CssClass="form-control input-sm" ></asp:DropDownList></td>
                                <td><asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-success btn-block btn btn-sm btn-add" OnClick="btadd_Click"/></td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
    
            <div class="divheader subheader top-devider no-padding no-margin-bottom"></div>

            <div class="container-fluid margin-bottom">
                    <div class="row divgrid">
                        <div class="overflow-y" style="width:100%; max-height:210px;">
                            <asp:GridView ID="grd" CssClass="table table-striped table-hover table-fix mygrid" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" AllowSorting="True">
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
                                    <asp:TemplateField HeaderText="Outlet Target">
                                        <ItemTemplate><%# Eval("qty_outlet") %></ItemTemplate>                                                
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-sm fst-inpt"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" targetcontrolid="txqty" filtertype="Numbers" validchars="0123456789" />
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

