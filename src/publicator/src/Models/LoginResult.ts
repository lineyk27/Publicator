import LoginResultEnum from "./LoginResultEnum"

export default interface LoginResult{
    result: LoginResultEnum,
    token: string
}