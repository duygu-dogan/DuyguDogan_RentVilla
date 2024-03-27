import axios from 'axios';
import React, { useCallback, useEffect, useState } from 'react';
import 'react-toastify/dist/ReactToastify.css';
import NewAttributeTypeModal from '../../modals/Attributes/NewAttributeTypeModal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons';
import AttributeTypeTable from '../../helpers/AttributeTypeTable';
import { ToastContainer, toast } from 'react-toastify';

const ListAttributeTypes = () => {
    const [items, setItems] = useState([]);
    const fetchItems = useCallback(() => {
        axios.get('http://localhost:5006/api/attributes/gettypes')
            .then((res) => {
                const newItems = res.data.map(item => ({
                    id: item.id,
                    name: item.name
                }));
                setItems(newItems);
                console.log(newItems)
            })
            .catch((err) => {
                console.log(err);
                toast('An error occurred while fetching attributes.', { type: 'error' });
            });
    }, []);
    useEffect(() => {
        fetchItems();
    }, [fetchItems]);
    const handleModalClose = () => {
        fetchItems();
        toast('Attribute type added successfully', { type: 'success' });
        setTimeout(() => {
            window.location.reload();
        }, 500);
    }

    return (
        <div className='container d-flex flex-column gap-3 mt-3'>
            <ToastContainer
                position='bottom-right'
                autoClose={1000}
                hideProgressBar={false}
                closeOnClick={true}
                rtl={false}
                draggable
                pauseOnHover
                pauseOnFocusLoss={false}
                theme='light'
                transition='Bounce'
            />

            <div className='col-md-11 d-flex gap-3 px-3 mt-3 justify-content-between'>
                <div>
                    <h3>Attribute Types</h3>
                </div>
                <div className='d-flex gap-2 align-items-center'>
                    <div>
                        <NewAttributeTypeModal onModalClose={handleModalClose} />
                    </div>
                    <div>
                        <a href='/deletedattributetypes' style={{ borderRadius: '3px' }} className='btn btn-danger btn-sm fs-6'><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} />  Recycle Bin</a>
                    </div>
                </div>
            </div>
            <div>
                <AttributeTypeTable rows={items} />
            </div>
        </div>
    )
}

export default ListAttributeTypes;