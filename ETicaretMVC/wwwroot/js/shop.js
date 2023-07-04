const computer_sub_item = document.querySelectorAll(
  ".toggle-list .shop-submenu .filters-list .filters-sub-item"
);
const big_ctg_select = document.querySelectorAll(
  ".toggle-list .filters-list .filters-item .big-ctg-select"
);
const sub_ctg_select = document.querySelectorAll(
  ".product-categories .filters-list .filters-item ul .filters-sub-item"
);

const sub_list = document.querySelectorAll(
  ".toggle-list .shop-submenu .filters-list .filters-item ul"
);

const toggle_categories = document.querySelector(".shop-sidebar .toggle-list ");
const toggle_categories_h3 = document.querySelector(
  ".shop-sidebar .toggle-list h3"
);

for (let i = 0; i < big_ctg_select.length; i++) {
  big_ctg_select[i].addEventListener("click", () => {
    if (big_ctg_select[i].classList.contains("active")) {
      big_ctg_select[i].classList.remove("active");
      sub_list[i].style.display = "none";
      big_ctg_select[i].style.color = "#777777";
    } else {
      removeBigCtg();
      remSubList();

      big_ctg_select[i].classList.add("active");
      big_ctg_select[i].style.color = "#3577f0";
      sub_list[i].style.display = "block";
    }
  });
}
function remSubList() {
  for (let item of sub_list) {
    item.style.display = "none";
  }
}
function removeBigCtg() {
  for (let item of big_ctg_select) {
    item.classList.remove("active");
    item.style.color = "#777777";
  }
}

const inputSubSelect = document.querySelectorAll(
  ".product-categories .filters-list .filters-item ul .filters-sub-item input"
);

for (let i = 0; i < sub_ctg_select.length; i++) {
  sub_ctg_select[i].addEventListener("click", () => {
    removeSubCtg();
    sub_ctg_select[i].style.color = "#3577f0";
  });
}
function removeSubCtg() {
  for (let item of sub_ctg_select) {
    item.style.color = "#777777";
  }
}
// ------------------------------------------------------------------------------------------------------------------
toggle_categories_h3.addEventListener("click", () => {
  if (toggle_categories.classList.contains("active")) {
    toggle_categories.classList.remove("active");
  } else {
    toggle_categories.classList.add("active");
  }
});
// ------------------------------------------Pagination-----------------------------------------------------------------
const pagination_numbers = document.querySelectorAll(
  ".main-shop nav .pagination-number-list li"
);

for (let i = 0; i < pagination_numbers.length; i++) {
  pagination_numbers[i].addEventListener("click", () => {
    currentRemoveNumbers();
    pagination_numbers[i].classList.add("current");
  });
}

function currentRemoveNumbers() {
  for (let item of pagination_numbers) {
    if (item.classList.contains("current")) {
      item.classList.remove("current");
    }
  }
}

const pagination_back = document.querySelector(
  ".main-shop nav .pagination-back"
);

pagination_back.addEventListener("click", () => {
  backOrient();
});

function backOrient() {
  for (let i = 0; i < pagination_numbers.length; i++) {
    if (pagination_numbers[i].classList.contains("current")) {
      if (!i == 0) {
        currentRemoveNumbers();
        pagination_numbers[i - 1].classList.add("current");
      } else {
      }
    }
  }
}
const pagination_next = document.querySelector(
  ".main-shop nav .pagination-next"
);

pagination_next.addEventListener("click", () => {
  nextOrient();
});
function nextOrient() {
  let numb;
  for (let i = 0; i < pagination_numbers.length; i++) {
    if (pagination_numbers[i].classList.contains("current")) {
      numb = i + 1;
    }
  }
  if (!(numb == pagination_numbers.length)) {
    currentRemoveNumbers();
    pagination_numbers[numb].classList.add("current");
  } else {
  }
}
// Alignment ---------------------------------------------------------------------
const toggle_alignment_h3 = document.querySelector("#alignmentMenu .title");

const toggle_alignmentSub = document.querySelector(
  "#alignmentMenu .shop-submenu"
);
const alignmentMenu = document.getElementById("alignmentMenu");

toggle_alignment_h3.addEventListener("click", () => {
  if (toggle_alignmentSub.classList.contains("active")) {
    toggle_alignmentSub.classList.remove("active");
    alignmentMenu.classList.remove("active");
  } else {
    toggle_alignmentSub.classList.add("active");
    alignmentMenu.classList.add("active");
  }
});

const alignmentSub_item = document.querySelectorAll(
  "#alignmentMenu .filters-sub-item"
);

for (let i = 0; i < alignmentSub_item.length; i++) {
  alignmentSub_item[i].addEventListener("click", () => {
    removeAlignSub();
    alignmentSub_item[i].style.color = "#3577f0";
  });

  function removeAlignSub() {
    for (let item of alignmentSub_item) {
      item.style.color = "#777777";
    }
  }
}
