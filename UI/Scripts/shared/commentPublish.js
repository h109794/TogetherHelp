document.getElementById("publish").addEventListener("click", function () {
    if (document.cookie.indexOf("loginInfo") === -1) {
        if (confirm("登录用户才能发布评论，单击确定跳转到登录页面。")) {
            location.href = "/login";
        }
        return;
    }

    var commentContent = document.getElementById("comment-content");
    if (commentContent.value.match(/^\s*$/)) {
        alert("评论内容不能为空");
        commentContent.value = '';
        return;
    }

    var articleId = document.URL.slice(document.URL.lastIndexOf('/') + 1);
    var xhr = new XMLHttpRequest();
    xhr.open("POST", `/Shared/PublishComment?articleId=${articleId}`);
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.send(`commentContent=${commentContent.value}`);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === xhr.DONE) {
            if (xhr.status === 200) {
                // 把返回的HTML文档转换成Node节点
                var newComment = new DOMParser().parseFromString(xhr.responseText, 'text/html').body.childNodes[0];
                document.getElementById("comments").appendChild(newComment);
                document.getElementById("comment-count").textContent++;
                commentContent.value = '';
                // 绑定回复按钮显隐事件
                newComment.getElementsByClassName("main-comment")[0].addEventListener("mouseenter", function () {
                    this.querySelector(".fa.fa-reply.d-none").className = "fa fa-reply";
                });
                newComment.getElementsByClassName("main-comment")[0].addEventListener("mouseleave", function () {
                    this.querySelector(".fa.fa-reply").className += " d-none";
                });
                // 绑定赞踩按钮评价事件
                newComment.querySelector("[name=agree]").addEventListener("click", evaluate);
                newComment.querySelector("[name=disagree]").addEventListener("click", evaluate);
            } else {
                alert("评论发布失败");
            }
        }
    };
});
