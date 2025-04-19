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

# Copy only .csproj files for faster restore caching
COPY Services/PaymentService/PaymentService.API/PaymentService.API.csproj Services/PaymentService/PaymentService.API/
COPY Services/PaymentService/PaymentService.Application/PaymentService.Application.csproj Services/PaymentService/PaymentService.Application/
COPY Services/PaymentService/PaymentService.Infrastructure/PaymentService.Infrastructure.csproj Services/PaymentService/PaymentService.Infrastructure/
COPY Shared/Shared.csproj Shared/

# Restore dependencies
RUN dotnet restore Services/PaymentService/PaymentService.API/PaymentService.API.csproj

# Copy everything else
COPY . .

# Build the project
WORKDIR /src/Services/PaymentService/PaymentService.API
RUN dotnet build PaymentService.API.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish PaymentService.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PaymentService.API.dll"]
