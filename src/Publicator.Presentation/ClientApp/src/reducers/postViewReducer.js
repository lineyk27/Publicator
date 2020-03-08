import {
    POST_VIEW_LOAD,
    POST_VIEW_UNLOAD,
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
        default:
            return state;
    }
}

export default postViewReducer;