import axios from "axios"
import pbkdf2 from 'crypto-js/pbkdf2'


export default class UserService {
    _apiBase = 'https://localhost:5001/'

    bcrypt = require('bcryptjs');

    authentication = async (username, password) => {

        return this.getSalt(username)
            .then(response => {
                const buffer = new Uint8Array(response.data)
                const backendSalt = this.convertToBase64(buffer)
                const randomBytes = this.generateRandomBytes()
                const clientSalt = this.convertToBase64(randomBytes)


                password = password + backendSalt + clientSalt

                const hashedPassword = this.bcrypt.hashSync(password, 10)

                return axios.post(this._apiBase + 'users/authenticate', {
                    username,
                    password: hashedPassword,
                    salt: clientSalt
                })

            })
    }

    getSalt = async (username) => {
        return axios.post(this._apiBase + 'users/get-salt', {
            username
        }, {
            responseType: 'arraybuffer',
            withCredentials: true
        })
    }

    generateRandomBytes = () => {
        const bytes = new Uint8Array(16)
        window.crypto.getRandomValues(bytes)
        return bytes
    }

    convertToBase64 = (bytes) => {

        let toEncode = ''

        bytes.forEach(item => {
            toEncode += (String.fromCharCode(item))
        })

        return window.btoa(toEncode)
    }
}