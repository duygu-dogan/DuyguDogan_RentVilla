import { TablePagination } from '@mui/base';
import { ChevronLeftRounded, ChevronRightRounded, FirstPageRounded, LastPageRounded } from '@mui/icons-material';
import React, { useState } from 'react'
import { ToastContainer, toast } from 'react-toastify';
import AssignRoleModelComponent from '../modals/Roles/AssignRoleModelComponent';

const RoleTable = ({ rows, menu, onPagination }) => {
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);

    const emptyRows =
        page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
        onPagination(newPage, rowsPerPage);
    };

    const handleChangeRowsPerPage = (event) => {
        const newRowsPerPage = parseInt(event.target.value, 10);
        setRowsPerPage(newRowsPerPage);
        onPagination(page, newRowsPerPage);
        setPage(0);
    };

    const handleModalClose = (message, type) => {
        toast(message, { type: type });
    }
    return (
        <div className='container-fluid'>
            <ToastContainer
                position='bottom-right'
            />
            <div className='paginated-table col-md-11 mt-4 mx-auto card-body'  >
                <table className='table' aria-label="custom pagination table">
                    <thead>
                        <tr className='table-headers row justify-content-center text-center align-items-center'>
                            <th className='col-1'>No</th>
                            <th className='col-2'>Action Type</th>
                            <th className='col-1'>Http Type</th>
                            <th className='col-3'>Definition</th>
                            <th className='col-3'>Role Code</th>
                            <th className='col-2'>#</th>
                        </tr>
                    </thead>
                    <tbody>
                        {(rowsPerPage > 0
                            ? rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                            : rows
                        ).map((row, index) => (
                            <tr key={index} className='row justify-content-center text-center'>
                                <th className='col-1'>{page * rowsPerPage + index + 1}</th>
                                <td className='col-2'>{row.actionType}</td>
                                <td className='col-1'>{row.httpType}</td>
                                <td className='col-3'>{row.definition}</td>
                                <td className='col-3'>{row.code}</td>
                                <td className='col-2 d-flex gap-2 justify-content-center'>
                                    <AssignRoleModelComponent menu={menu} action={row} onModalClose={handleModalClose} />
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

export default RoleTable