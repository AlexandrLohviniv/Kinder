const user = {};

let shownContent = 1;

const signUpBtn = document.querySelector('.sign-up-btn'),
      heightInput = document.querySelector('.height-input'),
      inputData = document.querySelectorAll('input');

async function getResourse(url) {
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var data = JSON.stringify({
    "firstName": "Alexandr",
    "lastName": "Lohvinov",
    "Sex": 0,
    "Role": 0,
    "DateOfBith": "2002-07-31",
    "AboutMe": "Lorem ipsum Lorem ipsum Lorem ipsum Lorem ipsum",
    "NickName": "Logarifm",
    "Email": "ato31ato@gmail.com",
    "Password": "12345",
    "Preferences": [
        {
            "SmokeRate": 1,
            "BabyRate": 1,
            "HeightRate": 1,
            "PetsRate": 1,
            "RelationshipRate": 1,
            "DrinkingRate": 1,
            "Sex": 0
        }
    ]
    });

    var requestOptions = {
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

function shownContentOnPage() {
    switch (shownContent) {
        case 1:
                        
            break;
        default:
            // statements_def
            break;
    }
}

function getUserPreferences() {
    return {
        SmokeRate: 
    }
}

function getInfoAboutUser() {
    user.firstName = document.querySelector('.first-name').value;
    user.lastName = document.querySelector('.last-name').value;
    user.nickName = document.querySelector('.username').value;
    user.password = document.querySelector('.password').value;
    user.preferences = [{
        "SmokeRate": 1,
        "BabyRate": 1,
        "HeightRate": 1,
        "PetsRate": 1,
        "RelationshipRate": 1,
        "DrinkingRate": 1,
        "Sex": 0
    }];
    
}

function register() {
    getInfoAboutUser();
}


signUpBtn.addEventListener('click', (e) => {
    e.preventDefault();
    register();
});

function showVal(newVal){
    document.getElementById("valBox").innerHTML = newVal;
}
