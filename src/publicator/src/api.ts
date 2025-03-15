import Axios, { AxiosInstance, AxiosRequestConfig } from "axios";
import SubscribeToUser from "./Models/SubscribeToUser";
import ConfirmEmail from "./Models/ConfirmEmail";
import HotPeriod from "./Models/HotPeriodEnum";
import Login from "./Models/Login";
import NewComment from "./Models/NewComment";
import NewPost from "./Models/NewPost";
import NewVote from "./Models/NewVote";
import PageRequest from "./Models/PageRequest";
import Register from "./Models/Register";
import { getToken }  from "./utils/token"

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

const CommentsApiUrl = `${BaseApiUrl}/comments`;

const Comments = {
    getByPost: (postId: string) => createAxios({}).get(`${CommentsApiUrl}/${postId}`),
    create: (comment: NewComment) => createAxios({}).post(`${BookmarksApiUrl}/create`, comment)
}

const CommunitiesApiUrl = `${BaseApiUrl}/Communities`;

const Communities = {
    byId: (communityId: string) => createAxios({}).get(`${CommunitiesApiUrl}/${communityId}`),
    all: () => createAxios({}).get(`${CommunitiesApiUrl}/all`)
}

const PostsApiUrl = `${BaseApiUrl}/communities`;

const Posts = {
    hot: (period: HotPeriod, page: number, pageSize: number) => createAxios({}).get(`${PostsApiUrl}/hot`, {params: {period, page, pageSize}}),
    new: (paged: PageRequest) => createAxios({}).get(`${PostsApiUrl}/new`, {params: paged}),
    bySubscription: (paged: PageRequest) => createAxios({}).get(`${PostsApiUrl}/subscription`, {params: paged}),
    byId: (id: string) => createAxios({}).get(`${PostsApiUrl}/${id}`),
    byCreatorUser: (username: string, page: number, pageSize: number) => createAxios({}).get(`${PostsApiUrl}/user`, {params: {username, page, pageSize}}),
    byCommunity: (communityId: string, page: number, pageSize: number) => createAxios({}).get(`${PostsApiUrl}/community`, {params: {communityId, page, pageSize}}),
    create: (post: NewPost) => createAxios({}).post(`${PostsApiUrl}/create`, post)
}

const UsersApiUrl = `${BaseApiUrl}/users`;

const Users = {
    currentSubscription: (username: string) => createAxios({}).get(`${UsersApiUrl}/currentSubscription`, {params: {username}}),
    subscribe: (user: SubscribeToUser) => createAxios({}).put(`${UsersApiUrl}/subscribe`, null, {params: user}),
    byUsername: (username: string) => createAxios({}).get(`${UsersApiUrl}`, {params: {username}}),
    byPost: (postId: string) => createAxios({}).get(`${UsersApiUrl}/post`, {params: {postId}})
}

const VotesApiUrl = `${BaseApiUrl}/votes`;

const Votes = {
    byPost: (postId: string) => createAxios({}).get(`${VotesApiUrl}/current`, {params: {postId}}),
    vote: (vote: NewVote) => createAxios({}).put(`${VotesApiUrl}/vote`, vote),
    currentRating: (postId: string) => createAxios({}).get(`${VotesApiUrl}/rating`, {params: {postId}})
}

export {
    Account,
    Bookmarks,
    Comments,
    Communities,
    Posts,
    Users,
    Votes
}
