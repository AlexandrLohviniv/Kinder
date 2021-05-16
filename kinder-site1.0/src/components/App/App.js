import Header from '../Header';
import Footer from '../Footer';
import { Route, Switch } from 'react-router';
import { BrowserRouter as Router } from 'react-router-dom';

import HomePage from '../../pages/HomePage';
import BanUsers from '../../pages/BanPage';
import MakeAdmin from '../../pages/MakeAdminPage';

// import WithKinderService from '../hoc';

const App = () => {
  return (
    <div className="App">
      <Router>
        <Header/>
        <Switch>
          <Route exact path='/' component={HomePage}/>
          <Route path='/ban_users' component={BanUsers}/>
          <Route path='/make_admin' component={MakeAdmin}/>
        </Switch>
        <Footer/>
      </Router>
    </div>
  );
}

// export default WithKinderService()(App);
export default App;
