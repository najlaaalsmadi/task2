const n1 = Number(localStorage.getItem("productId"));
const url1 = `http://localhost:8367/api/Products/prodectbyId/${n1}`;
const url11 = "http://localhost:8367/api/CartItems";

async function getProducts() {
  try {
    const response = await fetch(url1);
    if (!response.ok) throw new Error("Network response was not ok");
    const result = await response.json();
    const container = document.getElementById("contriner");
    container.innerHTML = `<div class="card" style="width: 18rem;">
      <img src="../img/${result.productImage}" class="card-img-top" alt="${result.productImage} NOT Found">
      <div class="card-body">
        <h5 class="card-title">${result.productName}</h5>
        <p class="card-text">${result.price}</p>
        <p class="card-text">${result.description}</p>
        <input id="cartQuantity" type="number">
        <button onclick="add()" class="btn btn-primary">اضافة الى السلة</button>
      </div>
      </div>`;
  } catch (error) {
    console.error("Error fetching product data:", error);
  }
}

async function add() {
  try {
    localStorage.setItem("cartID", 1);
    const CartID = localStorage.getItem("cartID");
    const input = document.getElementById("cartQuantity");
    const response = {
      cartId: CartID,
      productId: localStorage.getItem("productId"),
      Quantity: input.value,
    };
    const responseData = await fetch(url11, {
      method: "POST",
      body: JSON.stringify(response),
      headers: {
        "Content-Type": "application/json",
      },
    });
    if (!responseData.ok) throw new Error("Network response was not ok");
    // Handle success, e.g., display a success message
  } catch (error) {
    console.error("Error adding to cart:", error);
  }
}

getProducts();
