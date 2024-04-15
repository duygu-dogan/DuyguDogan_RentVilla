import React, { useState } from 'react'
import DeleteUserModal from '../modals/Users/DeleteUserModal';
import { ToastContainer, toast } from 'react-toastify';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faKey, faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { TablePagination } from '@mui/base';
import { ChevronLeftRounded, ChevronRightRounded, FirstPageRounded, LastPageRounded } from '@mui/icons-material';
import AssignRoleUserModal from '../modals/Users/AssignRoleUserModal';

const UserTable = ({ rows, onPagination }) => {
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [switchState, setSwitchState] = useState(true);

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
                            <th className='col-1'>No</th>
                            <th className='col-3'>Id</th>
                            <th className='col-3'>Name</th>
                            <th className='col-1'>Username</th>
                            <th className='col-2'>Gender</th>
                            <th className='col-2'>#</th>
                        </tr>
                    </thead>
                    <tbody>
                        {(rowsPerPage > 0
                            ? rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                            : rows
                        ).map((row, index) => (
                            <tr key={row.name} className='row justify-content-center text-center'>
                                <th className='col-1'>{page * rowsPerPage + index + 1}</th>
                                <td className='col-3'>{row.id}</td>
                                <td className='col-3'>{row.firstName} {row.lastName}</td>
                                <td className='col-1'>{row.userName}</td>
                                <td className='col-2'>{row.gender}</td>
                                <td className='col-1 d-flex gap-2 justify-content-center'>
                                    <AssignRoleUserModal id={row.id} onModalClose={handleModalClose} />
                                    <DeleteUserModal id={row.id} onModalClose={handleModalClose} />
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
        </div >
    )
}

export default UserTable