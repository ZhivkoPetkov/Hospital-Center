FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY ../PatientService.API/ ./
COPY ../PatientService.Domains ../PatientService.Domains/
COPY ../PatientService.Models ../PatientService.Models/
COPY ../PatientService.Data ../PatientService.Data/

RUN dotnet restore PatientService.API.csproj

RUN dotnet publish PatientService.API.csproj -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "PatientService.API.dll"]