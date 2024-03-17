import { CButton, CCard, CCol, CContainer, CForm, CFormCheck, CFormInput, CFormLabel, CFormSelect, CFormText, CFormTextarea, CInputGroup, CInputGroupText } from '@coreui/react'
import React from 'react'

const NewProduct = () => {
    return (
        <CContainer className='p-5 w-75'>
            <CForm className="row g-3">
                <CCol md={6}>
                    <CFormInput type="email" id="inputEmail4" label="Email" />
                </CCol>
                <CCol md={6}>
                    <CFormInput type="password" id="inputPassword4" label="Password" />
                </CCol>
                <CCol xs={12}>
                    <CFormInput id="inputAddress" label="Address" placeholder="1234 Main St" />
                </CCol>
                <CCol xs={12}>
                    <CFormInput id="inputAddress2" label="Address 2" placeholder="Apartment, studio, or floor" />
                </CCol>
                <CCol md={6}>
                    <CFormInput id="inputCity" label="City" />
                </CCol>
                <CCol md={4}>
                    <CFormSelect id="inputState" label="State">
                        <option>Choose...</option>
                        <option>...</option>
                    </CFormSelect>
                </CCol>
                <CCol md={2}>
                    <CFormInput id="inputZip" label="Zip" />
                </CCol>
                <CCol xs={12} className='d-flex'>

                    <CFormCheck type="checkbox" id="gridCheck" label="Check me out" />
                    <CFormCheck type="checkbox" id="gridCheck" label="Check me out" />
                    <CFormCheck type="checkbox" id="gridCheck" label="Check me out" />
                    <CFormCheck type="checkbox" id="gridCheck" label="Check me out" />
                </CCol>
                <CCol xs={12}>
                    <CCol xs={6}>
                        <CFormTextarea
                            id="exampleFormControlTextarea1"
                            label="Example textarea"
                            rows={3}
                            text="Must be 8-20 words long."
                        ></CFormTextarea>
                    </CCol>
                    <CCol xs={6}>
                        <CInputGroup className="mb-3">
                            <CInputGroupText as="label" htmlFor="inputGroupFile01">Upload</CInputGroupText>
                            <CFormInput type="file" id="inputGroupFile01" />
                        </CInputGroup>
                    </CCol>
                </CCol>
                <CCol xs={12}>
                    <CButton color="primary" type="submit">Save Changes</CButton>
                </CCol>
            </CForm>
        </CContainer>
    )
}

export default NewProduct