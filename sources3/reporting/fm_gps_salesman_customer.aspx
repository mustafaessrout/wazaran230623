<%@ Page Title="" Language="C#" MasterPageFile="~/reporting/reporting.master" AutoEventWireup="true" CodeFile="fm_gps_salesman_customer.aspx.cs" Inherits="fm_gps_salesman" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="assets/css/styles.css" rel="stylesheet" />
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAunjADn63o3HcVh6j-kVovthjYyn0eCTM"></script>
    
 <script type ="text/javascript">
     var markers = '';
     window.onload = function () {
         
         //LoadMap();
     };
     var map;
     var marker;
     function LoadMap() {
         var mapOptions = {
             center: new google.maps.LatLng(markers[0].latitude, markers[0].longitude),
             zoom: 10,
             mapTypeId: google.maps.MapTypeId.ROADMAP
         };
         map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
         //SetMarker(0);
     };
     function MarkerAll() {
         markers = GetMapData();
         LoadMap();
         //Remove previous Marker.
         //if (marker != null) {
         //    marker.setMap(null);
         //}
         for (i = 0; i < markers.length; i++) {
             SetMarker(i);
         }
     };
     function SetMarker(position) {
        

         //Set Marker on Map.
         var infoWindow = new google.maps.InfoWindow();
         //var conten

             var data = markers[position];
             var myLatlng = new google.maps.LatLng(data.latitude, data.longitude);
             if (data.office == "O") {
                 vicon = "flag.png";
             }
             else if (data.nearloc!=null) {
                 vicon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + data.cust_cd + '|ff00ff|000000'; //Red
             }
             else{
                 vicon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + data.cust_cd + '|00ff00|000000'; //Yellow
             }
             marker = new google.maps.Marker({
                 position: myLatlng,
                 map: map,
                 title: data.cust_nm
                 , icon: vicon
             });
             (function (marker, data) {
                 google.maps.event.addListener(marker, "click", function (e) {
                     content = "Cust_cd : " + data.cust_cd + " <br>Cust Name : " + data.cust_nm + "</br><br>Distance From Office: "+data.distanceKM+"</br><br> otlcd: " + data.otlcd + "</br><br> cusgrcd: " + data.cusgrcd + "</br><br> payment_term: " + data.payment_term + "</br><br> cuscate_cd: " + data.cuscate_cd + "</br><br> credit_limit: " + data.credit_limit + "</br";
                     infoWindow.setContent(content);
                     infoWindow.open(map, marker);
                 });
             })(marker, data)
         };
     function GetMapData() {
         var json = '';
         $.ajax({
             type: "POST",
             url: "fm_gps_salesman_customer.aspx/GetData",
             data: "{}",
             contentType: "application/json; charset=utf-8",
             dataType: "json",
             async: false,
             success: function (resp) {
                 json = resp.d;
             },
             error: function () { debugger; }
         });
         return json;
     };
     function CustSelected(sender, e) {
         $get('<%=hdcust.ClientID%>').value = e.get_value();
         $get('<%=btsearch.ClientID%>').click();
     }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <asp:HiddenField ID="hdcust" runat="server" />
            <div class="form-group">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbsp" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsp_SelectedIndexChanged"></asp:DropDownList>
                </div>
                 
                 <label class="control-label col-md-1">Salesman</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsalesman" CssClass="form-control" runat="server" OnSelectedIndexChanged="cbsalesman_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
                <label class="control-label col-md-1">Dist Screening</label>
                <div class="col-md-1">
                <asp:DropDownList ID="ddscreening" runat="server">
                    <asp:ListItem>10</asp:ListItem>
                    <asp:ListItem>25</asp:ListItem>
                    <asp:ListItem>50</asp:ListItem>
                    <asp:ListItem>75</asp:ListItem>
                    <asp:ListItem>100</asp:ListItem>
                    <asp:ListItem>150</asp:ListItem>
                    <asp:ListItem>200</asp:ListItem>
                </asp:DropDownList>
                </div>
                <label class="control-label col-md-1">Customer</label>
                <div class="col-md-2">
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
                            <asp:TextBox ID="txcustomer" runat="server"></asp:TextBox>
                            <asp:AutoCompleteExtender CompletionListCssClass="auto-complate-list" CompletionListHighlightedItemCssClass ="auto-complate-hover" CompletionListItemCssClass="auto-complate-item" ID="txcustomer_AutoCompleteExtender" runat="server" ServiceMethod="GetCompletionList" TargetControlID="txcustomer" UseContextKey="True" CompletionInterval="10" CompletionSetCount="1" EnableCaching="false" FirstRowSelected="false" MinimumPrefixLength="1" CompletionListElementID="divwidths" OnClientItemSelected="CustSelected">
                            </asp:AutoCompleteExtender>
                            <div class="input-group-btn">
                            <asp:LinkButton ID="btsearch" style="display:none" runat="server" CssClass="btn btn-primary btn-search" OnClick="btsearch_Click"><span class="fa fa-search"></span></asp:LinkButton>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btview" CssClass="btn btn-primary"  runat="server" OnClick="btview_Click">VIEW</asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" Width="30%" >
                                <Columns>
                                    <asp:TemplateField HeaderText="Cust CD">
                                        <ItemTemplate>
                                        <asp:Label ID="lbcust_cd" runat="server" Text='<%# Eval("cust_cd") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Customer Name">
                                        <ItemTemplate>
                                        <asp:Label ID="lbcust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                </Columns>

                            </asp:GridView>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
            <div id="dvMap" style="width: 100%; height: 500px"></div>
    
</asp:Content>

