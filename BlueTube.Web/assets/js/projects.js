function slideSwitch() {
    var $active = $('#slideshow IMG.active');
 
    if ( $active.length == 0 ) $active = $('#slideshow IMG:last');
 
    // use this to pull the images in the order they appear in the markup
    var $next =  $active.next().length ? $active.next()
        : $('#slideshow IMG:first');
 
    $active.addClass('last-active');
 
    $next.css({opacity: 0.0})
        .addClass('active')
        .animate({opacity: 1.0}, 1000, function() {
            $active.removeClass('active last-active');
        });
}

function slideCritique() {
    var $active = $('#quotes li.active');
 
    if ( $active.length == 0 ) $active = $('#quotes li:first');
 
    // use this to pull the images in the order they appear in the markup
    var $next =  $active.next().length ? $active.next()
        : $('#quotes li:first');
 
    $active.addClass('last-active');
 
    $next.css({opacity: 0.0})
        .addClass('active')
        .animate({opacity: 1.0}, 1000, function() {
            // $active.removeClass('active last-active');
        });
        
    $active.animate({opacity: 0.0}, 1000, function() {
            $active.removeClass('active last-active');
    });
}


//Scroll to
$(function() {
  $('a[href=#_overview]').click(function() {
    if (location.pathname.replace(/^\//,'') == this.pathname.replace(/^\//,'') && location.hostname == this.hostname) {
      var target = $(this.hash);
      target = target.length ? target : $('[name=' + this.hash.slice(1) +']');
      if (target.length) {
        $('html,body').animate({
          scrollTop: target.offset().top
        }, 1000);
        return false;
      }
    }
  });
});


//Annotations
			$(window).load(function() {
				$("#toAnnotate").annotateImage({
					editable: true,
					useAjax: false,
					notes: [
                                                {
                                                           "top": 286,
							   "left": 161,
							   "width": 52,
							   "height": 37,
							   "text": "Small people on the steps",
							   "id": "e69213d0-2eef-40fa-a04b-0ed998f9f1f5",
							   "editable": false },
							 
                                                         {
                                                           "top": 134,
							   "left": 179,
							   "width": 68,
							   "height": 74,
							   "text": "National Gallery Dome",
							   "id": "e7f44ac5-bcf2-412d-b440-6dbb8b19ffbe",
							   "editable": false } ]
				});
			});
