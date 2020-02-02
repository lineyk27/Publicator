import React from "react";
import { Container, Menu } from "semantic-ui-react";
import {Link} from "react-router-dom";

export class Navbar extends React.Component{
    render(){
        return (
            <Container fluid className="navbar">
                <Container>
                    <Menu secondary>
                        <Menu.Item link>
                            <Link to="/">Home</Link>
                        </Menu.Item>
                        <Menu.Item link>
                            <Link to="/communities">Communities</Link>
                        </Menu.Item>
                    </Menu>
                </Container>
            </Container>
        );
    }
}

