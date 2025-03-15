import { Url } from "url";

export default interface User{
    id: string,
    nickname: string,
    joinDate: Date,
    imageUrl: Url,
    role: any
}