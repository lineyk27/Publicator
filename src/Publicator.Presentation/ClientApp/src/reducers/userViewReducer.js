import {
    USER_VIEW_LOAD,
    USER_VIEW_UNLOAD,
} from "../actionTypes";

const initialState = {userInfo: null, loading: false};

function userViewReducer(state=initialState, action){
    switch(action.type){
        case USER_VIEW_LOAD:
            return {
                userInfo: action.userInfo,
                loading: false
            };
        case USER_VIEW_UNLOAD:
        default:
            return state;
    }
}

export default userViewReducer;