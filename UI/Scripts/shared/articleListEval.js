var agreeBtn = document.getElementsByName("agree");
var disagreeBtn = document.getElementsByName("disagree");

for (let i = 0; i < agreeBtn.length; i++) {
    agreeBtn[i].addEventListener("click", evaluate);
}

for (let i = 0; i < disagreeBtn.length; i++) {
    disagreeBtn[i].addEventListener("click", evaluate);
}

function evaluate() {
    event.preventDefault();
    if (document.cookie.indexOf("loginInfo") === -1) {
        if (confirm("登录用户才能评价，单击确定跳转到登录页面。")) {
            location.href = "/login";
        }
        return;
    }

    var articleId = this.parentElement.parentElement.firstElementChild.value;
    var isAgree = (this.name === "agree") ? true : false;

    requestEvaluate(this, isAgree, articleId, true);
}
