import { CTable } from '@coreui/react'
import axios from 'axios'
import React from 'react'

const ProductList = () => {
    const items = [];
    axios.get('http://localhost:5006/api/products/getproducts')
        .then((res) => {
            res.data.forEach(item => {
                items.push({
                    name: item.name,
                    description: item.description,
                    price: item.price,
                    deposit: item.deposit,
                    _cellProps: { id: { scope: 'row' }, class: { colSpan: 2 } }
                })
                console.log(items);
            });
        })
        .catch((err) => {
            console.log(err)
        })

    const columns = [
        {
            key: 'name',
            label: 'Name',
            _props: { scope: 'col' },
        },
        {
            key: 'description',
            _props: { scope: 'col' },
        },
        {
            key: 'price',
            label: 'Price',
            _props: { scope: 'col' },
        },
        {
            key: 'deposit',
            label: 'Deposit',
            _props: { scope: 'col' },
        },
    ]
    // const items = [
    //     {
    //         id: 1,
    //         name: 'Mark',
    //         description: 'Otto',
    //         heading_2: '@mdo',
    //         _cellProps: { id: { scope: 'row' } },
    //     },
    //     {
    //         id: 2,
    //         class: 'Jacob',
    //         heading_1: 'Thornton',
    //         heading_2: '@fat',
    //         _cellProps: { id: { scope: 'row' } },
    //     },
    //     {
    //         id: 3,
    //         class: 'Larry the Bird',
    //         heading_2: '@twitter',
    //         _cellProps: { id: { scope: 'row' }, class: { colSpan: 2 } },
    //     },
    // ]
    return <CTable striped columns={columns} items={items} />
}

export default ProductList