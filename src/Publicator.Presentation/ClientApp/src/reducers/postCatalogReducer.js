import { 
    POST_CATALOG_BEGIN, 
    POST_CATALOG_FAILURE, 
    POST_CATALOG_SUCCESFULL 
} from "../actionTypes";

const initialState = {posts: [], catalogType: null, loading: false};

function postCatalogReducer(state=initialState, action){
    switch(action.type){
        case POST_CATALOG_SUCCESFULL:
            return {
                loading: false,
                posts: [action.payload, ...state.posts],
                catalogType: action.catalogType
            };
        case POST_CATALOG_FAILURE:
            return {
                ...state,
                loading: false,
            };
        case POST_CATALOG_BEGIN:
            return {
                ...state,
                loading: true,
                catalogType: action.catalogType
            };
        default:
            return state;
    }
}

export default postCatalogReducer;