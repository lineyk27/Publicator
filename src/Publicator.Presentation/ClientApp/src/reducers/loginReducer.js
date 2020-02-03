import {
    LOGIN_SUCCESS,
    LOGIN_BEGIN,
    LOGIN_WRONG_CREDENTIALS,
    LOGIN_USER_NOT_FOUND,
    LOGIN_FAILURE
} from "./actionTypes";

const initialState = {isAuthorized : false, userInfo : {}, loading: false, error: null};

export default (state = initialState, action) => {
    switch(action.type){
        case LOGIN_FAILURE:
        case LOGIN_USER_NOT_FOUND:
        case LOGIN_WRONG_CREDENTIALS:
            return {
                ...state,
                error: LOGIN_FAILURE
            }
        case LOGIN_BEGIN:
            return {
                ...state,
                loading: true
            };
        case LOGIN_SUCCESS:
            return {
                ...state,
                isAuthorized: true,
                userInfo: action.payload
            };
    }
}
