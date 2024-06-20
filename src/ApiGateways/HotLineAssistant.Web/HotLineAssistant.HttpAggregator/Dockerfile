#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/BuildingBlocks/HotLineAssistant.Domain/Domain.csproj", "src/BuildingBlocks/HotLineAssistant.Domain/"]
COPY ["src/BuildingBlocks/HotLineAssistant.Application/Application.csproj", "src/BuildingBlocks/HotLineAssistant.Application/"]
COPY ["src/BuildingBlocks/HotlineAssistant.Infrastucture/Infrastucture.csproj", "src/BuildingBlocks/HotlineAssistant.Infrastucture/"]
COPY ["src/BuildingBlocks/HotLineAssistant.WebApi/WebApi.csproj", "src/BuildingBlocks/HotLineAssistant.WebApi/"]
COPY ["src/ApiGateways/Web.Bff.Assistant/HotLine.Assistant.HttpAggregator/HotLine.Assistant.HttpAggregator.csproj", "src/ApiGateways/Web.Bff.Assistant/HotLine.Assistant.HttpAggregator/"]
RUN dotnet restore "src/ApiGateways/Web.Bff.Assistant/HotLine.Assistant.HttpAggregator/HotLine.Assistant.HttpAggregator.csproj"
COPY . .
WORKDIR "/src/src/ApiGateways/Web.Bff.Assistant/HotLine.Assistant.HttpAggregator"
RUN dotnet build "HotLine.Assistant.HttpAggregator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotLine.Assistant.HttpAggregator.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotLine.Assistant.HttpAggregator.dll"]