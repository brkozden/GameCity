const single_image = document.querySelector(
  ".product-gallery .single-image-wrapper img"
);
const images = document.querySelectorAll(
  ".product-gallery .product-thumb .gallery-thumbs img"
);

for (let i = 0; i < images.length; i++) {
  images[i].addEventListener("click", () => {
    removeActive();
    images[i].classList.add("active");
    single_image.src = images[i].src;
  });
}

function removeActive() {
  for (let item of images) {
    item.classList.remove("active");
  }
}

const tab_btn = document.querySelectorAll(".single-tabs .tab-list .tab-button");
const tab_content = document.querySelectorAll(
  ".single-tabs .tab-panel .tab-alt-panel"
);

for (let i = 0; i < tab_btn.length; i++) {
  tab_btn[i].addEventListener("click", () => {
    removeTabBtn();
    tab_btn[i].classList.add("active");
    removeTabContent();
    tab_content[i].classList.add("active");
  });
}

console.log(tab_btn);
function removeTabBtn() {
  for (let item of tab_btn) {
    item.classList.remove("active");
  }
}
function removeTabContent() {
  for (let item of tab_content) {
    item.classList.remove("active");
  }
}

const starPoint = document.querySelectorAll(
  ".tab-panel-reviews .comment-form-rating .stars .star"
);
const totalStars = document.querySelectorAll(
  ".tab-panel-reviews .comment-form-rating .stars a i"
);

for (let i = 0; i < starPoint.length; i++) {
  starPoint[i].addEventListener("click", () => {
    notActiveStar();
    const starPointTag = starPoint[i].querySelectorAll("i");
    for (let item of starPointTag) {
      item.style.color = "gold";
    }
  });
}

function notActiveStar() {
  for (let item of totalStars) {
    item.style.color = "#dee0ea";
  }
}
