import React, { useEffect, useState } from 'react'
import axios from 'axios';
import TableComponent from '../../helpers/TableComponent';

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
        <TableComponent rows={items} />
    )
}

export default ListProducts