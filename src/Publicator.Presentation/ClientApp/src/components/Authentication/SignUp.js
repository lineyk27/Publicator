import React from "react"
import { Button, Form, Container, Alert } from "react-bootstrap"
import {withTranslation} from "react-i18next"
import { 
    T_EMAIL, 
    T_PASSWORD, 
    T_PASSWORDERROR, 
    T_EMAILERROR,
    T_USERNAME,
    T_CONFIRMPASSWORD,
    T_CONFIRMERROR,
    T_USERNAMEERROR,
    T_SIGNUP
} from "../../constants"

class SignUp extends React.Component{
    constructor(props){
        super(props);
        this.state = {validated: false, error: false}
    }
    handleSubmit = event => {
        const form = event.currentTarget;
        this.setState({validated: true});
        event.preventDefault();
        event.stopPropagation();
        if (form.checkValidity() === false) {
          return;
        }
        const{t} = this.props;
        if(form.elements["confirmPassword"].value != form.elements["password"]){
            form.elements["confirmPassword"].setCustomValidity(t(T_CONFIRMERROR));
            return;
        }
        let email = form.elements["email"].value;
        let username = form.elements["username"].value;
        let password = form.elements["password"].value;
        let confirmPassword = form.elements["confirmPassword"].value;

        this.setState({error: true});
        this.setState({validated: true});

    }
    
    render(){
        const { validated, error } = this.state;
        const {t} = this.props;
        return(
            <div className="row justify-content-center">
            <div className="w-50 ">
                {error && 
                    <Alert variant="danger" hidded="true" >{t(T_SIGNUP)}</Alert>
                }
                <Form
                    noValidate
                    validated={validated}
                    onSubmit={this.handleSubmit}
                    >
                        <Form.Group
                            //as={Col}
                            md="4"
                            >
                                <Form.Label>{t(T_USERNAME)}</Form.Label>
                                <Form.Control
                                    name="username"
                                    required
                                    minLength="4"
                                    type="text"
                                    />
                                <Form.Control.Feedback type="invalid">{t(T_USERNAMEERROR)}</Form.Control.Feedback>
                        </Form.Group>
                        <Form.Group
                            //as={Col}
                            md="4"
                            >
                                <Form.Label>{t(T_EMAIL)}</Form.Label>
                                <Form.Control
                                    name="email"
                                    required
                                    type="email"
                                    />
                                <Form.Control.Feedback type="invalid">{t(T_EMAILERROR)}</Form.Control.Feedback>
                        </Form.Group>
                        <Form.Group
                            //as={Col}
                            md="4"
                            >
                                <Form.Label>{t(T_PASSWORD)}</Form.Label>
                                <Form.Control
                                    name="password"
                                    required
                                    minLength="8"
                                    type="password"
                                    />
                                    <Form.Control.Feedback type="invalid">{t(T_PASSWORDERROR)}</Form.Control.Feedback>
                        </Form.Group>
                        <Form.Group
                            //as={Col}
                            md="4"
                            >
                                <Form.Label>{t(T_CONFIRMPASSWORD)}</Form.Label>
                                <Form.Control
                                    name="confirmPassword"
                                    required
                                    minLength="8"
                                    type="password"
                                    />
                                    <Form.Control.Feedback type="invalid">{t(T_CONFIRMERROR)}</Form.Control.Feedback>
                        </Form.Group>
                    <Button type="submit" >{t(T_SIGNUP)}</Button>
                </Form>
                </div>
                </div>
        )
    }
}

export default withTranslation()(SignUp)