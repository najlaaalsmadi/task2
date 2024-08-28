debugger;
var x = localStorage.getItem("categoryId");
const url = `http://localhost:8367/api/Categories?id=${x}`;
var form = document.getElementById("form");
debugger;
async function updateCategory() {
  event.preventDefault();
  var formData = new FormData(form);
  var responsive = await fetch(url, {
    method: "PUT",
    body: formData,
  });
  var category = responsive;
  alert("category updated successfully");
  window.location.href = "../Categories/Categories.html";
}
