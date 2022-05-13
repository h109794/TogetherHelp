var changeKeywordsBtn = document.getElementById("change-keywords");
var searchKeywordBtn = document.getElementById("search-keyword");
var keywordSearchBox = document.getElementById("keyword-search-box");

changeKeywordsBtn.addEventListener("click", function () {
    var keywordsBox = document.getElementById("keywords-box");
    var lastKeyword = keywordsBox.lastElementChild.getAttribute("keyword-id");
    var xhr = new XMLHttpRequest();
    xhr.open("GET", `/Keyword/Change?lastKeywordId=${lastKeyword}`);
    xhr.send();
    xhr.onreadystatechange = function myfunction() {
        if (xhr.readyState === xhr.DONE) {
            if (xhr.status === 200) {
                var keywords = JSON.parse(xhr.response);
                for (var i = 0; i < keywords.length; i++) {
                    keywordsBox.children[i].textContent = keywords[i].Text;
                    keywordsBox.children[i].setAttribute("keyword-id", keywords[i].Id);
                    keywordsBox.children[i].href = `/article/page/1?keywordId=${keywords[i].Id}`;
                }
            }
        }
    }
});

searchKeywordBtn.addEventListener("click", function () {
    var keywordText = keywordSearchBox.value;
    if (keywordText.trim() === '') {
        alert("关键字不能为空");
        return;
    }

    var xhr = new XMLHttpRequest();
    xhr.open('GET', `/Keyword/Search?keywordText=${keywordText}`);
    xhr.send();
    xhr.onreadystatechange = function () {
        if (xhr.readyState === xhr.DONE) {
            if (xhr.status === 200) {
                if (xhr.response === '') {
                    alert("关键字不存在");
                } else {
                    location.href = `/article/page/1?keywordId=${xhr.response}`;
                }
            }
        }
    }
});

// 文本框内回车搜索
keywordSearchBox.addEventListener("keydown", function (e) {
    if (e.keyCode === 13) {
        searchKeywordBtn.click();
    }
});
