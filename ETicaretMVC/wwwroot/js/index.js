const sideMenu = document.getElementById("side-menu");
const menuBtn = document.querySelector("#menu-btn");
const closeBtn = document.querySelector("#close-btn");
const themeToggler = document.querySelector(".theme-toggler");

// Now Date Start

let x = Days();
document.getElementById("dashboard-date").value = x;

function Days() {
  let d = new Date();
  var year = d.getFullYear();
  var month = d.getMonth() + 1;

  if (month < 10) {
    month = "0" + month;
  }
  var date = d.getDate();
  if (date < 10) {
    date = "0" + date;
  }
  var fullDate = year + "-" + month + "-" + date;
  return fullDate;
}

// Now Date End
// Show sidebar
menuBtn.addEventListener("click", () => {
  sideMenu.style.display = "block";
});
// Close sidebar
closeBtn.addEventListener("click", () => {
  sideMenu.style.display = "none";
});
// Change Theme

themeToggler.addEventListener("click", () => {
  document.body.classList.toggle("dark-theme-variables");
  themeToggler.querySelector("span:nth-child(1)").classList.toggle("active");
  themeToggler.querySelector("span:nth-child(2)").classList.toggle("active");
});

const dashboardBtn = document.querySelector("#dashboard-main-btn");
const dashhboardContent = document.querySelector(
  ".dashboard-list .main-dashboard-item"
);

dashboardBtn.addEventListener("click", dashboardOpen);

const dash_addProduct = document.querySelector(".sales-analytics .add_product");
const addProductContent = document.querySelector(".add-product-item");
const addProductBtn = document.querySelector("#addProductBtn");

dash_addProduct.addEventListener("click", addPrdOpen);

const AllBtn = document.querySelectorAll(".sidebar a");
function removeActiveBtn() {
  for (let item of AllBtn) {
    item.classList.remove("active");
  }
}
const sideaddProductBtn = document.querySelector("#addProductBtn");

sideaddProductBtn.addEventListener("click", addPrdOpen);

const allContent = document.querySelectorAll(".dashboard-list li");
function removeActiveContent() {
  for (let item of allContent) {
    item.classList.remove("active");
  }
}

const allProductBtn = document.querySelector("#allProductBtn");

const allProductContent = document.querySelector(".all-product-item");

allProductBtn.addEventListener("click", allPrdOpen);

function allPrdOpen() {
  removeActiveBtn();
  allProductBtn.classList.add("active");
  removeActiveContent();
  allProductContent.classList.add("active");
}
function addPrdOpen() {
  removeActiveBtn();
  addProductBtn.classList.add("active");
  removeActiveContent();
  addProductContent.classList.add("active");
}
const allPrd_addProductBtn = document.querySelector("#allPrd_addPrd");

allPrd_addProductBtn.addEventListener("click", addPrdOpen);

const addPrd_allProductBtn = document.querySelector("#addProduct_allPrdBtn");

addPrd_allProductBtn.addEventListener("click", allPrdOpen);

const SideOrdersBtn = document.querySelector("#ordersBtn");
const historyOrdersContent = document.querySelector(".history-orders-item ");

SideOrdersBtn.addEventListener("click", OrdersOpen);

function OrdersOpen() {
  removeActiveBtn();
  SideOrdersBtn.classList.add("active");
  removeActiveContent();
  historyOrdersContent.classList.add("active");
}

const dash_allOrders = document.querySelector("#dash_allOrders");

dash_allOrders.addEventListener("click", OrdersOpen);

const orderDetailsBtn = document.querySelectorAll(
  ".history-orders-table button"
);

const orderDetailsContent = document.querySelector(".orders-details-item");

for (let item of orderDetailsBtn) {
  item.addEventListener("click", () => {
    removeActiveContent();
    orderDetailsContent.classList.add("active");
  });
}

const dashboardBreadcrumbBtn = document.querySelectorAll(
  ".breadcrumb-dashboard-btn"
);

for (let item of dashboardBreadcrumbBtn) {
  item.addEventListener("click", dashboardOpen);
}
function dashboardOpen() {
  removeActiveBtn();
  removeActiveContent();

  dashhboardContent.classList.add("active");
  dashboardBtn.classList.add("active");
}
