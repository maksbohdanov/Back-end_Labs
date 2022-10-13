## Requirements
Installed [.NET 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-3.1.424-windows-x64-installer)  
Installed [ASP.NET Core 3.1 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-3.1.30-windows-x64-installer?cid=getdotnetcore)
## Getting Started
In order to download the app and run it, follow this steps:
```
git clone https://github.com/maksbohdanov/Back-end_Lab1
cd Back-end_Lab1\Lab1\Lab1
dotnet run
```
Open https://localhost:5001
### Endpoints:
* Getting a list of categories: `/lab1/categories`
* Getting a list of records for a specific user: `/lab1/records/user/{id}`
* Getting a list of entries in a category for a specific user: `/lab1/records/category/?userId={userId}&categoryId={categoryId}`
* Create a user: `/lab1/users/add`
* Create an expense category: `/lab1/categories/add`
* Creating an expense record: `/lab1/records/add`