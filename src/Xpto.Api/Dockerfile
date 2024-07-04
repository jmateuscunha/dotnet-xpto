FROM mcr.microsoft.com/dotnet/sdk:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["./Xpto.Api/Xpto.Api.csproj", "./Xpto.Api/"]
RUN dotnet restore "./Xpto.Api/Xpto.Api.csproj"

COPY . .
WORKDIR "./Xpto.Api"
RUN dotnet build "Xpto.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Xpto.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Xpto.Api.dll"]
