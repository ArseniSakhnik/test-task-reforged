import axios from "axios";

export default class QuotationService {


    _apiBase = '/'

    getLastQuotations = () => {
        const user = JSON.parse(localStorage.getItem('userData'))
        return axios.get(this._apiBase + 'quotations/get-quotations', {
                withCredentials: true,
                headers: {
                    "Accept": "application/json",
                    "Authorization": "Bearer " + user.token
                }
            }
        )
    }

    getQuotationByTickerAndDate = (ticker, startDate, endDate) => {

        const user = JSON.parse(localStorage.getItem('userData'))
        return axios.post(this._apiBase + 'quotations/get-quotations-by-ticker-and-date',{
            ticker: ticker,
            startDate: startDate,
            endDate: endDate
        }, {
            withCredentials: true,
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + user.token
            }
        })
    }
}