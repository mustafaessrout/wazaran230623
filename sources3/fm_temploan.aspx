<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_temploan.aspx.cs" Inherits="fm_temploan" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
      <link href="css/anekabutton.css" rel="stylesheet" /> 
     <style type="text/css">
         .auto-style7 {
             width: 100%;
         }
         .auto-style8 {
             width: 106px;
         }
    </style>
    <script>
        function SalesmanSelected(sender, e) {
            $get('<%=hdemp.ClientID%>').value = e.get_value();

                }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <br /> <strong>Closing temporary loan for office transaction</strong><br />
    <img src="div2.png" class="divid" />
    
    <div style="width:71%">

        <table class="auto-style7">
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="lblemp" runat="server" Text="Employee"></asp:Label>
                </td>
                <td>
                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server"><ContentTemplate>
                <asp:HiddenField ID="hdemp" runat="server" />
                    <asp:TextBox ID="txemp" runat="server" Width="300px"></asp:TextBox>
                    <div id="divwidths"></div>
                <asp:AutoCompleteExtender ID="txemp_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txemp" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionListElementID="divwidths" OnClientItemSelected="SalesmanSelected">
                </asp:AutoCompleteExtender>
                </ContentTemplate>
                </asp:UpdatePanel>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="lblbalance" runat="server" Text="Balance"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txbalance" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="auto-style8">
                    <asp:Label ID="lblapprove" runat="server" Text="Approve by"></asp:Label>
                </td>
                <td>

                    <asp:TextBox ID="txapprove" runat="server" Width="300px"></asp:TextBox>
                   
                </td>
            </tr>
        </table>
        <asp:GridView ID="GridView1" runat="server" ShowFooter="True">
            <Columns>
                <asp:TemplateField HeaderText="Type"></asp:TemplateField>
                <asp:TemplateField HeaderText="Item"></asp:TemplateField>
                <asp:TemplateField HeaderText="Ref NO."></asp:TemplateField>
                <asp:TemplateField HeaderText="Amount">
                    <FooterTemplate>
                                <asp:Label ID="lbtotamoun" runat="server" Text="Total="></asp:Label>
                            </FooterTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Remark">
                     <FooterTemplate>
                                <asp:Label ID="lbbalance" runat="server" Text="Balance="></asp:Label>
                            </FooterTemplate>

                </asp:TemplateField>
            </Columns>
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
            <img src="div2.png" class="divid" />
             <br /> <strong> <asp:Label ID="lbnote" runat="server" Text="Note: Temporary loan/ petty cash should be close within maximum 30 day's"></asp:Label></strong><br />
            <img src="div2.png" class="divid" />


        </div>

</asp:Content>

