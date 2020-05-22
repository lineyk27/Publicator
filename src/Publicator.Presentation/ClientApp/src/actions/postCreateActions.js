import PostsAPI from "../api/postsApi";
import { loadPostView } from "./postViewActions"

function createPost(name, content, communityId, tags) {
    return dispatch => {
        return PostsAPI.createNew(name, content, communityId, tags)
            .then(response => {
                console.log(response.data.id);
                return response.data.id;
            }).catch(error => {
                console.log(error);
            });
    }
}

export default createPost;