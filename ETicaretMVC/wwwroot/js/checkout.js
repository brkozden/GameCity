const opentoggleLogin = document.getElementById("open-toggle-login-btn");
const toggleDiv = document.querySelector(".checkout-area .checkout-login ");

opentoggleLogin.addEventListener("click", () => {
  if (!toggleDiv.classList.contains("toggle-open")) {
    toggleDiv.classList.add("toggle-open");
  } else {
    toggleDiv.classList.remove("toggle-open");
  }
});
