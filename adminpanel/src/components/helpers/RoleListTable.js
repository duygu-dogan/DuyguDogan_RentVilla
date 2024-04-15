import React, { useState } from 'react'
import DeleteRoleModalComponent from '../modals/Roles/DeleteRoleModalComponent';
import AddRoleModalComponent from '../modals/Roles/AddRoleModalComponent';
import { ToastContainer, toast } from 'react-toastify';
import { TablePagination } from '@mui/base';
import { ChevronLeftRounded, ChevronRightRounded, FirstPageRounded, LastPageRounded } from '@mui/icons-material';

const RoleListTable = (rows, onPagination) => {
    console.log(rows);
    const roles = rows.rows;
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);


    const emptyRows =
        page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
        onPagination(page, rowsPerPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        onPagination(page, rowsPerPage);
        setPage(0);
    };
    const handleModalClose = (message, type) => {
        toast(message, { type: type });
        window.location.reload();
    }
    return (
        <div className='container-fluid'>
            <ToastContainer
                position='bottom-right'
            />
            <div className='paginated-table col-md-11'  >
                <table className='table' aria-label="custom pagination table">
                    <thead>
                        <tr className='table-headers row justify-content-center text-center align-items-center'>
                            <th className='col-2'>No</th>
                            <th className='col-4'>Id</th>
                            <th className='col-2'>Name</th>
                            <th className='col-4'>#</th>
                        </tr>
                    </thead>
                    <tbody>
                        {(rowsPerPage > 0
                            ? roles.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                            : roles
                        ).map((role, index) => (
                            <tr key={roles.id} className='row justify-content-center text-center'>
                                <th className='col-2'>{page * rowsPerPage + index + 1}</th>
                                <td className='col-4'>{role.id}</td>
                                <td className='col-2'>{role.name}</td>
                                <td className='col-4 d-flex gap-2 justify-content-center'>
                                    {/* <div>
                                        <a href={`/admin/editproduct/${row.id}`} style={{ borderRadius: "3px" }} className='btn btn-warning btn-sm'><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faPenToSquare} /></a>
                                    </div> */}
                                    <DeleteRoleModalComponent id={role.id} onModalClose={handleModalClose} />
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
                                rowsPerPageOptions={[5, 10, 25, { label: 'All', value: -1 }]}
                                colSpan={5}
                                count={roles.length}
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
        </div >
    )
}

export default RoleListTable