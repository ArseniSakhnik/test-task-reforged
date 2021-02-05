import React, {useEffect, useState} from 'react'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'
import './navbar.css'
import {Link} from "react-router-dom";


export default function Navbar() {

    const [name, setName] = useState(() => {
        return localStorage.getItem('userData') === null ? '' : JSON.parse(localStorage.getItem('userData')).username
    })

    const logOut = () => {
        localStorage.removeItem('userData')
        window.location.reload()
    }

    const navbarAuthorizedAttributes = () => {
        return (
            <div>
                <ul className="navbar-nav ">
                    <li className="nav-item">
                        <a className="nav-link navbar-item" href="#">
                            {name}
                        </a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link stick">
                            |
                        </a>
                    </li>
                    <li className="nav-item">
                        <a className="nav-link navbar-item" onClick={logOut}>
                            Выход
                        </a>
                    </li>
                </ul>
            </div>
        )
    }

    console.log(name)

    return (
        <div>
            <nav className="navbar navbar-icon-top navbar-expand-lg navbar-dark bg-dark navbar-links">
                <button className="navbar-toggler" type="button" data-toggle="collapse"
                        data-target="#navbarSupportedContent"
                        aria-controls="navbarSupportedContent" aria-expanded="false" aria-label="Toggle navigation">
                    <span className="navbar-toggler-icon"></span>
                </button>
                <div className="collapse navbar-collapse navbar-items" id="navbarSupportedContent">
                    <ul className="navbar-nav mr-auto">
                        <li className="nav-item">
                            <Link to={{pathname: '/charts'}} className="nav-link navbar-item" >
                                Графики
                            </Link>
                        </li>
                        <li className="nav-item">
                            <Link to={{pathname: '/quotationList-list'}} className="nav-link navbar-item" href="#">
                                Котировки
                            </Link>
                        </li>
                        <li className="nav-item dropdown">
                            <a className="nav-link dropdown-toggle navbar-item" id="navbarDropdown"
                               role="button" data-toggle="dropdown"
                               aria-haspopup="true" aria-expanded="false">
                                Настройки
                            </a>
                            <div className="dropdown-menu dropdown-link-menu" aria-labelledby="navbarDropdown">
                                <Link to={{pathname: '/company-list'}} className="dropdown-item navbar-item dropdown-link" href="#">Список компаний</Link>
                            </div>
                        </li>
                    </ul>
                    {name.length > 0 ? navbarAuthorizedAttributes() : <div></div>}
                </div>
            </nav>
        </div>
    )
}