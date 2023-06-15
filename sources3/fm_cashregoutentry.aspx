<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_cashregoutentry.aspx.cs" Inherits="fm_cashregoutentry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
  <%--  <script src="js/jquery-1.9.1.min.js"></script>
    <link href="admin/css/bootstrap.min.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.min.js"></script>--%>
    <script>
        function EmpSelected(sender, e)
        {
            $get('<%=hdemp.ClientID%>').value = e.get_value();
        }

        function RefreshData(sval)
        {
            $get('<%=hdcash.ClientID%>').value = sval;
            $get('<%=btrefresh.ClientID%>').click();
        }

       
    </script>
    <style>
        .auto-complate-list{
            min-width:200px !important;
        }
        .table>tbody>tr>th{
            line-height:12px;
        }
        .table>tbody>tr>td{
            padding-bottom:10px !important;
        }
        .mygrid td, .rounded_corners th{
            min-width:0 !important;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:HiddenField ID="hdcash" runat="server" />
     <div class="divheader">Cashout Request</div>
    <div class="h-divider"></div>


    <div class="container-fluid">
        <div class="row" >
            <div class="clearfix margin-bottom">
                <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                    <label class="control-label col-sm-4">Cashout No</label>
                    <div class="col-sm-8">
                        <div class="input-group">
                             <asp:TextBox ID="txcashoutno" runat="server" CssClass="form-control"></asp:TextBox>
                             <div class="input-group-btn">
                                 <asp:LinkButton ID="btsearch" runat="server" OnClick="btsearch_Click" CssClass="btn btn-primary"><i class="fa fa-search"></i></asp:LinkButton>
                             </div>
                        </div>
                    </div>
                </div>
                <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                    <label class="control-label col-sm-4">Salespoint</label>
                    <div class="col-sm-8">
                            <asp:DropDownList ID="cbsalespoint" runat="server"  OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged" CssClass="form-control"></asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                    <label class="control-label col-sm-4">Date</label>
                    <div class="col-sm-8 drop-down-date">
                         <asp:TextBox ID="dtcashout" runat="server" CssClass="form-control  ro" Enabled="false"></asp:TextBox>
                        <asp:CalendarExtender ID="dtcashout_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="dtcashout">
                        </asp:CalendarExtender>
                    </div>
                </div>
                <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                    <label class="control-label col-sm-4">InOut</label>
                    <div class="col-sm-8 drop-down">
                          <asp:DropDownList ID="cbio" runat="server" OnSelectedIndexChanged="cbio_SelectedIndexChanged" CssClass="form-control input-sm"  AutoPostBack="True"></asp:DropDownList>
                    </div>
                </div>
                <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                      <label class="control-label col-sm-4">Manual No</label>
                    <div class="col-sm-8">
                        <asp:TextBox ID="txmanno" runat="server" CssClass="form-control" ></asp:TextBox>
                    </div>
           
                </div>
            </div>
            <div class="clearfix margin-bottom margin-top">
                
                    <label class="control-label pull-left" style="width:12%;">Remark</label>
                    <div class="pull-left" style="width:calc(88% - 15px)">
                          <asp:TextBox ID="txremark" runat="server"  CssClass="form-control "></asp:TextBox>
                    </div>
                
            </div>
            <div class="clearfix margin-bottom">
                <div class="clearfix col-md-4 col-sm-6 no-padding margin-bottom">
                    <label class="control-label col-sm-4">Cashier Balance</label>
                    <div class="col-sm-8 text-center">
                         <asp:Label ID="lbbalance" runat="server" CssClass="form-control well well-sm no-margin danger text-white text-bold"></asp:Label>
                    </div>
                </div>
                <div class="clearfix col-md-offset-4 col-md-4 col-sm-6 no-padding margin-bottom">
                    <label class="control-label col-sm-4">Status</label>
                    <div class="col-sm-8 text-center">
                       <asp:Label ID="lbstatus" runat="server" Text="NEW REQUEST"  CssClass="form-control well well-sm no-margin danger text-white text-bold"></asp:Label>
                    </div>
                </div>

            </div>


            <div class="divheader subheader subheader-bg">Cashout Details</div>
            <div class="h-divider"></div>


            <div class="clearfix">
                <div class="">
                     <table class="table table-striped mygrid">
                        <tr >
                            <th>REFERENS</th>
                            <th>TYPE</th>
                             <th>ITEM</th>
                            <th>
                                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbbank" runat="server" Text="Bank"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </th>
                            <th>Approval</th>
                            <th>Doc App</th>
                            <th>Person In Charge</th>
                            <th>Routine</th>
                            <th>
                                <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                                    <ContentTemplate>
                                         <asp:Label ID="lbkm" runat="server" Text="KM"></asp:Label>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </th>
                            <th>Qty</th>
                            <th>Amount</th>
                            <th>Asset</th>
                            <th>Claim To HO</th>
                            <th></th>
                        </tr>

                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txref" runat="server"  CssClass="form-control input-sm"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbtype" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbtype_SelectedIndexChanged"  CssClass="form-control input-sm">
                                </asp:DropDownList>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbio" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                    
                            </td>
                            <td class="drop-down">
                                 <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                           <ContentTemplate>
                                <asp:DropDownList ID="cbitem" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbitem_SelectedIndexChanged"  CssClass="form-control input-sm drop-down"></asp:DropDownList></ContentTemplate>
                           <Triggers>
                               <asp:AsyncPostBackTrigger ControlID="cbtype" EventName="SelectedIndexChanged" />
                           </Triggers>
                       </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="cbbankPnl" runat="server">
                                            <asp:DropDownList ID="cbbank" runat="server" CssClass="form-control input-sm">
                                            </asp:DropDownList>
                                        </asp:Panel>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbitem" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                           
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:DropDownList ID="cbapp" runat="server"  CssClass="form-control input-sm">
                                        </asp:DropDownList>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:Panel runat="server" ID="uplfilePnl">
                                    <asp:FileUpload ID="uplfile" runat="server"  CssClass="form-control input-sm"  />
                                </asp:Panel>
                            </td>
                            <td>
                    
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="txpicPnl">
                                             <asp:TextBox ID="txpic" runat="server"  CssClass="form-control input-sm"></asp:TextBox>
                                        </asp:Panel>
                                        <asp:HiddenField ID="hdemp" runat="server" />
                                         <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txpic_AutoCompleteExtender" runat="server" TargetControlID="txpic" ServiceMethod="GetCompletionList" CompletionInterval="10" CompletionSetCount="1" UseContextKey="true" OnClientItemSelected="EmpSelected" CompletionListElementID="divwidth" ShowOnlyCurrentWordInCompletionListItem="true" MinimumPrefixLength="1">
                                </asp:AutoCompleteExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    
                   
                   
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:Label ID="lbroutine" runat="server"  CssClass="form-control input-sm text-red"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="cbitem" EventName="SelectedIndexChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                    
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="txkmPnl">
                                            <asp:TextBox ID="txkm" runat="server"  CssClass="form-control input-sm"></asp:TextBox>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                   
                    
                            </td>
                            <td>
                                <asp:Panel runat="server" ID="txqtyPnl">
                                    <asp:TextBox ID="txqty" runat="server"  CssClass="form-control input-sm"  type="number"></asp:TextBox>
                                </asp:Panel>
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel runat="server" ID="txamtPnl">
                                            <asp:TextBox ID="txamt" runat="server"  CssClass="form-control input-sm" type="number"></asp:TextBox>
                                        </asp:Panel>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>
                            <td>
                                <asp:CheckBox ID="chkasset" runat="server" />
                            </td>
                            <td>
                                <asp:CheckBox ID="chkclaim" runat="server" AutoPostBack="True" OnCheckedChanged="chkclaim_CheckedChanged" />
                            </td>
                            <td>
                                <asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn btn-primary" OnClick="btadd_Click" />
                            </td>
                        </tr>

                    </table>
                </div>
            </div>
        </div>
    </div>
   
    <div class="divgrid">
       
       <div>
          <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
               <ContentTemplate>--%>
                   
           <asp:GridView ID="grd" runat="server" CssClass="table table-striped mygrid" AutoGenerateColumns="False" GridLines="None"  OnRowDeleting="grd_RowDeleting" OnRowDataBound="grd_RowDataBound" CellPadding="0" ShowFooter="True">
               <AlternatingRowStyle/>
               <Columns>
                   <asp:TemplateField HeaderText="Ref no.">
                       <ItemTemplate><%# Eval("ref_no") %></ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Type">
                      <ItemTemplate>
                          <asp:Label ID="lbtype" runat="server" Text='<%# Eval("cashout_typ") %>'></asp:Label>
                      </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Item Name">
                       <ItemTemplate>
                           <asp:HiddenField ID="hditem" Value='<%# Eval("itemco_cd") %>' runat="server" />
                           <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_desc") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Acc No.">
                       <ItemTemplate>
                           <asp:Label ID="lbacc_no" runat="server" Text='<%# Eval("acc_no") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="App File">
                       <ItemTemplate>
                           <asp:Label ID="lbappfile" runat="server" Text='<%# Eval("appfile") %>'></asp:Label>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Approval">
                       <ItemTemplate>
                           <asp:Label ID="lbapp_cd" runat="server" Text=' <%# Eval("app_cd") %>'></asp:Label>
                           <asp:HiddenField ID="hdapp" runat="server" Value='<%# Eval("app_cd") %>' />
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="PIC">
                       <ItemTemplate>
                           <asp:Label ID="lbemp_nm" runat="server" Text='<%# Eval("emp_nm") %>'></asp:Label>
                           <asp:HiddenField ID="hdemp_cd" runat="server" Value='<%# Eval("emp_cd") %>' />
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="KM">
                       <ItemTemplate><%# Eval("km") %></ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Qty">
                       <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="Amount">
                       <ItemTemplate>
                           <asp:Label ID="lbamt" runat="server" Text='<%# Eval("amt") %>'></asp:Label></ItemTemplate>
                       <FooterTemplate>
                          TOTAL : <asp:Label ID="lbtotamt" runat="server"></asp:Label>
                       </FooterTemplate>
                       <FooterStyle HorizontalAlign="Right" />
                       <HeaderStyle HorizontalAlign="Right" />
                       <ItemStyle HorizontalAlign="Right" />
                   </asp:TemplateField>
                   <asp:TemplateField HeaderText="R/NR">
                       <ItemTemplate>
                           <asp:DropDownList ID="cbroutine" runat="server" CssClass="form-control input-sm"></asp:DropDownList>
                       </ItemTemplate>
                   </asp:TemplateField>
                   <asp:CheckBoxField DataField="isasset" HeaderText="Asset" />
                   
                   <asp:CheckBoxField DataField="isclaim" HeaderText="Claim HO" />
                   <asp:CommandField ShowDeleteButton="True" />
               </Columns>
               <EditRowStyle CssClass="table-edit" />
               <FooterStyle CssClass="table-footer" />
               <HeaderStyle CssClass="table-header"/>
               <PagerStyle CssClass="table-page"/>
               <RowStyle  />
               <SelectedRowStyle CssClass="table-edit"/>
               <SortedAscendingCellStyle BackColor="#E9E7E2" />
               <SortedAscendingHeaderStyle BackColor="#506C8C" />
               <SortedDescendingCellStyle BackColor="#FFFDF8" />
               <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
           </asp:GridView>
              <%-- </ContentTemplate>
               <Triggers>
                   <asp:AsyncPostBackTrigger ControlID="btadd" EventName="Click" />
               </Triggers>
           </asp:UpdatePanel>--%>
          
       </div>
    </div>
    <div class="h-divider"></div>
    
     <div class="navi margin-bottom">
         <asp:Button ID="btnew" runat="server" Text="New" CssClass="btn btn-success " OnClick="btnew_Click"/>
         <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-warning " OnClick="btsave_Click" />
         <asp:Button ID="btprint" runat="server" Text="Print" CssClass="btn btn-info " OnClick="btprint_Click"/>
         <asp:Button ID="btrefresh" runat="server" OnClick="btrefresh_Click" Text="Button" CssClass="divhid"/>
     </div> 
    <table>
        <tr>
            <td>
                 <div id="divwidth"></div>
            </td>
        </tr>
    </table>
</asp:Content>

