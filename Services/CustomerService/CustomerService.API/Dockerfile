FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src


COPY ../../../../Shared ./Shared
COPY ../CustomerService.Application ./CustomerService.Application
COPY ../CustomerService.Domain ./CustomerService.Domain
COPY ../CustomerService.Infrastructure ./CustomerService.Infrastructure
COPY . ./CustomerService.API
COPY ../../OrderService/OrderService.Domain ./OrderService.Domain


RUN dotnet restore "./CustomerService.API/CustomerService.API.csproj"


WORKDIR "/src/CustomerService.API"
RUN dotnet build "CustomerService.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "CustomerService.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CustomerService.API.dll"]