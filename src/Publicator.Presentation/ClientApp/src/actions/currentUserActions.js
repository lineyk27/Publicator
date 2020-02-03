import AccountAPI from "./../api/accountApi";
import { loginSuccess, loginFailure } from "./loginActions";

// TODO: must be reconsidered
export const setCurrent = () => {
    return dispatch => {
        AccountAPI.current()
            .then((response) => {
                console.log(response.data);
                dispatch(loginSuccess(response.data))
            })
            .catch(error => {
                console.log(error);
                dispatch(loginFailure());
            });
    };
}