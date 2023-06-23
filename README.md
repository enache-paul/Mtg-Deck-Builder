# Magic The Gathering Deck Builder

## This is a *ASP.NET Core MVC App*

## How to run

### Pull both Mtg & Mtg-lib in a directory

Install Postgres
> https://www.guru99.com/download-install-postgresql.html

Using Datagrip:
> Create a new datasource of type *Postgres* 

> Right click on the newly created datasource -> New -> Database -> Configure it "Host=localhost;Port=5432;
Database=mtg;Username=postgres;Password=postgres" 

> Right click on your mtg databse and run all SQL scripts (SQL Scripts -> Run SQL script)

### Scaffold the existing PostgreDb
Go to the parent directory and browse to mtg_lib

 > `cd mtg_lib`

Before executing the commad bellow,

 > `dotnet ef dbcontext scaffold "Host=localhost;Port=5432;Database=mtg;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL --output-dir Data`

### Run the following commands
> `cd ..`

> `dotnet new sln`

> `dotnet sln add mtg`

> `dotnet sln add mtg_lib`

> `dotnet add mtg reference mtg_lib`

> `cd mtg`

> `dotnet run`
