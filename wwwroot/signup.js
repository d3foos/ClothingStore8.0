const form = document.getElementById('signup-form');
form.onsubmit = e => {
    e.preventDefault();
    const pw = document.getElementById('password').value;
    if (pw !== document.getElementById('confirm').value) {
        return alert('Passwords must match');
    }
    if (!document.getElementById('terms').checked) {
        return alert('You must agree to terms');
    }
    fetch('/api/account/register', {
        method: 'POST',
        headers: {'Content-Type':'application/json'},
        body: JSON.stringify({
            name: document.getElementById('name').value,
            email: document.getElementById('email').value,
            passwordHash: pw,
            address: document.getElementById('address').value,
            preferredSize: document.getElementById('size').value
        })
    })
        .then(r => r.ok ? alert('Registered!') : r.text().then(t => alert(t)));
};  