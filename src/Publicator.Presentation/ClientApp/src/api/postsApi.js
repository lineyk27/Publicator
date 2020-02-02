import requests from ".";

const BASE_URL = "/api/posts";

export var PostsAPI = {
    hot : (period, page, pageSize = 10) => {
        return requests.get(`${BASE_URL}/hot`, {
            params : { period, page, pageSize }
        });
    },
    bySubscription : (username, page, pageSize = 10) => {
        return requests.get(`${BASE_URL}/subscription`, {
            params : { username, page, pageSize }
        });
    },
    newPosts : (page, pageSize = 10) => {
        return requests.get(`${BASE_URL}/new`, {
            params : { page, pageSize }
        });
    },
    byId : (postId) => {
        return requests.get(`${BASE_URL}/${postId}`);
    },
    byUser : (username, page, pageSize = 10) => {
        return requests.get(`${BASE_URL}/user`, {
            params : { username, page, pageSize }
        });
    },
    byCommunity : (communityId, page, pageSize = 10) => {
        return requests.get(`${BASE_URL}/community`, {
            params : { communityId, page, pageSize}
        });
    },
    bySearch : (query, startDate, endDate, page, communityId = null, pageSize = 10) => {
        return requests.get(`${BASE_URL}/search`, { params : { query, startDate, communityId, endDate, page, pageSize }
        })
    },
    create : (name, content, tags,communityId = null) => {
        return requests.post(`${BASE_URL}/create`, { name, content, tags, communityId 
        });
    }
};