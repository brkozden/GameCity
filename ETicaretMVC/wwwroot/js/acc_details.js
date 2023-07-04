const tabs = document.querySelectorAll(".dashboard-nav .tab-list a");
console.log(tabs);

const tabContents = document.querySelectorAll(".tab-content .tab-pane");
console.log(tabContents);

for (let i = 0; i < tabContents.length; i++) {
  tabs[i].addEventListener("click", function () {
    noneActiveTabs();
    noneActiveTabContent();
    console.log(i + " tıklandı");
    tabs[i].classList.add("active");
    tabContents[i].classList.add("active");
  });
}

function noneActiveTabs() {
  for (let item of tabs) {
    item.classList.remove("active");
  }
}
function noneActiveTabContent() {
  for (let item of tabContents) {
    item.classList.remove("active");
  }
}
