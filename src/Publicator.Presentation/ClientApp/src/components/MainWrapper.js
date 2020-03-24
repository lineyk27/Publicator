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
import {
    ROUTE_LOGIN,
    ROUTE_SIGNUP,
    ROUTE_NEWPOST
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