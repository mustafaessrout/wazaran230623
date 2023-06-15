<%@ Page Language="C#" AutoEventWireup="true" CodeFile="news.aspx.cs" Inherits="news" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" href="css/anekabutton.css" />
    <link href="css/bootstrap.min.css" rel="stylesheet" />
    <script src="js/jquery.min.js"></script>
    <script src="js/bootstrap.min.js"></script> 
     <link rel="stylesheet" href="css/jquery.scrollbar.css"/>
    <script src="js/jquery.scrollbar.js"></script>
	<script src="js/modernizr.custom.js"></script>

     <!--custom css-->
    <link href="css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/custom/metro.css" rel="stylesheet" />
    <link href="css/custom/animate.css" rel="stylesheet" />
    <link href="css/custom/style.css" rel="stylesheet" />
    <link href="css/custom/responsive.css" rel="stylesheet" />
    <link href="css/font-face/khula.css" rel="stylesheet"/>
  
   

    <!--custom js-->
    <script src="js/index.js"></script>
  
    <script src="js/sweetalert.min.js"></script>
    <script src="js/sweetalert-dev.js"></script>
    <script src="js/modernizr.js"></script>
    <script src="js/jquery.floatThead.js"></script>


</head>
<body id="detail-news">
    <form id="form1" runat="server">
        <div class="container">
            <div class="row clearfix">
                <div class="col-md-8">
                    <div class="title">
                        <h1>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vulputate elementum porta. Curabitur cursus, libero molestie feugiat mattis, </h1>
                    </div>
                    <div class="detail-cat">
                        <a href="#"> <i class="fa fa-calendar-o"></i> Monday, 14 June 2014 14:00</a>
                        <a href="#"><i class="fa fa-user-circle"></i> Accounting</a>
                    </div>
                    <div class="top-image">
                        <img src="image/news.jpg" alt="Alternate Text" />
                    </div>
                    
                    <div class="news-content">
                        <p>
                            Lorem ipsum dolor sit amet,<u>consectetur adipiscing elit</u> . Etiam vulputate elementum porta. Curabitur cursus, libero molestie feugiat mattis, felis ligula gravida est, at vestibulum mi diam mollis nisi. Integer pretium augue velit, non dignissim massa semper in. Ut fringilla leo turpis, eu maximus lorem pellentesque in. In hac habitasse platea dictumst. Maecenas imperdiet volutpat libero eget semper. Vivamus vel tellus et quam blandit pellentesque ullamcorper ac tortor. Curabitur eros enim, eleifend ac magna et, sodales malesuada dui. Proin semper, nisi vitae pulvinar eleifend, lacus purus rhoncus purus, placerat fermentum velit turpis sed ligula. Nulla at elit quis metus imperdiet semper quis a risus.
                        </p>
                        <p>
                            Ut sed nisi vehicula, sodales nulla ac, <i>tincidunt sem</i>. Aliquam erat volutpat. Quisque mattis placerat odio, vel bibendum ex interdum in. Nam posuere risus congue, porttitor dolor et, finibus quam. Donec imperdiet dolor eget risus accumsan, id interdum sapien luctus. Nam sed mi id augue eleifend blandit et et sem. Cras risus metus, tempor at nisl in, bibendum pulvinar nisl. Curabitur mattis tellus sit amet urna auctor condimentum. Nullam vestibulum euismod neque, in dapibus orci congue tempor. Suspendisse ornare sem in elit interdum finibus. Integer diam nibh, aliquet non blandit eget, interdum vitae velit. Sed ullamcorper arcu lacinia, mollis augue vitae, maximus diam. Aenean viverra est at ex bibendum, quis porttitor ex ornare. Phasellus erat tortor, tempus at rutrum eget, vehicula posuere ante.
                        </p>
                        <img src="image/news.jpg" alt="content image" class="pull-left"/>
                        <p>
                            Nullam dignissim, <b>lorem ipsum </b>elit congue pretium aliquam, arcu ipsum volutpat mauris, sit amet lacinia turpis urna vitae tellus. Vivamus vitae tempor metus. Class aptent taciti sociosqu ad litora torquent per conubia nostra, per inceptos himenaeos. Integer at gravida nisi, in tincidunt nisi. Cras ac neque sit amet magna dignissim laoreet non eget massa. Aliquam erat volutpat. Ut eu lorem vitae massa rhoncus porttitor. Praesent commodo diam neque, facilisis hendrerit nunc sollicitudin vel. Aenean ac ante facilisis, auctor nisl sed, molestie augue. Pellentesque quis orci ac ex finibus mattis. In ultrices nisi ac blandit porta.
                        </p>
                        <blockquote>
                            Mauris lobortis justo ut lobortis semper. Etiam sagittis elit vitae felis tincidunt, a vehicula orci sodales. Mauris pharetra diam ante, in imperdiet erat scelerisque a. Curabitur sed erat elit. Suspendisse eu libero et tortor tempus ornare. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Mauris at nunc eget dui aliquet luctus. Sed id justo nibh. Praesent quis magna arcu. Donec et lectus velit. Nam viverra ante eu odio vulputate elementum. Praesent varius est quis risus volutpat, in vestibulum elit volutpat. In tincidunt, leo at convallis dictum, arcu leo rhoncus dui, id condimentum massa lorem non enim. Pellentesque at massa vitae justo finibus eleifend ut sit amet mi. Sed eros metus, ultrices non pretium sit amet, consequat dapibus diam.
                        </blockquote>
                    </div>
                    <div class="page-control">
                        <a href="#"> <i class="fa fa-angle-double-left" aria-hidden="true"></i>  <span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vulputate elementum porta</span></a>
                        <a href="#"><span>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vulputate elementum porta</span>  <i class="fa fa-angle-double-right" aria-hidden="true"></i> </a>
                    </div>
                </div>
            
                <div class="col-md-4">
                    <div class="sidebr">
                        <div class="title">
                            <p>Berita terbaru</p>
                        </div>
                        
                        <a href="#" class="new-news-list">
                            <div class="image">
                                <img src="image/news.jpg" alt="Alternate Text" />
                            </div>
                            <div class="titles">
                                <span class="categ">accounting</span>
                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vulputate elementum porta. Curabitur cursus, libero molestie feugiat mattis</p>
                                <span class="date"> <i class="fa fa-calendar"></i> Monday ,20 June 2017 14:14</span>
                            </div>
                        </a>
                        
                        <a href="#" class="new-news-list">
                            <div class="image">
                                <img src="image/news.jpg" alt="Alternate Text" />
                            </div>
                            <div class="titles">
                                <span class="categ">accounting</span>
                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vulputate elementum porta. Curabitur cursus, libero molestie feugiat mattis</p>
                                <span class="date"> <i class="fa fa-calendar"></i> Monday ,20 June 2017 14:14</span>
                            </div>
                        </a>

                        <a href="#" class="new-news-list">
                            <div class="image">
                                <img src="image/news.jpg" alt="Alternate Text" />
                            </div>
                            <div class="titles">
                                <span class="categ">accounting</span>
                                <p>Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam vulputate elementum porta. Curabitur cursus, libero molestie feugiat mattis</p>
                                <span class="date"> <i class="fa fa-calendar"></i> Monday ,20 June 2017 14:14</span>
                            </div>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div class="back">
            <a href="/default.aspx" class=""> <i class="fa fa-reply-all animated jackInTheBox" aria-hidden="true"></i> <span class="animated pulse">BACK TO HOME</span> </a>
        </div>
    </form>

    <script>
        $(".sidebr a .titles p").each(function () {
            if ($(this).text().length > 50) {
                $(this).text($(this).text().substr(0, 46));
                $(this).append('...');
            }
        });
    </script>
</body>
</html>
