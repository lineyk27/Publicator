import React from "react";
import { login } from "../actions/loginActions";
import { Form, Button } from "semantic-ui-react";
import {dispatch} from "redux";

export default class LoginForm extends React.Component{
    state = {login: "", password: ""};
        
    handleSubmit = () => {

        this.props.dispatch(login(this.state.login, this.state.password));
    };

    handleChange = (e, {name, value}) => {
        this.setState({
            [name] : value
        });
    }
    render(){
        const {login, password} = this.state;

        return (
            <Form onSubmit={this.handleSubmit}>
                <Form.Group>
                    <Form.Input name="login" onChange={this.handleChange} value={login} />
                    <Form.Input name="password" onChange={this.handleChange} value={password} />
                    <Form.Button content="Submit"/>
                </Form.Group>
            </Form>
        );
    }
}
