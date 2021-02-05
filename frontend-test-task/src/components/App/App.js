import './App.css';
import {BrowserRouter as Router, Redirect, Route} from 'react-router-dom'
import Authorization from "../Authorization";
import QuotationList from "../QuotationList/quotationList";
import CompanyList from "../CompanyList";
import ChartPage from "../ChartPage";

function App() {
    return (
        <Router>
            <div className="App">
                <Route path={'/authentication'}>
                    {localStorage.getItem('userData') === null ? <Authorization/> : <Redirect to={'/QuotationList-list'}/>}
                </Route>
                <Route exact strict path={'/'}>
                    {localStorage.getItem('userData') === null ? <Authorization/> : <Redirect to={'/QuotationList-list'}/>}
                </Route>
                <Route path={'/company-list'}>
                    {localStorage.getItem('userData') === null ? <Authorization/> : <CompanyList/>}
                </Route>
                <Route path={'/quotationList-list'}>
                    {localStorage.getItem('userData') === null ? <Authorization/> : <QuotationList/>}
                </Route>
                <Route path={'/charts'}>
                    {localStorage.getItem('userData') === null ? <Authorization/> : <ChartPage/>}
                </Route>
            </div>
        </Router>
    );
}

export default App;
