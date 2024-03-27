import { faList, faPenToSquare, faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { TablePagination } from '@mui/base';
import { ChevronLeftRounded, ChevronRightRounded, FirstPageRounded, LastPageRounded } from '@mui/icons-material';
import React, { useState } from 'react'

const TableComponent = ({ rows }) => {
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(5);
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
    const handleSwitchChange = (row) => {
        const updatedRow = { ...row, isactive: !row.isactive }
        setSwitchState(updatedRow.isactive);
    }

    return (
        <div className='container-fluid'>
            <div className='paginated-table col-md-11'  >
                <table className='table' aria-label="custom pagination table">
                    <thead>
                        <tr className='table-headers row justify-content-center text-center align-items-center'>
                            <th className='col-1'>No</th>
                            <th className='col-2'>Name</th>
                            <th className='col-2'>Price</th>
                            <th className='col-2'>Region</th>
                            <th className='col-2'>IsActive</th>
                            <th className='col-3'>#</th>
                        </tr>
                    </thead>
                    <tbody>
                        {(rowsPerPage > 0
                            ? rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                            : rows
                        ).map((row, index) => (
                            <tr key={row.name} className='row justify-content-center text-center'>
                                <th className='col-1'>{index + 1}</th>
                                <td className='col-2'>{row.name}</td>
                                <td className='col-2'>
                                    {row.price}
                                </td>
                                <td className='col-2'>
                                    {row.region}
                                </td>
                                <td className='col-2'>
                                    <div className="form-check form-switch d-flex justify-content-center">
                                        <input
                                            className="form-check-input"
                                            type="checkbox"
                                            id="flexSwitchCheckDefault"
                                            // checked={row.isactive}
                                            checked={switchState}
                                            onChange={() => handleSwitchChange(row)}
                                        />
                                    </div>
                                </td>
                                <td className='col-3 d-flex gap-2 justify-content-center'>
                                    <button style={{ borderRadius: "3px" }} className='btn btn-warning btn-sm'><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faPenToSquare} /></button>
                                    <button style={{ borderRadius: "3px" }} className='btn btn-danger btn-sm'><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} /></button>
                                    <button style={{ borderRadius: "3px" }} className='btn btn-primary btn-sm'> <FontAwesomeIcon style={{ fontSize: "15px" }} icon={faList} /></button>
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
        </div>
    )
}


export default TableComponent