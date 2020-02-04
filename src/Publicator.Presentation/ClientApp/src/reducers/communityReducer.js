import {
    COMMUNITY_VIEW_BEGIN,
    COMMUNITY_VIEW_FAILURE,
    COMMUNITY_VIEW_SUCCESFULL
} from '../actionTypes';

const initialState = {loading: false, community: null};

function communityReducer(state=initialState, action){
    switch(action.type){
        case COMMUNITY_VIEW_BEGIN:
            return {
                ...state,
                loading: true
            };
        case COMMUNITY_VIEW_SUCCESFULL:
            return {
                community: action.payload,
                loading: false
            };
        case COMMUNITY_VIEW_FAILURE:
            return {
                ...state,
                loading: false
            };
        default:
            return state;
    }
}

export default communityReducer;