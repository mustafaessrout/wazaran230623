<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_crosscheckcustomer.aspx.cs" Inherits="fm_crosscheckcustomer" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function ShowProgress() {
            $('#pnlmsg').show();
        }

        function HideProgress() {
            $("#pnlmsg").hide();
            return false;
        }

        function CustomerSelected(sender, e) {
            $get('<%=hdcustomer.ClientID%>').value = e.get_value();
            $get('<%=btlookup.ClientID%>').click();
        }
        function fn_getaddress(lat, lng) {
            var _address;
            $.ajax({
                type: 'GET',
                url: 'https://maps.googleapis.com/maps/api/geocode/json?latlng=' + lat + ',' + lng + '&key=AIzaSyDbFnnD5nwqBJVeQn_aD68irHKryMWPp2w',
                success: function (data, status) {
                    _address = data.results[0].formatted_address;
                    sweetAlert('Address At Google Maps', _address, 'info');
                    $get('<%=hdaddress.ClientID%>').value = _address;
                }
            });
             return _address;
             //});
         }
    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:HiddenField ID="hdcustomer" runat="server" />
    <asp:HiddenField ID="hdaddress" runat="server" />
    <div class="alert alert-info text-bold">Crosscheck Customer address</div>
    <div class="container">
        <div class="row margin-bottom margin-top">
               <label class="col-sm-1 control-label input-sm">Salespoint</label>
            <div class="col-sm-2 drop-down">
                <asp:DropDownList ID="cbsalespoint" CssClass="form-control input-sm" onchange="ShowProgress();" runat="server" OnSelectedIndexChanged="cbsalespoint_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
            </div>
            <label class="col-sm-1 control-label input-sm">Customer</label>
            <div class="col-sm-2">
                <asp:TextBox ID="txcustomer" runat="server"></asp:TextBox>
                <asp:AutoCompleteExtender ID="txcustomer_AutoCompleteExtender" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" ShowOnlyCurrentWordInCompletionListItem="true" UseContextKey="true" ServiceMethod="GetCustomerList" OnClientItemSelected="CustomerSelected" runat="server" TargetControlID="txcustomer">
                </asp:AutoCompleteExtender>
            </div>
        </div>
        <div class="row margin-bottom margin-top">
            <div class="col-sm-12">
                <asp:GridView ID="grd" CssClass="table table-bordered table-condensed input-sm" runat="server" AutoGenerateColumns="False" AllowPaging="True" OnPageIndexChanging="grd_PageIndexChanging">
                    <Columns>
                        <asp:TemplateField HeaderText="Cust Code">
                            <ItemTemplate>
                                <asp:Label ID="lbcustcode" runat="server" Text='<%#Eval("cust_cd") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust Name">
                            <ItemTemplate><%#Eval("cust_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Latitude">
                            <ItemTemplate>
                                <asp:Label ID="lblatitude" runat="server" Text='<%#Eval("latitude") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Longitude">
                            <ItemTemplate>
                                 <asp:Label ID="lblongitude" runat="server" Text='<%#Eval("longitude") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Reset LatLng Cust">
                            <ItemTemplate>
                                <asp:LinkButton ID="btreset" OnClick="btreset_Click" CssClass="btn btn-sm btn-danger" OnClientClick="ShowProgress();" runat="server">Reset LatLng</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address In System">
                            <ItemTemplate><%#Eval("addr") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address in Google">
                            <ItemTemplate>
                                <asp:Label ID="lbaddrgoogle" runat="server" Text='<%#Eval("addr_google") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman">
                            <ItemTemplate><%#Eval("salesman_nm") %></ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Update Address">
                            <ItemTemplate>
                                <asp:LinkButton ID="btupdate" OnClientClick="ShowProgress();" OnClick="btupdate_Click" CssClass="btn btn-primary btn-sm" runat="server">Check Address Google</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
        <div class="row margin-bottom">
            <div class="col-sm-12">
                <asp:Button ID="btlookup" OnClientClick="ShowProgress();" style="display:none" runat="server" Text="Button" OnClick="btlookup_Click" />
            </div>
        </div>
    </div>
    <div class="divmsg loading-cont" id="pnlmsg" style="display: none; text-align: center; vertical-align: middle">
        <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</asp:Content>

