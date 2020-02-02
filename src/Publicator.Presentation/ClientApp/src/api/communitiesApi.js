import requests from ".";

const BASE_URL = "/api/communities";

export var CommunitiesAPI = {
    changeCommunityPicture : (communityId, imageUrl) => {
        return requests.put(`${BASE_URL}/picture`, { communityId, imageUrl});
    },
    create : (name, description, imageUrl) => {
        return requests.post(`${BASE_URL}/create`, {name, description, imageUrl});
    },
    byId : (communityId) => {
        return requests.get(`${BASE_URL}/${communityId}`);
    }
}