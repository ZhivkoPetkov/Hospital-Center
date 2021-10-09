FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ../VaccineService.API/ ./
COPY ../VaccineService.Domains ../VaccineService.Domains/
COPY ../VaccineService.Models ../VaccineService.Models/
COPY ../VaccineService.Data ../VaccineService.Data/
COPY ../VaccineService.Services ../VaccineService.Services/

RUN dotnet restore VaccineService.API.csproj

RUN dotnet publish VaccineService.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "VaccineService.API.dll"]