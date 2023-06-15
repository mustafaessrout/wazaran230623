<%@ Page Title="" Language="C#" MasterPageFile="~/maps/maps.master" AutoEventWireup="true" CodeFile="fm_maps_view.aspx.cs" Inherits="maps_fm_maps_view" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
	<style>
		#map { position:absolute; top:0; bottom:0; width:95%; }
	</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainMaps" Runat="Server">

	<nav class="col-md-3 d-none d-md-block bg-light sidebar">
		<ul class="nav nav-tabs" id="myTab">
          <li class="active"><a href="#home">Home</a></li>
          <li><a href="#profile">Profile</a></li>
          <li><a href="#messages">Messages</a></li>
          <li><a href="#settings">Settings</a></li>
        </ul>
 
        <div class="tab-content">
          <div class="tab-pane active" id="home">...</div>
          <div class="tab-pane" id="profile">...</div>
          <div class="tab-pane" id="messages">...</div>
          <div class="tab-pane" id="settings">...</div>
        </div>
	</nav>

	<main role="main" class="col-md-8 ml-sm-auto col-lg-9 px-4">
		<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
		<h1 class="h2">Maps</h1>
		</div>
		<%--<canvas class="my-4 w-100" id="myChart" width="900" height="380">--%>
			<div id='map' ></div>
		<%--</canvas>--%>
		<script>
		mapboxgl.accessToken = 'pk.eyJ1Ijoibmljby1zYnRjIiwiYSI6ImNqcGdnMHNvZjA4ZGIzcXJtMWlrM2Vyb2gifQ.LDc5XM8DOTyD_ly1wkBwGw';
		var map = new mapboxgl.Map({
			container: 'map',
			style: 'mapbox://styles/mapbox/streets-v10'
		});
		</script>
	</main>


</asp:Content>

