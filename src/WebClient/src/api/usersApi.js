import { requests } from "./index";

const BASE_URL = "/api/users";
// TODO: add more methods
const UsersAPI = {
    byUsername: (username) => {
        return requests.get(`${BASE_URL}`, {params: {username}});
    },
    subscribe: (username) => {
        return requests.put(`${BASE_URL}/subscribe`, {username});
    },
    currentSubscription: username => {
        return requests.get(`${BASE_URL}/currentSubscription`,{params: {username}});
    }
}

export default UsersAPI;