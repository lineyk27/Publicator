import { 
    POST_VIEW_LOAD,
    POST_VIEW_UNLOAD
} from "../actionTypes";
import PostsAPI from "../api/postsApi";

const postViewLoad = (post) => ({
    type: POST_VIEW_LOAD,
    postInfo: post
});

const postViewUnload = () => ({
    type: POST_VIEW_UNLOAD
});

function loadPostView(postId){
    return dispatch => {
        return PostsAPI.byId(postId)
            .then(response => {
                let post = response.data;
                console.log("Post was loaded: ")
                console.log(response.data);
                dispatch(postViewLoad(post));
            }).catch(error => {
                console.log(error.response.status, error.response.data.message);
                dispatch(postViewUnload());
            });
    }
};

function unloadPostView(){
    return dispatch => {
        dispatch(postViewUnload());
    }
}

export {
    loadPostView,
    unloadPostView
};