<script>
  // Load existing cart or start fresh
    let cart = JSON.parse(localStorage.getItem('cart')) || [];

    // Update cart count at the top (next to Cart icon maybe)
    function updateCartCount() {
    const cartCount = document.getElementById('cart-count');
    if (cartCount) {
        cartCount.textContent = cart.length;
    }
  }

    // Update total price
    function updateTotal() {
    const totalEl = document.getElementById('total');
    if (totalEl) {
      const total = cart.reduce((sum, item) => sum + item.price, 0);
    totalEl.textContent = `Total: $${total.toFixed(2)}`;
    }
  }

    // Save cart into localStorage
    function saveCart() {
        localStorage.setItem('cart', JSON.stringify(cart));
    updateCartCount();
  }

    // Remove an item from cart by index
    function removeFromCart(index) {
        cart.splice(index, 1);
    saveCart();
    renderCart();
  }

    // Render the cart list
    function renderCart() {
    const cartList = document.getElementById('cart-items');
    if (!cartList) return;

    cartList.innerHTML = '';

    if (cart.length === 0) {
        cartList.innerHTML = '<p>Your cart is empty.</p>';
    updateTotal();
    return;
    }

    cart.forEach((item, index) => {
      const li = document.createElement('li');
    li.className = 'cart-item';
    li.innerHTML = `
    <span>${item.name} — $${item.price.toFixed(2)}</span>
    <button class="remove-item" data-index="${index}">&times;</button>
    `;
    cartList.appendChild(li);
    });

    // Attach remove buttons after rendering
    document.querySelectorAll('.remove-item').forEach(button => {
        button.addEventListener('click', () => {
            const index = parseInt(button.getAttribute('data-index'), 10);
            removeFromCart(index);
        });
    });

    updateTotal();
    updateCartCount();
  }

    // Add item to cart
    function addToCart(name, price) {
        cart.push({ name, price });
    saveCart();
    renderCart();
  }

    // Checkout function
    function checkout() {
    if (cart.length === 0) {
        alert("Your cart is already empty!");
    return;
    }
    cart = [];
    saveCart();
    renderCart();

    const checkoutMessage = document.getElementById('checkout-message');
    if (checkoutMessage) {
        checkoutMessage.textContent = "Thank you for your purchase!";
    }
  }

  // When page loads
  document.addEventListener('DOMContentLoaded', () => {
        // Render existing cart if there is one
        renderCart();

    // Attach event listeners to "Add to Cart" buttons
    document.querySelectorAll('.add-to-cart').forEach(button => {
        button.addEventListener('click', () => {
            const name = button.getAttribute('data-name');
            const price = parseFloat(button.getAttribute('data-price'));

            addToCart(name, price);
        });
    });

    // Attach event listener to Checkout button
    const checkoutBtn = document.getElementById('checkout-btn');
    if (checkoutBtn) {
        checkoutBtn.addEventListener('click', checkout);
    }
  });
</script>
