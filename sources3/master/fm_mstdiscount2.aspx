<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_mstdiscount2.aspx.cs" Inherits="fm_mstdiscount2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="css/anekabutton.css" rel="stylesheet" />
    <script>
        function RefreshData() {
            $get('<%=btrefresh.ClientID%>').click();
            //sweetAlert('Activated', '', 'success');
            return (false);
        }
    </script>
    <script>
        function openwindow(url)
        {
            mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=800,height=600");
            mywindow.moveTo(400, 200);
        }

        function DiscSelected(sender,e)
        {
            $get('<%=hddisc.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
            
        }
    </script>
    <script type = "text/javascript">
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to save data?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
    </script>
    <script type="text/javascript">
        //Stop Form Submission of Enter Key Press
        function stopRKey(evt) {
            var evt = (evt) ? evt : ((event) ? event : null);
            var node = (evt.target) ? evt.target : ((evt.srcElement) ? evt.srcElement : null);
            if ((evt.keyCode == 13) && (node.type == "text")) { return false; }
        }
        document.onkeypress = stopRKey;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
     <asp:HiddenField ID="hddisc" runat="server" />
    <div class="form-horizontal" style="font-size:small;font-family:Calibri">
    <h4 class="jajajarangenjang"> Master Discount</h4>
    <div class="h-divider"></div>
    <div class="form-group">
        <label class="control-label col-md-1">Search</label>
        <div class="col-md-6">
            <asp:TextBox ID="txsearch" CssClass="form-control" runat="server"></asp:TextBox>
            <asp:AutoCompleteExtender ID="txsearch_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txsearch" UseContextKey="True" FirstRowSelected="false" EnableCaching="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="DiscSelected">
                    </asp:AutoCompleteExtender>
        </div>
         <label class="control-label col-md-1">Status</label>
        <div class="col-md-2">
             <asp:DropDownList ID="cbstatus" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbstatus_SelectedIndexChanged" CssClass="form-control">
                        </asp:DropDownList>
              <asp:CheckBox ID="chall" runat="server" OnCheckedChanged="chall_CheckedChanged" AutoPostBack="True" Text="All" />
        </div>
                       
                 
    </div>
        <div class="form-group">
        <div class="col-md-12">
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grddisc" runat="server" CssClass="mGrid" AutoGenerateColumns="False" AllowPaging="True" OnSelectedIndexChanging="grddisc_SelectedIndexChanging" OnRowDeleting="grddisc_RowDeleting" OnRowEditing="grddisc_RowEditing" OnPageIndexChanging="grddisc_PageIndexChanging" OnRowDataBound="grddisc_RowDataBound" CellPadding="0" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="Discount Code">
                    <ItemTemplate>
                        <asp:Label ID="lbdisccode" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label>
                        </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Description">
                   <ItemTemplate>
                       <a href="javascript:openwindow('fm_discountinfo.aspx?dc=<%# Eval("disc_cd") %>');"><%# Eval("remark") %></a>
                       
                   </ItemTemplate>
                    
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Delivery Date">
                    <ItemTemplate>
                        <asp:Label ID="lbdelivery_dt" runat="server" Text='<%# Eval("delivery_dt","{0:d/M/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Start Date">
                    <ItemTemplate>
                        <asp:Label ID="lbstart_dt" runat="server" Text='<%# Eval("start_dt","{0:d/M/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Expired Date">
                    <ItemTemplate>
                        <asp:Label ID="lbend_dt" runat="server" Text='<%# Eval("end_dt","{0:d/M/yyyy}") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Prop No">
                    <ItemTemplate><%# Eval("proposal_no") %></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status" InsertVisible="False">
                    <ItemTemplate><strong><%# Eval("disc_sta_nm") %></strong></ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="false">
                <%--<asp:TemplateField HeaderText="sta_upd">--%>
                    <ItemTemplate>
                        <asp:Label ID="lbsta_upd" runat="server" Text='<%# Eval("sta_upd") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:CommandField HeaderText="ACTION" DeleteText="Deactivated" ShowDeleteButton="True" ShowEditButton="True" />--%>
                <asp:CommandField HeaderText="Edit" ShowEditButton="True" />
                <asp:CommandField HeaderText="Action" DeleteText="Deactivated" ShowDeleteButton="True"  />
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
                <asp:AsyncPostBackTrigger ControlID="cbstatus" EventName="SelectedIndexChanged" />
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged"></asp:AsyncPostBackTrigger>
                <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="chall" EventName="CheckedChanged" />
                <asp:AsyncPostBackTrigger ControlID="btrefresh" EventName="Click"></asp:AsyncPostBackTrigger>
            </Triggers>
        </asp:UpdatePanel>
        </div>
    </div>
        <div class="form-group">
            <div class="col-md-12" style="text-align:center">
                <asp:LinkButton ID="btnew" OnClick="btnew_Click" runat="server" CssClass="btn btn-primary">New</asp:LinkButton>
                <asp:LinkButton ID="btprint" OnClick="btprint_Click" runat="server" CssClass="btn btn-success">Print</asp:LinkButton>
            </div>
        </div>
  </div>
        <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" style="display:none" />
        <div class="navi">
        <asp:Button ID="btsearch" runat="server" OnClick="btsearch_Click" Text="Button" style="display:none" />

            
        <asp:Button ID="btprxint" runat="server" Text="Print" style="display:none" OnClick="btprint_Click" />
     
     </div>
</asp:Content>

