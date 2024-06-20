#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM registry.bank.lan/dotnet/core/aspnet:3.1 AS base
# add root certificates
ADD vostok-root-sha256.crt /usr/local/share/ca-certificates/vostok-root-sha256.crt
RUN update-ca-certificates
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/BuildingBlocks/HotLineAssistant.Domain/Domain.csproj", "src/BuildingBlocks/HotLineAssistant.Domain/"]
COPY ["src/BuildingBlocks/HotLineAssistant.Common/Common.csproj", "src/BuildingBlocks/HotLineAssistant.Common/"]
COPY ["src/BuildingBlocks/HotLineAssistant.Application/Application.csproj", "src/BuildingBlocks/HotLineAssistant.Application/"]
COPY ["src/BuildingBlocks/HotLineAssistant.WebApi/WebApi.csproj", "src/BuildingBlocks/HotLineAssistant.WebApi/"]
COPY ["src/Services/ContactManager/ContactManager.Infrastructure/ContactManager.Infrastructure.csproj", "src/Services/ContactManager/ContactManager.Infrastructure/"]
COPY ["src/Services/ContactManager/ContactManager.Application/ContactManager.Application.csproj", "src/Services/ContactManager/ContactManager.Application/"]
COPY ["src/Services/ContactManager/ContactManager.Domain/ContactManager.Domain.csproj", "src/Services/ContactManager/ContactManager.Domain/"]
COPY ["src/ContactManager.Api/ContactManager.Api.csproj", "src/ContactManager.Api/"]
RUN dotnet restore "src/ContactManager.Api/ContactManager.Api.csproj"
COPY . .
WORKDIR "src/ContactManager.Api"
RUN dotnet build "ContactManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ContactManager.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ContactManager.Api.dll"]