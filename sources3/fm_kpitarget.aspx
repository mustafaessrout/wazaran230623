<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_kpitarget.aspx.cs" Inherits="fm_kpitarget" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Adminbranch/Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
        //$(document).ready(function () {
        //    $('#pnlmsg').hide();
        //});

    </script>
        <style>
        body {
            overflow-y: auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="form-horizontal">
            <h4 class="jajarangenjang">KPI Target</h4>
            <div class="h-divider"></div>
            <div class="form-group">
                <div class="row">
                    <label class="control-label col-sm-1">Period</label>
                    <div class="col-sm-2 drop-down">
                        <asp:DropDownList ID="cbperiod" onchange="ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Level</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cblevel" onchange="ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cblevel_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Job Title</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbjobtitle" onchange="ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbjobtitle_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <label class="control-label col-md-1">Employee</label>
                    <div class="col-md-2 drop-down">
                        <asp:DropDownList ID="cbemployee" onchange="ShowProgress();" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbemployee_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowCancelingEdit="grd_RowCancelingEdit" OnRowDataBound="grd_RowDataBound" OnRowEditing="grd_RowEditing" OnRowUpdating="grd_RowUpdating" CellPadding="0" OnSelectedIndexChanged="grd_SelectedIndexChanged" OnSelectedIndexChanging="grd_SelectedIndexChanging">
                            <Columns>
                                <asp:TemplateField HeaderText="Key Responsible">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdids" Value='<%#Eval("IDS") %>' runat="server" />
                                        <asp:Label ID="lbkeyresp" runat="server" Text='<%#Eval("keyresp_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Section ">
                                    <ItemTemplate><%#Eval("section_nm") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Objective">
                                    <ItemTemplate><%#Eval("objective") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="KPI">
                                    <ItemTemplate>
                                        <%#Eval("kpi")%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Target">
                                    <ItemTemplate>
                                        <asp:Label ID="lbtarget" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtarget" CssClass="form-control" runat="server" Text=""></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowEditButton="True" AccessibleHeaderText="Action" ShowSelectButton="True" />
                            </Columns>
                            <SelectedRowStyle BackColor="Yellow" />
                        </asp:GridView>
                    </div>
                </div>
                <h5 class="jajarangenjang">KPI Detail</h5>
                <div class="h-divider"></div>
                <div class="row">
                    <div class="col-md-12">
                        <table class="mGrid">
                            <tr>
                                <th>Product Group</th>
                                <th>Qty Target</th>
                                <th>Add</th>
                            </tr>
                            <tr>
                                <td class="drop-down">
                                    <asp:DropDownList ID="cbproduct" CssClass="form-control" runat="server"></asp:DropDownList></td>
                                <td>
                                    <asp:TextBox ID="txqty" runat="server"></asp:TextBox></td>
                                <td>
                                    <asp:LinkButton ID="btadd" OnClientClick="javascript:ShowProgress();" CssClass="btn btn-primary" runat="server" OnClick="btadd_Click">Add</asp:LinkButton></td>
                            </tr>
                        </table>
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <asp:GridView ID="grddtl" CssClass="mGrid" runat="server" AutoGenerateColumns="False" 
                            OnRowDeleting="grddtl_RowDeleting" OnSelectedIndexChanged="grddtl_SelectedIndexChanged" OnSelectedIndexChanging="grddtl_SelectedIndexChanging"
                              >
                            <Columns>
                                <asp:TemplateField HeaderText="Group Product">
                                    <ItemTemplate><%#Eval("prod_desc") %></ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Qty Target">
                                     <ItemTemplate>
                                         <asp:HiddenField ID="hdfIDS" runat="server" Value='<%#Eval("IDS")%>' />
                                         <asp:HiddenField ID="hdfperiod" runat="server" Value='<%#Eval("period")%>' />
                                         <asp:HiddenField ID="hdfprod_cd" runat="server" Value='<%#Eval("prod_cd")%>' />
                                        <asp:Label ID="lbtarget" runat="server" Text='<%#Eval("qty")%>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtarget" CssClass="form-control" runat="server" Text='<%#Eval("qty")%>'></asp:TextBox>
                                    </EditItemTemplate>
                                    
                                </asp:TemplateField>
                               
                                <asp:CommandField ShowSelectButton="true" ShowDeleteButton="true"  AccessibleHeaderText="Action"/>
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>
            <div class="col-md-12" style="text-align: center">
                <asp:LinkButton ID="btnew" runat="server" CssClass="btn btn-primary" OnClick="btnew_Click"><i class="fa fa-plus">&nbsp;New</i></asp:LinkButton>
                <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-success" OnClick="btsave_Click"><i class="fa fa-save">&nbsp;Save</i></asp:LinkButton>
                <asp:LinkButton ID="bprint" CssClass="btn btn-danger" runat="server"><i class="fa fa-print">&nbsp;Print</i></asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

