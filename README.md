# postgres ef core

```sh
dotnet add package Microsoft.EntityFrameworkCore.Design
dotnet restore
dotnet build
dotnet ef migrations add InitialCreate

dotnet ef database update
```