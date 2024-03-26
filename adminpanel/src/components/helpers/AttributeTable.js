import { faList, faPenToSquare, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { TablePagination } from '@mui/base';
import { ChevronLeftRounded, ChevronRightRounded, FirstPageRounded, LastPageRounded } from '@mui/icons-material';
import React, { useState } from 'react';
import axios from 'axios';
import NewAttributeModal from '../modals/NewAttributeModal';

const AttributeTable = ({ rows }) => {
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [rowToDelete, setRowToDelete] = useState(null);
    // const [switchState, setSwitchState] = useState(true);

    const emptyRows =
        page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };
    const handleSubmit = (e, row) => {
        e.preventDefault();
        axios.delete(`http://localhost:5006/api/attributes/deletetype?id=${row.id}`)
            .then(response => {
                console.log(response);
            })
            .catch(error => {
                console.error(error);
            });
    }
    // const handleSwitchChange = (row) => {
    //     const updatedRow = { ...row, isactive: !row.isActive }
    //     setSwitchState(updatedRow.isactive);
    // }

    return (
        <div className='container-fluid'>
            {rows.length === 0 &&
                <div className='col-md-11'>
                    <div className="alert alert-info text-center" role="alert">
                        No attribute found
                    </div>
                </div>}
            <div className='paginated-table col-md-11 mt-2'  >
                <table className='table' aria-label="custom pagination table">
                    <thead>
                        <tr className='row justify-content-center text-center align-items-center'>
                            <th className='col-1 '>NO</th>
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
                            <tr key={row.id} className='row justify-content-center text-center '>
                                <th className='col-1'>{index + 1}</th>
                                <td className='col-2'>{row.name}</td>
                                <td className='col-4'>{row.id}</td>
                                <td className='col-4' >
                                    <div className='d-flex justify-content-center'>
                                        <div>
                                            <button style={{ borderRadius: "3px" }} className='btn btn-secondary btn-sm me-2'><FontAwesomeIcon icon={faList} /></button>
                                        </div>
                                        <div>
                                            <button style={{ borderRadius: "3px" }} className='btn btn-warning btn-sm me-2'><FontAwesomeIcon icon={faPenToSquare} /></button>
                                        </div>
                                        <div>
                                            <button type='button' style={{ borderRadius: "3px" }} className='btn btn-danger btn-sm me-2' data-bs-toggle="modal" data-bs-target="#exampleModal" onClick={() => { setRowToDelete(row.id); console.log(rowToDelete) }}> <FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} /> </button>
                                            <form onSubmit={(e) => handleSubmit(e, row.id)}>
                                                <div class="modal fade" id="exampleModal" tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-body">
                                                                Are you sure you want to delete this attribute type?
                                                                If you delete this attribute type, all attributes under this type will be deleted.
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
                                        <div>
                                            <NewAttributeModal {...row.id} />
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
    )
}
export default AttributeTable