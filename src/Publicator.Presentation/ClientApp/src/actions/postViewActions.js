import { 
    POST_VIEW_BEGIN,
    POST_VIEW_SUCCESFULL,
    POST_VIEW_FAILURE
} from "../actionTypes";
import PostsAPI from "../api/postsApi";

const postViewBegin = () => ({
    type: POST_VIEW_BEGIN
});

const postViewSuccesfull = (post) => ({
    type: POST_VIEW_SUCCESFULL,
    payload: post
});

const postViewFailure = () => ({
    type: POST_VIEW_FAILURE
});

function loadPostView(postId){
    return dispatch => {
        dispatch(postViewBegin());
        return PostsAPI.byId(postId)
            .then(response => {
                let post = response.data;
                dispatch(postViewSuccesfull(post));
            }).catch(error => {
                console.log(error.response.status, error.response.data.message);
                dispatch(postViewFailure());
            });
    }
};

export {
    loadPostView
};