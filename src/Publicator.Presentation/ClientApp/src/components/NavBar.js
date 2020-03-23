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
    ROUTE_PROFILE,
    ROUTE_BOOKMARKS,
    ROUTE_SETTINGS,
    T_PROFILE,
    T_SETTINGS,
    T_BOOKMARKS,
    T_LOGOUT
} from "../constants"
import { logout } from "../actions/loginActions"

class NavBar extends React.Component{
    userPopover = () => {
        const { t } = this.props;
        return (
            <Popover>
                <Popover.Content>
                    <ListGroup variant="flush" >
                        <Link to={ROUTE_PROFILE} className="router-link"  >
                            <ListGroup.Item action>
                                {t(T_PROFILE)}
                            </ListGroup.Item>
                        </Link>
                        <Link to={ROUTE_BOOKMARKS} className="router-link" >
                            <ListGroup.Item action>
                                {t(T_BOOKMARKS)}
                            </ListGroup.Item>
                        </Link>
                        <Link to={ROUTE_SETTINGS} className="router-link" >
                            <ListGroup.Item action>
                                {t(T_SETTINGS)}
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
                <Navbar bg="light" variant="light" collapseOnSelect expand="md"  >
                    <Navbar.Brand>/Some brand/</Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="mr-auto">
                            <Nav.Item as={Col}>
                                <Link to={ROUTE_HOME}>{t(T_HOME)}</Link>
                            </Nav.Item>
                            <Nav.Item as={Col}>
                                <Link to={ROUTE_COMMUNITIES}>{t(T_COMMUNITIES)}</Link>
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
                                    <Nav.Item as={Col}>
                                        <Link to={ROUTE_SIGNUP}>
                                            <Button size='sm' variant="outline-success">{t(T_SIGNUP)}</Button>
                                        </Link>
                                    </Nav.Item>
                                </React.Fragment>
                            }
                            {isAuthorized &&
                            <Nav.Item>
                                <OverlayTrigger trigger="click" placement="bottom-start" overlay={this.userPopover()}>
                                    <div style={{ cursor: "pointer" }} >
                                        <span>{userInfo.nickname}</span><span>&nbsp;</span>
                                        <Figure style={{margin: "0"}} >
                                            <Figure.Image roundedCircle src={userInfo.imageUrl} width={40} height={40} />
                                        </Figure>
                                    </div>
                                </OverlayTrigger>
                            </Nav.Item>
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