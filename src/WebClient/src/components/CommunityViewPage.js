import React from "react";
import { loadCommunityView, unloadCommunityView } from "../actions/communityActions"
import { loadByCommunityPostCatalog } from "../actions/postCatalogActions"
import Loading from "./Loading";
import { withRouter } from "react-router";
import { withTranslation } from "react-i18next";
import { connect } from "react-redux"
import { Figure } from "react-bootstrap"
import { bindActionCreators } from "redux"
import PostCatalog from "./PostCatalog"

class CommunityViewPage extends React.Component{
    componentDidMount(){
        let {communityId} = this.props.match.params;
        this.props.loadCommunityView(communityId);
        this.props.loadByCommunityPostCatalog(communityId, 1, 10);
    }
    render(){
        const {community} = this.props;
        console.log(this.props);
        console.log("in community page");
        return(
            <React.Fragment>
                <div>
                    {community === null &&
                        <Loading />
                    }{community !== null && 
                        <div>
                            <div className="row">
                                <div className="col-md-3">
                                    <Figure className="m-1">
                                        <Figure.Image
                                            width={200}
                                            height={200}
                                            rounded
                                            src={community.imageUrl} />
                                    </Figure>
                                </div>
                                <div className="col-md-5">
                                    <h3>{community.name}</h3>
                                    <p className="text-muted">{community.description}</p>
                                </div>
                            </div>
                            <h3>
                                Posted in community
                            </h3>
                            <PostCatalog/>
                        </div>
                    }
                </div>
            </React.Fragment>
        );
    }
}

const mapStateToProps = state => ({
    community: state.community.communityInfo
})

const mapDispatchToProps = dispatch => ({
    loadCommunityView: bindActionCreators(loadCommunityView, dispatch),
    unloadCommunityView: bindActionCreators(unloadCommunityView, dispatch),
    loadByCommunityPostCatalog: bindActionCreators(loadByCommunityPostCatalog, dispatch)
})

export default withRouter(withTranslation()(connect(mapStateToProps, mapDispatchToProps)(CommunityViewPage)))