import requests from ".";

const BASE_URL = "/api/account";

export default AccountAPI = {
    current: () =>{
        return requests.get(`${BASE_URL}/current`)
    },
    login: (login, password) =>{
        return requests.post(`${BASE_URL}/login`, {login, password});
        },
    register: (username, email, password, confirmPassword) => {
        return requests.post(`${BASE_URL}/register`, { username, email, password, confirmPassword
        })
    },
    confirm: (id, token) => {
        return requests.get(`${BASE_URL}/confirm`,{
            params: { id, token }
        })
    }
};
