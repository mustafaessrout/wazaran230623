<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_rpsgroup.aspx.cs" Inherits="fm_rpsgroup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
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

    <%--<div class="col-xs-12 overflow-y" style="max-height: 360px;">--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="alert alert-info text-bold">RPS By Group</div>
    <div class="container">
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <table class="table table-bordered input-sm">
                    <tr>
                        <th>Salesman</th>
                        <th>Group</th>
                        <th>Day</th>
                        <th>City</th>
                        <th>District</th>
                        <th>Add</th>
                    </tr>
                    <tr>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbsalesman" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="True"></asp:DropDownList>
                        </td>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbgroup" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbday" AutoPostBack="true" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbday_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td class="drop-down">
                            <asp:DropDownList ID="cbcity" AutoPostBack="true" onchange="ShowProgress();" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbcity_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:DropDownList ID="cbdistrict" CssClass="form-control input-sm" runat="server" OnSelectedIndexChanged="cbdistrict_SelectedIndexChanged1"></asp:DropDownList>
                        </td>
                        <td>
                            <asp:LinkButton ID="btadd" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btadd_Click">Add</asp:LinkButton>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 overflow-y" style="max-height: 360px;">
                <asp:GridView ID="grd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate><%#Eval("emp_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group">
                            <ItemTemplate>
                                <%#Eval("grouprps_nm") %>
                                <asp:HiddenField ID="hdgrouprps" runat="server" Value='<%#Eval("grouprps_cd") %>' />
                                <asp:HiddenField ID="hdday" runat="server" Value='<%#Eval("day_cd") %>' />
                                <asp:HiddenField ID="hddistrict" runat="server" Value='<%#Eval("district_cd") %>' />
                                <asp:HiddenField ID="hdemployee" Value='<%#Eval("emp_cd") %>' runat="server" />
                                <asp:Label ID="lbgrouprps" runat="server" Text='<%#Eval("grouprps_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Day">
                            <ItemTemplate><%#Eval("day_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="District">
                            <ItemTemplate><%#Eval("district_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12 text-center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-success btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
                <asp:LinkButton ID="btsave" CssClass="btn btn-primary btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsave_Click">Save</asp:LinkButton>
                <asp:LinkButton ID="btprint" CssClass="btn btn-info btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btprint_Click">Print</asp:LinkButton>
                <asp:LinkButton ID="btsalesmandistrict" CssClass="btn btn-warning btn-sm" OnClientClick="ShowProgress();" runat="server" OnClick="btsalesmandistrict_Click">Salesman District</asp:LinkButton>
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

