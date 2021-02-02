import './App.css';
import {BrowserRouter as Router, Redirect, Route} from 'react-router-dom'
import Navbar from "../Navbar";
import Authorization from "../Authorization";

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
            </div>
        </Router>
    );
}

export default App;
