<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_paymentdeposit.aspx.cs" Inherits="fm_paymentdeposit" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="admin/css/bootstrap.css" rel="stylesheet" />
    <script src="admin/js/bootstrap.js"></script>
    <script>
        function GetDeposit(sID)
        {
            $get('<%=hddepcode.ClientID%>').value = sID;
            $get('<%=btdeprefresh.ClientID%>').click();
        }
        function CustSelected(sender, e) {
                $get('<%=hdcust.ClientID%>').value = e.get_value();
                $get('<%=btrefresh.ClientID%>').click();
           }
     </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h3>Payment Deposit</h3>
    <asp:HiddenField ID="hddepcode" runat="server" />
    <img src="div2.png" class="divid" />
        
        <div class="container form-horizontal">
        <div class="form-group">
            <label for="txdepno" class="control-label col-md-1">Dep No.</label>
            <div class="col-md-3">
                <asp:TextBox ID="txdepno" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                <asp:Button ID="btsearch" runat="server" CssClass="button2 search" Height="2em" OnClick="btsearch_Click" />
            </div>
            <label for="dtdep" class="control-label col-md-1">Date</label>
            <div class="col-md-3">
                <asp:TextBox ID="dtdep" runat="server" CssClass="form-control input-group-lg" Height="2em"></asp:TextBox>
                <asp:CalendarExtender ID="dtdep_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtdep">
                </asp:CalendarExtender>
            </div>
             <label for="dtdep" class="control-label col-md-1">Type</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbdeptype" runat="server" Height="2em" Width="100%" OnSelectedIndexChanged="cbdeptype_SelectedIndexChanged"></asp:DropDownList>
            </div>
           
        </div>
         <div class="form-group">
            <label for="txdepno" class="control-label col-md-1">Cust</label>
            <div class="col-md-3">
                <asp:TextBox ID="txcustomer" runat="server" CssClass="form-control input-group-lg" Height="2em" Width="100%"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" runat="server" TargetControlID="txcustomer" ServiceMethod="GetCompletionList"  EnableCaching="false" FirstRowSelected="false" CompletionInterval="10" CompletionSetCount="1" MinimumPrefixLength="1" OnClientItemSelected="CustSelected" ShowOnlyCurrentWordInCompletionListItem="true" UseContextKey="true">
                </asp:AutoCompleteExtender>
            </div>
            <label for="dtdep" class="control-label col-md-1">Salesman</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbsalesman" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
            </div>
             <label for="dtdep" class="control-label col-md-1">Type</label>
            <div class="col-md-3">
                <asp:DropDownList ID="cbpaymenttype" runat="server" Height="2em" AutoPostBack="True" OnSelectedIndexChanged="cbpaymenttype_SelectedIndexChanged" Width="100%"></asp:DropDownList>
            </div>
           
        </div>
            <div class="form-group">
                <div class="col-md-1">
                    <asp:Label ID="Label1" runat="server" Text="INFO" CssClass="control-label col-md-1"></asp:Label>
                </div>
                <div class="col-md-8" style="border:1px solid red;padding:1em 1em 1em 1em">
                    <div class="col-md-2"> 
                        <label class="control-label">City</label>    
                         <asp:Label ID="lbcity" runat="server" Text="" CssClass="form-control"></asp:Label>
                    </div>
                     <div class="col-md-2">
                     <label class="control-label">CL</label>    
                     <asp:Label ID="lbcl" runat="server" Text="0" CssClass="form-control"></asp:Label></div>
                     <div class="col-md-2">
                    <label class="control-label">Channel</label>    
                   
                        <asp:Label ID="lbotlcd" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                    <div class="col-md-2">
                    <label class="control-label">Group</label>
                       
                        <asp:Label ID="lbcusgrcd" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                     <div class="col-md-2">
                    <label class="control-label">Category</label>
                       
                        <asp:Label ID="lbcat" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                    <div class="col-md-2">
                    <label class="control-label">Remain CL</label>
                       
                        <asp:Label ID="lbremain" runat="server" Text="" CssClass="form-control"></asp:Label>
                        </div>
                </div>

           </div>
            <div class="form-group">
                <label class="control-label col-md-1">Cheque/BT Date</label>
                <div class="col-md-2">
                 <asp:TextBox ID="dtcheque" runat="server" CssClass="form-control" Height="2em"></asp:TextBox>
                    <asp:CalendarExtender ID="dtcheque_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtcheque">
                    </asp:CalendarExtender>
                </div>
                 <label class="control-label col-md-1">Due</label>
                <div class="col-md-2">
                 <asp:TextBox ID="dtdue" runat="server" CssClass="form-control" Height="2em"></asp:TextBox>
                    <asp:CalendarExtender ID="dtdue_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtdue">
                    </asp:CalendarExtender>
                </div>
                 <label class="control-label col-md-1">Ref. No</label>
                <div class="col-md-1">
                 <asp:TextBox ID="txref" runat="server" CssClass="form-control" Height="2em" Width="100%"></asp:TextBox>
                </div>
                <label class="control-label col-md-1">Bank</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbbank" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                 <label class="control-label  col-md-1">Amount</label>
                   <div class="col-md-2">
                  <asp:TextBox ID="txamt" runat="server" Height="2em" CssClass="form-control" Width="100%"></asp:TextBox>
                </div>
                 <label class="control-label  col-md-1">Remark</label>
                   <div class="col-md-6">
                  <asp:TextBox ID="txremark" runat="server" Height="2em" CssClass="form-control" Width="100%"></asp:TextBox>
                </div>
            </div>
    <img src="div2.png" class="divid" />
    <div class="navi">
        <asp:Button ID="btdeprefresh" runat="server" Text="Button" OnClick="btdeprefresh_Click" />
        <asp:Button ID="btrefresh" runat="server" Text="Button" OnClick="btrefresh_Click" CssClass="divhid" />
        <asp:Button ID="btnew" runat="server" CssClass="btn btn-default button2 add" OnClick="btnew_Click" Text="New" />
        <asp:Button ID="btsave" runat="server" Text="Save" CssClass="button2 save" OnClick="btsave_Click" />
        <asp:Button ID="btprint" runat="server" CssClass="button2 print" Text="Print" OnClick="btprint_Click" />
        <asp:Button ID="btpayment" runat="server" Text="Make It Payment Release" CssClass="button2 add" OnClick="btpayment_Click" />
        </div>
    <asp:HiddenField ID="hdcust" runat="server" />
 
    </div>
</asp:Content>

