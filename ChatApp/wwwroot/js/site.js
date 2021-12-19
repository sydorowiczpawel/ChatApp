(function () {
    let sendBtn = document.getElementById('send-btn');
    let chatMessage = document.getElementById('chat-message');
    let chatContainer = document.getElementById('chat-container');
    let userLoginVal = document.getElementById('user-login').innerText;
    
    let connection = new signalR.HubConnectionBuilder()
        .withUrl("/chathub")
        .configureLogging(signalR.LogLevel.Information)
        .build();
    
    connection.start().then(function() {
        console.log("connected");
        connection.invoke('SignedUser', {
            login: userLoginVal,
        });
    });

    connection.on("ChatMessageRecieved", (obj) => {
        let message = obj.message;
        let createdOn = obj.forttedCreatedOn;
        let login = obj.login;

        $(chatContainer).prepend('<li><span class="text-primary">' +  login + ':</span> '+ message + '</li>');
    });

    connection.on("SignedUser", (obj) => {
        let createdOn = obj.formatedCreatedOn;
        let login = obj.login;

        $(chatContainer).prepend('<li class="text-warning">Użytkownik ' + login + ' dołączył do konwersacji</li>');
    });

    $(sendBtn).click(function () {
        let message = $(chatMessage).val();
        connection.invoke('SendMessage', {
            login: userLoginVal,
            message: message
        });

        $(chatMessage).val('');
    })
})();