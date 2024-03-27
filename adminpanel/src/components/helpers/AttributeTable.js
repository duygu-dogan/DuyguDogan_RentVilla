import { faPenToSquare } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { TablePagination } from '@mui/base';
import 'react-toastify/dist/ReactToastify.css';
import { ChevronLeftRounded, ChevronRightRounded, FirstPageRounded, LastPageRounded } from '@mui/icons-material';
import React, { useCallback, useState } from 'react';
import DeleteAttributeModal from '../modals/Attributes/DeleteAttributeModal';
import { ToastContainer, toast } from 'react-toastify';

const AttributeTable = ({ rows }) => {
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(10);
    const [switchState, setSwitchState] = useState(true);

    const emptyRows =
        page > 0 ? Math.max(0, (1 + page) * rowsPerPage - rows.length) : 0;

    const handleChangePage = (event, newPage) => {
        setPage(newPage);
    };

    const handleChangeRowsPerPage = (event) => {
        setRowsPerPage(parseInt(event.target.value, 10));
        setPage(0);
    };
    const handleSwitchChange = (changedSwitchState) => {
        setSwitchState(!changedSwitchState);
    }
    const fetchItems = useCallback(() => {
        window.location.reload();
    }, []);

    const handleModalClose = () => {
        fetchItems();
        toast('Operation successful!', { type: 'success' });
    }
    return (
        <div className='container-fluid'>
            <ToastContainer
                position='bottom-right'
                autoClose={3000}
                hideProgressBar={false}
                closeOnClick={true}
                pauseOnFocusLoss
                draggable
                pauseOnHover
                theme='light'
                transition='Bounce'
            />
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
                            <th className='col-4'>ID</th>
                            <th className='col-2'>TYPE</th>
                            <th className='col-2'>DESCRIPTION</th>
                            <th className='col-1'>ISACTIVE</th>
                            <th className='col-2'>#</th>
                        </tr>
                    </thead>

                    <tbody>
                        {(Array.isArray(rows) && rowsPerPage > 0
                            ? rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                            : rows
                        ).map((row, index) => (
                            <tr key={row.id} className='row justify-content-center text-center '>
                                <th className='col-1'>{index + 1}</th>
                                <td className='col-4'>{row.id}</td>
                                <td className='col-2'>{row.name}</td>
                                <td className='col-2'>{row.description}</td>
                                <td className='col-1'>
                                    <div className="form-check form-switch d-flex justify-content-center">
                                        <input
                                            className="form-check-input"
                                            type="checkbox"
                                            id="flexSwitchCheckDefault"
                                            checked={switchState}
                                            onChange={() => handleSwitchChange(row.isactive)}
                                        />
                                    </div></td>
                                <td className='col-2' >
                                    <div className='d-flex justify-content-center'>
                                        <div>
                                            <button style={{ borderRadius: "3px" }} className='btn btn-warning btn-sm me-2'><FontAwesomeIcon icon={faPenToSquare} /></button>
                                        </div>
                                        <div>
                                            <DeleteAttributeModal id={row.id} onModalClose={handleModalClose} />
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