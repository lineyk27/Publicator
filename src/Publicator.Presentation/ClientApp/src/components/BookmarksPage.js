import React from "react"
import { loadBookmarksPostCatalog } from "../actions/postCatalogActions"
import PostCatalog from "./PostCatalog";
import { 
    unloadPostCatalog, 
    loadByCommunityPostCatalog,
    loadBySubscriptionPostCatalog,
    loadCatalogHot,
    loadCatalogNew
} from "../actions/postCatalogActions"
import { bindActionCreators } from "redux";
import { withRouter, Link } from "react-router-dom"
import { withTranslation } from "react-i18next"
import { connect } from "react-redux"
import _ from "lodash"


class BookmarksPage extends React.Component{
    componentDidMount(){
        this.props.loadBookmarksCatalog();
    }
    componentWillUnmount(){
        this.props.unloadPostCatalog();
    }
    render(){
        return (
            <div>
                <h3 className="mb-3">
                    Your bookmarks
                </h3>
                <PostCatalog/>
            </div>
        );
    }
}

const mapStateToProps = state => ({
    posts: state.postCatalog.posts,
    lastPage: state.postCatalog.lastPage,
    catalogType: state.postCatalog.catalogType,
    end: state.postCatalog.end,
    isAuthorized: state.login.isAuthorized
})

const mapDispatchToProps = dispatch => ({
        unloadPostCatalog: bindActionCreators(unloadPostCatalog, dispatch),
        loadByCommunityPostCatalog: bindActionCreators(loadByCommunityPostCatalog, dispatch),
        loadCatalogBySubscription: bindActionCreators(loadBySubscriptionPostCatalog, dispatch),
        loadCatalogNew: bindActionCreators(loadCatalogNew, dispatch),
        loadCatalogHot: bindActionCreators(loadCatalogHot, dispatch),
        loadBookmarksCatalog: bindActionCreators(loadBookmarksPostCatalog, dispatch),
})

export default withRouter(withTranslation()(connect(mapStateToProps, mapDispatchToProps)(BookmarksPage)))