import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import ProductTable from '../../helpers/ProductTable';
import FileUploadComponent from '../../helpers/FileUploadComponent';

const ListProducts = () => {
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
    useEffect(() => {
        axios.get(`http://localhost:5006/api/products/get`, { params: pagination })
            .then((res) => {
                const newItems = res.data.map(item => ({
                    id: item.id,
                    name: item.name,
                    price: item.price,
                    region: item.productAddress.districtName,
                    isactive: item.isActive
                }));
                setItems(newItems);
                console.log(newItems)
            })
            .catch((err) => {
                console.log(err)
            })
    }, []);

    return (
        <div className='container d-flex flex-column gap-3 mt-3'>
            <div className='col-md-11 px-3'>
                <div className='d-flex gap-2 justify-content-end'>
                    <a style={{ borderRadius: "3px" }} href='/newproduct' className='btn btn-success btn-sm float-end fs-6'><FontAwesomeIcon icon={faPlus} style={{ fontSize: "15px" }} /> Add Product</a>
                    <a href='/deletedproducts' style={{ borderRadius: '3px' }} className='btn btn-danger btn-sm fs-6'><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} />  Recycle Bin</a>
                </div>
            </div>
            <div>
                <ProductTable rows={items} onPagination={handlePagination} />
                <FileUploadComponent modalButtonColor={"primary"} fileLabel="Image Upload" uploadUrl="http://localhost:5006/api/Products/Upload" />
            </div>
        </div>
    )
}

export default ListProducts