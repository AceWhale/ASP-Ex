document.addEventListener('DOMContentLoaded', function () {
    const authButton = document.getElementById("auth-button");
    if (authButton) authButton.addEventListener('click', authButtonClick);

    initAdminPage();
});

function authButtonClick() {
    const authEmail = document.getElementById("auth-email");
    if (!authEmail) throw "Element '#authEmail' not found!"
    const authPassword = document.getElementById("auth-password");
    if (!authPassword) throw "Element '#authPassword' not found!"
    const authMessage = document.getElementById("auth-message");
    if (!authMessage) throw "Element '#authMessage' not found!"

    const email = authEmail.value.trim();
    if (!email) {
        authMessage.classList.remove('visually-hidden');
        authMessage.innerText = "! You must enter your email";
        return;
    }
    const password = authPassword.value;

    fetch(`/api/auth?e-mail=${email}&password=${password}`)
        .then(r => {
            if (r.status != 200) {
                authMessage.classList.remove('visually-hidden');
                authMessage.innerText = "! Your login has been cancelled, please check your data";
            }
            else {
                window.location.reload();
            }
        });
}


///// ADMIN PAGE //////
function initAdminPage() {
    loadCategories();
}
function loadCategories() {
    const container = document.getElementById("category-container");
    if (!container) return;
    fetch("/api/category") 
        .then(r => r.json())
        .then(j => {
            let html = "";
            for (let ctg of j) {
                html += `<p data-id="${ctg["id"]}" onclick="ctgClick('${ctg["id"]}')">${ctg["name"]}</p>`;
            }
            html += `Назва: <input id="ctg-name" /><br/>
            Опис: <textarea id="ctg-description"></textarea><br/>
            Фото: <input type="file" id="ctg-photo" /><br/>
            <button onclick='addCategory()'>+</button>`;
            container.innerHTML = html;
        });

}

function addCategory() {
    const ctgName = document.getElementById("ctg-name").value;
    const ctgDescription = document.getElementById("ctg-description").value;
    const ctgPhoto = document.getElementById("ctg-photo");
    console.log(ctgName, ctgDescription, ctgPhoto.value)

    if (confirm(`Додаємо категорію ${ctgName} ${ctgDescription} ?`)) {
        let formData = new FormData();
        formData.append("name", ctgName);
        formData.append("description", ctgDescription);
        formData.append("photo", ctgPhoto.files[0]);

        fetch("/api/category", {
            method: 'POST',
            body: formData
        })
            .then(r => {
                if (r.status == 201) {
                    loadCategories();
                }
                else {
                    alert("error");
                }
            });
    }
}

function ctgClick(ctgid) {
    //const ctgid = e.target.closest('[data-id]').getAttribute('data-id');
    fetch("/api/product/" + ctgid)
        .then(r => r.json())
        .then(j => {
            const container = document.getElementById("product-container");
            let html = "";
            for (let prod of j) {
                html += `<p data-id="${prod["id"]}" onclick="prodClick(event)">${prod["name"]}</p>`;
            }
            html += `Назва: <input id="prod-name" /><br/>
            Опис: <textarea id="prod-description"></textarea><br/>
            Рейтинг: <input id="prod-stars" type="number"/><br/>
            Фото: <input type="file" id="prod-photo" /><br/>
            Цена: <input type="number" id="prod-price" /><br/>
            Кол-во: <input type="number" id="prod-count" /><br/>
            Slug: <input type="text" id="prod-slug" /><br/>
            <button onclick='addProduct("${ctgid}")'>+</button>`;
            container.innerHTML = html;
        });
}

function addProduct(ctgid) {
    const ctgName = document.getElementById("prod-name").value;
    const ctgDescription = document.getElementById("prod-description").value;
    const ctgStars = document.getElementById("prod-stars").value;
    const locPhoto = document.getElementById("prod-photo");
    const prodPrice = document.getElementById("prod-price").value;
    const prodCount = document.getElementById("prod-count").value;
    const prodSlug = document.getElementById("prod-slug").value;

    if (confirm(`Додаємо продукт ${ctgName} ${ctgDescription} ${ctgStars} ?`)) {
        let formData = new FormData();
        formData.append("categoryId", ctgid);
        formData.append("name", ctgName);
        formData.append("description", ctgDescription);
        formData.append("stars", ctgStars);
        formData.append("photo", locPhoto.files[0]);
        formData.append("price", prodPrice);
        formData.append("count", prodCount);
        formData.append("slug", prodSlug);
        fetch("/api/product", {
            method: 'POST',
            body: formData
        })
            .then(r => {
                if (r.status == 201) {
                    ctgClick(ctgid);
                }
                else {
                    alert("error");
                }
            });
    }
}