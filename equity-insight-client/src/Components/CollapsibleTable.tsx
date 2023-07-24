import React, { useState, useEffect } from 'react';
import { FinancialData } from './Stock';
import { TableColumn } from './StockFinancialTables'

type SortDirection = 'asc' | 'desc';

type SortConfig = {
    key: keyof FinancialData;
    direction: SortDirection;
}

interface TableProps {
    data: FinancialData[];
    columns: TableColumn[];
    title: string;
    notOpen: boolean;
}

export default function CollapsibleTable({ data, columns, title, notOpen }: TableProps) {
   
    const [isCollapsed, setIsCollapsed] = useState(notOpen);
    const [sortedData, setSortedData] = useState<FinancialData[]>(data);
    const [sortField, setSortField] = useState<string | null>(null);
    const [sortConfig, setSortConfig] = useState<SortConfig | null>(null);

    useEffect(() => {
        if (sortConfig !== null) {
            const sorted = [...data].sort((a, b) => {
                if (a[sortConfig.key] < b[sortConfig.key]) {
                    return sortConfig.direction === 'asc' ? -1 : 1;
                }
                if (a[sortConfig.key] > b[sortConfig.key]) {
                    return sortConfig.direction === 'asc' ? 1 : -1;
                }
                return 0;
            });
            setSortedData(sorted);
        }
    }, [sortConfig, data]);

    const handleHeaderClick = (property: keyof FinancialData) => {
        let direction: SortDirection = 'desc';
        if (sortConfig && sortConfig.key === property && sortConfig.direction === 'desc') {
            direction = 'asc';
        }
        setSortConfig({ key: property, direction });
    };

    return (
        <>
            <h2 className="collapsible-header" onClick={() => setIsCollapsed(!isCollapsed)}>
                <span className="collapsible-title">{title}</span>
                <span className={`arrow ${isCollapsed ? "collapsed" : ""}`}>▲</span>
            </h2>
            <div className={`collapsible-table ${isCollapsed ? "collapsed" : ""}`} >

                {!isCollapsed && (
                    <table className="table table-bordered table-striped">
                        <thead>
                            <tr>
                                {columns.map((column, index) => (
                                    <th key={index} onClick={() => handleHeaderClick(column.property)}>
                                        <div className="header-container">
                                            {column.header}
                                            {sortConfig && sortConfig.key === column.property && (
                                                <span className="column-arrow">{sortConfig.direction === 'asc' ? '▲' : '▼'}</span>
                                            )}
                                        </div>
                                    </th>
                                ))}
                            </tr>
                        </thead>
                        <tbody>
                            {sortedData.map((item, index) => (
                                <tr key={index}>
                                    {columns.map((column, index) => (
                                        <td key={index}>{column.accessor(item)}</td>
                                    ))}
                                </tr>
                            ))}
                        </tbody>
                    </table>
                )}
            </div>
        </>
        
    );
};
