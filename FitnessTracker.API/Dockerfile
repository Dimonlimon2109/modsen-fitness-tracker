# 1. Базовый runtime образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app

# 2. Образ для сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["FitnessTracker.API/FitnessTracker.API.csproj", "FitnessTracker.API/"]
COPY ["FitnessTracker.Application/FitnessTracker.Application.csproj", "FitnessTracker.Application/"]
COPY ["FitnessTracker.Domain/FitnessTracker.Domain.csproj", "FitnessTracker.Domain/"]
COPY ["FitnessTracker.Infrastructure/FitnessTracker.Infrastructure.csproj", "FitnessTracker.Infrastructure/"]
RUN dotnet restore "./FitnessTracker.API/FitnessTracker.API.csproj"
COPY . .
WORKDIR "/src/FitnessTracker.API"
RUN dotnet build "./FitnessTracker.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# 3. Публикация проекта
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./FitnessTracker.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# 4. Финальный образ с SDK и dotnet-ef, для возможности применять миграции
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final

WORKDIR /app

# Устанавливаем dotnet-ef глобально
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Копируем опубликованные dll и файлы из publish
COPY --from=publish /app/publish .

# **ВАЖНО: копируем исходники для возможности создания/применения миграций**
COPY FitnessTracker.API ./FitnessTracker.API
COPY FitnessTracker.Infrastructure ./FitnessTracker.Infrastructure
COPY FitnessTracker.Application ./FitnessTracker.Application
COPY FitnessTracker.Domain ./FitnessTracker.Domain

# Копируем и делаем исполняемым скрипт entrypoint.sh
COPY entrypoint.sh .
RUN chmod +x entrypoint.sh

ENTRYPOINT ["./entrypoint.sh"]
