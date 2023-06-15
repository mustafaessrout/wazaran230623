<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_custblock3.aspx.cs" Inherits="admin_fm_custblock3" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="admin/css/bootstrap.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.js"></script>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script src="css/jquery-1.9.1.js"></script>
     <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="form-horizontal" style="font-size:small;font-family:Calibri">
        <h4 class="jajarangenjang">Customer Block Document 
            <asp:Label ID="lbsp" runat="server" Font-Bold="True"  ForeColor="Red"></asp:Label>
        </h4>
    </div>
    <div class="form-group">
                <label class="control-label col-md-2">Customer Category</label>
                <div class="col-md-2">
                   <asp:DropDownList ID="cbcustcate_cd" runat="server" Width="100%" CssClass="form-control-static input-sm" OnSelectedIndexChanged="cbcustcate_cd_SelectedIndexChanged"></asp:DropDownList>
                </div>
                 <label class="control-label col-md-1">Start Date</label>
                 <div class="col-md-1">
                    <div class="input-group">
                        <asp:TextBox ID="dtfr" runat="server" CssClass="form-control" Width="100%" Height="100%"></asp:TextBox>
                    </div>
                 </div>
                 <label class="control-label col-md-1">End Date</label>
                <div class="col-md-1">
                     <div class="input-group">
                       <asp:TextBox ID="dtto" runat="server" CssClass="form-control" Width="100%" Height="100%"></asp:TextBox>
                     </div>
                </div>
                <div class="col-xs-3"> 
                        <asp:LinkButton ID="btAdd" 
                                        runat="server" 
                                        CssClass="btn btn-info btn-sm"    
                                        OnClick="btAdd_Click"> <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>Add </asp:LinkButton>        
                    </div>
    </div>
    <div class="form-group">
       <div class="col-md-12">
         <asp:CheckBoxList ID="chdoc_cd" runat="server" AutoPostBack="True" Height="74px" RepeatDirection="Horizontal" Width="1000px" RepeatColumns="6" OnSelectedIndexChanged="chdoc_cd_SelectedIndexChanged">
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
       </div>
     </div>
     <div class="form-group">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbdoc_cd" runat="server" CssClass="form-control"></asp:Label>
                </ContentTemplate>
                <Triggers>
                  <asp:AsyncPostBackTrigger  ControlID="chdoc_cd" EventName="SelectedIndexChanged"/>
                </Triggers>
            </asp:UpdatePanel>
         </div>
     </div>
    <div class="form-group">
       <div class="col-md-12">
         <asp:CheckBoxList ID="chotlcd" runat="server" AutoPostBack="True" Height="74px" RepeatDirection="Horizontal" Width="1000px" RepeatColumns="5" OnSelectedIndexChanged="chotlcd_SelectedIndexChanged">
            <asp:ListItem Value="BRN">Branch Buying Price (BBP)</asp:ListItem>
            <asp:ListItem Value="CSM">Cosmetic</asp:ListItem>
            <asp:ListItem Value="HRC">Horeca</asp:ListItem>
            <asp:ListItem Value="KA">Key Account</asp:ListItem>
            <asp:ListItem Value="MM">Mini Market</asp:ListItem>
            <asp:ListItem Value="PHA">Pharmacy</asp:ListItem>
            <asp:ListItem Value="RTA">Retail Arab</asp:ListItem>
            <asp:ListItem Value="RTI">Retail Indonesia</asp:ListItem>
            <asp:ListItem Value="WS">Whole Saler</asp:ListItem>
          </asp:CheckBoxList>
       </div>
     </div>
     <div class="form-group">
        <div class="col-md-12">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                <ContentTemplate>
                    <asp:Label ID="lbotlcd" runat="server" CssClass="form-control"></asp:Label>
                </ContentTemplate>
                <Triggers>
                  <asp:AsyncPostBackTrigger  ControlID="chotlcd" EventName="SelectedIndexChanged"/>
                </Triggers>
            </asp:UpdatePanel>
         </div>
     </div>
    <div class="h-divider"></div>
    <div class="form-group">
        <div class="col-md-12">

            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="grdcustcate_cd" runat="server" AutoGenerateColumns="False" Width="100%" BorderStyle="None" GridLines="Horizontal" OnRowDeleting="grdcustcate_cd_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Custcate CD">
                                <ItemTemplate>
                                    <asp:Label ID="lbcustcate_cd" runat="server" Text='<%# Eval("custcate_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Category Name">
                                <ItemTemplate>
                                    <%# Eval("custcate_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Document">
                                <ItemTemplate>
                                    <%# Eval("doc_cd") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Outled">
                                <ItemTemplate>
                                    <%# Eval("otlcd") %>
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
    <div class="row">
    <div class="form-group">
        <div class="h-divider"></div>
        <h4>
        <label class="control-label col-md-2">EXCLUDE</label>
        </h4>
    </div>
    </div>
    <div class="form-group">
        <label class="control-label col-xs-1">Customer</label>
        <div class="col-md-4">
            <div class="input-group">
                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txcustomer" runat="server" CssClass="form-control input-group-sm" Height="100%" Width="50%"></asp:TextBox>
                        <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ServiceMethod="GetCompletionList" ShowOnlyCurrentWordInCompletionListItem="true" TargetControlID="txcustomer" UseContextKey="True">
                        </asp:AutoCompleteExtender>
                        <asp:HiddenField ID="hdcust" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="col-xs-1"> 
        <asp:LinkButton ID="btAddCust" 
            runat="server" 
            CssClass="btn btn-info btn-sm"    
            OnClick="btAddCust_Click"> <span aria-hidden="true" class="glyphicon glyphicon-plus"></span>Add </asp:LinkButton>        
        </div>
    </div>
    <div class="form-group">
                <div class="col-md-12">
                    <div style="width:100%">
    <div style="float:left;width:50%;vertical-align:top;padding:10px 10px 10px 10px">

          <asp:UpdatePanel ID="UpdatePanel5" runat="server">
              <ContentTemplate>
                  <asp:GridView ID="grdcust_cd" runat="server" AutoGenerateColumns="False" BorderStyle="None" GridLines="Horizontal" OnRowDeleting="grdcust_cd_RowDeleting" Width="100%">
                      <Columns>
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

          </div>
                </div>
            </div>
    </div> 
</asp:Content>

