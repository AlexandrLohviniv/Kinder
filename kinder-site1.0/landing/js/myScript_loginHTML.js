const inputs = document.querySelectorAll('input'),
      _apiBase = 'http://localhost:5000',
      loginBtn = document.querySelector('[login-btn]');

function isEmpty() {
    let isNotEmpty = true;
    inputs.forEach(input => {
        if(input == "") {
            isNotEmpty = false;
        }
    });
    return isNotEmpty;
}

async function getResourse(url) {
    //const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiU2ltcGxlVXNlciIsIm5iZiI6MTYyMTg0NjE0MiwiZXhwIjoxNjIxOTMyNTQyLCJpYXQiOjE2MjE4NDYxNDJ9.RMCCwEeD4_UHty9YK53LFU_AC5ScDd5JkqQPltaLlLs";
    const myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    const data = JSON.stringify({
        "mail": inputs[0].value,
        "password": inputs[1].value
    });

    const requestOptions = {
    method: 'POST',
    headers: myHeaders,
    body: data,
    redirect: 'follow'
    };

    const res = await fetch(`${_apiBase}${url}`, requestOptions);

    if(!res.ok) {
        //TODO: Добавить оповещение, что такой пользователь не найден
        inputs.forEach(input => {
            showValidate(input);
        });
        return;
        //console.log(`ERROR at ${_apiBase}${url}. RESPONSE STATUS: ${res.status}`);
    }

    return await res.json();
    //return res;
}

function showValidate(input) {
    const thisAlert = input.parentNode;
    thisAlert.classList.add('alert-validate');
}

function hideValidate(input) {
    const thisAlert = input.parentNode;
    thisAlert.classList.remove('alert-validate');
}

function validateData(input) {

    if(input.getAttribute('name') == 'username') {
        const regexEmail = /^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/;
        if(!input.value.trim().match(regexEmail)) {
            showValidate(input);
            return false;
        }
    } else {
        if(input.value.trim() == "") {
            return false;
        }
    }
    return true;
}

function validate() {
    var check = true;

    inputs.forEach(input => {
        if(!validateData(input)) {
            showValidate(input);
            check = false;
        } else {
            hideValidate(input);
        }
    });

    return check;
}

function setCookies(c) {
    var d = new Date();
    d.setTime(d.getDate() + (24*60*60*1000)); //Куки ставится на 24 часа
    const expires = "expires=" + d.toUTCString();
    const res = `token=${c}; expires=${expires}; path=/`;
    document.cookie = res;
}

function Login() {
    if(validate()) {
        getResourse('/Login')
        .then(res => {
            localStorage.setItem("token", res.token); //ВРЕМЕННО

            //setCookies НЕ РАБОТАЕТ НА ЛОКАЛЬНОМ СЕРВЕРЕ file://. Раскомментить на продакшене
            //setCookies(res.token); 

            location.href = "index.html";
        });
    }
}

loginBtn.addEventListener('click', (e) => {
    e.preventDefault();
    Login();
});



