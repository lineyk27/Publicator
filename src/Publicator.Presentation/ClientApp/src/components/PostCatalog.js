import React from "react"
import { 
    unloadPostCatalog, 
    loadByCommunityPostCatalog,
    loadBySubscriptionPostCatalog,
    loadCatalogHot,
    loadCatalogNew
} from "../actions/postCatalogActions"
import ItemView from "./ItemView"
import {
    POST_CATALOG_TYPE_HOT,
    POST_CATALOG_TYPE_NEW,
    POST_CATALOG_TYPE_SUBSCRIPTION,
    POST_CATALOG_TYPE_USER_SUBSCRIPTION,
    POST_CATALOG_TYPE_BY_SEARCH
} from "../actionTypes"
import _ from "lodash";
import { bindActionCreators } from "redux";
import { withRouter, Link } from "react-router-dom"
import { withTranslation } from "react-i18next"
import { Card } from "react-bootstrap"
import { connect } from "react-redux"
import { BLOCK_PARAGRAPH, BLOCK_IMAGE, BLOCK_QUOTE } from "../constants";
const PAGE_SIZE = 10;

class PostCatalog extends React.Component{
    render(){
        const{posts, end} = this.props;
        return(
            <React.Fragment>
                {(posts === null || posts.length === 0) && 
                    <div>
                        {end === true && 
                            <p>End of catalog</p>
                        }{end === false && 
                            <p>Loading...</p>
                        }
                    </div>
                }
                {posts !== null && 
                    _.map(posts, (value, index) => {
                        return (<PostCardView post={value} key={index}/>)
                    })
                }
            </React.Fragment>
        );
    }
}

class PostCardView extends React.Component{
    getThumbnail = () => {
        var {content} = this.props.post;
        content = JSON.parse(content);
        const {blocks} = content;
        var firstBlock = _.find(blocks, block => {
            //block.type === BLOCK_PARAGRAPH ||block.type === BLOCK_IMAGE ||  block.type === BLOCK_QUOTE ||
            return block !== null;
        });

        let blockView = ItemView(firstBlock);
        return blockView;
    } 
    render(){
        const {post} = this.props;
        return(
            <React.Fragment>
                <Card className="mb-3">
                    <Card.Body>
                        <Card.Title>
                            {post.name}
                        </Card.Title>
                        <Card.Subtitle>
                            <small className="text-muted">{post.creationDate}</small>
                        </Card.Subtitle>
                        <Card.Text>
                            {this.getThumbnail()}
                        </Card.Text>
                        <Link to={`/posts/${post.id}`} >See all</Link>
                    </Card.Body>
                </Card>
            </React.Fragment>
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
//    loadPostCatalog: bindActionCreators(loadPostCatalog, dispatch),
    unloadPostCatalog: bindActionCreators(unloadPostCatalog, dispatch),
    loadByCommunityPostCatalog: bindActionCreators(loadByCommunityPostCatalog, dispatch),
    loadBySubscriptionPostCatalog: bindActionCreators(loadBySubscriptionPostCatalog, dispatch),
    loadCatalogNew: bindActionCreators(loadCatalogNew, dispatch),
    loadCatalogHot: bindActionCreators(loadCatalogHot, dispatch)
})

export default withRouter(withTranslation()(connect(mapStateToProps, mapDispatchToProps)(PostCatalog)))
