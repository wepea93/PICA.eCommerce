#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["eCommerce.AuthorizerUser.Api/eCommerce.AuthorizerUser.Api.csproj", "eCommerce.AuthorizerUser.Api/"]
RUN dotnet restore "eCommerce.AuthorizerUser.Api/eCommerce.AuthorizerUser.Api.csproj"
COPY . .
WORKDIR "/src/eCommerce.AuthorizerUser.Api"
RUN dotnet build "eCommerce.AuthorizerUser.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eCommerce.AuthorizerUser.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eCommerce.AuthorizerUser.Api.dll"]