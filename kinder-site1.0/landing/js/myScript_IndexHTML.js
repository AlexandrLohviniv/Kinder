const goToPage = (url) => {
    window.location.href = url;
}

// Go to Login
const enterBtns = document.querySelectorAll('.login');

enterBtns.forEach(btn => {
    btn.addEventListener('click', () => {
        goToPage('login.html');
    });
});

