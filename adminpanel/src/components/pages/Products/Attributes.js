import axios from 'axios';
import React, { useEffect, useState } from 'react'
import AttributeTable from '../../helpers/AttributeTable';
import NewAttribute from './NewAttribute';
import DynamicForm from '../../helpers/DynamicForm';

const Attributes = () => {
    const [items, setItems] = useState([
        { name: '', description: '', isactive: true }
    ]);
    useEffect(() => {
        axios.get('http://localhost:5006/api/attributes/get')
            .then((res) => {
                const newItems = res.data.map(item => ({
                    name: item.name,
                    description: item.description,
                    isactive: item.isactive
                }));
                setItems(newItems);
                console.log(newItems)
            })
            .catch((err) => {
                console.log(err)
            })
    }, []);
    const handleFormChange = (newInputs) => {
        setItems(newInputs);
        axios.post('http://localhost:5006/api/Attributes/Add', { newInputs })
            .then(response => {
                console.log(response);
            })
            .catch(error => {
                console.error(error);
            });
    };

    return (
        <div className='container d-flex flex-column gap-3 mt-3'>
            {/* <button onClick={}>Add Attribute</button> */}
            <div className='col-md-11'>
                <NewAttribute onFormChange={handleFormChange} />
            </div>
            {/* <DynamicForm initialInputs={items} onFormChange={handleFormChange} /> */}

            <div>
                <AttributeTable rows={items} />
            </div>
        </div>
    )
}

export default Attributes