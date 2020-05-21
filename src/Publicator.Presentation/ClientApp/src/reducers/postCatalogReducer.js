import { 
    POST_CATALOG_LOAD,
    POST_CATALOG_UNLOAD,
    POST_CATALOG_END
} from "../actionTypes";

const initialState = {posts: [], lastPage: 0, catalogType: null, end: false};

function postCatalogReducer(state=initialState, action){
    switch(action.type){
        case POST_CATALOG_LOAD:
            if(action.type !== state.catalogType){
                return{
                    posts: action.posts,
                    lastPage: action.page,
                    catalogType: action.catalogType,
                    end: false
                };
            }
            return {
                ...state,
                posts: [...state.posts, ...action.posts],
                lastPage: action.page,
            }
        case POST_CATALOG_UNLOAD:
            return initialState;
        case POST_CATALOG_END:
            if(state.catalogType !== action.catalogType){
                return{
                    posts: [],
                    lastPage: action.page,
                    end: true,
                    catalogType: action.catalogType
                }
            }
            return {
                ...state,
                end: true
            }
        default:
            return state;
    }
}

export default postCatalogReducer;