debugger;
const url3 = "http://localhost:8367/api/Products";
var form = document.getElementById("form");
async function addprodect() {
  debugger;
  var formdata = new FormData(form);
  event.preventDefault();
  let response = await fetch("http://localhost:8367/api/Products", {
    method: "POST",
    body: formdata,
  });
  var data = response;
}
