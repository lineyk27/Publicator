import {
    COMMUNITY_VIEW_SUCCESFULL,
    COMMUNITY_VIEW_FAILURE,
    COMMUNITY_VIEW_BEGIN
} from "../actionTypes";

import CommunitiesAPI from "../api/communitiesApi";

const communityViewBegin = () => ({
    type: COMMUNITY_VIEW_BEGIN
});

const communityViewFailure = () => ({
    type: COMMUNITY_VIEW_FAILURE
});

const communityViewSuccesfull = (community) => ({
    type: COMMUNITY_VIEW_SUCCESFULL,
    payload: community
});

function loadCommunityView(communityId){
    return dispatch => {
        dispatch(communityViewBegin());
        return CommunitiesAPI.byId(communityId)
            .then(response => {
                dispatch(communityViewSuccesfull(response.data));
            }).catch(error => {
                console.log(error.response.status, error.response.data.message);
                dispatch(communityViewFailure());
            });
    }
}
