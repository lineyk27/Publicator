import React from 'react';
import { Provider } from "react-redux";
import { I18nextProvider } from "react-i18next";
import { Container } from "react-bootstrap"
import { Switch, Route } from "react-router-dom"
import i18n from './../i18n';
import { BrowserRouter as Router } from "react-router-dom";
import store from './../store';
import NavBar from "./NavBar";
import LogIn from "./Authentication/LogIn"
import SignUp from "./Authentication/SignUp"
import {
  ROUTE_LOGIN,
  ROUTE_SIGNUP
} from "../constants" 


function App() {
  return ( 
    <Provider store={store}>
      <div className="wrapper">
          <I18nextProvider i18n={i18n}>
            <Router>
              <NavBar />
              <MainWrapper />
            </Router>
          </I18nextProvider>
      </div>
    </Provider>
  );
}



class MainWrapper extends React.Component{
  render(){
    return(
      <Container >
        <Switch>
          <Route path={ROUTE_LOGIN} >
            <LogIn />
          </Route>
          <Route path={ROUTE_SIGNUP} >
            <SignUp />
          </Route>
        </Switch>
      </Container>
    )
  }
}

export default App;
