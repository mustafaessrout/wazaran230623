<%@ Page Language="C#" AutoEventWireup="true" CodeFile="lookup_map.aspx.cs" Inherits="lookup_map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Lookup </title>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script>
        $(document).ready(function () {
            $.ajax({
                type: 'GET',
                url: 'https://maps.googleapis.com/maps/api/geocode/json?latlng=<%=Request.QueryString["la"]%>,<%=Request.QueryString["at"]%>&key=AIzaSyDbFnnD5nwqBJVeQn_aD68irHKryMWPp2w',
                success: function (data, status) {
                    var sAddr = data.results[0].formatted_address;
                    $("#<%=lbaddress.ClientID%>").text(sAddr);
                }
            });
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <script type="text/javascript" src="http://maps.googleapis.com/maps/api/js?sensor=false&key=AIzaSyDbFnnD5nwqBJVeQn_aD68irHKryMWPp2w&callback=initMap"></script>
        <script type="text/javascript">
            function InitializeMap() {
                var latlng = new google.maps.LatLng(<%=Request.QueryString["la"]%>, <%=Request.QueryString["at"]%>);
                var myOptions = {
                    zoom: 19,
                    center: latlng,
                    mapTypeId: google.maps.MapTypeId.ROADMAP
                };

                map = new google.maps.Map(document.getElementById("map"), myOptions);
                var marker = new google.maps.Marker
                    (
                    {
                        position: new google.maps.LatLng(<%=Request.QueryString["la"]%>, <%=Request.QueryString["at"]%>),
                        map: map,
                        title: 'Click me'
                    }
                    );
                var infowindow = new google.maps.InfoWindow({
                    content: 'Location info:<br/>Country Name:<br/>LatLng:'
                });
                google.maps.event.addListener(marker, 'click', function () {
                    // Calling the open method of the infoWindow 
                    infowindow.open(map, marker);
                });
            }


            window.onload = InitializeMap;

        </script>

        <h2>
            <asp:Label ID="lbcust" runat="server"></asp:Label></h2>
        <h4 style="color:red">Registered in Google Map:
            <p style="color:black;text-underline-position:below"><asp:Label ID="lbaddress" runat="server"></asp:Label></p>
        </h4>

        <div id="map" style="width: 100%; position: absolute; height: 100%">
        </div>

    </form>
</body>
</html>
