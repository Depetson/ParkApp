# 1. How to run:
1) Navigate to solution directory
2) Open command prompt
3) Execute comman "dotnet run"

or

Simply use ***RunApp.bat*** script from solution directory

Note that EF Core code first with mssql database is used, so setup connection string in *appsettings* if need. Also, database will seed automatically, to change seed behaviour change **DbSeedOptions** in *appsettings*

Swagger: **http://localhost:5026/swagger/index.html**


# 2. Assumptions:
1) Client or clients are some kind of devices on parking, like information board with free parking spaces, parking gate or console for non technical parking worker.
2) Car is charged per every started minute with precision to seconds.
3) Since there is no authorization the internal errors are not returned to the client.
4) VehicleReg and SpaceNumber must be unique.
5) VehicleReg can be in any format.
6) Not collecting historical data.
7) **/parking** route should refer to **parking** resource, but in this case it would be just artificial object.
8) Datetime is in local time.
9) Body for park car should contains two variables, there can be only one marked **[FromBody]** so I used dto request object to wrap the, tuple can be used instead.


# 3. Questions:
1) Does vehicleReg have a specific format?
2) What messages should be returned to client?
3) Will client handle exception on it's side somehow?
4) Will you want to add vehicle types in future or change prices?
5) Why POST instead of PUT or PATCH?
