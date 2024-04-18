import axios from 'axios';
import React, { useEffect, useState } from 'react'
import UserTable from '../../helpers/UserTable';
import Cookies from 'js-cookie';

const ListUsers = () => {
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
    axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;

    useEffect(() => {
        axios.get(`http://localhost:5006/api/users/getallusers`, { params: pagination })
            .then((res) => {
                const newItems = res.data.map(item => ({
                    id: item.id,
                    firstName: item.firstName,
                    lastName: item.lastName,
                    email: item.email,
                    phoneNumber: item.phoneNumber,
                    address: item.address,
                    userName: item.userName,
                    birthday: item.birthday,
                    gender: item.gender
                }));
                setItems(newItems);
            })
            .catch((err) => {
                console.log(err)
            })
    }, []);

    return (
        <div className='container d-flex flex-column gap-3 mt-3'>
            {/* <div className='col-md-11 px-3'>
                <div className='d-flex gap-2 justify-content-end'>
                    <a style={{ borderRadius: "3px" }} href='/admin/newproduct' className='btn btn-success btn-sm float-end fs-6'><FontAwesomeIcon icon={faPlus} style={{ fontSize: "15px" }} /> Add Product</a>
                    <a href='/admin/deletedproducts' style={{ borderRadius: '3px' }} className='btn btn-danger btn-sm fs-6'><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} />  Recycle Bin</a>
                </div>
            </div> */}
            <div>
                <UserTable rows={items} onPagination={handlePagination} />
            </div>
        </div>
    )
}

export default ListUsers