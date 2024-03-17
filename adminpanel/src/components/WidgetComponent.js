import { cilArrowTop, cilOptions } from '@coreui/icons'
import CIcon from '@coreui/icons-react'
import { CDropdown, CDropdownItem, CDropdownMenu, CDropdownToggle, CWidgetStatsA } from '@coreui/react'
import { CChartLine } from '@coreui/react-chartjs'
import React from 'react'

const WidgetComponent = ({ color, title }) => {
    return (
        <>
            <CWidgetStatsA
                className="mb-4"
                color={color}
                value={
                    <>
                        $9.000{' '}
                        <span className="fs-6 fw-normal">
                            (40.9% <CIcon icon={cilArrowTop} />)
                        </span>
                    </>
                }
                title={title}
                action={
                    <CDropdown alignment="end">
                        <CDropdownToggle color="transparent" caret={false} className="p-0">
                            <CIcon icon={cilOptions} className="text-white" />
                        </CDropdownToggle>
                        <CDropdownMenu>
                            <CDropdownItem>Action</CDropdownItem>
                            <CDropdownItem>Another action</CDropdownItem>
                            <CDropdownItem>Something else here...</CDropdownItem>
                            <CDropdownItem disabled>Disabled action</CDropdownItem>
                        </CDropdownMenu>
                    </CDropdown>
                }
                chart={
                    <CChartLine
                        className="mt-3 mx-3"
                        style={{ height: '70px' }}
                        data={{
                            labels: ['January', 'February', 'March', 'April', 'May', 'June', 'July'],
                            datasets: [
                                {
                                    label: 'My First dataset',
                                    backgroundColor: 'transparent',
                                    borderColor: 'rgba(255,255,255,.55)',
                                    pointBackgroundColor: '#5856d6',
                                    data: [65, 59, 84, 84, 51, 55, 40],
                                },
                            ],
                        }}
                        options={{
                            plugins: {
                                legend: {
                                    display: false,
                                },
                            },
                            maintainAspectRatio: false,
                            scales: {
                                x: {
                                    border: {
                                        display: false,
                                    },
                                    grid: {
                                        display: false,
                                        drawBorder: false,
                                    },
                                    ticks: {
                                        display: false,
                                    },
                                },
                                y: {
                                    min: 30,
                                    max: 89,
                                    display: false,
                                    grid: {
                                        display: false,
                                    },
                                    ticks: {
                                        display: false,
                                    },
                                },
                            },
                            elements: {
                                line: {
                                    borderWidth: 1,
                                    tension: 0.4,
                                },
                                point: {
                                    radius: 4,
                                    hitRadius: 10,
                                    hoverRadius: 4,
                                },
                            },
                        }}
                    />
                }
            />
        </>
    )
}

export default WidgetComponent