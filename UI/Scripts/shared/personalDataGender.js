// 未设置性别选择保密选项
var genderBox = document.getElementsByName("Gender");
for (var i = 0; i < 3; i++) {
    if (i < 2 && genderBox[i].checked) {
        break;
    } else if (i === 2) {
        genderBox[2].checked = true;
    }
}
