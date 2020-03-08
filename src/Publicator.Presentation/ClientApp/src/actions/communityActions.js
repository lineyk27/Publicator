import {
    COMMUNITY_VIEW_LOAD,
    COMMUNITY_VIEW_UNLOAD
} from "../actionTypes";

import CommunitiesAPI from "../api/communitiesApi";

const communityViewUnload = () => ({
    type: COMMUNITY_VIEW_UNLOAD
});

const communityViewLoad = (community) => ({
    type: COMMUNITY_VIEW_LOAD,
    communityInfo: community
});

function loadCommunityView(communityId){
    return dispatch => {
        // TODO: add ui/ux loading
        return CommunitiesAPI.byId(communityId)
            .then(response => {
                dispatch(communityViewLoad(response.data));
            }).catch(error => {
                console.log(error.response.status, error.response.data.message);
                dispatch(communityViewUnload());
            });
    }
}

function unloadCommunityView(){
    return dispatch => {
        dispatch(communityViewUnload);
    }
}

export default{
    loadCommunityView,
    unloadCommunityView
}
