<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_paymentpromise.aspx.cs" Inherits="fm_paymentpromise" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script>      
    <script>
        function RefreshData(dt) {
            $get('<%=hdcust.ClientID%>').value = dt;
            $get('<%=btcust.ClientID%>').click();
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:HiddenField ID="hdcust" runat="server" />

    <div class="container-fluid">
        <div class="page-header">
            <h3>Payment Promise <span style="color:red">(New)</span></h3>
        </div>

        <div class="form-horizontal">
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="customer" class="col-xs-2 col-form-label col-form-label-sm">Customer</label>  
                        <div class="col-xs-6">
                            <div class="input-group">
                                <asp:TextBox ID="txCustomer" runat="server" CssClass="form-control form-control-xs"></asp:TextBox>
                                <span class="input-group-btn">
                                    <button type="submit" class="btn btn-default btn-xs" runat="server" id="btsearchCust" onserverclick="btsearchCust_Click" >
                                      <span class="glyphicon glyphicon-search" aria-hidden="true"></span>
                                    </button>
                                </span>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            
            <asp:UpdatePanel ID="UpdatePanel11" runat="server">
            <ContentTemplate>
                <div id="tblCustomer" runat="server" style="background-color:silver">
                    <fieldset>
                        <legend><h4>Customer File</h4></legend>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                <label for="address" class="col-xs-2 col-form-label col-form-label-sm">Address :</label> 
                                <div class="col-xs-2">
                                    <asp:Label ID="lbaddress" runat="server" Font-Bold="True" ForeColor="#0033CC">-</asp:Label>
                                </div>
                                <label for="address" class="col-xs-2 col-form-label col-form-label-sm">City :</label>  
                                <div class="col-xs-2">
                                    <asp:Label ID="lbcity" runat="server" Font-Bold="True" ForeColor="#0033CC">-</asp:Label>
                                </div>
                                <label for="custtype" class="col-xs-2 col-form-label col-form-label-sm">Cust Type :</label>
                                <div class="col-xs-2">
                                    <asp:Label ID="lbcusttype" runat="server" Font-Bold="True" ForeColor="#0033CC">-</asp:Label>
                                </div>
                            </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                <label for="term" class="col-xs-2 col-form-label col-form-label-sm">Term Payment :</label> 
                                <div class="col-xs-2">
                                    <asp:Label ID="lbterm" runat="server" Font-Bold="True" ForeColor="#0033CC">-</asp:Label>
                                </div>
                                <label for="crlimit" class="col-xs-2 col-form-label col-form-label-sm">Credit Limit :</label>  
                                <div class="col-xs-2">
                                    <asp:Label ID="lbcredit" runat="server" Font-Bold="True" ForeColor="#0033CC">-</asp:Label>
                                </div>
                                <label for="crremain" class="col-xs-2 col-form-label col-form-label-sm">CL Remain :</label>
                                <div class="col-xs-2">
                                    <asp:Label ID="lbclremain" runat="server" Font-Bold="True" ForeColor="#0033CC">-</asp:Label>
                                </div>
                            </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </ContentTemplate>
            </asp:UpdatePanel>

            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <div class="col-xs-12">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                            <ContentTemplate>
                            <div class="table-responsive">
                                <asp:GridView ID="grdlist" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" ShowHeaderWhenEmpty="True" EmptyDataText="No Data Found"  >  
                                    <Columns>
                                        <asp:TemplateField HeaderText="Invoice No">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbinv" runat="server" Text='<%# Eval("inv_no") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Amount">
                                                <ItemTemplate>
                                                    <asp:Label ID="lbccnr" runat="server" Text='<%# Eval("totamt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Due Date">
                                            <ItemTemplate><%# Eval("due_dt","{0:d/M/yyyy}") %></ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            </ContentTemplate>
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="cbapprove" EventName="SelectedIndexChanged" />
                            </Triggers>
                            </asp:UpdatePanel>
                        </div>                        
                    </div>  
                </div>
            </div>
            
            <div class="row">
                <div class="col-sm-12">
                    <div class="text-center">
                        Approve By <br \ />
                        <asp:DropDownList ID="cbapprove" runat="server" OnSelectedIndexChanged="cbapprove_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control-static" Width="15%"></asp:DropDownList> 
                    </div>
                </div>
            </div>

            <br \ />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="tblPaid" runat="server" style="background-color:grey">
                    <fieldset>
                        <legend><h4>Approval By Paid</h4></legend>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="amountpaid" class="col-xs-2 col-form-label col-form-label-sm">Amount :</label> 
                                    <div class="col-xs-2">
                                        <asp:TextBox ID="txAmountPaid" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <label for="lbamountpaid" class="col-xs-1 col-form-label col-form-label-sm">SAR</label> 
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                <label for="paidType" class="col-xs-2 col-form-label col-form-label-sm">Paid By :</label> 
                                <div class="col-xs-2">
                                    <asp:DropDownList ID="cbpayement" runat="server" CssClass="form-control-static" Width="100%" ></asp:DropDownList> 
                                </div>
                                <label for="nochque" class="col-xs-2 col-form-label col-form-label-sm">No Cheque/Bank Transfer :</label>  
                                <div class="col-xs-2">
                                    <asp:TextBox ID="txNoCheque" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                </div>
                                <label for="cbbankcq" class="col-xs-1 col-form-label col-form-label-sm">Bank Code:</label>
                                <div class="col-xs-3">
                                    <asp:DropDownList ID="cbbankcq" runat="server" CssClass="form-control-static " Width="100%"></asp:DropDownList>
                                </div>
                            </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                <label for="date" class="col-xs-2 col-form-label col-form-label-sm">Due Date :</label> 
                                <div class="col-xs-2">
                                    <asp:TextBox ID="txduedate1" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtduedate1_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txduedate1">
                                    </asp:CalendarExtender>
                                </div>
                                <label for="crlimit" class="col-xs-2 col-form-label col-form-label-sm">Bank / Cheque Scan File</label>  
                                <div class="col-xs-6">
                                    <asp:UpdatePanel ID="UpdatePanel47" runat="server">
                                    <ContentTemplate>
                                    <asp:FileUpload ID="uplpaid" runat="server"></asp:FileUpload>
                                    <asp:HyperLink ID="uplpaid_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                                    <asp:Label ID="lbfilelocpaid" runat="server" Text='Scan File'></asp:Label></asp:HyperLink>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbapprove" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <div id="tblPromise" runat="server" style="background-color:grey">
                    <fieldset>
                        <legend><h4>Approval By Promise</h4></legend>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                    <label for="amountpromise" class="col-xs-2 col-form-label col-form-label-sm">Amount :</label> 
                                    <div class="col-xs-2">
                                        <asp:TextBox ID="txAmountPromise" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    </div>
                                    <label for="lbamountpromise" class="col-xs-1 col-form-label col-form-label-sm">SAR</label> 
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12">
                                <div class="form-group">
                                <label for="date" class="col-xs-2 col-form-label col-form-label-sm">Due Date :</label> 
                                <div class="col-xs-2">
                                    <asp:TextBox ID="txduedate2" runat="server" CssClass="form-control form-control-sm"></asp:TextBox>
                                    <asp:CalendarExtender ID="dtduedate2_CalendarExtender" runat="server" Format="dd/MM/yyyy" TargetControlID="txduedate2">
                                    </asp:CalendarExtender>
                                </div>
                                <label for="crlimit" class="col-xs-2 col-form-label col-form-label-sm">SOA Scan File</label>  
                                <div class="col-xs-6">
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                    <asp:FileUpload ID="uplPromise" runat="server"></asp:FileUpload>
                                    <asp:HyperLink ID="uplpromise_nm" runat="server" Visible="False" ForeColor="blue" class="example-image-link" data-lightbox="example-1">
                                    <asp:Label ID="lbfilelocpromise" runat="server" Text='Scan File'></asp:Label></asp:HyperLink>
                                    </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                            </div>
                            </div>
                        </div>
                    </fieldset>
                </div>
            </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cbapprove" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>

        </div>

        <br \ />
        <div class="row">
          <div class="col-sm-12">
            <div class="text-center">
                <button type="submit" class="btn btn-default btn-sm" runat="server" id="btnew" onserverclick="btnew_Click" >
                  <span class="glyphicon glyphicon-plus" aria-hidden="true" ></span> New</button>
                <button type="submit" class="btn btn-default btn-sm" runat="server" id="btsave" onserverclick="btsave_Click" >
                  <span class="glyphicon glyphicon-save" aria-hidden="true"></span> Save
                </button>
                <div id="button" style="display: none">
                    <asp:Button ID="btcust" runat="server" Text="Button" OnClick="btcust_click" />
                </div> 
            </div>
          </div>
        </div>

    </div>
</asp:Content>

