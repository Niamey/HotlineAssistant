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
COPY ["src/BuildingBlocks/HotLineAssistant.WebApi/WebApi.csproj", "src/BuildingBlocks/HotLineAssistant.WebApi/"]
COPY ["src/Services/CardManager/CardManager.Application/CardManager.Application.csproj", "src/Services/CardManager/CardManager.Application/"]
COPY ["src/Services/CardManager/CardManager.Domain/CardManager.Domain.csproj", "src/Services/CardManager/CardManager.Domain/"]
COPY ["src/Services/CardManager/CardManager.Infrastructure/CardManager.Infrastructure.csproj", "src/Services/CardManager/CardManager.Infrastructure/"]
COPY ["src/CardManager.Api/CardManager.Api.csproj", "src/CardManager.Api/"]
RUN dotnet restore "src/CardManager.Api/CardManager.Api.csproj"
COPY . .
WORKDIR "/src/CardManager.Api"
RUN dotnet build "CardManager.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CardManager.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CardManager.Api.dll"]