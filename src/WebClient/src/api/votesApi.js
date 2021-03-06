import {requests} from "./index";

const BASE_URL = "/api/votes";

const VotesAPI = {
    current: (postId) => {
        return requests.get(`${BASE_URL}/current`, {postId});
    },
    create: (postId, up) => {
        return requests.put(`${BASE_URL}/vote`, {postId, up});
    },
    currentRating: (postId) => {
        return requests.get(`${BASE_URL}/rating?postId=${postId}`);
    }
}

export default VotesAPI;