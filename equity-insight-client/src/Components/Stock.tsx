import React, { useEffect, useState } from 'react';
import { useParams } from 'react-router-dom';
import axios from 'axios';
import { ProfitabilityTable, LiquidityTable, SolvencyTable, EfficiencyTable, RecentFinancesTable } from './StockFinancialTables';
import '../Styles/Stock.css'
import { CustomizableChart } from './CustomizableChart';
import { StockDetails } from '../InterfacesAndTypes';



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
            <div className="relevantAndTableContainer">
                <div className="tableContainer">
                    <div className="card">
                        <div className="card-body">
                            <ProfitabilityTable financialDataList={details.financialDatas} />
                        </div>
                    </div>
                    <div className="card">
                        <div className="card-body">
                            <LiquidityTable financialDataList={details.financialDatas} />
                        </div>
                    </div>
                    <div className="card">
                        <div className="card-body">
                            <SolvencyTable financialDataList={details.financialDatas} />
                        </div>
                    </div>
                    <div className="card">
                        <div className="card-body">
                            <EfficiencyTable financialDataList={details.financialDatas} />
                        </div>
                    </div>
                </div>
                <div className="relevantContainer">
                    <div className="card relevant-card">
                        <div className="card-body">
                            <h5 className="card-title">Most Recent Financials</h5>
                            <RecentFinancesTable financialDataList={details.financialDatas} />
                        </div>
                    </div>
                   
                </div>
            </div>
            <div className="chartContainer">
                <CustomizableChart financialDataList={details.financialDatas} />
            </div>
        </div>
            
    );
}