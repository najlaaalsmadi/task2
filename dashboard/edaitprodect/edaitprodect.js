debugger;
var x = Number(localStorage.getItem("productId").trim());
const url = `http://localhost:8367/api/Products?id=${x}`;
var form = document.getElementById("form");
debugger;
async function updateProducts() {
  debugger;
  event.preventDefault();
  var formData = new FormData(form);
  var responsive = await fetch(url, {
    method: "PUT",
    body: formData,
  });
  var category = responsive;
  alert("prodect updated successfully");
  window.location.href = "../product/product.html";
}
