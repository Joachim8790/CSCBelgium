$(document).ready(function () {
    $('.slider').slider();
    $('.slider').height(550);
    $('.slides').height(550);
    $(".button-collapse").sideNav();
    $('.button-collapse').sideNav({
        menuWidth: 300, // Default is 300
        edge: 'right', // Choose the horizontal origin
        closeOnClick: true, // Closes side-nav on <a> clicks, useful for Angular/Meteor
        draggable: true, // Choose whether you can drag to open on touch screens,
        onOpen: function(el) { /* Do Stuff*/ }, // A function to be called when sideNav is opened
      onClose: function(el) { /* Do Stuff*/ }, // A function to be called when sideNav is closed
    }
  );
   
    $('.tooltipped').tooltip({ delay: 50 });
    $('select').material_select();
    $('.datepicker').pickadate({
        selectMonths: true, // Creates a dropdown to control month
        selectYears: 250, // Creates a dropdown of 15 years to control year,
        today: 'Vandaag',
        clear: 'Wissen',
        close: 'Ok',
        closeOnSelect: false // Close upon selecting a date,
        ,
        
    });
    $('.data-errorEMAIL').attr("data-error","Voer een geldig emailadres in.")
    $('.data-required').attr("required", "Dit veld is verplicht.");
    $('.data-required').attr("aria-required", "true");
    //$('.materialize-textarea.data-required').attr("required", "Dit veld is verplicht.");
    //$('.materialize-textarea.data-required').attr("aria-required", "true");
    $('.materialboxed').materialbox();
    $('.nav-arrow-next').on("click", function () {
        $('.slider.custom').slider('next');

    });
      
    // Previous slide
    $('.nav-arrow-prev').on("click", function () {
        $('.slider.custom').slider('prev');

    });

    console.log($("table tbody tr").size());
    for (var i = 0; i < $("table tbody tr").size(); i++)
    {
        console.log("length: " + i);
        console.log($("#ddl" + i));
        
        
    }
    
    $(window).bind("load", positionCircles);
    $(window).bind("resize", positionCircles);
    $(window).bind("orientationchange", positionCircles);
   
    window.onload = function () {
        $('select').material_select();
    };
});
function positionCircles()
{
    var height = $("ul.ch-grid").height();
    console.log(height);
    $("#circles").css("margin-top", "-" + height/2 + "px");

}
