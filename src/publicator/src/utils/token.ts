let tokenName = "Authorization";

const getToken = () : string | null => {
    return localStorage.getItem(tokenName);
}

const setToken = (token: string) : void=> {
    if(!token.includes("Bearer")){
        token = "Bearer " + token;
    }
    localStorage.setItem(tokenName, token);
}

export {
    tokenName,
    getToken,
    setToken
}