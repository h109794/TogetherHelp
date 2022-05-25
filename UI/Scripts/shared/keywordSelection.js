var keywordBox = document.getElementById("keyword-box");
var levelOneKeyword = document.getElementById("level-one-keyword");
var levelTwoKeyword = document.getElementById("level-two-keyword");
var levelTowOption = {
    language: ["C", "C++", "C#", "Java", "JavaScript", "PHP", "Python", "SQL", "VisualBasic"],
    system: ["Android", "iOS", "Linux", "macOS", "Unix", "Windows"],
    tool: ["CAD", "Eclipse", "Excel", "IntelliJIDEA", "PhotoShop", "PowerPoint", "VisualStudio", "Word"],
    frame: ["Angular", "Bootstrap", "React", "jQuery", "Node.js", "Vue"],
};

levelOneKeyword.addEventListener("change", selectLevelOneKeyword);
levelTwoKeyword.addEventListener("change", selectLevelTwoKeyword);
document.getElementById("user-defined-keyword").addEventListener("keydown", showUserDefinedKeyword);

function selectLevelOneKeyword() {
    // 每次一级关键字选项改变，先清空二级关键字下拉选项(保留一个空白占位选项)
    levelTwoKeyword.options.length = 1;
    // 未选择一级关键字禁用二级关键字下拉框
    if (this.selectedIndex !== 0) {
        levelTwoKeyword.disabled = false;
    } else {
        levelTwoKeyword.disabled = true;
    }
    // 根据一级关键字选项给二级关键字添加选项(看起来有点蠢)
    if (this.selectedIndex === 1) {
        for (let i = 0; i < levelTowOption.language.length; i++) {
            levelTwoKeyword.add(new Option(levelTowOption.language[i]));
        }
    } else if (this.selectedIndex === 2) {
        for (let i = 0; i < levelTowOption.system.length; i++) {
            levelTwoKeyword.add(new Option(levelTowOption.system[i]));
        }
    } else if (this.selectedIndex === 3) {
        for (let i = 0; i < levelTowOption.tool.length; i++) {
            levelTwoKeyword.add(new Option(levelTowOption.tool[i]));
        }
    } else {
        for (let i = 0; i < levelTowOption.frame.length; i++) {
            levelTwoKeyword.add(new Option(levelTowOption.frame[i]));
        }
    }

    for (let i = 1; i < this.options.length; i++) {
        if (this.selectedIndex === this.options[i].index) {
            showKeyword(this.options[i].innerText);
        }
    }
}

function selectLevelTwoKeyword() {
    for (let i = 1; i < this.options.length; i++) {
        if (this.selectedIndex === this.options[i].index) {
            showKeyword(this.options[i].innerText);
        }
    }
}

function showUserDefinedKeyword() {
    if (event.keyCode === 32 && this.value.trim() !== '') {
        showKeyword(this.value.trim());
        this.value = '';
    }
}

function showKeyword(selectedKeyword) {
    var lable = document.createElement("span");
    var text = document.createTextNode(selectedKeyword + ' ');
    var remove = document.createElement("i");
    var showKeywordBox = document.getElementById("show-keyword");

    for (let i = 0; i < showKeywordBox.children.length; i++) {
        if (text.nodeValue === showKeywordBox.children[i].innerText) {
            return;
        }
    }
    remove.setAttribute("class", "fa fa-times");
    remove.setAttribute("aria-hidden", "true");
    remove.addEventListener("click", function () {
        lable.remove();
    })

    lable.appendChild(text);
    lable.appendChild(remove);
    lable.className = "keyword";
    document.getElementById("show-keyword").insertBefore(lable, showKeywordBox.firstElementChild);
}

// 编辑文章时为已有关键字添加移除事件
var keywords = document.getElementsByClassName("keyword");
for (let i = 0; i < keywords.length; i++) {
    keywords[i].lastElementChild.addEventListener("click", function () {
        this.parentElement.remove();
    })
}
