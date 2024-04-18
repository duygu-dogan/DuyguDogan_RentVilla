import { faTrashCan } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios'
import React from 'react'
import Cookies from 'js-cookie';

const DeleteAttributeTypeModel = ({ id, onModalClose }) => {
    const modalId = `deleteModal${id}`;
    const accessToken = Cookies.get('RentVilla.Cookie_AT')
    axios.defaults.headers.common['Authorization'] = `Bearer ${accessToken}`;

    const handleSoftDelete = (e) => {
        e.preventDefault();
        console.log(id);
        axios.put(`http://localhost:5006/api/attributes/softdeletetype?id=${id}`)
            .then(response => {
                console.log(response);
                onModalClose();
            })
            .catch(error => {
                console.error(error);
            });
    }
    const handleDelete = (e) => {
        e.preventDefault();
        console.log(id);
        axios.delete(`http://localhost:5006/api/attributes/deletetype?id=${id}`)
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
            <form>
                <div class="modal fade" id={`${modalId}`} tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                If you select "Recycle Bin", the attribute type will be moved to the recycle bin. If you select "Delete", the attribute type will be deleted permanently. If you delete this attribute type, all attributes under this type will be deleted. You can't recover them.
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Cancel</button>
                                <button onClick={handleSoftDelete} class="btn btn-warning btn-sm" data-bs-dismiss="modal">Recycle Bin</button>
                                <button onClick={handleDelete} class="btn btn-danger btn-sm" data-bs-dismiss="modal">Delete</button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    )
}

export default DeleteAttributeTypeModel