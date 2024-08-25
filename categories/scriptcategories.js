const url = "http://localhost:8367/api/Categories";

async function getCategories() {
  debugger;
  var response = await fetch(url);
  var result = await response.json();
  var container = document.getElementById("contriner");

  result.forEach((element) => {
    container.innerHTML += `<div class="card" style="width: 18rem;">
  <img src="../img/${element.categoryImage}" class="card-img-top" alt="${element.categoryImage}NOT Found erorr">
  <div class="card-body">
    <h5 class="card-title">${element.categoryName}</h5>
    <p class="card-text">Some quick example text to build on the card title and make up the bulk of the card's content.</p>
    <button onclick="localStorageCategoryId(${element.categoryId})" class="btn btn-primary">Store CategoryID</button>
  </div>
</div>`;
  });
}
function localStorageCategoryId(id) {
  localStorage.categoryId = id;
  window.location.href = "../prodect/prodect.html";
}
getCategories();
