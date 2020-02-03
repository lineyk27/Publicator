import axios from "axios";

const authName = "Authorization";

function setToken (token){
    window.localStorage.setItem(authName, token);
}

function getToken(){
    return window.localStorage.getItem(authName);
}

const requests = axios.create({
    headers: {
        authName: getToken
    }
})

export {
    requests,
    setToken,
    getToken
}