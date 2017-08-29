
chat.client.sendPrivateNotifi = function (formId, user, fullName, text) {
    var code = '';
    code = $('<li id="addNotif' +
        formId +
        '">' +
        '<a href="/Profile/Index/' +
        formId +
        '">' +
        '<div class="pull-left">' +
        '<img src="/Images/Uploads/' +
        user.AvatarURL +
        '" class="img-circle" alt="' +
        fullName +
        '">' +
        '</div>' +
        '<h4>' +
        fullName +
        '</h4>' +
        '<p>' +
        text +
        '</p>' +
        '</a>' +
        '</li>');
    $('#notif').prepend(code);
    var num = 0;
    if ($('#notifNum').text() != '')
        num = parseInt($('#notifNum').text()) + 1;
    else {
        num = 1;
    }
    $('#notifNum').text(num);
    $('#textNotifnum').text('Bạn có ' + num + ' thông báo chưa đọc');
}

$('#SeeIsRead').click(function () {
    chat.server.isReadNotif(AccountId);
    $('#notifNum').text('');
    $('#textNotifnum').text('Bạn có ' + 0 + ' thông báo chưa đọc');
});

chat.client.unSendNotifi = function (fromId, toId) {
    $("#addNotif" + fromId).remove();
    var num = 0;
    if ($('#notifNum').text() != '')
        num = parseInt($('#notifNum').text()) - 1;

    if (num == 0) {
        $('#notifNum').text('');
    } else
        $('#notifNum').text(num);
    $('#textNotifnum').text('Bạn có ' + num + ' thông báo');
}