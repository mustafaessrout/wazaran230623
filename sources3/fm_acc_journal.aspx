<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_acc_journal.aspx.cs" Inherits="fm_acc_journal" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
         .main-content #mCSB_2_container{
            min-height: 520px;
        }
       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Journal Fact</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Print Journal</div>
            </div>
            &nbsp
        
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Journal No.</label>
                <div class="col-md-10">
                    <asp:TextBox ID="txjournalno" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>

            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                    <asp:Button ID="btprintjournal" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprintjournal_Click" />
                </div>
            </div>
        
            <br/><div class="h-divider"></div><br/>
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Print By Period By Unit Of Work</div>
            </div>
            &nbsp

            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Post Period (yyyyMM)</label>
                <div class="col-md-10">
                    <asp:TextBox ID="txpostperiodbyperiodbyuow" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>
            <br/><br/>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-10 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                    </asp:DropDownList>  
                </div>
            </div>
            <br/><br/>
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                    <asp:Button ID="btprintbyperiodbyuow" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprintbyperiodbyuow_Click" />
                </div>
            </div>

            <br/><div class="h-divider"></div>
            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8" style="font-size:medium;font-weight:bold";>Print By Period All Unit Of Work</div>
            </div>
            &nbsp
        
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="control-label col-md-1">Post Period (yyyyMM)</label>
                <div class="col-md-10">
                    <asp:TextBox ID="txpostperiodbyperiodalluow" runat="server" CssClass="form-control input-sm"></asp:TextBox>
                </div>
            </div>

            <div class="row navi">
                <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                    <asp:Button ID="btprintbyperiodalluow" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprintbyperiodalluow_Click" />
                </div>
            </div>
        </div>
    </div>
  
</asp:Content>

