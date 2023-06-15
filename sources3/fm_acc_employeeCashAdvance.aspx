<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acc_employeeCashAdvance.aspx.cs" Inherits="fm_acc_employeeCashAdvance" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .main-content #mCSB_2_container{
            min-height: 520px;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Employee Cash Advance</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">

        
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Print Employee Cash Advance</div>
            </div>
            &nbsp

            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-10 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                    </asp:DropDownList>  
                </div>
            </div>
            <br/><br/>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Employee</label>
                <div class="col-md-10">
                    <asp:DropDownList ID="ddlEmp" runat="server" CssClass="form-control input-sm">
                    </asp:DropDownList>
                </div>
            </div>
            <br/><br/>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Report Type</label>
                <div class="col-md-10">
                    <asp:DropDownList ID="ddlRptTyp" runat="server" CssClass="form-control input-sm">
                        <asp:ListItem Value="0">Select</asp:ListItem>
                        <asp:ListItem Value="tran">Transaction</asp:ListItem>
                        <asp:ListItem Value="summ">Summary</asp:ListItem>
                    </asp:DropDownList>
                </div>
            </div>
            <br/><br/>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Transaction Period (yyyyMM)</label>
                <div class="col-md-10">
                    <asp:TextBox ID="txpostperiodbyperiodbyuow" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <br/><br/>
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                    <asp:Button ID="btprintbyperiodbyuow" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
                </div>
            </div>
        </div>
    </div>
    <div class="h-divider"></div>

  
</asp:Content>

