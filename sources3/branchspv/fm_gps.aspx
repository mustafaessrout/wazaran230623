<%@ Page Title="" Language="C#" MasterPageFile="~/branchspv/branchspv.master" AutoEventWireup="true" CodeFile="fm_gps.aspx.cs" Inherits="fm_gps" %>

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
    <script type="text/javascript">
        
       
    function LoadMap() {
        var mapOptions = {
            center: new google.maps.LatLng(markers[0].lat, markers[0].lng),
            zoom: 8,
            mapTypeId: google.maps.MapTypeId.ROADMAP
        };
        var infoWindow = new google.maps.InfoWindow();
        var latlngbounds = new google.maps.LatLngBounds();
        var map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
 
        for (var i = 0; i < markers.length; i++) {
            var data = markers[i]
            var myLatlng = new google.maps.LatLng(data.lat, data.lng);
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                title: data.title,
                icon: data.icon
            });
            (function (marker, data) {
                google.maps.event.addListener(marker, "click", function (e) {
                    infoWindow.setContent("<div style = 'width:300px;min-height:100px'>" + data.description + "</div>");
                    infoWindow.open(map, marker);
                });
            })(marker, data);
            latlngbounds.extend(marker.position);
        }
        var bounds = new google.maps.LatLngBounds();
        map.setCenter(latlngbounds.getCenter());
        map.fitBounds(latlngbounds);
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
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="cbsp" EventName="SelectedIndexChanged" />
                        </Triggers>
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
                              <div id="dvMap" style="width:100%;height:100%">  </div>
               <%--         </ContentTemplate>
                    </asp:UpdatePanel>--%>
                    
                  
                </div>
            </div>
        </div>
    <%--</div>--%>
    <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
</asp:Content>

