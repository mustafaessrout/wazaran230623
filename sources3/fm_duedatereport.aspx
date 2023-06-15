<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_duedatereport.aspx.cs" Inherits="fm_duedatereport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
  
    </style>
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }


    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="divheader">Collecction Due Date Report</div>
    <div class="h-divider"></div>

    <div class="container-fluid">
        <div class="row">
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="col-sm-2 control-label">Report Type</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbtype" runat="server" CssClass="form-control">
                        <asp:ListItem Value="0">Collection Duedate by Salesman</asp:ListItem>
                        <asp:ListItem Value="1">Collection Duedate by Customer </asp:ListItem>
                    </asp:DropDownList>

                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="col-sm-2 control-label">Salespoint</label>
                <div class="col-sm-10 drop-down">
                    <asp:DropDownList ID="cbsalespoint" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                </div>
            </div>
            <div class="clearfix margin-bottom col-md-offset-2 col-md-8">
                <label class="col-sm-2 control-label">Payment Term</label>
                <div class="col-sm-9 drop-down">
                    <asp:DropDownList ID="cbtop" runat="server" CssClass="form-control">
                    </asp:DropDownList>

                </div>
                <div class="col-sm-1 checkbox">
                    <asp:CheckBox ID="chall" runat="server" AutoPostBack="True" OnCheckedChanged="chall_CheckedChanged" />
                </div>
            </div>
        </div>
        <div class="row navi ">
            <div class="margin-bottom col-md-offset-2 col-md-8">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button ID="btprint" OnClientClick="ShowProgress();" runat="server" Text="Print" CssClass="btn-info btn btn-print" OnClick="btprint_Click" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

