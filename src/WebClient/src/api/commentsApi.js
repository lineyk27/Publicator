import { requests } from "./index";

const BASE_URL = "/api/comments"

const CommentsAPI = {
    byPost: (postId) => {
        return requests.get(`${BASE_URL}/post`, {
            params: {postId},
        });
    },
    create: (postId, text, parentCommentId) => {
        return requests.post(`${BASE_URL}/create`, {postId, text, parentCommentId});
    }
}

export default CommentsAPI;