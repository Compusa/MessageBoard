# MessageBoard
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](https://opensource.org/licenses/MIT)
![](https://github.com/Compusa/MessageBoard/workflows/ASP.NET%20Core%20CI/badge.svg)

RESTful API to serve as the backend for a public message board. This is the very first draft of the message board, and it was created on my spare time (after my working days) in less than a week. 

There is still much left to do, improve or consider such as:

* Docker support
* Pagination
* HATEOAS
* Nullable reference types
* Use authentication instead of passing around a clientId
* Better test coverage and integration tests
* FluentValidation for commands
* Logging and error handling
* Richer and more robust DDD implementation

## Prerequisites
.NET Core 3.1o
https://dotnet.microsoft.com/download/dotnet-core/3.0

## Instructions for building, testing and running the solution
You can build, test and run the solution with The .NET Core command-line interface (CLI). Open up your command prompt/terminal of choice and set the working directory to the folder where `MessageBoard.sln` is located.

### Build
```
dotnet build MessageBoard.sln
```

### Test
```
dotnet test MessageBoard.sln
```

### Run
```
dotnet run --project .\src\MessageBoard.Api
```
The MessageBoard API service will be up and running when the `dotnet run` command has completed. 

The **endpoints** will be available at: https://localhost:5001/api/v1/messages

The **Swagger documentation** will be available at: https://localhost:5001/swagger

## Worth noting (Continious Integration)
This repository uses GitHub Actions/Workflows for continious integration. The status of the ASP.NET Core CI badge indicates the status of the latest push to the master branch. 

The CI workflow performs the following actions for the latest distributions of Mac OS, Ubuntu and Windows:
* Setup .NET Core 3.0
* Build with dotnet
* Test with dotnet
