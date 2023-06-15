﻿<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_adjustmentprice_booking.aspx.cs" Inherits="fm_adjustmentprice_booking" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function ItemSelected(sender, e) {
            $get('<%=hditem.ClientID%>').value = e.get_value();
            $get('<%=Button1.ClientID%>').click();
        }

        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
        }

        function PropSelected(sender, e) {
             <%--$get('<%=hdprop.ClientID%>').value = e.get_value();
             $("<%=txcust.ClientID%>").add("class", "ro");--%>
        }
    </script>
    <style>
        .divhid {
            display:none;
        }
        .divnormal {
            display:normal;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
       <div class="form-horizontal" style="font-size:small;font-family:Calibri">
           <h4 class="jajarangenjang">Booking Adjustment Price Setup</h4>
           <div class="h-divider"></div>
           <div class="form-group">
               <label class="control-label col-md-1">Price By</label>
               <div class="col-md-7">
                   hdcust
               </div>
               <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                <ContentTemplate>
               <div id="vSalespoint" runat="server">
                   <label class="control-label col-md-1">Salespoint</label>
                   <div class="col-md-3">
                        <asp:DropDownList ID="cbsalespoint" CssClass="form-control-static" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged">
                        </asp:DropDownList>
                   </div>
               </div>
               </ContentTemplate>
                   <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdcust" EventName="SelectedIndexChanged" />
                   </Triggers>
               </asp:UpdatePanel>
           </div>
           <div class="form-group">
               <div class="col-md-12">
                    <table class="table table-bordered" style="font-size:small;font-family:Calibri">
                        <tr style="background-color:silver">
                            <td>Item</td>
                            <td>Adjust (+/-)</td>
                            <td>Customer / Group / Type</td>
                            <%--<td>Proposal No</td>--%>
                            <td>Booking Date</td>
                            <td>Start Date</td>
                            <td>End Date</td>
                            <td>Save</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                                          <asp:TextBox ID="txitem" runat="server" CssClass="form-control" Height="100%" Width="15em" AutoCompleteType="Search"></asp:TextBox>
                                <asp:AutoCompleteExtender ID="txitem_AutoCompleteExtender" runat="server" CompletionInterval="10" CompletionListElementID="divwidth" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" ServiceMethod="GetCompletionList" TargetControlID="txitem" UseContextKey="True" OnClientItemSelected="ItemSelected">
                                </asp:AutoCompleteExtender>
                                        <asp:HiddenField ID="hditem" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                  
                                <div id="divwidth" style="font-size:smaller;font-family:Calibri">
                        
                                </div>
                            </td><td>
                                <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="txvalue" CssClass="form-control" Height="100%" runat="server" Width="4em" TabIndex="1"></asp:TextBox>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    
                            </td><td>
                                
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                    <ContentTemplate>                         
                                        <asp:TextBox ID="txcust" runat="server" CssClass="form-control" Height="100%" Width="15em" TabIndex="3"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txcust" UseContextKey="True" 
                                            EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" CompletionListElementID="divx" 
                                            OnClientItemSelected="CustSelected">
                                        </asp:AutoCompleteExtender>
                                        <div id="divx" style="font-family:Calibri;font-size:small"></div>
                                        <asp:DropDownList ID="cbgroup" runat="server" Width="20em" TabIndex="4" OnSelectedIndexChanged="cbgroup_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="cbtype" CssClass="form-control-static" runat="server" Width="20em" AutoPostBack="True" OnSelectedIndexChanged="cbtype_SelectedIndexChanged">
                                     </asp:DropDownList>
                                        <asp:HiddenField ID="hdcust" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                    
                   
                            </td>
                            <%--<td>
                                <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                    <ContentTemplate>                         
                                        <asp:TextBox ID="txproposal" runat="server" CssClass="form-control" Height="100%" Width="15em" TabIndex="3"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txproposal_AutoCompleteExtender" runat="server" ServiceMethod="GetListProposal" TargetControlID="txproposal" UseContextKey="True" EnableCaching="false" FirstRowSelected="false" CompletionSetCount="1" CompletionInterval="10" MinimumPrefixLength="1" CompletionListElementID="divx" OnClientItemSelected="PropSelected">
                                        </asp:AutoCompleteExtender>
                                        <asp:HiddenField ID="hdprop" runat="server" />
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </td>--%>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                                    <ContentTemplate>
                                         <asp:TextBox ID="dtbooking" runat="server" Height="100%" Width="8em" TabIndex="4"></asp:TextBox>
                                <asp:CalendarExtender ID="dtbooking_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtbooking">
                                </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                   
                   
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <asp:TextBox ID="dtstart" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                        <asp:CalendarExtender ID="dtstart_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtstart" Format="d/M/yyyy">
                                        </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                   
                   
                            </td>
                            <td>
                                <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                                    <ContentTemplate>
                                         <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-sm" ></asp:TextBox>
                                        <asp:CalendarExtender ID="dtend_CalendarExtender" CssClass="date" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                                        </asp:CalendarExtender>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                   
                   
                            </td>
                            <td>
                                <asp:LinkButton ID="btadd" runat="server" CssClass="btn btn-primary" OnClick="btadd_Click">Add</asp:LinkButton>
                                
                            </td>
                        </tr>
                    </table>
              </div>
           </div>
           <div class="form-group">
               <div class="col-md-12">
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
        <asp:GridView ID="grd" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" OnSelectedIndexChanging="grd_SelectedIndexChanging" CssClass="mygrid">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Code">
                    <ItemTemplate>
                        <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Customer / Group">
                    <ItemTemplate><%# Eval("cust") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Code">
                    <ItemTemplate>
                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Item Name">
                    <ItemTemplate>
                        <asp:Label ID="lbitemname" runat="server" Text='<%# Eval("item_nm") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Proposal No">
                    <ItemTemplate>
                        <asp:Label ID="lbproposal" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Adjustment (+/-)">
                    <ItemTemplate>
                        <asp:Label ID="lbadjust" runat="server" Text='<%# Eval("adjust_value") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Booking Date">
                    <ItemTemplate>
                        <%# Eval("booking_dt","{0:d/M/yyyy}") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date">
                    <ItemTemplate>
                        <asp:Label ID="lbstartdate" runat="server" Text='<%# Eval("start_dt","{0:d/M/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Date">
                    <ItemTemplate>
                        <%# Eval("end_dt","{0:d/M/yyyy}") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowSelectButton="True" />
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
        </asp:UpdatePanel>
        <asp:UpdatePanel ID="aspupd1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdcust" runat="server" AutoGenerateColumns="False" CellPadding="0" ForeColor="#333333" GridLines="None" Width="100%" CssClass="mygrid">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="Salespoint">
                            <ItemTemplate>
                                <asp:Label ID="lbsalespoint" runat="server" Text='<%# Eval("salespointcd") + ":" + Eval("salespoint_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcust" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Name">
                            <ItemTemplate><%# Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Code">
                            <ItemTemplate>
                                <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Item Name">
                            <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Proposal No">
                            <ItemTemplate>
                                <asp:Label ID="lbproposal" runat="server" Text='<%# Eval("prop_no") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Adjustment (+/-)">
                            <ItemTemplate><%# Eval("adjust_value") %></ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="Booking Date">
                            <ItemTemplate><%# Eval("booking_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date">
                            <ItemTemplate><%# Eval("start_dt","{0:d/M/yyyy}") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date">
                            <ItemTemplate><%# Eval("end_dt","{0:d/M/yyyy}") %></ItemTemplate>
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
        </asp:UpdatePanel>
               </div>
           </div>
       </div>
    </div>
       <div class="divgrid">
       

    </div>
    <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"  CssClass="divhid"/>
</asp:Content>

