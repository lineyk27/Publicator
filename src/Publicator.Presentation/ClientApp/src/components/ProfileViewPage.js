import React from "react"
import { connect } from "react-redux";
import { withRouter } from "react-router";
import { withTranslation } from "react-i18next"
import { bindActionCreators } from "redux";
import { 
    loadUser, 
    unloadUser, 
    loadSubscruption, 
    subscribe 
} from "../actions/userViewActions"
import { loadByCreatorPostCatalog } from "../actions/postCatalogActions"
import { Figure, Row, Col, Button } from "react-bootstrap"
import PostCatalog from "./PostCatalog";
import reprDate from "../utils/dateRepr"

class ProfileViewPage extends React.Component{
    componentDidMount(){
        const{nickname} = this.props.match.params
        this.props.loadUser(nickname);
        this.props.loadSubscruption(nickname);
        this.props.loadByCreatorPostCatalog(nickname, 1, 10);

    }
    handleSubscribe = () => {
        const{nickname} = this.props.userInfo;
        this.props.subscribe(nickname);
    }
    handleLoadMore = () => {
        const{lastPage, end} = this.props;
        const{nickname} = this.props.match.params
        if(!end)
            this.props.loadByCreatorPostCatalog(nickname, lastPage+1, 10);
    }
    render(){
        const{userInfo, isSubscribed,end} = this.props;
        console.log(this.props);
        return(
            <React.Fragment>
                {userInfo !== null &&
                <div>
                    <div className="row mt-3">
                        <div className="col-md-3">
                            <Figure style={{margin: "0"}} >
                                <Figure.Image roundedCircle src={userInfo.imageUrl} />
                            </Figure>
                        </div>
                        <div className="col-md-9">
                            <div className="row">
                                <div className="col-md-2 row justify-content-end">
                                    <div>Username: </div> 
                                </div>
                                <div className="col-md-3">
                                    {userInfo.nickname} 
                                </div>
                            </div>
                            <div className="row">
                                <div className="col-md-2 row justify-content-end">
                                    <div>Join date: </div> 
                                </div>
                                <div className="col-md-3">
                                    {reprDate(userInfo.joinDate)} 
                                </div>
                            </div>
                            <Row className="mt-1">
                                <Button variant={isSubscribed ? "success" : "primary"}
                                    onClick={this.handleSubscribe}
                                    >
                                    {isSubscribed ? "Unsubscribe" : "Subscribe"}
                                </Button>
                            </Row>
                        </div>
                    </div>
                    <h3>User's posts</h3>
                    <div>
                        <PostCatalog />
                    </div>
                    <Button onClick={this.handleLoadMore} disabled={end}>
                        {!end ? "Load more" : "All is loaded"}
                    </Button>
                    </div>
                }
            </React.Fragment>
        );
    }
}

const mapStateToProps = state => ({
    userInfo: state.userView.userInfo,
    loading: state.userView.loading,
    isSubscribed: state.userView.isSubscribed,
    login: state.login.userInfo,
    lastPage: state.postCatalog.lastPage,
    end: state.postCatalog.end
})

const mapDispatchtoProps = dispatch => ({
    loadUser: bindActionCreators(loadUser, dispatch),
    unloadUser: bindActionCreators(unloadUser, dispatch),
    loadSubscruption: nickname => {
        dispatch(loadSubscruption(nickname));
    },
    subscribe: nickname => {
        dispatch(subscribe(nickname));
    },
    loadByCreatorPostCatalog: (username, page, pageSize) => {
        dispatch(loadByCreatorPostCatalog(username, page, pageSize));
    }
})

export default withRouter(withTranslation()(connect(mapStateToProps, mapDispatchtoProps)(ProfileViewPage)))
