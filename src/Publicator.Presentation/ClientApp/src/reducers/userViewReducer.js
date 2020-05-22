import {
    USER_VIEW_LOAD,
    USER_VIEW_UNLOAD,
    USER_SUBSCRIPTION_LOAD
} from "../actionTypes";

const initialState = {userInfo: null, loading: false, isSubscribed: false};

function userViewReducer(state=initialState, action){
    switch(action.type){
        case USER_VIEW_LOAD:
            return {
                ...state,
                userInfo: action.user,
            };
        case USER_SUBSCRIPTION_LOAD:
            return{
                ...state,
                isSubscribed: action.isSubscribed
            }
        case USER_VIEW_UNLOAD:
        default:
            return state;
    }
}

export default userViewReducer;