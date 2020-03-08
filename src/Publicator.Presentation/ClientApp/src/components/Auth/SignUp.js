import React from 'react';
import { Form, Message, Label, Button, Input, Transition, Header } from "semantic-ui-react";
import { withTranslation } from "react-i18next";

const ANIMATION_DURATION = 500;
const EMAIL_EXP = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
const MIN_LENGTH_USERNAME = 4;
const MIN_LENGTH_PASSWORD = 8;


class SignUp extends React.Component {
    constructor(props){
        super(props);
        this.state = {
            username: "", 
            email: "", 
            password: "", 
            confirmPassword: "",
            usernameError: null, 
            emailError: null, 
            passwordError: null,
            confirmError: null,
            result: null
        };
    }
    
    handleChange = (e, {name, value}) => {
        this.setState({
            confirmError: null,
            passwordError: null,
            usernameError: null,
            emailError: null
        });
        this.setState({ [name]: value });
    }


    validate = () => {
        var result = true;
        const{t} = this.props;
        if(this.state.username < MIN_LENGTH_USERNAME){
            this.setState({usernameError: t("usernameError")});
            result = false;
        }
        if(!EMAIL_EXP.test(this.state.email)){
            this.setState({emailError: t("emailError")});
            result = false;
        }
        if(this.state.password.length < MIN_LENGTH_PASSWORD){
            this.setState({passwordError: t("passwordError")});
            result = false;
        }
        if(this.state.password !== this.state.confirmPassword || this.state.confirmPassword.length < MIN_LENGTH_PASSWORD){
            this.setState({confirmError: t("confirmPasswordError")});
            result = false;
        }
        return result;
    }
    handleSignup = () => {
        if(this.validate()){
            this.setState({result: "Username is already used"})
            // todo logic for sign up
        }
    }
    render(){
        const{email, password, username, confirmPassword, emailError, passwordError, confirmError, usernameError, result} = this.state;
        const emErShow = emailError !== null ;
        const psErShow = passwordError !== null;
        const resErShow = result !== null;
        const conErShow = confirmError !== null;
        const usNmErShow = usernameError !== null;
        const {t} = this.props;
        return(
            <div>
                <Header as="h2">{t("signUp")}</Header>
                <Transition visible={resErShow} animation='scale' duration={ANIMATION_DURATION}>
                    <Message name="result" hidden={result === null} error content={result !== null ? t(result) : ""}/>
                </Transition>
                <Form>
                    <Form.Field >
                        <label>{t("email")}</label>
                        <Input value={email} name="email" onChange={this.handleChange}/>
                        <Transition visible={emErShow} animation='scale' duration={ANIMATION_DURATION}>
                            <Label as="div" pointing="above" color="red" content={emailError}/>
                        </Transition>
                    </Form.Field>
                    <Form.Field >
                        <label>{t("username")}</label>
                        <Input value={username} name="username" onChange={this.handleChange}/>
                        <Transition visible={usNmErShow} animation='scale' duration={ANIMATION_DURATION}>
                            <Label as="div" pointing="above" color="red" content={usernameError}/>
                        </Transition>
                    </Form.Field>
                    <Form.Field >
                        <label>{t("password")}</label>
                        <Input type="password" value={password} name="password" onChange={this.handleChange}/>
                        <Transition visible={psErShow} animation='scale' duration={ANIMATION_DURATION}>
                            <Label as="div" pointing="above" color="red" content={passwordError}/>
                        </Transition>
                    </Form.Field>
                    <Form.Field >
                        <label>{t("confirmPassword")}</label>
                        <Input type="password" value={confirmPassword} name="confirmPassword" onChange={this.handleChange}/>
                        <Transition visible={conErShow} animation='scale' duration={ANIMATION_DURATION}>
                            <Label as="div" pointing="above" color="red" content={confirmError}/>
                        </Transition>
                    </Form.Field>
                    <Button content={t("signUp")} onClick={this.handleSignup} />
                </Form>
            </div>
        );
    }
}

export default withTranslation()(SignUp);
