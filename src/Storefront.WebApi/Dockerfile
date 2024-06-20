#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
# add root certificates
ADD vostok-root-sha256.crt /usr/local/share/ca-certificates/vostok-root-sha256.crt
RUN update-ca-certificates
WORKDIR /app

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Storefront/Storefront.csproj", "src/Storefront/"]
COPY ["src/Core/Core.Common/Core.Common.csproj", "src/Core/Core.Common/"]
COPY ["src/Storefront.WebApi/Storefront.WebApi.csproj", "src/Storefront.WebApi/"]
RUN dotnet restore "src/Storefront.WebApi/Storefront.WebApi.csproj"
COPY . .
WORKDIR "src/Storefront.WebApi"
RUN dotnet build "Storefront.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Storefront.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Hotline.Storefront.WebApi.dll"]