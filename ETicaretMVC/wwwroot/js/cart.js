const progress = document.querySelector(
  ".cart-page .free-progress-bar .progress-bar .progress"
);
const price = document.querySelector(
  ".cart-collaterals .cart-totals #subtotal"
);
const remMoney = document.querySelector(
  ".cart-page .free-progress-bar .progress-bar-title strong"
);
const titleProgressBar = document.querySelector(
  ".cart-page .free-progress-bar .progress-bar-title"
);
const decrease = document.querySelectorAll(
  ".cart-page .shop-table-wrapper .shop-table .decrease-count"
);

const increase = document.querySelectorAll(
  ".cart-page .shop-table-wrapper .shop-table .increase-count"
);
const del_product = document.querySelectorAll(
  ".cart-page .shop-table-wrapper .shop-table .cart-delete .bi.bi-x"
);

function progressStart() {
  var x = price.textContent;
  var remMny = 300 - x;
  console.log(x);

  if (x >= 300) {
    progress.style.width = "100%";
    titleProgressBar.textContent = "Tebrikler Kargonuz Ücretsizdir.";
  } else {
    let percent = 100 - (300 - x) / 3;

    remMoney.textContent = remMny;

    progress.style.width = percent + "%";
    titleProgressBar.innerHTML =
      " Kargonuzun bedava olması için <strong>" +
      remMny.toString() +
      "</strong> TL'lik ürün daha ekleyin.";
  }
}
progressStart();

for (let i = 0; i < decrease.length; i++) {
  decrease[i].addEventListener("click", () => {
    progressStart();
  });
}
for (let i = 0; i < increase.length; i++) {
  increase[i].addEventListener("click", () => {
    progressStart();
  });
}
for (let i = 0; i < del_product.length; i++) {
  del_product[i].addEventListener("click", () => {
    progressStart();
  });
}
