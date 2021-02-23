import axios from "axios";

export default class QuotationService {


    _apiBase = 'https://localhost:5001/'

    getLastQuotations = () => {
        return axios.get(this._apiBase + 'quotations/get-quotations', {
                withCredentials: true,
                headers: {
                    "Accept": "application/json"
                }
            }
        )
    }

    getQuotationByTickerAndDate = (ticker, startDate, endDate) => {

        return axios.post(this._apiBase + 'quotations/get-quotations-by-ticker-and-date',{
            ticker: ticker,
            startDate: startDate,
            endDate: endDate
        }, {
            withCredentials: true,
            headers: {
                "Accept": "application/json"
            }
        })
    }
}