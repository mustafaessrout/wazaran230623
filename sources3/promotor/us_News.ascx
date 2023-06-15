<%@ Control Language="C#" AutoEventWireup="true" CodeFile="us_News.ascx.cs" Inherits="us_News" %>
 <script src="statics/js/jquery-3.2.1.min.js"></script>
    <script src="statics/js/jquery.cookie.js"></script>
    
<script type="text/javascript">
    $(function () {

        $('#usr_nm').html($.cookie("usr_nm"));
        $('#exh_dt').html($.cookie("exh_dt"));
            <%--$(".menu").accordion({
                autoHeight: false,
                event: "mousedown",
                active: activeIndex,
                change: function (event, ui) {
                    var index = $(this).children('h3').index(ui.newHeader);
                    $('#<%#hidAccordionIndex.ClientID %>').val(index);
                }
            });--%>

        $.ajax({
            type: "POST",
            url: "Default.aspx/GetNotification",
            data: '{userID:' + $.cookie("usr_id") + '}',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {
                alert(response.d);
            },
            error: function (response) {
                alert(response.d);
            }
        });

        function OnSuccess(response) {

            var htmlData = '<ul id="notif-content">';
            var htmlPopup = '';
            var userDetails = '';
            $('.badge').html(response.d.length);
            for (i = 0; i < response.d.length; i++) {
                htmlData += ' <li"><a href="#" data-toggle="modal" data-target="#' + response.d[i]["notificationID"] + '" style="display:block" >' +
                            '<div class="choice">' +
                                '<i class="fa fa-envelope"></i>' +
                                '<div class="tab-content">' +
                                    '<p class="title">' + response.d[i]["titleSort"] + '</p>' +
                                    '<p class="date">' + response.d[i]["notificationDate"] + '</p>' +
                                    '<p class="ellapsis">' + response.d[i]["descSort"] + '</p>' +
                                '</div>' +
                            '</div>' +
                          '</a></li>';
                htmlPopup += '<div id="' + response.d[i]["notificationID"] + '" class="modal fade" role="dialog">' +
                               '<div class="modal-dialog">' +
                               '<div class="modal-content">' +
                                   '<div class="modal-header">' +
                                   '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                                   '<h4 class="modal-title">' + response.d[i]["notificationSortMessage"] + '</h4>' +
                                   '</div>' +
                                   '<div class="modal-body">' +
                                   '<p>' + response.d[i]["notificationDesc"] + '</p>' +
                                   '</div>' +
                                   '<div class="modal-footer">' +
                                   '<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>' +
                                   '</div>' +
                               '</div>' +
                               '</div>' +
                           '</div>';




                //if (response.d[i]["notificationID"] == 1) {
                //htmlData += ' <div id="dialog" title="Basic dialog">'+
                //                  '<p>This is the default dialog which is useful for displaying information. The dialog window can be moved, resized and closed with the "' + "x" +'"  icon.</p>'+
                //                  '</div>';

                //htmlData +='<a href="javascript:void(0)" onclick="document.getElementById('light').style.display="block";document.getElementById("fade").style.display="block"">here</a>';
                //htmlData +='<div id="light" class="white_content">This is the lightbox content. <a href="javascript:void(0)" onclick="document.getElementById("light").style.display="none";document.getElementById("fade").style.display="none"">Close</a>' +
                //'</div>'+
                //'<div id="fade" class="black_overlay"></div>';
                //}

                //htmlData += '<div id="light" class="white_content">This is the lightbox content. <a href="javascript:void(0)" onclick="document.getElementById("light").style.display="none";document.getElementById("fade").style.display="none">Close</a>' +
                //            '</div>' +
                //            '<div id="fade" class="black_overlay"></div>';
            }

            userDetails += '<div id="userDetails" class="modal fade" role="dialog">' +
                      '<div class="modal-dialog">' +
                      '<div class="modal-content">' +
                          '<div class="modal-header">' +
                          '<button type="button" class="close" data-dismiss="modal">&times;</button>' +
                          '<h4 class="modal-title">Login Details</h4>' +
                          '</div>' +
                          '<div class="modal-body">' +
                          '<div class="clearfix">' +
                          '<div class="col-sm-6">' +
                          '<p>Login ID</p>' +
                          '<p>' + response.d[0]["usr_id"] + '</p>' +
                          "</div>" +
                          '<div class="col-sm-6">' +
                          '<p>Full Name</p>' +
                          '<p>' + response.d[0]["fullname"] + '</p>' +
                          "</div>" +
                          "</div>" +
                          '</div>' +
                          '<div class="modal-footer">' +
                          '<button type="button" class="btn btn-default" data-dismiss="modal">Close</button>' +
                          '</div>' +
                      '</div>' +
                      '</div>' +
                  '</div>';

            $('#dvNotification').html(htmlData);



            $('#popShowMessage').html(htmlPopup);
            $('#popShowUserDetails').html(userDetails);
        }


    });
</script>
<div class="notifications dropdown">
                                <a href="#">
                                    <i class="fa fa-globe"></i>
                                    <span class="badge">10</span>
                                </a>

                                <div class="notif animated child">
                                    <div class="ntf" style="height: 550px; overflow-y: scroll;">
                                        <div class="tab-title">
                                            <a href="#">Notification</a>
                                        </div>
                                        <div class="tab-container" id="dvNotification">
                                        </div>
                                    </div>
                                </div>
                            </div>

  <div id="popShowMessage"> 
   </div>
    <div id="popShowUserDetails"> 
   </div>
