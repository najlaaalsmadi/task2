async function getProducts() {
  const n1 = Number(localStorage.getItem("cartID"));

  if (!n1) {
    console.error("cartID is not set in localStorage.");
    return;
  }

  const url1 = `http://localhost:8367/api/CartItems/${n1}`;

  try {
    const response = await fetch(url1);
    if (!response.ok) throw new Error("Network response was not ok");

    const result = await response.json();

    // Log the entire result to see what's being returned
    console.log("API response:", result);

    const container = document.getElementById("contriner");

    if (container) {
      container.innerHTML = result
        .map((item) => {
          const product = item.productRequestDTO;

          return `<div class="card" style="width: 18rem;">
            <img src="../img/${
              product.productImage || "default.jpg"
            }" class="card-img-top" alt="${
            product.productImage ? product.productImage : "Image NOT Found"
          }">
            <div class="card-body">
              <h5 class="card-title">${
                product.productName || "Product Name not available"
              }</h5>
              <p class="card-text">${
                product.price ? `${product.price} USD` : "Price not available"
              }</p>
              <p class="card-text">${
                product.description || "Description not available"
              }</p>
              <p class="card-text">Quantity: ${
                item.quantity || "Quantity not available"
              }</p>
            </div>
          </div>`;
        })
        .join("");
    } else {
      console.error("Container element with ID 'contriner' not found.");
    }
  } catch (error) {
    console.error("Error fetching product data:", error);
  }
}

getProducts();
