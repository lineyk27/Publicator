import { requests } from "./index";

const BASE_URL = "/api/posts"

const PostsAPI = {
    hot: (period, page, pageSize) => {
        return requests.get(`${BASE_URL}/hot`, {
            params: { period, page, pageSize}
        })
    },
    bySubscription: (username, page ,pageSize) => {
        return requests.get(`${BASE_URL}/subscription`, {
            params: {username, page, pageSize}
        });
    },
    new: (page, pageSize) => {
        return requests.get(`${BASE_URL}/new`, {
            params: {page, pageSize}
        });
    },
    byId: (postId) => {
        return requests.get(`${BASE_URL}/${postId}`);
    },
    byCreatorUser: (username, page, pageSize) => {
        return requests.get(`${BASE_URL}/user`, {
            params: {username, page, pageSize}
        });
    },
    byCommunity: (communityId, page, pageSize) => {
        return requests.get(`${BASE_URL}/community`, {
            params: {communityId, page, pageSize}
        })
    },
    bySearch: (query, startdate, enddate, page, pageSize) => {
        return requests.get(`${BASE_URL}/search`, {
            params: {query, startdate, enddate, page, pageSize}
        });
    },
    createNew: (name, content, communityId, tags) => {
        return requests.post(`${BASE_URL}/create`, {name, content, communityId, tags});
    }
}

export default PostsAPI;