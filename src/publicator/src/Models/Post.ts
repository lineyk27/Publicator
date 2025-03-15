import { Bookmark, Community, User, Vote } from ".";
import Tag from "./Tag";

export default interface Post{
    id: string,
    name: string,
    content: string,
    creatorUser: User,
    creationDate: string,
    currentRating: number,
    community: Community,
    currentVote: Vote,
    currentBookmark: Bookmark,
    tags: Array<Tag>
}