<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_custblock2.aspx.cs" Inherits="admin_fm_custblock2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
<script src="js/jquery.min.js"></script>
<script src="js/bootstrap.min.js"></script>
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <table>
    <tr>
        <td>
            <h3>
            <label class="control-label" for="lbsp">Customer UNBlock Document Activated -</label>
                </h3>
        </td>
        
        <td>
            <h3>
                <asp:Label ID="lbsp" runat="server" Font-Bold="True"  ForeColor="Red"></asp:Label>
            </h3>
        </td>
    </tr>
</table>
    <table>
        <tr>
            <td>Customer Outlet :>
            <td>
                <asp:DropDownList ID="cbotlcd" runat="server">
                </asp:DropDownList>
            </td>
            <td>
                Start Date :
            </td>
            <td>

                            <asp:TextBox ID="dtfr_dt" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                            <asp:CalendarExtender ID="dtfr_dt_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtfr_dt">
                            </asp:CalendarExtender>

            </td>
            <td>
                To Date :
            </td>
            <td>

                            <asp:TextBox ID="dtto_dt" runat="server" CssClass="makeitreadonly"></asp:TextBox>
                            <asp:CalendarExtender ID="dtto_dt_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtto_dt">
                            </asp:CalendarExtender>

            </td>
            <td>
<asp:Button ID="btadd" runat="server" Text="Add" OnClick="btadd_Click" CssClass="button2 add" Height="29px" Width="92px" />
            </td>
        </tr>
    </table>
  <table>
        <tr>
            <td>

                <asp:CheckBoxList ID="chdoc_cd" runat="server" AutoPostBack="True" Height="74px" RepeatDirection="Horizontal" Width="801px" RepeatColumns="6" OnSelectedIndexChanged="chdoc_cd_SelectedIndexChanged">
                    <asp:ListItem Value="CR1">Municipality Permit</asp:ListItem>
                    <asp:ListItem Value="CR2">Bank Account</asp:ListItem>
                    <asp:ListItem Value="CR3">ID Employee</asp:ListItem>
                    <asp:ListItem Value="CR4">Outlet Map</asp:ListItem>
                    <asp:ListItem Value="CR5">Customer Stamp</asp:ListItem>
                    <asp:ListItem Value="CR6">CRV Customer Registration Voucher</asp:ListItem>
                    <asp:ListItem Value="CR7">Customer Signature </asp:ListItem>
                    <asp:ListItem Value="CR8">Form Opening Customer</asp:ListItem>
                    <asp:ListItem Value="CR9">Stamp Of JCCI</asp:ListItem>
                    <asp:ListItem Value="CR10">ID Owner</asp:ListItem>
                    <asp:ListItem Value="CR11">Outlet Picture</asp:ListItem>
                    <asp:ListItem Value="CR12">Location License</asp:ListItem>
                </asp:CheckBoxList>

            </td>
        </tr>
      <tr>
          <td>

              <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                  <ContentTemplate>
                      <asp:Label ID="lbdoc_cd" runat="server" Text="Label"></asp:Label>
                  </ContentTemplate>
                  <Triggers>
                    <asp:AsyncPostBackTrigger  ControlID="chdoc_cd" EventName="SelectedIndexChanged"/>
                  </Triggers>
              </asp:UpdatePanel>

          </td>
      </tr>
  </table>
        
    
   <div class="form-group">
                <div class="col-md-12">
                    <div style="width:100%">
    <div style="float:left;width:50%;vertical-align:top;padding:10px 10px 10px 10px">

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdotlcd" runat="server" AutoGenerateColumns="False" BorderStyle="None" GridLines="Horizontal" OnRowDeleting="grdotlcd_RowDeleting" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Otlcd">
                            <ItemTemplate>
                                <asp:Label ID="lbOtlcd" runat="server" Text='<%# Eval("Otlcd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Outlet Name">
                            <ItemTemplate>
                                <%# Eval("Otlcd_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="document">
                            <ItemTemplate>
                                <%# Eval("doc_cd") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date">
                            <ItemTemplate>
                                <%# Eval("fr_dt","{0:d/M/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date">
                            <ItemTemplate>
                                <%# Eval("to_dt","{0:d/M/yyyy}") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
                        
    </div>
                </div>
            </div>
    </div>  
    <table>
        <tr>
            <td>Outlet Code</td>
            <td>
                <asp:DropDownList ID="cbotlcd2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbotlcd2_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                Customer Group :
            </td>
            <td>

                <asp:DropDownList ID="cbcusgrcd" runat="server" AutoPostBack="True">
                </asp:DropDownList>

            </td>
            
            <td>
            <asp:Button ID="btaddcusgrcd" runat="server" Text="Add" OnClick="btaddcusgrcd_Click" CssClass="button2 add" Height="29px" Width="92px" />
            </td>
        </tr>

        
    </table>
    <table>
        <tr>
            <td>
                <asp:CheckBoxList ID="chcuscate_cd" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="chcuscate_cd_SelectedIndexChanged">  
                    <asp:ListItem Text="CASH">CASH</asp:ListItem>  
                    <asp:ListItem Text="CREDIT1">CREDIT1</asp:ListItem>  
                    <asp:ListItem Text="CREDIT2">CREDIT2</asp:ListItem>  
                    <asp:ListItem Text="CREDIT3">CREDIT3</asp:ListItem>  
                    <asp:ListItem Text="NL">NL</asp:ListItem>  
    </asp:CheckBoxList>  

            </td>
            <td>Outlet Code :</td>
            <td>

                <asp:Label ID="lbcuscate_cd" runat="server"></asp:Label>

            </td>
        </tr>
    </table>
<div class="form-group">
                <div class="col-md-12">
                    <div style="width:100%">
    <div style="float:left;width:50%;vertical-align:top;padding:10px 10px 10px 10px">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdcusgrcd" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="Horizontal" OnRowDeleting="grdcusgrcd_RowDeleting">
                    <Columns>
                        <asp:TemplateField HeaderText="Outlet Code">
                            <ItemTemplate>
                                <asp:Label ID="lbOtlcd2" runat="server" Text='<%# Eval("Otlcd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcusgrcd" runat="server" Text='<%# Eval("cusgrcd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group Name">
                            <ItemTemplate>
                                <asp:Label ID="lbcusgr_nm" runat="server" Text='<%# Eval("cusgr_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Category">
                            <ItemTemplate>
                                <asp:Label ID="lbcuscate_cd" runat="server" Text='<%# Eval("cuscate_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowDeleteButton="True" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
                  
    <table>
        <tr>
            <td>

                Customer :</td>
            <td>

       
<div class="form-group">
    <div class="col-md-12">
                    <div class="input-group">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txcustomer" runat="server" CssClass="form-control input-group-sm" Height="100%" Width="100%"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txcustomer" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:HiddenField ID="hdcust" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>

       </div>
        </div>
    </div>

            </td>
            <td>

            <asp:Button ID="btaddcust" runat="server" Text="Add" CssClass="button2 add" Height="29px" Width="92px" OnClick="btaddcust_Click" />
            </td>
        </tr>
    </table>
    <div class="form-group">
                <div class="col-md-12">
                    <div style="width:100%">
    <div style="float:left;width:100%;vertical-align:top;padding:10px 10px 10px 10px">

        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="grdcust_cd" runat="server" AutoGenerateColumns="False" BorderStyle="None" GridLines="Horizontal" OnRowDeleting="grdcust_cd_RowDeleting" Width="100%">
                            <Columns>
                                <asp:TemplateField HeaderText="Otlcd">
                                    <ItemTemplate>
                                        <asp:Label ID="lbOtlcd3" runat="server" Text='<%# Eval("Otlcd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Outlet Name">
                                    <ItemTemplate>
                                        <%# Eval("Otlcd_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="cusgrcd">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcusgrcd3" runat="server" Text='<%# Eval("cusgrcd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Group Name">
                                    <ItemTemplate>
                                        <%# Eval("cusgr_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="cust Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbcust_cd" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Customer Name">
                                    <ItemTemplate>
                                        <%# Eval("cust_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </asp:UpdatePanel>

        </div>
                </div>
            </div>
    </div>              
</asp:Content>

