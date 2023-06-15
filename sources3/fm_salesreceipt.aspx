<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_salesreceipt.aspx.cs" Inherits="fm_salesreceipt" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <%--<link href="css/anekabutton.css" rel="stylesheet" />--%>

 <%--   <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>
    <script src="js/jquery-1.9.1.min.js"></script>
    <link href="css/newbootstrap.css" rel="stylesheet" />--%>
    <%--<link href="Content/beatifullcontrol.css" rel="stylesheet" />--%>
    <script>
        function SearchInvoice(vinv)
        {
            $get('<%=hdinv.ClientID%>').value = vinv;
            $get('<%=btrefresh.ClientID%>').click();
            //alert('gueee');
        }
    </script>
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Invoice Received Customer</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row" >
          <div class="clearfix">
              <div class="col-sm-4 no-padding clearfix margin-bottom">
                   <label class="control-label col-sm-4">Invoice No</label>
                   <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server" class="input-group">
                            <ContentTemplate>
                                <asp:TextBox ID="txinvno" runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                                <div class="input-group-btn">
                                    <asp:LinkButton ID="btsearch" runat="server" OnClick="btsearch_Click" CssClass="btn btn-primary"><i class="fa fa-search" aria-hidden="true"></i></asp:LinkButton>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                   </div>
             </div>
            <div class="col-sm-4 no-padding clearfix margin-bottom">
                   <label class="control-label col-sm-4"> Print Date</label>
                   <div class="col-sm-8">
                       <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                           <ContentTemplate>
                                  <asp:Label ID="lbprintdate" runat="server" CssClass="form-control" ></asp:Label>
                           </ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="click" />
                           </Triggers>
                       </asp:UpdatePanel>
                   </div>
            </div>
            <div class="col-sm-4 no-padding clearfix margin-bottom">
                <label class="control-label col-sm-4">Man No</label>
                  <div class="col-sm-8">
                      <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                          <ContentTemplate>
                                <asp:TextBox ID="txmanualno" runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                          </ContentTemplate>
                      </asp:UpdatePanel>
                  </div>
            </div>
                  
        
          </div>
          <div class="clearfix">
             <div class="col-sm-4 no-padding clearfix margin-bottom">
                 <label class="control-label col-sm-4">Status</label>
                 <div class="col-sm-8 drop-down">
                    <asp:DropDownList ID="cbrcpstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbrcpstatus_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                 </div>
             </div>
              <div class="col-sm-4 no-padding clearfix margin-bottom">
                   <label class="control-label col-sm-4">Recvd Dt</label>
                  <div class="col-sm-8">
                      <asp:UpdatePanel ID="updpnl1" runat="server" class="drop-down-date">
                        <ContentTemplate>
                             <asp:TextBox ID="dtdriverdate" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:CalendarExtender CssClass="date" ID="dtdriverdate_CalendarExtender" runat="server" TargetControlID="dtdriverdate" Format="d/M/yyyy">
                            </asp:CalendarExtender>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbrcpstatus" EventName="SelectedIndexChanged" />
                        </Triggers>
                        </asp:UpdatePanel>
                    </div>
              </div>
              <div class="col-sm-4 no-padding clearfix margin-bottom">
                    <label class="control-label col-sm-4">Driver</label>
                   <div class="col-sm-8">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" class="drop-down">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbdriver" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbdriver_SelectedIndexChanged" CssClass="form-control">
                                </asp:DropDownList>
                                <div class="text-center">
                                    <asp:Label ID="lbvhc" runat="server" CssClass="badge primary margin10"></asp:Label>
                                </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbdriver" EventName="SelectedIndexChanged"/>
                            </Triggers>
                        </asp:UpdatePanel>
                  </div>
              </div>
        </div>
         <div class="clearfix">
             <div class="col-md-4 col-sm-6 no-padding clearfix margin-bottom">
                 <label class="control-label col-sm-4">Remark</label>
                 <div class="col-sm-8">
                     <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                         <ContentTemplate>
                             <asp:Label ID="lbcap" runat="server" CssClass="text-bold text-red"></asp:Label>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                 </div>
             </div>
             <div class="col-md-8 col-sm-6 no-padding clearfix margin-bottom">
                 <label class="control-label col-sm-2">Reason</label>
                 <div class="col-sm-10 drop-down">
                       <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:DropDownList ID="cbreason" runat="server" CssClass="form-control">
                            </asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                 </div>
             </div>
         </div>
        <div class="col-sm-4 col-sm-6 no-padding clearfix margin-bottom">
            <label class="control-label col-sm-4">Quantity</label>
            <div class="col-sm-3">
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txqty" runat="server" CssClass="form-control  input-sm"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="col-md-8 col-sm-6 no-padding clearfix margin-bottom">
                <label class="control-label col-sm-2">Invoice Qty</label>
            <div class="col-sm-2">
                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txqtyinv" runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" class="navi margin-bottom">
            <ContentTemplate>
                <asp:LinkButton ID="btreceiptdriver" OnClick="btreceiptdriver_Click" runat="server" CssClass="btn btn-warning">Received By Driver</asp:LinkButton>
                <%--<asp:Button ID="btreceiptdriver2" runat="server" Text="Receipt By Driver" CssClass="btn btn-warning btn-lg" OnClick="btreceiptdriver_Click" />--%>
            </ContentTemplate>
        </asp:UpdatePanel>
                        <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                            <ContentTemplate>
                                <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" CssClass="table table-striped mygrid" PageSize="20">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Invoice No">
                                            <ItemTemplate>
                                        <%# Eval("inv_no") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="emp cd">
                                            <ItemTemplate>
                                        <%# Eval("emp_cd") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="emp name">
                                            <ItemTemplate>
                                        <%# Eval("emp_nm") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date Reciept">
                                            <ItemTemplate>
                                        <%# Eval("driver_receipt_dt","{0:d/M/yyyy}") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Quantity">
                                            <ItemTemplate>
                                        <%# Eval("qty") %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle CssClass="table-edit" />
                                    <FooterStyle CssClass="table-footer" />
                                    <HeaderStyle CssClass="table-header" />
                                    <PagerStyle CssClass="table-page" />
                                    <RowStyle />
                                    <SelectedRowStyle CssClass="table-edit" />
                                </asp:GridView>
                            </ContentTemplate>
            </asp:UpdatePanel>
        <div class="margin-top">
        </div>   
     
     <div class="h-divider"></div>

       <div class="clearfix ">
          <div class="col-sm-4 clearfix margin-bottom no-padding">
              <label class="control-label col-sm-4">Code</label>
               <div class="col-sm-8">
                   <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                       <ContentTemplate>
                             <asp:TextBox ID="txcust" runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                       </ContentTemplate>
                   </asp:UpdatePanel>
              
                </div>
          </div>
           <div class="col-sm-4 clearfix margin-bottom no-padding">
               <label class="control-label col-sm-4">Cust Nm</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txcustname" runat="server" CssClass="form-control ro " Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                
                </div>
          </div>
           <div class="col-sm-4 clearfix margin-bottom no-padding">
               <label class="control-label col-sm-4">Cust Received</label>
               <div class="col-sm-8">
                   <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                       <ContentTemplate>
                             <asp:TextBox ID="dtreceived" runat="server" CssClass="form-control"></asp:TextBox>
                           <asp:CalendarExtender ID="dtreceived_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="dtreceived">
                           </asp:CalendarExtender>
                       </ContentTemplate>
                   </asp:UpdatePanel>
                </div>
          </div>
       </div>

       <div class="clearfix ">
           <div class="col-sm-4 clearfix margin-bottom no-padding">
               <label class="control-label col-sm-4">Period</label>
               <div class="col-sm-8">
                    <asp:Label ID="lbperiod" runat="server" Font-Bold="True" CssClass="form-control"></asp:Label>
               </div>
           </div>
           <div class="col-sm-4 clearfix margin-bottom no-padding">
               <label class="control-label col-sm-4">Address</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                        <ContentTemplate>
                              <asp:TextBox ID="txaddress" runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  
                </div>
           </div>
           <div class="col-sm-4 clearfix margin-bottom no-padding">
               <label class="control-label col-sm-4">City</label>
               <div class="col-sm-8">
                   <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                       <ContentTemplate>
                             <asp:TextBox ID="txcity" runat="server"  CssClass="form-control ro" Enabled="false"></asp:TextBox>
                       </ContentTemplate>
                   </asp:UpdatePanel>
             
               </div>
           </div>
        </div>   
     
         <div class="clearfix ">
            <div class="col-sm-4 clearfix margin-bottom no-padding">
                <label class="control-label col-sm-4">Load Dt</label>
                <div class="col-sm-8">
                        <asp:TextBox ID="dtload" runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
              
                </div>
            </div>
            <div class="col-sm-4 clearfix margin-bottom no-padding">
                <label class="control-label col-sm-4">Amount</label>
                 <div class="col-sm-8">
                     <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                         <ContentTemplate>
                              <asp:TextBox ID="txamt" runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                   
                  </div>
            </div>
             <div class="col-sm-4 clearfix margin-bottom no-padding">
                 <label class="control-label col-sm-4">Balance</label>
                <div class="col-sm-8">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                              <asp:TextBox ID="txbalance" runat="server" CssClass="form-control ro" Enabled="false"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                  
                </div>
             </div>
         </div> 


       <div class="clearfix">
           <div class="col-md-12">
                <table class="table table-stripped">
                   <tr style="text-align:center">
                       <td style="font-weight:bold;font-family:Calibri;font-size:large">
                           Customer Sign </td>
                       <td style="font-weight:bold;font-family:Calibri;font-size:large">
                           Customer Stamp</td>
                   </tr>
                   <tr style="text-align:center">
                       <td>
                           <asp:Image ID="imgsign" runat="server" ImageUrl="~/noimage.jpg" Height="200px" Width="200px" CssClass="img-rounded" />
                       </td>
                       <td>
                           <asp:Image ID="imgstamp" runat="server" ImageUrl="~/noimage.jpg" Height="200px" Width="200px" CssClass="img-rounded" />
                       </td>
                   </tr>
                   <tr style="text-align:center">
                       <td>
                           &nbsp;</td>
                       <td>
                           &nbsp;</td>
                   </tr>
               </table>
           </div>
       </div>
    
      </div>
    </div>
    <div class="clearfix">
        <div class="navi margin-bottom">
            <div style="text-align:center">
                <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                    <ContentTemplate>
                        <%-- <asp:Button ID="btreceivedx" runat="server" Text="Received By Customer" CssClass="btn btn-success btn-lg" OnClick="btreceived_Click" />--%>
                        <asp:LinkButton ID="btreceived" runat="server" CssClass="btn btn-success " OnClick="btreceived_Click">Received by Customer</asp:LinkButton>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:HiddenField ID="hdinv" runat="server" />
                <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" style="display:none" />
            </div>
        </div>
    </div>
</asp:Content>

