import React, { useEffect, useState } from 'react'
import axios from 'axios';
import TableComponent from '../../helpers/ProductTable';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faPlus } from '@fortawesome/free-solid-svg-icons';

const ListProducts = () => {
    const [items, setItems] = useState([]);
    useEffect(() => {
        axios.get('http://localhost:5006/api/products/get')
            .then((res) => {
                const newItems = res.data.map(item => ({
                    name: item.name,
                    price: item.price,
                    region: 'unknown',
                    isactive: item.isactive
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
            <div className='col-md-11'>
                <a style={{ borderRadius: "3px" }} href='/newproduct' className='btn btn-success btn-sm float-end fs-6'><FontAwesomeIcon icon={faPlus} style={{ fontSize: "15px" }} /> Add Product</a>
            </div>
            <div>
                <TableComponent rows={items} />
            </div>
        </div>
    )
}

export default ListProducts