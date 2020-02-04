import AccountAPI from "../api/accountApi";

const registerResult = (result, message) => ({
    result: result,
    message: message
});

function register(username, email, password, confirmPassword){
    var result;
    AccountAPI.register(username, email, password, confirmPassword)
        .then(() => {
            result = registerResult(true, null);
        }).catch(error => {
            let message = error.response.data.message;
            result = registerResult(false, message);
        });
    return result;
}

function confirmAccount(id, token){
    var result;
    AccountAPI.confirm(id, token)
        .then(response => {
            result = registerResult(true, null)
        }).catch(error => {
            result = registerResult(false, message);
        });
    return result;
}

export {
    register,
    confirmAccount
}
