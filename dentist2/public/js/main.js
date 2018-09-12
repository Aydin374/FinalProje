$(document).ready(function($) {
  "use strict";

  // Back-to-top
  var btt = $('.back-to-top');

  btt.on('click', function() {
    $('html, body').animate({
          scrollTop: 0
    }, 600);
  });

  $(window).on('scroll',function() {
    var self = $(this),
    height = self.height(),
    top = self.scrollTop();

    if (top > height) {
          if (!btt.is(':visible')) {
                btt.show();
          }
    }   else {
          btt.hide();
    }
  });

	// Owl-Carousel
	$("#owl-demo").owlCarousel({
    navigation : true, // Show next and prev buttons
    slideSpeed : 900,
    paginationSpeed : 800,
    singleItem:true,
    autoPlay : 4200,
    navigationText: ["<i class='fa fa-long-arrow-left'></i>","<i class='fa fa-long-arrow-right'></i>"]
  });

  $("#owl-demo1").owlCarousel({
    pagination : true,
    paginationNumbers: false,
    // autoPlay: 4000, //Set AutoPlay to 4 seconds
    paginationSpeed : 800,
    items : 2,
    itemsDesktop : [1199,2],
    itemsTablet: [768,1],
    itemsMobile : [375,1]
  });

  $("#owl-demo2").owlCarousel({
    // navigation : true,
    slideSpeed : 900,
    paginationSpeed : 800,
    singleItem:true,
    // autoPlay : 4200,
  });

  // Fancybox
  $('[data-fancybox]').fancybox({
  });

  // counterUp
  $(".counterUp").counterUp({
     delay: 40,
     time: 8000,
  });

  //Anchor Tag
  $(document).on('click', 'a[href^="#"]', function (event) {
    event.preventDefault();

    $('html, body').animate({
        scrollTop: $($.attr(this, 'href')).offset().top
    }, 800);
  });

});