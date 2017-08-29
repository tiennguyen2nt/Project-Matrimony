$(function () {
    $.connection.hub.start().done(function () {
        chat.server.connect(AccountId);
    });
});