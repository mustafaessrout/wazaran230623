<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstemplist.aspx.cs" Inherits="fm_mstemplist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }
    </script>
    <style>
        th {
            position: sticky;
            top: 0;
        }
 </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">Employee List</div>
    <div class="container">
        <div class="row">
            <div class="clearfix form-group col-md-6 ">
                <label class="col-sm-2 control-label">Salespoint</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <div class="col-sm-offset-2 col-sm-10">
                    <asp:CheckBox ID="chsp" runat="server" Text="All" AutoPostBack="True" OnCheckedChanged="chsp_CheckedChanged" CssClass="checkbox no-margin" />
                </div>
            </div>
            <div class="clearfix form-group col-md-6 ">
                <label class="col-sm-2 control-label">Department</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbdepartment" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-offset-2 col-sm-10">
                    <asp:CheckBox ID="chdept" runat="server" Text="All" AutoPostBack="True" OnCheckedChanged="chsp_CheckedChanged" CssClass="checkbox no-margin" />
                </div>
            </div>
            <div class="clearfix form-group col-md-6 ">
                <label class="col-sm-2 control-label">Nationality</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbnationaligy" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                    </asp:DropDownList>
                </div>
                <div class="col-sm-offset-2 col-sm-10">
                    <asp:CheckBox ID="chnat" runat="server" Text="All" AutoPostBack="True" OnCheckedChanged="chsp_CheckedChanged" CssClass="checkbox no-margin" />
                </div>
            </div>
            <div class="clearfix form-group col-md-6 ">
                <label class="col-sm-2 control-label">Employee ID/Name</label>
                <div class="col-sm-10">
                    <div class="input-group">
                        <asp:TextBox ID="txemp" runat="server" CssClass="form-control" AutoPostBack="True"></asp:TextBox>
                        <div class="input-group-btn">
                            <asp:Button ID="btsearch" runat="server" CssClass="btn-primary btn btn-search" Text="Search" OnClick="btsearch_Click" />
                        </div>
                    </div>
                </div>

            </div>
        </div>
        <div class="row overflow-y" style="max-height: 360px;">
            <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" OnRowCommand="grd_RowCommand" OnRowEditing="grd_RowEditing" GridLines="None" OnPageIndexChanging="grd_PageIndexChanging" CellPadding="0" CssClass="table table-striped table-hover mygrid">
                <AlternatingRowStyle />
                <Columns>
                    <asp:TemplateField HeaderText="Emp Code">
                        <ItemTemplate>
                            <asp:Label ID="lbempcd" runat="server" Text='<%# Eval("emp_cd") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <ItemTemplate><%# Eval("emp_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Position">
                        <ItemTemplate><%# Eval("job_title_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Department">
                        <ItemTemplate><%# Eval("dept_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Nationality">
                        <ItemTemplate><%# Eval("nationality") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Salespoint Code">
                        <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Join Date">
                        <ItemTemplate><%# Eval("join_dt","{0:d/M/yyyy}") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Have Access">
                        <ItemTemplate><%# Eval("usr_access") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Have Tablet">
                        <ItemTemplate><%# Eval("usr_tablet") %></ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField HeaderText="Action" EditText="Select" ShowEditButton="True" />
                </Columns>
                <EditRowStyle BackColor="#999999" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle CssClass="table-page" />
                <RowStyle />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#E9E7E2" />
                <SortedAscendingHeaderStyle BackColor="#506C8C" />
                <SortedDescendingCellStyle BackColor="#FFFDF8" />
                <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
            </asp:GridView>
        </div>
        <div class="row navi">
            <asp:Button ID="btnew" OnClientClick="ShowProgress();" runat="server" Text="NEW EMPLOYEE" OnClick="btnew_Click" CssClass="btn-success btn btn-add" />
            <asp:Button ID="btprint" runat="server" Text="Print" OnClientClick="ShowProgress();" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>

</asp:Content>

