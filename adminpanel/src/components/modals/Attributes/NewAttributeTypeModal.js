import React, { useState } from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import axios from 'axios';
import 'react-toastify/dist/ReactToastify.css';

const NewAttributeTypeModal = ({ onModalClose }) => {
    const [name, setName] = useState('');
    const handleNameChange = (e) => {
        setName(e.target.value);
        console.log(name)
    }
    const handleSubmit = (e) => {
        e.preventDefault();
        if (name) {
            axios.post(`http://localhost:5006/api/attributes/addtype?name=${name}`)
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
            <button style={{ borderRadius: "3px" }} type="button" className="btn btn-success btn-sm float-end fs-6" data-bs-toggle="modal" data-bs-target="#staticBackdrop">
                <FontAwesomeIcon icon={faPlus} style={{ fontSize: "15px" }} /> Add Attribute Type
            </button>
            <div className="modal fade" id="staticBackdrop" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div className="modal-dialog" >
                    <div className="modal-content h-100 px-3" style={{ width: "700px" }}>
                        <div className="modal-header">
                            <h1 className="modal-title fs-5" id="staticBackdropLabel">Add Attribute Type</h1>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body mt-5">
                            <form onSubmit={handleSubmit} className="row g-3">
                                <div className="mb-3 row">
                                    <label htmlFor="inputName" class="col-sm-2 col-form-label">Name:</label>
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
    )
}

export default NewAttributeTypeModal