<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_contractpayment_inv.aspx.cs" Inherits="fm_contractpayment_inv" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Payment Contract Invoice</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/sweetalert.css" rel="stylesheet" />

    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
    <script src="js/sweetalert.min.js"></script>
    <script>
       function openreport(url)
       {
           window.open(url, "myrep");
       }

       function openreport2(url) {
           window.open(url, "myrep"); 
       }

       function popupwindow(url) {
           mywindow = window.open(url, "mywindow", "location=1,status=1,scrollbars=1,  width=900,height=600");
           mywindow.moveTo(400, 200);
       }
       function MsgWarning(sCap, sMsg) {
           sweetAlert(sCap, sMsg, 'warning');
       }

       function MsgSuccess(sCap, sMsg) {
           sweetAlert(sCap, sMsg, 'success');
       }
   </script>
</head>
<body >

    <div class="container-fluid margin-top">
        <div class=" bg-white">
            <div class="container-fluid" style="padding: 10px 20px;">
                <div class="divheader">Business Agreement (<asp:Label ID="lbcontract" runat="server"></asp:Label>_<asp:Label ID="lbbranch" runat="server" ></asp:Label>)</div>
                <div class="h-divider"></div>
                <form id="form1" runat="server" >
                    <asp:ToolkitScriptManager ID="ScriptManager1" runat="server"></asp:ToolkitScriptManager>
            
                    <div class="row" runat="server" id="uploadAG">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3 control-label">Document</label>
                                <div class="col-md-9">
                                    <asp:FileUpload ID="upl" runat="server" />
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" runat="server" id="rejectAG">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="col-md-3 control-label">Remarks</label>
                                <div class="col-md-9">
                                    <asp:TextBox ID="txremarkReject" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%" TextMode="MultiLine"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" runat="server" id="paymentAG">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grdschedule" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdschedule_RowDataBound" OnRowCommand="grdschedule_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Seq No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate><%# Eval("due_dt") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        <%#  String.Format("{0:n} {1}", Eval("qty"), Eval("uom")) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>   
                                                <asp:TemplateField>
                                                    <ItemTemplate>                
                                                        <asp:Button ID="btnPrint" runat="server" Text="Print Invoice" CssClass="btn btn-info" CommandName="print" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>            
                                            </Columns>
                                        </asp:GridView>      
                                        <asp:GridView ID="grdscheduleg" runat="server" CssClass="table table-striped table-bordered " AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdscheduleg_RowDataBound" OnRowCommand="grdscheduleg_RowCommand">
                                            <Columns>
                                                <asp:TemplateField HeaderText="Seq No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Date">
                                                    <ItemTemplate><%# Eval("due_dt") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Customer">
                                                    <ItemTemplate><asp:Label ID="lbcustomer" runat="server" Text='<%# Eval("customer") %>'></asp:Label></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Total">
                                                    <ItemTemplate>
                                                        <%#  String.Format("{0:n} {1}", Eval("qty"), Eval("uom")) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Paid">
                                                    <ItemTemplate>
                                                        <%#  String.Format("{0:n} {1}", Eval("iqty"), Eval("uom")) %>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Status">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>   
                                                <asp:TemplateField>
                                                    <ItemTemplate>                
                                                        <asp:Button ID="btnPrint" runat="server" Text="Print Invoice" CssClass="btn btn-info" CommandName="print" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>            
                                            </Columns>
                                        </asp:GridView>                          
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                        <div class="form-group">
                                        <label class="col-md-3 control-label">Contract No</label>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txagreecdPaid" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Sequence No</label>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txseqnoPaid" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Due Date</label>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="dtduePaid" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Paid Date</label>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="dtpay" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Manual No Inv</label>
                                        <div class="col-md-9">
                                            <asp:TextBox ID="txmanual" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                        </div>
                                    </div>
                                    <div class="form-group" runat="server" id="customer">
                                        <label class="col-md-3 control-label">Customer</label>
                                        <div class="col-md-9">
                                            <asp:DropDownList ID="cbcustomer" runat="server" CssClass="form-control input-sm" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Warehouse</label>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="cbwhs" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbwhs_SelectedIndexChanged" CssClass="form-control input-sm" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-md-3 control-label">Bin</label>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="cbbin" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbbin_SelectedIndexChanged" CssClass="form-control input-sm" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Driver</label>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="cbdriver" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbdriver_SelectedIndexChanged" CssClass="form-control input-sm" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                        <label class="col-md-3 control-label">Vehicle</label>
                                        <div class="col-md-3">
                                            <asp:DropDownList ID="cbvehicle" runat="server" CssClass="form-control input-sm" Width="100%">
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label class="col-md-3 control-label">Total Payment</label>
                                        <div class="col-md-3">
                                            <asp:TextBox ID="txamount" runat="server" CssClass="form-control input-sm" Height="100%" Width="100%"></asp:TextBox>
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="lbuomPaid" runat="server" class="control-label"></asp:Label>
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <div class="table-responsive">
                                        <asp:GridView ID="grditem" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" >
                                            <Columns>
                                                <asp:TemplateField HeaderText="Seq No">
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbitempay" runat="server" Text='<%# String.Format("{0}_{1}", Eval("item_cd"), Eval("item_nm")) %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Current Stock">
                                                    <ItemTemplate><%# Eval("stock") %></ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Input Payment">
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txamountpay" Text="0" runat="server" Width="5em"></asp:TextBox></ItemTemplate>
                                                </asp:TemplateField>       
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" runat="server" id="reprintAG">
                        <div class="col-md-12">
                            <div class="table-responsive">
                                <asp:GridView ID="grdscheduleprint" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdschedule_RowDataBound" OnRowCommand="grdschedule_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Seq No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate><%# Eval("due_dt") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <%#  String.Format("{0:n} {1}", Eval("qty"), Eval("uom")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField>
                                            <ItemTemplate>                
                                                <asp:Button ID="btnPrint" runat="server" Text="Print Invoice" CssClass="btn btn-info" CommandName="reprint" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>            
                                    </Columns>
                                </asp:GridView>
                                <asp:GridView ID="grdschedulegprint" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="true" EmptyDataText="No Data Found" OnRowDataBound="grdscheduleg_RowDataBound" OnRowCommand="grdscheduleg_RowCommand">
                                    <Columns>
                                        <asp:TemplateField HeaderText="Seq No">
                                            <ItemTemplate>
                                                <asp:Label ID="lbseqno" runat="server" Text='<%# Eval("sequenceno") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date">
                                            <ItemTemplate><%# Eval("due_dt") %></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Customer">
                                            <ItemTemplate><asp:Label ID="lbcustomer" runat="server" Text='<%# Eval("customer") %>'></asp:Label></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Total">
                                            <ItemTemplate>
                                                <%#  String.Format("{0:n} {1}", Eval("qty"), Eval("uom")) %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Status">
                                            <ItemTemplate>
                                                <asp:Label ID="lbstatus" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>   
                                        <asp:TemplateField>
                                            <ItemTemplate>                
                                                <asp:Button ID="btnPrint" runat="server" Text="Print Invoice" CssClass="btn btn-info" CommandName="reprint" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>            
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div> 

                    <div class="row" id="driverAG" runat="server">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Receive Date</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="dtDR" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                        <asp:CalendarExtender CssClass="date" ID="dtDR_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtDR">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Driver</label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="cbdriverDR" runat="server" AutoPostBack="true" OnSelectedIndexChanged="cbdriverDR_SelectedIndexChanged" CssClass="form-control input-sm" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                    <label class="col-md-3 control-label">Vehicle</label>
                                    <div class="col-md-3">
                                        <asp:DropDownList ID="cbvehicleDR" runat="server" CssClass="form-control input-sm" Width="100%">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row" id="customerAG" runat="server">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Receive Date</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="dtCR" runat="server" CssClass="form-control input-sm" Height="100%" Width="50%"></asp:TextBox>
                                        <asp:CalendarExtender ID="dtCR_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="dtCR">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-12 margin-bottom">
                            <div class="form-actions text-center">                                
                                <asp:Button ID="btpayment" runat="server" Text=" " CssClass="btn btn-primary" OnClick="btpayment_Click" />
                            </div>
                        </div>
                    </div>

                </form>
            </div>
        </div>
    </div>
</body>
</html>
