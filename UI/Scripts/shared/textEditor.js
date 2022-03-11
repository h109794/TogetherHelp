const E = window.wangEditor;
const editor = new E(document.getElementById("editor"));
editor.config.focus = false;
editor.config.zIndex = 0;
editor.config.onchange = function (html) {
    document.getElementById("Body").value = html;
}
editor.create();
