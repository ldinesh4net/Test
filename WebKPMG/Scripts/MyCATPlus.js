//ajax Call standarise
function ajaxPost(url, data, success, error, anim) {
    anim = anim || '#loaderAnimDiv';
    if (error == null) {
        error = function (xhr, status, error) {
            console.log(xhr);
            console.log(status);
            console.log(error);

            getAjaxError(xhr, true);
        };
    }
    $.ajax({
        url: url,
        cache: false,
        type: 'post',
        dataType: 'json',
        data: data,
        beforeSend: function () {
            $(anim).show();
        },
        complete: function () {
            $(anim).hide();
        },
        success: success,
        error: error
    });
}
//ajax Call standarise
function ajaxGet(url, data, success, error, anim) {
    anim = anim || '#loaderAnimDiv';
    if (error == null) {
        error = function (xhr, status, error) {
            console.log(xhr);
            console.log(status);
            console.log(error);

            getAjaxError(xhr, true);
        };
    }
    $.ajax({
        url: url,
        cache: false,
        type: 'get',
        dataType: 'json',
        data: data,
        beforeSend: function () {
            $(anim).show();
        },
        complete: function () {
            $(anim).hide();
        },
        success: success,
        error: error
    });
}