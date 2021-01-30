var newImg = $('.list');
var mainImg = $('#mainImg');

$('ul').click(function(event){
  var target = $(event.target);
  var name = target.attr('alt');
  
  mainImg.attr('src', target.attr('src')).hide().fadeIn(1000);
  $('#name').show().text(target.attr('alt'));
  if (target.is(newImg)) {
   target.animate({
    opacity: 1,
    borderWidth : 3,
    }, 500) } newImg.css({
    opacity: 0.5,
    borderWidth : 1,
    })

})