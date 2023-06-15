<%@ Page Title="" Language="C#" MasterPageFile="~/master/homaster.master" AutoEventWireup="true" CodeFile="fm_reqdiscount3.aspx.cs" Inherits="fm_reqdiscount3" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />    
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>     
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <script>

        function PropSelected(sender, e) {
            $get('<%=hdprop.ClientID%>').value = e.get_value();
            $get('<%=btrefresh.ClientID%>').click();
        }

        function CustSelected(sender, e) {
            $get('<%=hdcust_new.ClientID%>').value = e.get_value();
        }

        function openwindow(url) {
            mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=800,height=600");
            mywindow.moveTo(400, 200);
        }

        function ShowPopup() {
            $("#btnLaunch").click();
        }
  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    
    <asp:HiddenField ID="hditem" runat="server" />
    <asp:HiddenField ID="hdfreeitem" runat="server" />
    <asp:HiddenField ID="hdprop" runat="server" />
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hdcust_new" runat="server" />
    <asp:HiddenField ID="hdvendor" runat="server" />
   
    <%--<div class="container-fluid">--%>
    <div class="page-header">
        <h4 class="jajarangenjang">Discount Entry
            <br />
            <small>Branch : 
            <asp:Label ID="lbsalespoint" runat="server" ForeColor="Red"></asp:Label>
            </small>
            <!-- Button trigger modal -->
            <button style="display:none" type="button" class="btn btn-primary btn-lg" data-toggle="modal" data-target="#checkDiscount" id="btnLaunch">
            </button>
        </h4>
    </div>
    <div class="h-divider"></div>


    <!-- Modal -->
    <div class="modal fade" id="checkDiscount" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
      <div class="modal-dialog" role="document">
        <div class="modal-content">
          <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title" id="myModalLabel">Similar Discount</h4>
          </div>
          <div class="modal-body">
            <asp:UpdatePanel ID="UpdatePanel19" runat="server">
            <ContentTemplate>
            <div class="table-responsive">
                <asp:GridView ID="grddiscount" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found" >  
                    <Columns> 
                        <asp:TemplateField HeaderText="Discount No.">
                            <ItemTemplate>
                                <a href="javascript:openwindow('fm_discountinfo.aspx?dc=<%# Eval("disc_cd")%>');">
                                <asp:Label ID="lbdiscount" runat="server" Text='<%# Eval("disc_cd") %>'></asp:Label>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remark">
                            <ItemTemplate>
                                <%# Eval("remark") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
            </ContentTemplate>
            </asp:UpdatePanel>
          </div>
          <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
          </div>
        </div>
      </div>
    </div>
       

    <div class="bd-example">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="propNo">Prop No</label>
                    <asp:TextBox ID="txproposal" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txproposal_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList3" TargetControlID="txproposal" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="PropSelected">
                    </asp:AutoCompleteExtender>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="discCode" class="col-xs-4 col-form-label">Discount No</label>
                    <div class="col-xs-8">
                        <asp:TextBox ID="txdisccode" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="discType" class="col-xs-4 col-form-label">Discount Type</label>
                    <div class="col-xs-8">
                    <asp:DropDownList ID="cbdisctyp" runat="server" CssClass="form-control-static" Height="2em" Width="100%"></asp:DropDownList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="startDate" class="col-xs-4 col-form-label">Start Date</label>
                    <div class="col-xs-8">
                    <asp:TextBox ID="dtstart" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    <asp:CalendarExtender ID="dtstart_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtstart">
                        </asp:CalendarExtender>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="endDate" class="col-xs-4 col-form-label">End Date</label>
                    <div class="col-xs-8">
                    <asp:TextBox ID="dtend" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    <asp:CalendarExtender ID="dtend_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtend">
                        </asp:CalendarExtender>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="deliveryDate" class="col-xs-4 col-form-label">Delivery Date</label>
                    <div class="col-xs-8">
                    <asp:TextBox ID="dtdelivery" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    <asp:CalendarExtender ID="dtdelivery_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtdelivery">
                        </asp:CalendarExtender>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="deliveryDate" class="col-xs-4 col-form-label">Max Claim</label>
                    <div class="col-xs-8">
                    <asp:TextBox ID="dtmaxclaim" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="remark">Remark</label>
                    <asp:TextBox ID="txremark" runat="server" CssClass="form-control input-group-lg" TextMode="MultiLine"></asp:TextBox>
                </div>
            </div>
        </div>        
    </div>

    <div class="bd-example">
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="refNo" class="col-xs-4 col-form-label">Vendor Ref No.</label>
                    <div class="col-xs-8">
                    <asp:TextBox ID="txvendorref" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="vendor" class="col-xs-4 col-form-label">Vendor</label>
                    <div class="col-xs-8">
                    <asp:TextBox ID="txvendor" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <label for="disc" class="col-xs-4 col-form-label">Disc Used To</label>
                <div class="col-xs-8">
                    <asp:RadioButtonList ID="rdused" runat="server" CssClass="form-control" style="background-color:skyblue" RepeatDirection="Horizontal" CellPadding="0" CellSpacing="0" Width="100%">
                        <asp:ListItem Value="M">Manual</asp:ListItem>
                        <asp:ListItem Value="A">Automatic</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>
            <div class="col-sm-6">
                <label for="benefit" class="col-xs-4 col-form-label">Benefit Promotion</label>
                <div class="col-xs-8">
                    <asp:TextBox ID="txbenefit" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                </div>
            </div>
        </div>

        <div class="row">
            <div class="col-sm-6">
                <label for="disc" class="col-xs-4 col-form-label">Salespoint</label>
                <div class="col-xs-8">
                    <asp:RadioButtonList ID="rdisalespoint" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdisalespoint_SelectedIndexChanged" CssClass="form-control" style="background-color:skyblue" RepeatDirection="Horizontal"  Width="100%">
                        <asp:ListItem Value="Y">From Proposal</asp:ListItem>
                        <asp:ListItem Value="N">Create New</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="col-sm-6">                
                <div class="col-xs-8">
                    <asp:UpdatePanel ID="UpdatePanel17" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="rdsalespoint" runat="server" CssClass="form-control-static"  Width="100%">
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rdisalespoint" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel> 
                </div>    
                <div class="col-xs-4">
                    <asp:UpdatePanel ID="UpdatePanel18" runat="server">
                        <ContentTemplate>
                            <div class="col-xs-2">
                            </div>
                            <div class="col-xs-2">
                                <asp:LinkButton ID="btaddsalespoint" runat="server" CssClass="btn btn-primary" OnClick="btaddsalespoint_Click">ADD</asp:LinkButton>
                             </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rdisalespoint" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>                  
            </div>
        </div>
        <div class="col-sm-12">
            <div class="form-group">
                <asp:UpdatePanel ID="UpdatePanel24" runat="server">
                <ContentTemplate>
                <div class="table-responsive">
                    <asp:GridView ID="grdslspoint" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                        <Columns>
                            <asp:TemplateField HeaderText="Salespoint Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                <ItemTemplate>
                                    <asp:Label ID="lblslspointcd" runat="server" Text='<%# Eval("salespoint_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salespoint Name" ItemStyle-CssClass="visible-xs" HeaderStyle-CssClass="visible-xs">
                                <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdslspointnew" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                        <Columns>
                            <asp:TemplateField HeaderText="Salespoint Code" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg">
                                <ItemTemplate>
                                    <asp:Label ID="lblslspointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salespoint Name" ItemStyle-CssClass="visible-xs" HeaderStyle-CssClass="visible-xs">
                                <ItemTemplate><%# Eval("salespoint_nm") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <label for="disc" class="col-xs-4 col-form-label">Customer</label>
                <div class="col-xs-8">
                    <asp:RadioButtonList ID="rdicust" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdicust_SelectedIndexChanged" CssClass="form-control" style="background-color:skyblue" RepeatDirection="Horizontal"  Width="100%">
                        <asp:ListItem Value="Y">From Proposal</asp:ListItem>
                        <asp:ListItem Value="N">Create New</asp:ListItem>
                    </asp:RadioButtonList>
                </div>
            </div>

            <div class="col-sm-6">                
                <div class="col-xs-4">
                    <asp:UpdatePanel ID="UpdatePanel13" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="rdcust" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdcust_SelectedIndexChanged" CssClass="form-control-static"  Width="100%">
                                <asp:ListItem Value="C">Customer</asp:ListItem>
                                <asp:ListItem Value="G">Customer Group</asp:ListItem>
                                <asp:ListItem Value="T">Customer Type</asp:ListItem>
                            </asp:DropDownList>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rdicust" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel> 
                </div>    
                <div class="col-xs-8">
                    <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                        <ContentTemplate>
                            <div class="col-xs-8">
                            <asp:TextBox ID="txsearchcust" runat="server" CssClass="form-control input-group-lg" Height="2em"  ></asp:TextBox>
                            <asp:AutoCompleteExtender ID="txsearchcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList2" TargetControlID="txsearchcust" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" ShowOnlyCurrentWordInCompletionListItem="true" OnClientItemSelected="CustSelected">
                            </asp:AutoCompleteExtender>
                            <asp:DropDownList ID="cbcusgrcd" runat="server" AutoPostBack="True" CssClass="form-control-static"  >
                            </asp:DropDownList>
                            </div>
                            <div class="col-xs-4">
                                <asp:LinkButton ID="btaddcust" OnClick="btaddcust_Click" CssClass="btn btn-warning" runat="server">ADD</asp:LinkButton>
                            </div>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="rdcust" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>                  
            </div>
        </div>

        <div class="col-sm-12">
            <div class="form-group">
                <asp:UpdatePanel ID="UpdatePanel15" runat="server">
                <ContentTemplate>
                <div class="table-responsive">
                    <asp:GridView ID="grdcust" AllowPaging="True" 
                        OnPageIndexChanging="grdfreeitem_PageIndexChanging" OnRowDeleting="grdcust_RowDeleting" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">
                        <Columns>
                            <asp:TemplateField HeaderText="Cust Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcustcode" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Name">
                                <ItemTemplate>
                                    <%# Eval("cust_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Type">
                                <ItemTemplate>
                                    <%# Eval("otlcd") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salespoint Code">
                                <ItemTemplate>
                                    <asp:Label ID="lblsalespointcd" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:CommandField ShowDeleteButton="True" />--%>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdcusgrcd" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDeleting="grdcusgrcd_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Group Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcusgrcd" runat="server" Text='<%# Eval("cusgrcd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Name">
                                <ItemTemplate><%# Eval("cusgrcd_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:CommandField ShowDeleteButton="True" />--%>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdcusttype" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDeleting="grdcusttype_RowDeleting">
                        <Columns>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbcusttype" runat="server" Text='<%# Eval("cust_typ") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Type">
                                <ItemTemplate>
                                    <%# Eval("custtyp_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <%--<asp:CommandField ShowDeleteButton="True" />--%>
                        </Columns>
                    </asp:GridView>
                </div>
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btaddcust" EventName="click" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="col-sm-12">
            <div class="form-group">
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                <ContentTemplate>
                <div class="table-responsive">
                    <asp:GridView ID="grdviewcust" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDeleting="grdcust_RowDeleting">  
                        <Columns>
                            <asp:TemplateField HeaderText="Cust Code">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Name">
                                <ItemTemplate>
                                    <%# Eval("cust_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Cust Type">
                                <ItemTemplate>
                                    <%# Eval("otlcd") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Salespoint Code">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("salespointcd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdviewcusgrcd" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDeleting="grdcusgrcd_RowDeleting">  
                        <Columns>
                            <asp:TemplateField HeaderText="Group Code">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("cusgrcd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Name">
                                <ItemTemplate><%# Eval("cusgrcd_nm") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdviewcusttype" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                        <Columns>
                            <asp:TemplateField HeaderText="Code">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("cust_typ") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Customer Type">
                                <ItemTemplate>
                                    <%# Eval("custtyp_nm") %>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>

        <div class="col-sm-12">
            <div class="form-group">
                <label for="product">Product</label>
                <asp:UpdatePanel ID="UpdatePanel16" runat="server">
                <ContentTemplate>
                <div class="table-responsive">
                    <asp:GridView ID="grdviewgroup" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDeleting="grdproduct_RowDeleting">  
                        <Columns>
                            <asp:TemplateField HeaderText="Group Code">
                            <ItemTemplate>
                                <asp:Label ID="lbgroupcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Group Name">
                                <ItemTemplate><%# Eval("prod_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblitemcode" runat="server" Text='<%# String.Format("{0}_{1}", Eval("item_cd"), Eval("item_nm")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DBP">
                                <ItemTemplate><%# Eval("dbp") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="BBP">
                                <ItemTemplate><%# Eval("bbp") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RBP - Before">
                                <ItemTemplate>
                                    <asp:Label ID="lbrbpbefore" runat="server" Text='<%# Eval("price_rbp_before") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RBP - After">
                                <ItemTemplate>
                                    <asp:Label ID="lbrbpafter" runat="server" Text='<%# Eval("price_rbp") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdviewitem" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDeleting="grditem_RowDeleting">  
                        <Columns>
                            <asp:TemplateField HeaderText="Item Code">
                                <ItemTemplate>
                                    <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item Name">
                                <ItemTemplate><%# Eval("item_nm") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DBP">
                                <ItemTemplate><%# Eval("price_dbp") %></ItemTemplate>
                            </asp:TemplateField>
                                <asp:TemplateField HeaderText="BBP">
                                <ItemTemplate><%# Eval("price_bbp") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RBP - Before">
                                <ItemTemplate>
                                    <asp:Label ID="lbrbpbefore" runat="server" Text='<%# Eval("price_rbp_before") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RBP - After">
                                <ItemTemplate>
                                    <asp:Label ID="lbrbpafter" runat="server" Text='<%# Eval("price_rbp") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowDeleteButton="True" />
                        </Columns>
                    </asp:GridView>
                </div>
                </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="regCost" class="col-xs-4 col-form-label">Regular Cost</label>
                    <div class="col-xs-8">
                    <asp:TextBox ID="txregularcost" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="netCost" class="col-xs-4 col-form-label">Net Cost</label>
                    <div class="col-xs-8">
                    <asp:TextBox ID="txnetcost" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="orderMin" class="col-xs-4 col-form-label">Minimum Order</label>
                    <div class="col-xs-8">
                    <asp:TextBox ID="txminqty" runat="server" CssClass="form-control input-group-lg" Height="2em" OnTextChanged="txminqty_TextChanged" AutoPostBack="True"></asp:TextBox>
                    </div>
                </div>
            </div>
            <div class="col-sm-6">
                <div class="form-group">
                    <label for="orderMax" class="col-xs-4 col-form-label">Maximum Order</label>
                    <div class="col-xs-8">
                    <asp:TextBox ID="txmaxorder" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="catalog" class="col-xs-2 col-form-label">Catalog Image</label>
                    <div class="col-xs-10">
                        <asp:FileUpload ID="uplcatalog" runat="server" CssClass="form-control" />
                        <asp:HyperLink ID="hpfile_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                        <asp:Label ID="lbfileloc" runat="server" Text='Catalog Image'></asp:Label></asp:HyperLink>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="bd-example">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="discMechanism" class="col-xs-3 col-form-label">Discount Mechanism</label>
                    <div class="col-xs-9">
                    <asp:RadioButtonList ID="rdmethod" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdmethod_SelectedIndexChanged" CssClass="form-control" style="background-color:skyblue" RepeatDirection="Horizontal"  Width="100%">
                        <asp:ListItem Value="FG">Free Good</asp:ListItem>
                        <asp:ListItem Value="CH">Free Cash</asp:ListItem>
                        <asp:ListItem Value="PC">Free Percentage</asp:ListItem>
                    </asp:RadioButtonList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <div class="col-md-1">
                        <label for="minQty">Min Qty</label>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txordermin" runat="server" CssClass="form-control input-group-lg" Height="2em" ></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="txminqty" EventName="TextChanged" />
                            </Triggers>
                        </asp:UpdatePanel>                        
                    </div>
                    <div class="col-md-2">
                        <label for="minQty">UOM</label>
                        <asp:DropDownList ID="cbuom" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                    </div>
                    <div class="col-md-2">
                        <label for="minQty">Free Qty</label>
                        <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txqty" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>                        
                    </div>
                    <div class="col-md-2">
                        <label for="minQty">UOM Free</label>
                        <asp:DropDownList ID="cbuomfree" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <label for="minQty">Cash</label>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txcash" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>                        
                    </div>
                    <div class="col-md-1">
                        <label for="minQty">% Cash</label>
                        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                                <asp:TextBox ID="txpercent" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>                        
                    </div>
                    <div class="col-md-2">
                        <label for="minQty">Type</label>
                        <asp:DropDownList ID="cbmethod" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                    </div>
                    <div class="col-md-1">
                        <label for="minQty"> </label>
                         <asp:LinkButton ID="btaddformula" OnClick="btaddformula_Click" CssClass="btn btn-danger" runat="server">ADD</asp:LinkButton>
                    </div>
                </div>
            </div>
        </div>
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                    <div class="table-responsive">
                        <asp:GridView ID="grdformula" OnRowDeleting="grdformula_RowDeleting" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                            <Columns>
                                <asp:TemplateField HeaderText="Min Qty">
                                     <ItemTemplate>
                                         <asp:Label ID="lbminqty" runat="server" Text='<%# Eval("min_qty") %>'></asp:Label>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="UOM">
                                     <ItemTemplate>
                                         <%# Eval("UOM") %>
                                     </ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Free Qty">
                                     <ItemTemplate><%# Eval("qty") %></ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="UOM Free">
                                     <ItemTemplate><%# Eval("uom_free") %></ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Free Cash">
                                     <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Free Pct">
                                     <ItemTemplate><%# Eval("percentage") %></ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Disc Type">
                                     <ItemTemplate><%# Eval("disc_typ") %></ItemTemplate>
                                 </asp:TemplateField>
                                 <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btaddformula" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
    </div>

    <asp:UpdatePanel ID="UpdatePanel11" runat="server">
    <ContentTemplate>
    <div id="tbdiscount" runat="server">
        <div class="bd-example">
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <label for="freeItem" class="col-xs-3 col-form-label">Free Item</label>
                    <div class="col-xs-9">
                    <asp:RadioButtonList ID="rdfree" runat="server" AutoPostBack="True" OnSelectedIndexChanged="rdfree_SelectedIndexChanged" CssClass="form-control" style="background-color:skyblue" RepeatDirection="Horizontal"  Width="100%">
                        <asp:ListItem Value="P">Item</asp:ListItem>
                        <asp:ListItem Value="G">Product </asp:ListItem>
                    </asp:RadioButtonList>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <div class="col-md-2">
                        <label for="branded" style="background-color:skyblue">Branded</label>
                        <asp:DropDownList ID="cbbrandedfree" runat="server" OnSelectedIndexChanged="cbbrandedfree_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%"></asp:DropDownList>  
                    </div>
                    <div class="col-md-3">
                        <label for="branded" style="background-color:skyblue">Group Product</label>
                        <asp:UpdatePanel ID="UpdatePanel6" runat="server">
                            <ContentTemplate>
                                 <asp:DropDownList ID="cbprodgroupfree" runat="server" OnSelectedIndexChanged="cbprodgroupfree_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%"></asp:DropDownList> 
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbbrandedfree" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>                        
                    </div>
                    <div class="col-md-3">
                        <label for="branded" style="background-color:skyblue">Product</label>
                        <asp:UpdatePanel ID="UpdatePanel7" runat="server">
                            <ContentTemplate>
                                 <asp:DropDownList ID="cbitemfree" runat="server" OnSelectedIndexChanged="cbitemfree_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="100%"></asp:DropDownList> 
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbprodgroupfree" EventName="SelectedIndexChanged" />
                            </Triggers>
                        </asp:UpdatePanel>                         
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel10" runat="server">
                    <ContentTemplate>
                    <div id="showItemFree" runat="server">
                        <div class="col-md-3">
                            <label for="branded" style="background-color:skyblue">Item</label>
                            <asp:UpdatePanel ID="UpdatePanel8" runat="server">
                                <ContentTemplate>
                                     <asp:DropDownList ID="cbitemfrees" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:AsyncPostBackTrigger ControlID="cbitemfree" EventName="SelectedIndexChanged" />
                                </Triggers>
                            </asp:UpdatePanel>                          
                        </div>
                    </div>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="rdfree" EventName="SelectedIndexChanged" />
                    </Triggers>
                    </asp:UpdatePanel>  

                    <div class="col-md-1">
                        <label for="action"> </label>
                        <asp:Button ID="btaddfree" runat="server" Text="Add" CssClass="btn btn-default" OnClick="btaddfree_Click" />
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-sm-12">
                <div class="form-group">
                    <asp:UpdatePanel ID="UpdatePanel9" runat="server">
                    <ContentTemplate>
                    <div class="table-responsive">
                        <asp:GridView ID="grdfreeitem" AllowPaging="True" OnPageIndexChanging="grdfreeitem_PageIndexChanging" OnRowDeleting="grdfreeitem_RowDeleting1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                            <Columns>
                                <asp:TemplateField HeaderText="Item Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbitemcode" runat="server" Text='<%# Eval("item_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name"><ItemTemplate><%# Eval("item_desc") %></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Size">
                                   <ItemTemplate> <%# Eval("size") %></ItemTemplate></asp:TemplateField>
                                <asp:TemplateField HeaderText="Branded">
                                    <ItemTemplate>
                                        <asp:Label ID="lbbranded" runat="server" Text='<%# Eval("branded_nm") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="UOM">
                                    <ItemTemplate><%# Eval("uom") %></ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                        <asp:GridView ID="grdfreeproduct" AllowPaging="True" OnPageIndexChanging="grdfreeitem_PageIndexChanging" OnRowDeleting="grdfreeitem_RowDeleting1" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found">  
                            <Columns>
                                <asp:TemplateField HeaderText="Prod Code">
                                    <ItemTemplate>
                                        <asp:Label ID="lbprodcode" runat="server" Text='<%# Eval("prod_cd") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Name">
                                    <ItemTemplate>
                                        <%# Eval("prod_nm") %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:CommandField ShowDeleteButton="True" />
                            </Columns>
                        </asp:GridView>
                    </div>
                    </ContentTemplate>
                     <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btaddfree" EventName="Click" />
                    </Triggers>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </div>
    </div>
    </ContentTemplate>
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="rdmethod" EventName="SelectedIndexChanged" />
    </Triggers>
    </asp:UpdatePanel> 

   <%-- </div>--%>
    <div class="h-divider"></div>
    <div class="row">
      <div class="col-sm-12">
        <div class="text-center">
            
            <button type="submit" class="btn btn-default btn-sm" runat="server" id="btnew" onserverclick="btnew_Click" >
              <span class="fa fa-plus" aria-hidden="true"></span> New
            </button>
            <button type="submit" class="btn btn-default btn-sm" runat="server" id="btsave" onserverclick="btsave_Click" >
              <span class="fa fa-save" aria-hidden="true"></span> Save
            </button>
            <div id="button" style="display: none">
                <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" />
            </div> 
        </div>
      </div>
    </div>

</asp:Content>

