<%@ Page Title="" Language="C#" MasterPageFile="~/maps/maps.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="maps_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        #map { position:absolute; top:0; bottom:0; width:100%; }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainMaps" Runat="Server">
	<nav class="col-md-2 d-none d-md-block bg-light sidebar">
		<div class="sidebar-sticky">
		<ul class="nav flex-column">
			<li class="nav-item">
			<a class="nav-link active" href="#">
				<span data-feather="home"></span>
				Dashboard <span class="sr-only">(current)</span>
			</a>
			</li>
			<li class="nav-item">
			<a class="nav-link" href="#">
				<span data-feather="file"></span>
				Orders
			</a>
			</li>
			<li class="nav-item">
			<a class="nav-link" href="#">
				<span data-feather="shopping-cart"></span>
				Products
			</a>
			</li>
			<li class="nav-item">
			<a class="nav-link" href="#">
				<span data-feather="users"></span>
				Customers
			</a>
			</li>
			<li class="nav-item">
			<a class="nav-link" href="#">
				<span data-feather="bar-chart-2"></span>
				Reports
			</a>
			</li>
			<li class="nav-item">
			<a class="nav-link" href="#">
				<span data-feather="layers"></span>
				Integrations
			</a>
			</li>
		</ul>

		<h6 class="sidebar-heading d-flex justify-content-between align-items-center px-3 mt-4 mb-1 text-muted">
			<span>Saved reports</span>
			<a class="d-flex align-items-center text-muted" href="#">
			<span data-feather="plus-circle"></span>
			</a>
		</h6>
		<ul class="nav flex-column mb-2">
			<li class="nav-item">
			<a class="nav-link" href="#">
				<span data-feather="file-text"></span>
				Current month
			</a>
			</li>
			<li class="nav-item">
			<a class="nav-link" href="#">
				<span data-feather="file-text"></span>
				Last quarter
			</a>
			</li>
			<li class="nav-item">
			<a class="nav-link" href="#">
				<span data-feather="file-text"></span>
				Social engagement
			</a>
			</li>
			<li class="nav-item">
			<a class="nav-link" href="#">
				<span data-feather="file-text"></span>
				Year-end sale
			</a>
			</li>
		</ul>
		</div>
	</nav>
	<main role="main" class="col-md-9 ml-sm-auto col-lg-10 px-4">
		<div class="d-flex justify-content-between flex-wrap flex-md-nowrap align-items-center pt-3 pb-2 mb-3 border-bottom">
		<h1 class="h2">Maps</h1>
		</div>
        <canvas class="my-4 w-100" id="myChart" width="900" height="380">
            <div id='map' ></div>
        </canvas>
		<script>
		mapboxgl.accessToken = 'pk.eyJ1Ijoibmljby1zYnRjIiwiYSI6ImNqcGdnMHNvZjA4ZGIzcXJtMWlrM2Vyb2gifQ.LDc5XM8DOTyD_ly1wkBwGw';
		var map = new mapboxgl.Map({
			container: 'map',
			style: 'mapbox://styles/mapbox/streets-v10'
		});
		</script>
	</main>

</asp:Content>

