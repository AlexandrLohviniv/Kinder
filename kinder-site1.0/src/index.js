import React from 'react';
import ReactDOM from 'react-dom';
import 'bootstrap/dist/css/bootstrap.min.css';

import {Provider} from 'react-redux';
import {BrowserRouter as Router} from 'react-router-dom';

import ErrorBoundry from './components/ErrorBoundry';
import KinderService from './services/kinder-service';
import KinderServiceContext from './components/KinderServiceContext';
import store from './store';

import App from './components/App/App';
import reportWebVitals from './reportWebVitals';

const kinderService = new KinderService();

ReactDOM.render(
  <Provider store={store}>
    <ErrorBoundry>
      <KinderServiceContext.Provider value={kinderService}>
        <Router>
            <App/>
        </Router>
      </KinderServiceContext.Provider>
    </ErrorBoundry>
  </Provider>,
  document.getElementById('root')
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
