const user = {};

let shownContent = 1;

const signUpBtn = document.querySelector('.sign-up-btn'),
      inputDataToCheck = document.querySelectorAll('[data-check]'),
      listItems = document.querySelectorAll('.dropdown-menu'),
      heightPreference = document.querySelector('#height'),
      userPreferences = {};

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

function checkPreferences(preferences) {

    preferences.forEach(preference => {
        if(preference == "") {
            
        }
    });

}

function showNotValidString(msg) {

}

function checkValidation(msg) {

    inputDataToCheck.forEach(input => {
        if(input.value == "") {
            msg.notValidString = "Empty fields are not allowed";
            return false;
        }
    });

    const inputsPass = document.querySelector('.checkPass');
    if(inputsPass[0] != inputsPass[1]) {
        msg.notValidString = "Fields 'password' and 'repeat password' are not equal";
        return false;
    }

    return true;
}

function getInfoAboutUser(role = 0, AboutMe = "", ) {
    msg = {};

    if(!checkValidation(msg)) {
        showNotValidString(msg);
        return;
    }

    user.firstName = document.querySelector('.first-name').value;
    user.lastName = document.querySelector('.last-name').value;
    user.NickName = document.querySelector('.username').value;
    user.Password = document.querySelector('.password').value;
    user.DateOfBith = document.querySelector('#date-of-birth').value;
    user.Email = document.querySelector('.email').value;

    user.Sex = 0;
    user.Role = role;
    user.AboutMe = AboutMe;

    userPreferences.HeightRate = heightPreference.innerText;
    userPreferences.RelationshipRate = 0;
    
    user.Preferences = [
        userPreferences
    ];

    checkPreferences(user.Preferences);
}

function register() {
    getInfoAboutUser();
}


signUpBtn.addEventListener('click', (e) => {
    e.preventDefault();
    register();
});

//Event for list preferences
function getPreferenceName(e) {
    return e.target.parentElement.attributes[1].nodeValue;
}

function getPreferenceValue(e) {
    return e.target.attributes[1].nodeValue;
}

listItems.forEach(ul => {
    let uls = [];
    for(let i = 0; i < ul.children.length; ++i) {
        uls[uls.length] = ul.children[i];
    }
    uls.forEach(li => {
        li.addEventListener('click', (e) => {
            userPreferences[getPreferenceName(e)] = getPreferenceValue(e);
         });
    });
});


function showVal(newVal){
    document.getElementById("height").innerHTML = newVal;
}
