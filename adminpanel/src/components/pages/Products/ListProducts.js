import React, { useEffect, useState } from 'react'
import axios from 'axios';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import ProductTable from '../../helpers/ProductTable';

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
                const newItems = res.data.nonDeletedProducts.map(item => ({
                    id: item.id,
                    image: item.productImages[0],
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
                    <a style={{ borderRadius: "3px" }} href='/admin/newproduct' className='btn btn-success btn-sm float-end fs-6'><FontAwesomeIcon icon={faPlus} style={{ fontSize: "15px" }} /> Add Product</a>
                    <a href='/admin/deletedproducts' style={{ borderRadius: '3px' }} className='btn btn-danger btn-sm fs-6'><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} />  Recycle Bin</a>
                </div>
            </div>
            <div>
                <ProductTable rows={items} onPagination={handlePagination} />
            </div>
        </div>
    )
}

export default ListProducts