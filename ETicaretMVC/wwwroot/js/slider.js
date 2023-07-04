//-------------------------------------------------------------
// Slider start
let slideIndex = 1;
showSlides();

setInterval(() => {
  showSlides((slideIndex += 1));
}, 4000);
function plusSlide(n) {
  showSlides((slideIndex += n));
}

function currentSlide(n) {
  showSlides((slideIndex = n));
}
function showSlides(n) {
  const sliders = document.getElementsByClassName("slider-item");
  const dots = document.getElementsByClassName("slider-dot");
  if (n > sliders.length) {
    slideIndex = 1;
  }
  if (n < 1) {
    slideIndex = sliders.length;
  }
  for (let i = 0; i < sliders.length; i++) {
    sliders[i].style.display = "none";
  }
  for (let i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" active", "");
  }
  sliders[slideIndex - 1].style.display = "flex";
  dots[slideIndex - 1].className += " active";
}
// Slider end
