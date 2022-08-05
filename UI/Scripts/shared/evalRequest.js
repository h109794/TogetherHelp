function requestEvaluate(evaluationBtn, isAgree, contentId, isArticle) {
    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/Shared/Evaluate");
    xhr.setRequestHeader("Content-Type", "application/x-www-form-urlencoded");
    xhr.send(`isAgree=${isAgree}&contentId=${contentId}&isArticle=${isArticle}`);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === xhr.DONE) {
            if (xhr.status === 200) {
                var response = JSON.parse(xhr.response);
                if (response === "insert") {
                    evaluationBtn.getElementsByClassName("evaluation-count")[0].textContent++;
                    evaluationBtn.firstElementChild.className = isAgree ? "fa fa-thumbs-up" : "fa fa-thumbs-down"
                } else if (response === "delete") {
                    evaluationBtn.getElementsByClassName("evaluation-count")[0].textContent--;
                    evaluationBtn.firstElementChild.className = isAgree ? "fa fa-thumbs-o-up" : "fa fa-thumbs-o-down"
                } else if (response === "update") {
                    evaluationBtn.getElementsByClassName("evaluation-count")[0].textContent++;
                    evaluationBtn.firstElementChild.className = isAgree ? "fa fa-thumbs-up" : "fa fa-thumbs-down"
                    // 相反按钮恢复，数量--
                    var evalInverseBtn = isAgree ? evaluationBtn.nextElementSibling : evaluationBtn.previousElementSibling;
                    evalInverseBtn.getElementsByClassName("evaluation-count")[0].textContent--;
                    evalInverseBtn.firstElementChild.className = isAgree ? "fa fa-thumbs-o-down" : "fa fa-thumbs-o-up";
                }// else nothing
            } else {
                alert("评价失败");
            }
        }
    }
}
