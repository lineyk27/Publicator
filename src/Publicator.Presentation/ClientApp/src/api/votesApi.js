import requests from ".";
const BASE_URL = "/api/votes";

export var VotesAPI = {
    current : (postId) => {
        return requests.get(`${BASE_URL}/current`, {params : {postId}});
    },
    makeVote : (postId, up) => {
        return requests.put(`${BASE_URL}/vote`, null, { params : {postId, up} });
    },

}