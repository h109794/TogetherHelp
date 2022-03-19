var emailAddressBox = document.getElementById("EmailAddress");
var verificationBox = document.getElementById("VerificationCode");
var sendButton = document.getElementById("send");
var emailIcon = sendButton.getElementsByClassName("fa fa-envelope-o")[0];
var bindingButton = document.getElementById("bind");
var changeButton = document.getElementById("change");
var bindingDiv = document.getElementById("binding-div");
var countDown = 0;

if (emailAddressBox.disabled === true) {
    sendButton.disabled = true;
    bindingButton.disabled = true;
}

// 更换邮箱，开放绑定框
if (changeButton !== null) {
    changeButton.addEventListener("click", function () {
        this.className = "d-none";
        bindingDiv.className = "form-group";
        emailAddressBox.disabled = false;
        emailAddressBox.className = "form-control";
        emailAddressBox.value = '';
    });
}

// 验证邮箱格式
var regexp = /^[a-zA-Z0-9]+([_\-.]?[a-zA-Z0-9]+)+@[a-zA-Z0-9]+(\.[a-zA-Z0-9]+)+$/;
emailAddressBox.addEventListener("keyup", function () {
    // 发送倒计时禁用输入框
    if (countDown > 0) {
        return;
    }
    if (regexp.test(emailAddressBox.value)) {
        sendButton.disabled = false;
    } else {
        sendButton.disabled = true;
    }
});
verificationBox.addEventListener("keyup", function () {
    if (verificationBox.value.length === 6) {
        bindingButton.disabled = false;
    } else {
        bindingButton.disabled = true;
    }
});

// 发送验证码
sendButton.addEventListener("click", function () {
    if (emailAddressBox.value === '') {
        alert("请先填写邮箱地址。");
        return;
    }
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Email/Send");
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.send(`emailAddress=${emailAddressBox.value}`);
    // 请求发送后禁用按钮，防止多次请求
    sendButton.disabled = true;
    xhr.onreadystatechange = function () {
        if (xhr.readyState === xhr.DONE) {
            if (xhr.status === 200 && xhr.response === 'false') {
                alert("该邮箱已注册。");
                sendButton.disabled = false;
            } else if (xhr.status !== 200) {
                alert("邮件发送失败，请检查邮箱地址并尝试再次发送。");
                sendButton.disabled = false;
            } else {
                sendBtnCountDown();
            }
        }
    }
});

function sendBtnCountDown() {
    sendButton.disabled = true;
    countDown = 59;

    var sendBtnText = document.getElementById("send-btn-text");
    var timerId = setInterval(function () {
        emailIcon.className = "fa fa-envelope-o d-none";
        sendBtnText.textContent = `${countDown--} 秒后发送`;
        if (countDown < 0) {
            clearInterval(timerId);
            sendButton.disabled = false;
            sendBtnText.textContent = "发送";
            emailIcon.className = "fa fa-envelope-o";
        }
    }, 1000);
}
