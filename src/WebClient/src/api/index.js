import axios from "axios";

const authName = "Authorization";

function setToken (token){
    window.localStorage.setItem(authName, token);
}

function getToken(){
    return window.localStorage.getItem(authName);
}

function removeToken() {
    window.localStorage.removeItem(authName);
}

function getAuthHeaders() {
    return getToken() !== null ? { [authName]: `Bearer ${getToken()}`} : null;
}

const requests = axios.create({
    headers: getAuthHeaders()
})

export {
    requests,
    setToken,
    getToken,
    removeToken,
    getAuthHeaders
}