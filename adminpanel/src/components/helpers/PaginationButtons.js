import { CButton, CButtonGroup, CDropdown, CDropdownItem, CDropdownMenu, CDropdownToggle } from '@coreui/react'
import React, { useState } from 'react'

const PaginationButtons = () => {

    const [selectedNumber, setSelectedNumber] = useState(1); // Default value

    const handleItemClick = (number) => {
        setSelectedNumber(number);
    };

    return (
        <div>
            <CButtonGroup role="group" aria-label="Button group with nested dropdown">
                <CDropdown variant="btn-group">
                    <CDropdownToggle color="primary">{selectedNumber}</CDropdownToggle>
                    <CDropdownMenu>
                        <CDropdownItem onClick={() => handleItemClick(1)}>1</CDropdownItem>
                        <CDropdownItem onClick={() => handleItemClick(2)}>2</CDropdownItem>
                        <CDropdownItem onClick={() => handleItemClick(50)}>50</CDropdownItem>
                        <CDropdownItem onClick={() => handleItemClick(100)}>100</CDropdownItem>
                    </CDropdownMenu>
                </CDropdown>
            </CButtonGroup>
        </div>

    );
}

export default PaginationButtons