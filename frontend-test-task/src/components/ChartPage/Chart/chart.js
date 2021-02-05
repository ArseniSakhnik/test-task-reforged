import React, {useEffect, useState} from "react";
import {LineChart, Line, XAxis, YAxis, CartesianGrid, Tooltip, Legend,} from "recharts";
import QuotationService from "../../../Services/QuototationService";
import {forEach} from "react-bootstrap/ElementChildren";

export default function Chart({ticker, time}) {

    const quotationService = new QuotationService()

    const [quotations, setQuotations] = useState([])

    const [data, setData] = useState([])

    const dataFormat = (data) => {
        return data.substring(8, 10) + '.' + data.substring(5, 7) + '.' + data.substring(2, 4)
    }

    const setDataToChart = (startYear, startMonth, startDay, startHours, startMinutes, startSeconds) => {


        const startDate = new Date(startYear,
            startMonth,
            startDay,
            startHours,
            startMinutes,
            startSeconds)

        const endDate = new Date()

        console.log(startDate, ' ', endDate)

        const dataChart = []

        quotationService.getQuotationByTickerAndDate(ticker, startDate, endDate)
            .then((response) => {
                console.log(response.data)

                response.data.forEach(item => {
                    dataChart.push({
                        name: dataFormat(item.date),
                        price: item.price
                    })
                })

                setData(dataChart)

                setQuotations(response.data)
            })
    }

    useEffect(() => {
        if (ticker && ticker.length > 0) {

            const date = new Date()

            if (time === '5m') {
                console.log('5m')

                setDataToChart(date.getFullYear(),
                    date.getMonth(),
                    date.getDay(),
                    date.getHours(),
                    date.getMinutes() - 5,
                    date.getSeconds())

            } else if (time === '1h') {

                setDataToChart(date.getFullYear(),
                    date.getMonth(),
                    date.getDay(),
                    date.getHours() - 1,
                    date.getMinutes(),
                    date.getSeconds())

            } else if (time === '4h') {

                setDataToChart(date.getFullYear(),
                    date.getMonth(),
                    date.getDay(),
                    date.getHours() - 4,
                    date.getMinutes(),
                    date.getSeconds())

            } else if (time === '1w') {

                setDataToChart(date.getFullYear(),
                    date.getMonth(),
                    date.getDay() - 7,
                    date.getHours(),
                    date.getMinutes(),
                    date.getSeconds())

            } else if (time === '1m') {

                setDataToChart(date.getFullYear(),
                    date.getMonth() - 1,
                    date.getDay(),
                    date.getHours(),
                    date.getMinutes(),
                    date.getSeconds())

            } else if (time === '1y') {

                setDataToChart(date.getFullYear() - 1,
                    date.getMonth(),
                    date.getDay(),
                    date.getHours(),
                    date.getMinutes(),
                    date.getSeconds())

            }
        }

    }, [ticker, time])


    return (
        <div>
            <LineChart
                width={650}
                height={300}
                data={data}
                margin={{
                    top: 0, right: 0, left: 0, bottom: 0,
                }}
            >
                <CartesianGrid strokeDasharray="3 3"/>
                <XAxis dataKey="name"/>
                <YAxis/>
                <Tooltip/>
                <Line type="monotone" dataKey="price" stroke="#8884d8" fill="#8884d8"/>
            </LineChart>
        </div>
    )
}