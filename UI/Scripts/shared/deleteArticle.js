var deleteBtn = document.getElementsByName("delete-article");

for (let i = 0; i < deleteBtn.length; i++) {
    deleteBtn[i].addEventListener("click", function () {
        event.preventDefault();
        if (confirm("文章删除后无法恢复，确认执行此操作吗？")) {
            var articleId = document.getElementsByName("articleId")[i].value;
            var url = window.location.pathname;
            var urlLastsegment = url.substring(url.lastIndexOf('/') + 1, url.length);
            var pageIndex = isNaN(urlLastsegment) ? 1 : urlLastsegment;
            // 当前页文章数量，只有一篇重定向到上一页，否则在当前页
            var articleCount = document.getElementsByName('articleId').length;
            var redirectRouteValue = articleCount === 1 ? --pageIndex : pageIndex;
            location.href = `/article/delete?articleId=${articleId}&routeValue=${redirectRouteValue}`;
        }
    });
}
