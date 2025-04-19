# Base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Optional: install tools (if needed)
RUN apt-get update && apt-get install -y git

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy project files first for faster docker cache usage
COPY Services/CustomerService/CustomerService.API/CustomerService.API.csproj Services/CustomerService/CustomerService.API/
COPY Services/CustomerService/CustomerService.Infrastructure/CustomerService.Infrastructure.csproj Services/CustomerService/CustomerService.Infrastructure/
COPY Services/CustomerService/CustomerService.Application/CustomerService.Application.csproj Services/CustomerService/CustomerService.Application/
COPY Services/CustomerService/CustomerService.Domain/CustomerService.Domain.csproj Services/CustomerService/CustomerService.Domain/

# Restore dependencies
RUN dotnet restore Services/CustomerService/CustomerService.API/CustomerService.API.csproj

# Copy the rest of the files
COPY . .

# Build the app
WORKDIR /src/Services/CustomerService/CustomerService.API
RUN dotnet build CustomerService.API.csproj -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish CustomerService.API.csproj -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final stage
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerService.API.dll"]