import Axios, { AxiosInstance, AxiosRequestConfig } from "axios";
import { create } from "domain";
import { config } from "process";
import ConfirmEmail from "./Models/ConfirmEmail";
import Login from "./Models/login";
import Register from "./Models/Register";

let tokenName = "Authorization";

const getToken = () : string | null => {
    return localStorage.getItem(tokenName);
}

const setToken = (token: string) : void=> {
    if(!token.includes("Bearer")){
        token = "Bearer " + token;
    }
    localStorage.setItem(tokenName, token);
}

const createAxios = (startConfig: AxiosRequestConfig) : AxiosInstance => {
    let instance = Axios.create(startConfig);
    let token = getToken();
    if(token != null){
        instance.defaults.headers = {
            tokenName: token
        }
    };
    return instance;
}

const BaseApiUrl = "/api";

const AccountApiUrl = `${BaseApiUrl}/account`;

const Account = {
    login: (login: Login) => 
        createAxios({}).post(`${AccountApiUrl}/login`,login),
    register: (register: Register) => 
        createAxios({}).post(`${AccountApiUrl}/register`, register),
    confirmEmail: (confirm: ConfirmEmail) => 
        createAxios({}).get(`${AccountApiUrl}/confirm`, { params: confirm }),
    current: () => createAxios({}).get(`${AccountApiUrl}/current`)
}

const BookmarksApiUrl = `${BaseApiUrl}/bookmarks`;

const Bookmarks = {
    allBookmarks: () => createAxios({}).get(`${BookmarksApiUrl}/current`),
    create: (postId: string) => createAxios({}).put(`${BookmarksApiUrl}/create`, null, {params: {postId}})
}






