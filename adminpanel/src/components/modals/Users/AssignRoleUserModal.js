import { faKey, faUser } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import 'react-toastify/dist/ReactToastify.css';
import axios from 'axios'
import React, { useEffect, useState } from 'react'
import { MultiSelect } from 'react-multi-select-component';

const AssignRoleUserModal = ({ userId, onModalClose }) => {
    const modalId = `assignModal${userId}`;
    const [roles, setRoles] = useState([]);
    const [selectedRoles, setSelectedRoles] = useState([]);

    useEffect(() => {
        axios.get(`http://localhost:5006/api/roles/getroles`)
            .then((res) => {
                const entries = Object.entries(res.data);
                const newRoles = entries.map(([key, value]) => ({
                    label: value,
                    value: key
                }));
                setRoles(newRoles);

            })
            .catch((err) => {
                console.log(err)
            })
    }, []);
    const handleSelectedRoles = () => {
        setSelectedRoles([]);
        axios.get(`http://localhost:5006/api/Users/GetUserRoles?userId=${userId}`)
            .then((res) => {
                const value = res.data.map(role => role.id);
                console.log(value);
                setSelectedRoles(roles.filter(role => value.includes(role.value)))
            })
            .catch((err) => {
                console.log(err)
            })
    };
    const handleAssignment = (e) => {
        e.preventDefault();
        axios.post(`http://localhost:5006/api/users/AssignRoleToUser`, { userId: userId, roleIds: selectedRoles.map(role => role.value) })
            .then(response => {
                console.log(response);
                onModalClose("Role assigned successfully.", "success");
            })
            .catch(error => {
                console.error(error);
                onModalClose("Role assignment failed.", "error");
            });
    }
    return (
        <div>
            <button type='button' onClick={handleSelectedRoles} style={{ borderRadius: "3px" }} className='btn btn-success btn-sm' data-bs-toggle="modal" data-bs-target={`#${modalId}`}><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faKey} /></button>
            <form onSubmit={handleAssignment}>
                <div class="modal fade" id={modalId} tabindex="-1" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog">
                        <div class="modal-content">
                            <div class="modal-header">
                                <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                            </div>
                            <div class="modal-body">
                                You can assign roles to the selected endpoint on this page.
                                <div className="mx-5 my-3">
                                    <MultiSelect
                                        onChange={setSelectedRoles}
                                        value={selectedRoles}
                                        options={roles}
                                        labelledBy='Select'
                                    />
                                </div>
                            </div>
                            <div class="modal-footer">
                                <button type="button" class="btn btn-secondary btn-sm" data-bs-dismiss="modal">Cancel</button>
                                <button type='submit' class="btn btn-danger btn-sm" data-bs-dismiss="modal">Assign Roles</button>
                            </div>
                        </div>
                    </div>
                </div>
            </form>
        </div>
    )
}

export default AssignRoleUserModal