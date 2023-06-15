<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="frmRPS.aspx.cs" Inherits="frmRPS" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script>
        function ItemSelectedSalesman_CD(sender, e) {
             $get('<%=hdsalesman_CD.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    <script>
        function ItemSelectedCust(sender, e) {
            $get('<%=hdcust_cd.ClientID%>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }

    </script>
    <style type="text/css">


        .button2.add {
    background: #f3f3f3 url('css/5Fm069k.png') no-repeat 10px -27px;
    padding-left: 30px;
    border-radius:8px;
}

.button2 {
    color: #6e6e6e;
    font: bold 12px Helvetica, Arial, sans-serif;
    text-decoration: none;
    padding: 7px 12px;
    position: relative;
    display: inline-block;
    text-shadow: 0 1px 0 #fff;
    -webkit-transition: border-color .218s;
    -moz-transition: border .218s;
    -o-transition: border-color .218s;
    transition: border-color .218s;
    background: #f3f3f3;
    background: -webkit-gradient(linear,0% 40%,0% 70%,from(#F5F5F5),to(#F1F1F1));
    background: #f3f3f3;
    border: solid 1px #dcdcdc;
    border-radius: 2px;
    -webkit-border-radius: 2px;
    -moz-border-radius: 2px;
    margin-right: 10px;
            top: 0px;
            left: 0px;
            height: 39px;
            width: 133px;
        }
        
.button2 {
    color: #6e6e6e;
    font: bold 12px Helvetica, Arial, sans-serif;
    text-decoration: none;
    padding: 7px 12px;
    position: relative;
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
    border-radius: 2px;
    -webkit-border-radius: 2px;
    -moz-border-radius: 2px;
    margin-right: 10px;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <p>
        <strong>ROUTE PLAN&nbsp; SALES </strong></p>
    <table>
        <tr>
            <td>RPS CD</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txKey" runat="server"  Width="40px" style="display:none"></asp:TextBox>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txRPSCD" runat="server" CssClass="makeitreadonly" Enabled="False" Width="130px"></asp:TextBox>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Sales Point CD</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="cbSalesPointCD" runat="server" AutoPostBack="True" Enabled="False" Height="20px" Width="195px" CssClass="makeitreadonly">
                        </asp:DropDownList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>Day</td>
            <td>
                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                    <ContentTemplate>
                        <asp:RadioButtonList ID="rbDayCD" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rbDayCD_SelectedIndexChanged" RepeatDirection="Horizontal" Width="105px">
                            <asp:ListItem Value="1">Sun</asp:ListItem>
                            <asp:ListItem Value="2">Mon</asp:ListItem>
                            <asp:ListItem Value="3">Tue</asp:ListItem>
                            <asp:ListItem Value="4">Wed</asp:ListItem>
                            <asp:ListItem Value="5">Thu</asp:ListItem>
                            <asp:ListItem Value="6">Fri</asp:ListItem>
                            <asp:ListItem Value="7">Sat</asp:ListItem>
                        </asp:RadioButtonList>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>Salesman Code</td>
            <td>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txsearchsalesman_CD" runat="server" Width="250px" AutoPostBack="True" OnTextChanged="txsearchsalesman_CD_TextChanged"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txsearchsalesman_CD_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelectedSalesman_CD" ServiceMethod="GetCompletionListSalesman_CD" TargetControlID="txsearchsalesman_CD" UseContextKey="True">
                                </asp:AutoCompleteExtender>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Fill Salesman" Font-Bold="True" Font-Size="Medium" ForeColor="Red" ControlToValidate="txsearchsalesman_CD">**</asp:RequiredFieldValidator>
                                <asp:HiddenField ID="hdsalesman_CD" runat="server" ClientIDMode="Static" />
                                &nbsp;
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
        </tr>
    </table>
    <table>
        <tr  style="background-color:silver;border-color:yellow;border:none">
            <td>Customer</td>
            
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txsearchCust" runat="server" AutoPostBack="True" Width="356px"></asp:TextBox>
                        
                        <asp:AutoCompleteExtender ID="txsearchCust_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="ItemSelectedCust" ServiceMethod="GetCompletionListCust" TargetControlID="txsearchCust" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:HiddenField ID="hdcust_cd" runat="server" ClientIDMode="Static" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
            <td>
                            <asp:Button ID="btAdd" runat="server" CssClass="button2 add" OnClick="btAdd_Click" Text="Add" />
                        </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" OnRowDeleting="grd_RowDeleting" Width="1031px">
                            <Columns>
                                <asp:TemplateField HeaderText="No">
                                    <ItemTemplate>
                                        <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("seqno") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcust_cd" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbRPSDetID" runat="server" Text='<%# Eval("RPSDetID") %>'></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField Visible="false">
                                    <ItemTemplate>
                                       <asp:Label ID="lbSalesPointCD" runat="server" Text='<%# Eval("SalesPointCD") %>'></asp:Label>
                                    </ItemTemplate>
                                 </asp:TemplateField>
                                <asp:TemplateField HeaderText="Action" ShowHeader="False">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?'); "></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <SelectedRowStyle BackColor="#3399FF" />
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>
    <table>
        <tr>
            <td>
                        <asp:Button ID="btnew" runat="server" CssClass="button2 add" OnClick="btnew_Click" Text="New" />
                    </td>
            <td>
                        <asp:Button ID="btsave" runat="server" Text="SAVE" OnClick="btsave_Click" CssClass="button2 save" style="top: 0px; left: 0px; " />
                    </td>
            <td>
                        <asp:Button ID="btDelete" runat="server" CssClass="button2 delete" OnClick="btDelete_Click" Text="Delete" OnClientClick="return confirm('Are you sure you want to delete?'); "/>
                    </td>
            <td></td>
        </tr>
    </table>
</asp:Content>

