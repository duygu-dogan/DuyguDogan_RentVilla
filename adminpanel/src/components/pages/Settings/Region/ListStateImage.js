import axios from 'axios';
import React, { useCallback, useEffect, useState } from 'react';
import 'react-toastify/dist/ReactToastify.css';
import { ToastContainer, toast } from 'react-toastify';
import StateTableComponent from '../../../helpers/StateTableComponent';

const ListStateImage = () => {
    const [items, setItems] = useState([]);
    const fetchItems = useCallback(() => {
        axios.get('http://localhost:5006/api/region/getallstates')
            .then((res) => {
                const newItems = res.data.map(item => ({
                    id: item.id,
                    name: item.name,
                    stateimagefile: item.images[0]
                }));
                setItems(newItems);
                console.log(newItems)
            })
            .catch((err) => {
                console.log(err);
                toast('An error occurred while fetching states.', { type: 'error' });
            });
    }, []);
    useEffect(() => {
        fetchItems();
    }, [fetchItems]);

    return (
        <div className='container d-flex flex-column gap-3 mt-3'>
            <ToastContainer
            />

            <div className='col-md-11 d-flex gap-3 px-3 mt-3 justify-content-between'>
                <div>
                    <h3>States</h3>
                </div>
            </div>
            <div>
                <StateTableComponent rows={items} entityName='State Image' uploadUrl={"http://localhost:5006/api/region/UploadStateImage/?StateId"} />

            </div>
        </div>
    )
}
export default ListStateImage