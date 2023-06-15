<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    <link href="Content/beatifullcontrol.css" rel="stylesheet" />
    <style>
        .imgauto img{
            width:auto !important;
            height:auto !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    
    <%--<div class=" margin-bottom" id="news">
        <h1> News</h1>
        <div class="news-menu">
            <ul>
                <li>
                    <a href="#">SBTC News</a>
                </li>
                <li >
                    <a href="#">Government</a>
                </li>
            </ul>
        </div>
        <div class="news-content">
            <div class="content">
               <div>
                    <%
                            System.Data.SqlClient.SqlDataReader rs = null;
                            cbll bll = new cbll();
                            bll.vGetTopNews(ref rs);
                            while (rs.Read())
                            {
                        
                         %>
                    <div class="list">
                        <div class="pic">
                            <div class="category">
                                <a href="#"><%=rs["dept_cd"].ToString() %></a>
                            </div>
                            <div class="img">
                                <img src="image/new.jpg" alt="Alternate Text" />
                            </div>

                        </div>
                       
                        <div class="ttl">
                            <h3 class="title"><%=rs["news_subject"].ToString() %></h3>
                            <div class="date">
                                <a href="#"><i class="fa fa-calendar"></i><span >12 June 2017</span></a> 
                            </div>
                        </div>
                        <div class="ctn">
                            <p><%=rs["news_content"].ToString() %></p>
                            
                            <img src="image/new.jpg"/>
                        </div>
                        <div class="read-more"><a class="btn btn-link"  href="/news.aspx" >Read More...</a></div>
                       
                    </div>
                    <%
                        }
                            rs.Close();    
                         %>
                    <div class="list">
                        <div class="pic">
                            <div class="category">
                                <a href="#">Accounting</a>
                            </div>
                            <div class="img">
                                <img src="image/new.jpg" alt="Alternate Text" />
                            </div>

                        </div>
                        <div class="ttl">
                            <h3 class="title">lorem ipsum</h3>
                            <div class="date">
                                <a href="#"><i class="fa fa-calendar"></i><span >12 June 2017</span></a> 
                            </div>
                        </div>
                        <div class="ctn">
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum cursus lectus sed leo venenatis laoreet. Integer ante purus, maximus a accumsan maximus, suscipit a velit. Donec pretium ligula at pharetra ornare. Aenean posuere scelerisque tortor, imperdiet commodo odio tempor vitae. Aliquam pulvinar a elit sit amet consequat. Aenean tincidunt accumsan erat, eget pharetra sem sagittis et. Nunc mollis, ipsum at finibus aliquet, odio quam sagittis nulla, et condimentum nisi dui eget nulla. Quisque elementum massa commodo est ornare, id suscipit dui dictum. Morbi bibendum sapien ut eros fermentum, ac congue tortor feugiat. Integer in pellentesque erat, vitae suscipit dui. Mauris nec tincidunt ipsum, id finibus tortor. Curabitur at mi ultrices, convallis tellus sit amet, dapibus erat. Curabitur auctor placerat nunc, in scelerisque nisl mattis at.
                        </div>
                        <div class="read-more"><a class="btn btn-link"  href="/news.aspx" >Read More...</a></div>
                    
                    </div>
                 
                </div>
               <div  class="page">
                    <ul>
                        <li>
                            <a href="#"><i class="fa fa-angle-double-left" aria-hidden="true"></i>Prev</a>
                        </li>
                        <li>
                            <a href="#">...</a>
                        </li>  
                        <li>
                            <a href="#">21</a>
                        </li> 
                        <li>
                            <a href="#">22</a>
                        </li> 
                        <li>
                            <a href="#">23</a>
                        </li> 
                        <li>
                            <a href="#">24</a>
                        </li> 
                        <li>
                            <a href="#">25</a>
                        </li> 
                        <li>
                            <a href="#">...</a>
                        </li>  
                        <li>
                            <a href="#">Next<i class="fa fa-angle-double-right" aria-hidden="true"></i></a>
                        </li>
                    </ul>
                </div>
            </div>

            <div class="content">
                <div>
                    <div class="list">
                        <div class="pic">
                            <div class="category">
                                <a href="#">Accounting</a>
                            </div>
                            <div class="img">
                                <img src="image/news.jpg" alt="Alternate Text" />
                            </div>

                        </div>
                        <div class="ttl">
                            <h3 class="title">lorem ipsum</h3>
                            <div class="date">
                                <a href="#"><i class="fa fa-calendar"></i><span >12 June 2017</span></a> 
                            </div>
                        </div>
                        <div class="ctn">
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum cursus lectus sed leo venenatis laoreet. Integer ante purus, maximus a accumsan maximus, suscipit a velit. Donec pretium ligula at pharetra ornare. Aenean posuere scelerisque tortor, imperdiet commodo odio tempor vitae. Aliquam pulvinar a elit sit amet consequat. Aenean tincidunt accumsan erat, eget pharetra sem sagittis et. Nunc mollis, ipsum at finibus aliquet, odio quam sagittis nulla, et condimentum nisi dui eget nulla. Quisque elementum massa commodo est ornare, id suscipit dui dictum. Morbi bibendum sapien ut eros fermentum, ac congue tortor feugiat. Integer in pellentesque erat, vitae suscipit dui. Mauris nec tincidunt ipsum, id finibus tortor. Curabitur at mi ultrices, convallis tellus sit amet, dapibus erat. Curabitur auctor placerat nunc, in scelerisque nisl mattis at.
                        </div>
                        <div class="read-more"><a class="btn btn-link"  href="/news.aspx" >Read More...</a></div>
                    
                    </div>
                    <div class="list">
                        <div class="pic">
                            <div class="category">
                                <a href="#">Accounting</a>
                            </div>
                            <div class="img">
                                <img src="image/news.jpg" alt="Alternate Text" />
                            </div>

                        </div>
                        <div class="ttl">
                            <h3 class="title">lorem ipsum</h3>
                            <div class="date">
                                <a href="#"><i class="fa fa-calendar"></i><span >12 June 2017</span></a> 
                            </div>
                        </div>
                        <div class="ctn">
                            Lorem ipsum dolor sit amet, consectetur adipiscing elit. Vestibulum cursus lectus sed leo venenatis laoreet. Integer ante purus, maximus a accumsan maximus, suscipit a velit. Donec pretium ligula at pharetra ornare. Aenean posuere scelerisque tortor, imperdiet commodo odio tempor vitae. Aliquam pulvinar a elit sit amet consequat. Aenean tincidunt accumsan erat, eget pharetra sem sagittis et. Nunc mollis, ipsum at finibus aliquet, odio quam sagittis nulla, et condimentum nisi dui eget nulla. Quisque elementum massa commodo est ornare, id suscipit dui dictum. Morbi bibendum sapien ut eros fermentum, ac congue tortor feugiat. Integer in pellentesque erat, vitae suscipit dui. Mauris nec tincidunt ipsum, id finibus tortor. Curabitur at mi ultrices, convallis tellus sit amet, dapibus erat. Curabitur auctor placerat nunc, in scelerisque nisl mattis at.
                        </div>
                        <div class="read-more"><a class="btn btn-link"  href="/news.aspx" >Read More...</a></div>
                    
                    </div>
                </div>
               
                <div  class="page">
                    <ul>
                        <li>
                            <a href="#"><i class="fa fa-angle-double-left" aria-hidden="true"></i> Prev</a>
                        </li>
                        <li>
                            <a href="#">...</a>
                        </li>  
                        <li>
                            <a href="#">21</a>
                        </li> 
                        <li>
                            <a href="#">22</a>
                        </li> 
                        <li>
                            <a href="#">23</a>
                        </li> 
                        <li>
                            <a href="#">24</a>
                        </li> 
                        <li>
                            <a href="#">25</a>
                        </li> 
                        <li>
                            <a href="#">...</a>
                        </li>  
                        <li>
                            <a href="#">Next<i class="fa fa-angle-double-right" aria-hidden="true"></i></a>
                        </li>
                    </ul>
                </div>
            </div>
        </div>


        <div class="modal fade bd-example-modal-lg" id="content-news" tabindex="-1" role="dialog" aria-labelledby="exampleModalLongTitle" aria-hidden="true">
          <div class="modal-dialog" role="document">
            <div class="modal-content">
              <div class="modal-header">
                <h5 class="modal-title" id="exampleModalLongTitle">Title News</h5>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                  <i class="fa fa-close" aria-hidden="true"></i>
                </button>
              </div>
              <div class="modal-body margin-bottom">
                lorem ipsum
              </div>
  
            </div>
          </div>
        </div>
    </div>--%>

 
    <div class="imgauto">
         <asp:Image ID="Image1" runat="server" ImageUrl="~/indomie.png" />
        <asp:SlideShowExtender ID="slidersbtc" runat="server" TargetControlID="Image1"
                    SlideShowServiceMethod="GetSlides"  AutoPlay="true" PlayInterval="5000" Loop="true" PlayButtonID="btnPlay" StopButtonText="Stop"
                    PlayButtonText="Play" NextButtonID="btnNext" PreviousButtonID="btnPrevious" ImageDescriptionLabelID="lbdesc" ImageTitleLabelID="lbtitle" Enabled="true">
        </asp:SlideShowExtender>
    </div>
   


    <asp:Button ID="btnPlay" runat="server" Text="Button" style="display:none"/><asp:Button ID="btnStop" runat="server" Text="Button" style="display:none" />
    <asp:Button ID="btnNext" runat="server" Text="Button" style="display:none" /><asp:Button ID="btnPrevious" runat="server" Text="Send SMS" OnClick="btnPrevious_Click" style="display:none" />
    <asp:Label ID="lbtitle" runat="server" Text="Label" Visible="False"></asp:Label><asp:Label ID="lbdesc" runat="server" Text="lbdesc" style="display:none"></asp:Label>
    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="kerem" style="display:none"/>

    
    



    
     <script>
         $(".ctn").each(function () {
             if ($(this).text().length > 500) {
                 $(this).text($(this).text().substr(0, 496));
                 $(this).append('...');
             }
         });

         var mn = 1;
         newContent(1);
         $(".news-menu li:first-child").addClass('active');

         $(".news-menu li").each(function (index) {
             $(this).click(function () {
                 $(".news-menu li").removeClass('active');
                 $(this).addClass('active');
                 newContent(index+1);
             })
         });

         $(".news-content .content .list").each(function (index) {
             $(this).find(".read-more").click(function () {
                 
             })
         })

         function newContent(ind){
             $(".news-content .content").each(function (index) {
                 $(this).addClass('hide');
             });
             
             $(".news-content .content:nth-child(" + ind + ")").removeClass('hide');
         }

   </script>
</asp:Content>

