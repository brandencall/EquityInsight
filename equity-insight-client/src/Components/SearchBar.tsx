import React, { useState, useEffect } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';
import TextField from '@mui/material/TextField';
import Autocomplete from '@mui/material/Autocomplete';

interface Stock {
    name: string;
    ticker: string;
}

export default function SearchBar() {
    const [stockList, setStockList] = useState<Stock[]>([]);
    const navigate = useNavigate();

    const fetchStockList = async () => {
        try {
            const response = await axios.get('https://localhost:7052/api/Company');
            setStockList(response.data);
        } catch (error) {
            console.error("Error fetching data: ", error);
        }
    }


    const handleStockSelect = (event: any, value: Stock | null) => {
        if (value) {
            navigate(`/stock/${value.ticker}`);
        }
    };

    useEffect(() => {
        fetchStockList();
    }, []);

    return (
        <Autocomplete
            options={stockList}
            getOptionLabel={(option) => `${option.name} (${option.ticker})`}
            onChange={handleStockSelect}
            style={{ width: 300 }}
            renderInput={(params) => <TextField {...params} label="Search for a stock" variant="outlined" />}
        />
    );
}