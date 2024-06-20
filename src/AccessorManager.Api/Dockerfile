#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
# add root certificates
ADD vostok-root-sha256.crt /usr/local/share/ca-certificates/vostok-root-sha256.crt
RUN update-ca-certificates
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/BuildingBlocks/HotLineAssistant.Domain/Domain.csproj", "src/BuildingBlocks/HotLineAssistant.Domain/"]
COPY ["src/BuildingBlocks/HotLineAssistant.Common/Common.csproj", "src/BuildingBlocks/HotLineAssistant.Common/"]
COPY ["src/BuildingBlocks/HotLineAssistant.Application/Application.csproj", "src/BuildingBlocks/HotLineAssistant.Application/"]
COPY ["src/BuildingBlocks/HotlineAssistant.Infrastucture/Infrastucture.csproj", "src/BuildingBlocks/HotlineAssistant.Infrastucture/"]
COPY ["src/BuildingBlocks/HotLineAssistant.WebApi/WebApi.csproj", "src/BuildingBlocks/HotLineAssistant.WebApi/"]
COPY ["src/Services/AccessorManager/AccessorManager.Application/AccessorManager.Application.csproj", "src/Services/AccessorManager/AccessorManager.Application/"]
COPY ["src/Services/AccessorManager/AccessorManager.Infrastructure/AccessorManager.Infrastructure.csproj", "src/Services/AccessorManager/AccessorManager.Infrastructure/"]
COPY ["src/AccessorManager.Api/AccessorManager.Api.csproj", "src/AccessorManager.Api/"]
RUN dotnet restore "src/AccessorManager.Api/AccessorManager.Api.csproj"
COPY . .
WORKDIR "/src/AccessorManager.Api"
RUN dotnet build "AccessorManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AccessorManager.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccessorManager.Api.dll"]