import React from 'react';
import './App.css';
import {BrowserRouter as Router, Link} from "react-router-dom";

const App = () => {
  return (
    <div className="App">
      <Router>
        <Link to="/home">

        </Link>
      </Router>
    </div>
  );
}

export default App;
