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

## CI/CD
Application makes use of Continuous Integration and Delivery by being deployed in Microsoft's Azure. Any commit made to Github repo will trigger a build process in
Azure. After a successful build, the new version of the application will be running and can be access from the link provided in the Azure url section.

## Azure URL
https://bvcustomerapi.azurewebsites.net

## Set application up locally
In order to set the application locally you have to download [.Net 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0) 
and Microsoft's [visual studio](https://visualstudio.microsoft.com/downloads/). 
After that clone/download the repository to your machine, open the application in visual studio wait untill all the packages are restored and just click run(play
button). The application will run regardless your platform's operating system(Windows, Mac)

## Make requests to endpoints
* To test the application locally, first you have to run it through your visual studio instance, after that the swagger UI will appear, in a new page in your
  browser, and what's left is for to fill the required fieds. You can retrieve the CustomerId of one of the 2 pre existing users by navigating to AccountContext
  class within DbContext folder of the project.
  
* To test the live version of the application all you have to do is visit the following link [CustomerAPI](https://bvcustomerapi.azurewebsites.net/swagger/index.html), the swagger's UI will appear and the same steps from the previous bullet point take place.

* Lastly you can make manual requests to the API's endpoints by using Postman.

## Contributor
[Vasilis Dimitriou](https://github.com/Vasilisdm)
