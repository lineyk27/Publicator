import requests from ".";

export var UsersAPI = {
    byUsername : (username) => {
        return requests.get(`/api/users/${username}`)
    },
    byPost : (postId) => {
        return requests.get("/api/users/post", {
            params : {
                postId : postId
            }
        });
    },
    subscribe : (postId) => {
        return requests.put(`/api/users/subscribe/${postId}`);
    }
}