const user = {};
let msg = {};


const signUpBtn = document.querySelector('.sign-up-btn'),
      inputDataToCheck = document.querySelectorAll('[data-check]'),
      listItems = document.querySelectorAll('.dropdown-menu'),
      heightPreference  = document.querySelector('#height'),
      genderInputs = document.querySelectorAll('.sex-radio-btn'),
      userPreferences = {    
        SmokeRate: null,
        BabyRate: null,
        HeightRate: null,
        PetsRate: null,
        RelationshipRate: null,
        DrinkingRate: null,
        Sex: null
    };


    async function getResourse(url) {
        const _apiBase = 'http://localhost:5000';
        //const token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6IjEiLCJyb2xlIjoiU2ltcGxlVXNlciIsIm5iZiI6MTYyMTg0NjE0MiwiZXhwIjoxNjIxOTMyNTQyLCJpYXQiOjE2MjE4NDYxNDJ9.RMCCwEeD4_UHty9YK53LFU_AC5ScDd5JkqQPltaLlLs";
        const myHeaders = new Headers();
        myHeaders.append("Content-Type", "application/json");
    
        const data = JSON.stringify({
            "mail": user.Email,
            "password": user.Password
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
            return;
            //console.log(`ERROR at ${_apiBase}${url}. RESPONSE STATUS: ${res.status}`);
        }
    
        return await res.json();
        //return res;
    }

async function postResourse(url) {
    const _apiBase = 'http://localhost:5000';
    var myHeaders = new Headers();
    myHeaders.append("Content-Type", "application/json");

    var data = JSON.stringify(user);

    var requestOptions = {
    method: 'POST',
    headers: myHeaders,
    body: data,
    redirect: 'follow'
    };
    const res = await fetch(`${_apiBase}${url}`, requestOptions);

    if(!res.ok) {
        console.log(`ERROR at ${_apiBase}${url}. RESPONSE STATUS: ${res.status}`);
    }

    return await res.json();
    //return res;
}

function checkPrefences(preferences) {

    for(let p in preferences) {
        if(preferences[p] == null) {
            if(p === "Sex") {
                preferences[p] = 2;
            } else {
                preferences[p] = 1;
            }
        }
    }

}

function getUserSex() {
    let res = "";
    genderInputs.forEach(input => {
        if(input.checked) {
            res = input.attributes[4].nodeValue;
        } 
    });
    return res = res !== "" ? res : "0";
}

function showNotValidString({notValidString}) {
    document.querySelector('#sign-up-lable').innerText = notValidString;
}

function checkValidation(msg) {
    let res = true;
    inputDataToCheck.forEach(input => {
        if(input.value === "") {
            msg.notValidString = "Empty fields are not allowed";
            res = false;
        }
    });
    if(!res) return false;

    const inputsPass = document.querySelectorAll('.checkPass');
    if(inputsPass[0].value !== inputsPass[1].value) {
        msg.notValidString = "Fields 'password' and 'repeat password' are not equal";
        return false;
    }

    return true;
}

function getAndCheckInfoAboutUser(role = 0, AboutMe = "", ) {
    

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

    user.Sex = getUserSex();
    user.Role = role;
    user.AboutMe = AboutMe;

    userPreferences.HeightRate = heightPreference.innerText;
    userPreferences.RelationshipRate = 0;
    
    user.Preferences = [
        userPreferences
    ];

    checkPrefences(user.Preferences[0]);
}

async function register() {
    getAndCheckInfoAboutUser();
    await postResourse('/Register/registerUser')
    .then((res) => getResourse('/Login')
          .then(res => {

            localStorage.setItem("token", res.token);
          
          })
          .catch(e => console.error(e)))
    .catch(e => console.error(e));
    
    document.location.href = '../../index.html';
}


signUpBtn.addEventListener('click', (e) => {
    e.preventDefault();
    register();
});

//Event for list ps
function getpName(e) {
    return e.target.parentElement.attributes[1].nodeValue;
}

function getpValue(e) {
    return e.target.attributes[1].nodeValue;
}

listItems.forEach(ul => {
    let uls = [];
    for(let i = 0; i < ul.children.length; ++i) {
        uls[uls.length] = ul.children[i];
    }
    uls.forEach(li => {
        li.addEventListener('click', (e) => {
            userPreferences[getpName(e)] = getpValue(e);
         });
    });
});


function showVal(newVal){
    document.getElementById("height").innerHTML = newVal;
}
