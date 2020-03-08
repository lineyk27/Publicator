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
            console.log(response.data);
            result = registerResult(true, null)
        }).catch(error => {
            console.log(error.response.data.message);
            result = registerResult(false, error.response.data.message);
        });
    return result;
}

export {
    register,
    confirmAccount
}
