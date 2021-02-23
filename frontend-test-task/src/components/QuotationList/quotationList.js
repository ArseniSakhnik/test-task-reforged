import React, {useState, useEffect} from 'react'
import Navbar from "../Navbar";
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'
import './quototations.css'
import QuotationService from "../../Services/QuototationService";
import Error from "../Error";

export default function QuotationList() {

    const quotationService = new QuotationService()

    const [quotations, setQuotations] = useState([])
    const [error, setError] = useState('')

    useEffect(() => {
        quotationService.getLastQuotations()
            .then(response => {
                setQuotations(response.data)
            })
            .catch((e) => {
                if (e.response) {
                    setError(e.response.data)
                } else {
                    setError(e.message)
                }
            })
    }, [])

    const dataFormat = (data) => {
        return data.substring(8, 10) + '.' + data.substring(5, 7) + '.' + data.substring(2, 4)
    }

    const row = () => {
        return quotations.map(item => {
            return (
                <tr key={item.ticker}>
                    <th scope="row" className="quotes-table-company-name">{item.companyName}</th>
                    <td>{item.ticker}</td>
                    <td>{item.price.toFixed(2)}
                        <div
                            className="quotes-table-rub"> {item.currencyUnit === 'SUR' ? 'RUB' : item.currencyUnit}
                        </div>
                    </td>
                    <td>{dataFormat(item.date)}</td>
                </tr>
            )
        })
    }

    return (
        <div>
            <Navbar/>
            {error.length > 0 ? <Error errorMessage={error}/> : (<div className="quotes-page">
                <div className=' quotes-table'>
                    <h1 className="quotes">Котировки</h1>
                    <table className="table quote-table-area">
                        <thead>
                        <tr className="quotes-table-head">
                            <th scope="col" className="quotes-table-company-name">Компания</th>
                            <th scope="col">Тикер</th>
                            <th scope="col">Цена</th>
                            <th scope="col">Дата</th>
                        </tr>
                        </thead>
                        <tbody>
                        {row()}
                        </tbody>
                    </table>
                </div>
            </div>)}
        </div>
    )
}