import { faRedo, faTrashCan, faTrashRestore } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import 'react-toastify/dist/ReactToastify.css';
import { TablePagination } from '@mui/base';
import { ChevronLeftRounded, ChevronRightRounded, FirstPageRounded, LastPageRounded } from '@mui/icons-material';
import React, { useCallback, useEffect, useState } from 'react';
import axios from 'axios';
import { ToastContainer, toast } from 'react-toastify';
import Cookies from 'js-cookie';

const DeletedProductTable = () => {
    const [rows, setRows] = useState([]);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);

    const emptyRows =
        page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };
    const accessToken = Cookies.get('RentVilla.Cookie_AT')
    axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;
    const fetchItems = useCallback(() => {
        axios.get('http://localhost:5006/api/products/getdeletedproducts')
            .then((res) => {
                console.log(res)
                const newItems = res.data.deletedProducts.map(item => ({
                    id: item.id,
                    name: item.name,
                    price: item.price,
                    region: item.productAddress.districtName,
                    isactive: item.isactive
                }));
                setRows(newItems);
                console.log(newItems)
            })
            .catch((err) => {
                console.log(err)
            })
    }, []);
    useEffect(() => {
        fetchItems();
    }, [fetchItems]);

    const handleHardDelete = (e, rowId) => {
        e.preventDefault();
        axios.delete(`http://localhost:5006/api/products/delete?ProductId=${rowId}`)
            .then(response => {
                console.log(response);
                fetchItems();
                toast('Product deleted successfully', { type: 'success' })
            })
            .catch(error => {
                console.error(error);
                toast('Product deletion failed', { type: 'error' })
            });
    }
    const handleRecyle = (e, rowId) => {
        e.preventDefault();
        axios.put(`http://localhost:5006/api/products/softdelete?ProductId=${rowId}`)
            .then(response => {
                console.log(response);
                fetchItems();
                toast('Product moved to recycle bin successfully', { type: 'success' })
            })
            .catch(error => {
                console.error(error);
                toast('Product movement to recycle bin failed', { type: 'error' })
            });
    }

    return (
        <div className='d-flex flex-column container'>
            <ToastContainer
                position='bottom-right'
            />
            <div className='mt-5 col-11'>
                <a href='/admin/products' style={{ borderRadius: "3px" }} className="btn btn-success btn-sm float-end fs-6" > Back to Products
                </a>
            </div>
            <div className='mt-3'>
                {rows.length === 0 &&
                    <div className='col-md-11'>
                        <div className="alert alert-info text-center" role="alert">
                            No product found
                        </div>
                    </div>}
                <div className='paginated-table col-md-11'  >
                    <table className='table' aria-label="custom pagination table">
                        <thead>
                            <tr className='table-headers row justify-content-center text-center align-items-center'>
                                <th className='col-2'>Name</th>
                                <th className='col-2'>Price</th>
                                <th className='col-2'>Region</th>
                                <th className='col-3'>IsActive</th>
                                <th className='col-3'>#</th>
                            </tr>
                        </thead>
                        <tbody>
                            {(Array.isArray(rows) && rowsPerPage > 0
                                ? rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                : rows
                            ).map((row, index) => (
                                <tr key={row.name} className='row justify-content-center text-center'>
                                    <td className='col-2'>{row.name}</td>
                                    <td className='col-2'>{row.price}</td>
                                    <td className='col-2'>{row.region}</td>
                                    <td className='col-3'>
                                        <div className="form-check form-switch d-flex justify-content-center">
                                            <input
                                                className="form-check-input"
                                                type="checkbox"
                                                id="flexSwitchCheckDefault"
                                                // checked={row.isactive}
                                                checked={row.isactive}
                                            />
                                        </div>
                                    </td>
                                    <td className='col-3' align="right">
                                        <div className='d-flex justify-content-center'>
                                            <div className='d-flex'>
                                                <div>
                                                    <button type='button' style={{ borderRadius: "3px" }} className='btn btn-success btn-sm me-2' data-bs-toggle="modal" data-bs-target={`#modalRecyle${row.id}`} ><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashRestore} /> </button>
                                                    <form onSubmit={(e) => handleRecyle(e, row.id)}>
                                                        <div class="modal fade" id={`modalRecyle${row.id}`} tabindex="-1" aria-labelledby="modalRecycleLabel" aria-hidden="true">
                                                            <div class="modal-dialog">
                                                                <div class="modal-content">
                                                                    <div class="modal-body">
                                                                        Are you sure you want to recycle this attribute type?
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                                                                        <button type="submit" class="btn btn-success" data-bs-dismiss="modal">Recycle</button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </form></div>
                                                <div>
                                                    <button type='button' style={{ borderRadius: "3px" }} className='btn btn-danger btn-sm me-2' data-bs-toggle="modal" data-bs-target={`#modalDelete${row.id}`}><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} /> </button>
                                                    <form onSubmit={(e) => handleHardDelete(e, row.id)}>
                                                        <div class="modal fade" id={`modalDelete${row.id}`} tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                            <div class="modal-dialog">
                                                                <div class="modal-content">
                                                                    <div class="modal-body">
                                                                        Are you sure you want to permanently delete this product?
                                                                    </div>
                                                                    <div class="modal-footer">
                                                                        <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancel</button>
                                                                        <button type="submit" class="btn btn-danger" data-bs-dismiss="modal">Delete</button>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </form>
                                                </div>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                            ))}
                            {emptyRows > 0 && (
                                <tr style={{ height: 41 * emptyRows }}>
                                    <td colSpan={3} aria-hidden />
                                </tr>
                            )}
                        </tbody>
                        <tfoot>
                            <tr>
                                <TablePagination id='pagination'
                                    rowsPerPageOptions={[10, 25, 50, { label: 'All', value: -1 }]}
                                    colSpan={10}
                                    count={rows.length}
                                    rowsPerPage={rowsPerPage}
                                    page={page}
                                    slotProps={{
                                        select: {
                                            'aria-label': 'rows per page',
                                        },
                                        actions: {
                                            showFirstButton: true,
                                            showLastButton: true,
                                            slots: {
                                                firstPageIcon: FirstPageRounded,
                                                lastPageIcon: LastPageRounded,
                                                nextPageIcon: ChevronRightRounded,
                                                backPageIcon: ChevronLeftRounded,
                                            },
                                        },
                                    }}
                                    onPageChange={handleChangePage}
                                    onRowsPerPageChange={handleChangeRowsPerPage}
                                />
                            </tr>
                        </tfoot>
                    </table>
                </div>
            </div>
        </div>
    )
}
export default DeletedProductTable