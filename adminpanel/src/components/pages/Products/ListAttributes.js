import axios from 'axios';
import React, { useCallback, useEffect, useState } from 'react'
import 'react-toastify/dist/ReactToastify.css';
import AttributeTable from '../../helpers/AttributeTable';
import NewAttributeModal from '../../modals/Attributes/NewAttributeModal';
import { useParams } from 'react-router-dom';
import { ToastContainer, toast } from 'react-toastify';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faArrowCircleLeft } from '@fortawesome/free-solid-svg-icons';

const ListAttributes = () => {
    const [items, setItems] = useState([]);
    const { id } = useParams();
    const fetchItems = useCallback(() => {
        axios.get('http://localhost:5006/api/attributes/getbytypeid/typeid?typeid=' + id)
            .then((res) => {
                const newItems = res.data.map(item => ({
                    id: item.id,
                    name: item.name,
                    description: item.description,
                    isActive: item.isActive
                }));
                setItems(newItems);
            })
            .catch((err) => {
                console.log(err);
                toast.error('An error occurred while fetching attributes.');
            });
    }, [id]);

    useEffect(() => {
        fetchItems();
    }, [fetchItems]);

    const handleModalClose = () => {
        fetchItems();
        toast('Attribute added successfully.', { type: 'success' });
    }

    return (
        <div className='container d-flex flex-column gap-3 mt-3'>
            <ToastContainer
            />
            <div className='col-md-11 d-flex mt-3 px-3 justify-content-between'>
                <h3>Attributes</h3>

                <div className='d-flex align-items-end gap-2'>
                    <NewAttributeModal attributeTypeId={id} onModalClose={handleModalClose} />
                    <a href='/admin/attributetypes' className='btn btn-secondary btn-sm'><FontAwesomeIcon icon={faArrowCircleLeft} /> Geri Dön</a>
                </div>
            </div>
            <div>
                <AttributeTable rows={items} />
            </div>
        </div>
    )
}

export default ListAttributes;