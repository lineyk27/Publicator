import React from "react"
import { Button, Form, Container, Alert } from "react-bootstrap"
import {withTranslation} from "react-i18next"
import { bindActionCreators } from "redux"
import { 
    T_EMAIL, 
    T_PASSWORD, 
    T_PASSWORDERROR, 
    T_EMAILERROR, 
    T_LOGIN, 
    T_LOGINSUCCES, 
    T_LOGINERROR
} from "../../constants"
import { login } from "../../actions/loginActions"
import { connect } from "react-redux"

class LogIn extends React.Component{
    constructor(props){
        super(props);
        this.state = {validated: false, error: false}
    }
    handleSubmit = event => {
        this.handleChange();
        const form = event.currentTarget;
        this.setState({validated: true});
        event.preventDefault();
        event.stopPropagation();
        if (form.checkValidity() === false) {
          return;
        }
        let email = form.elements["email"].value;
        let password = form.elements["password"].value;
        this.props.login(email, password);
        if(this.props.error !== null && this.props.error !== false){
            this.setState({
                error: this.props.error
            });
        }
        this.setState({validated: true});
    }
    handleChange = () => {
        this.setState({
            error: false
        });
    }
    
    render(){
        const { validated, error } = this.state;
        const { t, isAuthorized } = this.props;
        return(
            <div className="row justify-content-center">
            <div className="w-50 ">
                {error && 
                    <Alert variant="danger">{t(T_LOGINERROR)}</Alert>
                }
                {isAuthorized && 
                    <Alert variant="success" >{t(T_LOGINSUCCES)}</Alert>
                }
                <Form
                    noValidate
                    validated={validated}
                    onSubmit={this.handleSubmit}
                    onChange={this.handleChange}
                    >
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
                    <Button type="submit" >{t(T_LOGIN)}</Button>
                </Form>
            </div>
            </div>
        )
    }
}

const mapDispatchToProps = dispatch => ({
    login: (email, password) => {
        dispatch(login(email, password))
    }
})

const mapStateToProps = state => ({
    isAuthorized: state.login.isAuthorized,
    error: state.login.error
})

//export default withTranslation()(LogIn)

export default withTranslation()(connect(mapStateToProps, mapDispatchToProps)(LogIn))
