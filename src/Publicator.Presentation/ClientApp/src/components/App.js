import React from 'react';
import './App.css';
import {Navbar} from "./Navbar";
import {BrowserRouter as Router} from "react-router-dom";

class App extends React.Component {

  render() {
    return (
        <Router>
          <Navbar />
        </Router>
      );
  }
}

export default App;