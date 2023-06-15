<%@ Page Language="C#" AutoEventWireup="true" CodeFile="fm_loginho2.aspx.cs" Inherits="fm_loginho2" EnableEventValidation="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Wazaran Head Office</title>
    <script src="js/jquery-1.9.1.min.js"></script>
    <%--<link href="css/loginstyle.css" rel="stylesheet" />--%>
    <link href="css/anekabutton.css" rel="stylesheet" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />

    <!--custom css-->
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/animate.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/responsive.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>

    <script src="js/sweetalert.min.js"></script>
    <link href="css/sweetalert.css" rel="stylesheet" />
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
    <style>
        a.text-white.underline,
        a.text-white.underline:visited{
            color:#fff;
            text-decoration:underline;
            transition: all .5s;
             animation-duration: .5s;
            animation-fill-mode: both;
        }
        a.text-white.underline:hover,
        a.text-white.underline:focus{
            text-decoration:none;
            transform: scale(1.05);
        }
        #login-remember:focus + label{
            transform: scale(1.05);
        }
    </style>
</head>
<body>
     <form runat="server" id="frmsubmit" class="full-height login-ho">
         <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
         <div class="login4 center middle full-height" >
             <div class="content animated fadeInUp"  id="loginbox" >
                 <div class="logo ">
                    <img src="image/logo1.jpg" alt="logo sbtc" />
                </div>
               <div class="login-form">
                   <h3 class="title text-center">Sign In Wazaran ERP Head Office</h3>
           
                    <div class="login-inputs">
                        <div class="clearfix">
                            <p class="pull-left">Sign In</p>
                            <p class="pull-right"><a href="#" class="text-white underline">Forgot password?</a></p>
                        </div>
                        

                        <div class="border-white tops">
                            <asp:TextBox ID="txusername" runat="server" placeholder="Username"></asp:TextBox>
                        </div>
                         <div class="border-white bottoms">
                            <asp:TextBox ID="txpassword" runat="server" TextMode="Password" placeholder="Password"></asp:TextBox>
                        </div>
                        <div class="checkbox">
                            <input id="login-remember" type="checkbox" name="remember" value="1"/> 
                            <label for="login-remember">
                                Remember me
                            </label>
                        </div>
                         
                        <asp:LinkButton ID="btlogin" OnClientClick="javascript:ShowProgress();" runat="server" CssClass="btn btn-white full" OnClick="btlogin_Click"><i class="fa fa-sign-in padding-right"></i>Login</asp:LinkButton>
                      
                        <div class="clearfix">
                            <div class=" pull-right margin-top margin-bottom" >
                                Don't have an account! <a href="#" class="text-white underline" onclick="$('#loginbox').hide(); $('#signupbox').fadeIn();">Sign Up Here</a>
                            </div>
                        </div>
                      
                    </div>
        
                </div>
             </div>
             <div class="content animated fadeInUp" id="signupbox" style="display:none;">
                <div class="logo ">
                    <img src="image/logo1.jpg" alt="logo sbtc" />
                </div>
                <div class="login-form">
                   <h3 class="title text-center">Sign Up Wazaran ERP Head Office</h3>

                    <div class="login-inputs">
                        <div class="clearfix">
                           
                             <div id="signupalert" style="display:none" class="alert alert-danger">
                                <p>Error:</p>
                                <span></span>
                            </div>
                        </div>
                    </div>

                     <div class="bg-white clearfix" style="padding:15px 10px 10px; border-radius:5px;">
                        <div class="margin-bottom clearfix">
                            <label for="txtemail" class="col-md-3 control-label" style="color:#555">Email</label>
                            <div class="col-md-9 no-padding">
                                <input type="text" class="form-control" name="email" id="txtemail" placeholder="Email Address" />
                            </div>
                        </div>
                         <div class="margin-bottom clearfix">
                            <label for="firstname" class="col-md-3 control-label" style="color:#555">First Name</label>
                            <div class="col-md-9 no-padding">
                                <input type="text" class="form-control" name="firstname" placeholder="First Name" />
                            </div>
                        </div>
                        <div class="margin-bottom clearfix">
                            <label for="lastname" class="col-md-3 control-label" style="color:#555">Last Name</label>
                            <div class="col-md-9 no-padding">
                                <input type="text" class="form-control" name="lastname" placeholder="Last Name"/>
                            </div>
                        </div>
                        <div class="margin-bottom clearfix">
                            <label for="password" class="col-md-3 control-label" style="color:#555">Password</label>
                            <div class="col-md-9 no-padding">
                                <input type="password" class="form-control" name="passwd" placeholder="Password" />
                            </div>
                        </div>
                                    
                        <div class="clearfix">
                            <label for="icode" class="col-md-3 control-label" style="color:#555">Invitation Code</label>
                            <div class="col-md-9 no-padding">
                                <input type="text" class="form-control" name="icode" placeholder="code" />
                            </div>
                        </div>

                    </div>
                    
                        <div class="margin-bottom margin-top clearfix">
                            <!-- Button -->                                        
                                <button id="btn-signup" type="button" class="btn btn-block btn-info"><i class="fa fa-user-plus padding-right"></i>  Sign Up</button>
                                <p  class="text-center margin-top">or</p>  
                                <button id="btn-fbsignup" type="button" class="btn btn-block btn-primary"><i class="fa fa-facebook padding-right"></i>Sign Up with Facebook</button>
                               
                        </div>
                    <div class="clearfix" style="border-top:1px solid #fff">
                        <p class="pull-right"><a id="signinlink" href="#" onclick="$('#signupbox').hide(); $('#loginbox').show()" class="text-white underline">Sign In</a></p>
                    </div>
                         
                </div>
                 
             </div>
         </div>

         <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>
       </form>     
</body>
</html>
