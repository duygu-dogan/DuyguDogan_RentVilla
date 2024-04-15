import { faCancel, faTrashCan } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios'
import React from 'react'

const CancelReservationModal = ({ id, onModalClose }) => {
    const modalId = `deleteModal${id}`;

    const handleCancel = (e) => {
        console.log(id)
        e.preventDefault();
        axios.delete(`http://localhost:5006/api/reservations/CancelReservation?ReservationId=${id}`)
            .then(response => {
                console.log(response);
                onModalClose("Reservation canceled successfully.", "success");
            })
            .catch(error => {
                console.error(error);
                onModalClose("Reservation cancellation failed.", "error");
            });
    }
    return (
        <div>
            <button type='button' style={{ borderRadius: "3px" }} className='btn btn-danger btn-sm' data-bs-toggle="modal" data-bs-target={`#${modalId}`}> <FontAwesomeIcon style={{ fontSize: "15px" }} icon={faCancel} /> </button>
            <form >
                <div class="modal fade" id={modalId} tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                Are you sure you want to cancel this reservation? If you cancel, you can't recover it.
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Quit</button>
                                <button type="button" onClick={handleCancel} class="btn btn-danger btn-sm" data-bs-dismiss="modal">Cancel</button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    )
}

export default CancelReservationModal