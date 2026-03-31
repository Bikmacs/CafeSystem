# Образ для запуска сервера
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081 

# Образ для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build 
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CafeAPI/CafeAPI.csproj", "CafeAPI/"]
RUN dotnet restore "./CafeAPI/CafeAPI.csproj"
COPY . .
WORKDIR "/src/CafeAPI"
RUN dotnet build "./CafeAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Сборка и публикация приложения
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CafeAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

#  создание исполняемого образа
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

USER root
RUN mkdir -p /app/wwwroot/images && chown -R $APP_UID /app/wwwroot
USER $APP_UID

ENTRYPOINT ["dotnet", "CafeAPI.dll"]