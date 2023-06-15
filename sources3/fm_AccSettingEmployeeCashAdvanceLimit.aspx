<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="fm_AccSettingEmployeeCashAdvanceLimit.aspx.cs" Inherits="fm_AccSettingEmployeeCashAdvanceLimit" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="head" Runat="Server">
    <link href="css/anekabutton.css" rel="stylesheet" />

    <script src="admin/js/bootstrap.min.js"></script>
    <script type = "text/javascript" >

        function preventBack() { window.history.forward(); }
        setTimeout("preventBack()", 0);
        window.onunload = function () { null };

        //$j(document, parent.document).ready(function () {
        //    alert('Done');
        //});

        //app.route('/*')
        //    .get(function (req, res) {
        //        res.sendfile(app.get('localhost:9999') + '/index.html');
        //    });
    </script>

    <style>
        .hidobject{
            display:none;
        }
    </style>

  <%--  <script>
        function Selecteditem(sender, e) {
            //$get('<//%=//hditem.ClientID%//>').value = e.get_value();
            dv.attributes["class"].value = "showdiv";
        }
    </script>
    
     <script>
         function ShowProgress() {
             $j('#pnlmsg').show();
         }

         function HideProgress() {
             $j("#pnlmsg").hide();
             return false;
         }
         $j(document).ready(function () {
             $j('#pnlmsg').hide();
         });

    </script>--%>
    <style type="text/css">
        .divmsg{
       /*position:static;*/
       top:30%;
       right:50%;
       left:50%;
       width: 50px;
        height: 45px;
       position:fixed;
       /*background-color:greenyellow;*/
       overflow-y:auto;
        }
        .divhid {
            display:none;
        }

        .divnormal {
            display:normal;
        }
    </style> 

 

	
	<script type="text/javascript" src="assets/jqGrid/js/jquery-1.9.0.min.js"></script>
	<script type="text/javascript" src="assets/jquery/js/jquery-ui-1.10.3.custom.js"></script>
	<link type="text/css" href="assets/jquery/css/ocean-cloudy-min/jquery-ui.min.css" rel="stylesheet" />
	<link type="text/css" href="assets/jqGrid/css/ui.jqgrid.css" rel="stylesheet" />
	<script type="text/javascript" src="assets/jqGrid/js/jquery.jqGrid.src.js"></script>
	<script type="text/javascript" src="assets/jqGrid/js/i18n/grid.locale-en.js"></script>
    <script type="text/javascript" src="assets/jsOtherLibs/json3.js" ></script>

    
    <style type="text/css">
        .ui-jqgrid .ui-widget-header {
            background-color: steelblue;
            background-image: none
        }
        #editmodlist .ui-jqdialog-titlebar{
            background-color: steelblue;
            background-image: none
        }
        #alerthd_list{
            background-color: steelblue;
            background-image: none
        }
        #delhdlist{
            background-color: steelblue;
            background-image: none
        }
        .ui-datepicker-header{
            background-color: steelblue;
            background-image: none
        }
        #searchhdfbox_list {
            background-color: steelblue;
            background-image: none
        }
    </style>

    <script type="text/javascript">
         var $j = jQuery.noConflict();

         //$j(document).ready(function () {
         $j(document, parent.document).ready(function () {
             function getCountry() {
                 var country;
                 $j.ajax({
                     type: "POST",
                     contentType: "application/json",
                     data: "{}",
                     async: false,
                     url: "fm_AccSettingEmployeeCashAdvanceLimit.aspx/getNational",
                     dataType: "json",
                     success: function (data) {

                         country = data.d;

                     },

                     error: function (XMLHttpRequest, textStatus, errorThrown) {
                         //error: function (xhRequest, textStatus, errorThrown) {
                         debugger;
                     }

                 });
                 return country;
             }




             $j.ajax({
                 type: "POST",
                 contentType: "application/json",
                 data: "{}",
                 url: "fm_AccSettingEmployeeCashAdvanceLimit.aspx/getEmployee",
                 dataType: "json",
                 success: function (data) {
                     data = data.d;
                     $j("#list").jqGrid({
                         datatype: "local",
                         colNames: ['Employee Id', 'Name', 'Email', 'Phone', 'Password', 'Nationality', 'Date of Birth'],
                         colModel: [
                             { name: 'employeeId', index: 'employeeId', width: 55, stype: 'text', sortable: true, editable: true, formoptions: { rowpos: 1, colpos: 1 }, editrules: { integer: true }, editoptions: { readonly: true } },
                             { name: 'name', index: 'name', width: 90, stype: 'text', sortable: true, editable: true, editrules: { required: true }, formoptions: { rowpos: 2, colpos: 1 } },
                             { name: 'email', index: 'email', width: 100, stype: 'text', sortable: true, editable: true, editrules: { email: true, required: true }, formoptions: { rowpos: 2, colpos: 2 } },
                             { name: 'phone', index: 'phone', width: 80, align: "left", stype: 'text', sortable: true, editable: true, formoptions: { rowpos: 3, colpos: 1 } },
                             { name: 'pwd', index: 'pwd', width: 80, align: "left", stype: 'text', edittype: 'password', sortable: true, editable: true, formoptions: { rowpos: 3, colpos: 2 } },
                             {
                                 //name: 'nationalId', index: 'nationalId', width: 80, align: "right", formatter: 'select', stype: 'select',
                                 name: 'nationalityId', index: 'nationalityId', width: 80, align: "left", formatter: 'select', stype: 'select',
                                 editable: true, edittype: "select", editoptions: { value: getCountry() }, formoptions: { rowpos: 4, colpos: 1 }
                             },
                             {
                                 name: 'dateOfBirth', index: 'dateOfBirth', width: 80, align: "left",
                                 edittype: 'text', editable: true,
                                 editoptions: {
                                     dataInit: function (el) {
                                         $j(el).datepicker({
                                             //dateFormat: "dd/mm/yy",
                                             formatter: 'date',
                                             formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' },
                                             //changeMonth: true,
                                             //changeYear: true,
                                         });
                                     }
                                 },
                                 formoptions: { rowpos: 4, colpos: 2 },
                                 formatter: 'date',
                                 formatoptions: { srcformat: 'Y-m-d', newformat: 'Y-m-d' },
                             }

                         ],
                         data: JSON.parse(data),
                         rowno: 10,
                         loadonce: true,
                         /* multiselect:true,*/
                         rowlist: [5, 10, 20],
                         pager: '#pager',
                         viewrecords: true,
                         gridview: true,
                         sortorder: 'asc',
                         toppager: true,
                         cloneToTop: true,
                         altrows: true,
                         autowidth: true,
                         hoverrows: true,

                         height: 300,
                         /* toolbar: [true, "top"],*/
                         rownumbers: true,
                         caption: " ",
                         editurl: 'fm_AccSettingEmployeeCashAdvanceLimit.aspx/EditUpdateDel',
                         /*  ondblClickRow: function(rowid) {
                                 $j(this).jqGrid('editGridRow', rowid,
                                 {recreateForm:true,closeAfterEdit:true,
                                 closeOnEscape:true,reloadAfterSubmit:false, width: 500});
                                 }*/


                     });
                    $j('#list').jqGrid('navGrid', '#pager',
                        {
                            edit: true,
                            add: true,
                            del: true,
                            search: true,
                            searchtext: "Search",
                            addtext: "Add",
                            edittext: "Edit",
                            deltext: "Delete",
                            cloneToTop: true

                        }
                        , {
                            recreateForm: true,
                            reloadAfterSubmit: true,
                            width: 500,
                            closeAfterEdit: true,
                            ajaxEditOptions: { contentType: "application/json" },
                            serializeEditData: function (postData) {
                                var postdata = { 'data': postData };
                                return JSON.stringify(postdata);
                            },
                            afterShowForm: function ($form) {
                                // "editmodlist"
                                $form.closest('div#editmodlist').position({
                                    of: $("#list").closest('div.ui-jqgrid'),
                                    my: "center",
                                    //at: "center"
                                });
                            },


                        },
                        {
                            recreateForm: true,

                            beforeShowForm: function (form) {
                                $j('#tr_employeeId', form).hide();
                            },
                            afterShowForm: function ($form) {
                                // "editmodlist"
                                $form.closest('div#editmodlist').position({
                                    of: $("#list").closest('div.ui-jqgrid'),
                                    my: "center",
                                    //at: "center"
                                });
                            },
                            width: 500,
                            reloadAfterSubmit: true,
                            closeAfterAdd: false,
                            ajaxEditOptions: { contentType: "application/json" },
                            serializeEditData: function (postData) {
                                var postdata = { 'data': postData };
                                return JSON.stringify(postdata);
                            }




                        },
                        {
                            ajaxDelOptions: { contentType: "application/json" },
                            reloadAfterSubmit: false,
                            onclickSubmit: function (eparams) {
                                var retarr = {};
                                var sr = $j("#list").getGridParam('selrow');
                                rowdata = $j("#list").getRowData(sr);
                                retarr = { employeeId: rowdata.employeeId };

                                return retarr;
                            },
                            serializeDelData: function (data) {
                                var postData = { 'data': data };
                                return JSON.stringify(postData);
                            },
                            afterShowForm: function ($form) {
                                // "editmodlist"
                                $form.closest('div#delmodlist').position({
                                    of: $("#list").closest('div.ui-jqgrid'),
                                    my: "center",
                                    //at: "center"
                                });
                            }
                        },
                        {
                            ajaxSearchOptions: { contentType: "application/json" },
                            //closeAfterSearch: true,
                            //afterShowForm: test,
                            //onClose: test
                            beforeShowForm: function (formid) {
                                centerViewForm();
                            }
                        },
                    );

                    $j("#list").jqGrid('filterToolbar', {
                        stringResult: true, searchOnEnter: true, defaultSearch: 'cn',
                        //afterShowForm: function ($form) {
                        //    // "editmodlist"
                        //    $form.closest('div#searchmodfbox_list').position({
                        //        of: $("#list").closest('div.ui-jqgrid'),
                        //        my: "center",
                        //        //at: "center"
                        //    });
                        //}
                    });
                    var grid = $j("#list");
                    var topPagerDiv = $j('#' + grid[0].id + '_toppager')[0];         // "#list_toppager"
                    /* $j("#edit_" + grid[0].id + "_top", topPagerDiv).remove();        // "#edit_list_top"
                    $j("#del_" + grid[0].id + "_top", topPagerDiv).remove();         // "#del_list_top"
                    $j("#search_" + grid[0].id + "_top", topPagerDiv).remove();      // "#search_list_top"
                    $j("#refresh_" + grid[0].id + "_top", topPagerDiv).remove();     // "#refresh_list_top"*/
                    $j("#" + grid[0].id + "_toppager_center", topPagerDiv).remove(); // "#list_toppager_center"
                    $j(".ui-paging-info", topPagerDiv).remove();

                    var bottomPagerDiv = $j("div#pager")[0];
                    $j("#add_" + grid[0].id, bottomPagerDiv).remove();
                    $j("#edit_" + grid[0].id, bottomPagerDiv).remove();
                    $j("#del_" + grid[0].id, bottomPagerDiv).remove();
                    $j("#refresh_" + grid[0].id, bottomPagerDiv).remove();
                    // "#add_list"

                },

                //loadComplete: function () {
                //    $j("#list").setColProp('Nationality', { editoptions: { value: nationalities } });
                //},

                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //error: function (xhRequest, textStatus, errorThrown) {
                    debugger;
                }
            });
         });

         test = function () {
             $("#searchmodfbox_list").position({ my: "center", at: "center", of: "#list" });
             //$form.closest('div#searchmodfbox_list').position({
             //    of: $("#list").closest('div.ui-jqgrid'),
             //    my: "center",
             //    //at: "center"
             //});
         }
         //test2 = function () {
         //    $("#searchhdfbox_list").attr({ style: "width: auto; height: auto; z-index: 950; overflow: hidden; top: 4px; left: 4px; display: block;" })
         //}

        //////////////////////////////////////////
        //$j(function () {
        //    $j("#startdate").datepicker({ dateFormat: "dd-mm-yy" }).val()
        //    $j("#enddate").datepicker({ dateFormat: "dd-mm-yy" }).val()
        //});

        //$.datepicker.setDefaults({ dateFormat: 'yy-mm-dd' });
        //$j('#datepicker1, #datepicker2').datepicker();

        //$j("#hasDatepicker").datepicker({ dateFormat: "dd-mm-yyyy" });

        //$j(function () {

        //    $j(".hasDatepicker").datepicker({
        //        onSelect: function (dateText) {
        //            $j("#dateOfBirth").val(dateText);
        //            $j(this).change();
        //        }
        //    }).on("change", function () {
        //        $j("#dateOfBirth").val(dateText);
        //    });
        //});

         //var grid = $j("#list");
         //grid.jqGrid('navGrid', '#pager',
         //    {
         //        beforeShowForm: function (form) {
         //            // "editmodlist"
         //            var dlgDiv = $("#editmodlist" + grid[0].id);
         //            var parentDiv = dlgDiv.parent(); // div#gbox_list
         //            var dlgWidth = dlgDiv.width();
         //            var parentWidth = parentDiv.width();
         //            var dlgHeight = dlgDiv.height();
         //            var parentHeight = parentDiv.height();
         //            // TODO: change parentWidth and parentHeight in case of the grid
         //            //       is larger as the browser window
         //            dlgDiv[0].style.top = Math.round((parentHeight - dlgHeight) / 2) + "px";
         //            dlgDiv[0].style.left = Math.round((parentWidth - dlgWidth) / 2) + "px";
         //        }
         //    });

         function centerViewForm() {
             $(".ui-jqdialog").css("visibility", "hidden");
             setTimeout(function () {
                 $(viewform).css("left", ($(document).width() / 2) - ($(viewform).width() / 2));
                 $(viewform).css("top", ($(document).height() / 2) - ($(viewform).height() / 2));
                 $(viewform).css("visibility", "visible");
             });
         }

    </script>

</asp:Content>
<asp:Content ID="BodyContent" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <div class="divheader">Employee Cash Advance Limit Setting</div>

    <div class="h-divider"></div>
    
    <div class="container-fluid">
        <div>
            <div id="topList"></div>
            <table id="list"></table> <%--for grid--%>
            <div id="pager"> </div> <%--for paging--%>
        </div>

        <br/><br/>

<%--        <div class="divmsg loading-cont" id="pnlmsg" >
                <div><i class="fa fa-spinner fa-5x fa-spin spiner" aria-hidden="true"></i></div>
        </div>--%>
    </div>
</asp:Content>

