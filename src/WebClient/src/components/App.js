import React from 'react';
import { Provider } from "react-redux";
import { I18nextProvider } from "react-i18next";
import i18n from './../i18n';
import { BrowserRouter as Router } from "react-router-dom";
import store from './../store';
import MainWrapper from "./MainWrapper"

export default function App() {
  return ( 
    <Provider store={store}>
        <I18nextProvider i18n={i18n}>
          <Router>
            <MainWrapper />
          </Router>
        </I18nextProvider>
    </Provider>
  );
}