import { CCol, CRow } from '@coreui/react'
import React, { useEffect, useState } from 'react'
import WidgetComponent from './WidgetComponent'
import ReservationTable from './helpers/ReservationTable'
import axios from 'axios'
import Cookies from 'js-cookie'

const MainComponent = () => {
    const [items, setItems] = useState([]);
    const [pagination, setPagination] = useState(
        { Page: 0, Size: 10 }
    );
    const handlePagination = (rowSize, pageSize) => {
        console.log(rowSize, pageSize)
        setPagination(
            { Page: pageSize, Size: rowSize }
        );
    }
    const accessToken = Cookies.get('RentVilla.Cookie_AT')
    console.log(accessToken);
    axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;
    useEffect(() => {
        axios.get(`http://localhost:5006/api/reservations/getactivereservations`,
            {
                params: pagination,
                withCredentials: true
            })
            .then((res) => {
                console.log(res);
                const newItems = res.data.activeReservations.map(item => ({
                    id: item.id,
                    user: item.userName,
                    name: item.productName,
                    totalCost: item.totalCost,
                    startDate: item.startDate,
                    endDate: item.endDate,
                    isPaid: item.isPaid,
                    status: item.reservationStatus,
                }));
                setItems(newItems);
                console.log(newItems)
            })
            .catch((err) => {
                console.log(err)
            })
    }, []);
    return (
        <>
            <CRow className='main-widget-container col-md-11 mx-auto'>
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
            <div className='container d-flex flex-column mt-2'>
                <div className='container-fluid col-md-11'>
                    <h4 className='container-fluid mb-2'>Active Reservations</h4>
                </div>
                <ReservationTable rows={items} onPagination={handlePagination} />
            </div>
        </>
    )
}

export default MainComponent