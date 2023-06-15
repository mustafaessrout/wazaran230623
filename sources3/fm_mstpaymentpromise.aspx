<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_mstpaymentpromise.aspx.cs" Inherits="fm_mstpaymentpromise" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <div class="container-fluid">
        <div class="page-header">
            <h3>Payment Promise Report</h3>
        </div>

        <div class="form-horizontal">

            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="branch" class="col-xs-2 col-form-label col-form-label-sm">Branch</label>    
                        <div class="col-xs-6">
                            <asp:DropDownList ID="cbbranch" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList> 
                        </div>
                        <div class="col-xs-4"></div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="status" class="col-xs-2 col-form-label col-form-label-sm">Status</label>    
                        <div class="col-xs-6">
                            <asp:DropDownList ID="cbstatus" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList> 
                        </div>
                        <div class="col-xs-4"></div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-sm-12">
                    <div class="form-group">
                        <label for="type" class="col-xs-2 col-form-label col-form-label-sm">Approval Type</label>    
                        <div class="col-xs-6">
                            <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList> 
                        </div>
                        <div class="col-xs-4"></div>
                    </div>
                </div>
            </div>

        </div>

        <div class="row">
          <div class="col-sm-12">
            <div class="text-center">
                <button type="submit" class="btn btn-default btn-sm" runat="server" id="btprint" onserverclick="btprint_Click" >
                  <span class="glyphicon glyphicon-print" aria-hidden="true" ></span> Print</button> 
            </div>
          </div>
        </div>

    </div>

</asp:Content>

