var paginationList = document.getElementsByClassName("pagination")[0];

window.onload = function () {
    if (window.innerWidth < 768) {
        paginationList.className = "pagination pagination-sm";
    }
};

window.onresize = function () {
    if (window.innerWidth < 768) {
        paginationList.className = "pagination pagination-sm";
    } else {
        paginationList.className = "pagination";
    }
};
