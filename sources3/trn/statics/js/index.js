$(document).ready(function(){
	var wh = $( window ).height();
	var mh = wh - 183;
	// $('.sidebar').click(function(){
	// 	if($('.sidebar').hasClass('open')){
	// 		$('.sidebar').removeClass('open');
	// 	}
	// 	else{
	// 		$('.sidebar').addClass('open');
	// 	}
	// })




	$('.btn-nav').click(function(){
		$('.sidebar').addClass('open');
	})
	$('.menu').click(function(){
		$('.sidebar').removeClass('open');
	})

	// header2 open hamurger
	$('.icon-hamburger').click(function(){
		$('.menu-burger').addClass('open');
		$('.nav-bl').addClass('open');
	})
	$('.close-burger').click(function(){
		$('.menu-burger').removeClass('open');
		$('.nav-bl').removeClass('open');
	})
	$('.nav-bl').click(function(){
		$('.menu-burger').removeClass('open');
		$('.nav-bl').removeClass('open');
	})


	//menu
	$('.childs').hide();
	$('.menu .dropdown .dropselect').click(function(){
		if($(this).parent().hasClass('open')) {
		  	$(this).parent().removeClass('open');
		  	$(this).parent().find(".childs").slideUp( "fast" );
		  }
		else{
			$(this).parent().addClass('open');
			$(this).parent().find(".childs").slideDown( "fast" );
			$(this).parent().find(".dropdown-child").removeClass('open');
			$(this).parent().find(".dropdown-child .childs").hide();

		  } 
	})

	$('.icon-menu-left > a').click(function(){
		$('.sidebar').addClass('opened');
	})
	$('.icon-close > a').click(function(){
		$('.sidebar').removeClass('opened');
	})
});