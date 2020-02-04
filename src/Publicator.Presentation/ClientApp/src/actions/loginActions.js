import { 
    LOGIN_BEGIN,
    LOGIN_FAILURE,
    LOGIN_SUCCESFULL
} from "../actionTypes";
import AccountAPI from "../api/accountApi";
import { setToken } from "../api";

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

function login(login, password){
    return dispatch => {
        dispatch(loginBegin());
        return AccountAPI.login(login, password)
            .then( response => {
                // TODO: must be reconsidered
                let token = response.data.data;
                setToken(token);
                // must be reconsidered
                setCurrent()(dispatch);
            })
            .catch(error => {
                console.log(error);
                dispatch(loginFailure());
            })
    };
}

function setCurrent(){
    return dispatch => {
        dispatch(loginBegin());
        AccountAPI.current()
            .then( response => {
                let userInfo = response.data;
                dispatch(loginSuccesfull(userInfo));
            })
            .catch(error => {
                console.log(error.status, error.data.Message)
                dispatch(loginFailure());
            })
    }
}

export {
    setCurrent, 
    login
};
