import React, { useEffect, useState } from 'react'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios';
import { CFormSelect } from '@coreui/react';
import { MultiSelect } from 'react-multi-select-component';

const ProductAttributesModal = ({ onModalClose, onAttributeChange }) => {
    const [attributeTypes, setAttributeTypes] = useState([]);
    const [selectedType, setSelectedType] = useState(null);
    const [attributes, setAttributes] = useState([]);
    const [selectedAttributes, setSelectedAttributes] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5006/api/attributes/gettypes')
            .then((res) => {
                setAttributeTypes(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    }, []);
    const handleTypeChange = (e) => {
        const typeId = e.target.value;
        setSelectedType(typeId);
        axios.get(`http://localhost:5006/api/attributes/getbytypeid/typeid?typeId=${typeId}`)
            .then((res) => {
                const options = res.data.map((item) => ({
                    label: item.description,
                    value: item.id
                }));
                setAttributes(options);
            })
            .catch((err) => {
                console.log(err);
            });
    };
    const handleSubmit = () => {
        onAttributeChange(selectedAttributes)
        console.log(selectedAttributes)
    }
    return (
        <div className='mt-3'>
            <button type="button" className="btn btn-primary" data-bs-toggle="modal" data-bs-target="#staticBackdrop2">
                <FontAwesomeIcon icon={faPlus} /> Add Attribute
            </button>
            <div className="modal fade" id="staticBackdrop2" data-bs-backdrop="static" data-bs-keyboard="false" tabindex="-1" aria-labelledby="staticBackdropLabel" aria-hidden="true">
                <div className="modal-dialog">
                    <div className="modal-content h-100 px-3" style={{ width: "700px" }}>
                        <div className="modal-header">
                            <h1 className="modal-title fs-5" id="staticBackdropLabel">Product Attributes</h1>
                            <button type="button" className="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                        </div>
                        <div className="modal-body mt-5">
                            <form className="row g-3">
                                <div className="mb-3 row">
                                    <label htmlFor="inputDescription" className="col-sm-3 col-form-label">Attribute Type</label>
                                    <div className="col-sm-9">
                                        <CFormSelect id="inputRegion" onChange={handleTypeChange}>
                                            <option>Select</option>
                                            {attributeTypes.map((item) => (
                                                <option key={item.id} value={item.id}>{item.name}</option>
                                            ))}
                                        </CFormSelect>
                                    </div>
                                </div>
                                <div className="mb-3 row">
                                    <label htmlFor="inputDescription" className="col-sm-3 col-form-label">Attributes</label>
                                    <div className="col-sm-9">
                                        <MultiSelect
                                            onChange={setSelectedAttributes}
                                            value={selectedAttributes}
                                            options={attributes}
                                            labelledBy='Select'
                                        />
                                    </div>
                                </div>
                                <div className="modal-footer">
                                    <button type="button" className="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                                    <button type="button" onClick={handleSubmit} className="btn btn-success" data-bs-dismiss="modal">Save</button>
                                </div>
                            </form>
                        </div>
                    </div>
                </div>
            </div>
        </div >
    )
}

export default ProductAttributesModal