import React, {useState, useEffect} from "react";
import Navbar from "../Navbar";
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'
import './companyList.css'
import CompanyService from "../../Services/CompanyService";
import RedactModalWindow from "./RedactModalWindow";
import RemoveModalWindow from "./RemoveModalWindow";
import {forEach} from "react-bootstrap/ElementChildren";

export default function CompanyList() {

    const companyService = new CompanyService()

    const [companies, setCompanies] = useState([])

    const [rowData, setRowData] = useState({
        type: '',
        id: '',
        name: '',
        ticker: ''
    })

    useEffect(() => {
        companyService.getCompanies().then((response) => {
            console.log(response)
            setCompanies(response.data)
        })
    }, [])

    useEffect(() => {
        console.log(companies)
    })

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
            .catch(() => {
                    alert("Такой тикер уже есть в базе данных")
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
            .catch(() => {
                alert("Такой тикер уже есть в базе данных")
            })
    }

    const removeCompany = (id, name, ticker) => {
        console.log('removing company')
        console.log({id, name, ticker})
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

    return (
        <div>
            <RemoveModalWindow id={rowData.id} name={rowData.name} ticker={rowData.ticker}
                               removeCompany={removeCompany}/>
            <RedactModalWindow type={rowData.type} id={rowData.id} name={rowData.name} ticker={rowData.ticker}
                               changeCompany={changeCompany} addCompany={addCompany}/>
            <Navbar/>
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