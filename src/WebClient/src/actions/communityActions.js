import {
    COMMUNITY_VIEW_LOAD,
    COMMUNITY_VIEW_UNLOAD,
    COMMUNITIES_CATALOG_LOAD,
    COMMUNITIES_CATALOG_UNLOAD
} from "../actionTypes";

import CommunitiesAPI from "../api/communitiesApi";

const communityViewUnload = () => ({
    type: COMMUNITY_VIEW_UNLOAD
});

const communityViewLoad = (community) => ({
    type: COMMUNITY_VIEW_LOAD,
    communityInfo: community
});

const communitiesCatalogLoad = (communities) => ({
    type: COMMUNITIES_CATALOG_LOAD,
    communities: communities
});

const communitiesCatalogUnload = () => ({
    type: COMMUNITIES_CATALOG_UNLOAD
});

export function loadCommunityView(communityId){
    return dispatch => {
        return CommunitiesAPI.byId(communityId)
            .then(response => {
                dispatch(communityViewLoad(response.data));
            }).catch(error => {
                console.log(error.response.status, error.response.data.message);
                dispatch(communityViewUnload());
            });
    }
}

export function loadCommunitiesCatalog(){
    return dispatch => {
        return CommunitiesAPI.all()
            .then(response => {
                let communities = response.data;
                dispatch(communitiesCatalogLoad(communities));
            }).catch(error => {
                console.log(error);
            })
    }
}

export function unloadCommunitiesCatalog(){
    return dispatch => {
        dispatch(communitiesCatalogUnload);
    }
};

export function unloadCommunityView(){
    return dispatch => {
        dispatch(communityViewUnload);
    }
};
