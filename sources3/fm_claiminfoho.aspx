<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_claiminfoho.aspx.cs" Inherits="fm_claiminfoho" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
 
</head>
<body>
    <div class="container-fluid">
        <form id="form1" runat="server" class="form-horizontal">
        <div>
            <div class="row">
                <div class="col-sm-12">
                    <ol class="breadcrumb">
                        <li><h3>Claim HO Info (<asp:Label ID="lbclaim" runat="server"></asp:Label>_<asp:Label ID="lbbranch" runat="server" ></asp:Label>)</h3></li>
                    </ol>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>Created Date</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbdate" runat="server"></asp:Label></strong></h6></div>
                <div class="col-sm-2"><h6><strong>Status</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbstatus" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>Proposal No</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbpropno" runat="server"></asp:Label></strong></h6></div>
                <div class="col-sm-2"><h6><strong>CCNR No</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbccnr" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>Remark</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-9"><h6><strong><asp:Label ID="lbremark" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>Total Order (<asp:Label ID="lborder" runat="server"></asp:Label>)</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbtotalorder" runat="server"></asp:Label></strong></h6></div>
                <div class="col-sm-2"><h6><strong>Total Free (<asp:Label ID="lbfree" runat="server"></asp:Label>)</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbtotalfree" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h6><strong>Total Amount (<asp:Label ID="lbamount" runat="server"></asp:Label>)</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3">
                    <div class="form-group">
                        <div class="col-sm-6">
                            <asp:Label ID="lbtotalamount" runat="server"></asp:Label>
                            <asp:TextBox ID="txtotalamount" runat="server" Width="100%"></asp:TextBox>
                        </div>
                        <div class="col-sm-6">
                            <asp:Button ID="btadjustment" runat="server" Text="Edit" OnClick="btadjustment_Click" CssClass="btn btn-default btn-sm"/>
                        </div>
                    </div>
                    <%--<h6><strong><asp:Label ID="lbtotalamount" runat="server"></asp:Label></strong></h6>--%>
                </div>
                <div class="col-sm-2"><h6><strong>Cost SBTC / Principal</strong></h6></div>
                <div class="col-sm-1"><h6>:</h6></div>
                <div class="col-sm-3"><h6><strong><asp:Label ID="lbcost" runat="server"></asp:Label></strong></h6></div>
            </div>
            <div class="row">
                <div class="col-sm-2"><h5><strong>Claim Details</strong></h5></div>
                <div class="col-sm-1"><h6></h6></div>
                <div class="col-sm-9"></div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>--%>
                    <asp:GridView ID="grddetail" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="false" EmptyDataText="NO DATA" OnRowEditing="grddetail_RowEditing" OnRowUpdating="grddetail_RowUpdating" CssClass="table table-striped">
                        <Columns>
                            <asp:TemplateField HeaderText="Invoice No">
                                <ItemTemplate><asp:Label ID="lbinvoiceno" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Invoice Date">
                                <ItemTemplate><%# Eval("inv_dt", "{0:dd/MM/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Type">
                                <ItemTemplate><%# Eval("disc_typ") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Product">
                                <ItemTemplate><%# String.Format("{0}_{1}", Eval("item_cd"), Eval("item_nm")) %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Order Qty">
                                <ItemTemplate><%# Eval("qtyorder") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txqtyorder" Text='<%# Eval("qtyorder") %>' runat="server" CssClass="form-control input-sm" Width="100%" Height="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Free Qty">
                                <ItemTemplate><%# Eval("qtyfree") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txqtyfree" Text='<%# Eval("qtyfree") %>' runat="server" CssClass="form-control input-sm" Width="100%" Height="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Unitprice">
                                <ItemTemplate><%# Eval("unitprice") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txunitprice" Text='<%# Eval("unitprice") %>' runat="server" CssClass="form-control input-sm" Width="100%" Height="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Discount">
                                <ItemTemplate><%# Eval("disc_amt") %></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txdisc_amt" Text='<%# Eval("disc_amt") %>' runat="server" CssClass="form-control input-sm" Width="100%" Height="100%"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:CommandField ShowEditButton="True" />
                        </Columns>
                    </asp:GridView>
                    <asp:GridView ID="grdcash" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="false" EmptyDataText="NO DATA" CssClass="table table-striped">
                        <Columns>
                            <asp:TemplateField HeaderText="Cashout No">
                                <ItemTemplate><%# Eval("claimco_cd") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Schedule Date">
                                <ItemTemplate><%# Eval("schedule_dt", "{0:dd/MM/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="Paid Date">
                                <ItemTemplate><%# Eval("paid_dt", "{0:dd/MM/yyyy}") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Amount">
                                <ItemTemplate><%# Eval("amt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Received By ">
                                <ItemTemplate><%# String.Format("{0}_{1}", Eval("emp_cd"), Eval("emp_nm")) %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                    <%--</ContentTemplate>
                    </asp:UpdatePanel>--%>
                </div>
            </div> 
            <div class="row">
                <div class="col-sm-2"><h5><strong>Documents Support</strong></h5></div>
                <div class="col-sm-1"><h6></h6></div>
                <div class="col-sm-9"></div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="grddocument" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="False" EmptyDataText="NO DATA" CssClass="table table-striped" OnRowDataBound="grddocument_RowDataBound">
                        <Columns>
                            <asp:TemplateField HeaderText="Claim No">
                                <ItemTemplate>
                                    <asp:HiddenField ID="hddoc_cd" Value='<%#Eval("doc_cd") %>' runat="server" />
                                    <asp:Label ID="lbclaimno" runat="server" Text='<%# Eval("claim_no") %>'></asp:Label></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Document">
                                <ItemTemplate><%# String.Format("{0}_{1}", Eval("doc_cd"), Eval("doc_nm")) %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="File Location">
                                <ItemTemplate>
                                    <a class="example-image-link" href="/images/claim_doc/<%# Eval("fileloc") %>" download data-lightbox="example-1<%# Eval("fileloc") %>">
                                    <asp:Label ID="lbfileloc" runat="server" Text='Download'></asp:Label>
                                    </a>
                                    <a class="example-image-link" href="/images/proposal_doc/<%# Eval("fileloc") %>" download data-lightbox="example-1<%# Eval("fileloc") %>">
                                    <asp:Label ID="lbfilelocproposal" runat="server" Text='Download'></asp:Label>
                                    </a>
                                </ItemTemplate>
                                <%--<HeaderStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />--%>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

            <%--<div class="row">
                <div class="col-sm-4"><h5><strong>Sales Department Approval</strong></h5></div>                
                <div class="col-sm-8"></div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <asp:GridView ID="grdsales" runat="server" AutoGenerateColumns="False" CellPadding="0" GridLines="None" AllowPaging="True" EmptyDataText="NO DATA" CssClass="table table-striped">
                        <Columns>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate><%# Eval("claim_dt") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remark">
                                <ItemTemplate><%# Eval("remark") %></ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Rcv / Snd">
                                <ItemTemplate><%# Eval("status_code") %></ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
            <div class="row">
              <div class="col-sm-12">
                <div class="text-center">
                    <button type="submit" class="btn btn-default btn-sm" runat="server" id="btapprove" onserverclick="btapprove_Click" >
                      <span class="glyphicon glyphicon-ok" aria-hidden="true"></span> Approve
                    </button>
                    <button type="submit" class="btn btn-default btn-sm" runat="server" id="btreject" onserverclick="btreject_Click" >
                      <span class="glyphicon glyphicon-remove" aria-hidden="true"></span> Dis-Approve
                    </button>
                </div>
              </div>
            </div>--%>
        </div>

        <%--<div class="row">
          <div class="col-sm-12">
            <div class="text-center">
                <button type="submit" class="btn btn-info btn-sm" runat="server" id="print" data-toggle="modal" onserverclick="print_Click" >
                  <span class="glyphicon glyphicon-briefcase" aria-hidden="true"></span> Print 
                </button>
                <%--<button type="submit" class="btn btn-info btn-sm" runat="server" id="salesRcv" data-toggle="modal" >
                  <span class="glyphicon glyphicon-briefcase" aria-hidden="true"></span> Sales Rcv/Snd
                </button>
                <%--<button type="submit" class="btn btn-info btn-sm" runat="server" id="vendorApp" onserverclick="btprint_Click" >
                  <span class="glyphicon glyphicon-ok" aria-hidden="true"></span> Vendor Approval
                </button>
                <button type="submit" class="btn btn-info btn-sm" runat="server" id="Button1" onserverclick="btprint_Click" >
                  <span class="glyphicon glyphicon-book" aria-hidden="true"></span> Claim To Vendor
                </button>
            </div>
          </div>
        </div>--%>

        </form>
    </div>
</body>
</html>
