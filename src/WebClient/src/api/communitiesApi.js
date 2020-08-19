import { requests } from "./index";

const BASE_URL = "/api/communities"
// TODO: add suggested communities
const CommunitiesAPI = {
    byId: (communityId) => {
        return requests.get(`${BASE_URL}/${communityId}`);
    },
    bySearch: (query) => {
        return requests.get(`${BASE_URL}/search`, {params: {query}});
    },
    all: () => {
        return requests.get(`${BASE_URL}/all`);
    }
}

export default CommunitiesAPI;