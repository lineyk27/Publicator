import { Url } from "url";

export default interface Community{
    id: string,
    name: string,
    description: string,
    imageUrl: Url
}