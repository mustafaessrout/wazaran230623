<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_adjustmentprice3.aspx.cs" Inherits="fm_adjustmentprice3" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="divheader">Adjustment Price</div>
    <div class="container-fluid">
        <div class="row">
            <div class="col-lg-3">
                <label>Select Metd</label>
                <asp:RadioButtonList ID="opcust" runat="server" AutoPostBack="True" CellPadding="0" CellSpacing="0" RepeatDirection="Horizontal" BorderWidth="1px" OnSelectedIndexChanged="opcust_SelectedIndexChanged" BackColor="#CCCCCC" Height="28px" Width="20em">
                    <asp:ListItem Selected="True" Value="C" Text="Cust"></asp:ListItem>
                    <asp:ListItem Value="G" Text="Group"></asp:ListItem>
                </asp:RadioButtonList>
            </div>
            <div class="col-md-2">
                <label>Salespoint</label><asp:DropDownList ID="cbsalespoint" runat="server"></asp:DropDownList>
            </div>
            <div class="col-md-4">
                <label>Customer/Group </label><asp:TextBox ID="txcustomer" runat="server" CssClass="form-control" Height="2em"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcustomer" UseContextKey="True" EnableCaching="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1">
                </asp:AutoCompleteExtender>
                <asp:DropDownList ID="cbcusgrcd" runat="server" CssClass="form-control" Height="2em"></asp:DropDownList>
            </div>
            </div>
        </div>
        <br />
        <div class="row">
            <div class="col-md-4">
                <label>Item</label><asp:TextBox ID="txitemsearch" runat="server" CssClass="form-control" Height="2em"></asp:TextBox>
            </div>
            <div class="col-md-3">
                <label>Adjust Value (+/-)</label><asp:TextBox ID="txadjust" runat="server" CssClass="form-control" Height="2em"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>Start Date</label><asp:TextBox ID="dtstart" runat="server" CssClass="form-control" Height="2em"></asp:TextBox>
            </div>
            <div class="col-md-2">
                <label>End Date</label><asp:TextBox ID="dtend" runat="server" CssClass="form-control" Height="2em"></asp:TextBox>
               
            </div>
              <div class="col-md-1">
                <label></label><asp:Button ID="btadd" runat="server" Text="Add" CssClass="btn-block" Height="3em"/>
            </div>
        </div>
        
   
    
    <img src="div2.png" class="divid" />
    <div class="row">
        <div class="col-lg-12">
            <asp:GridView ID="grd" runat="server" CellPadding="0" CssClass="mygrid"></asp:GridView>
        </div>
    </div>
         </div>
</asp:Content>

