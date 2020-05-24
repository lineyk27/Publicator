import React from "react"
import { setCurrent } from "../actions/loginActions"
import { Switch, Route } from "react-router-dom"
import { Container } from "react-bootstrap"
import { connect } from "react-redux"
import { bindActionCreators } from "redux"
import LogIn from "./Authentication/LogIn"
import SignUp from "./Authentication/SignUp"
import NavBar from "./NavBar"
import NewPostPage from "./NewPostPage"
import PostViewPage from "./PostViewPage"
import PostCatalogPage from "./PostCatalogPage"
import ProfileViewPage from "./ProfileViewPage"
import {
    ROUTE_LOGIN,
    ROUTE_SIGNUP,
    ROUTE_NEWPOST,
    ROUTE_POSTVIEW,
    ROUTE_MAIN,
    ROUTE_USER,
    ROUTE_BOOKMARKS,
    ROUTE_COMMUNITIES,
    ROUTE_COMMUNITY
} from "../constants"
import BookmarksPage from "./BookmarksPage"
import Footer from "./Footer"
import CommunitiesCatalogPage from "./CommunitiesCatalogPage"
import CommunityViewPage from "./CommunityViewPage"

class MainWrapper extends React.Component{
    componentDidMount(){
        this.props.setCurrent();
    }
    render(){
        return(
            <React.Fragment>
            <div id="wrapper" >
                <NavBar/>
                <br/><br/><br/><br/><br/>
                <Container className="mt-2 mb-2 full" >
                    <Switch>
                        <Route path={ROUTE_LOGIN} >
                            <LogIn />
                        </Route>
                        <Route path={ROUTE_SIGNUP} >
                            <SignUp />
                        </Route>
                        <Route path={ROUTE_NEWPOST} >
                            <NewPostPage />
                        </Route>
                        <Route path={ROUTE_COMMUNITY} >
                            <CommunityViewPage />
                        </Route>
                        <Route path={ROUTE_COMMUNITIES} >
                            <CommunitiesCatalogPage />
                        </Route>
                        <Route path={ROUTE_POSTVIEW} >
                            <PostViewPage />
                        </Route>
                        <Route path={ROUTE_BOOKMARKS} exact>
                            <BookmarksPage/>
                        </Route>
                        <Route path={[ROUTE_MAIN, "/"]} exact>
                            <PostCatalogPage/>
                        </Route>
                        <Route path={ROUTE_USER} exact>
                            <ProfileViewPage/>
                        </Route>
                    </Switch>
                </Container>
            </div>
            <Footer/>
            </React.Fragment>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        setCurrent: bindActionCreators(setCurrent, dispatch)
    }
}

export default connect(null, mapDispatchToProps)(MainWrapper);