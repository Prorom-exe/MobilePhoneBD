// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function GetMan() {
    const url = "/api/AllApi/GetMan?"
    const xhr = new XMLHttpRequest()
    var Id = document.getElementById("cat").value
    xhr.open("GET", url + "catId=" + Id)
    xhr.onload = () => {
        if (xhr.status == 200) {
            var list = JSON.parse(xhr.response)
            console.log(list)
            var sel = document.getElementById("man")
            while (sel.childNodes.length) {
                if (sel.firstChild.tagName == 'option') {
                    while (sel.firstChild.childNodes.length) {
                        sel.firstChild.removeChild(sel.firstChild.firstChild);
                    }
                }
                sel.removeChild(sel.firstChild);
            }
             list.forEach(function (lt) {
                let opt = document.createElement("option");
                opt.value = lt["id"]
                opt.text = lt["name"]
                sel.add(opt)
            })
        }
    }
    xhr.send()
}

function New() {
    var selectMan = document.getElementById("man")
    var newMan = document.getElementById("newMan")
    var input = document.getElementById("newCat")
    if (!(input.value.trim() == '')) {
       
        selectMan.disabled = true;
        newMan.setAttribute("required", "true")
    }
    else {
        selectMan.disabled = false;
        newMan.setAttribute("required", "false")
    }
}

function Delete(id) {
    if (confirm("Вы действительно хотите удалить этот товар?")) {
        const url = "/api/AllApi/Delete?"
        const xhr = new XMLHttpRequest()
        xhr.open("GET", url + "Id=" + id)
        xhr.onload = () => {
            alert("Товар успешно удалён")
            location.reload()
        }
        xhr.send()
    }
}

function BasketAdd(id,kolProd){
    var kol = prompt("Введите количество", "1")
    if (kol > kolProd) {
        do {
            if (kol == "000") {
                break;
            }
            kol = prompt("Введите меньшее количество,для отмены введите 000", "1")
        }
        while (kol <= kolProd)
    }
    
    if(kol <= kolProd & kol != 0){
        const url = "/api/AllApi/BasketAdd?"
        const xhr = new XMLHttpRequest()
        xhr.open("GET", url + "id=" + id + "&kol=" + kol)
        xhr.onload = () => {
            alert("Товар добавлен в корзину")
            location.reload();
        }
        xhr.send()
    }
}

function BasketDelete(id) {
    if (confirm("Вы действительно хотите удалить этот товар?")) {
        const url = "/api/AllApi/BasketDelete?"
        const xhr = new XMLHttpRequest()
        xhr.open("GET", url + "id=" + id)
        xhr.onload = () => {
            alert("Товар успешно удалён")
            location.reload()
        }
        xhr.send()
    }
}

function Buy() {
    if (confirm("Вы действительно хотите купить все товары?")) {
        const url = "/api/AllApi/Buy"
        const xhr = new XMLHttpRequest()
        xhr.open("GET", url)
        xhr.onload = () => {
            alert("Товар успешно куплены")
            location.reload()
        }
        xhr.send()
    }
}