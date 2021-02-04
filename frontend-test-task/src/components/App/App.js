import './App.css';
import {BrowserRouter as Router, Redirect, Route} from 'react-router-dom'
import Navbar from "../Navbar";
import Authorization from "../Authorization";
import Quotes from "../Quotes/quotes";
import CompanyList from "../CompanyList";

function App() {
    return (
        <Router>
            <div className="App">
                <Route path={'/authentication'}>
                    {localStorage.getItem('userData') === null ? <Authorization/> : <Redirect to={'/quotes'}/>}
                </Route>
                <Route exact strict path={'/'}>
                    {localStorage.getItem('userData') === null ? <Authorization/> : <Redirect to={'/quotes'}/>}
                </Route>
                <Route path={'/companyList'}>
                    {localStorage.getItem('userData') === null ? <Authorization/> : <CompanyList/>}
                </Route>
                <Route path={'/quotes'}>
                    {localStorage.getItem('userData') === null ? <Authorization/> : <Quotes/>}
                </Route>
            </div>
        </Router>
    );
}

export default App;
