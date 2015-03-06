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
					editable: false,
					useAjax: false,
					notes: [
                                                {
                                                           "top": 286,
							   "left": 161,
							   "width": 30,
							   "height": 30,
							   "text": "A world of videos at your finger tips",
							   "id": "e69213d0-2eef-40fa-a04b-0ed998f9f1f5",
							   "editable": false },
							 
                                                         {
                                                           "top": 628,
							   "left": 297,
							   "width": 30,
							   "height": 30,
							   "text": "Simple, Easy to use controls",
							   "id": "e7f44ac5-bcf2-412d-b440-6dbb8b19ffbe",
							   "editable": false },
							   {
                                                           "top": 159,
							   "left": 829,
							   "width": 30,
							   "height": 30,
							   "text": "Intelligent search engine",
							   "id": "7pwy5h9q",
							   "editable": false },
							    {
                                                           "top": 159,
							   "left": 529,
							   "width": 30,
							   "height": 30,
							   "text": "Save and easily manage all your favorites",
							   "id": "0-yie8-9w9a",
							   "editable": false },
							   {
                                                           "top": 740,
							   "left": 870,
							   "width": 30,
							   "height": 30,
							   "text": "More watching, less searching. With carefully selected related videos",
							   "id": "5-ov29w",
							   "editable": false }]
				});
			});
