import React, {useState, useEffect} from "react";
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap/dist/js/bootstrap.js'
import './redactModalWindow.css'

export default function RedactModalWindow({type, id, name, ticker, changeCompany, addCompany}) {

    const [companyName, setCompanyName] = useState(name)
    const [companyTicker, setCompanyTicker] = useState(ticker)
    const [buttonClasses, setButtonClasses] = useState("btn btn-primary save-button btn-warning first-style-button")
    const [disabled, setDisabled] = useState(false)

    useEffect(() => {
        setCompanyName(name)
        setCompanyTicker(ticker)
    }, [id])

    useEffect(() => {
        if (companyName.length == 0 || companyTicker.length == 0) {
            setButtonClasses("btn btn-primary save-button btn-warning disabled-button")
            setDisabled(true)
        } else {
            setButtonClasses("btn btn-primary save-button btn-warning first-style-button")
            setDisabled(false)
        }
    }, [companyTicker, companyName])

    const onCompanyNameChanged = (event) => {
        setCompanyName(event.target.value)
    }

    const onCompanyTickerChanged = (event) => {
        setCompanyTicker(event.target.value)
    }

    const onRedactSubmit = (event) => {
        event.preventDefault()
        changeCompany(id, companyName, companyTicker)
    }

    const onAddSubmit = (event) => {
        event.preventDefault()
        addCompany(id, companyName, companyTicker)
    }

    return (
        <div className="modal fade" id="redactModal" tabIndex="-1" role="dialog" aria-labelledby="exampleModalLabel"
             aria-hidden="true" data-toggle="modal">
            <div className="modal-dialog" role="document">
                <div className="modal-content">
                    <div className="modal-body">
                        <h2 className='modalWindowHeader'>{type == 'redact' ? 'Редактирование компании' : 'Добавление компании'}</h2>
                        <form onSubmit={type == 'redact' ? onRedactSubmit : onAddSubmit}>
                            <div className="form-group">
                                <label className='input-label-name'>Название компании</label>
                                <input
                                    type="text"
                                    className="form-control"
                                    value={companyName}
                                    onChange={onCompanyNameChanged}
                                />
                            </div>
                            <div className="form-group">
                                <label className='input-label-ticker'>Тикер</label>
                                <input
                                    type="text"
                                    className="form-control companyTicker-input"
                                    value={companyTicker}
                                    onChange={onCompanyTickerChanged}
                                />
                            </div>
                            <button type="submit" className={buttonClasses} disabled={disabled}>Сохранить</button>
                            <button
                                type='button'
                                className="btn btn-primary cancel-button btn-warning second-style-button"
                                aria-label="Close"
                                data-dismiss="modal"
                            >Отменить</button>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    )
}