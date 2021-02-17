import axios from "axios";

export default class CompanyService {
    _apiBase = 'https://localhost:5001/'

    getCompanies = () => {
        const user = JSON.parse(localStorage.getItem('userData'))
        return axios.get(this._apiBase + 'companies/get-companies', {
            withCredentials: true,
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + user.token
            }
        })
    }

    companyPostRequest = (link, id, name, ticker) => {
        const user = JSON.parse(localStorage.getItem('userData'))
        return axios.post(this._apiBase + link, {
            id, name, ticker
        }, {
            withCredentials: true,
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + user.token
            }
        })
    }

    removeCompany = (id, name, ticker) => {
        return this.companyPostRequest('companies/remove-company', id, name, ticker.toUpperCase().replace(/\s/g, ''))
    }

    addCompany = (id, name, ticker) => {
        console.log('sending data ', id, ' ', name, ' ', ticker)
        return this.companyPostRequest('companies/add-company', 1, name, ticker.toUpperCase().replace(/\s/g, ''))
    }

    changeCompany = (id, name, ticker) => {
        return this.companyPostRequest('companies/change-company', id, name, ticker.toUpperCase().replace(/\s/g, ''))
    }
}