import { FinancialData, TableColumn } from './InterfacesAndTypes';


const columnDefinitions: { [key: string]: TableColumn } = {
    endDate: {
        header: 'Date',
        accessor: (data: FinancialData) => data.endDate,
        property: 'endDate',
    },
    earningsPerShareBasic: {
        header: 'EPS Basic',
        accessor: (data: FinancialData) => data.earningsPerShareBasic === 0 ? 'N/A' : data.earningsPerShareBasic,
        property: 'earningsPerShareBasic',
    },
    earningsPerShareDiluted: {
        header: 'EPS Diluted',
        accessor: (data: FinancialData) => data.earningsPerShareDiluted === 0 ? 'N/A' : data.earningsPerShareDiluted,
        property: 'earningsPerShareDiluted',
    },
    grossProfit: {
        header: 'Gross Profit',
        accessor: (data: FinancialData) => data.grossProfit === 0 ? 'N/A' : data.grossProfit,
        property: 'grossProfit',
    },
    netIncomeLoss: {
        header: 'Net Income',
        accessor: (data: FinancialData) => data.netIncomeLoss === 0 ? 'N/A' : data.netIncomeLoss,
        property: 'netIncomeLoss',
    },
    revenues: {
        header: 'Revenue',
        accessor: (data: FinancialData) => data.revenues === 0 ? 'N/A' : data.revenues,
        property: 'revenues',
    },
    salesRevenueNet: {
        header: 'Sales Net Revenue',
        accessor: (data: FinancialData) => data.salesRevenueNet === 0 ? 'N/A' : data.salesRevenueNet,
        property: 'salesRevenueNet',
    },
    cash: {
        header: 'Cash',
        accessor: (data: FinancialData) => data.cash === 0 ? 'N/A' : data.cash,
        property: 'cash',
    },
    cashAndCashEquivalentsAtCarryingValue: {
        header: 'CashAndCashEquivalents',
        accessor: (data: FinancialData) => data.cashAndCashEquivalentsAtCarryingValue === 0 ? 'N/A' : data.cashAndCashEquivalentsAtCarryingValue,
        property: 'cashAndCashEquivalentsAtCarryingValue',
    },
    assets: {
        header: 'Assets',
        accessor: (data: FinancialData) => data.assets === 0 ? 'N/A' : data.assets,
        property: 'assets',
    },
    liabilities: {
        header: 'Liabilities',
        accessor: (data: FinancialData) => data.liabilities === 0 ? 'N/A' : data.liabilities,
        property: 'liabilities',
    },
    shortTermInvestments: {
        header: 'Short Term Investments',
        accessor: (data: FinancialData) => data.shortTermInvestments === 0 ? 'N/A' : data.shortTermInvestments,
        property: 'shortTermInvestments',
    },
    noncurrentAssets: {
        header: 'Noncurrent Assets',
        accessor: (data: FinancialData) => data.noncurrentAssets === 0 ? 'N/A' : data.noncurrentAssets,
        property: 'noncurrentAssets',
    },
    longTermDebt: {
        header: 'Long Term Debt',
        accessor: (data: FinancialData) => data.longTermDebt === 0 ? 'N/A' : data.longTermDebt,
        property: 'longTermDebt',
    },
    longTermDebtNoncurrent: {
        header: 'Long Term Debt Noncurrent',
        accessor: (data: FinancialData) => data.longTermDebtNoncurrent === 0 ? 'N/A' : data.longTermDebtNoncurrent,
        property: 'longTermDebtNoncurrent',
    },
    stockHoldersEquity: {
        header: 'Stockholders Equity',
        accessor: (data: FinancialData) => data.stockHoldersEquity === 0 ? 'N/A' : data.stockHoldersEquity,
        property: 'stockHoldersEquity',
    },
    operatingIncomeLoss: {
        header: 'Operating Income',
        accessor: (data: FinancialData) => data.operatingIncomeLoss === 0 ? 'N/A' : data.operatingIncomeLoss,
        property: 'operatingIncomeLoss',
    },
}

export function createTableColumns(keys: string[]): TableColumn[] {
    return keys.map(key => columnDefinitions[key]);
}