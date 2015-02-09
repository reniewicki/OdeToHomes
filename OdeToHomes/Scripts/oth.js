$(function () {

    var ajaxFormSubmit = function () {
        var $form = $(this);

        var options = {
            url: $form.attr("action"),
            type: $form.attr("method"),
            data: $form.serialize()
        };

        $.ajax(options).done(function (data) {
            var $target = $($form.attr("data-oth-target"));
            $target.replaceWith(data)

        });

        return false;

    };


    $("form[data-oth-ajax='true']").submit(ajaxFormSubmit);

});

$(window).scroll(function (e) {
    var top = $(window).scrollTop();

    if (top > 100) {
        $("#floating-box").addClass("fixed");
    }
    else
        $("#floating-box").removeClass("fixed");
});