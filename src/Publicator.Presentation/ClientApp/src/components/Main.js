import React from "react";
import { Switch, Route } from "react-router-dom";


class Main extends React.Component{
    render (){
        return (
            <Switch>
                <Route to="/post/:id" />
            </Switch>
        );
    }
}