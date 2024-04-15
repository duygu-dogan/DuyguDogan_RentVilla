import { faTrashCan } from '@fortawesome/free-solid-svg-icons';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import axios from 'axios';
import React from 'react'

const DeleteRoleModalComponent = (id, onModalClose) => {
    const modalId = `deleteModal${id.id}`;

    const handleDelete = (e) => {
        e.preventDefault();
        axios.delete(`http://localhost:5006/api/roles/deleterole/${id.id}`)
            .then(response => {
                console.log(response);
                id.onModalClose("Role deleted successfully.", "success");
            })
            .catch(error => {
                console.error(error);
                id.onModalClose("Role deletion failed.", "error");
            });
    }
    return (
        <div>
            <button type='button' style={{ borderRadius: "3px" }} className='btn btn-danger btn-sm' data-bs-toggle="modal" data-bs-target={`#${modalId}`}> <FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} /> </button>
            <form >
                <div class="modal fade" id={modalId} tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                If you delete this role, you can't recover it.
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Cancel</button>
                                <button onClick={handleDelete} class="btn btn-danger btn-sm" data-bs-dismiss="modal">Delete</button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    )
}

export default DeleteRoleModalComponent