<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_login.aspx.cs" Inherits="eis2_fm_login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>Executive Information System</title>
	<link rel="stylesheet" href="assets/fonts/font-custom/opensans/stylesheet.css" />

	<link rel="stylesheet" href="assets/css/fontello/fontello.css" />
	<link rel="stylesheet" href="assets/css/fontello/animation.css" />
	<link rel="stylesheet" href="assets/css/bootsrap/bootstrap.min.css" />
	<link rel="stylesheet" href="assets/css/animate.css" />
	<link rel="stylesheet" href="assets/css/flag-icon.min.css" />
	<link rel="stylesheet" href="assets/css/font-awesome.min.css" />
	<link rel="stylesheet" href="assets/css/bootstrap-datepicker.standalone.min.css" />
	<link rel="stylesheet" href="assets/css/style.css" />
	<link rel="stylesheet" href="assets/css/responsive.css" />

	<script src="js/jquery-3.2.1.min.js" ></script>
	<script src="js/jquery.form-validator.js" ></script>
	<script src="js/popper.min.js" ></script>
	<script src="js/bootstrap.min.js"></script>
	<script src="js/bootstrap-datepicker.min.js"></script>
	<script src="js/script.js"></script>
</head>
<body class="bg-city">
	<div class="grey-opacity d-flex justify-content-center align-items-center fixed-full">
		<div class="container content col-sm-4">
			  <form class="login">
						<p class="logo text-center display-4 no-margin"><i class="icon-demo icon-globe"></i></p>
						<p class="login-text text-center">Executive Information System</p>
			      <div class="input">
			        <input type="email" class="form-control" id="inputEmail3" placeholder="Email" data-validation="email" data-validation-error-msg="Email wrong structure. Ex: jhon@Doe.com" required />
						</div>
						<div >
			        <input type="password" class="form-control" id="inputPassword3" placeholder="Password" data-validation="required" data-validation-error-msg="Password Can't be empty" />
			      </div>
				    <div class="text-right">
				    	<p>Not have Account ? <a href="#"> Register Now</a></p>
				    </div>

                <asp:LinkButton ID="btlogin" CssClass="btn btn-primary btn-block" runat="server" OnClick="btlogin_Click">Login</asp:LinkButton>
			  </form>
			</div>
	</div>
</body>
</html>
