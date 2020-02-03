import { 
    LOGIN_BEGIN,
    LOGIN_FAILURE,
    LOGIN_USER_NOT_FOUND,
    LOGIN_WRONG_CREDENTIALS,
    LOGIN_SUCCESFULL
} from "../actionTypes";

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

const login = (login, password) => {
    // todo: add loggining from user api
}
