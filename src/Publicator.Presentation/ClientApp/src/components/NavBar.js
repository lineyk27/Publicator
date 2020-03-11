import React from "react";
import { withTranslation } from "react-i18next";
import { connect } from "react-redux";
import { Link } from "react-router-dom";
import {Container, Menu, Responsive, Icon,Sidebar, Button, Header, Image, Input } from "semantic-ui-react";
import {
    T_LOGIN,
    T_SIGNUP,
    T_HOME,
    T_COMMUNITIES,
    T_SEARCH,
    ROUTE_LOGIN,
    ROUTE_HOME,
    ROUTE_COMMUNITIES,
    MAX_MOBILE_WIDTH,
    ROUTE_SIGNUP
} from "../constants";
import _ from "lodash";

const LogInButton = withTranslation()(class extends React.Component{
    render(){
        const{t, location} = this.props;
        return(
            <Link to={{
                pathname: ROUTE_LOGIN,
                state: { background: location }
            }}>
                <Button content={t(T_LOGIN)} size="tiny" color="green"/>
            </Link>
        );
    }
});

const SignUpButton = withTranslation()(class extends React.Component{
    render(){
        const{t, location} = this.props;
        return(
            <Link to={{
                pathname: ROUTE_SIGNUP,
                state: { background: location }
            }}>
                <Button content={t(T_SIGNUP)} size="tiny" color="blue"/>
            </Link>
        );
    }
})

const SearchInput = withTranslation()(class extends React.Component{
    handleSearch = () => {
        console.log("search was clicked.");
    }
    render(){
        const{t} = this.props;
        return(
            <Input action={{icon: 'search', onClick: this.handleSearch}}  placeholder={t(T_SEARCH)}/>
        );
    }
})

class NavBar extends React.Component{
    constructor(props){
        super(props);
        this.state = {visible: false};
    }

    turnOn = () => {
        this.setState({visible: true});
    }
    turnOff = () => {
        this.setState({visible: false});
    }
    userThumbnail = () => {
        // TODO: must be reconsidered
        const{nickname, imageUrl} = this.props.userInfo || ["user", "image"];
        return (
            <div>          
                <Header as='h4' image>
                    <Image src={imageUrl} rounded size='mini' />
                    <Header.Content> {nickname} </Header.Content>
                </Header>
            </div>
        );
    }
    menuButtons = () => {
        let res = {};
        res[T_HOME] = ROUTE_HOME;
        res[T_COMMUNITIES] = ROUTE_COMMUNITIES;
        console.log(res);
        return res;
    }
    render(){
        const {t, isAuthorized} = this.props;
        const {children} = this.props;
        const {visible} = this.state;
        const thumbnail = isAuthorized ? this.userThumbnail() : null;
        return(
            <Container>
                <Sidebar.Pushable>
                    <Sidebar
                        as={Menu}
                        width="wide"
                        animation="overlay"
                        inverted
                        onHide={() => this.turnOff()}
                        vertical
                        visible={visible}
                        >
                        <Menu.Item>
                            <SearchInput/>
                        </Menu.Item>
                        {isAuthorized &&
                            <Menu.Item>
                                {thumbnail}
                            </Menu.Item>
                        }
                        {!isAuthorized &&
                            <Menu.Item> 
                                <LogInButton/>
                            </Menu.Item>
                        }
                        {!isAuthorized &&
                            <Menu.Item> 
                                <SignUpButton/>
                            </Menu.Item>
                        }
                    </Sidebar>
                    <Sidebar.Pusher dimmed={visible}>
                        <Menu inverted>
                            <Responsive  maxWidth={MAX_MOBILE_WIDTH}>
                                <Menu.Item icon onClick={this.turnOn}>
                                    <Icon name="bars" />
                                </Menu.Item>
                            </Responsive>
                            <Menu.Item>
                                <Link to={ROUTE_HOME}>
                                    Publicator
                                </Link>
                            </Menu.Item>
                            {
                                _.map(this.menuButtons(), (route, name) => {
                                    return(
                                        <Menu.Item>
                                            <Link to={route}>{t(name)}</Link>
                                        </Menu.Item>
                                    );
                                })
                            }
                            <Menu.Menu position='right'>
                                <Responsive minWidth={MAX_MOBILE_WIDTH} as={Menu.Item}>
                                    <SearchInput/>
                                </Responsive>
                                {isAuthorized && 
                                    <Responsive minWidth={MAX_MOBILE_WIDTH}>
                                        <Menu.Item>
                                            {thumbnail}
                                        </Menu.Item>
                                    </Responsive>
                                }
                                {!isAuthorized &&
                                    <Responsive minWidth={MAX_MOBILE_WIDTH}>
                                        <Menu.Item>
                                            <LogInButton/>
                                        </Menu.Item>
                                    </Responsive>
                                }
                                {!isAuthorized &&
                                    <Responsive minWidth={MAX_MOBILE_WIDTH}>
                                        <Menu.Item>
                                            <SignUpButton/>
                                        </Menu.Item>
                                    </Responsive>
                                }
                            </Menu.Menu>
                        </Menu>
                        {children}
                    </Sidebar.Pusher>
                </Sidebar.Pushable>
            </Container>
        )
    }
};

const mapStateToProps = (state) => ({
    userInfo: state.login.userInfo,
    isAuthorized: state.login.isAuthorized
})

export default withTranslation()((connect(mapStateToProps, null)(NavBar)));