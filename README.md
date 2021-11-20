# minimal-api
.Net 6 minimal api sample.

## Install

- [.Net 6 SDK]
- [EF 6 Tool]
- [Docker]

## Run

- To generate a new ef bundle, delete the existing one.
    - `dotnet ef migrations add InitialCreate`
- Inside the directory containing your .csproj.
    - `dotnet ef migrations bundle`
- Finally, execute the application.
    - Inside the project directory: `dotnet run`
    - Root directory: `dotnet run --project /minimal-api/`

<!-- Links -->
[.Net 6 SDK]: https://dotnet.microsoft.com/download/dotnet/6.0
[EF 6 Tool]: https://www.nuget.org/packages/dotnet-ef/
[Docker]: https://docs.docker.com/get-docker