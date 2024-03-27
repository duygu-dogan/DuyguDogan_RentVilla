import { CButton, CCol, CContainer, CForm, CFormCheck, CFormInput, CFormLabel, CFormSelect, CFormTextarea, CInputGroup } from '@coreui/react'
import axios from 'axios';
import React, { useEffect, useState } from 'react'
import Select from 'react-select';

const NewProduct = () => {
    const [States, setStates] = useState([]);
    const [SelectedState, setSelectedState] = useState(null);
    const [Cities, setCities] = useState([]);
    const [selectedCity, setSelectedCity] = useState(null);
    const [Districts, setDistricts] = useState([]);
    const [attributeTypes, setAttributeTypes] = useState([]);
    const [selectedOption, setSelectedOption] = useState([]);
    const [options, setOptions] = useState([]);
    useEffect(() => {
        axios('http://localhost:5006/api/region/getallstates')
            .then(data => {
                setStates(data.data);
                console.log(data)
            })
            .catch((error) => {
                console.error('Error:', error);
            });
    }, []);
    const handleStateChange = (e) => {
        const stateId = e.target.value;
        setSelectedState(stateId);
        axios.get(`http://localhost:5006/api/region/getallcities?stateId=${stateId}`)
            .then((res) => {
                setCities(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    };
    const handleCityChange = (e) => {
        const cityId = e.target.value;
        setSelectedCity(cityId);
        axios.get(`http://localhost:5006/api/region/getalldistricts?cityId=${cityId}`)
            .then((res) => {
                setDistricts(res.data);
            })
            .catch((err) => {
                console.log(err);
            });
    };
    const handleDistrictChange = (e) => {
        const districtId = e.target.value;
    }

    useEffect(() => {
        axios.get('http://localhost:5006/api/attributes/gettypes')
            .then((res) => {
                setAttributeTypes(res.data);
                Promise.all(attributeTypes.map(type =>
                    axios.get(`http://localhost:5006/api/attributes/getbytypeid/typeid?typeId=${type.id}`)
                ))
                    .then((responses) => {
                        const newOptions = responses.map((response, index) => ({
                            key: index,
                            label: attributeTypes[index].name,
                            options: response.data.map((item) => ({
                                value: item.id,
                                label: item.description
                            }))
                        }));
                        setOptions(newOptions);
                        console.log(options)
                    })
                    .catch((err) => {
                        console.log(err);
                    });
            })
    }, []);
    const handleChange = (selectedOption) => {
        setSelectedOption(selectedOption);
        console.log(`Option selected:`, selectedOption);
    };
    return (
        <CContainer className='p-5 w-75'>
            <CForm className="row g-3">
                <CCol md={6}>
                    <CFormInput type="text" id="inputName" label="Name" />
                </CCol>
                <CCol md={6}>
                    <CFormInput type="text" id="inputPrice" label="Price" />
                </CCol>
                <CCol xs={12}>
                    <CFormInput id="inputAddress" label="Address" placeholder="1234 Main St" />
                </CCol>
                <CCol md={4}>
                    <CFormSelect id="inputRegion" label="Region" onChange={handleStateChange}>
                        <option>Choose...</option>
                        {States.map((item) => (
                            <option key={item.id} value={item.id}>{item.name}</option>
                        ))}
                    </CFormSelect>
                </CCol>
                <CCol md={4}>
                    <CFormSelect id="inputCity" label="City" onChange={handleCityChange}>
                        <option>Choose...</option>
                        {Cities.map((item) => (
                            <option key={item.id} value={item.id}>{item.name}</option>
                        ))}
                    </CFormSelect>
                </CCol>
                <CCol md={4}>
                    <CFormSelect id="inputDistrict" label="District" onChange={handleDistrictChange}>
                        <option>Choose...</option>
                        {Districts.map((item) => (
                            <option key={item.id} value={item.id}>{item.name}</option>
                        ))}
                    </CFormSelect>
                </CCol>
                <CCol xs={6}>
                    <CInputGroup className="mb-3 d-flex flex-column">
                        <CFormLabel >Image Upload</CFormLabel>
                        {/* <CInputGroupText as="label" htmlFor="inputGroupFile01">Upload</CInputGroupText> */}
                        <CFormInput className='w-50' type="file" id="inputGroupFile01" />
                    </CInputGroup>
                </CCol>
                <CCol xs={12}>
                    <CFormTextarea
                        id="exampleFormControlTextarea1"
                        label=""
                        rows={3}
                        text="Must be 8-20 words long."
                    ></CFormTextarea>
                </CCol>
                <CCol xs={12} className='d-flex justify-content-between row row-cols-5'>
                    {options.map((item, index) => (
                        <Select
                            key={index}
                            className='col'
                            isMulti
                            name={item.label}
                            options={item.options}
                            onChange={handleChange}
                        />
                    ))}
                </CCol>
                <CCol xs={12}>
                    <CButton color="primary" type="submit">Save Changes</CButton>
                </CCol>
            </CForm>
        </CContainer>
    )
}

export default NewProduct