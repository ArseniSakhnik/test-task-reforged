import React, {useState, useEffect} from "react";
import Chart from "./Chart";
import Navbar from "../Navbar";
import QuotationService from "../../Services/QuototationService";
import './chartPage.css'


export default function ChartPage() {

    const quotationService = new QuotationService()

    const [quotations, setQuotations] = useState([])

    const [dateType, setDateType] = useState('1w')

    const [currentQuotation, setCurrentQuotation] = useState({
        ticker: '',
        name: '',
        date: '',
        price: '',
        currencyUnit: '',
    })

    useEffect(() => {
        quotationService.getLastQuotations()
            .then(response => {
                setQuotations(response.data)
                setCurrentQuotation(response.data[0])
                choseCurrentQuotation(response.data[0].ticker, response.data[0].companyName,
                    response.data[0].date, response.data[0].price, response.data[0].currencyUnit)
            })
    }, [])

    const dataFormat = (data) => {
        return data.substring(8, 10) + '.' + data.substring(5, 7) + '.' + data.substring(2, 4)
    }

    const choseCurrentQuotation = (ticker, name, date, price, currencyUnit) => {
        console.log('current ticker ', ticker)
        console.log('items: ', ticker, ' ', name, ' ', date, ' ', price)
        setCurrentQuotation({
            ticker,
            name,
            date,
            price,
            currencyUnit
        })
    }

    const row = () => {
        return quotations.map(item => {
            return (
                <tr className={item.ticker === currentQuotation.ticker ? 'row-quote-chart active-row' : 'row-quote-chart'}
                    key={item.ticker}
                    onClick={() => choseCurrentQuotation(item.ticker, item.companyName, item.date, item.price, item.currencyUnit)}>
                    <th className={'ticker-chart'} scope="row">{item.ticker}</th>
                    <th>
                        {item.price}
                        <div
                            className={"quotes-table-rub"}> {item.currencyUnit === 'SUR' ? 'RUB' : item.currencyUnit}
                        </div>
                    </th>

                    <th>{dataFormat(item.date)}</th>
                </tr>
            )
        })
    }

    const oneMoreDateFormat = (date) => {

        const day = date.substring(0, 1) == '0' ? date.substring(1, 2) : date.substring(0, 1)
        let month;
        switch (date.substring(3, 5)) {
            case '01':
                month = 'января'
                break;
            case '02':
                month = 'февраля'
                break;
            case '03':
                month = 'марта'
                break;
            case '04':
                month = 'апреля'
                break;
            case '05':
                month = 'мая'
                break;
            case '06':
                month = 'июня'
                break;
            case '07':
                month = 'июля'
                break;
            case '08':
                month = 'августа'
                break;
            case '09':
                month = 'сентября'
                break;
            case '10':
                month = 'октября'
                break;
            case '11':
                month = 'ноября'
                break;
            case '12':
                month = 'декабрая'
                break;
        }

        return day + ' ' + month + ' ' + '20' + date.substring(6, 8)

    }

    return (
        <div>
            <Navbar/>
            <div className={'chart-page'}>
                <div className={'chart-block'}>
                    <div className={'words'}>
                        <div className={'chart-company-name'}>
                            {currentQuotation.name}
                        </div>
                        <div className={'chart-company-ticker'}>
                            ({currentQuotation.ticker})
                        </div>
                        <div>
                            <div className={'chart-company-price'}>
                                {currentQuotation.price}
                            </div>
                            <div className={'chart-company-currencyUnit'}>
                                {currentQuotation.currencyUnit === 'SUR' ? 'RUB' : currentQuotation.currencyUnit}
                            </div>
                            <div className={'chart-company-date'}>
                                {oneMoreDateFormat(dataFormat(currentQuotation.date))}
                            </div>
                        </div>
                    </div>
                    <Chart
                        ticker={currentQuotation.ticker}
                        time={dateType}
                    />
                    <div className={'buttons-chart'}>
                        <button className={'btn btn-warning btn-chart'} onClick={() => setDateType('5m')}>5 минут
                        </button>
                        <button className={'btn btn-warning btn-chart'} onClick={() => setDateType('1h')}>1 час</button>
                        <button className={'btn btn-warning btn-chart'} onClick={() => setDateType('4h')}>4 часа
                        </button>
                        <button className={'btn btn-warning btn-chart'} onClick={() => setDateType('1w')}>1 день
                        </button>
                        <button className={'btn btn-warning btn-chart'} onClick={() => setDateType('1m')}>1 неделя
                        </button>
                        <button className={'btn btn-warning btn-chart'} onClick={() => setDateType('1y')}>1 месяц
                        </button>
                        <button className={'btn btn-warning btn-chart'}>1 год</button>
                    </div>
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