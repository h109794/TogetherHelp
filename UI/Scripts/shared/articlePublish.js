document.getElementsByTagName('form')[0].addEventListener("submit", function () {
    var title = document.getElementById("Title");
    var body = document.getElementById("Body");
    var keywords = document.getElementById("show-keyword").children;
    var keywordsReceiver = document.getElementById("KeywordsReceiver");
    var validationElement = document.getElementsByClassName("field-validation-error");
    var isValidated = true;

    // 将关键字拼接成字符串传递至后台解析
    for (let i = 0; i < keywords.length; i++) {
        keywordsReceiver.value += keywords[i].textContent;
    }
    // 再次提交时消除验证信息
    for (let i = 0; i < validationElement.length; i++) {
        validationElement[i].textContent = '';
        validationElement[i].className = "field-validation-valid";
    }

    if (title.value === '') {
        title.nextElementSibling.textContent = "* 标题不能为空";
        title.nextElementSibling.className = "field-validation-error";
        isValidated = false;
    }
    if (body.value === '') {
        body.nextElementSibling.textContent = "* 正文不能为空";
        body.nextElementSibling.className = "field-validation-error";
        isValidated = false;
    }
    if (keywordsReceiver.value === '') {
        keywordsReceiver.nextElementSibling.textContent = "* 关键字不能为空";
        keywordsReceiver.nextElementSibling.className = "field-validation-error";
        isValidated = false;
    }
    if (!isValidated) {
        keywordsReceiver.value = '';
        event.preventDefault();
    }
});
