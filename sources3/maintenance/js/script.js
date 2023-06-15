$( document ).ready(function() {
	$('body').click(function(){

		setTimeout(function() {
		       $('.dropdown-menu-full').removeClass('active');
		   }, 800);
    	$('.menu-full').slideUp();

    	
		$(".nav-left").removeClass('active');
		$(".blck-bg").removeClass("active");
    	
    })


	$(".menu-full .dropdown-menu-full").each(function(){
		var i = 0;
		var wdth ;
		$(this).find(".item-menu-full").each(function(){
			i++;
		})
		if(i < 4){
			 wdth =100/i;
		}else{
			 wdth =25;
		}
		
		$(this).find(".item-menu-full").css("width",wdth+"%");
	});



	// action click
    $('.header-top .dropdown-hover').hover(function() {
	  $(this).find('.dropdown-menu').stop(true, true).delay(200).show();
	}, function() {
		$(this).find('.dropdown-menu').stop(true, true).delay(400).hide(300);
	});

	$('.header-top-line-h .dropdown-hover').hover(function() {
	  $(this).find('.dropdown-menu').stop(true, true).delay(200).show();
	}, function() {
		$(this).find('.dropdown-menu').stop(true, true).delay(400).hide(300);
	});

    $('.dropdown-full').click(function(e){
    	e.stopPropagation();
    	var data = $(this).attr('data-dropdown');
    	if($(data).hasClass('active')){
    		$(this).removeClass('active');

			$('.menu-full').slideUp();
			setTimeout(function() {
		       $(data).removeClass('active');
		   }, 800);
    	}else{
    		$(".dropdown-full").removeClass('active');
    		$(this).addClass('active');
    		$('.dropdown-menu-full').removeClass('active');
    		$(data).addClass('active');
    		$('.menu-full').hide().slideDown("fast");
    	}
    });


    $('.menu-full').click(function(e){
    	e.stopPropagation();
    })
	
	$('.menu-closed').click(function(){
		$('.menu-full').removeClass("show");
		setTimeout(function() {
		       $('.dropdown-menu-full').removeClass('active');
		   }, 800);
    	$('.menu-full').slideUp();
    })


	 // responsive
    var docW = $(document).width();
    var res;
    
    if(docW < 769){
    	res = "tablet";
    }else if(docW < 427){
    	res = "lmob";
    }else if(docW < 377){
    	res = "mmob";
    }else if(docW < 322){
    	res = "smob";
    }else{
    	res = "computer";
    }


    $(".header-top .nav-menu-left a").click(function(e){
    	e.stopPropagation();
    	$(".nav-left").addClass("active");
    	$(".blck-bg").addClass("active");
    })

    $(".nav-left").click(function(e){
    	e.stopPropagation();
    })

    var top1 = $(".nv-right").html();
    var top2 = $('.header-bottom .contnt').html();
    // console.log(top1);

    $('.nav-left .nav-content').append(top1).find(".navbar-nav").removeClass("navbar-right");
     $('.nav-left .nav-content').append(top2).find(".dropdown-full").removeClass("dropdown-full").addClass("dropdown-full-fl");

     
    $('.dropdown-full-fl').click(function(e){
    	var data = $(this).attr('data-dropdown');
    	if($(data).hasClass('active')){
    		$(this).removeClass('active');

			$('.menu-full').removeClass("show");
			setTimeout(function() {
		       $(data).removeClass('active');
		   }, 800);
    	}else{
    		$(".dropdown-full").removeClass('active');
    		$(this).addClass('active');
    		$('.menu-full').addClass("show");
    		$('.dropdown-menu-full').removeClass('active');
    		$(data).addClass('active');
    		
    	}
    });


    $('.nav-left .dropdown-menu').closest("li").addClass('dropdown-icon');

 	$('.nav-left .dropdown-hover > a').click(function() {
 		if($(this).closest(".dropdown-hover").hasClass("active")){
 			$(this).closest(".dropdown-hover").removeClass("active");
 			$(this).closest(".dropdown-hover").find("ul").slideUp();
 		}else{
 			$('.nav-left .dropdown-hover').removeClass("active");
 			$('.nav-left .dropdown-hover').find("ul").slideUp();
 			$(this).closest(".dropdown-hover").addClass('active');
 			$(this).closest(".dropdown-hover").find("ul").slideDown();
 		}
	});

    // in tablet
    if(res == "tab"){

    }

});