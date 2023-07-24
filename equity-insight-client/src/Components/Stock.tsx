import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { ProfitabilityTable, LiquidityTable, SolvencyTable, EfficiencyTable } from './StockFinancialTables';
import '../Styles/Stock.css'


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


export default function Stock() {

    const { ticker } = useParams();
    const [details, setDetails] = useState<StockDetails | null>(null);

    useEffect(() => {
        const fetchDetails = async () => {
            const response = await axios.get(`https://localhost:7052/api/Company/ticker/${ticker}`);
            setDetails(response.data);
        };

        fetchDetails();
    }, [ticker]);

    if (!details) {
        return <div>Loading...</div>;
    } 

    
    return (
        <div className="stockContainer">
            <h1>{details.name} ({details.ticker})</h1>
            <ProfitabilityTable financialDataList={details.financialDatas} />
            <LiquidityTable financialDataList={details.financialDatas} />
            <SolvencyTable financialDataList={details.financialDatas} />
            <EfficiencyTable financialDataList={details.financialDatas} />
        </div>
    );
}