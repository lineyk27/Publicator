import { User } from ".";

export default interface Comment{
    id: string,
    content: string,
    creationTime: string,
    creatorUser: User,
    replies: Array<Comment>
}