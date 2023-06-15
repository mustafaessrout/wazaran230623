<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salestargetho2.aspx.cs" Inherits="fm_salestargetho2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="css/jquery-1.12.4"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery-3.2.1.min.js"></script>
    <script src="js/bootstrap.min.js"></script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">

        function updpnl() {
            document.getElementById('<%=btnUpdateAssignDate.ClientID%>').click();
            return (false);
        }
        //$(document).ready(function () {

        //});

        function openwindow3() {
            var period = document.getElementById('<%=cbperiod.ClientID%>').value;
            var oNewWindow = window.open("fm_salestargetho2_lookUp.aspx?period=" + period, "lookup", "height=700,width=800,menubar=no,resizable=no,scrollbars=yes,titlebar=no,toolbar=no,location=no");
        }


      
    </script>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:Label ID="lblMsg" runat="server" Width="100%" Visible="false"></asp:Label>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="divheader">
        Sales Target Head Office
    </div>
    <%--    <div  class="alert alert-success alert-dismissable fade in">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
    <strong>Success!</strong> This alert box could indicate a successful or positive action.
  </div>
  <div class="alert alert-info alert-dismissable fade in">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
    <strong>Info!</strong> This alert box could indicate a neutral informative change or action.
  </div>
  <div class="alert alert-warning alert-dismissable fade in">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
    <strong>Warning!</strong> This alert box could indicate a warning that might need attention.
  </div>
  <div class="alert alert-danger alert-dismissable fade in">
    <a href="#" class="close" data-dismiss="alert" aria-label="close">&times;</a>
    <strong>Danger!</strong> This alert box could indicate a dangerous or potentially negative action.
  </div>--%>
    <img src="div2.png" class="divid" />
    <div>
        Level :
        <asp:DropDownList ID="cblevel" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cblevel_SelectedIndexChanged" Width="10em">
            <asp:ListItem Value="2">Level 2</asp:ListItem>
            <asp:ListItem Value="3">Level 3</asp:ListItem>
        </asp:DropDownList>
        Salespoint :
        <asp:DropDownList ID="cbsalespoint" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged"></asp:DropDownList>&nbsp;Period :
        <asp:DropDownList ID="cbperiod" runat="server" Width="10em" AutoPostBack="True" OnSelectedIndexChanged="cbperiod_SelectedIndexChanged">
        </asp:DropDownList>
        &nbsp;Distributor :
        <asp:DropDownList ID="cbdistri" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbdistri_SelectedIndexChanged" Width="10em">
        </asp:DropDownList>
    </div>
    <table style="width: 100%">
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" DataKeyNames="prod_nm" runat="server" AutoGenerateColumns="False" Width="100%" CellPadding="1" ForeColor="#333333" GridLines="None" OnRowDataBound="grd_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="Product ">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hdprodcd" Value='<%# Eval("prod_cd") %>' runat="server" />
                                        <asp:Label ID="lbprodname" runat="server" Text='<%# Eval("prod_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txqty" runat="server"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate>
                                        CTN (Carton)
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Assign Group ">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAssignGroup" ForeColor="Brown" runat="server" Text='<%# Eval("AssignGroup") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkIsPriority" runat="server" OnCheckedChanged="chkIsPriority_CheckedChanged" AutoPostBack="true" />
                                    </ItemTemplate>

                                    <HeaderTemplate>
                                        Priority
                                    </HeaderTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Mini Load / Salesman">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txMinimumLoading" runat="server" Text="" MaxLength="4" AutoPostBack="true" OnTextChanged="txMinimumLoading_TextChanged"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sales Group">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnk_productid" Text='Assign / View' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>

                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                            <SortedAscendingCellStyle BackColor="#E9E7E2" />
                            <SortedAscendingHeaderStyle BackColor="#506C8C" />
                            <SortedDescendingCellStyle BackColor="#FFFDF8" />
                            <SortedDescendingHeaderStyle BackColor="#6F8DAE" />

                        </asp:GridView>
                    </ContentTemplate>

                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="cbperiod" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cblevel" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="cbsalespoint" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>

            </td>
        </tr>
    </table>
    <div class="navi">
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        <asp:Button ID="btprint" runat="server" Text="Print" CssClass="button2 print" OnClick="btprint_Click" />
        <asp:Button ID="btnAchievement" runat="server" Text="View Achievement" CssClass="button2 print" OnClick="btnAchievement_Click" />
        <asp:LinkButton ID="btsearch" CssClass="btn btn-primary" runat="server" OnClientClick="openwindow3();return(false);">Sync Branch</asp:LinkButton>
        <asp:Button ID="btnUpdateAssignDate" runat="server" Text="Save" CssClass="button2 save" OnClick="btnUpdate_Click" Style="display: none" />
    </div>
</asp:Content>

