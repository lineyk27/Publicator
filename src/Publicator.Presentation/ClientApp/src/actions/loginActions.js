import { 
    LOGIN_BEGIN,
    LOGIN_FAILURE,
    LOGIN_SUCCESFULL,
    LOGOUT
} from "../actionTypes";
import AccountAPI from "../api/accountApi";
import { setToken, removeToken } from "../api";

const loginBegin = () => ({
    type: LOGIN_BEGIN
})

const loginSuccesfull = (userInfo) => ({
    type: LOGIN_SUCCESFULL,
    payload: userInfo
})

const loginFailure = () => ({
    type: LOGIN_FAILURE
})

function login (login, password){
    return dispatch => {
        dispatch(loginBegin());
        return AccountAPI.login(login, password)
            .then(response => {
                // TODO: must be reconsidered
                console.log(response);
                let token = response.data;
                setToken(token);
                // must be reconsidered
                setCurrent()(dispatch);
            })
            .catch(error => {
                console.log("Error happened!")
                console.log(error);
                dispatch(loginFailure());
            })
    };
}

function setCurrent(){
    return dispatch => {
        dispatch(loginBegin());
        return AccountAPI.current()
            .then( response => {
                let userInfo = response.data;
                dispatch(loginSuccesfull(userInfo));
            })
            .catch(error => {
                console.log(error.status)
                dispatch(loginFailure());
            })
    }
}

function logout(){
    return dispatch => {
        removeToken();
        dispatch({ type: LOGOUT });
    }
}

export {
    setCurrent, 
    login,
    logout
};
