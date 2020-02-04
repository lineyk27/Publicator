import { requests } from "./index";

const BASE_URL = "/api/communities"
// TODO: add suggested communities
const CommunitiesAPI = {
    byId: (communityId) => {
        return requests.get(`${BASE_URL}/${communityId}`);
    }
}
export default CommunitiesAPI;