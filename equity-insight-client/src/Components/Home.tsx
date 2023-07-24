import React, { Component } from 'react';
import SearchBar from './SearchBar';

export default function Home(){
    
    return (
        <div className="d-flex align-items-center justify-content-center vh-100">
            <div>
                <h1 className="display-1 mb-5 text-center">Equity Insights</h1>
                <div className="d-flex justify-content-center mb-3 pt-3">
                    <h6 className="display-6 mr-5">Search for a stock</h6>
                    <SearchBar />
                </div>
            </div>
        </div>
    );
}