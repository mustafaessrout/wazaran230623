<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_bankdepositentry.aspx.cs" Inherits="fm_bankdepositentry" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">
        Bank Deposit Entry
    </div>
    <div class="h-divider"></div>
    <div class="container">
        <div class="row">
            <div class="col-md-6 margin-bottom" style="margin-bottom: 10px;">
                <label class="control-label col-md-3 titik-dua">Cheque/ Transfer/ Cash Deposit No. </label>
                <div class="col-md-9">
                    <asp:TextBox ID="txdocno" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6 margin-bottom">
                <label class="control-label col-md-3 titik-dua">Date </label>
                <div class="col-md-9 drop-down-date">
                    <asp:TextBox ID="dtdeposit" runat="server" CssClass="form-control makeitreadonly"></asp:TextBox>
                    <asp:CalendarExtender ID="dtdeposit_CalendarExtender" CssClass="date" runat="server" Format="d/M/yyyy" TargetControlID="dtdeposit">
                    </asp:CalendarExtender>
                     
                </div>
            </div>
            <div class="col-md-6 margin-bottom">
                <label class="control-label col-md-3 titik-dua"> Bank Account No.</label>
                <div class="col-md-9 drop-down">
                    <asp:DropDownList ID="cbbankaccount" runat="server" CssClass="form-control"></asp:DropDownList>
                      
                </div>
            </div>
            <div class="col-md-6 margin-bottom">
                <label class="control-label col-md-3 titik-dua"> Amount</label>
                <div class="col-md-9">
                    <asp:TextBox ID="txamount" runat="server"  CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <div class="col-md-6 margin-bottom">
                <label class="control-label col-md-3 titik-dua"> Deposit Type </label>
                <div class="col-md-9 drop-down">
                    <asp:DropDownList ID="cbdeposittype" runat="server" CssClass="form-control"></asp:DropDownList>
                     
                </div>
            </div>
            <div class="col-md-12 margin-bottom">
                <label class="control-label">Remark</label>
                <div class="" >
                    <asp:TextBox ID="txremark" runat="server" CssClass="form-control" ></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
    
    <div class="navi margin-bottom ">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Button ID="btnew" runat="server" CssClass="btn btn-new btn-success" OnClick="btnew_Click" Text="New" />
                <asp:Button ID="btsave" runat="server" Text="Save" CssClass="btn btn-warning btn-save" OnClick="btsave_Click" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

