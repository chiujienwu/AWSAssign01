create a new console program from template
dotnet new console
dotnet run
--adding NLOG
dotnet add package NLog.Web.AspNetCore
--install EntityFrameworkCore packages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet ef migrations add CreateDatabase
dotnet ef database update
