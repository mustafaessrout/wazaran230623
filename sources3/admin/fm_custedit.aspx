<%@ Page Title="" Language="C#" MasterPageFile="~/admin/adm.master" AutoEventWireup="true" CodeFile="fm_custedit.aspx.cs" Inherits="admin_fm_custedit" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../css/anekabutton.css" rel="stylesheet" />
    <style type="text/css">
        .h-divider{
         margin-top:5px;
         margin-bottom:5px;
         height:1px;
         width:100%;
         border-top:1px solid gray;
        }
</style>
    <script>
        function CustSelected(sender, e)
        {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentbody" Runat="Server">
    <div class="container">
        <h3>Customer Edit</h3>
        <div class="form-horizontal">
        <div class="form-group">
           
             <label class="control-label col-md-1">Customer:</label>
             <div class="col-md-4">
                <asp:TextBox ID="txcust" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcust" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="CustSelected">
                </asp:AutoCompleteExtender>
                <asp:HiddenField ID="hdcust" runat="server" />
            </div>
        </div>
        <div class="form-group">
            <div class="h-divider"></div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Code:</label>
            <div class="col-md-2">
                <asp:Label ID="lbcustcode" runat="server" CssClass="form-control" Width="100%"></asp:Label>
            </div>
             <label class="control-label col-md-1">Name:</label>
            <div class="col-md-2">
                <asp:TextBox ID="txcustname" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>                
            </div>
             <label class="control-label col-md-1">Address:</label>
            <div class="col-md-2">
                <asp:Label ID="lbadderss" runat="server" CssClass="form-control" Width="100%"></asp:Label>
            </div>
             <label class="control-label col-md-1">City:</label>
            <div class="col-md-2">
                <asp:Label ID="lbcity" runat="server" CssClass="form-control" Width="100%"></asp:Label>
            </div>
        </div>
            <div class="form-group">
                <div class="h-divider"></div>
            </div>
             <div class="form-group">
            <label class="control-label col-md-1">Credit:</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbcustcate" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
            </div>
             <label class="control-label col-md-1">CL:</label>
            <div class="col-md-2">
                <asp:TextBox ID="txcl" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
            </div>
             <label class="control-label col-md-1">TOP:</label>
            <div class="col-md-2">
                <asp:TextBox ID="txtop" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
            </div>
             <label class="control-label col-md-1">:Group</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbcusgrcd" runat="server" CssClass="form-control-static" Width="100%"></asp:DropDownList>
            </div>
        </div>
            <div class="form-group">
                <label class="control-label col-md-1">Chnel</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbchannel" runat="server" CssClass="form-control-static input-sm" Width="100%"></asp:DropDownList>
                </div>
            </div>
            <div class="form-group">
                <div class="h-divider"></div>
            </div>
            <div class="form-group">
                <label class="control-label col-md-1">Reason</label>
                <div class="col-md-11">
                    <asp:TextBox ID="txreason" runat="server" Width="100%" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
       </div>
    </div>
    <div class="navi">
        <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" CssClass="divhid" />
        <asp:Button ID="btsave" runat="server" Text="Update" CssClass="btn btn-default" OnClick="btsave_Click" />
    </div>
</asp:Content>

