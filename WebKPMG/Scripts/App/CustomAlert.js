function CustomAlert(title, message, buttons) {
    $('#CustomAlert').find('.actions').html('');
    if (buttons == null || buttons.length == 0) {
        buttons = [];
        var okButton = $('<div>');
        okButton.addClass('ui positive right  icon button');
        okButton.html('okay');
        buttons.push(okButton);
    }
    buttons.forEach(function (button) {
        $('#CustomAlert').find('.actions').append(button);
    });

    $('#CustomAlert').find('.header').html(title);
    $('#CustomAlert').find('.description').html(message);
    $('#CustomAlert').modal('show');
}