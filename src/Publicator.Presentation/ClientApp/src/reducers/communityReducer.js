import {
    COMMUNITY_VIEW_LOAD,
    COMMUNITY_VIEW_UNLOAD,
    COMMUNITIES_CATALOG_LOAD,
    COMMUNITIES_CATALOG_UNLOAD
} from '../actionTypes';

const initialState = {communityInfo: null, communities: null};

function communityReducer(state=initialState, action){
    switch(action.type){
        case COMMUNITY_VIEW_LOAD:
            return {
                ...state,
                communityInfo: action.communityInfo
            };
        case COMMUNITY_VIEW_UNLOAD:
            return initialState;
        case COMMUNITIES_CATALOG_LOAD:
            return {
                ...state,
                communities: action.communities
            };
        case COMMUNITIES_CATALOG_UNLOAD:
            return {
                ...state,
                communities: null
            };
        default:
            return state;
    }
}

export default communityReducer;