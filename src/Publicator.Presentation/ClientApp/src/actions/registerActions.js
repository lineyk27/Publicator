import AccountAPI from "../api/accountApi";

const registerResult = (success, message) => ({
    success,
    message
});

function register(username, email, password, confirmPassword){
    return AccountAPI.register(username, email, password, confirmPassword)
        .then(() => {
            return registerResult(true, null);
        }).catch(error => {
            let message = error.status || "Sign up error";
            let code = error.status;
            console.log(`${code}: ${message}`);
            return registerResult(false, message);
        });
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
