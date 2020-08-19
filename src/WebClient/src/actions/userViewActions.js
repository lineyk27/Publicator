import { USER_VIEW_LOAD, USER_VIEW_UNLOAD, USER_SUBSCRIPTION_LOAD } from "../actionTypes";
import UsersAPI from "../api/usersApi"

const loadUserView = (user) => ({
    type: USER_VIEW_LOAD,
    user: user
})

const unloadUserView = () => ({
    type: USER_VIEW_UNLOAD,
})

const loadUserSubscription = (state) => ({
    type: USER_SUBSCRIPTION_LOAD,
    isSubscribed: state
})

function loadUser(nickname){
    return dispatch => {
        return UsersAPI.byUsername(nickname)
            .then(response => {
                let user = response.data;
                dispatch(loadUserView(user));
            }).catch(error => {
                console.log(error);
            });
    };
}

function loadSubscruption(nickname){
    return dispatch => {
        return UsersAPI.currentSubscription(nickname)
            .then(response => {
                console.log(response);
                let currstate = response.data.state;
                dispatch(loadUserSubscription(currstate))
            }).catch(error => {
                console.log(error);
            });
    } 
}

function subscribe(nickname){
    return dispatch => {
        return UsersAPI.subscribe(nickname)
            .then(response => {
                let currstate = response.data.state;
                dispatch(loadUserSubscription(currstate));
            }).catch(error => {
                console.log(error);
            })
    }
}

function unloadUser(){
    return dispatch => {
        dispatch(unloadUserView());
    }
}

export {
    loadUser,
    unloadUser,
    loadSubscruption,
    subscribe
}
