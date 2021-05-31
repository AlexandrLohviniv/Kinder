/*Dropdown Menu*/
$('.dropdown').click(function () {
        $(this).attr('tabindex', 1).focus();
        $(this).toggleClass('active');
        $(this).find('.dropdown-menu').slideToggle(300);
    });
    $('.dropdown').focusout(function () {
        $(this).removeClass('active');
        $(this).find('.dropdown-menu').slideUp(300);
    });
    $('.dropdown .dropdown-menu li').click(function () {
        $(this).parents('.dropdown').find('span').text($(this).text());
        // var a = $(this).parents('.dropdown').find('span').text($(this).text());
        $(this).parents('.dropdown').find('input').attr('value', $(this).attr('id'));
        // var b = $(this).parents('.dropdown').find('input').attr('value', $(this).attr('id'));
    });
/*End Dropdown Menu*/


$('.dropdown-menu li').click(function () {
  var input = '<strong>' + $(this).parents('.dropdown').find('input').val() + '</strong>',
      msg = '<span class="msg">Hidden input value: ';
  $('.msg').html(msg + $(input).attr('value') + '</span>');
//   var a = $("#gender").attr('value');
});

$(function () {
    // идентификатор элемента (например: #datetimepicker1), для которого необходимо инициализировать виджет Bootstrap DateTimePicker
    $('#datetimepicker1').datetimepicker();
  });

 let checker = true;
    $(".choose").click(function() {
      if(checker == true) {
        checker = false;
      $(".container").show('slow');
    }
      else  {
        $(".container").hide('slow');
        checker = true;

      }
    });
$(".dss").click(function() {
  $(".container").hide('slow');
  checker = true;
  $(".choose").text("Edit your preferences")
});

$(".sign-up-btn").click(function () {
  $("#sign-up-lable").show('slow');
});
