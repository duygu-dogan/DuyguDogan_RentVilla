import { CButton, CCard, CCol, CContainer, CForm, CFormCheck, CFormInput, CFormLabel, CFormSelect, CFormText, CFormTextarea, CInputGroup, CInputGroupText } from '@coreui/react'
import React from 'react'

const NewProduct = () => {
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
                    <CFormSelect id="inputRegion" label="Region">
                        <option>Choose...</option>
                        <option>...</option>
                    </CFormSelect>
                </CCol>
                <CCol md={4}>
                    <CFormSelect id="inputCity" label="City">
                        <option>Choose...</option>
                        <option>...</option>
                    </CFormSelect>
                </CCol>
                <CCol md={4}>
                    <CFormSelect id="inputDistrict" label="District">
                        <option>Choose...</option>
                        <option>...</option>
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
                <CCol xs={12} className='d-flex justify-content-between'>

                    <CFormCheck type="checkbox" id="gridCheck" label="Check me out" />
                    <CFormCheck type="checkbox" id="gridCheck" label="Check me out" />
                    <CFormCheck type="checkbox" id="gridCheck" label="Check me out" />
                    <CFormCheck type="checkbox" id="gridCheck" label="Check me out" />
                </CCol>
                <CCol xs={12}>
                    <CButton color="primary" type="submit">Save Changes</CButton>
                </CCol>
            </CForm>
        </CContainer>
    )
}

export default NewProduct