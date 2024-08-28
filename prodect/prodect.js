const url = "http://localhost:8367/api/Products";
const n = localStorage.getItem("categoryId");
const url2 = `http://localhost:8367/api/Products/prodectbycategoryId/${n}`;

async function getProducts() {
  debugger;
  var container = document.getElementById("contriner");

  // تأكد من العثور على العنصر
  if (!container) {
    console.error("Element with id 'contriner' not found.");
    return;
  }

  if (n != null) {
    var response = await fetch(url2);
    var result = await response.json();
    result.forEach((element) => {
      container.innerHTML += `<div class="card" style="width: 18rem;">
        <img src="../img/${element.productImage}" class="card-img-top" alt="${element.productImage} NOT Found image">
        <div class="card-body">
          <h5 class="card-title">${element.productName}</h5>
          <p class="card-text">${element.productId}</p>
          <button onclick="localStorageCategoryId1(${element.productId})" class="btn btn-primary">detilies product2</button>
        </div>
      </div>`;
    });
  } else {
    var response1 = await fetch(url);
    var result1 = await response1.json();
    result1.forEach((element) => {
      container.innerHTML += `<div class="card" style="width: 18rem;">
        <img src="../img/${element.productImage}" class="card-img-top" alt="${element.productImage} NOT Found image">
        <div class="card-body">
          <h5 class="card-title">${element.productName}</h5>
          <p class="card-text">${element.productId}</p>
          <button onclick="localStorageCategoryId1(${element.productId})" class="btn btn-primary">detilies product1</button>
        </div>
      </div>`;
    });
  }
}

function localStorageCategoryId1(ProductID) {
  localStorage.setItem("productId", ProductID);
  window.location.href = "detililes.html";
}

// تنفيذ الدالة بعد تحميل الصفحة بالكامل
window.onload = function () {
  getProducts();
};
