import requests from ".";

const BASE_URL = "/api/bookmarks";

export default BookmarksAPI = {
    byCurrent : () => {
        return requests.get(`${BASE_URL}/current`);
    },
    create : (postId) => {
        return requests.put(`${BASE_URL}/create`, {
            params : { postId }
        });
    }
}