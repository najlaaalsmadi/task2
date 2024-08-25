const apiURL = "http://localhost:8367/api/Categories"; // تأكد من صحة هذا العنوان

function fetchCategories() {
  fetch(apiURL)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json(); // تحويل الاستجابة إلى JSON
    })
    .then((data) => {
      // معالجة البيانات وعرضها على الصفحة
      const categoriesList = document.getElementById("categoriesList");
      categoriesList.innerHTML = ""; // تفريغ المحتوى الحالي

      data.forEach((category) => {
        const colDiv = document.createElement("div");
        colDiv.className = "col-md-4 mb-3";

        const cardDiv = document.createElement("div");
        cardDiv.className = "card";

        const cardBodyDiv = document.createElement("div");
        cardBodyDiv.className = "card-body";

        const cardTitle = document.createElement("h5");
        cardTitle.className = "card-title";
        cardTitle.textContent = category.name;

        cardBodyDiv.appendChild(cardTitle);
        cardDiv.appendChild(cardBodyDiv);
        colDiv.appendChild(cardDiv);
        categoriesList.appendChild(colDiv);
      });
    })
    .catch((error) => {
      console.error(
        "There has been a problem with your fetch operation:",
        error
      );
    });
}

document.addEventListener("DOMContentLoaded", fetchCategories);
