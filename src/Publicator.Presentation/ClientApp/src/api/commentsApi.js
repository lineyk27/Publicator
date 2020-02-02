import requests from ".";

const BASE_URL = "/api/comments";

export var CommentsAPI = {
    byPost : (postId, page, pageSize) => {
        return requests.get(`${BASE_URL}/post`, {
            params : { postId,page, pageSize }
        });
    },
    create : (postId, text, parentCommentId = null) => {
        return requests.post(`${BASE_URL}/create`, { postId, text, parentCommentId });
    }
};