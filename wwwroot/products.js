let cart = 0;
const cartCount = document.getElementById('cart-count');
const list = document.getElementById('product-list');

fetch('/api/products')
    .then(r => r.json())
    .then(products => {
        products.forEach(p => {
            const card = document.createElement('div');
            card.innerHTML = `
        <img src="${p.imagePath}" alt="${p.name}" />
        <h3>${p.name}</h3>
        <p>$${p.price}</p>
        <button>Add to Cart</button>
      `;
            card.querySelector('button').onclick = () => {
                cart++;
                cartCount.textContent = `Cart: ${cart}`;
            };
            list.append(card);
        });
    });