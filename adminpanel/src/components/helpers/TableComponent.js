import { faPenToSquare, faTrashCan } from '@fortawesome/free-solid-svg-icons';
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
                        <tr className='table-headers'>
                            <th>Name</th>
                            <th>Price</th>
                            <th>Region</th>
                            <th>IsActive</th>
                            <th>#</th>
                        </tr>
                    </thead>
                    <tbody>
                        {(rowsPerPage > 0
                            ? rows.slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                            : rows
                        ).map((row) => (
                            <tr key={row.name}>
                                <td style={{ width: 160 }}>{row.name}</td>
                                <td style={{ width: 160 }} align="right">
                                    {row.price}
                                </td>
                                <td style={{ width: 160 }} align="right">
                                    {row.region}
                                </td>
                                <td style={{ width: 160 }} >
                                    <div className="form-check form-switch">
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
                                <td style={{ width: 160 }} align="right">
                                    <button style={{ borderRadius: "3px" }} className='btn btn-warning btn-sm'><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faPenToSquare} /></button>
                                    <button style={{ borderRadius: "3px" }} className='btn btn-danger btn-sm'><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} /></button>
                                    <button style={{ borderRadius: "3px" }} className='btn btn-primary btn-sm w-75'>Details</button>
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