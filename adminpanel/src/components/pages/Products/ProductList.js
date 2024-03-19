import { CTable } from '@coreui/react'
import axios from 'axios'
import React, { useEffect, useState } from 'react'
import ReactPaginate from 'react-paginate';
import PaginationButtons from '../../helpers/PaginationButtons';

const ProductList = () => {
    const [itemsPerPage, setItemsPerPage] = useState(1);

    const handleDropdownChange = (event) => {
        setItemsPerPage(Number(event.target.value));
        console.log(`Items per page: ${event.target.value}`)
    };
    const [items, setItems] = useState([]);

    useEffect(() => {
        axios.get('http://localhost:5006/api/products/get')
            .then((res) => {
                const newItems = res.data.map(item => ({
                    name: item.name,
                    description: item.description,
                    price: item.price,
                    deposit: item.deposit,
                    _cellProps: { id: { scope: 'row' }, class: { colSpan: 2 } }
                }));
                setItems(newItems);
            })
            .catch((err) => {
                console.log(err)
            })
    }, []);
    const [itemOffset, setItemOffset] = useState(0);
    const [pageCount, setPageCount] = useState(0);
    const [currentItems, setCurrentItems] = useState([]);
    useEffect(() => {
        const endOffset = itemOffset + itemsPerPage;
        console.log(`Loading items from ${itemOffset} to ${endOffset}`);
        setCurrentItems(items.slice(itemOffset, endOffset));
        setPageCount(Math.ceil(items.length / itemsPerPage));
    }, [itemOffset, items, itemsPerPage]);

    const handlePageClick = (event) => {
        const newOffset = (event.selected * itemsPerPage) % items.length;
        console.log(
            `User requested page number ${event.selected}, which is offset ${newOffset}`
        );
        setItemOffset(newOffset);
    };

    const columns = [
        {
            key: 'name',
            label: 'Name',
            _props: { scope: 'col' },
        },
        {
            key: 'description',
            label: 'Description',
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
    return (
        <>
            <div className='container'>
                <div>
                    <PaginationButtons onChange={handleDropdownChange} />
                    {itemsPerPage === 10 && <CTable />}
                    <CTable striped columns={columns} items={currentItems} />
                </div>
                <div className='d-flex justify-content-center fixed-bottom'>
                    <ReactPaginate
                        nextLabel="next >"
                        onPageChange={handlePageClick}
                        pageCount={pageCount}
                        pageRangeDisplayed={5}
                        marginPagesDisplayed={2}
                        previousLabel="< previous"
                        pageClassName='page-item'
                        pageLinkClassName='page-link'
                        previousClassName='page-item'
                        previousLinkClassName='page-link'
                        nextClassName='page-item'
                        nextLinkClassName='page-link'
                        breakLabel="..."
                        breakClassName='page-item'
                        breakLinkClassName='page-link'
                        containerClassName='pagination'
                        activeClassName='active'
                        renderOnZeroPageCount={null}
                    />
                </div>
            </div>
        </>
    )
}

export default ProductList