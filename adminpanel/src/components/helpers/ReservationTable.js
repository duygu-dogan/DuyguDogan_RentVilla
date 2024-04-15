import { TablePagination } from '@mui/base';
import { ChevronLeftRounded, ChevronRightRounded, FirstPageRounded, LastPageRounded } from '@mui/icons-material';
import React, { useEffect, useState } from 'react'
import { ToastContainer, toast } from 'react-toastify';
import CancelReservationModal from '../modals/Reservations/CancelReservationModal';
import axios from 'axios';

const ReservationTable = ({ rows, onPagination }) => {
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [switchState, setSwitchState] = useState(rows.map(row => row.status));

    useEffect(() => {
        setSwitchState(rows.map(row => row.status));
    }, [rows]);

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
    const handleSwitchChange = (index, row, newStatus) => {
        const updatedSwitchState = [...switchState];
        updatedSwitchState[index] = newStatus;
        setSwitchState(updatedSwitchState);

        axios.put(`http://localhost:5006/api/reservations/updatereservationstatus`, { ReservationId: row.id, Status: newStatus })
            .then((response) => {
                console.log('Reservation status updated successfully');
                toast('Reservation status updated successfully', { type: 'success' });
                window.location.reload();
            }).catch((error) => {
                console.error('Failed to update reservation status');
                toast('Failed to update reservation status', { type: 'error' });
                window.location.reload();
            });
    }
    const handleModalClose = (message, type) => {
        toast(message, { type: type });
        window.location.reload();
    }
    return (
        <div className='container-fluid'>
            <ToastContainer
                position='bottom-right'
            />
            <div className='paginated-table col-md-11 mt-4 mx-auto'  >
                <table className='table' aria-label="custom pagination table">
                    <thead>
                        <tr className='table-headers row justify-content-center text-center align-items-center'>
                            <th className='col-1'>No</th>
                            <th className='col-2'>Name</th>
                            <th className='col-1'>UserName</th>
                            <th className='col-2'>Total Cost</th>
                            <th className='col-2'>Rent Period</th>
                            <th className='col-1'>IsPaid</th>
                            <th className='col-2'>Status</th>
                            <th className='col-1'>#</th>
                        </tr>
                    </thead>
                    <tbody>
                        {(rowsPerPage > 0
                            ? rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                            : rows
                        ).map((row, index) => (
                            <tr key={row.name} className='row justify-content-center text-center'>
                                <th className='col-1'>{page * rowsPerPage + index + 1}</th>
                                <td className='col-2'>{row.name}</td>
                                <td className='col-1'>{row.user}</td>
                                <td className='col-2'>
                                    {row.totalCost}
                                </td>
                                <td className='col-2'>
                                    {new Date(row.startDate).toLocaleDateString()} - {new Date(row.endDate).toLocaleDateString()}
                                </td>
                                <td className='col-1'>
                                    <div className="form-check form-switch d-flex justify-content-center">
                                        <input
                                            className="form-check-input"
                                            type="checkbox"
                                            id={`flexSwitchCheckDefault${row.id}`}
                                            checked={row.isPaid}
                                            readOnly={true}
                                        />
                                    </div>
                                </td>
                                <td className='col-2'>
                                    <div className="mx-2">
                                        <select
                                            className="form-select text-center "
                                            id={`reservationStatus${row.id}`}
                                            value={row.status === 'Open' ? 'open' : (row.status === 'Completed' ? 'completed' : 'Canceled')}
                                            onChange={(event) => handleSwitchChange(index, row, event.target.value)}
                                        >
                                            <option value="open">Open</option>
                                            <option value="completed">Completed</option>
                                            <option value="canceled">Canceled</option>
                                        </select>
                                    </div>
                                </td>
                                <td className='col-1 d-flex gap-2 justify-content-center'>
                                    <CancelReservationModal id={row.id} onModalClose={handleModalClose} />
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

export default ReservationTable