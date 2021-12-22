FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/API/Auth/Auth.csproj", "API/Auth/"]
COPY ["src/Infrastructure/Jwt.Extensions.Autofac/Jwt.Extensions.Autofac.csproj", "Infrastructure/Jwt.Extensions.Autofac/"]
COPY ["src/Infrastructure/Jwt/Jwt.csproj", "Infrastructure/Jwt/"]
RUN dotnet restore "API/Auth/Auth.csproj"
COPY . .
WORKDIR "src/API/Auth"

RUN dotnet build "Auth.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Auth.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Auth.dll"]
