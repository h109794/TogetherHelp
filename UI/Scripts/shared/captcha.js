// 验证码输入框
document.getElementById("Captcha").addEventListener("focus", showCaptcha);
// 更换图片按钮
document.getElementById("replace-img").addEventListener("click", replaceImage)

function showCaptcha() {
    var image = document.getElementById("captcha-img");
    if (image.style.display === 'none') {
        image.setAttribute("src", "/Shared/GenerateCaptcha");
        image.style.display = '';
        // 隐藏生成验证码提示语
        image.nextElementSibling.style.display = "none";
        document.getElementById("replace-img").style.display = '';
    }
}

function replaceImage() {
    event.preventDefault();
    // 加入当前时间作为url参数，防止图片被浏览器缓存
    document.getElementById("captcha-img").setAttribute("src", "/Shared/GenerateCaptcha?t=" + new Date().getTime());
}