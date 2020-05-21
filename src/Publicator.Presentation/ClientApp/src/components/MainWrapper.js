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
import {
    ROUTE_LOGIN,
    ROUTE_SIGNUP,
    ROUTE_NEWPOST,
    ROUTE_POSTVIEW,
    ROUTE_MAIN
} from "../constants"

class MainWrapper extends React.Component{
    componentDidMount(){
        this.props.setCurrent();
    }
    render(){
        return(
            <div id="wrapper" >
                <NavBar/>
                <Container >
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
                        <Route path={ROUTE_POSTVIEW} >
                            <PostViewPage />
                        </Route>
                        <Route path={[ROUTE_MAIN, "/"]} exact>
                            <PostCatalogPage/>
                        </Route>
                    </Switch>
                </Container>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => {
    return {
        setCurrent: bindActionCreators(setCurrent, dispatch)
    }
}

export default connect(null, mapDispatchToProps)(MainWrapper);