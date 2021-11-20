# minimal-api
.Net 6 minimal api sample, connecting to a postgres database, with jwt authentication and authorization.

## Context

- Fast up and running, less ceremony, [minimal api].
- [Open API Specification] with Swagger UI.
- [JWT] Authentication and Authorization.

## Install

- [.Net 6 SDK]
- [EF 6 Tool]
- [Docker]

## Run

- Start postgres container.
    - `docker-compose up`
- To generate a new ef bundle, delete the existing one.
    - `dotnet ef migrations add InitialCreate`
- Inside the directory containing your .csproj.
    - `dotnet ef migrations bundle`
- Finally, execute the application.
    - Inside the project directory: `dotnet run`
    - Root directory: `dotnet run --project /minimal-api/`

<!-- Links -->
[minimal api]: https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis?view=aspnetcore-6.0
[Open API Specification]: https://swagger.io/specification/
[JWT]: https://jwt.io/
[.Net 6 SDK]: https://dotnet.microsoft.com/download/dotnet/6.0
[EF 6 Tool]: https://www.nuget.org/packages/dotnet-ef/
[Docker]: https://docs.docker.com/get-docker