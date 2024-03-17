import { cilBell, cilEnvelopeOpen, cilList } from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import { CAvatar, CButton, CContainer, CForm, CFormInput, CHeader, CHeaderBrand, CHeaderNav, CLink, CNavItem, CNavLink } from '@coreui/react'
import React from 'react'
import Avatar from '../assets/images/vesika.jpg';

const HeaderComponent = () => {
    return (
        <>
            <CHeader>
                <CContainer fluid>

                    <CHeaderBrand href="#">Dashboard</CHeaderBrand>
                    <CHeaderNav className='d-flex flex-row gap-4'>
                        <CNavItem className='d-flex gap-2 me-3'>
                            <CNavLink>
                                <CLink>
                                    <CIcon className='header-icon' icon={cilBell} />
                                </CLink>
                            </CNavLink>
                            <CNavLink>
                                <CLink>
                                    <CIcon className="header-icon" icon={cilList} />
                                </CLink>
                            </CNavLink>
                            <CNavLink>
                                <CLink>
                                    <CIcon className="header-icon" icon={cilEnvelopeOpen} />
                                </CLink>
                            </CNavLink>
                        </CNavItem>
                        <CForm className="d-flex">
                            <CFormInput className="me-2" type="search" placeholder="Search" />
                            <CButton type="submit" color="success" variant="outline">
                                Search
                            </CButton>
                        </CForm>
                        <CAvatar className='mt-2' src={Avatar} />
                    </CHeaderNav>
                </CContainer>
            </CHeader>
        </>
    )
}

export default HeaderComponent