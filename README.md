This project was bootstrapped with ASP.NET Core Web API for the backend development and React Framework for frontend development.

## Installation

In order to get the project to run you have to follow a few setps:

i. You two option. First, you can import the database backup file in the API folder "InterviewTask.bak" in order to create the database and load its dummy data. Second is one is you can run the project and apply the command "Update-Database" against the ITI.Enterpirse.InterviewTask.DataModel through the nuget package manager console or the cmd to create the database without the dummy data.

ii. Make the ITI.Enterpirse.InterviewTask.Api as the start up project.

iii. Run the project through Visual Studio to host it on IIS Express or through cmd using the run command "dotnet run"

iv. Once the project starts nagivate to the hostip/index.html for the documentation. // For example http://62.210.5.62:5000/index.html

v. Follow up the API documentation and you are good to go! 

## Sample Requests

* GET /api/companies
   http://62.210.5.62:5000/api/companies

* POST /api/companies 
    http://62.210.5.62:5000/api/companies

* GET /api/products
    http://62.210.5.62:5000/api/products

* PUT api/products
    http://62.210.5.62:5000/api/products

* DELETE api/products
    http://62.210.5.62:5000/api/products

* POST api/products
    http://62.210.5.62:5000/api/products