import React from "react"
import './removeModalWindow.css'

export default function RemoveModalWindow({id, name, ticker, removeCompany}) {


    return (
        <div className="modal fade" id="removeModal" tabIndex="-1" role="dialog" aria-labelledby="exampleModalLabel"
             aria-hidden="true" data-toggle="modal">
            <div className="modal-dialog" role="document">
                <div className="modal-content">
                    <div className="modal-body">
                        <div className='line string'>Удалить компанию <div className='line bold'>{name}</div> из списка?
                        </div>
                        <div className='buttons'>
                            <button type="submit" className="btn btn-primary btn-warning first-style-button remove-button-modal"
                                    data-dismiss="modal"
                                    onClick={() => removeCompany(id, name, ticker)}
                            >Удалить
                            </button>
                            <button
                                type='button'
                                className="btn btn-primary btn-warning second-style-button cancel-button-modal"
                                data-dismiss="modal"
                            >Отмена
                            </button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    )
}