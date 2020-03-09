import React from 'react';
import {Provider} from "react-redux";
import {I18nextProvider} from "react-i18next";
import i18n from './../i18n';
import { BrowserRouter as Router } from "react-router-dom";
import store from './../store';
import AuthModal from "./Auth/AuthModal";

function App() {
  return (
    <Provider store={store}>
      <div className="wrapper">
          <I18nextProvider i18n={i18n}>
            <Router>
              <AuthModal show={true}/>
            </Router>
          </I18nextProvider>
      </div>
    </Provider>
  );
}

export default App;
