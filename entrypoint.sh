#!/bin/sh

# Применяем миграции
echo "Applying database migrations..."
dotnet ef database update --project FitnessTracker.Infrastructure --startup-project FitnessTracker.API


# Запускаем приложение
echo "Starting application..."
exec dotnet FitnessTracker.API.dll