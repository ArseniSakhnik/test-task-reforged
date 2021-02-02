import React from 'react'
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'
import './navbar.css'


export default function Navbar() {
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
                            <a className="nav-link navbar-item" href="#">
                                Графики
                            </a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link navbar-item" href="#">
                                Котировки
                            </a>
                        </li>
                        <li className="nav-item dropdown">
                            <a className="nav-link dropdown-toggle navbar-item" href="#" id="navbarDropdown"
                               role="button" data-toggle="dropdown"
                               aria-haspopup="true" aria-expanded="false">
                                Настройки
                            </a>
                            <div className="dropdown-menu dropdown-link-menu" aria-labelledby="navbarDropdown">
                                <a className="dropdown-item navbar-item dropdown-link" href="#">Список компаний</a>
                            </div>
                        </li>
                    </ul>
                    <ul className="navbar-nav ">
                        <li className="nav-item">
                            <a className="nav-link navbar-item" href="#">
                                Иванов И.И.
                            </a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link stick">
                                |
                            </a>
                        </li>
                        <li className="nav-item">
                            <a className="nav-link navbar-item" href="#">
                                Выход
                            </a>
                        </li>
                    </ul>
                </div>
            </nav>
        </div>
    )
}