// 回复按钮显示
var mainComments = document.getElementsByClassName("main-comment");
var replyComments = document.getElementsByClassName("reply-comment");

for (let i = 0; i < mainComments.length; i++) {
    mainComments[i].addEventListener("mouseenter", function () {
        this.getElementsByClassName("fa fa-reply d-none")[0].className = "fa fa-reply";
    });
    mainComments[i].addEventListener("mouseleave", function () {
        this.getElementsByClassName("fa fa-reply")[0].className = "fa fa-reply d-none";
    });
}

for (let i = 0; i < replyComments.length; i++) {
    replyComments[i].addEventListener("mouseenter", function () {
        this.getElementsByClassName("fa fa-reply d-none")[0].className = "fa fa-reply";
    });
    replyComments[i].addEventListener("mouseleave", function () {
        this.getElementsByClassName("fa fa-reply")[0].className = "fa fa-reply d-none";
    });
}

// 回复功能
document.getElementById("comments").addEventListener("click", function (e) {
    // 防止点到回复按钮外层a标签回到页面顶部
    if (e.target.firstChild !== null && e.target.firstChild.className === "fa fa-reply") {
        e.preventDefault();
        return;
    }

    if (e.target.className === "fa fa-reply") {
        e.preventDefault();
        if (document.cookie.indexOf("loginInfo") === -1) {
            if (confirm("登录用户才能发布评论，单击确定跳转到登录页面。")) {
                location.href = "/login";
            }
            return;
        }
        // 改变回复按钮名字，获取外层div标签
        var replyBoxButton = e.target;
        var replyBoxDiv = replyBoxButton.parentElement.parentElement;
        replyBoxButton.textContent = " 取消回复";
        // 再次点击折叠回复框
        if (replyBoxDiv.lastChild.className === "input-group input-group-sm mt-1 mb-3 pl-3") {
            replyBoxDiv.lastChild.remove();
            replyBoxButton.textContent = " 回复";
            return;
        }

        // 生成回复框
        var replyBox = document.createElement("div");
        var replyInputBox = document.createElement("input");
        var replyButtonDiv = document.createElement("div");
        var replyButton = document.createElement("button");
        var articleId = document.URL.slice(document.URL.lastIndexOf('/') + 1);
        var replyUsername = replyBoxDiv.parentElement.getElementsByTagName('a')[0].textContent;
        var replyCommentId = replyBoxDiv.parentElement.firstElementChild.value;
        var replyMainCommentId = (replyBoxDiv.parentElement.className !== "reply-comment ml-3") ?
            replyCommentId : replyBoxDiv.parentElement.parentElement.previousElementSibling.firstElementChild.value;

        replyBox.className = "input-group input-group-sm mt-1 mb-3 pl-3";
        replyInputBox.className = "form-control";
        replyInputBox.placeholder = `回复 ${replyUsername}`;
        replyButtonDiv.className = "input-group-append";
        replyButton.className = "btn btn-outline-secondary";
        replyButton.textContent = "回复";

        replyBox.appendChild(replyInputBox);
        replyBox.appendChild(replyButtonDiv);
        replyButtonDiv.appendChild(replyButton);
        replyBoxDiv.appendChild(replyBox);

        // 提交回复内容
        replyButton.addEventListener("click", function () {
            if (replyInputBox.value.match(/^\s*$/)) {
                alert("回复内容不能为空");
                replyInputBox.value = '';
                return;
            }
            var xhr = new XMLHttpRequest();
            xhr.open("POST", `/Shared/PublishComment?articleId=${articleId}`);
            xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
            xhr.send(`commentContent=${replyInputBox.value}&replyUsername=${replyUsername}` +
                `&replyMainCommentId=${replyMainCommentId}&replyCommentId=${replyCommentId}`);
            xhr.onreadystatechange = function () {
                if (xhr.readyState === xhr.DONE) {
                    if (xhr.status === 200) {
                        // 回复完成收起评论框
                        replyBoxDiv.lastChild.remove();
                        replyBoxButton.textContent = " 回复";
                        replyBoxButton.className += " d-none";
                        // 把返回的HTML文档转换成Node节点
                        var newReplyComment = new DOMParser().parseFromString(xhr.responseText, 'text/html').body.childNodes[0];
                        if (replyBoxDiv.parentElement.className === "main-comment") {
                            replyBoxDiv.parentElement.parentElement.getElementsByClassName("mt-3 mr-0 mb-0 ml-3")[0].appendChild(newReplyComment);
                        } else {
                            replyBoxDiv.parentElement.parentElement.parentElement.getElementsByClassName("mt-3 mr-0 mb-0 ml-3")[0].appendChild(newReplyComment);
                        }
                        document.getElementById("comment-count").textContent++;
                        // 绑定回复按钮显隐事件
                        newReplyComment.addEventListener("mouseenter", function () {
                            this.querySelector(".fa.fa-reply.d-none").className = "fa fa-reply";
                        });
                        newReplyComment.addEventListener("mouseleave", function () {
                            this.querySelector(".fa.fa-reply").className += " d-none";
                            // 回复新发布的评论会有问题
                        });
                        // 绑定赞踩按钮评价事件
                        newReplyComment.querySelector("[name=agree]").addEventListener("click", evaluate);
                        newReplyComment.querySelector("[name=disagree]").addEventListener("click", evaluate);
                    } else {
                        alert("回复评论失败");
                    }
                }
            }
        });
    }
});
