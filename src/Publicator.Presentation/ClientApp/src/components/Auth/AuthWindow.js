import React from 'react';
import { Segment, Header} from "semantic-ui-react";
import { withTranslation } from "react-i18next";
import SignUp from "./SignUp";
import LogIn from "./LogIn";

const LOGIN_PAGE = 1;
const SIGNUP_PAGE = 2;

class AuthenticationWindow extends React.Component{
    constructor(props){
        super(props);
        this.state = {
            currentPage : LOGIN_PAGE
        }
    }
    handleClick = () => {
        console.log("Changed page");
        if(this.state.currentPage === LOGIN_PAGE)
            this.setState({currentPage: SIGNUP_PAGE});
        else
            this.setState({currentPage: LOGIN_PAGE});
    }
    render(){
        const{t} = this.props;
        const{currentPage} = this.state;
        return(
            <Segment>
                {currentPage === LOGIN_PAGE &&  
                    <div>
                        <LogIn />
                        <Header as="h4">{t("loginInvite")} <Header color="blue" as="a" size="tiny" basic onClick={this.handleClick}>{t("signUp")}</Header></Header>
                    </div>
                }{ currentPage === SIGNUP_PAGE &&   
                    <div>
                        <SignUp />
                        <Header as="h4">{t("signupInvite")} <Header color="blue" as="a" size="tiny" basic onClick={this.handleClick}>{t("logIn")}</Header></Header>
                    </div>
                }
            </Segment>
        );
    }
}






export default withTranslation()(AuthenticationWindow);

