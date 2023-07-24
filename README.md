# EquityInsight

This project consists of four main components:
1. DataAccess
2. DataInitializer
3. equity-insight-client
4. WebAPI

### DataAccess
The main purpose of the DataAccess component is to control data access to the Microsoft SQL database. It uses Entity Framework to map the Models to the database tables. There are two main tables: Company and FinancialData. 

### DataInitializer
DataInitializer initializes (and eventually updates) the database. It calls the SEC EDGAR API https://www.sec.gov/edgar/sec-api-documentation, processes the data and updates the database. Here is an example of one of the SEC EDGAR API calls for a companies financials: https://data.sec.gov/api/xbrl/companyfacts/CIK0000789019.json

### equity-insight-client
This component is the front end of the project. It is written in React typescript. The front end calls the C# backend and displays the different finances of certian companies.

### WebAPI
The WebAPI is a C# API that access the Microsoft SQL database through the DataAccess component. 
