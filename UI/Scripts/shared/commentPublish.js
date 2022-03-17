document.getElementById("publish").addEventListener("click", function () {
    if (document.cookie.indexOf("loginInfo") === -1) {
        if (confirm("登录用户才能发布评论，单击确定跳转到登录页面。")) {
            location.href = "/Login";
        }
        return;
    }

    var commentContent = document.getElementById("comment-content");
    if (commentContent.value.match(/^\s*$/)) {
        alert("评论内容不能为空");
        commentContent.value = '';
        return;
    }

    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Shared/PublishComment");
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.send(`commentContent=${commentContent.value}`);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === xhr.DONE) {
            if (xhr.status === 200) {
                var newComment = document.createElement("div");
                newComment.className = "mb-3";
                newComment.innerHTML = xhr.responseText;
                document.getElementById("comments").appendChild(newComment);
                document.getElementById("comment-count").textContent++;
                commentContent.value = '';
                // 绑定回复按钮显隐事件
                newComment.addEventListener("mouseenter", function () {
                    this.getElementsByClassName("fa fa-reply")[0].style.display = '';
                });
                newComment.addEventListener("mouseleave", function () {
                    this.getElementsByClassName("fa fa-reply")[0].style.display = 'none';
                });
            } else {
                alert("评论发布失败");
            }
        }
    };
});
