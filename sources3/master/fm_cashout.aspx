<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_cashout.aspx.cs" Inherits="master_fm_cashout" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function DebetSelected(sender, e) {
            $get('<%=hddebet.ClientID%>').value = e.get_value();
        }

        function CreditSelected(sender, e) {
            $get('<%=hdcredit.ClientID%>').value = e.get_value();
        }
        function updpnl() {
            document.getElementById('<%=btnUpdate.ClientID%>').click();
            return (false);
        }
    </script>
    <style>
        
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <div class="form-horizontal" style="font-size: small; font-family: Calibri">
        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hditem" runat="server" />
                <asp:HiddenField ID="hddebet" runat="server" />
                <asp:HiddenField ID="hdcredit" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="UpdatePanel12" runat="server">
            <ContentTemplate>
                <asp:Label ID="lblMsg" runat="server" Width="100%" Visible="false"></asp:Label>
            </ContentTemplate>
        </asp:UpdatePanel>
        <h4 class="jajarangenjang">Item Cashout</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <label class="control-label col-md-1">For</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbfor" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbfor_SelectedIndexChanged">
                    <asp:ListItem Value="B">Branches Cash IN OUT</asp:ListItem>
                    <asp:ListItem Value="E">Exhibition Cash IN OUT</asp:ListItem>
                </asp:DropDownList>
            </div>
            <label class="control-label col-md-1">InOut</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbinout" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbinout_SelectedIndexChanged">
                    <asp:ListItem Value="O">Cash Out</asp:ListItem>
                    <asp:ListItem Value="I">Cash In</asp:ListItem>
                </asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Routine/Non Routine</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbroutine" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbroutine_SelectedIndexChanged">
                    <asp:ListItem Value="R">Routine</asp:ListItem>
                    <asp:ListItem Value="NR">Non Routine</asp:ListItem>
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12">
                <table>
                    <tr style="background-color: lightskyblue">
                        <th>Category</th>
                        <th>Cashout Code</th>
                        <th>Cashout Name</th>
                        <th>Save</th>
                    </tr>
                    <tr style="vertical-align: top">
                        <td>
                            <asp:DropDownList ID="cbcategory" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="cbcategory_SelectedIndexChanged"></asp:DropDownList>
                        </td>
                        <td>

                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="input-group">
                                        <asp:TextBox ID="txcashoutcode" ReadOnly="true" CssClass="form-control input-group-sm" runat="server"></asp:TextBox>
                                        <div class="input-group-btn">
                                            <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClick="btsearch_Click"><span class="glyphicon glyphicon-search"></span></asp:LinkButton>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="grd" EventName="SelectedIndexChanging" />
                                </Triggers>
                            </asp:UpdatePanel>


                        </td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <asp:TextBox ID="txcashoutname" CssClass="form-control" runat="server"></asp:TextBox>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="grd" EventName="SelectedIndexChanging" />
                                </Triggers>
                            </asp:UpdatePanel>

                        </td>
                        <td></td>
                        <td></td>
                        <td>
                            <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                <ContentTemplate>
                                    <asp:LinkButton ID="btsave" runat="server" CssClass="btn btn-primary" OnClick="btsave_Click">Save</asp:LinkButton>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" CssClass="mydatagridView" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellPadding="0"
                            OnPageIndexChanging="grd_PageIndexChanging" OnSelectedIndexChanging="grd_SelectedIndexChanging" OnRowDeleting="grd_RowDeleting"
                            OnRowDataBound="grd_RowDataBound" PagerStyle-CssClass="pager"
 HeaderStyle-CssClass="header" RowStyle-CssClass="rows" Width="1000px">
                            <Columns>
                                <asp:TemplateField HeaderText="Cashout Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%#Eval("itemco_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Cashout Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemname" runat="server" Text='<%#Eval("itemco_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="InOut">
                                    <ItemTemplate><%#Eval("inout") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Routine">
                                    <ItemTemplate><%#Eval("routine") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Remarks" Text='Edit' runat="server" />
                                        <asp:LinkButton ID="lnk_RemarksNew" Text='New' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Attribute">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_Attribute" Text='Edit' runat="server" />
                                        <asp:LinkButton ID="lnk_AttributeNew" Text='New' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--<asp:TemplateField HeaderText="Acc Debet">
                            <ItemTemplate>
                                <asp:Label ID="lbdebet" runat="server" Text='<%#Eval("coa_cd_debet_desc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Acc Credit">
                            <ItemTemplate>
                                <asp:Label ID="lbcredit" runat="server" Text='<%#Eval("coa_cd_credit_desc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:CommandField ShowDeleteButton="True" HeaderText="Delete" ShowSelectButton="True" />
                            </Columns>
                            <SelectedRowStyle BackColor="Silver" />
                        </asp:GridView>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbfor" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbcategory" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbinout" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbroutine" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12" style="text-align: center">
                <asp:Button ID="btnUpdate" runat="server" Text="Save" CssClass="button2 save" OnClick="btnUpdate_Click" Style="display: none" />
                <asp:LinkButton ID="btadd" CssClass="btn btn-success" runat="server" OnClick="btadd_Click">NEW</asp:LinkButton>
            </div>
        </div>
    </div>
</asp:Content>

