import axios from 'axios';
import React, { useEffect } from 'react';

const ApiComponent = ({ name, description, isactive }) => {
    useEffect(() => {
        if (name && description && isactive !== undefined) {
            axios.post('http://localhost:5006/api/Attributes/Add', { name, description, isactive })
                .then(response => {
                    console.log(response);
                })
                .catch(error => {
                    console.error(error);
                });
        }
    }, [name, description, isactive]);

    return null;
};

export default ApiComponent;