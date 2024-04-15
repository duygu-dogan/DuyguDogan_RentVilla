import axios from 'axios';
import React, { useEffect, useState } from 'react'
import RoleTable from '../../helpers/RoleTable';

const RoleAssignment = () => {
    const [items, setItems] = useState([]);
    const [pagination, setPagination] = useState(
        { Page: 0, Size: 10 }
    );
    const handlePagination = (rowSize, pageSize) => {
        setPagination(
            { Page: pageSize, Size: rowSize }
        );
    }
    useEffect(() => {
        axios.get(`http://localhost:5006/api/authConfigs/GetAuthorizeDefinitionEndpoints`)
            .then((res) => {
                const newItems = res.data.map(item => ({
                    Name: item.name,
                    Actions: item.actions
                }));
                setItems(newItems);
            })
            .catch((err) => {
                console.log(err)
            })
    }, []);

    return (
        <div className='container d-flex flex-column mt-3'>
            <div class="accordion mx-5" id="menu-accordion">
                <h4 className='container-fluid mb-3'>Role Assignment</h4>
                {items.map(menu =>
                    <div class="accordion-item">
                        <h2 class="accordion-header" id={`title-menu-${menu.Name}`}>
                            <button class="accordion-button collapsed" type="button" data-bs-toggle="collapse" data-bs-target={`#menu-details-${menu.Name}`} aria-expanded="false" aria-controls={`menu-details-${menu.Name}`}>
                                <div class="row p-2">
                                    <h6 class="fw-bold">{menu.Name}</h6>
                                </div>
                            </button>
                        </h2>
                        <div id={`menu-details-${menu.Name}`} class="accordion-collapse collapse" aria-labelledby={`title-menu-${menu.Name}`} data-bs-parent="#menu-accordion">
                            <RoleTable rows={menu.Actions} menu={menu.Name} onPagination={handlePagination} />
                        </div>
                    </div>
                )}
            </div>
        </div>
    )
}

export default RoleAssignment