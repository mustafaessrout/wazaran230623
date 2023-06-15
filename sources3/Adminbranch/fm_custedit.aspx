<%@ Page Title="" Language="C#" MasterPageFile="~/adminbranch/admbranch.master" AutoEventWireup="true" CodeFile="fm_custedit.aspx.cs" Inherits="admin_fm_custedit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <style type="text/css">
        .h-divider {
            margin-top: 5px;
            margin-bottom: 5px;
            height: 1px;
            width: 100%;
            border-top: 1px solid gray;
        }
    </style>
    <script>
        function CustSelected(sender, e) {
            $get('<%=hdcust.ClientID%>').value = e.get_value();
            $get('<%=btsearch.ClientID%>').click();
        }

        function fn_getaddress(lat, lng) {
            //$(document).ready(function () {
            $.ajax({
                type: 'GET',
                url: 'https://maps.googleapis.com/maps/api/geocode/json?latlng=' + lat + ',' + lng + '&key=AIzaSyDbFnnD5nwqBJVeQn_aD68irHKryMWPp2w',
                success: function (data, status) {
                    var sAddr = data.results[0].formatted_address;
                    $("#<%=lbaddressgmap.ClientID%>").text(sAddr);
                    }
                });
            //});
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
    <asp:HiddenField ID="hdlat" runat="server" />
    <asp:HiddenField ID="hdlng" runat="server" />
    <h3>Customer Edit</h3>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="control-label col-md-1">Customer:</label>
            <div class="col-md-4">
                <asp:TextBox ID="txcust" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcust_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcust" UseContextKey="True" MinimumPrefixLength="1" EnableCaching="false" FirstRowSelected="false" OnClientItemSelected="CustSelected">
                </asp:AutoCompleteExtender>
            </div>
            <div class="col-md-1">Address (GMAP)</div>
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Label ID="lbaddressgmap" CssClass="control-label" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Latitude</label>
            <div class="col-md-2">
                <asp:TextBox ID="txlatitude" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <label class="control-label col-md-1">Longitude</label>
            <div class="col-md-2">
                <asp:TextBox ID="txlongitude" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="col-md-6">
                <strong style="color: red">Latitude and Longitude can be changed by Control Room Only</strong>
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
            <label class="control-label col-md-1">Arabic Name:</label>
            <div class="col-md-2">
                <asp:TextBox ID="txcustarabic" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
            </div>
            <label class="control-label col-md-1">Address:</label>
            <div class="col-md-2">
                <asp:TextBox ID="txadderss" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">TAX No:</label>
            <div class="col-md-2">
                <asp:TextBox ID="txvat" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
            </div>
            <label class="control-label col-md-1">Vat AR Nm:</label>
            <div class="col-md-2">
                <asp:TextBox ID="txcustarvat" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
            </div>
            <label class="control-label col-md-1">Vat EN Nm:</label>
            <div class="col-md-2">
                <asp:TextBox ID="txcustenvat" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
            </div>
            <label class="control-label col-md-1">City:</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cblocation" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cblocation_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">District:</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbdistrict" runat="server" CssClass="form-control">
                </asp:DropDownList>
            </div>
            <label class="control-label col-md-1">Cust SN Name:</label>
            <div class="col-md-2">
                <asp:TextBox ID="txsnname" runat="server" CssClass="form-control-static" Width="100%"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="h-divider"></div>
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Credit:</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbcustcate" runat="server" CssClass="form-control" Width="100%" AutoPostBack="true" OnSelectedIndexChanged="cbcustcate_SelectedIndexChanged"></asp:DropDownList>
            </div>
            <label class="control-label col-md-1">CL:</label>
            <div class="col-md-2">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="txcl" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
                        Min: <asp:Label ID="lblmincredit" runat="server"></asp:Label>
                        Max: <asp:Label ID="lblmaxcredit" runat="server"></asp:Label>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <label class="control-label col-md-1">TOP:</label>
            <div class="col-md-2">
                <asp:TextBox ID="txtop" runat="server" CssClass="form-control" Width="100%"></asp:TextBox>
            </div>
            <label class="control-label col-md-1">:Group</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbcusgrcd" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
            </div> 
        </div>
        <div class="form-group">
            <label class="control-label col-md-1">Channel</label>
            <div class="col-md-2">
                <asp:DropDownList ID="cbchannel" runat="server" CssClass="form-control" Width="100%"></asp:DropDownList>
            </div>
            <%--<label class="control-label col-md-1">Tax No</label>
			<div class="col-md-2">
				<asp:TextBox ID="txtaxno" runat="server" CssClass="form-control"></asp:TextBox>
			</div>--%>
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
    <%--</div>--%>
    <div class="form-group">
        <div style="text-align: center">
            <asp:Button ID="btsearch" runat="server" Text="Button" OnClick="btsearch_Click" CssClass="divhid" Style="display: none" />
            <asp:LinkButton ID="btsave" OnClick="btsave_Click" runat="server" CssClass="btn btn-primary">Save</asp:LinkButton>

        </div>
    </div>
</asp:Content>

