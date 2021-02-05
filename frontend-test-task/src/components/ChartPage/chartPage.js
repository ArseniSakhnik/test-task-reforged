import React, {useState, useEffect} from "react";
import Chart from "./Chart";
import Navbar from "../Navbar";
import QuotationService from "../../Services/QuototationService";
import './chartPage.css'


export default function ChartPage() {

    const quotationService = new QuotationService()

    const [quotations, setQuotations] = useState([])

    const [currentQuotation, setCurrentQuotation] = useState({})

    useEffect(() => {
        quotationService.getLastQuotations()
            .then(response => {
                setQuotations(response.data)
                setCurrentQuotation(response.data[0])
            })
    }, [])

    const dataFormat = (data) => {
        return data.substring(8, 10) + '.' + data.substring(5, 7) + '.' + data.substring(2, 4)
    }

    const choseCurrentQuotation = (ticker, name, date, price, ) => {
        console.log('current ticker ', ticker)
        setCurrentQuotation({
            ticker
        })
    }

    const row = () => {
        return quotations.map(item => {
            return (
                <tr className='row-quote-chart' key={item.ticker} onClick={() => choseCurrentQuotation(item.ticker)}>
                    <th className={'ticker-chart'} scope="row">{item.ticker}</th>
                    <th>
                        {item.price}
                        <div
                            className="quotes-table-rub"> {item.currencyUnit === 'SUR' ? 'RUB' : item.currencyUnit}
                        </div>
                    </th>

                    <th>{dataFormat(item.date)}</th>
                </tr>
            )
        })
    }

    return (
        <div>
            <Navbar/>
            <div className={'chart-page'}>

                <div className={'chart-block'}>
                    <div>

                    </div>
                    <Chart
                        ticker={currentQuotation.ticker}
                        time={'1h'}
                    />
                </div>
                <div className="quotes-page-chart">
                    <div className='quotes-table-chart'>
                        <table className="table quote-table-area-chart">
                            <thead>
                            <tr className="quotes-table-head">
                                <th className={'talbe-head '} scope="col">Тикер</th>
                                <th className={'talbe-head '} scope="col">Цена</th>
                                <th className={'talbe-head '} scope="col">Дата</th>
                            </tr>
                            </thead>
                            <tbody>
                            {row()}
                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    )
}