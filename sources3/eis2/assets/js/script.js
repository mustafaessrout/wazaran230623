
$(window).on('load', function(){
	// setLoader('demo-icon icon-spin6');
});

$(document).ready(function(){

	$.validate();

	var h = $(window).height() - 40;
	$('.content ').css("min-height",h);
	// header fixed-top scrolled
	scrolled($('body').scrollTop());

	$(document).scroll(function(){
		var top = $(this).scrollTop();
		scrolled(top);
	});

	deleteLoader();

  $('[data-toggle="popover"]').popover();

	$('[data-provide="datepicker"]').each(function() {
		$(this).datepicker();
	});

  $('[data-toggle="tooltip"]').tooltip();

	$('[type="checkbox"]').each(function() {
		var id = $(this).attr('id');
		$(this).after("<label class='cbx' for='"+id+"'></label>");
	});
	$('[type="radio"]').each(function() {
		var id = $(this).attr('id');
		$(this).after("<label class='cbx' for='"+id+"'></label>");
	});

	$('.ellapsisjs').each(function(){
		var long = $(this).attr('long-ellapsis');
		var tx = $(this).text();
		if(tx.length > long){
			$(this).text(tx.substr(0, tx.lastIndexOf(' ', long-4)) + '...');
		}
	})


	$('.hamburger').click(function () {
		if($(".body-class").hasClass('active')){
			$(".body-class").removeClass('active');
		}else{
			$(".body-class").addClass('active');
		}
	});

	$(".dropdown-hover-toogler").click(function() {

		if($(this).hasClass('active')){
			$(this).removeClass('active');
			if($(this).parent().find('.dropdown-hover-toogler').hasClass('active')){
				 $(this).parent().find('.dropdown-hover-toogler').removeClass("active");
			 }
		}else{
			$(this).addClass('ex');
			$(this).addClass('active');
			$(this).parent().parent().parent().parent().find('.dropdown-hover-toogler').each(function(){
				if(!$(this).hasClass('ex')){
					$(this).removeClass('active');
				}
			});
			$(this).removeClass('ex');
		}
		//  if($(this).parent().find('.dropdown-hover-toogler').hasClass('active')){
		// 	 $(this).parent().find('.dropdown-hover-toogler').removeClass("active");
		//  }
	});

	// dropdown main-menu
	$('.main-menu .dropdown').click(function(e) {
		e.stopPropagation();

	})
	$('.dropdown > .dropdown-toggle').click(function(e) {
		$(this).parent().addClass('ini');
		$(' .dropdown').each(function() {
			if(!$(this).hasClass('ini')){
				$(this).removeClass('show').find(".dropdown-menu").removeClass('show');
			}
		});
		$(this).parent().removeClass('ini');
		if($(this).parent().hasClass('show')){
			$(this).parent().removeClass('show').find(".dropdown-menu").removeClass('show');
		}else{
			$(this).parent().addClass('show').find(".dropdown-menu").addClass('show');
		}

		$(this).parent().find('.dropdown-hover-toogler').removeClass('active');
		e.stopPropagation();
	})

	$('.dropdown .dropdown-menu').click(function(e){
			e.stopPropagation();
	})

	$('body').click(function(){
		$('.dropdown').each(function() {
			$(this).removeClass('show').find(".dropdown-menu").removeClass('show');
		});
	});

	$('.dropdown-child > .dropdown-child-toogler').click(function() {
		$(this).addClass('ex');
		$(this).parent().parent().find('.dropdown-child-toogler').each(function(){
			if(!$(this).hasClass('ex')){
				$(this).removeClass('open');
				$(this).parent().find('.dropdown-child-list').removeClass('show');
			}
		});
		$(this).removeClass('ex');

		if($(this).hasClass('open')){
			$(this).removeClass('open');
			$(this).parent().find('.dropdown-child-list').removeClass('show');
		}else{
			$(this).addClass('open');
			$(this).parent().find('.dropdown-child-list').addClass('show');
		}

	});

	$('.nav-item.search .nav-link').click(function() {
		$('.search-inpt').focus();
	});

});


function scrolled(top) {
	console.log(top);
	if(top > 80){
		$('.header.fixed-top').addClass('scrolled');
	}else{
		$('.header.fixed-top').removeClass('scrolled');
	}
}

function setLoader(icon){
	var html = '<div class="loaderfull"><i class="spinner animate-spin '+icon+'""></i></div>';
	$('.loading-process').append(html);
	$('.loaderfull').css({"position":"fixed",
												"background-color":"rgba(0,0,0,.3)",
												"top":"0","bottom":"0","left":"0",
												"right":"0",
												"z-index": "9999",
												"display": "flex",
												"justify-content": "center",
		    								"align-items": "center"});
  $('.spinner').css({"font-size":"30px",
											"color":"#fff"});
	$('html').css({"overflow":"hidden"});
}

function deleteLoader(){
	$('html').css({"overflow":"auto"});
	$(".loaderfull").hide();
	$(".loaderfull").remove();
}
