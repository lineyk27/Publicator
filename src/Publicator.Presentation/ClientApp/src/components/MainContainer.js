import React from "react";
import { Container, Grid } from "semantic-ui-react";

class MainContainer extends React.Component{
    render(){
        return(
            <Container>
                <Grid celled>
                    <Grid.Column width={10}>

                    </Grid.Column>
                    <Grid.Column width={6}>

                    </Grid.Column>
                </Grid>
            </Container>
        );
    }
}

