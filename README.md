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
.NET Core 3.1
https://dotnet.microsoft.com/download/dotnet-core/3.1

## Instructions for building, testing and running the solution
You can build, test and run the solution with The .NET Core command-line interface (CLI). Open up your command prompt/terminal of choice and be sure to set the folder where `MessageBoard.sln` is located as the working directory.

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

#### Endpoints
The MessageBoard API service will be up and running when the command has completed, and the endpoints will be available at:
https://localhost:5001/api/v1/messages

#### Documentation with Swagger
The Message Board API is documented with Swagger and you can explore and test the API at:
https://localhost:5001/swagger
