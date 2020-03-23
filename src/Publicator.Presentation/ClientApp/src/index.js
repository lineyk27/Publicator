import React from 'react';
import ReactDOM from 'react-dom';
import './index.css';
import App from './components/App';
import * as serviceWorker from './serviceWorker';
import 'bootstrap/dist/css/bootstrap.min.css';
//import jquery from 'jquery'
//global.$ = jquery;
//global.jQuery =  jquery;
//require("semantic-ui-css/semantic.js");
//require("semantic-ui-css/semantic.css");

ReactDOM.render(<App />, document.getElementById('root'));

serviceWorker.unregister();
