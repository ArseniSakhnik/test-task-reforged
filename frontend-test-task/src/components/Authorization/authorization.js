import React, {useState, useEffect} from 'react'
import Navbar from "../Navbar"
import Error from "../Error"
import UserService from "../../Services/UserService"
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'
import './authorizationWindow.css'
import {useHistory} from 'react-router-dom'




export default function Authorization() {

    const userService = new UserService()

    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const [activeButton, setActiveButton] = useState(true)
    const history = useHistory()

    const onUsernameChanged = (event) => {
        setUsername(event.target.value)
    }

    const onPasswordChanged = (event) => {
        setPassword(event.target.value)
    }

    const handleSubmit = (event) => {

        event.preventDefault()
        if (username.replace(/\s/g, '').length > 0
            && password.replace(/\s/g, '').length > 0) {
            setActiveButton(false)
            userService.authentication(username, password)
                .then((response) => {
                    localStorage.setItem('userData', JSON.stringify(response.data))
                    history.go(0)
                })
                .catch((e) => {
                    if (e.response) {
                        setError(e.response.data)
                    } else {
                        setError(e.message)
                    }

                    setActiveButton(true)
                })
        } else {
            setError('Please fill password and login field')
        }
    }

    return (
        <div className={'authorize'}>
            <Navbar/>
            <div className="container authorizationForm">
                <div className="row">
                    <div className="col-md-5 mx-auto">
                        <div id="first">
                            <div className="myform form ">
                                <div className="logo mb-3">
                                    <h1 className="greetingLabel">Добро пожаловать на биржу</h1>
                                </div>
                                <form action="" method="post" name="login" onSubmit={handleSubmit}>
                                    <div className="form-group">
                                        <label>Логин</label>
                                        <input type="text"
                                               name="email"
                                               className="form-control form-input"
                                               aria-describedby="emailHelp"
                                               value={username}
                                               onChange={onUsernameChanged}
                                        />
                                    </div>
                                    <div className="form-group">
                                        <label>Пароль</label>
                                        <input type="password"
                                               name="password"
                                               className="form-control form-input"
                                               aria-describedby="emailHelp"
                                               value={password}
                                               onChange={onPasswordChanged}
                                        />
                                    </div>
                                    <div className="text-center ">
                                        <button type="submit" className=" btn btn-warning btn-primary myBtn disabled-auth" disabled={!activeButton}>Войти
                                        </button>
                                    </div>
                                </form>
                                {error.length > 0 ? <Error errorMessage={error}/> : <></> }
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}