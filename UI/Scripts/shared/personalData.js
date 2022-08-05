// 未设置性别选择保密选项
var genderBox = document.getElementsByName("Gender");
for (var i = 0; i < 3; i++) {
    if (i < 2 && genderBox[i].checked) {
        break;
    } else if (i === 2) {
        genderBox[2].checked = true;
    }
}

// 上传头像
var input = document.getElementsByName("Profile")[0];
var upload = document.getElementById("profile-upload");

upload.addEventListener("click", function () {
    input.click();
});

input.addEventListener("change", function () {
    var profile = this.files[0];
    if (profile === undefined) return;
    if (!/image\/\w+/.test(profile.type)) {
        alert("文件类型错误。");
        return;
    }
    if (profile.size > 1024 * 1024 * 2) {
        alert("图片需要不大于2M。");
        return;
    }

    var formData = new FormData();
    formData.append("profile", profile);

    var reader = new FileReader();
    reader.addEventListener("load", function () {
        upload.src = reader.result;
        document.getElementById("nav-profile").src = reader.result;
    });

    var xhr = new XMLHttpRequest();
    xhr.open("POST", "/PersonalData/UploadProfile");
    xhr.send(formData);
    xhr.onreadystatechange = function () {
        if (xhr.readyState === xhr.DONE) {
            if (xhr.status === 200) {
                var response = xhr.response;
                if (response === "false") {
                    alert("素材文件加载失败。");
                } else {
                    reader.readAsDataURL(profile);
                }
            } else {
                alert("头像更换失败");
            }
        }
    }
});
