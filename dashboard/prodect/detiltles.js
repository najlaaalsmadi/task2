debugger;
const n1 = Number(localStorage.getItem("productId"));
const url1 = `http://localhost:8367/api/Products/prodectbyId/${n1}`;

async function getProducts() {
  debugger;

  var response = await fetch(url1);
  debugger;

  var result = await response.json();
  debugger;

  var container = document.getElementById("categoryTableBody");
  debugger;

  container.innerHTML = `
  <tr>
          <td><img src="../img/${result.productImage}" alt="../img/${result.productImage} NOT Found image" style="width: 100px;"></td>
          <td>${result.productName}</td>
          <td>${result.price}</td>
         <td> ${result.description}</td>
      
         <td><button onclick="store(${result.productId})" class="btn btn-primary">اضافة المنتج الى السلة  </button></td>
        </tr>`;
}

getProducts();
