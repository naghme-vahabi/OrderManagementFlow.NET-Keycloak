# Base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081
RUN apt-get update && apt-get install -y git

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project and referenced .csproj files
COPY Services/OrderService/OrderService.API/OrderService.API.csproj Services/OrderService/OrderService.API/
COPY Services/OrderService/OrderService.Infrastructure/OrderService.Infrastructure.csproj Services/OrderService/OrderService.Infrastructure/
COPY Services/OrderService/OrderService.Application/OrderService.Application.csproj Services/OrderService/OrderService.Application/
COPY Services/OrderService/OrderService.Domain/OrderService.Domain.csproj Services/OrderService/OrderService.Domain/
COPY Shared/Shared.csproj Shared/

# Restore only
RUN dotnet restore Services/OrderService/OrderService.API/OrderService.API.csproj

# Copy the rest of the project
COPY . .

# Build the project
WORKDIR /src/Services/OrderService/OrderService.API
RUN dotnet build OrderService.API.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish OrderService.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OrderService.API.dll"]
