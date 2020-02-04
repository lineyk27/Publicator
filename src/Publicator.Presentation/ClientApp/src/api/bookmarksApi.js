import {requests} from "./index";

const BASE_URL = "/api/bookmarks";

const BookmarksAPI = {
    bookmarks: () => {
        return requests.get(`${BASE_URL}/current`);
    },
    create: (postId) => {
        return requests.put(`${BASE_URL}/create`, {postId});
    }
}

export default BookmarksAPI;