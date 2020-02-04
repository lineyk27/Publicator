import { 
    POST_CATALOG_BEGIN,
    POST_CATALOG_FAILURE,
    POST_CATALOG_SUCCESFULL,
    POST_CATALOG_TYPE_SUBSCRIPTION,
    POST_CATALOG_TYPE_NEW,
    POST_CATALOG_TYPE_HOT,
    POST_CATALOG_TYPE_BY_COMMUNITY,
    POST_CATALOG_TYPE_BY_SEARCH,
    POST_CATALOG_TYPE_USER_SUBSCRIPTION
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

function loadBySubscriptionPostCatalog(username, page, pageSize){
    return dispatch => {
        dispatch(postCatalogBegin(POST_CATALOG_TYPE_USER_SUBSCRIPTION));
        return PostsAPI.bySubscription(username, page, pageSize)
            .then(response => {
                let posts = response.data;
                dispatch(postCatalogSuccess(posts, POST_CATALOG_TYPE_USER_SUBSCRIPTION));
            }).catch(error => {
                console.log(error.status, error.data.message);
            });
    }
}

function loadByCommunityPostCatalog(communityId, page, pageSize){
    return dispatch => {
        dispatch(postCatalogBegin(POST_CATALOG_TYPE_BY_COMMUNITY));
        return PostsAPI.byCommunity(communityId, page, pageSize)
            .then(response => {
                let posts = response.data;
                dispatch(postCatalogSuccess(posts, POST_CATALOG_TYPE_BY_COMMUNITY));
            }).catch( error => {
                dispatch(postCatalogFailure(POST_CATALOG_TYPE_BY_COMMUNITY));
                console.log(error.status, error.data.message);
            });
    }
}

function loadBySearchPostCatalog(query, startdate, enddate, page, pageSize){
    return dispatch => {
        dispatch(postCatalogBegin(POST_CATALOG_TYPE_BY_SEARCH));
        return PostsAPI.bySearch(query, startdate, enddate, page, pageSize)
            .then(response => {
                let posts = response.data;
                dispatch(postCatalogSuccess(posts, POST_CATALOG_TYPE_BY_SEARCH));
            }).catch(error => {
                console.log(error.status, error.data.message);
                dispatch(postCatalogFailure(POST_CATALOG_TYPE_BY_SEARCH));
            });
    };
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
    loadPostCatalog,
    loadBySearchPostCatalog,
    loadBySubscriptionPostCatalog,
    loadByCommunityPostCatalog
}