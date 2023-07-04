// ADD PRODUCTS TO LOCAL STORAGE START

async function getData() {
  const photos = await fetch("../js/data.json");
  const data = await photos.json();

  data ? localStorage.setItem("products", JSON.stringify(data)) : [];
}

getData();

const product = localStorage.getItem("products");
console.log(JSON.parse(product));

// ADD PRODUCTS TO LOCAL STORAGE END
