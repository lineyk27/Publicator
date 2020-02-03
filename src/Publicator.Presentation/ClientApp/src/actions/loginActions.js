import {
    LOGIN_BEGIN,
    LOGIN_FAILURE,
    LOGIN_SUCCESS,
    LOGIN_USER_NOT_FOUND,
    LOGIN_WRONG_CREDENTIALS
} from "./../actionTypes";
import AccountAPI from "./../api/accountApi";
import {setToken} from "./../api";
import { setCurrent } from "./currentUserActions";

export const login = (login, password) => {
    return dispatch => {
        dispatch(loginBegin());
        return AccountAPI.login(login, password)
            .then((response) => {
                let token = response.data;
                // TODO: dont forget to remove it
                console.log(token);
                setToken(token);
                dispatch(setCurrent());
            })
            .catch((error) => {
                // TODO  add checking wrong credentials, or something other
                dispatch(loginFailure());
            });
    };
}

export const loginBegin = () => ({
    type: LOGIN_BEGIN
});

export const loginSuccess = (userInfo) = ({
    type: LOGIN_SUCCESS,
    payload: userInfo
})

export const loginFailure = () => ({
    type: LOGIN_FAILURE
});

export const loginWrong = () => ({
    type: LOGIN_WRONG_CREDENTIALS
});

export const loginNotFound = () => ({
    type: LOGIN_USER_NOT_FOUND
});