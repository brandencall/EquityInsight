export interface FinancialData {
    id: number;
    assets: number;
    cash: number;
    cashAndCashEquivalentsAtCarryingValue: number;
    earningsPerShareBasic: number;
    earningsPerShareDiluted: number;
    endDate: string;
    grossProfit: number;
    liabilities: number;
    liabilitiesAndStockholdersEquity: number;
    longTermDebt: number;
    longTermDebtNoncurrent: number;
    netIncomeLoss: number;
    noncurrentAssets: number;
    operatingIncomeLoss: number;
    revenues: number;
    salesRevenueNet: number;
    shortTermInvestments: number;
    stockHoldersEquity: number;
}

export interface StockDetails {
    name: string;
    ticker: string;
    nextEarnings: string;
    financialDatas: FinancialData[];
}

export interface TableColumn {
    header: string;
    accessor: (data: FinancialData) => any;
    property: keyof FinancialData;
}

export interface FinancialTableProps {
    financialDataList: FinancialData[]
}

export type SortDirection = 'asc' | 'desc';

export interface SortConfig {
    key: keyof FinancialData;
    direction: SortDirection;
}

export interface TableProps {
    data: FinancialData[];
    columns: TableColumn[];
    title: string;
    notOpen: boolean;
}