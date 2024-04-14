import { cilBell, cilEnvelopeOpen, cilList } from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import { CAvatar, CBadge, CButton, CContainer, CForm, CFormInput, CHeader, CHeaderBrand, CHeaderNav, CLink, CModal, CModalBody, CModalFooter, CModalHeader, CModalTitle, CNavItem, CNavLink } from '@coreui/react'
import React, { useState } from 'react'
import Avatar from '../assets/images/vesika.jpg';
import SignalRService from './services/SignalRService';

const HeaderComponent = () => {
    const unreadMessages = JSON.parse(localStorage.getItem('unreadMessages')) || 0;
    const [messNumber, setMessNumber] = useState(unreadMessages);
    const [visible, setVisible] = useState(false)
    const messages = JSON.parse(localStorage.getItem('messages')) || [];

    const handleModalBtn = () => {
        setVisible(!visible)
        setMessNumber(0);
        localStorage.setItem('unreadMessages', JSON.stringify(0));
    };

    return (
        <>
            <CHeader>
                <CContainer fluid className='mr-3'>
                    <SignalRService procedureNames={["receiveProductAddedMessage", "receiveReservationCreatedMessage"]} setMessNumber={setMessNumber} hubUrls={["product-hub", "reservation-hub"]} />
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
                                <CButton onClick={() => handleModalBtn()} color='light' style={{ padding: "0" }} className="position-relative">
                                    <CLink>
                                        <CIcon className="header-icon" icon={cilEnvelopeOpen} />
                                    </CLink>
                                    {messNumber !== 0 && (
                                        <CBadge
                                            className="border border-light p-2"
                                            color="danger"
                                            position="top-end"
                                            shape="rounded-circle"
                                        >
                                            {messNumber}
                                            <span className="visually-hidden">New alerts</span>
                                        </CBadge>
                                    )}
                                    <CModal
                                        scrollable
                                        visible={visible}
                                        onClose={() => setVisible(false)}
                                        aria-labelledby="ScrollingLongContentExampleLabel2"
                                    ><CModalHeader>
                                            <CModalTitle id="ScrollingLongContentExampleLabel2">Messages</CModalTitle>
                                        </CModalHeader>
                                        <CModalBody>
                                            {messages.length === 0 ? <div className='alert alert-warning'>No messages found</div> :
                                                <div className="">
                                                    <table class="table table-striped">
                                                        <thead>
                                                            <tr >
                                                                <th scope="col">#</th>
                                                                <th scope="col">Messages</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody>
                                                            {messages.map((message, index) => (
                                                                <tr className=''>
                                                                    <th scope="row">{index + 1}</th>
                                                                    <td className='d-flex'>{message}</td>
                                                                </tr>
                                                            ))}

                                                        </tbody>
                                                    </table>
                                                </div>
                                            }
                                        </CModalBody>
                                        <CModalFooter>
                                            <CButton color="secondary" onClick={() => setVisible(false)}>
                                                Close
                                            </CButton>
                                        </CModalFooter>
                                    </CModal>
                                </CButton>

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