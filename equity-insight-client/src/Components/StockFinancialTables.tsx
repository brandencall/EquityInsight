import React from 'react';
import CollapsibleTable from './CollapsibleTable';
import { createTableColumns } from '../FinancialProperties'
import { FinancialTableProps } from '../InterfacesAndTypes';
import { formatter } from '../CurrencyFormatter';


export function ProfitabilityTable({ financialDataList }: FinancialTableProps) {

    const columns = createTableColumns([
        'endDate',
        'earningsPerShareBasic',
        'earningsPerShareDiluted',
        'grossProfit',
        'netIncomeLoss',
        'revenues',
        'salesRevenueNet'
    ])

    return (
        <CollapsibleTable
            data={financialDataList}
            columns={columns}
            title="Profitability Metrics"
            notOpen={false}
        />
    )
}

export function LiquidityTable({ financialDataList }: FinancialTableProps) {


    const columns = createTableColumns([
        'endDate',
        'cash',
        'cashAndCashEquivalentsAtCarryingValue',
        'assets',
        'liabilities',
        'shortTermInvestments',
    ])

    return (
        <CollapsibleTable
            data={financialDataList}
            columns={columns}
            title="Liquidity Metrics"
            notOpen={true}
        />
    )
}

export function SolvencyTable({ financialDataList }: FinancialTableProps) {

  
    const columns = createTableColumns([
        'endDate',
        'assets',
        'noncurrentAssets',
        'liabilities',
        'longTermDebt',
        'longTermDebtNoncurrent',
        'stockHoldersEquity',
    ])

    return (
        <CollapsibleTable
            data={financialDataList}
            columns={columns}
            title="Solvency Metrics"
            notOpen={true}
        />
    )
}

export function EfficiencyTable({ financialDataList }: FinancialTableProps) {

 
    const columns = createTableColumns([
        'endDate',
        'salesRevenueNet',
        'operatingIncomeLoss',
        'assets',
        'stockHoldersEquity',
    ])

    return (
        <CollapsibleTable
            data={financialDataList}
            columns={columns}
            title="Efficiency Metrics"
            notOpen={true}
        />
    )
}

export function RecentFinancesTable({ financialDataList }: FinancialTableProps) {

    if (financialDataList.length === 0) return null; // handle empty list

    const mostRecentData = financialDataList[0];

    const columns = createTableColumns([
        'earningsPerShareBasic',
        'grossProfit',
        'netIncomeLoss',
        'assets',
        'liabilities',
    ])

    return (
        <table>
            <tbody>
                {columns.map((column, index) => (
                    <tr key={index} className="recentFinancesRow">
                        <td className="recentFinancesProperty">{column.header}</td>
                        <td className="recentFinancesValue">{
                            typeof column.accessor(mostRecentData) === "number" && column.property !== "earningsPerShareBasic"
                                ? formatter.format(column.accessor(mostRecentData))
                                : column.accessor(mostRecentData)
                        }</td>
                    </tr>
                ))}
            </tbody>
        </table>
    )
}