import { faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import axios from 'axios';
import React, { useState } from 'react'

const EditAttributeTypeModal = ({ onModalClose, attType }) => {
    const [name, setName] = useState(attType.name);
    const handleNameChange = (e) => {
        setName(e.target.value);
    }
    const handleSubmit = (e) => {
        e.preventDefault();
        if (name !== '') {
            attType.name = name;
            axios.put(`http://localhost:5006/api/attributes/updateType`, attType)
                .then(response => {
                    console.log(response);
                    onModalClose();
                })
                .catch(error => {
                    console.error(error);
                });
        }
    }
    return (
        <>
            <div>
                <button style={{ borderRadius: "3px" }} type="button" className="btn btn-warning btn-sm me-2" data-bs-toggle="modal" data-bs-target={`#editStaticBackdrop${attType.id}`}>
                    <FontAwesomeIcon icon={faPenToSquare} style={{ fontSize: "15px" }} />
                </button>
                <div className="modal fade" id={`editStaticBackdrop${attType.id}`} data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby={`editStaticBackdrop${attType.id}Label`} aria-hidden="true">
                    <div className="modal-dialog" >
                        <div className="modal-content h-100 px-3" style={{ width: "700px" }}>
                            <div className="modal-header">
                                <h1 className="modal-title fs-5" id={`editStaticBackdrop${attType.id}Label`}>Edit Attribute Type</h1>
                                <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div className="modal-body mt-5">
                                <form onSubmit={handleSubmit} className="row g-3">
                                    <div className="mb-3 row">
                                        <label htmlFor="inputName" class="col-sm-2 col-form-label fs-6">Name:</label>
                                        <div className="col-sm-10">
                                            <input type="text" className="form-control" id="inputName" value={name} onChange={handleNameChange} />
                                        </div>
                                    </div>
                                    <div className="modal-footer">
                                        <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                                        <button type="submit" className="btn btn-success" data-bs-dismiss="modal" >Save</button>
                                    </div>
                                </form>
                            </div>
                        </div>
                    </div>
                </div>
            </div >
        </>
    )
}

export default EditAttributeTypeModal