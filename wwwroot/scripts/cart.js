<script>
  let cart = [];

  document.querySelectorAll('.add-to-cart').forEach(button => {
    button.addEventListener('click', () => {
      const name = button.getAttribute('data-name');
      const price = parseFloat(button.getAttribute('data-price'));

      // Add to cart
      cart.push({ name, price });

      // Update cart count (optional)
      document.getElementById('cart-count').textContent = cart.length;

      console.log(cart); // just to check
    });
  });
</script>