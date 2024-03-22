import { faPlus } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import React, { useEffect, useState } from 'react'

const DynamicForm = ({ initialInputs, onFormChange }) => {
    const [inputFields, setInputFields] = useState(initialInputs);
    const handleFormChange = (index, event) => {
        let newInputs = [...inputFields];
        const { name, value, type, checked } = event.target;
        newInputs[index][name] = type === 'checkbox' ? checked : value;
        setInputFields(newInputs);
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
                            <form>
                                {inputFields.map((input, index) => {
                                    return (
                                        <div key={index} className='row g-3'
                                        >
                                            <div className='col-auto'>
                                                {/* <label htmlFor={`inputName${index}`} className="form-label">Name</label> */}
                                                <input type="text" class="form-control" id={`inputName${index}`}
                                                    placeholder='Name'
                                                    value={input.name}
                                                    onChange={event => handleFormChange(index, event)}
                                                />
                                            </div>
                                            <div className='col-auto'>
                                                {/* <label htmlFor={`inputDescription${index}`} class="form-label">Description</label> */}
                                                <input type="text" class="form-control" id={`inputDescription${index}`}
                                                    placeholder='Description'
                                                    value={input.description}
                                                    onChange={event => handleFormChange(index, event)}
                                                />
                                            </div>
                                            <div className="col-auto">
                                                {/* <label htmlFor={`flexSwitchCheckChecked${index}`} className="form-check-label">Is Active</label> */}
                                                <div class="form-check form-switch ps-5 ms-3">
                                                    <input class="form-check-input" type="checkbox" role="switch" id={`flexSwitchCheckChecked${index}`}
                                                        value={input.isActive}
                                                        onChange={event => handleFormChange(index, event)}
                                                    />
                                                </div>
                                            </div>
                                            <div className='col-auto'>
                                                <button className='btn btn-success' type='submit'>Add</button>
                                            </div>
                                        </div>
                                    )
                                })}
                            </form>
                            {/* <APIPostComponent name={name} description={description} isactive={isActive} /> */}
                        </div>
                    </div>
                </div>
            </div>
        </div >

    )
}
export default DynamicForm