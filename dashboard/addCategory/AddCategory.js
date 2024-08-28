const url = "http://localhost:8367/api/Categories";
var form = document.getElementById("form");
async function addCategory() {
  debugger;
  var formdata = new FormData(form);
  event.preventDefault();
  let response = await fetch("http://localhost:8367/api/Categories", {
    method: "POST",
    body: formdata,
  });
  var data = response;
  alert("nvdjjkjgjgguk");
  window.location.href = "../dashboard/categories/Categories.html";
}
