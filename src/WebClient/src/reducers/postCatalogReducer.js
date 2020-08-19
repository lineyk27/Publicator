import { 
    POST_CATALOG_LOAD,
    POST_CATALOG_UNLOAD,
    POST_CATALOG_END
} from "../actionTypes";

const initialState = {posts: [], lastPage: 0, catalogType: null, end: false};

function postCatalogReducer(state=initialState, action){
    switch(action.type){
        case POST_CATALOG_LOAD:
            if(action.catalogType !== state.catalogType){
                console.log("in action type non equalnes");
                console.log(`action type = ${action.catalogType}, state type = ${state.catalogType}`);
                return{
                    posts: action.posts,
                    lastPage: action.page,
                    catalogType: action.catalogType,
                    end: false
                };
            }
            console.log("Catalog types are equal");
            return {
                ...state,
                posts: [...state.posts, ...action.posts],
                lastPage: action.page,
            }
        case POST_CATALOG_UNLOAD:
            return initialState;
        case POST_CATALOG_END:
            if(state.catalogType !== action.catalogType){
                console.log("Types are not equal!");
                console.log(action.catalogType);
                console.log(state.catalogType);
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