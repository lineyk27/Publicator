import {
    POST_VIEW_LOAD,
    POST_VIEW_UNLOAD,
    POST_VOTE_SUCCESFULL,
    POST_VOTE_BEGIN,
    POST_VOTE_FAILURE,
    POST_BOOKMARK_SUCCESFULL,
    POST_BOOKMARK_FAILURE,
    POST_BOOKMARK_BEGIN
} from "../actionTypes";

const initialState = {postInfo: null};

function postViewReducer(state=initialState, action){
    switch(action.type){
        case POST_VIEW_LOAD:
            return {
                postInfo: action.postInfo
            };
        case POST_VIEW_UNLOAD:
            return initialState;
        case POST_VOTE_SUCCESFULL:
            state = {
                postInfo: {
                    ...state.postInfo,
                    currentVote: action.vote
                }
            }
            return state;
        case POST_BOOKMARK_SUCCESFULL:
            state = {
                postInfo: {
                    ...state.postInfo,
                    currentBookmark: {
                        bookmarked: action.bookmarked
                    }
                }
            }
            console.log(state);
            return state;
        default:
            return state;
    }
}

export default postViewReducer;