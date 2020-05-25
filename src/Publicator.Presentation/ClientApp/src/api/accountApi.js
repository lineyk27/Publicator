import { requests } from "./index";

const BASE_URL = "/api/account"

const AccountAPI = {
    login: (login, password) => {
        return requests.post(`${BASE_URL}/login`, {login, password});
    },
    current: () => {
        return requests.get(`${BASE_URL}/current`)
    },
    register: (username, email, password, confirmPassword) => {
        return requests.post(`${BASE_URL}/register`, {
            username,
            email,
            password,
            confirmPassword
        });
    },
    confirm: (userId, token) => {
        return requests.get(`${BASE_URL}/confirm`, {
            params: {
                id: userId,
                token
            }
        });
    },
    changePicture: (imageUrl) => {
        return requests.put(`${BASE_URL}/picture`, {url: imageUrl});
    } 
}

export default AccountAPI;