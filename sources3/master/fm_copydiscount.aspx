<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_copydiscount.aspx.cs" Inherits="master_fm_copydiscount" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>
        function DiscSelected(sender, e)
        {
            $get('<%=hddisc.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function PropSelected(sender, e) {
            $get('<%=hdprop.ClientID%>').value = e.get_value();
            $get('<%=btsearchdisc.ClientID%>').click();
          }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hddisc" runat="server" />
    <asp:HiddenField ID="hdprop" runat="server" />
    <div class="form-horizontal" style="font-size:small;font-family:Calibri">
        <h4 class="jajarangenjang">Copy Discount/scheme</h4>
        <div class="h-divider"></div>
        <div class="form-group">
             <label class="control-label col-md-1">Proposal</label>
            <div class="col-md-8">
                <asp:TextBox ID="txproposal" runat="server" CssClass="form-control"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txproposal_AutoCompleteExtender" runat="server" TargetControlID="txproposal" MinimumPrefixLength="1" EnableCaching="false" CompletionInterval="10" CompletionSetCount="1" FirstRowSelected="false" OnClientItemSelected="PropSelected" ServiceMethod="GetCompletionList2" UseContextKey="True">
                </asp:AutoCompleteExtender>
            </div>
        </div>
        <h4 class="jajarangenjang">Discount Hit By Proposal</h4>
        <div class="h-divider"></div>
        <div class="form-group">
            <div class="col-md-12">
                <asp:GridView ID="grd" runat="server" CssClass="mydatagrid" AutoGenerateColumns="False" HeaderStyle-CssClass="header" SelectedRowStyle-CssClass="rows" PagerStyle-CssClass="pager" OnSelectedIndexChanging="grd_SelectedIndexChanging" CellPadding="0" EmptyDataText="No Discount Found !" ShowHeaderWhenEmpty="True">
                    <Columns>
                        <asp:TemplateField HeaderText="Disc Code">
                            <ItemTemplate>
                                <asp:Label ID="lbdisccode" runat="server" Text='<%#Eval("disc_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate><%#Eval("remark") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Start Date">
                            <ItemTemplate>
                                <asp:Label ID="lbstartdate" runat="server" Text='<%#Eval("start_dt","{0:d/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="End Date">
                            <ItemTemplate>
                                <asp:Label ID="lbenddate" runat="server" Text='<%#Eval("end_dt","{0:d/MM/yyyy}") %>'></asp:Label></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delivery Date">
                            <ItemTemplate>
                                <asp:Label ID="lbdeliverydate" runat="server" Text='<%#Eval("delivery_dt","{0:d/MM/yyyy}") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Status">
                            <ItemTemplate><%#Eval("disc_sta_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField HeaderText="Detail" SelectText="Detail" ShowSelectButton="True" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
   
        <div class="form-group">
            <label class="control-label col-md-1">Selected</label>
             <div class="col-md-2">
                 <asp:Label ID="lbselecteddiscount" CssClass="form-control" runat="server"></asp:Label>
             </div>
             <label class="control-label col-md-1">Method</label>
             <div class="col-md-2">
                 <asp:Label ID="lbmethod" CssClass="form-control" runat="server"></asp:Label>
             </div>
             <label class="control-label col-md-1">Disc For</label>
             <div class="col-md-2">
                 <asp:Label ID="lbcust" CssClass="form-control" runat="server"></asp:Label>
             </div>
            <label class="control-label col-md-1">Free By</label>
             <div class="col-md-2">
                 <asp:Label ID="lbfreeitem" CssClass="form-control" runat="server"></asp:Label>
             </div>
        </div>
        <div class="form-group">
            <div class="btn btn-block">
               <table style="width:100%">
                   <tr style="background-color:silver;vertical-align:top"><th>Effective Salespoint</th><th>Effective Group Customer</th><th>Effective Channel</th><th>Effective Customer</th></tr>
                   <tr style="vertical-align:top"><td>
                       <asp:GridView ID="grdsp" CssClass="mGrid" runat="server" AutoGenerateColumns="False">
                           <Columns>
                               <asp:TemplateField HeaderText="Salespoint Code">
                                   <ItemTemplate><%#Eval("salespointcd") %></ItemTemplate>
                               </asp:TemplateField>
                               <asp:TemplateField HeaderText="Salespoint Name">
                                   <ItemTemplate><%#Eval("salespoint_nm") %></ItemTemplate>
                               </asp:TemplateField>
                           </Columns>
                       </asp:GridView>
                       </td>
                       <td>
                           <asp:GridView ID="grdcusgrcd" CssClass="mGrid" runat="server" AutoGenerateColumns="False" CellPadding="0" EmptyDataText="No Group Customer" ShowHeaderWhenEmpty="True">
                               <Columns>
                                   <asp:TemplateField HeaderText="Group Code">
                                       <ItemTemplate><%#Eval("cusgrcd") %></ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Group Name">
                                       <ItemTemplate><%#Eval("cusgrcd_nm") %></ItemTemplate>
                                   </asp:TemplateField>
                               </Columns>
                           </asp:GridView>
                       </td>
                       <td>
                           <asp:GridView ID="grdchannel" runat="server" CssClass="mGrid" AutoGenerateColumns="False" EmptyDataText="No Channel Found" ShowHeaderWhenEmpty="True">
                               <Columns>
                                   <asp:TemplateField HeaderText="Channel Code">
                                       <ItemTemplate><%#Eval("otlcd") %></ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Channel Name">
                                       <ItemTemplate><%#Eval("otlcd") %></ItemTemplate>
                                   </asp:TemplateField>
                               </Columns>
                           </asp:GridView>
                       </td>
                       <td>
                           <asp:GridView ID="grdcust" runat="server" CssClass="mGrid" AutoGenerateColumns="False" EmptyDataText="No Customer Found" ShowHeaderWhenEmpty="True">
                               <Columns>
                                   <asp:TemplateField HeaderText="Salespoint Code">
                                       <ItemTemplate><%#Eval("salespoint_desc") %></ItemTemplate>
                                   </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Customer Name">
                                       <ItemTemplate><%#Eval("cust_desc") %></ItemTemplate>
                                   </asp:TemplateField>
                               </Columns>
                           </asp:GridView>
                       </td>
                   </tr>
               </table>
               
            </div>
        </div>
        <h4 class="jajarangenjang">Discount Date Changed</h4>
        <div class="h-divider"></div>
        <div class="form-group">
              <label class="control-label col-md-1">Start Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtstart" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" TargetControlID="dtstart" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
                <label class="control-label col-md-1">Delivery Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtdelivery" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtdelivery_CalendarExtender" runat="server" TargetControlID="dtdelivery" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
            <label class="control-label col-md-1">End Date</label>
            <div class="col-md-2">
                <asp:TextBox ID="dtend" CssClass="form-control" runat="server"></asp:TextBox>
                <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" TargetControlID="dtend" Format="d/M/yyyy">
                </asp:CalendarExtender>
            </div>
            <div class="col-md-2">
                <asp:LinkButton ID="btexecute" OnClientClick="javascript:ShowProgress();" OnClick="btexecute_Click" CssClass="btn btn-primary" runat="server">Duplicate Now!</asp:LinkButton>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Remark</label>
            <div class="col-md-11">
                <asp:TextBox ID="txremark" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">New Discount</label>
            <div class="col-md-2">
                <asp:Label ID="lbnewdiscount" CssClass="form-control" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <div class="form-group">
            <div class="col-md-12" style="text-align:center">
                <asp:LinkButton ID="btnew" CssClass="btn btn-success" runat="server" OnClick="btnew_Click">New</asp:LinkButton>
            </div>
        </div>
         
    </div>
    <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" style="display:none"/>
    <asp:Button ID="btsearchdisc" runat="server" Text="Button" OnClick="btsearchdisc_Click" style="display:none" />
</asp:Content>

