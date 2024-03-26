import axios from 'axios';
import React, { useEffect, useState } from 'react'
import AttributeTable from '../../helpers/AttributeTable';
import NewAttributeTypeModal from '../../modals/NewAttributeTypeModal';
import { FontAwesomeIcon } from '@fortawesome/react-fontawesome';
import { faTrashCan } from '@fortawesome/free-solid-svg-icons';

const Attributes = () => {
    const [items, setItems] = useState([]);
    useEffect(() => {
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
                console.log(err)
            })
    }, []);

    return (
        <div className='container d-flex flex-column gap-3 mt-3'>
            <div className='col-md-11 d-flex gap-3 mt-3 justify-content-end'>
                <div>
                    <NewAttributeTypeModal />
                </div>
                <div>
                    <a href='/getdeletedattributetypes' style={{ borderRadius: '3px' }} className='btn btn-danger btn-sm fs-6'><FontAwesomeIcon style={{ fontSize: "15px" }} icon={faTrashCan} />  Recycle Bin</a>
                </div>
            </div>
            <div>
                <AttributeTable rows={items} />
            </div>
        </div>
    )
}

export default Attributes