import Header from '../header';
import { Route, Switch } from 'react-router';
import { BrowserRouter as Router } from 'react-router-dom';

import HomePage from '../../pages/homePage';
import BanUsers from '../../pages/banPage';
import MakeAdmin from '../../pages/makeAdminPage';

function App() {
  return (
    <div className="App">
      <Router>
        <Header/>
        <Switch>
          <Route exact path='/' component={HomePage}/>
          <Route path='/ban_users' component={BanUsers}/>
          <Route path='/make_admin' component={MakeAdmin}/>
        </Switch>
      </Router>
    </div>
  );
}

export default App;
