import React from 'react';
import { Form, Message, Label, Button, Input, Transition, Header } from "semantic-ui-react";
import { withTranslation } from "react-i18next";
import {MIN_LENGTH_PASSWORD, ANIMATION_DURATION, EMAIL_EXP} from "../../constants"; 
import {connect} from "react-redux";
import {bindActionCreators} from "redux";
import {login} from "../../actions/loginActions";

class LogInPage extends React.Component{
    constructor(props){
        super(props);
        this.state = {email: "", password: "", emailError: null,  passwordError: null, result: null};
    };

    handleChange = (e, { name, value }) => {
        this.setState({
           emailError: null,
           passwordError: null
        });
        this.setState({ [name]: value });
    };

    validate = () => {
        var validEmail = EMAIL_EXP.test(this.state.email) && this.state.email !== "";
        var validPassword = this.state.password.length >= MIN_LENGTH_PASSWORD;
        const {t} = this.props;
        if(!validEmail) {
            this.setState({emailError: t("emailError")})
            return false;
        }
        if(!validPassword){
             this.setState({passwordError: t("passwordError")})
             return false;
        }
        return true;
    }

    handleLogin = () => {
        if(this.validate()){
            const login = this.props.login;
            login(this.state.email, this.state.password);
        }
    }

    render(){
        const{email, password, emailError, passwordError, result} = this.state;
        const{error} = this.props;
        const emErShow = emailError !== null ;
        const psErShow = passwordError !== null;
        const {t} = this.props;
        return(
            <div>
                <Header as="h2">{t("logIn")}</Header>
                <Transition visible={error} animation='scale' duration={ANIMATION_DURATION}>
                    <Message name="result" hidden={result === null} error content={error !== null ? t("loginError") : ""}/>
                </Transition>
                <Form >
                    <Form.Field>
                        <label>{t("email")}</label>
                        <Input type="email" value={email} name="email" onChange={this.handleChange}/>
                        <Transition visible={emErShow} animation='scale' duration={ANIMATION_DURATION}>
                            <Label as="div" pointing="above" color="red" content={emailError}/>
                        </Transition>
                    </Form.Field>
                    <Form.Field >
                        <label>{t("password")}</label>
                        <Input type="password" value={password} name="password" onChange={this.handleChange}/>
                        <Transition visible={psErShow} animation='scale' duration={ANIMATION_DURATION}>
                            <Label as="div" pointing="above" color="red" content={passwordError}/>
                        </Transition>
                    </Form.Field>
                    <Button content={t("logIn")} onClick={this.handleLogin} />
                </Form>
            </div>
        );
    }
}

const mapStateToProps = (state) => ({
    isAuthorized: state.login.isAuthorized,
    userInfo: state.login.userInfo,
    loading: state.login.loading,
    error: state.login.error
})

const mapDispatchToProps = (dispatch) => {
    return bindActionCreators({
        login: (email, password) => login(email, password)
    }, dispatch);
}

export default withTranslation()((connect(mapStateToProps, mapDispatchToProps)(LogInPage)));