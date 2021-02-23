import React, {useState, useEffect} from "react";
import Navbar from "../Navbar";
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'
import './companyList.css'
import CompanyService from "../../Services/CompanyService";
import RedactModalWindow from "./RedactModalWindow";
import RemoveModalWindow from "./RemoveModalWindow";
import Error from "../Error"

export default function CompanyList() {

    const companyService = new CompanyService()

    const [companies, setCompanies] = useState([])

    const [error, setError] = useState('')

    const [rowData, setRowData] = useState({
        type: '',
        id: '',
        name: '',
        ticker: ''
    })

    useEffect(() => {
        companyService.getCompanies().then((response) => {
            setCompanies(response.data)
        })
            .catch(e => {
                if (e.response) {
                    setError(e.response.data)
                } else {
                    setError(e.message)
                }
            })
    }, [])

    const sendRowDataToModalWindow = (type, id, name, ticker) => {
        setRowData({
            type, id, name, ticker
        })
    }


    const changeCompany = (id, name, ticker) => {
        companyService.changeCompany(id, name, ticker)
            .then((response) => {

                const changedCompanies = [...companies]

                const changedCompany = {
                    id: response.data.id,
                    name: response.data.name,
                    ticker: response.data.ticker
                }

                for (let i = 0; i < changedCompanies.length; i++) {
                    if (changedCompanies[i].id === changedCompany.id) {
                        changedCompanies[i] = changedCompany
                        break;
                    }
                }

                setCompanies(changedCompanies)

            })
            .catch((e) => {
                    if (e.response) {
                        alert(e.response.data)
                    } else {
                        alert(e.message)
                    }
                }
            )
    }

    const addCompany = (id, name, ticker) => {
        companyService.addCompany(id, name, ticker)
            .then((response) => {
                const company = {
                    id: response.data.id,
                    name: response.data.name,
                    ticker: response.data.ticker
                }
                setCompanies(oldCompanies => [...oldCompanies, company])
            })
            .catch((e) => {
                if (e.response) {
                    alert(e.response.data)
                } else {
                    alert(e.message)
                }
            })
    }

    const removeCompany = (id, name, ticker) => {
        companyService.removeCompany(id, name, ticker)
            .then(response => {
                const oldCompanies = [...companies].filter(item => item.id !== response.data.id)
                setCompanies(oldCompanies)
            })
            .catch(ex => {
                alert(ex.message)
            })
    }


    const row = () => {
        return companies.map(item => {
            return (
                <tr key={item.id}>
                    <th scope="row">{item.name}</th>
                    <td>{item.ticker}</td>
                    <td>
                        <div className="buttons-company-list">
                            <button
                                type="button"
                                className="btn btn-warning redact-remove-company"
                                data-toggle="modal"
                                data-target="#redactModal"
                                onClick={() => sendRowDataToModalWindow('redact', item.id, item.name, item.ticker)}
                            >Редактировать
                            </button>
                            |
                            <button
                                type="button"
                                className="btn btn-warning redact-remove-company"
                                data-toggle="modal"
                                data-target="#removeModal"
                                onClick={() => sendRowDataToModalWindow('remove', item.id, item.name, item.ticker)}
                            >Удалить</button>
                        </div>
                    </td>
                </tr>
            )
        })
    }

    if (error.length > 0) {
        return (
            <div>
                <Navbar/>
                <Error errorMessage={error}/>
            </div>
        )
    }

    return (
        <div>
            <Navbar/>
            <RemoveModalWindow id={rowData.id} name={rowData.name} ticker={rowData.ticker}
                               removeCompany={removeCompany}/>
            <RedactModalWindow type={rowData.type} id={rowData.id} name={rowData.name} ticker={rowData.ticker}
                               changeCompany={changeCompany} addCompany={addCompany}/>
            <h1 className="companyList">Список компаний</h1>
            <div className="companyListPage">
                <div className="main-content">
                    <button className="btn btn-warning btn-add-company"
                            onClick={() => sendRowDataToModalWindow('add', '', '', '')}
                            data-toggle="modal"
                            data-target="#redactModal"
                    >Добавить компанию
                    </button>
                    <table className="table companyList-table">
                        <thead>
                        <tr className="company-list-table-head">
                            <th scope="col">Компания</th>
                            <th scope="col">Тикер</th>
                            <th></th>
                        </tr>
                        </thead>
                        <tbody>
                        {row()}
                        </tbody>
                    </table>
                </div>
            </div>


        </div>
    )
}