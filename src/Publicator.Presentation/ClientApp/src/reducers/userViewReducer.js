import {
    USER_VIEW_BEGIN,
    USER_VIEW_FAILURE,
    USER_VIEW_SUCCESFULL
} from "../actionTypes";

const initialState = {user: null, loading: false};

function userViewReducer(state=initialState, action){
    switch(action.type){
        case USER_VIEW_BEGIN:
            return {
                ...state,
                loading: true
            };
        case USER_VIEW_SUCCESFULL:
            return {
                user: action.payload,
                loading: false
            };
        case USER_VIEW_FAILURE:
        default:
            return state;
    }
}

export default userViewReducer;