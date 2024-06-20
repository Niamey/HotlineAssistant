#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
# add root certificates
ADD vostok-root-sha256.crt /usr/local/share/ca-certificates/vostok-root-sha256.crt
RUN update-ca-certificates
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Gateway/Client/Counterpart/Counterpart/Counterpart.csproj", "src/Gateway/Client/Counterpart/Counterpart/"]
COPY ["src/Gateway/Client/Cards/Cards.csproj", "src/Gateway/Client/Cards/"]
COPY ["src/Gateway/Client/Accounts/Accounts.csproj", "src/Gateway/Client/Accounts/"]
COPY ["src/Gateway/Client/Agreements/Agreements.csproj", "src/Gateway/Client/Agreements/"]
# COPY ["src/Gateway/Client/Addresses/Addresses.csproj", "src/Gateway/Client/Addresses/"]
# COPY ["src/Gateway/Client/References/References.csproj", "src/Gateway/Client/References/"]
COPY ["src/Gateway/Client/Transactions/Transactions.csproj", "src/Gateway/Client/Transactions/"]
COPY ["src/Core/Core.Common/Core.Common.csproj", "src/Core/Core.Common/"]
COPY ["src/Gateway.Client.WebApi/Gateway.Client.WebApi.csproj", "src/Gateway.Client.WebApi/"]
RUN dotnet restore "src/Gateway.Client.WebApi/Gateway.Client.WebApi.csproj"
COPY . .
WORKDIR "src/Gateway.Client.WebApi"
RUN dotnet build "Gateway.Client.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.Client.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hotline.Gateway.Client.WebApi.dll"]