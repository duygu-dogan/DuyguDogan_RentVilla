import { cilAccountLogout, cilCalendar, cilHome, cilLayers, cilSettings, cilSpeedometer, cilUser } from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import { CNavGroup, CNavItem, CNavTitle, CSidebar, CSidebarBrand, CSidebarHeader, CSidebarNav } from '@coreui/react'
import React, { useEffect, useState } from 'react'

const SidebarComponent = () => {
    const [isSmallScreen, setIsSmallScreen] = useState(window.innerWidth < 768);

    useEffect(() => {
        const checkScreenSize = () => {
            setIsSmallScreen(window.innerWidth < 768);
        };

        window.addEventListener('resize', checkScreenSize);

        return () => window.removeEventListener('resize', checkScreenSize);
    }, []);
    return (
        <CSidebar className="border-end h-100" colorScheme="dark" narrow={isSmallScreen}>
            <CSidebarHeader className="border-bottom">
                <CIcon className='fs-2' customClassName="nav-icon-logo" icon={cilHome} />
                <CSidebarBrand style={{ background: 'none' }}>RentVilla</CSidebarBrand>
            </CSidebarHeader>
            <CSidebarNav>
                <CNavTitle>Admin Panel</CNavTitle>
                <CNavItem href="/"><CIcon customClassName="nav-icon" icon={cilSpeedometer} /> Dashboard</CNavItem>

                <CNavGroup
                    toggler={
                        <>
                            <CIcon customClassName="nav-icon" icon={cilLayers} /> Products
                        </>
                    }
                >
                    <CNavItem href="/admin/products"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> List All Products</CNavItem>
                    <CNavItem href="/admin/newproduct"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> Add New Product</CNavItem>
                    <CNavItem href="/admin/attributetypes"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> Attributes</CNavItem>
                </CNavGroup>
                <CNavGroup
                    toggler={
                        <>
                            <CIcon customClassName="nav-icon" icon={cilCalendar} /> Reservations
                        </>
                    }
                >
                    <CNavItem href="#"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> Nav dropdown item</CNavItem>
                    <CNavItem href="#"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> Nav dropdown item</CNavItem>
                </CNavGroup>
                <CNavGroup
                    toggler={
                        <>
                            <CIcon customClassName="nav-icon" icon={cilUser} /> Customers
                        </>
                    }
                >
                    <CNavItem href="#"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> Nav dropdown item</CNavItem>
                    <CNavItem href="#"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> Nav dropdown item</CNavItem>
                </CNavGroup>
                <CNavGroup
                    toggler={
                        <>
                            <CIcon customClassName="nav-icon" icon={cilAccountLogout} /> Manage Roles
                        </>
                    }
                >
                    <CNavItem href="#"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> Nav dropdown item</CNavItem>
                    <CNavItem href="#"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> Nav dropdown item</CNavItem>
                </CNavGroup>
                <CNavGroup
                    toggler={
                        <>
                            <CIcon customClassName="nav-icon" icon={cilSettings} /> Setting
                        </>
                    }
                >
                    <CNavItem href="#"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> Nav dropdown item</CNavItem>
                    <CNavItem href="#"><span className="nav-icon"><span className="nav-icon-bullet"></span></span> Nav dropdown item</CNavItem>
                </CNavGroup>

            </CSidebarNav>
        </CSidebar>
    )
}

export default SidebarComponent