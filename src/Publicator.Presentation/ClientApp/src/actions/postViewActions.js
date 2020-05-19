import { 
    POST_VIEW_LOAD,
    POST_VIEW_UNLOAD,
    POST_BOOKMARK_BEGIN,
    POST_BOOKMARK_FAILURE,
    POST_BOOKMARK_SUCCESFULL,
    POST_VOTE_BEGIN,
    POST_VOTE_FAILURE,
    POST_VOTE_SUCCESFULL,
    POST_RATING_UPDATE
} from "../actionTypes";
import PostsAPI from "../api/postsApi";
import BookmarkAPI from "../api/bookmarksApi";
import VotesAPI from "../api/votesApi";

const postViewLoad = (post) => ({
    type: POST_VIEW_LOAD,
    postInfo: post
});

const postViewUnload = () => ({
    type: POST_VIEW_UNLOAD
});

const postBookmarkBegin = () => ({
    type: POST_BOOKMARK_BEGIN
});

const postBookmarkFailure = () => ({
    type: POST_BOOKMARK_FAILURE
});

const postBookmarkSuccesfull = (bookmarked) => ({
    type: POST_BOOKMARK_SUCCESFULL,
    bookmarked
});

const votePostBegin = () => ({
    type: POST_VOTE_BEGIN
})

const votePostFailure = () => ({
    type: POST_VOTE_FAILURE
})

const votePostSuccesfull = (vote) => ({
    type: POST_VOTE_SUCCESFULL,
    vote
})

const updateRatingSuccesfull = (rating) => ({
    type: POST_RATING_UPDATE,
    rating
})

function votePost(postId, up){
    return dispatch => {
        dispatch(votePostBegin());
        VotesAPI.create(postId, up)
            .then(response => {
                let vote = response.data;
                dispatch(votePostSuccesfull(vote));
                dispatch(updateRating(postId));
            }).catch(error => {
                console.log(error);
                dispatch(votePostFailure());
            })
    }
}

function updateRating(postId){
    return dispatch => {
        VotesAPI.currentRating(postId)
            .then(response => {
                let rating = response.data.currentRating;
                dispatch(updateRatingSuccesfull(rating));
            }).catch(error => {
                console.log(error);
            });
    }
}

function bookmarkPost(postId){
    return dispatch => {
        dispatch(postBookmarkBegin());
        return BookmarkAPI.create(postId)
            .then(response => {
                console.log("In bookmark post action");
                console.log(response);
                let bookmarked = response.data.state;
                dispatch(postBookmarkSuccesfull(bookmarked));
            }).catch(error => {
                console.log(error);
                dispatch(postBookmarkFailure());
            });
    }
}

function loadPostView(postId){
    return dispatch => {
        return PostsAPI.byId(postId)
            .then(response => {
                let post = response.data;
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
    unloadPostView,
    bookmarkPost,
    votePost,
    updateRating
};