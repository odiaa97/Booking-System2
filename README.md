# Booking-App
This project is built with Angular 10 served by ASP.NET Core(5.0) Web API in the backend, connected with a Sql Server database.

## Description
This App is for booking-recources.

## Prerequisites
You should have 
[NodeJS](https://nodejs.org/en/),
[npm](https://www.npmjs.com/package/download),
[Angular10 CLI](https://angular.io/cli#installing-angular-cli),
[Dotnet 5.0 SDK](https://dotnet.microsoft.com/download) and
[Sql Server Management Studio ](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)
installed in your PC to run the application

## Installation
1- Clone the github repo ``` git clone https://github.com/odiaa97/Booking-app ```

2- In your terminal go to the project directory ``` cd "Booking System" ```

3- Go to backend project ``` cd API ```

4- Delete old migrations if exist in ```migrations``` folder inside ```API``` project

5- Run the following commands to configure your database:

``` dotnet ef migrations add InitialMigration ``` to create your first migration

``` dotnet ef database update ``` to create your databse and schema

here database is seeded with 1 admin user, 2 roles admin and member and 1 appUserRole for giving the created user admin role

``` Dotnet run ``` to run the backend server



```
>>cd ..
>>cd client
```

6- Install npm if not installed:

``` >>npm install ```

7- go to the Client folder 
```
>>cd src
>>cd app
```

8- Run the angular app ``` ng serve -- open ```

## NOTES:
```
* The app seeds the database with 1 user: 
user1 "P@$$w0rd"

2 Roles: 
admin and member roles

1 UserRole: For giving the user admin role
```
