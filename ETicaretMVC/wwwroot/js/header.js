// Home Sidebar start
const btnOpenSidebar = document.querySelector("#btn-mobile-menu");
const sidebar = document.querySelector("#sidebar");
const btnCloseSidebar = document.querySelector("#close-sidebar");

btnOpenSidebar.addEventListener("click", () => {
  sidebar.style.left = "0";
});

btnCloseSidebar.addEventListener(
  "click",
  (sidebarClose = () => {
    sidebar.style.left = "-100%";
  })
);

// Click outside start
document.addEventListener("click", (event) => {
  if (
    !event.composedPath().includes(sidebar) &&
    !event.composedPath().includes(btnOpenSidebar)
  ) {
    sidebarClose();
  }
});
// Click outside end
// Home side bar end

// Search Modal Start

const btnOpenSearch = document.querySelector(".search-button");
const btnCloseSearch = document.getElementById("close-search");
const modalSearch = document.getElementsByClassName("modal-search");
const modalSearchWrapper = document.getElementsByClassName("modal-wrapper");
btnOpenSearch.addEventListener("click", () => {
  modalSearch[0].style.opacity = "1";
  modalSearch[0].style.visibility = "visible";
  modalSearch[0].style.display = "flex";
});
btnCloseSearch.addEventListener(
  "click",
  (closeSearch = () => {
    modalSearch[0].style.opacity = "0";
    modalSearch[0].style.visibility = "none";
    modalSearch[0].style.display = "none";
  })
);

// Click outside start
document.addEventListener("click", (e) => {
  if (
    !e.composedPath().includes(modalSearchWrapper[0]) &&
    !e.composedPath().includes(btnOpenSearch)
  ) {
    closeSearch();
  }
});

// Search modal end
