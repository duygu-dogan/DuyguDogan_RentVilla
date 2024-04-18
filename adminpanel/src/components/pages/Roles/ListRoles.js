import React, { useEffect, useState } from 'react'
import RoleListTable from '../../helpers/RoleListTable';
import axios from 'axios';
import AddRoleModalComponent from '../../modals/Roles/AddRoleModalComponent';
import { toast } from 'react-toastify';
import Cookies from 'js-cookie';

const ListRoles = () => {
    const [roles, setRoles] = useState([]);
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
    axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;

    useEffect(() => {
        axios.get(`http://localhost:5006/api/roles/getroles`, { params: pagination })
            .then((res) => {
                const entries = Object.entries(res.data);
                const newRoles = entries.map(([key, value]) => ({
                    id: key,
                    name: value
                }));
                setRoles(newRoles);
            })
            .catch((err) => {
                console.log(err)
            })
    }, []);
    const handleModalClose = (message, type) => {
        toast(message, { type: type });
        window.location.reload();
    }

    return (
        <div className='container d-flex flex-column gap-3 mt-3'>
            <div className='col-md-11 px-3'>
                <AddRoleModalComponent onModalClose={handleModalClose} />
            </div>
            <div>
                <RoleListTable rows={roles} onPagination={handlePagination} />
            </div>
        </div>
    )
}

export default ListRoles