# CustomerAPI

## Introduction
A simple API built with ASP.Net core 5, which will be used for opening a new “current account” of already existing customers.

## Endpoints
* api/customeraccounts/{customerId}/currentaccount/{initialCredit}
* api/customers/{customerId}

## Endpoint Behaviour
* The api/customeraccounts/{customerId}/currentaccount/{initialCredit} will be used for creating a new "current account" of an
existing customer(customerId). If the initalCredit parameter is not 0, then a transaction will be sent to the new account. If 
initialCredit is 0 then no transaction will take place.
* The api/customers/{customerId} will be used for customer account information retrieval. If the customerId is null an exception 
will be thrown, otherwise if the customerId is not null, and the corresponding customer exists in database, the customer's FirstName,
LastName, Balance, Accounts and a list of transactions for each account will be retrieved.

## Technologies Used
* Asp.Net core 5
* Entity Framework
* SQLite
* Automapper
* WebApplicationFactory: Factory for bootstrapping an application in memory for functional end to end tests.
* CI/CD: The project is deployed in Microsoft's Azure.

## Azure URL
https://bvcustomerapi.azurewebsites.net

## Contributor
[Vasilis Dimitriou](https://github.com/Vasilisdm)
