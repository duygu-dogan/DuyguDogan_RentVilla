import { faTrashCan } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios'
import React from 'react'

const DeleteAttributeModal = ({ id, onModalClose }) => {
    const modalId = `deleteModal${id}`;
    const handleSubmit = (e) => {
        e.preventDefault();
        axios.delete(`http://localhost:5006/api/attributes/delete?id=${id}`)
            .then(response => {
                console.log(response);
                onModalClose();
            })
            .catch(error => {
                console.error(error);
            });
    }
    return (
        <div>
            <button type='button' style={{ borderRadius: "3px" }} className='btn btn-danger btn-sm me-2' data-bs-toggle="modal" data-bs-target={`#${modalId}`}> <FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} /> </button>
            <form onSubmit={(e) => handleSubmit(e)}>
                <div class="modal fade" id={modalId} tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                Are you sure you want to delete this attribute?
                                If you delete this attribute, you can't recover it.
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Cancel</button>
                                <button type="submit" class="btn btn-danger btn-sm" data-bs-dismiss="modal">Delete</button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    )
}

export default DeleteAttributeModal