const url = "http://localhost:8367/api/Products";
const n = localStorage.getItem("categoryId");
const url2 = `http://localhost:8367/api/Products/prodectbycategoryId/${n}`;

async function getProducts() {
  debugger;
  var container = document.getElementById("productTableBody");

  // تأكد من العثور على العنصر
  if (!container) {
    console.error("Element with id 'contriner' not found.");
    return;
  }

  var tableBody = document.getElementById("productTableBody");

  // جلب البيانات وبناء الجدول
  if (n != null) {
    var response = await fetch(url2);
    var result = await response.json();
    result.forEach((element) => {
      tableBody.innerHTML += `
        <tr>
          <td><img src="../img/${element.productImage}" alt="${element.productImage} NOT Found image" style="width: 100px;"></td>
          <td>${element.productName}</td>
          <td>${element.productId}</td>
           <td><button onclick="localStorageCategoryId1(${element.productId})" class="btn btn-primary">تفاصيل المنتج</button></td>
          <td><button onclick="delete(${element.productId})" class="btn btn-danger">حذف المنتج</button></td>
          <td><button onclick="edait(${element.productId})" class="btn btn-secondary">تعديل المنتج</button></td>
        </tr>`;
    });
  } else {
    var response1 = await fetch(url);
    var result1 = await response1.json();
    result1.forEach((element) => {
      tableBody.innerHTML += `
        <tr>
          <td><img src="../img/${element.productImage}" alt="${element.productImage} NOT Found image" style="width: 100px;"></td>
          <td>${element.productName}</td>
          <td>${element.productId}</td>
             <td><button onclick="localStorageCategoryId1(${element.productId})" class="btn btn-primary">تفاصيل المنتج</button></td>
          <td><button onclick="delete(${element.productId})" class="btn btn-danger">حذف المنتج</button></td>
          <td><button onclick="edait(${element.productId})" class="btn btn-secondary">تعديل المنتج</button></td>
        </tr>`;
    });
  }
}

function localStorageCategoryId1(productId) {
  localStorage.setItem("productId", productId);
  window.location.href = "detililes.html";
}
function edait(productId) {
  localStorage.setItem("productId", productId);
  window.location.href = "../edaitprodect/edaitprodect.html";
}
// تنفيذ الدالة بعد تحميل الصفحة بالكامل
window.onload = function () {
  getProducts();
};
