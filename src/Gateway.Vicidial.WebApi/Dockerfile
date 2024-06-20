#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
# add root certificates
ADD vostok-root-sha256.crt /usr/local/share/ca-certificates/vostok-root-sha256.crt
RUN update-ca-certificates
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Gateway.Vicidial/Gateway.Vicidial.csproj", "src/Gateway.Vicidial/"]
COPY ["src/Core/Core.Common/Core.Common.csproj", "src/Core/Core.Common/"]
COPY ["src/Gateway.Vicidial.WebApi/Gateway.Vicidial.WebApi.csproj", "src/Gateway.Vicidial.WebApi/"]
RUN dotnet restore "src/Gateway.Vicidial.WebApi/Gateway.Vicidial.WebApi.csproj"
COPY . .
WORKDIR "src/Gateway.Vicidial.WebApi"
RUN dotnet build "Gateway.Vicidial.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gateway.Vicidial.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hotline.Gateway.Vicidial.WebApi.dll"]