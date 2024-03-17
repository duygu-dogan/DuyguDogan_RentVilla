import { CCol, CRow } from '@coreui/react'
import React from 'react'
import WidgetComponent from './WidgetComponent'

const MainComponent = () => {
    return (
        <>
            <CRow className='main-widget-container'>
                <CCol sm={3}>
                    <WidgetComponent className='main-widget'
                        color={"primary"}
                        title={'Products'} />
                </CCol>
                <CCol sm={3}>
                    <WidgetComponent className='main-widget'
                        color={"danger"}
                        title={'Products'} />
                </CCol>
                <CCol sm={3}>
                    <WidgetComponent className='main-widget'
                        color={"warning"}
                        title={'Products'} />
                </CCol>
                <CCol sm={3}>
                    <WidgetComponent className='main-widget'
                        color={"success"}
                        title={'Products'} />
                </CCol>
            </CRow>
        </>
    )
}

export default MainComponent