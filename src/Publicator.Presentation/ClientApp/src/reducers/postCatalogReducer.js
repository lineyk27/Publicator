import { 
    POST_CATALOG_LOAD,
    POST_CATALOG_UNLOAD
} from "../actionTypes";

const initialState = {posts: [], lastPage: null, catalogType: null, end: false};

function postCatalogReducer(state=initialState, action){
    switch(action.type){
        case POST_CATALOG_LOAD:
            return {
                posts: [...action.posts, ...state.posts],
                catalogType: action.catalogType,
                lastPage: action.page
            };
        case POST_CATALOG_UNLOAD:
            return initialState;
        case POST_CATALOG_END:
            return {
                ...state,
                end: true
            };
        default:
            return state;
    }
}

export default postCatalogReducer;