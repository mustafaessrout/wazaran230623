<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmWizRecCheque.aspx.cs" Inherits="frmWizRecCheque" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">


    .button2.save {
    background: #f3f3f3 url('css/5Fm069k.png') no-repeat 10px 7px;
    padding-left: 30px;
    border-radius:2px;
}

.button2 {
    color: #6e6e6e;
    font: bold 12px Helvetica, Arial, sans-serif;
    text-decoration: none;
    /* padding: 7px 12px; */
    padding:3px 8px;
    /* position: relative; */
    display: inline-block;
    text-shadow: 0 1px 0 #fff;
    -webkit-transition: border-color .218s;
    -moz-transition: border .218s;
    -o-transition: border-color .218s;
    transition: border-color .218s;
    background: #f3f3f3;
    background: -webkit-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
    background: -moz-linear-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
    border: solid 1px #dcdcdc;
    border-radius: 6px;
    -webkit-border-radius: 6px;
    -moz-border-radius: 6px;
    margin-right: 10px;
  }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        <h3>Wizart Receipt Cheque</h3>
    </div>
    <img src="div2.png" class="divid" />
    <div>
    <table>
        <tr>
            <td>Criteria Due Date&lt;=</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txinsDueDate" runat="server" Width="150px"></asp:TextBox>
                        <asp:CalendarExtender ID="txinsDueDate_CalendarExtender" runat="server" TargetControlID="txinsDueDate" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                        </asp:CalendarExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="d/M/yyyy" Font-Bold="True" Font-Size="Medium" ForeColor="Red">**</asp:RequiredFieldValidator>
                Transaction Date</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txinsTransDate" runat="server" Width="150px"></asp:TextBox>
                        <asp:CalendarExtender ID="txinsTransDate_CalendarExtender" runat="server" TargetControlID="txinsTransDate" Format="d/M/yyyy" TodaysDateFormat="d/M/yyyy">
                        </asp:CalendarExtender>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="d/M/yyyy" Font-Bold="True" Font-Size="Medium" ForeColor="Red">**</asp:RequiredFieldValidator>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
        <table>
        <tr>
            <td>
                <asp:RadioButtonList ID="rbtobe" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtobe_SelectedIndexChanged">
                    <asp:ListItem Value="1">Tobe Deposit</asp:ListItem>
                    <asp:ListItem Value="2">Tobe Cleared</asp:ListItem>
                    <asp:ListItem Value="3">Tobe Rejected</asp:ListItem>
                    <asp:ListItem Value="4">Tobe Reissued</asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbBankID" runat="server" AutoPostBack="True" CssClass="auto-style3" Height="20px" Width="195px">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    </div>
    <div class="divgrid">

        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" Width="1031px">
                    <Columns>
                        <asp:TemplateField HeaderText="Trans No">
                            <ItemTemplate>
                                <asp:Label ID="lblinsDocNo" runat="server" Text='<%# Eval("insDocNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Trans Date">
                            <ItemTemplate>
                                <asp:Label ID="lblinsDocDate" runat="server" Text='<%# Eval("insDocDate","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CH Due Date">
                            <ItemTemplate>
                                <asp:Label ID="lblinsDueDate" runat="server" Text='<%# Eval("insDueDate","{0:d/M/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer">
                            <ItemTemplate>
                                <asp:Label ID="lblcust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                       <asp:TemplateField HeaderText="CH Amount">
                            <ItemTemplate>
                                <asp:Label ID="lblinsAmount" runat="server" Text='<%# Eval("insAmount","{0:n0}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblSalesPointCD" runat="server" Text='<%# Eval("SalesPointCD") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="lblInstruID" runat="server" Text='<%# Eval("InstruID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Select">
                             <ItemTemplate>
                                <asp:CheckBox ID="chk" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <SelectedRowStyle BackColor="#3399FF" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>

    </div>
    <div class="nav">

                    <asp:Button ID="btok" runat="server" CssClass="button2 save" OnClick="btok_Click" Text="OK" />

    </div>
</asp:Content>

