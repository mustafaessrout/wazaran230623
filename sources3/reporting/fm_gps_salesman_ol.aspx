<%@ Page Title="" Language="C#" MasterPageFile="~/reporting/reporting.master" AutoEventWireup="true" CodeFile="fm_gps_salesman_ol.aspx.cs" Inherits="fm_gps_salesman_ol" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainholder" Runat="Server">
    <div>
     <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAunjADn63o3HcVh6j-kVovthjYyn0eCTM"></script>
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
       var markers = GetMapData();
       window.onload = function () {
           LoadMap();
           SetMarker();
       };

       var map;
       var marker;
       function LoadMap() {
           var mapOptions = {
               center: new google.maps.LatLng(markers[0].Latitude, markers[0].Longitude),
               zoom: 10,
               mapTypeId: google.maps.MapTypeId.ROADMAP
           };
           map = new google.maps.Map(document.getElementById("dvMap"), mapOptions);
           //SetMarker(0);
       };
       function SetMarker() {
           markers = GetMapData();
           //Remove previous Marker.
           if (marker != null) {
               marker.setMap(null);
           }
           

           //Set Marker on Map.
           var infoWindow = new google.maps.InfoWindow();
           //var conten
           for (i = 0; i < markers.length; i++) {
               var data = markers[i];
               var myLatlng = new google.maps.LatLng(data.Latitude, data.Longitude);
               if (data.office=="O"){
                   vicon = "flag.png";
               }
               else if (data.distanceKM > 1) {
                   vicon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + data.Emp_cd + '|00ff00|000000'; //green
               }
               else 
                   vicon = 'http://chart.apis.google.com/chart?chst=d_map_pin_letter&chld=' + data.Emp_cd + '|ff00ff|000000'; //red

               
              
               marker = new google.maps.Marker({
                   position: myLatlng,
                   map: map,
                   title: data.nm
                   , icon: vicon
               });
               (function (marker, data) {        
                   google.maps.event.addListener(marker, "click", function (e) {
                       if (data.office == "O") {
                           content = data.sp_nm;
                       }
                       else {
                           content = "Time : " + data.Locdatetime + " <br>Distance From " + data.sp_nm + " : " + data.distanceKM + " Km</br><br>Accuracy : " + data.Accuracy + "</br><br> Salesman: " + data.nm + "</br";
                       }
                       infoWindow.setContent(content);
                       infoWindow.open(map, marker);
                   });
               })(marker, data)
           }
       };
       function GetMapData() {
           var json = '';
           $.ajax({
               type: "POST",
               url: "fm_gps_salesman_ol.aspx/GetData",
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
       }
    </script>
    </div>
    <div class="form-horizontal" style="font-family:Calibri;font-size:small">
            <div class="form-group">
                <label class="control-label col-md-1">Salespoint</label>
                <div class="col-md-2">
                    <asp:DropDownList ID="cbsp" CssClass="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cbsp_SelectedIndexChanged"></asp:DropDownList>
                </div>
                <%--<div class="col-md-1">
                    <asp:LinkButton ID="btview" CssClass="btn btn-primary" OnClientClick="javascript:ShowProgress();" runat="server" OnClick="btview_Click">VIEW</asp:LinkButton>
                </div>--%>
            </div>
        </div>
    <div id="dvMap" style="width: 100%; height: 500px"></div>
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
        <asp:Timer ID="TimerMarker" runat="server" Interval="60000" OnTick="TimerMarker_Tick">
        </asp:Timer>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="TimerMarker" EventName="Tick" />
            </Triggers>
        </asp:UpdatePanel>    
    <div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:GridView ID="grdgps" runat="server" AutoGenerateColumns="False" Caption="Last Salesman Location" CellPadding="0" CssClass="table table-striped mygrid" ShowHeaderWhenEmpty="false">
                    <AlternatingRowStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="No">
                            <ItemTemplate>
                                <asp:Label ID="lbgpsNo" runat="server" Text='<%# Eval("gpsNo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Branch Name">
                            <ItemTemplate>
                                <%# Eval("sp_nm") %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Salesman Name">
                            <ItemTemplate>
                                <asp:Label ID="lbsalesman_nm" runat="server" Text='<%# Eval("salesman_nm") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Latitude">
                            <ItemTemplate>
                                <asp:Label ID="lbLatitude" runat="server" Text='<%# Eval("Latitude") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Longitude">
                            <ItemTemplate>
                                <asp:Label ID="lbLongitude" runat="server" Text='<%# Eval("Longitude") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Accuracy">
                            <ItemTemplate>
                                <asp:Label ID="lbAccuracy" runat="server" Text='<%# Eval("Accuracy") %>'></asp:Label>
                            </ItemTemplate>
                             </asp:TemplateField>
                        <asp:TemplateField HeaderText="DistanceKM From Office">
                            <ItemTemplate>
                                <asp:Label ID="lbDistanceKM" runat="server" Text='<%# Eval("DistanceKM") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Last Update">
                            <ItemTemplate>
                                <asp:Label ID="lbLocdatetime" runat="server" Text='<%# Eval("Locdatetime","{0:dd/MM/yyyy HH:mm:ss}") %>'></asp:Label>
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

