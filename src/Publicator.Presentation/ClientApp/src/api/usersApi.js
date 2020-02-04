import { requests } from "./index";

const BASE_URL = "/api/users";
// TODO: add more methods
const UsersAPI = {
    byUsername: (username) => {
        return requests.get(`${BASE_URL}/${username}`);
    },
    subscribe: (username) => {
        return requests.put(`${BASE_URL}`, {username});
    }
}

export default UsersAPI;