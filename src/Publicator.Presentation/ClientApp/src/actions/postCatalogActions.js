import { 
    POST_CATALOG_BEGIN,
    POST_CATALOG_FAILURE,
    POST_CATALOG_SUCCESFULL,
    POST_CATALOG_TYPE_SUBSCRIPTION,
    POST_CATALOG_TYPE_NEW,
    POST_CATALOG_TYPE_HOT
} from "../actionTypes";
import PostsAPI from "../api/postsApi";

const postCatalogBegin = (catalogType) => ({
    type: POST_CATALOG_BEGIN,
    catalogType: catalogType
});

const postCatalogSuccess = (posts, catalogType) => ({
    type: POST_CATALOG_SUCCESFULL,
    catalogType: catalogType,
    payload: posts
});

const postCatalogFailure = (catalogType) => ({
    type: POST_CATALOG_FAILURE,
    catalogType: catalogType
});

function loadPostCatalog(catalogType, page, pageSize, period){
    return dispatch => {
        dispatch(postCatalogBegin(catalogType));
        let method = methodByCatalogType(catalogType);
        return method(page=page, pageSize=pageSize, period=period)
            .then(response => {
                // TODO must be reconsidered
                let posts = response.data;
                dispatch(postCatalogSuccess(posts, catalogType));
            })
            .catch(error => {
                // TODO must be reconsidered
                console.log(error.status, error.data.message);
                dispatch(postCatalogFailure(catalogType));
            })
    }
}

function methodByCatalogType(catalogType){
    switch(catalogType){
        case POST_CATALOG_TYPE_NEW:
            return PostsAPI.new;
        case POST_CATALOG_TYPE_SUBSCRIPTION:
            return PostsAPI.bySubscription;
        case POST_CATALOG_TYPE_HOT:
        default:
            return PostsAPI.hot;

    }
}

export {
    loadPostCatalog
}