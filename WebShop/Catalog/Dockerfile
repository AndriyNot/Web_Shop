# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Копіюємо весь контекст (зміст каталогу з усіма папками та файлами)
COPY . /src

# Встановлюємо робочий каталог на той, де знаходиться проект
WORKDIR /src/WebShop/Catalog

# Відновлюємо залежності для проекту
RUN dotnet restore "Catalog.csproj"

# Публікуємо проект в режимі Release
RUN dotnet publish "Catalog.csproj" -c Release -o /app

# Final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:8.0

# Встановлюємо робочий каталог в контейнері
WORKDIR /app

# Копіюємо публіковані файли з етапу "build"
COPY --from=build /app .

# Вказуємо точку входу для запуску застосунку
ENTRYPOINT ["dotnet", "Catalog.dll"]
