<%@ Page Language="C#" AutoEventWireup="true" CodeFile="switcher.aspx.cs" Inherits="switcher" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Select Apps to Enter</title>
    <%--<link href="Content/beatifullcontrol.css" rel="stylesheet" />--%>
    <%--<link href="Content/bootstrap.min.css" rel="stylesheet" />--%>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
     <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/animate.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/responsive.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
    <script src="js/jquery-1.9.1.min.js"></script>
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
</head>
<body class="switcher">
    <form id="form1" runat="server" >
    <div>
        <div class="container-fluid">
            <div class="header animated fadeInUp">
                <div class="center ">
                    <div class="logo ">
                        <img src="image/logo1.jpg" alt="logo sbtc"/>
                    </div>
                </div>
                <div class="divheader text-center text-white">Please select destination</div>
            </div>


            <div class="container margin-bottom">
                <div class="clearfix top-text text-white">
                    <label  class="control-label pull-left">Enter Head Office Apps</label>
                    <asp:Label ID="lbcaption" CssClass="pull-right no-margin" runat="server" Text="* Please select Application Head Office from Portal"></asp:Label>
                </div>
			    <div class="tile-container">
                    <%
                        cbll bll = new cbll();List<cArrayList> arr= new List<cArrayList>();
                        System.Data.SqlClient.SqlDataReader rs = null;
                        arr.Add(new cArrayList("@emp_cd", Request.Cookies["usr_id"].Value.ToString()));
                        bll.vGetSwitcher(arr, ref rs);
                        while (rs.Read())
                        {     
                    %>
				    <a href="<%=rs["urls"].ToString() %>" class="tile fg-white animated flipInX" data-role="tile">
				        <div class="btns ">
                            <div class="btn-1">
                                <svg>
                                    <rect x="0" y="0" fill="none" width="100%" height="100%"/>
                                </svg>
                                <div class="cntn">
                                    <img src="image/<%=rs["imgname"].ToString() %>" alt="Alternate Text" />
                                    <p><%=rs["app_nm"].ToString() %></p>
                                </div>
                            </div>
                        </div>	
				    </a>
                    <%}
                        rs.Close(); %>
                 <%--   <a href="#" class="tile fg-white animated flipInX" data-role="tile">
				        <div class="btns ">
                            <div class="btn-1">
                                <svg>
                                    <rect x="0" y="0" fill="none" width="100%" height="100%"/>
                                </svg>
                                <div class="cntn">
                                    <img src="image/information_system.png" alt="Alternate Text" />
                                    <p>Executive Information System</p>
                                </div>
                            </div>
                        </div>	
                        <a href="#" class="tile fg-white animated flipInX" data-role="tile">
				        <div class="btns ">
                            <div class="btn-1">
                                <svg>
                                    <rect x="0" y="0" fill="none" width="100%" height="100%"/>
                                </svg>
                                <div class="cntn">
                                    <img src="image/HO.png" alt="Alternate Text" />
                                    <p>Head Office Master Data</p>
                                </div>
                            </div>
                        </div>	
				    </a>
                        <a href="#" class="tile fg-white animated flipInX" data-role="tile">
				        <div class="btns ">
                            <div class="btn-1">
                                <svg>
                                    <rect x="0" y="0" fill="none" width="100%" height="100%"/>
                                </svg>
                                <div class="cntn">
                                    <img src="image/car.png" alt="Alternate Text" />
                                    <p>Car Maintenance</p>
                                </div>
                            </div>
                        </div>	
				    </a>
                        <a href="#" class="tile fg-white animated flipInX" data-role="tile">
				        <div class="btns ">
                            <div class="btn-1">
                                <svg>
                                    <rect x="0" y="0" fill="none" width="100%" height="100%"/>
                                </svg>
                                <div class="cntn">
                                    <img src="image/promotor.png" alt="Alternate Text" />
                                    <p>Promoter Apps</p>
                                </div>
                            </div>
                        </div>	
				    </a>
                        <a href="#" class="tile fg-white animated flipInX" data-role="tile">
				        <div class="btns ">
                            <div class="btn-1">
                                <svg>
                                    <rect x="0" y="0" fill="none" width="100%" height="100%"/>
                                </svg>
                                <div class="cntn">
                                    <img src="image/branch.png" alt="Alternate Text" />
                                    <p>Branches Reporting</p>
                                </div>
                            </div>
                        </div>	
				    </a>
				    </a>--%>
                   
                    			          
			    </div>

                <div class="navi margin-top padding-top text-right">
                    <asp:LinkButton ID="btBack" runat="server" CssClass="btn btn-link" style="min-width:150px;color:#fff;" OnClick="btBack_Click">
                        <i class="fa fa-angle-double-left" aria-hidden="true"></i> Back
                    </asp:LinkButton>
                </div>
		    </div>
           
            <div class="form-group hide">
                <div class="col-md-4 ">
                    <asp:DropDownList ID="cbapps" runat="server" CssClass="form-control">
                    </asp:DropDownList>
                </div>
                <asp:LinkButton ID="btview" CssClass="btn btn-primary" OnClientClick="javascript:ShowProgress();" runat="server" OnClick="btview_Click">GO</asp:LinkButton>
            </div>
        </div>
    </div>
    </form>
    <div class="divmsg loading-cont" id="pnlmsg" >
            <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
    </div>
</body>
</html>
