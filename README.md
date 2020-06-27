# Publicator
Publicator is portal for users who want to share his history
# Installing and using
All needed preinstalled software:
- IIS Express
- Microsoft Sql Server

Used frameworks and libraries
- .NET Core 3.0
- ASP NET Core 3.0
- Entity Framework Core 3.0
- ReactJS
- Redux

Installation instructions:
1. clone the repository 
2. restore dependencies with command: `dotnet restore`
3. create database schema(from src/Publicator.Presentation directory): 
   - `update-database -project Publicator.Insfrastructure`
4. run using command(from src/Publicator.Presentation directory): `dotnet run`
