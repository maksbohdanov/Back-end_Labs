## Requirements
Installed [.NET 3.1](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/sdk-3.1.424-windows-x64-installer)  
Installed [ASP.NET Core 3.1 Runtime](https://dotnet.microsoft.com/en-us/download/dotnet/thank-you/runtime-aspnetcore-3.1.30-windows-x64-installer?cid=getdotnetcore)
## Getting Started
In order to download the app and run it, follow this steps:
```
git clone https://github.com/maksbohdanov/Back-end_Labs
cd Back-end_Labs\Lab2
dotnet run
```
Open https://localhost:5001
### Endpoints:
* Getting a list of categories: `api/categories`
* Getting a list of custom categories by user : `api/categories?userId=1`
* Getting a list of records for a specific user: `api/records/user/{id}`
* Getting a list of entries in a category for a specific user: `api/records/user/{userId}/category/{categoryId}`
* Create a user: `api/users/add`
* Create an expense category: `api/categories/add`
* Create a custom expense category: `api/users/{userId}/categories/add"`
* Creating an expense record: `api/records/add`
