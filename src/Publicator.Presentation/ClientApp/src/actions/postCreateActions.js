import {
    POST_CREATE_BEGIN,
    POST_CREATE_CHANGE
} from "../actionTypes";
import PostsAPI from "../api/postsApi";

const postCreateBegin = () => ({
    type: POST_CREATE_BEGIN
});

const postCreateSuccesfull = (postid) => ({
    type: POST_CREATE_CHANGE,
    postId: postid
});

function createPost(name, content, communityId, tags){
    return dispatch => {
        dispatch(postCreateBegin());
        var postId = "";
        PostsAPI.createNew(name, content, communityId, tags)
            .then(response => {
                postId = response.data.postid;
            }).catch(error => {
                console.log(error.response.data);
            });
        dispatch(postCreateSuccesfull(postid));
        return postId;
    };
}

export default createPost;