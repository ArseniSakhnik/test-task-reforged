import axios from "axios";

export default class UserService {
    _apiBase = '/'

    authentication = (username, password) => {
        return axios.post(this._apiBase + 'users/authenticate', {
            username,
            password
        }, {
            withCredentials: true
        })
    }

    getTest = () => {
        const user = JSON.parse(localStorage.getItem('userData'))
        console.log(user.token)
        return axios.get(this._apiBase + 'users/get',  {
            withCredentials: true,
            headers: {
                "Accept": "application/json",
                "Authorization": "Bearer " + user.token
            }
        })
    }
}