const form = document.getElementById('login-form');
document.getElementById('toggle-pw').onclick = () => {
    const pw = document.getElementById('password');
    pw.type = pw.type === 'password' ? 'text' : 'password';
};
form.onsubmit = e => {
    e.preventDefault();
    fetch('/api/account/login', {
        method: 'POST',
        headers: {'Content-Type':'application/json'},
        body: JSON.stringify({
            email: document.getElementById('email').value,
            passwordHash: document.getElementById('password').value
        })
    })
        .then(r => r.ok ? alert('Logged in!') : r.text().then(t => alert(t)));
};