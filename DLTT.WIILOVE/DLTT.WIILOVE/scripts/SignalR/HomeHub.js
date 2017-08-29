$(function() {
    chat.client.onConnected = function(id, accId, listUser) {
        $('#hdId').val(accId);
        for (var i = 0; i < listUser.length; i++) {
            AddUser(accId, listUser[i]);
        }
    }



    chat.client.onNewUserConnected = function (id, user) {

    }

    $.connection.hub.start().done(function () {
        
        chat.server.connect(AccountId);
    });
});