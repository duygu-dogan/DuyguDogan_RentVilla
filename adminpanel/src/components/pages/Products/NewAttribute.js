import React, { useState } from 'react'
import APIPostComponent from '../../helpers/APIPostComponent';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

const NewAttribute = ({ onFormChange }) => {
    // const [name, setName] = useState('');
    // const [description, setDescription] = useState('');
    // const [isActive, setIsActive] = useState(true);
    // const handleNameChange = (e) => {
    //     setName(e.target.value);
    // }
    // const handleDescriptionChange = (e) => {
    //     setDescription(e.target.value);
    // }
    // const handleIsActiveChange = (e) => {
    //     setIsActive(e.target.checked);
    // }
    const [inputs, setInputs] = useState([]);
    const handleFormChange = (newInputs) => {
        setInputs(newInputs);
        if (onFormChange) onFormChange(newInputs);
    }
    return (
        <div>
            <button style={{ borderRadius: "3px" }} type="button" className="btn btn-success btn-sm float-end fs-6" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                <FontAwesomeIcon icon={faPlus} style={{ fontSize: "15px" }} /> Add Attribute
            </button>
            <div className="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div className="modal-dialog" style={{ height: "350px" }}>
                    <div className="modal-content h-100 px-3" style={{ width: "700px" }}>
                        <div className="modal-header">
                            <h1 className="modal-title fs-5" id="staticBackdropLabel">Add Attribute</h1>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body mt-5">
                            <form className="row g-3">
                                <div className="mb-3 row">
                                    <label htmlFor="inputName" class="col-sm-2 col-form-label">Name</label>
                                    <div className="col-sm-10">
                                        <input type="text" className="form-control" id="inputName" value={inputs.name} onChange={handleFormChange} />
                                    </div>
                                </div>
                                <div className="mb-3 row">
                                    <label htmlFor="inputDescription" className="col-sm-2 col-form-label">Description</label>
                                    <div className="col-sm-10">
                                        <input type="text" className="form-control" id="inputDescription" value={inputs.description} onChange={handleFormChange} />
                                    </div>
                                </div>
                                <div className="mb-3 row">
                                    <label htmlFor="flexSwitchCheckChecked" className="col-sm-2 form-check-label">Is Active</label>
                                    <div className="form-check form-switch col-sm-9 ps-5 ms-3">
                                        <input className="form-check-input" type="checkbox" role="switch" id="flexSwitchCheckChecked" value={inputs.isActive} onChange={handleFormChange} />
                                    </div>
                                </div>
                                <div className="modal-footer">
                                    <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                                    <button type="submit" className="btn btn-success">Save</button>
                                </div>
                            </form>
                            {/* <APIPostComponent name={name} description={description} isactive={isActive} /> */}
                        </div>
                    </div>
                </div>
            </div>
        </div >
    )
}

export default NewAttribute