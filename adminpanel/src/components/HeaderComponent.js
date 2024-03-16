import { CButton, CCollapse, CContainer, CDropdown, CDropdownDivider, CDropdownItem, CDropdownMenu, CDropdownToggle, CForm, CFormInput, CHeader, CHeaderBrand, CHeaderNav, CHeaderToggler, CNavItem, CNavLink, CNavbarToggler } from '@coreui/react'
import React, { useState } from 'react'

const HeaderComponent = () => {
    const [visible, setVisible] = useState(true)
    return (
        <>
            <CHeader>
                <CContainer fluid>
                    <CNavbarToggler
                        aria-label="Toggle navigation"
                        aria-expanded={visible}
                        onClick={() => setVisible(!visible)}
                    />
                    <CHeaderBrand href="#">Dashboard</CHeaderBrand>
                    <CHeaderToggler onClick={() => setVisible(visible)} />
                    <CCollapse className="header-collapse" visible={visible}>
                        <CForm className="d-flex">
                            <CFormInput className="me-2" type="search" placeholder="Search" />
                            <CButton type="submit" color="success" variant="outline">
                                Search
                            </CButton>
                        </CForm>
                    </CCollapse>
                </CContainer>
            </CHeader>
        </>
    )
}

export default HeaderComponent