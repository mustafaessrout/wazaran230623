
$(document).ready(function () {
    $(".menu .dropdown-child  .childs").each(function () {
        $(this).has('li').addClass('sss');
        if (!$(this).hasClass('sss')) {
            $(this).parent().remove();
        }
        $(this).has('li').removeClass('remove');
    });

    $("header .menu .dropdown-child .childs ul").each(function () {
        var h = 0;
        $(this).find('li').each(function () {
            h += 1;
        });
        if (h > 9) {
            $(this).addClass('to-height');
            $(this).attr('data-scroll', '0');
            $(this).attr('data-height', '0');
            $(this).prepend("<li class='menu-btn-scroll-up hide'><a href='#'><i class='fa fa-angle-double-up' aria-hidden='true'></i></a></li>");
            $(this).append("<li class='menu-btn-scroll-down'><a href='#'><i class='fa fa-angle-double-down' aria-hidden='true'></i></a></li>");
        }
    });

    var interval;
    var interval2;
    $("header .menu .dropdown-child .childs .to-height .menu-btn-scroll-up").hover(function () {
        var li = $(this);
        var hg = parseInt(li.parent().attr("data-height"));

        interval = setInterval(function () {
            var d = li.parent().attr('data-scroll');
            var ds = parseInt(d);

            if (!li.hasClass("hide")) {
                ds -= 10;
                li.parent().attr('data-scroll', ds);
                li.parent().scrollTop(ds);
            }
            if (ds == 0) {
                li.addClass('hide');
                clearInterval(interval);
            }
            if (ds <= hg) {
                li.parent().find(".menu-btn-scroll-down").removeClass("hide");
            }
        }, 50);
    },
    function () {
        clearInterval(interval);
    });

    $("header .menu .dropdown-child .childs .to-height .menu-btn-scroll-down").mouseover(function () {
        var li = $(this);

        var hg = parseInt(li.parent().attr("data-height"));
        if (hg == 0) {
            $(this).parent().children(".menu-link").each(function () {
                hg += $(this).height();
            });
            $(this).parent().attr("data-height", hg);
        }
        hg = parseInt($(this).parent().attr("data-height")) - 280;


        interval2 = setInterval(function () {
            var d = li.parent().attr('data-scroll');
            var ds = parseInt(d);

            if (!li.hasClass("hide")) {
                ds += 10;

                li.parent().attr('data-scroll', ds);
                li.parent().scrollTop(ds);
            }

            if (ds <= 0) {
                li.parent().find(".menu-btn-scroll-up").addClass("hide");
            } else {
                li.parent().find(".menu-btn-scroll-up").removeClass("hide");
            }

            if (ds >= hg) {
                li.addClass("hide");
                clearInterval(interval2);
            }
        }, 50);
    });

    $("header .menu .dropdown-child .childs .to-height .menu-btn-scroll-down").mouseout(function () {
        clearInterval(interval2);
    });




    //ellapsis
    elapsis();

    $("header").append("<div class='fullgrey'></div>");
    var cpy = $('.menu').html();
    $(".copy-menu").append(cpy);

    //icon-hamburger
    $('.icon-hamburger').click(function () {
        var d = $(this).attr('data-sidebar');
        var ss = $(this).attr('data-body-all');
        $('.fullgrey').addClass('active');
        $('.menu .childs').removeClass('show animateds fadeInUp');
        $(d).addClass('open');

        if (ss == "true") {
            $("body > *").addClass("body-geser-kiri");
        }
    });

    $('.close-burger').click(function () {
        $("body > *").removeClass("body-geser-kiri");
        $(".fullgrey").removeClass("active");
        $(".menu-burger").each(function () {
            $(this).removeClass('open');
        });
    });
    $('.fullgrey').click(function () {
        $("body > *").removeClass("body-geser-kiri");
        $(".fullgrey").removeClass("active");
        $(".menu-burger").each(function () {
            $(this).removeClass('open');
        });
    });
    //input empty fill
    $('.err-empty > input').keydown(function (e) {
        $(this).closest('.err-empty').removeClass('error err-empty');
    });

    //scroll
    $(".auto-complate-list").each(function () {
        $(this).hover(function () {
        }, function () {
            $(".menu-content .scrollbars").mCustomScrollbar("update");
        });
    });

    //dropdown icon
    tableDropdown();


    //auto focus
    var er = false;
    $('.error').each(function () {
        er = true;
    });

    if (er) {
        $('.error input').focus();
    } else {
        $(".containers.bg-white").find('input, select, a').filter(':visible:enabled:first').focus();
    }


    $("span.form-control").find('input[type="checkbox"]').parent().css({ "background-color": "#fff" });
    $("span.form-control.ro").find('input[type="checkbox"]').parent().css({ "background-color": "rgba(144, 144, 144, 0.4)" });

    var wh = $(window).height();
    var mh = wh - 183;

    //menu
    $('body').click(function () {
        $('header .dropdown-master > .dropselect').each(function () {
            $(this).parent().removeClass('open');
            $(this).parent().children(".childs").find(".childs").removeClass('show animateds fadeInUp');
            $(this).parent().children(".childs").removeClass("fadeInUp show animateds");
        });
    });

    $('header .dropdown-master > .dropselect').click(function (e) {
        e.stopPropagation();

        if ($(this).parent().hasClass('open')) {
            $(this).parent().removeClass('open');
            $(this).parent().children(".childs").find(".childs").removeClass('show animateds fadeInUp');
            $(this).parent().children(".childs").removeClass("fadeInUp show animateds");
        } else {
            $('.dropdown-master').removeClass('open');
            $(this).parent().addClass('open');
            $('.dropdown-master').children(".childs").removeClass('show animateds fadeInUp ');
            $('.dropdown-child').children(".childs").removeClass('show animateds fadeInUp');
            $(this).parent().children(".childs").addClass('show animateds fadeInUp');
        }
    });


    $('.copy-menu .dropdown > .dropselect').click(function (e) {

        if ($(this).parent().hasClass("open")) {
            $(this).parent().removeClass("open");
        } else {
            if ($(this).parent().hasClass("dropdown-master")) {
                $(".copy-menu .dropdown-master").removeClass("open");
                $(".copy-menu .dropdown-child").removeClass("open");
            }
            if ($(this).parent().hasClass("dropdown-child")) {
                $(".copy-menu .dropdown-child").removeClass("open");
            }
            $(this).parent().addClass("open");
        }


    });

    $(".copy-menu .dropdown-child > .dropselect").click(function (e) {

    });


    $('.nav-show-hide').click(function () {
        tabFix();
        $(this).toggleClass('active');
        if ($(this).hasClass('active')) {
            $('.nav-show-hide span').text('Auto Hide Menu');
            showhide(true);
        } else {
            $('.nav-show-hide span').text('Always Show Menu');
            showhide(false);
        }
    });




    //menu show hide
    var sidebarClick = false;
    $("#mCSB_1_scrollbar_vertical").mousedown(function () {
        sidebarClick = true;
    });
    //$('body').mouseup(function () {
    //    sidebarClick = false;
    //    console.log(sidebarClick);
    //});

    $('.sidebar').hover(function () {
        if (!$('.nav-show-hide').hasClass('active')) {
            showhide(true);
        }
    }, function () {
        if (!$("#mCSB_1_scrollbar_vertical").hasClass("mCSB_scrollTools_onDrag")) {
            if (!$('.nav-show-hide').hasClass('active')) {
                showhide(false);
            }
        }
        $('.main-content').mouseup(function () {
            if (!$('.nav-show-hide').hasClass('active')) {
                showhide(false);
            }
        });
    });


    //tooltip
    $('[data-toggle="tooltip"]').tooltip();

    //scroll header info bottom
    var timeOut;

    //table fixed
    tabFix();


    //functions

    function showhide(act) {
        if (act) {
            $('.sidebar').addClass('active');
            $('.main-content').removeClass('sidebar-deac');
            $('.nav-show-hide span').delay(100).fadeIn('fast');
            $('.menu .dropdown').removeClass('open');

            fnSaveData(true);
        } else {
            $('.sidebar').removeClass('active');
            $('.main-content').addClass('sidebar-deac');
            $('.menu .dropdown .childs').hide();
            $('.nav-show-hide span').hide();
            fnSaveData(false);
        }
    }



    //dafault main menu
    $(".menu .menu-link").each(function () {
        var ss = $(this).attr("id");
        var lngt = ss.length;
        var dd = ss.substr(0, lngt - 5);
        $(this).attr("id", dd);
    });


    var loc = window.location.pathname.substr(1);

    var mn = jsonMenu();
    var prntMn = -1;
    var chldMn = -1;
    var lnkMn = -1;
    var brk = false;
    if (loc == "default.aspx") {
        $("#home-btn").addClass('active');
    } else {
        for (var i = 0; i < Object.keys(mn).length; i++) {
            prntMn = i;
            for (var s = 0; s < Object.keys(mn[i].menu.child_menu).length; s++) {
                chldMn = s;
                for (var d = 0; d < Object.keys(mn[i].menu.child_menu[s].menu_link).length; d++) {
                    var f = mn[i].menu.child_menu[s].menu_link[d].link;
                    if (f == loc) {
                        lnkMn = d;
                        brk = true;
                        break;
                    }
                }
                if (brk) {
                    break;
                }
            }
            if (brk) {
                break;
            }
        }
        menuDefault("." + mn[prntMn].id, "." + mn[prntMn].menu.child_menu[chldMn].class, "." + mn[prntMn].menu.child_menu[chldMn].menu_link[lnkMn].id);
    }


});

function showScroll() {
    $(".scrollbars").mCustomScrollbar({
        theme: "inset-2"
    });
    $(".menu-content .scrollbars").mCustomScrollbar("update");
}

function tabErr() {
    //tab button function
    $('input').keydown(function (e) {
        var code = e.keyCode || e.which;
        if (code == '9') {
            if ($(this).val() == '') {
                $(this).addClass("errorss").delay(10).queue(function (next) {
                    $(this).focus();
                    $(this).removeClass("errorss");
                    next();
                });
                $(this).parent().find(".error-text").remove();
                $(this).addClass("empty-text").parent().addClass("relative").append("<div class='error-text'>Can't be blank</div>").find(".empty-text");
            }
        } else {
            $(this).parent().removeClass("relative").find(".error-text").remove();
            $(this).removeClass("empty-text");
        }
    });

    $('.error > input').keydown(function (e) {
        $(this).closest('.error').removeClass('error');
    });
    $('.error > input').click(function () {
        if ($(this).closest('.error').find('.date').hasClass('date')) {
            $(this).closest('.error').removeClass('error');
        }
    });
}

function fnSaveData(cont) {
    window.localStorage.setItem("showhidemenu", cont);
    return (window.localStorage.getItem("showhidemenu"));
}
function fnGetData() {
    return (window.localStorage.getItem("showhidemenu"));
}
function menuDefault(parenetId, childId, acr) {
    $(parenetId).each(function () {
        $(this).addClass('active');
    });
}

function tablePageCopy() {
    tabFix();
    $('.table-page-fix').each(function () {
        var id = $(this).attr('data-table-page');
        var html = $(this).find('.table-page');
        if (id !== undefined) {
            $(id).html(html);
        }
    });
}


function tabFix() {
    $(".table-fix").each(function (index) {
        var ss = $(this).find('.table-header').html();
        $(this).append("<thead>" + ss + "</thead>").find('tbody .table-header').remove();

        justTable(this);

        var sd = $(this).parent('.floatThead-wrapper').find('.overflow-x').find('thead').html();
        $(this).parent('.floatThead-wrapper').delay(300).find('overflow-x').find('thead').hide();
    });
}

function justTable(args) {
    $(args).floatThead({
        position: 'absolute',
        scrollContainer: true
    });
}

function elapsis() {
    $('.data-ellapsis').each(function () {

        var long = $(this).attr('data-ell-length');
        var ss = $(this).text();
        $(this).attr("true-data", $(this).text());

        var tx;

        if ($(this).attr('data-del-space') == 'true') {
            var d = ss.replace(/  +/g, '');
            tx = d.split(',').join(', ');
        } else if ($(this).attr('data-del-space') == 'false') {
            tx = $(this).text();
        }

        if (tx.length > long) {
            $(this).text(tx.substr(0, tx.lastIndexOf(' ', long - 4)) + '...');
        }

    });
}


function editTable() {
    $('.table .table-edit').each(function () {
        $(this).find('input,select').filter(":first").focus();
        return false;
    });
}

function tableDropdown() {
    $('.table select').each(function () {
        if ($(this).width() > 80) {
            $(this).parent().addClass("drop-down drop-down-sm");
        }
    });
}

function jsonMenu() {
    var menu = [
    {
        "name": "acounting",
        "id": "accounting-btn",
        "menu": {
            "child_menu": [
                {
                    "name": "Master",
                    "class": "menu-master",
                    "menu_link": [
                        {
                            "name": "item cashout",
                            "id": "fm_mstitemcashout",
                            "link": "fm_mstitemcashout.aspx"
                        }
                    ]
                },
                {
                    "name": "Request",
                    "class": "menu-request",
                    "menu_link": [
                        {
                            "name": "Request CN / DN",
                            "id": "fm_reqcreditnote",
                            "link": "fm_reqcreditnote.aspx"
                        },
                        {
                            "name": "Request Cash Out",
                            "id": "fm_cashregoutentry",
                            "link": "fm_cashregoutentry.aspx"
                        }
                    ]
                },
                {
                    "name": "approval",
                    "class": "menu-approv",
                    "menu_link": [
                        {
                            "name": "Approval CN/ DN",
                            "id": "fm_appcreditnote",
                            "link": "fm_appcreditnote.aspx"
                        }
                    ]
                },
                {
                    "name": "Transaction Branch",
                    "class": "menu-transaction",
                    "menu_link": [
                        {
                            "name": "Payment Receipt",
                            "id": "fm_paymentreceipt2",
                            "link": "fm_paymentreceipt2.aspx"
                        },
                        {
                            "name": "Cash Register",
                            "id": "fm_cashregisterentry",
                            "link": "fm_cashregisterentry.aspx"
                        },
                        {
                            "name": "Bank Collection",
                            "id": "fm_bankdepositlist",
                            "link": "fm_bankdepositlist.aspx"
                        },
                        {
                            "name": "Return Payment Applied",
                            "id": "fm_applyreturn",
                            "link": "fm_applyreturn.aspx"
                        },
                        {
                            "name": "Daily Closing Cashier",
                            "id": "cashamountclosing",
                            "link": "cashamountclosing.aspx"
                        },
                        {
                            "name": "Payment Suspense Applied",
                            "id": "fm_paymentsuspense",
                            "link": "fm_paymentsuspense.aspx"
                        }
                    ]
                },
                {
                    "name": "report",
                    "class": "menu-report",
                    "menu_link": [
                        {
                            "name": "Daily Collection Report",
                            "id": "fm_rptpayment",
                            "link": "fm_rptpayment.aspx"
                        },
                        {
                            "name": "Daily Collection Report",
                            "id": "fm_rptpayment",
                            "link": "fm_rptpayment.aspx"
                        },
                        {
                            "name": "Internal Statment Of Account",
                            "id": "fm_rptsoa",
                            "link": "fm_rptsoa.aspx"
                        },
                        {
                            "name": "Summry TO and Collection from Outlet",
                            "id": "fm_summtakingorderbysales",
                            "link": "fm_summtakingorderbysales.aspx"
                        },
                        {
                            "name": "Cash Denomination Report",
                            "id": "fm_cashdenomination",
                            "link": "fm_cashdenomination.aspx"
                        },
                        {
                            "name": "Collection over duedate Report",
                            "id": "fm_duedatereport",
                            "link": "fm_duedatereport.aspx"
                        },
                        {
                            "name": "Cashier Report",
                            "id": "fm_cashiermonthly",
                            "link": "fm_cashiermonthly.aspx"
                        },
                        {
                            "name": "External Statment Of Account",
                            "id": "fm_soa",
                            "link": "fm_soa.aspx"
                        }
                    ]
                }
            ]
        }
    },
    {
        "name": "Claim (A&P) ",
        "id": "claim-btn",
        "menu": {
            "child_menu": [
                {
                    "name": "Master",
                    "class": "btn-master",
                    "menu_link": [
                        {
                            "name": "Customer Price",
                            "id": "fm_custtypepriceentry",
                            "link": "fm_custtypepriceentry.aspx"
                        },
                        {
                            "name": "Proposal List",
                            "id": "fm_listproposal",
                            "link": "fm_listproposal.aspx"
                        },
                        {
                            "name": "Discount (Scheme)",
                            "id": "fm_mstdiscount2",
                            "link": "fm_mstdiscount2.aspx"
                        },
                        {
                            "name": "Document Claim",
                            "id": "fm_datarowclaimdocument",
                            "link": "fm_datarowclaimdocument.aspx"
                        }
                    ]
                },
                {
                    "name": "Request",
                    "class": "btn-req",
                    "menu_link": [
                         {
                             "name": "Request Claim Cash Out",
                             "id": "fm_claimcashout",
                             "link": "fm_claimcashout.aspx"
                         },
                         {
                             "name": "Request Claim CN DN",
                             "id": "fm_cndnclaim",
                             "link": "fm_cndnclaim.aspx"
                         },
                         {
                             "name": "Request Business Agrement",
                             "id": "fm_mstcontract",
                             "link": "fm_mstcontract.aspx"
                         },
                         {
                             "name": "Payment Agrement",
                             "id": "fm_contractpayment",
                             "link": "fm_contractpayment.aspx"
                         }
                    ]
                },
                {
                    "name": "Operation",
                    "class": "btn-op",
                    "menu_link": [
                        {
                            "name": "Claim Daily Confirmation",
                            "id": "fm_claimconfirm",
                            "link": "fm_claimconfirm.aspx"
                        },
                        {
                            "name": "Claim Daily Business Agreement",
                            "id": "fm_claimconfirmba.aspx",
                            "link": "fm_claimconfirmba.aspx"
                        },
                        {
                            "name": "Claim Daily Preview",
                            "id": "fm_claimconfirmlist",
                            "link": "fm_claimconfirmlist.aspx"
                        },
                        {
                            "name": "Claim Entry",
                            "id": "fm_claimEntry",
                            "link": "fm_claimEntry.aspx"
                        },
                        {
                            "name": "Claim Generated",
                            "id": "fm_claimgenerate",
                            "link": "fm_claimgenerate.aspx"
                        },
                        {
                            "name": "Claim List",
                            "id": "fm_claimList",
                            "link": "fm_claimList.aspx"
                        }
                    ]
                },
                {
                    "name": "Report",
                    "class": "btn-rpt",
                    "menu_link": [
                        {
                            "name": "Claim Monthly Report",
                            "id": "fm_claimReport",
                            "link": "fm_claimReport.aspx"
                        },
                        {
                            "name": "Claim (A&P) Report",
                            "id": "fm_claimsumm",
                            "link": "fm_claimsumm.aspx"
                        },
                        {
                            "name": "Raw Data Proposal",
                            "id": "fm_datarowproposal",
                            "link": "fm_datarowproposal.aspx"
                        },
                        {
                            "name": "Claim Expense Report",
                            "id": "fm_claimexpenserep",
                            "link": "fm_claimexpenserep.aspx"
                        }
                    ]
                }
            ]
        }
    },
    {
        "name": "Logistic",
        "id": "logistic-btn",
        "menu": {
            "child_menu": [
                {
                    "name": "Master",
                    "class": "btn-master",
                    "menu_link": [
                        {
                            "name": "Warehouse",
                            "id": "fm_mstwarehouselist",
                            "link": "fm_mstwarehouselist.aspx"
                        },
                        {
                            "name": "Stock Opname Scheduling",
                            "id": "fm_sopschedule",
                            "link": "fm_sopschedule.aspx"
                        }
                    ]
                },
                {
                    "name": "Approval",
                    "class": "btn-approval",
                    "menu_link": [
                        {
                            "name": "New Retur From Branch",
                            "id": "fm_appreturho",
                            "link": "fm_appreturho.aspx"
                        },
                        {
                            "name": "DO Branch Modif Appval",
                            "id": "fm_appdomodif",
                            "link": "fm_appdomodif.aspx"
                        }
                    ]
                },
                {
                    "name": "Transaction HO",
                    "class": "btn-transaction",
                    "menu_link": [
                        {
                            "name": "Good Receipt HO",
                            "id": "frmGoodReceiptHO",
                            "link": "frmGoodReceiptHO.aspx"
                        },
                        {
                            "name": "Delivery Order",
                            "id": "fm_dolist",
                            "link": "fm_dolist.aspx"
                        },
                        {
                            "name": "Loading Main WH",
                            "id": "fm_doloading",
                            "link": "fm_doloading.aspx"
                        },
                        {
                            "name": "Receipt Return",
                            "id": "frmreceiptreturho",
                            "link": "frmreceiptreturho.aspx"
                        },
                        {
                            "name": "Invoice Copy Receipt",
                            "id": "fm_invreceived",
                            "link": "fm_invreceived.aspx"
                        }
                    ]
                },
                {
                    "name": "Transaction Branch",
                    "class": "btn-transactionb",
                    "menu_link": [
                        {
                            "name": "PO Branch",
                            "id": "fm_po",
                            "link": "fm_po.aspx"
                        },
                        {
                            "name": "Good Receipt",
                            "id": "fm_goodreceiptlist",
                            "link": "fm_goodreceiptlist.aspx"
                        },
                        {
                            "name": "Return To HO",
                            "id": "fm_returhoentry",
                            "link": "fm_returhoentry.aspx"
                        },
                        {
                            "name": "Stock Opname",
                            "id": "fm_stockopnameentry",
                            "link": "fm_stockopnameentry.aspx"
                        }, {
                            "name": "Internal Transfer",
                            "id": "fm_internaltransfer",
                            "link": "fm_internaltransfer.aspx"
                        },
                        {
                            "name": "Auto PO",
                            "id": "fm_autopolist",
                            "link": "fm_autopolist.aspx"
                        },
                        {
                            "name": "Stock Adjustment",
                            "id": "frmTranStockAdj",
                            "link": "frmTranStockAdj.aspx"
                        },
                        {
                            "name": "Trx destroy, add and loss",
                            "id": "frmTranStock",
                            "link": "frmTranStock.aspx"
                        },
                        {
                            "name": "Delivery Man Record Report",
                            "id": "fm_dsr",
                            "link": "fm_dsr.aspx"
                        },
                        {
                            "name": "Stock OP Schedule",
                            "id": "fm_stockopschedule",
                            "link": "fm_stockopschedule.aspx"
                        }
                    ]
                },
                {
                    "name": "Report",
                    "class": "btn-report",
                    "menu_link": [
                        {
                            "name": "Print Stock Report",
                            "id": "frmPrnStock",
                            "link": "frmPrnStock.aspx"
                        },
                        {
                            "name": "Print Stock In by Reference Report",
                            "id": "frmPrngoodreceiptbyinv",
                            "link": "frmPrngoodreceiptbyinv.aspx"
                        },
                        {
                            "name": "Print Sales Target VS Actual Chart",
                            "id": "frmprnSalestargetVsActualchart",
                            "link": "frmprnSalestargetVsActualchart.aspx"
                        },
                        {
                            "name": "Print Sales by Monthly",
                            "id": "frmprnSalesbymonthly",
                            "link": "frmprnSalesbymonthly.aspx"
                        },
                        {
                            "name": "Print Sales Activity by Customer",
                            "id": "frmprnSalesActivityByCustomer",
                            "link": "frmprnSalesActivityByCustomer.aspx"
                        },
                        {
                            "name": "Print Item Conversion",
                            "id": "fm_itemconversion",
                            "link": "fm_itemconversion.aspx"
                        },
                        {
                            "name": "Print Stock monitoring",
                            "id": "fm_stockmonitoring",
                            "link": "fm_stockmonitoring.aspx"
                        },
                        {
                            "name": "Print Closing Stock Jaret Monthly",
                            "id": "fm_rptclosingstockjaretmonthly",
                            "link": "fm_rptclosingstockjaretmonthly.aspx"
                        },
                        {
                            "name": "Daily invoice Report",
                            "id": "fm_dailyinvoices",
                            "link": "fm_dailyinvoices.aspx"
                        },
                        {
                            "name": "Print Jaret Report",
                            "id": "fm_jared",
                            "link": "fm_jared.aspx"
                        }
                    ]
                }
            ]
        }
    },
    {
        "name": "Sales Marketing",
        "id": "sales-marketing",
        "menu": {
            "child_menu": [
                {
                    "name": "Master",
                    "class": "btn-master",
                    "menu_link": [
                        {
                            "name": "Mst Customer",
                            "id": "fm_mstcustomerlist",
                            "link": "fm_mstcustomerlist.aspx"
                        },
                        {
                            "name": "Mst Salespoint",
                            "id": "fm_mstsalespointlist",
                            "link": "fm_mstsalespointlist.aspx"
                        },
                        {
                            "name": "Mst RPS",
                            "id": "fm_mstrps",
                            "link": "fm_mstrps.aspx"
                        },
                        {
                            "name": "Mst Reason",
                            "id": "fm_mstreasonlist",
                            "link": "fm_mstreasonlist.aspx"
                        },
                        {
                            "name": "Mst Location",
                            "id": "fm_mstloc",
                            "link": "fm_mstloc.aspx"
                        },
                        {
                            "name": "Mst Sales Target SP",
                            "id": "fm_salestargetsalespoint",
                            "link": "fm_salestargetsalespoint.aspx"
                        },
                        {
                            "name": "Mst Outlet Target",
                            "id": "fm_outlettarget",
                            "link": "fm_outlettarget.aspx"
                        },
                        {
                            "name": "Mst Salesman Coll Target",
                            "id": "fm_salesmantargetcollection",
                            "link": "fm_salesmantargetcollection.aspx"
                        },
                        {
                            "name": "Mst Doc Rejected",
                            "id": "fm_docreject",
                            "link": "fm_docreject.aspx"
                        },
                        {
                            "name": "Mst Doc Cust Category",
                            "id": "frmcustomercategory_doc",
                            "link": "frmcustomercategory_doc.aspx"
                        },
                        {
                            "name": "Mst Item",
                            "id": "fm_mslist2",
                            "link": "fm_mslist2.aspx"
                        },
                        {
                            "name": "Salesman Group Mapping",
                            "id": "fm_SalesmanGroupMapping",
                            "link": "fm_SalesmanGroupMapping.aspx"
                        }
                    ]
                },
                {
                    "name": "Request",
                    "class": "btn-request",
                    "menu_link": [
                        {
                            "name": "Request New Item",
                            "id": "fm_reqitem",
                            "link": "fm_reqitem.aspx"
                        },
                        {
                            "name": "Request New Customer",
                            "id": "fm_mstcustomerentry",
                            "link": "fm_mstcustomerentry.aspx"
                        }
                    ]
                },
                {
                    "name": "Approval",
                    "class": "btn-approval",
                    "menu_link": [
                        {
                            "name": "Approval New Cust",
                            "id": "fm_appcustomer",
                            "link": "fm_appcustomer.aspx"
                        },
                        {
                            "name": "Approval New Item",
                            "id": "fm_appitem",
                            "link": "fm_appitem.aspx"
                        },
                        {
                            "name": "Verify Item Request",
                            "id": "fm_reqitemlist",
                            "link": "fm_reqitemlist.aspx"
                        },
                        {
                            "name": "Verify Cashout Request",
                            "id": "fm_cashoutlist",
                            "link": "fm_cashoutlist.aspx"
                        },
                        {
                            "name": "Sales Return Approval",
                            "id": "fm_appsalesreturn",
                            "link": "fm_appsalesreturn.aspx"
                        }
                    ]
                },
                {
                    "name": "Transaction Branch",
                    "class": "btn-transactionb",
                    "menu_link": [
                        {
                            "name": "Take Order",
                            "id": "fm_takeorderentry",
                            "link": "fm_takeorderentry.aspx"
                        },
                        {
                            "name": "Canvasser",
                            "id": "fm_canvasorder",
                            "link": "fm_canvasorder.aspx"
                        },
                        {
                            "name": "Customer Received",
                            "id": "fm_salesreceipt",
                            "link": "fm_salesreceipt.aspx"
                        },
                        {
                            "name": "Sales Returned",
                            "id": "fm_salesreturn",
                            "link": "fm_salesreturn.aspx"
                        },
                        {
                            "name": "Customer Transfer",
                            "id": "fm_customertransfer",
                            "link": "fm_customertransfer.aspx"
                        },
                        {
                            "name": "Promise Payment",
                            "id": "fm_paymentpromised2",
                            "link": "fm_paymentpromised2.aspx"
                        },
                        {
                            "name": "Return Full Invoice",
                            "id": "fm_returnfullinvoice",
                            "link": "fm_returnfullinvoice.aspx"
                        }
                    ]
                },
                {
                    "name": "Report",
                    "class": "btn-report",
                    "menu_link": [
                        {
                            "name": "List Cust By salesman",
                            "id": "frmprnlistcustbysalesman",
                            "link": "frmprnlistcustbysalesman.aspx"
                        },
                        {
                            "name": "Invoice Report Outlet by due date",
                            "id": "fm_reportbyoutletdate",
                            "link": "fm_reportbyoutletdate.aspx"
                        },
                        {
                            "name": "Sales report by date and salesman",
                            "id": "fm_salessummrybyproduct",
                            "link": "fm_salessummrybyproduct.aspx"
                        },
                        {
                            "name": "Daily invoice and voucher report by outlet",
                            "id": "fm_dailyinvrecpvouch",
                            "link": "fm_dailyinvrecpvouch.aspx"
                        },
                        {
                            "name": "Summary Taking Order",
                            "id": "fm_summtakingorderbysales",
                            "link": "fm_summtakingorderbysales.aspx"
                        },
                        {
                            "name": "Sales Of Salesman By Outlet",
                            "id": "fm_salesofsalesman",
                            "link": "fm_salesofsalesman.aspx"
                        },
                        {
                            "name": "Aging Performance By Branch",
                            "id": "fm_sumoutletperformance",
                            "link": "fm_sumoutletperformance.aspx"
                        },
                        {
                            "name": "Balance Performance",
                            "id": "fm_balanceperformance",
                            "link": "fm_balanceperformance.aspx"
                        },
                        {
                            "name": "Raw Data",
                            "id": "fm_datarow",
                            "link": "fm_datarow.aspx"
                        },
                        {
                            "name": "Due Date Report",
                            "id": "fm_Duedate",
                            "link": "fm_Duedate.aspx"
                        },
                        {
                            "name": "Collection Report",
                            "id": "fm_collection",
                            "link": "fm_collection.aspx"
                        },
                        {
                            "name": "Pending invoice Report",
                            "id": "fm_pedndinginvoices",
                            "link": "fm_pedndinginvoices.aspx"
                        },
                        {
                            "name": "Customer List Report",
                            "id": "fm_customerreport",
                            "link": "fm_customerreport.aspx"
                        },
                        {
                            "name": "Daily Salesman Report",
                            "id": "fm_dailysalesmanreport",
                            "link": "fm_dailysalesmanreport.aspx"
                        },
                        {
                            "name": "SALES CONTRACT",
                            "id": "fm_salescontract",
                            "link": "fm_salescontract.aspx"
                        }
                    ]
                }
            ]
        }
    },
    {
        "name": "HR",
        "id": "btn-HR",
        "menu": {
            "child_menu": [
                {
                    "name": "Master",
                    "class": "btn-master",
                    "menu_link": [
                        {
                            "name": "Employee",
                            "id": "fm_mstemplist",
                            "link": "fm_mstemplist.aspx"
                        },
                        {
                            "name": "Vehicle",
                            "id": "fm_mstvehiclelist",
                            "link": "fm_mstvehiclelist.aspx"
                        }
                    ]
                }
            ]
        }
    },
    {
        "name": "Tools",
        "id": "btn-tools",
        "menu": {
            "child_menu": [
                {
                    "name": "User Tools",
                    "class": "btn-utool",
                    "menu_link": [
                        {
                            "name": "Change Password",
                            "id": "ChangePassword",
                            "link": "ChangePassword.aspx"
                        },
                        {
                            "name": "Reprint Document",
                            "id": "fm_reprintDocument",
                            "link": "fm_reprintDocument.aspx"
                        },
                        {
                            "name": "Synchrounize",
                            "id": "fm_synctab",
                            "link": "fm_synctab.aspx"
                        },
                        {
                            "name": "Stock Confirmed",
                            "id": "fm_stockconfirm",
                            "link": "fm_stockconfirm.aspx"
                        },
                        {
                            "name": "Production Support",
                            "id": "fm_prodsupport",
                            "link": "fm_prodsupport.aspx"
                        },
                        {
                            "name": "Sanad Document",
                            "id": "BookingSanadHO",
                            "link": "BookingSanadHO.aspx"
                        },
                        {
                            "name": "Receipt Sanad Document",
                            "id": "BookingSanadBranch",
                            "link": "BookingSanadBranch.aspx"
                        },
                        {
                            "name": "Dummay Update",
                            "id": " BookingSanadBranchUpdate",
                            "link": " BookingSanadBranchUpdate.aspx"
                        },
                        {
                            "name": "Branch Data Sync",
                            "id": "SyncBranch",
                            "link": "SyncBranch.aspx"
                        },
                        {
                            "name": "Salesman Tracker",
                            "id": "branchspv/default",
                            "link": "branchspv/default.aspx"
                        }
                    ]
                }
            ]
        }
    }
    ];




    return menu;
}
