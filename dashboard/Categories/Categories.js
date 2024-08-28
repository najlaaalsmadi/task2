const url = "http://localhost:8367/api/Categories";

async function getCategories() {
  debugger;
  var response = await fetch(url);
  var result = await response.json();
  var container = document.getElementById("categoryTableBody");

  result.forEach((element) => {
    const imagePath = element.categoryImage.replace("\\", "/");

    container.innerHTML += `
      <tr>
    <td>
            <img src="${element.categoryImage}" class="card-img-top" alt="${element.categoryImage} NOT Found error" style="width: 100px; height: auto;">
                </td>
                <td>${element.categoryName}</td>
                <td> 
             <button onclick="localStorageCategoryId(${element.categoryId})" class="btn btn-primary" style="width: 100px">المنتجات بالفئة</button>
            <button onclick="localStorageCategoryId2(${element.categoryId})" class="btn btn-secondary" style="width: 100px">تعديل</button>
            <button onclick="deleteCategory(${element.categoryId})" class="btn btn-danger" style="width: 100px">حذف</button>

         </td>

            </tr>
 `;
  });
}
var x = localStorage.getItem("categoryId"); // نحصل على معرّف الفئة المخزّن في LocalStorage
var url1 = `http://localhost:8367/api/Categories/${x}`; // نبني رابط الـ API باستخدام المعرّف

async function deleteCategory(id) {
  event.preventDefault();

  try {
    var response = await fetch(url1, {
      method: "DELETE",
    });

    if (response.ok) {
      alert("تم حذف الفئة بنجاح");
    } else {
      alert("فشل في حذف الفئة");
    }
  } catch (error) {
    console.error("حدث خطأ أثناء محاولة حذف الفئة:", error);
    alert("حدث خطأ أثناء محاولة حذف الفئة.");
  }
}
function ADDCategory() {
  window.location.href = "../addCategory/addCategory.html";
}
function localStorageCategoryId(id) {
  localStorage.categoryId = id;
  window.location.href = "../prodect/prodect.html";
}
function localStorageCategoryId2(id) {
  localStorage.categoryId = id;
  window.location.href = "../edait/edait.html";
}
getCategories();
