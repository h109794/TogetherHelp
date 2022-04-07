document.getElementById("agree").addEventListener("click", evaluate);
document.getElementById("disagree").addEventListener("click", evaluate);

var commentAgreeBtn = document.getElementsByName("agree");
var commentDisagreeBtn = document.getElementsByName("disagree");

for (let i = 0; i < commentAgreeBtn.length; i++) {
    commentAgreeBtn[i].addEventListener("click", evaluate);
}

for (let i = 0; i < commentDisagreeBtn.length; i++) {
    commentDisagreeBtn[i].addEventListener("click", evaluate);
}

function evaluate() {
    event.preventDefault();
    if (document.cookie.indexOf("loginInfo") === -1) {
        if (confirm("登录用户才能评价，单击确定跳转到登录页面。")) {
            location.href = "/login";
        }
        return;
    }

    var isAgree = (this.id === "agree" || this.name === "agree") ? true : false;
    var articleId = (this.id !== '') ?
        document.URL.slice(document.URL.lastIndexOf('/') + 1) :
        this.parentElement.parentElement.firstElementChild.value;
    var isArticle = (this.id !== '') ? true : false;

    requestEvaluate(this, isAgree, articleId, isArticle);
}
