import axios from "axios";

const requests = axios.create({
    baseURL: "http://localhost:5000/",
    headers: {"Authorization": getToken()}
});

const getToken = () => {
    return window.localStorage.getItem("Authorization");
};

export const setToken = (token) => {
    window.localStorage.setItem("Authorization", token);
}

export default requests;
