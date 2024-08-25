const n1 = Number(localStorage.getItem("productId"));
const url1 = `http://localhost:8367/api/Products/prodectbyId/${n1}`;

async function getProducts() {
  debugger;

  var response = await fetch(url1);
  debugger;

  var result = await response.json();
  debugger;

  var container = document.getElementById("contriner");
  debugger;

  container.innerHTML = `<div class="card" style="width: 18rem;">
    <img src="../img/${result.productImage}" class="card-img-top" alt="${result.productImage}NOT Found image">
    <div class="card-body">
      <h5 class="card-title">${result.productName}</h5>
      <p class="card-text">${result.price}</p>
            <p class="card-text">${result.description}</p>

    </div>
    </div>`;
}

getProducts();
