function log() {
    document.getElementById('results').innerText = '';

    Array.prototype.forEach.call(arguments, function (msg) {
        if (msg instanceof Error) {
            msg = "Error: " + msg.message;
        }
        else if (typeof msg !== 'string') {
            msg = JSON.stringify(msg, null, 2);
        }
        document.getElementById('results').innerHTML += msg + '\r\n';
    });
}

document.getElementById("login").addEventListener("click", login, false);
document.getElementById("users").addEventListener("click", users, false);
document.getElementById("bills").addEventListener("click", bills, false);
document.getElementById("userBills").addEventListener("click", userBills, false);
document.getElementById("logout").addEventListener("click", logout, false);

var config = {
    authority: "https://localhost:5001",
    client_id: "js",
    redirect_uri: "https://localhost:44388/callback.html",
    response_type: "code",
    scope:"openid profile trackerApi",
    post_logout_redirect_uri : "https://localhost:44388/index.html",
};
var mgr = new Oidc.UserManager(config);

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

mgr.getUser().then(function (user) {
    if (user) {
        log("User logged in", user.profile);
    }
    else {
        log("User not logged in");
    }
});

function login() {
    mgr.signinRedirect();
}

function users() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:44328/api/Users";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function bills() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:44328/api/Bills";

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            log(xhr.status, JSON.parse(xhr.responseText));
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}

function userBills() {
    mgr.getUser().then(function (user) {
        var url = "https://localhost:44328/api/Users/";
        var us = user.profile;

        url += "accountName:"+ us.name;

        var xhr = new XMLHttpRequest();
        xhr.open("GET", url);
        xhr.onload = function () {
            var userToFetch = JSON.parse(xhr.responseText);
            var url = "https://localhost:44328/api/Bills/user/userId:" + userToFetch.Id;

            var xhr2 = new XMLHttpRequest();
            xhr2.open("GET", url);
            xhr2.onload = function () {
                log(xhr2.status, JSON.parse(xhr2.responseText));
            }
            xhr2.setRequestHeader("Authorization", "Bearer " + user.access_token);
            xhr2.send();
        }
        xhr.setRequestHeader("Authorization", "Bearer " + user.access_token);
        xhr.send();
    });
}


function logout() {
    mgr.signoutRedirect();
}