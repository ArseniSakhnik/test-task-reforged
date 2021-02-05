import React, {useState, useEffect} from 'react'
import Navbar from "../Navbar"
import Error from "../Error"
import UserService from "../../Services/UserService"
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'
import './authorizationWindow.css'


export default function Authorization() {

    const userService = new UserService()

    const [username, setUsername] = useState('')
    const [password, setPassword] = useState('')
    const [error, setError] = useState('')
    const [activeButton, setActiveButton] = useState(true)

    const onUsernameChanged = (event) => {
        setUsername(event.target.value)
    }

    const onPasswordChanged = (event) => {
        setPassword(event.target.value)
    }

    const handleSubmit = (event) => {
        setActiveButton(false)
        event.preventDefault()
        userService.authentication(username, password)
            .then((response) => {
                console.log(response.data)
                localStorage.setItem('userData', JSON.stringify(response.data))
                window.location.reload()
            })
            .catch((e) => {
                console.log('error')
                console.log(e.message)
                if (e.message === 'Request failed with status code 400') {
                    setError('Неправильный логин или пароль')
                }
                setActiveButton(true)
            })
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