import React from "react"
import {Link} from "react-router-dom"
import { withTranslation } from "react-i18next"
import { connect } from "react-redux"
import { Button, Navbar, Nav, Col, Figure, OverlayTrigger, Popover, ListGroup } from "react-bootstrap"
import {
    T_HOME,
    ROUTE_HOME,
    T_COMMUNITIES,
    ROUTE_COMMUNITIES,
    T_LOGIN,
    T_SIGNUP,
    ROUTE_SIGNUP,
    ROUTE_LOGIN,
    ROUTE_BOOKMARKS,
    ROUTE_NEWPOST,
    T_PROFILE,
    T_NEWPOST,
    T_SETTINGS,
    T_BOOKMARKS,
    T_LOGOUT,
    ROUTE_USER
} from "../constants"
import { logout } from "../actions/loginActions"
import logo from "../logo.png"

class NavBar extends React.Component{
    userPopover = () => {
        const { t, userInfo } = this.props;
        return (
            <Popover>
                <Popover.Content>
                    <ListGroup variant="flush" >
                        <Link to={`/users/${userInfo.nickname}`} >
                            <ListGroup.Item action>
                                {t(T_PROFILE)}
                            </ListGroup.Item>
                        </Link>
                        <Link to={ROUTE_BOOKMARKS}>
                            <ListGroup.Item action>
                                {t(T_BOOKMARKS)}
                            </ListGroup.Item>
                        </Link>
                        <ListGroup.Item action onClick={this.handleLogout}>
                            {t(T_LOGOUT)}
                        </ListGroup.Item>
                    </ListGroup>
                </Popover.Content>
            </Popover>
        )
    }

    handleLogout = () => {
        this.props.logout();
    }
    render(){
        const { t, isAuthorized, userInfo } = this.props;
        return(
                <Navbar bg="dark" className="p-0 pl-2 pr-3" variant="light" fixed="top" collapseOnSelect expand="md"  >
                    <Navbar.Brand>
                        <Link to="/hot">
                            <img src={logo} />
                        </Link>
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="mr-auto">
                            <Nav.Item as={Col}>
                                <Link to={"/hot"}>{t(T_HOME)}</Link>
                            </Nav.Item>
                            <Nav.Item as={Col}>
                                <Link to={ROUTE_COMMUNITIES}>Categories</Link>
                            </Nav.Item>
                        </Nav>
                        <Nav>
                            {!isAuthorized && 
                                <React.Fragment>
                                    <Nav.Item as={Col}>
                                        <Link to={ROUTE_LOGIN} >
                                            <Button size='sm' variant="outline-primary" >{t(T_LOGIN)}</Button>
                                        </Link>
                                    </Nav.Item>
                                    <Nav.Item >
                                        <Link to={ROUTE_SIGNUP} className="btn btn-sm btn-outline-success">{t(T_SIGNUP)}
                                        </Link>
                                    </Nav.Item>
                                </React.Fragment>
                            }
                            {isAuthorized &&
                            <React.Fragment>
                                <Nav.Item>
                                    <Link to={ROUTE_NEWPOST} className="btn btn-light mt-2">
                                        New post
                                    </Link>
                                </Nav.Item>
                                <Nav.Item>
                                    <OverlayTrigger trigger="click" placement="bottom-start" overlay={this.userPopover()}>
                                        <Button variant="link" >
                                            <span>{userInfo.nickname}</span><span>&nbsp;</span>
                                            <Figure style={{margin: "0"}} >
                                                <Figure.Image roundedCircle src={userInfo.imageUrl} width={40} height={40} />
                                            </Figure>
                                        </Button>
                                    </OverlayTrigger>
                                </Nav.Item>
                            </React.Fragment>
                            }
                        </Nav>
                    </Navbar.Collapse>
                </Navbar>
            );
    }
}

const mapStateToProps = state => ({
    isAuthorized: state.login.isAuthorized,
    userInfo: state.login.userInfo
})

const mapDispatchToProps = {
    logout
}

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(NavBar))