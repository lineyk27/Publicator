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
import { PROFILE_EMPTY_URL } from "../constants"
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
    getProfilePic = () => {
        const{userInfo} = this.props;
        if(userInfo !== null && userInfo.imageUrl !== null){
            return userInfo.imageUrl;
        }
        return PROFILE_EMPTY_URL;
    }
    get isMyself(){
        const{userInfo, login} = this.props;
        if(userInfo !== null && login !== null){
            return userInfo.id === login.id;
        }
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
                            <Figure className="m-0"
                                >
                                <Figure.Image 
                                    width={200}
                                    height={200}
                                    roundedCircle 
                                    src={this.getProfilePic()} />
                            </Figure>
                        </div>
                        <div className="col-md-9 container">
                            <div className="row">
                                <h3>
                                    {userInfo.nickname}
                                </h3>
                            </div>
                            <div className="row">
                                <span className="text-muted">
                                    {reprDate(userInfo.joinDate)}
                                </span>
                            </div>
                            {!this.isMyself && 
                                <div className="row">
                                    <Button variant={isSubscribed ? "success" : "primary"}
                                        onClick={this.handleSubscribe}
                                        >
                                        {isSubscribed ? "Unsubscribe" : "Subscribe"}
                                    </Button>
                                </div>
                            }
                        </div>
                    </div>
                    <h3>User's posts</h3>
                    <div>
                        <PostCatalog />
                    </div>
                    <Button 
                        block
                        size="lg"
                        onClick={this.handleLoadMore} 
                        disabled={end}>
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
