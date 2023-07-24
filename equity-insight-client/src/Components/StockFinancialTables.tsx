import React from 'react';
import CollapsibleTable from './CollapsibleTable';
import { FinancialData } from './Stock';

interface FinancialTableProps {
    financialDataList: FinancialData[]
}

export interface TableColumn {
    header: string;
    accessor: (data: FinancialData) => any;
    property: keyof FinancialData;
}

const formatter = new Intl.NumberFormat('en-US', {
    style: 'currency',
    currency: 'USD',
    minimumFractionDigits: 0,
    maximumFractionDigits: 0,
});

export function ProfitabilityTable({ financialDataList }: FinancialTableProps) {

    const columns: TableColumn[] = [
        { header: 'Date', accessor: (data: FinancialData) => data.endDate, property: 'endDate' },
        {
            header: 'EPS Basic',
            accessor: (data: FinancialData) => data.earningsPerShareBasic === 0 ? 'N/A' : data.earningsPerShareBasic,
            property: 'earningsPerShareBasic'
        },
        {
            header: 'EPS Diluted',
            accessor: (data: FinancialData) => data.earningsPerShareDiluted === 0 ? 'N/A' : data.earningsPerShareDiluted,
            property: 'earningsPerShareDiluted'
        },
        {
            header: 'GrossProfit',
            accessor: (data: FinancialData) => data.grossProfit === 0 ? 'N/A' : formatter.format(data.grossProfit),
            property: 'grossProfit'
        },
        {
            header: 'Net Income',
            accessor: (data: FinancialData) => data.netIncomeLoss === 0 ? 'N/A' : formatter.format(data.netIncomeLoss),
            property: 'netIncomeLoss'
        },
        {
            header: 'Revenue',
            accessor: (data: FinancialData) => data.revenues === 0 ? 'N/A' : formatter.format(data.revenues),
            property: 'revenues'
        },
        {
            header: 'Sales Net Revenue',
            accessor: (data: FinancialData) => data.salesRevenueNet === 0 ? 'N/A' : formatter.format(data.salesRevenueNet),
            property: 'salesRevenueNet'
        },
    ]

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

   

    const columns: TableColumn[] = [
        { header: 'Date', accessor: (data: FinancialData) => data.endDate, property: 'endDate' },
        {
            header: 'Cash',
            accessor: (data: FinancialData) => data.cash === 0 ? 'N/A' : formatter.format(data.cash),
            property: 'cash'
        },
        {
            header: 'CashAndCashEquivalents',
            accessor: (data: FinancialData) => data.cashAndCashEquivalentsAtCarryingValue === 0 ? 'N/A' : formatter.format(data.cashAndCashEquivalentsAtCarryingValue),
            property: 'cashAndCashEquivalentsAtCarryingValue'
        },
        {
            header: 'Assets',
            accessor: (data: FinancialData) => data.assets === 0 ? 'N/A' : formatter.format(data.assets),
            property: 'assets'
        },
        {
            header: 'Liabilities',
            accessor: (data: FinancialData) => data.liabilities === 0 ? 'N/A' : formatter.format(data.liabilities),
            property: 'liabilities'
        },
        {
            header: 'Short Term Investments',
            accessor: (data: FinancialData) => data.shortTermInvestments === 0 ? 'N/A' : formatter.format(data.shortTermInvestments),
            property: 'shortTermInvestments'            
        },
    ]

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

    const columns: TableColumn[] = [
        { header: 'Date', accessor: (data: FinancialData) => data.endDate, property: 'endDate' },
        {
            header: 'Assets',
            accessor: (data: FinancialData) => data.assets === 0 ? 'N/A' : formatter.format(data.assets),
            property: 'assets'
        },
        {
            header: 'Noncurrent Assets',
            accessor: (data: FinancialData) => data.noncurrentAssets === 0 ? 'N/A' : formatter.format(data.noncurrentAssets),
            property: 'noncurrentAssets'
        },
        {
            header: 'Liabilities',
            accessor: (data: FinancialData) => data.liabilities === 0 ? 'N/A' : formatter.format(data.liabilities),
            property: 'liabilities'
        },
        {
            header: 'Long Term Debt',
            accessor: (data: FinancialData) => data.longTermDebt === 0 ? 'N/A' : formatter.format(data.longTermDebt),
            property: 'longTermDebt'
        },
        {
            header: 'Long Term Debt Noncurrent',
            accessor: (data: FinancialData) => data.longTermDebtNoncurrent === 0 ? 'N/A' : formatter.format(data.longTermDebtNoncurrent),
            property:'longTermDebtNoncurrent'
        },
        {
            header: 'Stockholders Equity',
            accessor: (data: FinancialData) => data.stockHoldersEquity === 0 ? 'N/A' : formatter.format(data.stockHoldersEquity),
            property: 'stockHoldersEquity'
        },
    ]

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

    const columns: TableColumn[] = [
        { header: 'Date', accessor: (data: FinancialData) => data.endDate, property: 'endDate' },
        {
            header: 'Sales Net Revenue',
            accessor: (data: FinancialData) => data.salesRevenueNet === 0 ? 'N/A' : formatter.format(data.salesRevenueNet),
            property: 'salesRevenueNet'
        },
        {
            header: 'Operating Income',
            accessor: (data: FinancialData) => data.operatingIncomeLoss === 0 ? 'N/A' : formatter.format(data.operatingIncomeLoss),
            property: 'operatingIncomeLoss'
        },
        {
            header: 'Assets',
            accessor: (data: FinancialData) => data.assets === 0 ? 'N/A' : formatter.format(data.assets),
            property:'assets'
        },
        {
            header: 'StockholdersEquity',
            accessor: (data: FinancialData) => data.stockHoldersEquity === 0 ? 'N/A' : formatter.format(data.stockHoldersEquity),
            property: 'stockHoldersEquity'
        }
    ]

    return (
        <CollapsibleTable
            data={financialDataList}
            columns={columns}
            title="Efficiency Metrics"
            notOpen={true}
        />
    )
}