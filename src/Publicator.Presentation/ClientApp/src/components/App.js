import React from 'react';
import {Provider} from "react-redux";
import {I18nextProvider} from "react-i18next";
import i18n from './../i18n';
import { BrowserRouter as Router } from "react-router-dom";
import store from './../store';
import NavBar from "./NavBar";
import {Placeholder} from "semantic-ui-react";

function App() {
  const elem = (
    <Placeholder>
    <Placeholder.Header image>
      <Placeholder.Line />
      <Placeholder.Line />
    </Placeholder.Header>
    <Placeholder.Paragraph>
      <Placeholder.Line />
      <Placeholder.Line />
      <Placeholder.Line />
      <Placeholder.Line />
    </Placeholder.Paragraph>
    
    <Placeholder.Paragraph>
      <Placeholder.Line />
      <Placeholder.Line />
      <Placeholder.Line />
      <Placeholder.Line />
    </Placeholder.Paragraph>
    <Placeholder.Paragraph>
      <Placeholder.Line />
      <Placeholder.Line />
      <Placeholder.Line />
      <Placeholder.Line />
    </Placeholder.Paragraph>
    <Placeholder.Paragraph>
      <Placeholder.Line />
      <Placeholder.Line />
      <Placeholder.Line />
      <Placeholder.Line />
    </Placeholder.Paragraph>
    <Placeholder.Paragraph>
      <Placeholder.Line />
      <Placeholder.Line />
      <Placeholder.Line />
      <Placeholder.Line />
    </Placeholder.Paragraph>
  </Placeholder>
  );
  return (
    <Provider store={store}>
      <div className="wrapper">
          <I18nextProvider i18n={i18n}>
            <Router>
              <NavBar children={elem}/>
            </Router>
          </I18nextProvider>
      </div>
    </Provider>
  );
}

export default App;
