import {
    POST_VIEW_BEGIN,
    POST_VIEW_FAILURE,
    POST_VIEW_SUCCESFULL
} from "../actionTypes";

const initialState = {post: null, loading: false};

function postViewReducer(state=initialState, action){
    switch(action.type){
        case POST_VIEW_BEGIN:
            return {
                ...state,
                loading: true
            };
        case POST_VIEW_SUCCESFULL:
            return {
                post: action.payload,
                loading: false
            };
        case POST_VIEW_FAILURE:
            return {
                post: null,
                loading: false
            };
        default:
            return state;
    }
}

export default postViewReducer;