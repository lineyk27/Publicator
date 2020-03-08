import {
    COMMUNITY_VIEW_LOAD,
    COMMUNITY_VIEW_UNLOAD,
} from '../actionTypes';

const initialState = {communityInfo: null};

function communityReducer(state=initialState, action){
    switch(action.type){
        case COMMUNITY_VIEW_LOAD:
            return {
                communityInfo: action.communityInfo
            };
        case COMMUNITY_VIEW_UNLOAD:
            return initialState;
        default:
            return state;
    }
}

export default communityReducer;