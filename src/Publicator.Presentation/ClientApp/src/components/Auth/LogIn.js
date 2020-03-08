import React from 'react';
import { Form, Message, Segment, Label, Button, Input, Transition, Header } from "semantic-ui-react";
import { withTranslation } from "react-i18next";

const ANIMATION_DURATION = 500;
const EMAIL_EXP = /^(([^<>()\[\]\.,;:\s@\"]+(\.[^<>()\[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;
const MIN_LENGTH_USERNAME = 4;
const MIN_LENGTH_PASSWORD = 8;


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
        var result = true;
        if(!validEmail) {
            this.setState({emailError: t("emailError")})
            result = false;
        }
        if(!validPassword){
             this.setState({passwordError: t("passwordError")})
             result = false
        }
        return result;
    }

    handleLogin = () => {
        if(this.validate()){
            // todo logic for login
        }
    }

    render(){
        const{email, password, emailError, passwordError, result} = this.state;
        const emErShow = emailError !== null ;
        const psErShow = passwordError !== null;
        const resErShow = result !== null;
        const {t} = this.props;
        return(
            <div>
                <Header as="h2">{t("logIn")}</Header>
                <Transition visible={resErShow} animation='scale' duration={ANIMATION_DURATION}>
                    <Message name="result" hidden={result === null} error content={result !== null ? t(result) : ""}/>
                </Transition>
                <Form>
                    <Form.Field >
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

export default withTranslation()(LogInPage);