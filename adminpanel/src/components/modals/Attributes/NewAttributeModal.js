import React, { useState } from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';

const NewAttributeModal = ({ attributeTypeId, onModalClose }) => {
    const [description, setDescription] = useState('');
    const [isActive, setIsActive] = useState(true);
    const handleDescriptionChange = (e) => {
        setDescription(e.target.value);
    }
    const handleIsActiveChange = (e) => {
        setIsActive(e.target.checked);
    }
    const handleSubmit = (e) => {
        e.preventDefault();
        if (attributeTypeId) {
            console.log(attributeTypeId, description, isActive)
            axios.post('http://localhost:5006/api/Attributes/Add', { attributeTypeId, description, isActive })
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
        <div>
            <button style={{ borderRadius: "3px" }} type="button" className="btn btn-success btn-sm float-end" data-bs-toggle="modal" data-bs-target="#staticBackdrop2">
                <FontAwesomeIcon icon={faPlus} /> Add
            </button>
            <div className="modal fade" id="staticBackdrop2" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content h-100 px-3" style={{ width: "700px" }}>
                        <div className="modal-header">
                            <h1 className="modal-title fs-5" id="staticBackdropLabel">Add Attribute</h1>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body mt-5">
                            <form onSubmit={handleSubmit} className="row g-3">
                                <div className="mb-3 row">
                                    <label htmlFor="inputDescription" className="col-sm-2 col-form-label">Description</label>
                                    <div className="col-sm-10">
                                        <input type="text" className="form-control" id="inputDescription" value={description} onChange={handleDescriptionChange} />
                                    </div>
                                </div>
                                <div className="mb-3 row">
                                    <label htmlFor="flexSwitchCheckChecked" className="col-sm-2 form-check-label">Is Active</label>
                                    <div className="form-check form-switch col-sm-9 ps-5 ms-3">
                                        <input className="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckChecked" value={isActive} onChange={handleIsActiveChange} />
                                    </div>
                                </div>
                                <div className="modal-footer">
                                    <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                                    <button type="submit" className="btn btn-success" data-bs-dismiss="modal">Save</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div >
    )
}

export default NewAttributeModal