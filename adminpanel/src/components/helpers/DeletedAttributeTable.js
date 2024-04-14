import { faRedo, faTrashAlt } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import 'react-toastify/dist/ReactToastify.css';
import { TablePagination } from '@mui/base';
import { ChevronLeftRounded, ChevronRightRounded, FirstPageRounded, LastPageRounded } from '@mui/icons-material';
import React, { useCallback, useEffect, useState } from 'react';
import axios from 'axios';
import { ToastContainer, toast } from 'react-toastify';

const DeletedAttributeTable = () => {
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

    const fetchItems = useCallback(() => {
        axios.get('http://localhost:5006/api/attributes/getdeletedtypes')
            .then((res) => {
                const newItems = res.data.map(item => ({
                    id: item.id,
                    name: item.name
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

    const handleSubmit = (e, rowId) => {
        e.preventDefault();
        console.log(rowId)
        axios.put(`http://localhost:5006/api/attributes/softdeletetype?id=${rowId}`)
            .then(response => {
                console.log(response);
                fetchItems();
                toast('Attribute type recycled successfully', { type: 'success' })
            })
            .catch(error => {
                console.error(error);
            });
    }
    const handleDelete = (e, rowId) => {
        e.preventDefault();
        axios.delete(`http://localhost:5006/api/attributes/deletetype?id=${rowId}`)
            .then(response => {
                console.log(response);
                fetchItems();
                toast('Attribute type deleted successfully', { type: 'success' })
            })
            .catch(error => {
                console.error(error);
            });
    }

    return (
        <div className='d-flex flex-column container'>
            <ToastContainer
                position='bottom-right'
            />
            <div className='mt-5 col-11'>
                <a href='/admin/attributetypes' style={{ borderRadius: "3px" }} className="btn btn-success btn-sm float-end fs-6" > Attribute Type Table
                </a>
            </div>
            <div className='mt-3'>
                {rows.length === 0 &&
                    <div className='col-md-11'>
                        <div className="alert alert-info text-center" role="alert">
                            No attribute found
                        </div>
                    </div>}
                <div className='paginated-table col-md-11'  >
                    <table className='table' aria-label="custom pagination table">
                        <thead>
                            <tr className='table-headers row justify-content-center text-center align-items-center'>
                                <th className='col-1'>NO</th>
                                <th className='col-2'>TYPE</th>
                                <th className='col-4'>ID</th>
                                <th className='col-4'>#</th>
                            </tr>
                        </thead>
                        <tbody>
                            {(Array.isArray(rows) && rowsPerPage > 0
                                ? rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                : rows
                            ).map((row, index) => (
                                <tr key={row.name} className='row justify-content-center text-center'>
                                    <td className='col-1'>{index + 1}</td>
                                    <td className='col-2'>{row.name}</td>
                                    <td className='col-4'>{row.id}</td>
                                    <td className='col-4' align="right">
                                        <div className='d-flex justify-content-center'>
                                            <div>
                                                <button type='button' style={{ borderRadius: "3px" }} className='btn btn-success btn-sm me-2' data-bs-toggle="modal" data-bs-target={`#modalRedo${row.id}`} ><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faRedo} /> </button>
                                                <form onSubmit={(e) => handleSubmit(e, row.id)}>
                                                    <div class="modal fade" id={`modalRedo${row.id}`} tabindex="-1" aria-labelledby={`#modalRedo${row.id}Label`} aria-hidden="true">
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
                                                </form>
                                            </div>
                                            <div>
                                                <button class="btn btn-danger btn-sm me-2" data-bs-toggle="modal" data-bs-target={`#modal${row.id}`}><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashAlt} /></button>
                                                <form onSubmit={(e) => handleDelete(e, row.id)}>
                                                    <div class="modal fade" id={`modal${row.id}`} tabindex="-1" aria-labelledby={`modal${row.id}Label`} aria-hidden="true">
                                                        <div class="modal-dialog">
                                                            <div class="modal-content">
                                                                <div class="modal-body">
                                                                    Are you sure you want to delete this attribute type permanently?
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
export default DeletedAttributeTable