import { 
    POST_CATALOG_TYPE_SUBSCRIPTION,
    POST_CATALOG_TYPE_NEW,
    POST_CATALOG_TYPE_HOT,
    POST_CATALOG_TYPE_BY_COMMUNITY,
    POST_CATALOG_TYPE_BY_SEARCH,
    POST_CATALOG_TYPE_USER_SUBSCRIPTION,
    POST_CATALOG_LOAD,
    POST_CATALOG_UNLOAD,
    POST_CATALOG_END
} from "../actionTypes";
import PostsAPI from "../api/postsApi";

const postCatalogLoad = (posts, catalogType, page) => ({
    type: POST_CATALOG_LOAD,
    catalogType: catalogType,
    posts: posts,
    page: page
});

const postCatalogUnload = () => ({
    type: POST_CATALOG_UNLOAD
});

const postCatalogEnd = () => ({
    type: POST_CATALOG_END
})

function loadPostCatalog(catalogType, page, pageSize, period){
    return dispatch => {
        // TODO: add load to ui/ux
        let method = methodByCatalogType(catalogType);
        return method(page=page, pageSize=pageSize, period=period)
            .then(response => {
                // TODO must be reconsidered
                let posts = response.data;
                // check here is empty
                if(response.data.length !== 0){
                    dispatch(postCatalogLoad(posts, catalogType, page));
                }
                else{
                    dispatch(postCatalogEnd());
                }
            })
            .catch(error => {
                // TODO must be reconsidered
                console.log(error.response.status, error.response.data.message);
                dispatch(postCatalogUnload());
            })
    }
}

function loadBySubscriptionPostCatalog(username, page, pageSize){
    return dispatch => {
        // TODO: add load to ui/ux
        return PostsAPI.bySubscription(username, page, pageSize)
            .then(response => {
                let posts = response.data;
                if(response.data.length !== 0){
                    dispatch(postCatalogLoad(posts, POST_CATALOG_TYPE_USER_SUBSCRIPTION, page));
                }
                else{
                    dispatch(postCatalogEnd());
                }
            }).catch(error => {
                console.log(error.response.status, error.response.data.message);
                dispatch(postCatalogUnload());
            });
    }
}

function loadByCommunityPostCatalog(communityId, page, pageSize){
    return dispatch => {
        // TODO: add load to ui/ux
        return PostsAPI.byCommunity(communityId, page, pageSize)
            .then(response => {
                let posts = response.data;
                if(response.data.length !== 0){
                    dispatch(postCatalogLoad(posts, POST_CATALOG_TYPE_BY_COMMUNITY, page));
                }
                else {
                    dispatch(postCatalogEnd());
                }
            }).catch( error => {
                console.log(error.response.status, error.response.data.message);
                dispatch(postCatalogUnload());
            });
    }
}

function loadBySearchPostCatalog(query, startdate, enddate, page, pageSize){
    return dispatch => {
        // TODO: add load to ui/ux
        return PostsAPI.bySearch(query, startdate, enddate, page, pageSize)
            .then(response => {
                let posts = response.data;
                if(response.data.length !== 0){
                    dispatch(postCatalogLoad(posts, POST_CATALOG_TYPE_BY_SEARCH, page));
                }
                else{
                    dispatch(postCatalogEnd());
                }
            }).catch(error => {
                console.log(error.response.status, error.response.data.message);
                dispatch(postCatalogUnload());
            });
    };
}

function unloadPostCatalog(){
    return dispatch =>{
        dispatch(postCatalogUnload());
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
    loadPostCatalog,
    loadBySearchPostCatalog,
    loadBySubscriptionPostCatalog,
    loadByCommunityPostCatalog,
    unloadPostCatalog
}