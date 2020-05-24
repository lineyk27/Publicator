import React from "react"
import { Container } from "react-bootstrap";
export default class Footer extends React.Component{
    render(){
        return(
            <div className="bg-dark text-light">
                <Container style={{minHeight: "100px"}}>
                    <div className="pt-4" >
                        <span className="pt-4 h4">Publicator@2020</span>
                    </div>
                </Container>
            </div>
        );
    }
}
