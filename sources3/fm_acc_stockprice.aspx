<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acc_stockprice.aspx.cs" Inherits="fm_acc_stockprice" Debug="true"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .main-content #mCSB_2_container{
            min-height: 520px;
        }
    </style>
    <script src="js/jquery.min.js"></script>
    <script type="text/javascript">

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Inventory Stock Price List</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Print Inventory Stock Price List</div>
            </div>
            &nbsp
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-10 drop-down">
                   <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control" Enabled="false" >
                   </asp:DropDownList>  
                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Price Date</label>
                <div class="col-sm-10 drop-down-date">
                    <asp:TextBox ID="dt" runat="server" CssClass="form-control"></asp:TextBox>
                    <ajaxToolkit:CalendarExtender ID="dt_CalendarExtender" CssClass="date" runat="server" BehaviorID="dt_CalendarExtender" Format="d/M/yyyy" TargetControlID="dt">
                    </ajaxToolkit:CalendarExtender>
                </div>
            </div>
        </div>

    </div> 

    <div class="row navi">
        <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
            <asp:Button ID="printreport" runat="server" Text="Print" CssClass="btn-info btn btn-print" 
                OnClick="btPrintstockpricelist_Click" />
            <br/>
        </div>
        <br />
    </div>
    
    <br />

    <div id="output">
        <%--tes--%>
    </div>

</asp:Content>
