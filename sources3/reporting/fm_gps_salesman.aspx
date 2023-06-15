<%@ Page Title="" Language="C#" MasterPageFile="~/reporting/reporting.master" AutoEventWireup="true" CodeFile="fm_gps_salesman.aspx.cs" Inherits="fm_gps_salesman" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <link href="assets/css/styles.css" rel="stylesheet" />
    <script src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAunjADn63o3HcVh6j-kVovthjYyn0eCTM"></script>
     <script>
         function ShowProgress() {
             $('#pnlmsg').show();
         }

         function HideProgress() {
             $("#pnlmsg").hide();
             return false;
         }
         $(document).ready(function () {
             $('#pnlmsg').hide();
         });

    </script>
  
 <script type ="text/javascript">
     function initMap() {
         
         var service = new google.maps.DirectionsService;
             var mapOptions = {
                 center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
                 zoom: 13,
                 mapTypeId: google.maps.MapTypeId.ROADMAP
             };
             var infoWindow = new google.maps.InfoWindow();
             var latlngbounds = new google.maps.LatLngBounds();
             var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);

             var stations = markers;
         // Zoom and center map automatically by stations (each station will be in visible map area)
         var lngs = stations.map(function (station) { return station.lng; });
         var lats = stations.map(function (station) { return station.lat; });
         map.fitBounds({
             west: Math.min.apply(null, lngs),
             east: Math.max.apply(null, lngs),
             north: Math.min.apply(null, lats),
             south: Math.max.apply(null, lats),
         });

         // Show stations on the map as markers
         var x=0, z=0;
         for (var i = 0; i < stations.length; i++) {
             var data = markers[i]
             var vicon;
             if (data.trx_typ == "R") { x = x + 1 } else {
                 var markerletter = "A".charCodeAt(0);
                 markerletter += z;
                 markerletter = String.fromCharCode(markerletter);
                 z = z + 1
             }
             var nn;
             if (data.maptitle == "") { nn = markerletter }else{data.maptitle}

             if (data.trx_typ == "SRPS") {
                 vicon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + data.maptitle + '|00ff00|000000'; //green
             }
             else if (data.trx_typ == "SNRPS") {
                 vicon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + data.maptitle + '|ffff00|000000'; //Yellow
             }
             else if (data.trx_typ == "NSRPS") {
                 vicon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + data.maptitle + '|0000ff|000000'; //BLUE
             }
             else if (data.trx_typ == "NSNRPS") {
                 vicon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + data.maptitle + '|ff0000|000000'; //RED
             }
             else if (data.trx_typ == "VRPS") {
                 vicon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + data.maptitle + '|ffa500|000000'; //Orange
             }
             else if (data.trx_typ == "NVRPS") {
                 vicon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + data.maptitle + '|000000|fffaf0'; //black
             }
            
             
             var marker = new google.maps.Marker({ 
                 position: stations[i],
                 map: map,
                 title: stations[i].maptitle,
                 icon: vicon                 
             });
             (function (marker, data) {
                 google.maps.event.addListener(marker, "click", function (e) {
                     infoWindow.setContent("<div style = 'width:300px;min-height:100px'>" + data.trandescription + "</div>");
                     infoWindow.open(map, marker);
                 });
             })(marker, data);
             latlngbounds.extend(marker.position);
         }

         var stations2=new Array();
         for (var i = 0; i < markers.length; i++) {
             if (markers[i].trx_typ == "VRPS" || markers[i].trx_typ == "SRPS" || markers[i].trx_typ == "NSRPS") {
                 stations2.push(markers[i]);
             }
         }
         
         // Divide route to several parts because max stations limit is 25 (23 waypoints + 1 origin + 1 destination)
         for (var i = 0, parts = [], max = 25 - 1; i < stations2.length  ; i = i + max)
             
                 parts.push(stations2.slice(i, i + max + 1));
             
         // Service callback to process service results
         var service_callback = function (response, status) {
             if (status != 'OK') {
                 console.log('Directions request failed due to ' + status);
                 return;
             }
             var renderer = new google.maps.DirectionsRenderer;
             renderer.setMap(map);
             renderer.setOptions({ suppressMarkers: true, preserveViewport: true });
             renderer.setDirections(response);
         };

         // Send requests to service to get route (for stations count <= 25 only one request will be sent)
         for (var i = 0; i < parts.length; i++) {
             // Waypoints does not include first station (origin) and last station (destination)
             var waypoints = [];
             for (var j = 1; j < parts[i].length - 1; j++)
                 waypoints.push({ location: parts[i][j], stopover: false });
             // Service options
             var service_options = {
                 origin: parts[i][0],
                 destination: parts[i][parts[i].length - 1],
                 waypoints: waypoints,
                 travelMode: 'WALKING'
             };
             // Send request
             service.route(service_options, service_callback);
         }
     }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <%--  <div class="container">--%>
        <div class="form-horizontal" style="font-family:Calibri;font-size:small">
            <div class="form-group">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbsp" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsp_SelectedIndexChanged"></asp:DropDownList>
                </div>
                 
                 <label class="control-label col-md-1">Salesman</label>
                <div class="col-md-3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="cbsalesman" CssClass="form-control" runat="server"></asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    
                </div>
                 <label class="control-label col-md-1">Date</label>
                <div class="col-md-2">
                    <asp:TextBox ID="dtgps" runat="server" CssClass="form-control" Width="100%" Height="3em"></asp:TextBox>
                    <asp:CalendarExtender ID="dtgps_CalendarExtender" runat="server" Format="d/M/yyyy" TargetControlID="dtgps">
                    </asp:CalendarExtender>
                </div>
                <div class="col-md-1">
                    <asp:LinkButton ID="btview" CssClass="btn btn-primary" OnClientClick="javascript:ShowProgress();" runat="server" OnClick="btview_Click">VIEW</asp:LinkButton>
                </div>
            </div>
            <div class="form-group">
                <div class="col-md-12">
                   <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>--%>
                              <div id="dvMap" style="width:100%;height:300px">  </div>
               <%--         </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    
                  
                </div>
                    <div id="directions_panel"></div>
            </div>
        </div>
    <%--</div>--%>
    <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
    <div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdgps" runat="server" AutoGenerateColumns="False" Caption="Summary Route Salesman" CellPadding="0" CssClass="table table-striped mygrid" GridLines="None" ShowHeaderWhenEmpty="True" OnRowDataBound="grdgps_RowDataBound" ShowFooter="True">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="Rps No">
                            <ItemTemplate>
                                <asp:Label ID="lbmaptitle" runat="server" Text='<%# Eval("maptitle") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Cust CD">
                            <ItemTemplate>
                                <%# Eval("cust_cd") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer Name">
                            <ItemTemplate>
                                <asp:Label ID="lbcust_nm" runat="server" Text='<%# Eval("cust_nm") %>'></asp:Label>
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label Text=" T O T A L " runat="server" Font-Bold="true" font-size="20px" color="red" ></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SalesRPS">
                            <ItemTemplate>
                                <asp:Label ID="lbSalesRPSAmt" runat="server" Text='<%# Eval("SalesRPSAmt") %>' ></asp:Label>                                               
                                                </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lbtotSalesRPSAmt" runat="server" Font-Bold="true" font-size="20px" color="red"  ></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sales Non RPS">
                            <ItemTemplate>
                           <asp:Label ID="lbSalesNRPSAmt" runat="server" Text='<%# Eval("SalesNRPSAmt") %>' ></asp:Label>                          
                                                </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lbtotSalesNRPSAmt" runat="server"  Font-Bold="true"  font-size="20px" color="red"></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment RPS">
                            <ItemTemplate>
                                <asp:Label ID="lbpaymentRPSamt" runat="server" Text='<%# Eval("paymentRPSamt") %>' ></asp:Label>  
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lbtotpaymentRPSamt" runat="server" Font-Bold="true"  font-size="20px" color="red" ></asp:Label>
                            </FooterTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Payment Non RPS" >
                            <ItemTemplate>
                                <asp:Label ID="lbpaymentNRPSamt" runat="server" Text='<%# Eval("paymentNRPSamt") %>'></asp:Label> 
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lbtotpaymentNRPSamt" runat="server"  Font-Bold="true"  font-size="20px" color="red" ></asp:Label>
                            </FooterTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Return RPS" >
                             <ItemTemplate>
                                 <asp:Label ID="lbreturRPSamt" runat="server" Text='<%# Eval("returRPSamt") %>' ></asp:Label> 
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lbtotreturRPSamt" runat="server" Font-Bold="true"  font-size="20px" color="red" ></asp:Label>
                            </FooterTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Return Non RPS" >
                             <ItemTemplate>
                                 <asp:Label ID="lbreturNRPSamt" runat="server" Text='<%# Eval("returNRPSamt") %>'></asp:Label> 
                            </ItemTemplate>
                            <FooterTemplate>
                            <asp:Label ID="lbtotreturNRPSamt" runat="server"  Font-Bold="true"  font-size="20px" color="red" ></asp:Label>
                            </FooterTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Check IN" >
                             <ItemTemplate>
                                 <%# Eval("CheckIn") %>
                            </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Check Out" >
                             <ItemTemplate>
                                 <%# Eval("CheckOut") %>
                            </ItemTemplate>
                         </asp:TemplateField>
                        <asp:TemplateField HeaderText="Time In Customer" >
                             <ItemTemplate>
                                 <%# Eval("TIC") %>
                            </ItemTemplate>
                         </asp:TemplateField>
                        
                    </Columns>
                    <EditRowStyle CssClass="table-edit" />
                    <FooterStyle CssClass="table-footer" />
                    <HeaderStyle CssClass="table-header" />
                    <PagerStyle CssClass="table-page" />
                    <RowStyle />
                    <SelectedRowStyle CssClass="table-edit" />
                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>

