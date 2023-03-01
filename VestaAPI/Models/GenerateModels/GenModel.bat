set mypath=%cd%

dotnet script %mypath%\PocosGenerator.csx -- output:DBModel.cs namespace:VestaAPI.DBModel config:..\appsettings.json connectionstring:ConnectionStrings:DefaultConnection dapper:true
