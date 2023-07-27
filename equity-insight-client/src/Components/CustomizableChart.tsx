import React, { useEffect, useRef, useState } from 'react';
import { Line, Bar } from 'react-chartjs-2';
import {
    Chart as ChartJS,
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
    PointElement,
    LineElement
} from 'chart.js';
import { createTableColumns } from '../FinancialProperties';
import { FinancialTableProps, TableColumn, FinancialData } from '../InterfacesAndTypes'; 

ChartJS.register(
    CategoryScale,
    LinearScale,
    BarElement,
    Title,
    Tooltip,
    Legend,
    PointElement,
    LineElement
);

export function CustomizableChart({ financialDataList }: FinancialTableProps) {

    const columns = createTableColumns([
        'grossProfit',
        'netIncomeLoss',
        'revenues',
        'cashAndCashEquivalentsAtCarryingValue',
        'assets',
        'liabilities',
        'longTermDebt',
        'operatingIncomeLoss',
    ])

    const [selectedColumn, setSelectedColumn] = useState<TableColumn>(columns[0]);
    const [chartType, setChartType] = useState('bar');


    const chartData = {
        labels: [...financialDataList.map(datum => datum.endDate)].reverse(),  //assuming endDate represents the period for the profit
        datasets: [
            {
                label: selectedColumn.header,
                data: [...financialDataList.map((datum: FinancialData) => selectedColumn.accessor(datum))].reverse(),
                backgroundColor: 'rgba(75,192,192,0.4)',
                borderColor: 'rgba(75,192,192,1)',
                borderWidth: 1,
            }
        ]
    };

    const options = {
        responsive: true,
        plugins: {
            legend: {
                position: 'top' as const,
            },
            
        },
    };

    const handlePropertyChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        const column = columns.find(column => column.property === event.target.value);
        if (column) setSelectedColumn(column);
    };

    const handleTypeChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setChartType(event.target.value);
    };


    return (
        <div>
            <form className="form-inline">
                <label>
                    Property:
                    <select className="form-control ml-2" value={selectedColumn.property} onChange={handlePropertyChange}>
                        {columns.map(column => (
                            <option value={column.property} key={column.property}>{column.header}</option>
                        ))}
                    </select>
                </label>
                <label style={{'marginLeft': '10px'}}>
                    Chart Type:
                    <select className="form-control ml-2" value={chartType} onChange={handleTypeChange}>
                        <option value="line">Line</option>
                        <option value="bar">Bar</option>
                    </select>
                </label>
            </form>
           
            {chartType === 'bar' && <Bar data={chartData} options={options} />}
            {chartType === 'line' && <Line data={chartData} options={options} />}
        </div>
        
    );
}